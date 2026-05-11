#include "components/authentication.hpp"
#include "blaze/tdf.hpp"
#include "network/client_connection.hpp"
#include "utils/logger.hpp"
#include <random>
#include <sstream>
#include <iomanip>

namespace ds2::components {

Authentication::Authentication()
    : Component(blaze::ComponentId::Authentication, "Authentication")
{
}

std::unique_ptr<blaze::Packet> Authentication::handlePacket(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    uint16_t command = request.getCommand();
    
    switch (static_cast<blaze::AuthCommand>(command)) {
        case blaze::AuthCommand::login:
            return handleLogin(request, client);

        case blaze::AuthCommand::expressLogin:
            return handleExpressLogin(request, client);

        case blaze::AuthCommand::logout:
            return handleLogout(request, client);

        case blaze::AuthCommand::getPersona:
            return handleGetPersona(request, client);

        case blaze::AuthCommand::listPersonas:
            return handleListPersonas(request, client);

        case blaze::AuthCommand::getOriginPersona:
            return handleGetOriginPersona(request, client);

        default:
            LOG_WARN("[Auth] Unknown command: 0x{:04X}", command);
            return request.createReply();
    }
}

std::unique_ptr<blaze::Packet> Authentication::handleLogin(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    auto requestTdf = request.getPayloadAsTdf();

    std::string email;
    if (auto it = requestTdf.find("MAIL"); it != requestTdf.end()) {
        if (it->second && it->second->type == blaze::TdfType::String)
            email = std::get<blaze::TdfString>(it->second->value);
    }

    // Generate session
    uint64_t sessionId = generateSessionId();
    uint64_t userId = sessionId;  // For now, use session ID as user ID
    std::string authToken = generateAuthToken();
    std::string personaName = email.empty() ? "Player" : email.substr(0, email.find('@'));

    // Store session
    {
        std::lock_guard<std::mutex> lock(m_sessionMutex);
        m_sessions[sessionId] = client;
    }

    // Update client state
    client->setSessionId(sessionId);
    client->setUserId(userId);
    client->setPersonaName(personaName);
    client->setConnectionState(blaze::ConnectionState::AUTHENTICATED);

    blaze::TdfBuilder builder;
    builder
        .boolean("ANON", false)
        .boolean("NTOS", false)
        .beginStruct("SESS")
            .boolean("1CON", false)
            .int64("BUID", userId)
            .boolean("FRST", false)
            .string("KEY", authToken)
            .int64("LLOG", 0)
            .string("MAIL", email)
            .beginStruct("PDTL")
                .string("DSNM", personaName)
                .uint32("LAST", 1612345678)
                .int64("PID", userId)
                .int64("PLAT", 4) //pc
                .int64("STAS", 2) //ACTIVE
                .uint64("XREF", 1234)
            .endStruct()
            .int64("UID", userId)
        .endStruct()
        .boolean("SPAM", true)
        .boolean("UNDR", false);


    auto reply = request.createReply();
    reply->setPayload(builder.build());

    LOG_INFO("[auth] login persona={} session={}", personaName, sessionId);

    client->sendPacket(std::move(reply));
    sendUserAuthenticatedNotification(client, userId, authToken, personaName);
    return nullptr;
}

void Authentication::sendUserAuthenticatedNotification(
    std::shared_ptr<network::ClientConnection> client,
    uint64_t userId,
    const std::string& sessionKey,
    const std::string& personaName
) {

    blaze::TdfBuilder builder;
    builder
        .boolean("1CON", false)
        .uint32("ALOC", 1701729619) // 'enUS' packed locale
        .int64("BUID", userId)
        .pair("CGID", 0, 0) //objectId type?
        .string("DSNM", personaName)
        .boolean("FRST", false)
        .string("KEY",  sessionKey)
        .uint32("LAST", 1612345678)
        .int64("LLOG", 1234)
        .string("MAIL", "")
        .string("NASP", "cem_ea_id")
        .int64("PID", userId)
        .int64("PLAT", 4) //pc
        .int64("UID", userId)
        .int64("USTP", 0)
        .uint64("XREF", 1234);

    auto notif = std::make_unique<blaze::Packet>(
        blaze::ComponentId::UserSessions,
        /*command (notif id)*/ 8,
        blaze::MessageType::Notification,
        m_nextNotifMsgNum++
    );
    notif->setPayload(builder.build());
    client->sendPacket(std::move(notif));
    LOG_INFO("[auth] pushed UserAuthenticated notif (uid={})", userId);
}


std::unique_ptr<blaze::Packet> Authentication::handleExpressLogin(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {

    auto requestTdf = request.getPayloadAsTdf();

    std::string authCode;
    std::string personaName = "PvZPlayer";

    if (auto it = requestTdf.find("AUTH"); it != requestTdf.end()) {
        if (it->second && it->second->type == blaze::TdfType::String)
            authCode = std::get<blaze::TdfString>(it->second->value);
    }
    if (auto it = requestTdf.find("PNAM"); it != requestTdf.end()) {
        if (it->second && it->second->type == blaze::TdfType::String)
            personaName = std::get<blaze::TdfString>(it->second->value);
    }

    uint64_t sessionId = generateSessionId();
    uint64_t userId = sessionId;
    std::string newToken = generateAuthToken();

    {
        std::lock_guard<std::mutex> lock(m_sessionMutex);
        m_sessions[sessionId] = client;
    }

    client->setSessionId(sessionId);
    client->setUserId(userId);
    client->setPersonaName(personaName);
    client->setConnectionState(blaze::ConnectionState::AUTHENTICATED);

    blaze::TdfBuilder builder;
    builder
        .integer("AGUP", 0)
        .string("LDHT", "")
        .integer("NTOS", 0)
        .string("PCTK", newToken)
        .string("PRIV", "")
        .beginStruct("SESS")
            .integer("BUID", userId)
            .integer("FRST", 0)
            .string("KEY", newToken)
            .integer("LLOG", 0)
            .string("MAIL", "")
            .beginStruct("PDTL")
                .string("DPTS", "")
                .integer("EXID", 0)
                .integer("GTYP", 0)
                .string("MAIL", "")
                .integer("PID", userId)
                .string("PNAM", personaName)
            .endStruct()
            .integer("UID", userId)
        .endStruct()
        .integer("SPAM", 0)
        .string("THST", "")
        .string("TSUI", "")
        .string("TURI", "");

    auto reply = request.createReply();
    reply->setPayload(builder.build());

    LOG_INFO("[auth] expressLogin persona={} session={}", personaName, sessionId);

    client->sendPacket(std::move(reply));
    sendUserAuthenticatedNotification(client, userId, newToken, personaName);
    return nullptr;
}

std::unique_ptr<blaze::Packet> Authentication::handleLogout(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    LOG_INFO("[auth] logout session={}", client->getSessionId());
    
    uint64_t sessionId = client->getSessionId();
    
    {
        std::lock_guard<std::mutex> lock(m_sessionMutex);
        m_sessions.erase(sessionId);
    }
    
    client->setSessionId(0);
    client->setUserId(0);
    client->setPersonaName("");
    client->setConnectionState(blaze::ConnectionState::CONNECTED);
    
    // Empty response
    auto reply = request.createReply();
    return reply;
}

std::unique_ptr<blaze::Packet> Authentication::handleGetPersona(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    LOG_INFO("[auth] getPersona session={}", client->getSessionId());

    uint64_t userId = client->getUserId();
    std::string personaName = client->getPersonaName();

    blaze::TdfBuilder builder;
    builder
        .beginStruct("PDTL")
            .string("DPTS", "")
            .integer("EXID", 0)
            .integer("GTYP", 0)
            .string("MAIL", "")
            .integer("PID", userId)
            .string("PNAM", personaName)
        .endStruct();

    auto reply = request.createReply();
    reply->setPayload(builder.build());

    return reply;
}

std::unique_ptr<blaze::Packet> Authentication::handleListPersonas(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    LOG_INFO("[auth] listPersonas session={}", client->getSessionId());

    uint64_t userId = client->getUserId();
    std::string personaName = client->getPersonaName();
    if (personaName.empty()) personaName = "PvZPlayer";

    // TdfBuilder has no list-of-structs API, so build the PLST field manually.
    blaze::TdfStruct pdtl;
    pdtl["DPTS"] = std::make_shared<blaze::TdfValue>("DPTS", blaze::TdfType::String,  blaze::TdfString(""));
    pdtl["EXID"] = std::make_shared<blaze::TdfValue>("EXID", blaze::TdfType::Integer, blaze::TdfInteger(0));
    pdtl["GTYP"] = std::make_shared<blaze::TdfValue>("GTYP", blaze::TdfType::Integer, blaze::TdfInteger(0));
    pdtl["MAIL"] = std::make_shared<blaze::TdfValue>("MAIL", blaze::TdfType::String,  blaze::TdfString(""));
    pdtl["PID"]  = std::make_shared<blaze::TdfValue>("PID",  blaze::TdfType::Integer, blaze::TdfInteger(static_cast<int64_t>(userId)));
    pdtl["PNAM"] = std::make_shared<blaze::TdfValue>("PNAM", blaze::TdfType::String,  blaze::TdfString(personaName));

    blaze::TdfList personaList;
    personaList.push_back(std::make_shared<blaze::TdfValue>("", blaze::TdfType::Struct, pdtl));

    blaze::TdfStruct root;
    root["NTOS"] = std::make_shared<blaze::TdfValue>("NTOS", blaze::TdfType::Integer, blaze::TdfInteger(0));
    root["PLST"] = std::make_shared<blaze::TdfValue>("PLST", blaze::TdfType::List,    personaList);

    auto reply = request.createReply();
    reply->setPayload(root);


    return reply;
}

std::unique_ptr<blaze::Packet> Authentication::handleGetOriginPersona(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    LOG_INFO("[auth] getOriginPersona session={}", client->getSessionId());

    uint64_t userId = client->getUserId();
    std::string personaName = client->getPersonaName();
    if (personaName.empty()) personaName = "PvZPlayer";

    blaze::TdfBuilder builder;
    builder
        .beginStruct("PDTL")
            .string("DPTS", "")
            .integer("EXID", 0)
            .integer("GTYP", 0)
            .string("MAIL", "")
            .integer("PID", userId)
            .string("PNAM", personaName)
        .endStruct();

    auto reply = request.createReply();
    reply->setPayload(builder.build());

    return reply;
}

uint64_t Authentication::generateSessionId() {
    std::lock_guard<std::mutex> lock(m_sessionMutex);
    return m_nextSessionId++;
}

std::string Authentication::generateAuthToken() {
    static std::random_device rd;
    static std::mt19937_64 gen(rd());
    static std::uniform_int_distribution<uint64_t> dis;
    
    std::stringstream ss;
    ss << std::hex << std::setfill('0');
    ss << std::setw(16) << dis(gen);
    ss << std::setw(16) << dis(gen);
    
    return ss.str();
}

} // namespace ds2::components
