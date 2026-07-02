#include "components/util.hpp"
#include "blaze/tdf.hpp"
#include "network/client_connection.hpp"
#include "utils/logger.hpp"
#include "utils/config.hpp"
#include "utils/json.hpp"
#include "utils/server_time.hpp"
#include "config.hpp"

#include <nlohmann/json.hpp>

#include <fstream>
#include <iterator>
#include <map>
#include <string>
#include <unordered_map>

namespace gw2::components {

namespace {

const std::unordered_map<std::string, std::string>& localizationTable() {
    static const std::unordered_map<std::string, std::string> table = [] {
        std::unordered_map<std::string, std::string> m;
        const nlohmann::json& j = utils::dataSection("localization");
        for (const auto& [k, v] : j.items()) m.emplace(k, v.get<std::string>());
        LOG_INFO("[util] loaded {} localized strings", m.size());
        return m;
    }();
    return table;
}

const std::map<std::string, std::string>& playlistsConfig() {
    static const std::map<std::string, std::string> cfg = [] {
        std::map<std::string, std::string> m;
        const nlohmann::json& j = utils::dataSection("playlists_config");
        if (j.contains("CONF") && j["CONF"].is_object()) {
            for (const auto& [k, v] : j["CONF"].items())
                m.emplace(k, v.get<std::string>());
        }
        LOG_INFO("[util] loaded {} multiplayer playlists", m.size());
        return m;
    }();
    return cfg;
}

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

Util::Util(): Component(blaze::ComponentId::Util, "Util"){
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

        case blaze::UtilCommand::localizeStrings:
            return handleLocalizeStrings(request, client);

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

    // PingResponse (Blaze::Util::PingResponse)
    blaze::TdfBuilder builder;
    builder.integer("STIM", blazeServerNow());  // serverTime (Blaze time = unix seconds)
    
    auto reply = request.createReply();
    reply->setPayload(builder.build());
    
    return reply;
}

std::unique_ptr<blaze::Packet> Util::handlePreAuth(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    blaze::TdfBuilder builder;
    builder
        .string("ASRC", "310695")                                       // authenticationSource
        .intList("CIDS", {61448, 1, 61449, 25, 61450, 27, 4, 7, 9, 10, 33, 126978, 15, 61440, 61441, 61442, 61443, 61444, 61445, 61446, 61447, 3984}) // componentIds
        .string("CLID", "PVZGW2-PC-SERVER-BLAZE")                       // clientId
        .beginStruct("CONF")                                            // config (FetchConfigResponse)
            .stringMap("CONF", {                                        // config map
                {"Override_ProtoHttp_LoginStateMachine_DedicatedServer_vers", "770,0,NULL"},
                {"associationListSkipInitialSet",  "1"},
                {"autoReconnectEnabled",           "0"},
                {"bytevaultHostname",              "127.0.0.1"},
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
        .string("ESRC", "310695")                                       // entitlementSource
        .string("INST", "plantsvszombies-gw2-pc")                       // serviceName
        .integer("MAID", 3310897674)                                    // machineId
        .integer("MINR", 1)                                             // underageSupported
        .string("NASP", "cem_ea_id")                                    // personaNamespace
        .string("PILD", "")                                            // legalDocGameIdentifier
        .string("PLAT", "pc")                                          // platform
        .beginStruct("QOSS")                                            // qosSettings (QosConfigInfo)
            .beginStruct("BWPS")                                        // bandwidthPingSiteInfo (QosPingSiteInfo)
                .string("PSA", "")                                      // address
                .integer("PSP", 0)                                     // port
            .endStruct()
            .integer("LNP", 10)                                         // numLatencyProbes
            .beginMap("LTPS", "string", "struct")                       // pingSiteInfoByAliasMap (alias -> QosPingSiteInfo{PSA address, PSP port})
                .string("bio-sjc")
                .beginStruct()
                    .string("PSA", "localhost")
                    .integer("PSP", 34976)
                .endStruct()
            .endMap()
            .integer("TIME", 10000000)                                  // timeout
        .endStruct()
        .string("RSRC", "310695")                                       // registrationSource
        .string("SVER", "Blaze 15.1.1.4.6 (CL# 2136954)");             // serverVersion

    auto reply = request.createReply();
    reply->setPayload(builder.build());
    return reply;
}

std::unique_ptr<blaze::Packet> Util::handlePostAuth(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    // PostAuthResponse (Blaze::Util::PostAuthResponse)
    blaze::TdfBuilder builder;
    builder
        .beginStruct("TELE")                                            // telemetryServer (GetTelemetryServerResponse)
            .string("ADRS", "https://river.data.ea.com")               // address
            .integer("ANON", 0)                                        // isAnonymous
            .string("DISA", "")                                        // disable
            .integer("EDCT", 0)                                        // enableDisconnectTelemetry
            .string("FILT", "-UION/****")                             // filter
            .integer("LOC", config::locale)                           // locale
            .integer("MINR", 0)                                        // underage
            .string("NOOK", "")                                       // noToggleOk
            .integer("PORT", 827)                                     // port
            .integer("SDLY", 29976)                                   // sendDelay
            .string("SESS", "Qa8AqZFw1/G")                            // sessionID
            .string("SKEY", "weirdbinarykey")                         // (anon - session key)
            .integer("SPCT", 139)                                     // sendPercentage
            .string("STIM", "Default")                                // useServerTime
            .string("SVNM", "telemetry-3-common")                     // telemetryServiceName
        .endStruct()
        .beginStruct("TICK")                                            // tickerServer (GetTickerServerResponse)
            .string("ADRS", "10.10.78.150")                          // address
            .integer("PORT", 17959)                                   // port
            .string("SKEY", "1006900723798,10.10.78.150:8999,plantsvszombies-gw2-pc,10,50,50,50,50,0,0") // (anon - session key)
        .endStruct()
        .beginStruct("UROP")                                            // userOptions (UserOptions)
            .integer("TMOP", 0)                                       // telemetryOpt
            .integer("UID", config::blazeId)                         // userId
        .endStruct();

    client->setConnectionState(blaze::ConnectionState::POST_AUTH);

    auto reply = request.createReply();
    reply->setPayload(builder.build());

    client->sendPacket(std::move(reply));
    //pushUserAddedNotification(client);
    //pushUserSessionExtendedDataUpdate(client);
    return nullptr;
}

namespace {

void encodeExtendedData(blaze::TdfBuilder& b) {
    b.beginStruct("DATA")
         .string("BPS", "bio-sjc")                                      // bestPingSiteAlias
         .string("CTY", "US")                                          // country
         .integer("HWFG", 0)                                            // hardwareFlags
         .string("ISP", "")                                            // ISP name
         .list("PSLM", blaze::TdfType::Integer, {})                     // latencyList
         .integerMap("PSM", {{"bio-sjc", 0}})                           // pingServerLatencyMap
         .beginStruct("QDAT")                                           // qosData (NetworkQosData)
             .uint32("BWHR", 0)                                         // bandwidthErrorCode
             .uint32("DBPS", 0)                                         // downstreamBitsPerSecond
             .uint32("NAHR", 0)                                         // natErrorCode
             .uint32("NATT", 0)                                         // natType
             .uint32("UBPS", 0)                                         // upstreamBitsPerSecond
         .endStruct()
         .string("TZ", "America/New_York")                              // timeZone
         .uint64("UATT", 0)                                             // userInfoAttribute
         .list("ULST", blaze::TdfType::ObjectType, {})                  // blazeObjectIdList
         .boolean("XPLT", true)                                         // crossplay flag
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
            .integer("AID", 0)                                          // accountId
            .beginStruct("AIDS")
                .integer("PLAT", 4)
            .endStruct()
            .integer("ALOC", 1701729619)                               // accountLocale
            .integer("CNTY", 0)
            .integer("EXID", 0)                                         // externalId
            .integer("ID",   userId)                                    // blazeId
            .integer("ISPP", 1)
            .string("NAME", personaName)                                // name
            .string("NASP", "cem_ea_id")                               // personaNamespace
            .integer("ORIG", 0)                                        // originPersonaId
            .integer("PIDI", 0)                                        // pidId
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

    blaze::TdfBuilder builder;
    encodeExtendedData(builder);
    builder
        .boolean("SUBS", true)                                          // subscribed
        .integer("USID", static_cast<int64_t>(userId));               // userId

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
            {"display",      "console2/welcome"},
            {"redirect_uri", "http://127.0.0.1/success"},
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
            {"url", "https://localhost:42220/PlantsVsZombies/GW2/config/pc/game.xml"},
        });
    } else if (section == "SPOTLIGHT") {
        builder.stringMap("CONF", {
            {"CLIENT_ID",     "pvz"},
            {"ENABLED",       "true"},
            {"FETCH_ENABLED", "false"},
            {"PLACEMENT_IDS", "10186,10187,13359,13463"},
            {"URL",           "https://emapi.prm.data.ea.com/em/v2/messages"},
            {"URL_CLICKS",    "https://emapi.prm.data.ea.com/em/v2/clicks"},
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
    } else if (section == "PVZ_PLAYLISTS") {
        builder.stringMap("CONF", playlistsConfig());
    }
    else {
        builder.stringMap("CONF", {});
    }

    auto reply = request.createReply();
    reply->setPayload(builder.build());
    return reply;
}

std::unique_ptr<blaze::Packet> Util::handleLocalizeStrings(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    auto tdf = request.getPayloadAsTdf();
    const auto& table = localizationTable();

    // SMAP: each requested string id -> localized text
    std::map<std::string, std::string> smap;
    size_t resolved = 0;
    auto it = tdf.find("LSID");
    if (it != tdf.end() && it->second && it->second->type == blaze::TdfType::List) {
        for (const auto& elem : std::get<blaze::TdfList>(it->second->value)) {
            if (!elem || elem->type != blaze::TdfType::String) continue;
            const std::string& id = std::get<blaze::TdfString>(elem->value);
            auto found = table.find(id);
            if (found != table.end()) { smap[id] = found->second; ++resolved; }
            else                      { smap[id] = id; }
        }
    }
    LOG_INFO("[util] localizeStrings from {} ({} requested, {} resolved)",
             client->getRemoteAddress(), smap.size(), resolved);

    auto reply = request.createReply();
    reply->setPayload(blaze::TdfBuilder().stringMap("SMAP", smap).build());
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


} // namespace gw2::components
