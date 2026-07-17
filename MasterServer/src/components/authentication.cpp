#include "components/authentication.hpp"
#include "blaze/tdf.hpp"
#include "network/client_connection.hpp"
#include "utils/logger.hpp"
#include "config.hpp"

namespace gw2::components {

// Blaze::UserSessions::UserAuthenticated (0x0008): Used to notify the UserManager (BlazeSDK) when a user becomes fully authenticated
void Authentication::sendUserAuthenticatedNotification(std::shared_ptr<network::ClientConnection> client) {

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

// Blaze::UserSessions::UserAdded (0x0002): Sent when the server would like the client to cache a users information
void Authentication::sendUserAddedNotification(std::shared_ptr<network::ClientConnection> client) {
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

Authentication::Authentication()
    : Component(blaze::ComponentId::Authentication, "Authentication")
{
}

std::unique_ptr<blaze::Packet> Authentication::handlePacket(const blaze::Packet& request, std::shared_ptr<network::ClientConnection> client) {
    uint16_t command = request.getCommand();

    switch (static_cast<blaze::AuthCommand>(command)) {
        case blaze::AuthCommand::login:
            return handleLogin(request, client);

        case blaze::AuthCommand::logout:
            return handleLogout(request, client);

        case blaze::AuthCommand::getAuthToken:
            return handleGetAuthToken(request, client);

        default:
            LOG_WARN("[Auth] Unknown command: 0x{:04X}", command);
            return request.createReply();
    }
}

// Blaze::Authentication::Login (id=0x0A): Login with Identity 2.0 Client Auth Code.
std::unique_ptr<blaze::Packet> Authentication::handleLogin(const blaze::Packet& request, std::shared_ptr<network::ClientConnection> client) {

    client->setUserId(config::blazeId);
    client->setSessionId(config::blazeId);
    client->setPersonaName(config::persona);
    client->setConnectionState(blaze::ConnectionState::AUTHENTICATED);

    // LoginResponse (Blaze::Authentication::LoginResponse)
    blaze::TdfBuilder builder;
    builder
        .integer("ANON", 0)                           // isAnonymous — true if this user is flagged as an anonymous account in Nucleus
        .beginStruct("SESS")                          // sessionInfo (UserLoginInfo) — Information about this login
            .integer("1CON", 0)                       // isFirstConsoleLogin — True if first console login (not Web) & has external data set
            .integer("BUID", config::blazeId)         // blazeId — The unique Nucleus persona id assigned to the persona associated with this session
            .integer("FRST", 0)                       // isFirstLogin — True if this is the first time the user has logged on to this Blaze server
            .string("KEY", config::sessionKey)        // sessionKey — The SessionKey created by the Blaze server for this session
            .integer("LLOG", 3557254017)              // lastLoginDateTime — the date/time this user was last authenticated by this Blaze server
            .string("MAIL", config::email)            // email — The email address for the user associated with this session
            .beginStruct("PDTL")                      // personaDetails (PersonaDetails) — The persona details for the persona associated with this session
                .string("DSNM", config::persona)      // displayName — Persona name
                .integer("LAST", 0)                   // lastAuthenticated — Last authentication timestamp for persona
                .integer("PID", config::blazeId)      // personaId — Nucleus personaId
                .integer("PLAT", 4)                   // platform (4 = pc)
                .integer("STAS", 0)                   // status — The status of persona.
                .integer("XREF", config::nucleusId)   // extId — DEPRECATED (Use PlatformInfo) - External reference value such as XUID
            .endStruct()
            .integer("UID", config::nucleusId)        // accountId — DEPRECATED (Use PlatformInfo) - The Nucleus account id for the user associated with this session
        .endStruct()
        .integer("SPAM", 0)                           // isSpammable — True if the user old enough to opt in to receive third party email
        .integer("UNDR", 0);                          // isUnderage — true if this user is flagged as an underage account in Nucleus

    auto reply = request.createReply();
    reply->setPayload(builder.build());

    LOG_INFO("[auth] login persona={}", config::persona);

    sendUserAuthenticatedNotification(client);
    client->sendPacket(std::move(reply));
    sendUserAddedNotification(client);
    return nullptr;
}

// Blaze::Authentication::Logout (id=0x46): Log out current user session.
std::unique_ptr<blaze::Packet> Authentication::handleLogout(const blaze::Packet& request,std::shared_ptr<ClientConnection> client) {
    LOG_INFO("[auth] logout session={}", client->getSessionId());

    client->setSessionId(0);
    client->setUserId(0);
    client->setPersonaName("");
    client->setConnectionState(blaze::ConnectionState::CONNECTED);

    // Empty response
    auto reply = request.createReply();
    return reply;
}

// Blaze::Authentication::GetAuthToken (id=0x24): Fetch a Nucleus access token for the logged-in user
std::unique_ptr<blaze::Packet> Authentication::handleGetAuthToken(const blaze::Packet& request,std::shared_ptr<network::ClientConnection> client) {

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

} // namespace gw2::components
