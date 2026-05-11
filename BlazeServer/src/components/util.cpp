#include "components/util.hpp"
#include "blaze/tdf.hpp"
#include "network/client_connection.hpp"
#include "utils/logger.hpp"
#include "utils/config.hpp"

namespace ds2::components {

namespace {

int64_t getIntField(const blaze::TdfStruct& tdf, const std::string& tag, int64_t fallback = 0) {
    auto it = tdf.find(tag);
    if (it == tdf.end() || !it->second) {
        return fallback;
    }
    if (it->second->type != blaze::TdfType::Integer) {
        return fallback;
    }
    return std::get<blaze::TdfInteger>(it->second->value);
}

std::string getStringField(const blaze::TdfStruct& tdf, const std::string& tag,
                           const std::string& fallback = "") {
    auto it = tdf.find(tag);
    if (it == tdf.end() || !it->second) {
        return fallback;
    }
    if (it->second->type != blaze::TdfType::String) {
        return fallback;
    }
    return std::get<blaze::TdfString>(it->second->value);
}

} // namespace

Util::Util()
    : Component(blaze::ComponentId::Util, "Util")
{
}

std::unique_ptr<blaze::Packet> Util::handlePacket(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    uint16_t command = request.getCommand();
    
    switch (static_cast<blaze::UtilCommand>(command)) {
        case blaze::UtilCommand::ping:
            return handlePing(request, client);
        
        case blaze::UtilCommand::preAuth:
            return handlePreAuth(request, client);
        
        case blaze::UtilCommand::postAuth:
            return handlePostAuth(request, client);
        
        case blaze::UtilCommand::fetchClientConfig:
            return handleFetchClientConfig(request, client);
        
        case blaze::UtilCommand::getTelemetryServer:
            return handleGetTelemetryServer(request, client);
        
        case blaze::UtilCommand::setClientState:
            return handleSetClientState(request, client);
        
        default:
            LOG_WARN("[Util] Unknown command: 0x{:04X}", command);
            return request.createReply();
    }
}

std::unique_ptr<blaze::Packet> Util::handlePing(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    LOG_DEBUG("[util] ping");

    blaze::TdfBuilder builder;
    builder.integer("STIM", static_cast<int64_t>(time(nullptr)));
    
    auto reply = request.createReply();
    reply->setPayload(builder.build());
    
    return reply;
}

std::unique_ptr<blaze::Packet> Util::handlePreAuth(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    LOG_INFO("[util] preAuth from {}", client->getRemoteAddress());

    uint16_t redirectorPort = utils::Config::instance().getServerConfig().redirector_port;
    std::string nucleusConnect = "https://127.0.0.1:" + std::to_string(redirectorPort);

    blaze::TdfBuilder builder;
    builder
        .string("ASRC", "310335")
        .intList("CIDS", {1, 25, 4, 27, 28, 6, 7, 9, 10, 11, 30720, 30721, 30722, 30723, 20, 30725, 30726, 2000, 0x801, 0x7802})
        .string("CLID", "PVZGW2-PC-SERVER-BLAZE")
        .beginStruct("CONF")
            .stringMap("CONF", {
                {"Override_ProtoHttp_LoginStateMachine_DedicatedServer_vers", "770,0,NULL"},
                {"associationListSkipInitialSet",  "1"},
                {"autoReconnectEnabled",           "0"},
                {"bytevaultHostname",              "bytevault.gameservices.ea.com"},
                {"bytevaultPort",                  "42210"},
                {"bytevaultSecure",                "true"},
                {"cachedUserRefreshInterval",      "1s"},
                {"connIdleTimeout",                "40s"},
                {"defaultRequestTimeout",          "20s"},
                {"maxReconnectAttempts",           "30"},
                {"nucleusConnect",                 "https://accounts.ea.com"},
                {"nucleusConnectTrusted",          "https://accounts2s.ea.com"},
                {"nucleusPortal",                  "https://signin.ea.com"},
                {"nucleusProxy",                   "https://gateway.ea.com"},
                {"pingPeriod",                     "20s"},
                {"userManagerMaxCachedUsers",      "0"},
                {"voipHeadsetUpdateRate",          "1000"},
                {"xblTokenUrn",                    "accounts.ea.com"},
                {"xboxOneStringValidationUri",     "client-strings.xboxlive.com"},
            })
        .endStruct()
        .string("ESRC", "310695")
        .string("INST", "plantsvszombies-gw2-pc")
        .uint32("MAID", 3757971085)
        .boolean("MINR", 0)
        .string("NASP", "cem_ea_id")
        .string("PILD", "pvzgw2")
        .string("PLAT", "pc")
        .beginStruct("QOSS")
            .beginStruct("BWPS")
                .string("PSA", "127.0.0.1")
                .uint32("PSP", 17502)
                .string("SNA", "ams")
            .endStruct()
            .uint16("LNP", 10)
            .beginMap("LTPS", "string", "struct")
                .string("ams")
                .beginStruct()
                    .string("PSA", "127.0.0.1")
                    .uint32("PSP", 17502)
                    .string("SNA", "ams")
                .endStruct()
            .endMap()
            .uint32("SVID", 1161889797)
            .string("TIME", "10000000")
        .endStruct()
        .string("RSRC", "310695")
        .string("SVER", "Blaze 15.1.1.4.6 (CL# 2136954)")
        ;

    auto reply = request.createReply();
    reply->setPayload(builder.build());
    return reply;
}

std::unique_ptr<blaze::Packet> Util::handlePostAuth(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    LOG_INFO("[util] postAuth from {}", client->getRemoteAddress());

    // PostAuth response — telemetry/ticker/user-options.
    blaze::TdfBuilder builder;
    builder
        .beginStruct("PSS")
        .endStruct()
        .beginStruct("TELE")
            .string("ADRS", "127.0.0.1")
            .integer("ANON", 0)
            .string("DPTS", "")
            .string("FILT", "")
            .integer("LOC", 1701729619) //en-US locale
            .string("NOOK", "US,CA,MX")
            .integer("PORT", 9988)
            .integer("SDLY", 15000)
            .string("SESS", "ds2sess")
            .string("SKEY", "ds2key")
            .integer("SPCT", 75)
            .string("STIM", "")
        .endStruct()
        .beginStruct("TICK")
            .string("ADRS", "")
            .integer("PORT", 0)
            .string("SKEY", "")
        .endStruct()
        .beginStruct("UROP")
            .integer("TMOP", 0)
            .integer("UID", client->getUserId())
        .endStruct();

    client->setConnectionState(blaze::ConnectionState::POST_AUTH);

    auto reply = request.createReply();
    reply->setPayload(builder.build());

    client->sendPacket(std::move(reply));
    pushUserAddedNotification(client);
    pushUserSessionExtendedDataUpdate(client);
    return nullptr;
}

namespace {

void encodeExtendedData(blaze::TdfBuilder& b) {
    b.beginStruct("DATA")
         .string("BPS", "")           // best ping site alias
         .string("CTY", "US")
         .integer("HWFG", 0)
         .string("ISP", "")
         .string("TZ", "America/New_York")
         .integer("UATT", 0)
         .integer("XPLT", 1)
     .endStruct();
}

} // namespace

void Util::pushUserAddedNotification(std::shared_ptr<network::ClientConnection> client) {
    uint64_t userId = client->getUserId();
    std::string personaName = client->getPersonaName();
    if (personaName.empty()) personaName = "Player";

    blaze::TdfBuilder builder;
    encodeExtendedData(builder);
    builder
        .beginStruct("USER")
            .integer("AID", 0)
            .beginStruct("AIDS")
                .integer("PLAT", 4)
            .endStruct()
            .integer("ALOC", 1701729619)
            .integer("CNTY", 0)
            .integer("EXID", 0)
            .integer("ID",   userId)
            .integer("ISPP", 1)
            .string("NAME", personaName)
            .string("NASP", "cem_ea_id")
            .integer("ORIG", 0)
            .integer("PIDI", 0)
        .endStruct();

    auto notif = std::make_unique<blaze::Packet>(
        blaze::ComponentId::UserSessions,
        /*command (notif id)*/ 2,
        blaze::MessageType::Notification,
        m_nextNotifMsgNum++
    );
    notif->setPayload(builder.build());
    client->sendPacket(std::move(notif));
    LOG_INFO("[util] pushed UserAdded notif (uid={})", userId);
}

void Util::pushUserSessionExtendedDataUpdate(std::shared_ptr<network::ClientConnection> client) {
    uint64_t userId = client->getUserId();

    // UserSessionExtendedDataUpdate schema (userextendeddatatypes.tdf:100):
    //   DATA (UserSessionExtendedData)
    //   USID (BlazeId)
    //   SUBS (bool)  — true if pushed because of a subscription
    blaze::TdfBuilder builder;
    encodeExtendedData(builder);
    builder
        .integer("SUBS", 1)
        .integer("USID", userId);

    auto notif = std::make_unique<blaze::Packet>(
        blaze::ComponentId::UserSessions,
        /*command (notif id)*/ 1,
        blaze::MessageType::Notification,
        m_nextNotifMsgNum++
    );
    notif->setPayload(builder.build());
    client->sendPacket(std::move(notif));
    LOG_INFO("[util] pushed UserSessionExtendedDataUpdate notif (uid={})", userId);
}

std::unique_ptr<blaze::Packet> Util::handleFetchClientConfig(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    auto tdf = request.getPayloadAsTdf();
    std::string section;
    auto it = tdf.find("CFID");
    if (it != tdf.end() && it->second) {
        section = std::get<blaze::TdfString>(it->second->value);
    }
    LOG_INFO("[util] fetchClientConfig section={}", section);

    blaze::TdfBuilder builder;
    if (section == "IdentityParams") {

        builder.stringMap("CONF", {
            {"redirect_uri", "http://127.0.0.1/success"},
            {"display",      "console2/welcome"},
        });
    } else if (section == "PVZ_NEWS") {

        builder.stringMap("CONF", {
            {"NEWS_EMPTY_TITLE", "53,NEWS_EMPTY_DESC,1406547939,2526314400,enabled,billboard_logo,8,0,0,empty"},
        });
        } else if (section == "BlazeSDK") {

        builder.stringMap("CONF", {
            {"pingPeriod",            "20000"},
            {"defaultRequestTimeout", "30000"},
            {"connIdleTimeout",       "120000"},
            {"autoReconnectEnabled",  "1"},
            {"maxReconnectAttempts",  "3"},
        });
    } else if (section == "EDITORIAL") {

        builder.stringMap("CONF", {
            {"url", "https://editorial.gos.ea.com/PlantsVsZombies/GardenWarfare/config/pc/server.xml"},
        });
    } else if (section == "PVZ_ONLINE_PLAYLISTS") {

        static const std::string kLargeCoopLevels =
            "Levels/Level_Coop_Asia/Level_Coop_Asia;"
            "Levels/Level_Coop_Dino/Level_Coop_Dino;"
            "Levels/Level_Coop_Egypt/Level_Coop_Egypt;"
            "Levels/Level_Coop_Rome/Level_Coop_Rome;"
            "Levels/Level_Coop_Space/Level_Coop_Space;"
            "Levels/Level_Coop_TimePark/Level_Coop_TimePark;"
            "Levels/Level_Coop_ZombossFactory/Level_Coop_ZombossFactory;"
            "Levels/Level_Coop_Snow/Level_Coop_Snow;"
            "Levels/Level_Coop_Brainstreet/Level_Coop_Brainstreet;"
            "Levels/Level_Coop_Zomburbia/Level_Coop_Zomburbia;";
        builder.stringMap("CONF", {
            {"BossHunt0",
                "Levels/Level_Coop_Egypt/Level_Coop_Egypt"},
            {"Endless0",
                "Levels_Sandbox/Level_Endless_Sandbox/Level_Endless_Sandbox"},
            {"GardenOps0",
                "ID_M_FILTER_ANY;" + kLargeCoopLevels},
            {"GnGLarge0",
                "Levels/Level_Rush_Themepark/Level_Rush_Themepark;"
                "Levels/Level_Rush_Snow/Level_Rush_Snow;"},
            {"GnomeBomb0",
                kLargeCoopLevels},
            {"GraveyardOps0",
                "ID_M_FILTER_ANY;" + kLargeCoopLevels},
            {"HerbalAssaultLarge0",
                "Levels/Level_Herb_Space/Level_Herb_Space;"
                "Levels/Level_Herb_Zomburbia/Level_Herb_Zomburbia;"
                "Levels/Level_Herb_Brainstreet/Level_Herb_Brainstreet;"},
            {"SuburbinationLarge0",
                kLargeCoopLevels},
            {"TeamVanquishLarge0",
                kLargeCoopLevels},
            {"VanquishConfirmedLarge0",
                kLargeCoopLevels},
        });
    } else if (section == "KILL_SWITCHES") {

        builder.stringMap("CONF", {
            {"assocListsAndStatsKillSwitch", "false"},
            {"blazeAchievementServiceKillSwitch", "false"},
            {"matchmakeResetProbability", "1"},
            {"packExternalPurchaseKillSwitch", "false"},
        });
    } else {

        builder.stringMap("CONF", {});
    }

    auto reply = request.createReply();
    reply->setPayload(builder.build());
    return reply;
}

std::unique_ptr<blaze::Packet> Util::handleGetTelemetryServer(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    auto requestTdf = request.getPayloadAsTdf();
    std::string serviceName = getStringField(requestTdf, "SNAM", "pvz-gw2");
    std::string cmac = getStringField(requestTdf, "CMAC", "");

    LOG_INFO("[util] getTelemetryServer from {} (SNAM='{}', CMAC='{}')",
             client->getRemoteAddress(), serviceName, cmac);

    blaze::TdfBuilder builder;
    builder
        .string("ADRS", "127.0.0.1")
        .integer("ANON", 0)
        .string("DPTS", "")
        .string("FILT", "")
        .integer("LOC", 1701729619)
        .integer("MINR", 0)
        .string("NOOK", "US,CA,MX")
        .integer("PORT", 9988)
        .integer("SDLY", 15000)
        .string("SESS", "ds2sess")
        .string("SKEY", "ds2key")
        .integer("SPCT", 75)
        .string("STIM", "")
        .string("SVNM", serviceName);

    auto reply = request.createReply();
    reply->setPayload(builder.build());
    return reply;
}

std::unique_ptr<blaze::Packet> Util::handleSetClientState(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    auto requestTdf = request.getPayloadAsTdf();
    int64_t mode = getIntField(requestTdf, "MODE", -1);
    int64_t state = getIntField(requestTdf, "STAT", -1);

    LOG_INFO("[util] setClientState from {} (MODE={}, STAT={})",
             client->getRemoteAddress(), mode, state);

    auto reply = request.createReply();
    return reply;
}


} // namespace ds2::components
