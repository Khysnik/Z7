#include "components/util.hpp"
#include "blaze/tdf.hpp"
#include "network/client_connection.hpp"
#include "utils/logger.hpp"
#include "utils/config.hpp"
#include "utils/json.hpp"
#include "utils/server_time.hpp"
#include "utils/editorial.hpp"
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

std::string getStringField(const blaze::TdfStruct& tdf, const std::string& tag, const std::string& fallback = "") {
    auto it = tdf.find(tag);
    if (it == tdf.end() || !it->second) {
        return fallback;
    }
    if (it->second->type != blaze::TdfType::String) {
        return fallback;
    }
    return std::get<blaze::TdfString>(it->second->value);
}

void encodeExtendedData(blaze::TdfBuilder& b) {
    // DATA = UserSessionExtendedData (Blaze::UserSessionExtendedData)
    b.beginStruct("DATA")
         .string("BPS", "bio-sjc")                                      // bestPingSiteAlias — The best available ping site.
         .string("CTY", "US")                                          // country — Country Code provided by the GeoIp database.
         .integer("HWFG", 0)                                            // hardwareFlags — Hardware flags.
         .string("ISP", "")                                            // ISP — ISP provided by the GeoIP database.
         .list("PSLM", blaze::TdfType::Integer, {})                     // latencyList — DEPRECATED list of ping site latency the user session owns.
         .integerMap("PSM", {{"bio-sjc", 0}})                           // pingServerLatencyMap — ping site latency map keyed by name.
         .beginStruct("QDAT")                                           // qosData (NetworkQosData) — Bandwidth and NAT type info.
             .uint32("BWHR", 0)                                         // bandwidthErrorCode — [DEPRECATED] (QoS 1.0) hResult of determining the client's Bandwidth.
             .uint32("DBPS", 0)                                         // downstreamBitsPerSecond — The client's downstream network bandwidth (in bits per second).
             .uint32("NAHR", 0)                                         // natErrorCode — [DEPRECATED] (QoS 1.0) hResult of determining the client's NAT type.
             .uint32("NATT", 0)                                         // natType — The client's NAT type (aka firewall/router type).
             .uint32("UBPS", 0)                                         // upstreamBitsPerSecond — The client's upstream network bandwidth (in bits per second).
         .endStruct()
         .string("TZ", "America/New_York")                              // timeZone — Time zone provided by the GeoIP database.
         .uint64("UATT", 0)                                             // userInfoAttribute — Custom user info attribute.
         .list("ULST", blaze::TdfType::ObjectType, {})                  // blazeObjectIdList — A list of BlazeObjectIds that the user session belongs to.
         .boolean("XPLT", true)                                         // crossPlatformOptIn — crossplay flag
     .endStruct();
}

} // namespace

