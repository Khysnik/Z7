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
    
    // Ping response - just echo back with timestamp
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

    // GW2 / BlazeSDK 15.1.1.1.0 preAuth response
    // Component IDs: Auth=1, GameManager=4, Stats=7, Util=9, Association=25,
    //   Playgroups=30, RSP=0x801, UserSessions=0x7802
    uint16_t redirectorPort = utils::Config::instance().getServerConfig().redirector_port;
    std::string nucleusConnect = "https://127.0.0.1:" + std::to_string(redirectorPort);

    blaze::TdfBuilder builder;
    builder
        .string("ASRC", "310335")
        .intList("CIDS", {1, 25, 4, 27, 28, 6, 7, 9, 10, 11, 30720, 30721, 30722, 30723, 20, 30725, 30726, 2000, 0x801, 0x7802})
        .string("CLID", "pvzgw2-client")
        .beginStruct("CONF")
            .stringMap("CONF", {
                {"connIdleTimeout",        "90s"},
                {"defaultRequestTimeout", "80s"},
                {"pingPeriod", "20s"},
                {"autoReconnectEnabled", "1"},
                {"voipHeadsetUpdateRate", "1000"},
                {"xlspConnectionIdleTimeout", "300"},
            })
        .endStruct()
        .string("ESRC", "310335")
        .string("INST", "plantsvszombies-gw2-pc")
        .uint32("MAID", 69420)
        .boolean("MINR", false)
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
            .string("TIME", "5s")
        .endStruct()
        .string("RSRC", "310335")
        .string("SVER", "Blaze 15.1.1.1.0/GW2")
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
            .integer("LOC", 1701729619)
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
            .integer("TMOP", 1)
            .integer("UID", client->getUserId())
        .endStruct();

    client->setConnectionState(blaze::ConnectionState::POST_AUTH);

    auto reply = request.createReply();
    reply->setPayload(builder.build());

    // Send postAuth response, then push UserAdded + UserSessionExtendedData-
    // Update so the SDK has every code path it might key off of:
    //   * UserAdded (id=2) feeds NotifyUserAdded → calls setExtendedData *only*
    //     when isSubscribedByIndex(userIndex) was false.
    //   * UserSessionExtendedDataUpdate (id=1) is the unconditional path:
    //     it dispatches onExtendedUserDataInfoChanged regardless of subscription
    //     state, which is the trigger LocalUser::onExtendedUserDataInfoChanged
    //     listens for to fire onLocalUserAuthenticated once postAuth has
    //     also completed (and it has, since the reply just went out).
    client->sendPacket(std::move(reply));
    pushUserAddedNotification(client);
    pushUserSessionExtendedDataUpdate(client);
    return nullptr;
}

