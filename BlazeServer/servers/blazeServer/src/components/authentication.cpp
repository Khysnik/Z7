#include "components/authentication.hpp"
#include "blaze/tdf.hpp"
#include "network/client_connection.hpp"
#include "utils/logger.hpp"
#include "config.hpp"

namespace gw2::components {

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

        case blaze::AuthCommand::getAuthToken:
            return handleGetAuthToken(request, client);

        case blaze::AuthCommand::listPersonas:
            return handleListPersonas(request, client);

        case blaze::AuthCommand::getOriginPersona:
            return handleGetOriginPersona(request, client);

        default:
            LOG_WARN("[Auth] Unknown command: 0x{:04X}", command);
            return request.createReply();
    }
}


// Game client sends auth token, server responds with user info and session details.

std::unique_ptr<blaze::Packet> Authentication::handleLogin(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {

    client->setUserId(config::blazeId);
    client->setSessionId(config::blazeId);
    client->setPersonaName(config::persona);
    client->setConnectionState(blaze::ConnectionState::AUTHENTICATED);

    blaze::TdfBuilder builder;
    builder
        .integer("ANON", 0) // isAnonymous
        .beginStruct("SESS")
            .integer("1CON", 0)
            .integer("BUID", config::blazeId)
            .integer("FRST", 0)
            .string("KEY", config::sessionKey)
            .integer("LLOG", 3557254017)
            .string("MAIL", config::email)
            .beginStruct("PDTL")
                .string("DSNM", config::persona)
                .integer("LAST", 0)
                .integer("PID", config::blazeId)
                .integer("PLAT", 4)
                .integer("STAS", 0)
                .integer("XREF", config::nucleusId)
            .endStruct()
            .integer("UID", config::nucleusId)
        .endStruct()
        .integer("SPAM", 0)
        .integer("UNDR", 0);

    auto reply = request.createReply();
    reply->setPayload(builder.build());

    LOG_INFO("[auth] login persona={}", config::persona);

    sendUserAuthenticatedNotification(client);
    client->sendPacket(std::move(reply));
    sendUserAddedNotification(client);
    return nullptr;
}


void Authentication::sendUserAuthenticatedNotification(
    std::shared_ptr<network::ClientConnection> client
) {

    blaze::TdfBuilder builder;
    builder
        .integer("1CON", 0)
        .integer("ALOC", config::locale)
        .integer("BUID", config::blazeId)
        .objectId("CGID", /*component=*/0x7802, /*type=*/0x0002, /*id=*/config::connGroupId)
        .string("DSNM", config::persona)
        .integer("FRST", 0)
        .string("KEY",  config::sessionKey)
        .integer("LAST", 3557254017)
        .integer("LLOG", 3557264656)
        .string("MAIL", config::email)
        .string("NASP", config::nasp)
        .integer("PID", config::blazeId)
        .integer("PLAT", 4)
        .integer("UID", config::nucleusId)
        .integer("USTP", 0)
        .integer("XREF", config::nucleusId);

    auto notif = std::make_unique<blaze::Packet>(
        blaze::ComponentId::UserSessions,
        /*command (notif id)*/ 8,
        blaze::MessageType::Notification,
        m_nextNotifMsgNum++
    );
    notif->setPayload(builder.build());
    client->sendPacket(std::move(notif));
}

void Authentication::sendUserAddedNotification(
    std::shared_ptr<network::ClientConnection> client
) {
    blaze::TdfBuilder builder;
    builder
        .beginStruct("DATA")
            .unionValue("ADDR", 0x7F, nullptr)
            .string("BPS", "")
            .string("CTY", "")
            .variable("CVAR", nullptr)
            .integerMap("DMAP", {
                {"4026793985", 0},
                {"917509",     94763},
                {"917510",     0},
                {"917511",     129},
            })
            .integer("HWFG", 0)
            .string("ISP", "")
            .beginStruct("QDAT")
                .integer("BWHR", 0)
                .integer("DBPS", 0)
                .integer("NAHR", 0)
                .integer("NATT", 0)
                .integer("UBPS", 0)
            .endStruct()
            .string("TZ", "")
            .integer("UATT", 0)
            .objectIdList("ULST", {{0x7802, 0x0002, static_cast<uint64_t>(config::connGroupId)}})
        .endStruct()
        .beginStruct("USER")
            .integer("AID",  config::nucleusId)
            .integer("ALOC", config::locale)
            .binary("EXBB", {})
            .integer("EXID", config::nucleusId)
            .integer("ID",   config::blazeId)
            .string("NAME", config::persona)
            .string("NASP", config::nasp)
            .integer("ORIG", config::blazeId)
            .integer("PIDI", config::nucleusId)
        .endStruct();

    auto notif = std::make_unique<blaze::Packet>(
        blaze::ComponentId::UserSessions,
        /*command (notif id)*/ 2,
        blaze::MessageType::Notification,
        m_nextNotifMsgNum++
    );
    notif->setPayload(builder.build());
    client->sendPacket(std::move(notif));
}

// Game client requests a bytevault auth token

std::unique_ptr<blaze::Packet> Authentication::handleGetAuthToken(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {

    auto requestTdf = request.getPayloadAsTdf();

    std::string authCode = "696969420696969";

    blaze::TdfBuilder builder;
    builder
        .string("AUTH", authCode);

    auto reply = request.createReply();
    reply->setPayload(builder.build());
    client->sendPacket(std::move(reply));
    return nullptr;
}

// Seemingly unused, but implemented just in case

std::unique_ptr<blaze::Packet> Authentication::handleExpressLogin(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {

    auto requestTdf = request.getPayloadAsTdf();

    std::string personaName = config::persona;
    if (auto it = requestTdf.find("PNAM"); it != requestTdf.end()) {
        if (it->second && it->second->type == blaze::TdfType::String)
            personaName = std::get<blaze::TdfString>(it->second->value);
    }

    const uint64_t userId = static_cast<uint64_t>(config::blazeId);
    const std::string newToken = config::sessionKey;

    client->setUserId(userId);
    client->setSessionId(userId);
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

    LOG_INFO("[auth] expressLogin persona={}", personaName);

    client->sendPacket(std::move(reply));
    sendUserAuthenticatedNotification(client);
    return nullptr;
}

// Client logout, server clears session data

std::unique_ptr<blaze::Packet> Authentication::handleLogout(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    LOG_INFO("[auth] logout session={}", client->getSessionId());

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

    auto reply = request.createReply();
    reply->setPayload(blaze::TdfBuilder()
        .integer("NTOS", 0)
        .beginList("PLST")
            .beginStruct()
                .string("DPTS", "")
                .integer("EXID", 0)
                .integer("GTYP", 0)
                .string("MAIL", "")
                .integer("PID", static_cast<int64_t>(userId))
                .string("PNAM", personaName)
            .endStruct()
        .endList()
        .build());
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

} // namespace gw2::components