void Util::pushUserAddedNotification(std::shared_ptr<ClientConnection> client) {
    uint64_t userId = client->getUserId();
    std::string personaName = client->getPersonaName();
    if (personaName.empty()) personaName = "Player";

    blaze::TdfBuilder builder;
    encodeExtendedData(builder);
    builder
        // USER = UserIdentification (Blaze::UserIdentification)
        .beginStruct("USER")
            .integer("AID", 0)                                          // accountId — DEPRECATED (Use PlatformInfo) - The master account id (e.g. the Nucleus master id)
            .beginStruct("AIDS")                                        // platformInfo — Contains platform ids and current client platform.
                .integer("PLAT", 4)                                     // clientPlatform (4 = pc)
            .endStruct()
            .integer("ALOC", 1701729619)                               // accountLocale — The user's account locale
            .integer("CNTY", 0)                                        // accountCountry
            .integer("EXID", 0)                                         // externalId — DEPRECATED (Use PlatformInfo) - The user's ExternalId (XUID or PSN ID)
            .integer("ID",   userId)                                    // blazeId — The user's Blaze Id
            .integer("ISPP", 1)                                        // isPrimaryPersona
            .string("NAME", personaName)                                // name — The persona name
            .string("NASP", "cem_ea_id")                               // personaNamespace — The persona namespace for mName.
            .integer("ORIG", 0)                                        // originPersonaId — DEPRECATED (Use PlatformInfo) - The user's Origin persona id
            .integer("PIDI", 0)                                        // pidId — The user's Pid Id
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

void Util::pushUserSessionExtendedDataUpdate(std::shared_ptr<ClientConnection> client) {
    uint64_t userId = client->getUserId();

    // UserSessionExtendedDataUpdate (Blaze::UserSessionExtendedDataUpdate)
    blaze::TdfBuilder builder;
    encodeExtendedData(builder);   // DATA member (UserSessionExtendedData)
    builder
        .boolean("SUBS", true)                                          // subscribed — True if this UED update is sent/received due to a subscription
        .integer("USID", static_cast<int64_t>(userId));               // userSessionId — The user's unique blaze Id

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

Util::Util(): Component(blaze::ComponentId::Util, "Util"){
}

std::unique_ptr<blaze::Packet> Util::handlePacket(const blaze::Packet& request, std::shared_ptr<ClientConnection> client) {
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

// Blaze::Util::Ping (id=0x02): Ping the server to keep the connection alive.
std::unique_ptr<blaze::Packet> Util::handlePing(const blaze::Packet& request, std::shared_ptr<ClientConnection> client) {
    LOG_DEBUG("[util] ping");

    // PingResponse (Blaze::Util::PingResponse)
    blaze::TdfBuilder builder;
    builder.integer("STIM", blazeServerNow());  // serverTime (Blaze time = unix seconds)

    auto reply = request.createReply();
    reply->setPayload(builder.build());

    return reply;
}

// Blaze::Util::PreAuth (id=0x07): Perform setClientData, fetchClientConfig, fetchQosConfig before authentication. requires_authentication=false, allowGuestCall=true.
std::unique_ptr<blaze::Packet> Util::handlePreAuth(const blaze::Packet& request, std::shared_ptr<ClientConnection> client) {
    blaze::TdfBuilder builder;
    builder
        .string("ASRC", "310695")                                       // authenticationSource — Authentication source
        .intList("CIDS", {61448, 1, 61449, 25, 61450, 27, 4, 7, 9, 10, 33, 126978, 15, 61440, 61441, 61442, 61443, 61444, 61445, 61446, 61447, 3984}) // componentIds — List of components configured on the server.
        .string("CLID", "PVZGW2-PC-SERVER-BLAZE")                       // clientId — Nucleus clientId associated with this service name.
        .beginStruct("CONF")                                            // config (FetchConfigResponse) — Contains all entries in client config of util.cfg which will be also passed to client side.
            .stringMap("CONF", {                                        // config map
                {"Override_ProtoHttp_LoginStateMachine_DedicatedServer_vers", "770,0,NULL"},
                {"associationListSkipInitialSet",  "1"},
                {"autoReconnectEnabled",           "0"},
                {"bytevaultHostname",              "127.0.0.1"},
                {"bytevaultPort",                  "42230"},
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
        .string("ESRC", "310695")                                       // entitlementSource — The Entitlement Source
        .string("INST", "plantsvszombies-gw2-pc")                       // serviceName — Service name.
        .integer("MAID", 3310897674)                                    // machineId — Uniquely identify the machine
        .integer("MINR", 1)                                             // underageSupported — Underage support
        .string("NASP", "cem_ea_id")                                    // personaNamespace — Persona namespace.
        .string("PILD", "")                                            // legalDocGameIdentifier — Title-specific identifier for legal documents retrieval
        .string("PLAT", "pc")                                          // platform — Server platform.
        .beginStruct("QOSS")                                            // qosSettings (QosConfigInfo) — Contains all info in QosSettings of util.cfg which will be also passed to client side.
            .beginStruct("BWPS")                                        // bandwidthPingSiteInfo (QosPingSiteInfo)
                .string("PSA", "")                                      // address
                .integer("PSP", 0)                                     // port
            .endStruct()
            .integer("LNP", 10)                                         // numLatencyProbes
            .beginMap("LTPS", "string", "struct")                       // pingSiteInfoByAliasMap (alias -> QosPingSiteInfo{PSA address, PSP port})
                .string("bio-sjc")
                .beginStruct()
                    .string("PSA", "localhost")
                    .integer("PSP", 42230)
                .endStruct()
            .endMap()
            .integer("TIME", 10000000)                                  // timeout
        .endStruct()
        .string("RSRC", "310695")                                       // registrationSource — Registration source
        .string("SVER", "Blaze 15.1.1.4.6 (CL# 2136954)");             // serverVersion — Server version.

    auto reply = request.createReply();
    reply->setPayload(builder.build());
    return reply;
}

// Blaze::Util::PostAuth (id=0x08): Perform getTickerServer and getTelemetryServer after
std::unique_ptr<blaze::Packet> Util::handlePostAuth(const blaze::Packet& request, std::shared_ptr<ClientConnection> client) {
    // PostAuthResponse (Blaze::Util::PostAuthResponse)
    blaze::TdfBuilder builder;
    builder
        // telemetryServer (GetTelemetryServerResponse) — Telemetry server info.
        // Class: "Contains Telemetry Server Information."
        .beginStruct("TELE")
            .string("ADRS", "https://river.data.ea.com")               // address (mAddress)
            .integer("ANON", 0)                                        // isAnonymous (mIsAnonymous)
            .string("DISA", "")                                        // disable (mDisable)
            .integer("EDCT", 0)                                        // enableDisconnectTelemetry (mEnableDisconnectTelemetry)
            .string("FILT", "-UION/****")                             // filter (mFilter)
            .integer("LOC", config::locale)                           // locale (mLocale)
            .integer("MINR", 0)                                        // underage (mUnderage)
            .string("NOOK", "")                                       // noToggleOk (mNoToggleOk)
            .integer("PORT", 827)                                     // port (mPort)
            .integer("SDLY", 29976)                                   // sendDelay (mSendDelay)
            .string("SESS", "Qa8AqZFw1/G")                            // sessionID (mSessionID)
            .string("SKEY", "weirdbinarykey")                         // key (mKey; may contain non-utf8 data)
            .integer("SPCT", 139)                                     // sendPercentage (mSendPercentage)
            .string("STIM", "Default")                                // useServerTime (mUseServerTime)
            .string("SVNM", "telemetry-3-common")                     // telemetryServiceName (mTelemetryServiceName)
        .endStruct()
        // tickerServer (GetTickerServerResponse) — Ticker server info.
        // Class: "Contains Ticker Server Information."
        .beginStruct("TICK")
            .string("ADRS", "10.10.78.150")                          // address (mAddress)
            .integer("PORT", 17959)                                   // port (mPort)
            .string("SKEY", "1006900723798,10.10.78.150:8999,plantsvszombies-gw2-pc,10,50,50,50,50,0,0") // key (mKey)
        .endStruct()
        .beginStruct("UROP")                                            // userOptions (UserOptions) — user options
            .integer("TMOP", 0)                                       // telemetryOpt — Describe user options
            .integer("UID", config::blazeId)                         // userId — The ID of the user whose data is to be fetched, 0 means own's settings.
        .endStruct();

    client->setConnectionState(blaze::ConnectionState::POST_AUTH);

    auto reply = request.createReply();
    reply->setPayload(builder.build());

    client->sendPacket(std::move(reply));
    //pushUserAddedNotification(client);
    //pushUserSessionExtendedDataUpdate(client);
    return nullptr;
}

// Blaze::Util::FetchClientConfig (id=0x01): Provides configuration data for the client.
std::unique_ptr<blaze::Packet> Util::handleFetchClientConfig(const blaze::Packet& request, std::shared_ptr<ClientConnection> client) {
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
            {"url", utils::kEditorialBase + "/PlantsVsZombies/GW2/config/pc/game.xml"},
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

// Blaze::Util::LocalizeStrings (id=0x04): Get map of id-to-localized strings for supplied list of ids.
std::unique_ptr<blaze::Packet> Util::handleLocalizeStrings(const blaze::Packet& request, std::shared_ptr<ClientConnection> client) {
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

// Blaze::Util::GetTelemetryServer (id=0x05): Get telemetry server connection data.
std::unique_ptr<blaze::Packet> Util::handleGetTelemetryServer(const blaze::Packet& request, std::shared_ptr<ClientConnection> client) {
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

// Blaze::Util::SetClientState (id=0x1C): Allows current user to set information about its client state.
std::unique_ptr<blaze::Packet> Util::handleSetClientState(const blaze::Packet& request, std::shared_ptr<ClientConnection> client) {
    auto requestTdf = request.getPayloadAsTdf();
    int64_t mode = getIntField(requestTdf, "MODE", -1);
    int64_t state = getIntField(requestTdf, "STAT", -1);

    LOG_INFO("[util] setClientState from {} (MODE={}, STAT={})",
             client->getRemoteAddress(), mode, state);

    auto reply = request.createReply();
    return reply;
}


} // namespace gw2::components