namespace {

// UserSessionExtendedData (userextendeddatatypes.tdf:53). Build it once and
// reuse for both UserAdded.DATA and UserSessionExtendedDataUpdate.DATA.
//
// Only fields that are actually scalars/structs we can build are encoded.
// List<ObjectId>, ping-site latency lists/maps, NetworkQosData and the
// NetworkAddress union have no fluent helper here — and their TDF default
// (absent → empty / zero-initialised) is exactly what we want. Encoding
// them as empty STRUCTs would mis-type the wire payload and the BlazeSDK
// parser would reject the whole notification.
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
        // GW2 binary (sub_140b5c520) only validates that "redirect_uri" exists
        // in the response map. Other keys are stored and forwarded as URL
        // parameters to the Nucleus /connect/auth endpoint by the SDK iterator
        // in LoginStateInitConsole::doNucleusLogin. Including the standard
        // BlazeSDK keys (display, client_id) keeps the URL well-formed for
        // any future Nucleus mock.
        builder.stringMap("CONF", {
            {"redirect_uri", "http://127.0.0.1/success"},
            {"display",      "console2/welcome"},
            {"client_id",    "PVZ_GW2_PC"},
        });
    } else if (section == "BlazeSDK") {
        // Optional BlazeSDK runtime tunables. SDK falls back to defaults if
        // missing, so we just supply sane values to silence any warnings.
        builder.stringMap("CONF", {
            {"pingPeriod",            "20000"},
            {"defaultRequestTimeout", "30000"},
            {"connIdleTimeout",       "120000"},
            {"autoReconnectEnabled",  "1"},
            {"maxReconnectAttempts",  "3"},
        });
    } else if (section == "EDITORIAL") {
        // The callback (sub_14100b580) does a binary search on the
        // response map for a single key, "url", and hands its value to
        // the editorial fetcher. The fetcher then GETs that URL for
        // news/MOTD content (likely XML or JSON).
        //
        // We don't host a news feed yet, so point at a local stub URL
        // that 404s harmlessly; if the binary times out we'll switch
        // to an empty body endpoint on our HTTP listener.
        builder.stringMap("CONF", {
            {"url", "http://127.0.0.1/editorial"},
        });
    } else if (section == "PVZ_ONLINE_PLAYLISTS") {
        // Per-playlist matchmaking metadata. The handler in the binary
        // (callback sub_14100c920 → value parser sub_141075110) iterates
        // the map, then splits each value on ';' (delimiter byte at
        // 0x1421e4794) and stores (key → token list) in a tree the
        // matchmaking UI later reads.
        //
        // Playlist names enumerated from the reflection tables at
        // 0x14316a700..0x14316a958 plus the string pool at
        // 0x142342400..0x1423427e8 (PlaylistSettings_*).
        //
        // Token semantics aren't fully reversed yet — the value below is
        // empty for every playlist, which produces an empty token list.
        // That's enough to register each playlist key in the client's
        // map; populating the tokens (likely scenario IDs / map names
        // separated by ';') is the next step once we can see how the
        // matchmaking UI reacts.
        builder.stringMap("CONF", {
            {"PVZServerGame.OnlinePlaylists.PlaylistSettings_GnG",                    ""},
            {"PVZServerGame.OnlinePlaylists.PlaylistSettings_GnGNight_GW1",           ""},
            {"PVZServerGame.OnlinePlaylists.PlaylistSettings_GnG_GW1",                ""},
            {"PVZServerGame.OnlinePlaylists.PlaylistSettings_HerbalAssault",          ""},
            {"PVZServerGame.OnlinePlaylists.PlaylistSettings_HerbalAssaultTournament","" },
            {"PVZServerGame.OnlinePlaylists.PlaylistSettings_VanquishConfirmed",      ""},
            {"PVZServerGame.OnlinePlaylists.PlaylistSettings_TeamVanquish",           ""},
            {"PVZServerGame.OnlinePlaylists.PlaylistSettings_GnGTournament",          ""},
            {"PVZServerGame.OnlinePlaylists.PlaylistSettings_GnGTournament_GW1",      ""},
            {"PVZServerGame.OnlinePlaylists.PlaylistSettings_Herb",                   ""},
            {"PVZServerGame.OnlinePlaylists.PlaylistSettings_HerbTournament",         ""},
            {"PVZServerGame.OnlinePlaylists.PlaylistSettings_GnomeBomb",              ""},
            {"PVZServerGame.OnlinePlaylists.PlaylistSettings_GnomeBombTournament",    ""},
            {"PVZServerGame.OnlinePlaylists.PlaylistSettings_TeamElimination",        ""},
            {"PVZServerGame.OnlinePlaylists.PlaylistSettings_TeamElimination16",      ""},
            {"PVZServerGame.OnlinePlaylists.PlaylistSettings_CatsVsDinos",            ""},
            {"PVZServerGame.OnlinePlaylists.PlaylistSettings_CatsVsDinosTournament",  ""},
            {"PVZServerGame.OnlinePlaylists.PlaylistSettings_Suburbination",          ""},
            {"PVZServerGame.OnlinePlaylists.PlaylistSettings_TacoBandits",            ""},
            {"PVZServerGame.OnlinePlaylists.PlaylistSettings_BossHuntPlantPlayers",   ""},
            {"PVZServerGame.OnlinePlaylists.PlaylistSettings_BossHuntZombiePlayers",  ""},
        });
    } else if (section == "KILL_SWITCHES") {
        // Per-license / per-feature kill switches. Names are enumerated
        // by the GW2 binary in the PVZServerGameSettings.KillSwitches
        // reflection table at 0x14316bef0..0x14316c178 (27 entries).
        // Key format follows the convention seen elsewhere in the binary
        // (e.g. "PVZServerGame.CommunityPortal.Widget" at 0x14231f0a0):
        //
        //     PVZServerGame.KillSwitches.<EnumName>
        //
        // Value semantics: "0" = active/enabled, "1" = killed/disabled.
        // Default state below leaves every feature enabled. To kill a
        // particular feature server-side, change its value to "1".
        builder.stringMap("CONF", {
            {"PVZServerGame.KillSwitches.MenchiesChallengeLicense",        "0"},
            {"PVZServerGame.KillSwitches.BlackMarketGnomeLicense",         "0"},
            {"PVZServerGame.KillSwitches.PlantTimeTrialsLicense",          "0"},
            {"PVZServerGame.KillSwitches.ZombieTimeTrialsLicense",         "0"},
            {"PVZServerGame.KillSwitches.PlantGnomeTargetsMinigameLicense", "0"},
            {"PVZServerGame.KillSwitches.ZombieGnomeTargetsMinigameLicense","0"},
            {"PVZServerGame.KillSwitches.GnomeTargetsLeaderboardLicense",  "0"},
            {"PVZServerGame.KillSwitches.Halloween2016License",            "0"},
            {"PVZServerGame.KillSwitches.Halloween2017License",            "0"},
            {"PVZServerGame.KillSwitches.Festivus2016License",             "0"},
            {"PVZServerGame.KillSwitches.Festivus2017License",             "0"},
            {"PVZServerGame.KillSwitches.Springening2017License",          "0"},
            {"PVZServerGame.KillSwitches.LuckOZombie2017License",          "0"},
            {"PVZServerGame.KillSwitches.FestivalWeek1",                   "0"},
            {"PVZServerGame.KillSwitches.FestivalWeek2",                   "0"},
            {"PVZServerGame.KillSwitches.FestivalWeek3",                   "0"},
            {"PVZServerGame.KillSwitches.FestivalWeek4",                   "0"},
            {"PVZServerGame.KillSwitches.FestivalWeek5",                   "0"},
            {"PVZServerGame.KillSwitches.FestivalWeek6",                   "0"},
            {"PVZServerGame.KillSwitches.FestivalWeek7",                   "0"},
            {"PVZServerGame.KillSwitches.FestivalWeek8",                   "0"},
            {"PVZServerGame.KillSwitches.UnderageLicense",                 "0"},
            {"PVZServerGame.KillSwitches.GdprStopProcessLicense",          "0"},
            {"PVZServerGame.KillSwitches.MarketingOptOutLicense",          "0"},
            {"PVZServerGame.KillSwitches.UpsellDisable",                   "0"},
            {"PVZServerGame.KillSwitches.LoyaltyDisable",                  "0"},
            {"PVZServerGame.KillSwitches.AccessDisable",                   "0"},
        });
    } else {
        // Unknown section: return empty map. Returning UTIL_CONFIG_NOT_FOUND
        // would be more correct, but the GW2 callback treats a non-zero
        // BlazeError as fatal and disconnects.
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

    // GW2 request variant uses CMAC + SNAM.
    // Response is the standard telemetry envelope; include SNAM in SVNM.
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

    // setClientState request body: MODE + STAT. Response is empty.
    auto reply = request.createReply();
    return reply;
}


} // namespace ds2::components
