#include "components/game_manager.hpp"
#include "components/user_sessions.hpp"
#include "blaze/tdf.hpp"
#include "network/client_connection.hpp"
#include "utils/logger.hpp"
#include "config.hpp"

/*
                        ,---.
                       /    |
                      /     |
                     /      |
                    /       |
               ___,'        |
             <  -'          :
              `-.__..--'``-,_\_
                 |o/ <o>` :,.)_`>
                 :/ `     ||/)
                 (_.).__,-` |\
                 /( `.``   `| :
                 \'`-.)  `  ; ;
                 | `       /-<
                 |     `  /   `.
 ,-_-..____     /|  `    :__..-'\
/,'-.__\\  ``-./ :`      ;       \
`\ `\  `\\  \ :  (   `  /  ,   `. \
  \` \   \\   |  | `   :  :     .\ \
   \ `\_  ))  :  ;     |  |      ): :
  (`-.-'\ ||  |\ \   ` ;  ;       | |
   \-_   `;;._   ( `  /  /_       | |
    `-.-.// ,'`-._\__/_,'         ; |
       \:: :     /     `     ,   /  |
        || |    (        ,' /   /   |
        ||                ,'   / SSt|
------------------------------------------------
Beware young adventurer, avert your eyes. Shitty code lies ahead
 */


namespace gw2::components {

namespace {
    constexpr int64_t kGameId = 35204832001283;
    constexpr int64_t kGsid   = 3520483201;
    constexpr int64_t kGpvh   = 5177781531;
    constexpr int64_t kSeed   = 1142158229;
    constexpr int64_t kTid    = 131006;
    constexpr int64_t kCtim   = 3557264724342786;  // game create time
    constexpr int64_t kPtime  = 3557264724343085;  // player join time

    constexpr int64_t kDedGameId     = 17814386520391; // 0x1033BC2E3147
    constexpr int64_t kDedHostId     = -9178336040581069026;
    constexpr int64_t kDedHostConnGrp= 1411041660678;
    constexpr int64_t kDedHostSid    = 1412541660678;
    constexpr int64_t kScenarioId    = 450359962880915339;
    constexpr int64_t kSessionId     = 504403158403780290;

    constexpr const char* kDedGameUuid   = "207d566b-9720-4b88-aeea-cd4dd9cc049d";
    constexpr const char* kDedPlayerUuid = "9c3d634f-bd82-4c8d-a706-ba443e434707";
}

static blaze::TdfStruct buildNetAddr() {
    return blaze::TdfBuilder()
        .beginStruct("EXIP").integer("IP", 2499368075).integer("MACI", 0).integer("PORT", 7307).endStruct()
        .beginStruct("INIP").integer("IP", 6464471575).integer("MACI", 0).integer("PORT", 7307).endStruct()
        .integer("MACI", 3310897674)
        .build();
}

static blaze::TdfStruct buildGameSetup() {
    const std::vector<uint8_t> emptyBin;

    blaze::TdfStruct game = blaze::TdfBuilder()
        .intList("ADMN", { config::blazeId })
        .integer("APRS", 1)
        .stringMap("ATTR", { {"game","0"}, {"level","Level_FE_Splash"}, {"mode","Splash"} })
        .intList("CAP", { 4, 0, 0, 0 })
        .integer("CCMD", 1)
        .string("COID", "")
        .string("CSID", "")
        .integer("CTIM", kCtim)
        .beginStruct("DHST").integer("CONG",0).integer("CSID",0).integer("HPID",0).integer("HSES",0).integer("HSLT",0).endStruct()
        .integer("DRTO", 0)
        .beginStruct("ESID")
            .beginStruct("PS4").string("NPSI","").endStruct()
            .beginStruct("XONE").string("COID","").string("ESNM","").string("STMN","").endStruct()
        .endStruct()
        .string("ESNM", "")
        .integer("GGTY", 1)
        .integer("GID", kGameId)
        .integer("GMRG", 0)
        .string("GNAM", "")
        .integer("GPVH", kGpvh)
        .integer("GSET", 1114116)
        .integer("GSID", kGsid)
        .integer("GSTA", 1)
        .string("GTYP", "")
        .string("GURL", "")
        .integer("MCAP", 4)
        .integer("MNCP", 1)
        .string("NPSI", "")
        .beginStruct("NQOS").integer("BWHR",0).integer("DBPS",13714230).integer("NAHR",0).integer("NATT",3).integer("UBPS",7680000).endStruct()
        .integer("NRES", 0)
        .integer("NTOP", 447)
        .string("PGID", "")
        .binary("PGSR", emptyBin)
        .beginStruct("PHST").integer("CONG",config::connGroupId).integer("CSID",0).integer("HPID",config::blazeId).integer("HSES",config::userSessionId).integer("HSLT",0).endStruct()
        .integer("PRES", 1)
        .integer("PRTO", 0)
        .string("PSAS", "bio-iad")
        .integer("PSEU", 0)
        .integer("QCAP", 0)
        .beginStruct("RNFO")
            .beginMap("CRIT", "string", "struct")
                .string("")
                .beginStruct().integer("RCAP", 4).endStruct()
            .endMap()
        .endStruct()
        .string("SCID", "")
        .integer("SEED", kSeed)
        .string("STMN", "")
        .beginStruct("THST").integer("CONG",config::connGroupId).integer("CSID",0).integer("HPID",config::blazeId).integer("HSES",config::userSessionId).integer("HSLT",0).endStruct()
        .intList("TIDS", { kTid })
        .string("UUID", "d439e0e3-8c1c-4249-962d-e807b77121d5")
        .integer("VOIP", 0)
        .string("VSTR", "66-3644673")
        .build();
    blaze::TdfList hnet;
    {
        blaze::TdfUnion u;
        u.arm = 0x02;
        u.member = std::make_shared<blaze::TdfValue>("VALU", blaze::TdfType::Struct, buildNetAddr());
        hnet.push_back(std::make_shared<blaze::TdfValue>("", blaze::TdfType::Struct, u));
    }
    game["HNET"] = std::make_shared<blaze::TdfValue>("HNET", blaze::TdfType::List, hnet);

    auto pnetMember = std::make_shared<blaze::TdfValue>("VALU", blaze::TdfType::Struct, buildNetAddr());
    blaze::TdfStruct player = blaze::TdfBuilder()
        .binary("BLOB", emptyBin)
        .integer("CONG", config::connGroupId).integer("CSID", 0).integer("DSUI", 0)
        .binary("EXBL", emptyBin)
        .integer("EXID", config::nucleusId).integer("GID", kGameId).integer("JFPS", 1).integer("JVMM", 0).integer("LOC", config::locale)
        .string("NAME", config::persona).string("NASP", config::nasp)
        .integer("PID", config::blazeId)
        .unionValue("PNET", 0x02, pnetMember)
        .integer("PSET", 1).integer("RCRE", 0).string("ROLE", "")
        .integer("SID", 0).integer("SLOT", 0).integer("STAT", 4).integer("TIDX", 0)
        .integer("TIME", kPtime)
        .objectId("UGID", 0x7802, 0x0002, config::connGroupId)
        .integer("UID", config::userSessionId)
        .string("UUID", "9c3d634f-bd82-4c8d-a706-ba443e434707")
        .build();
    blaze::TdfList pros;
    pros.push_back(std::make_shared<blaze::TdfValue>("", blaze::TdfType::Struct, player));

    auto dlsc = std::make_shared<blaze::TdfValue>("DLSC", blaze::TdfType::Struct,
                    blaze::TdfBuilder().integer("DCTX", 0).build());
    blaze::TdfStruct setup = blaze::TdfBuilder()
        .integer("LFPJ", 0)
        .string("MNAM", "playlist")
        .beginStruct("QOSS").integer("DURA",0).integer("INTV",0).integer("SIZE",0).endStruct()
        .integer("QOSV", 0)
        .unionValue("REAS", 0x00, dlsc)
        .integer("TELM", 40000000)
        .build();
    setup["GAME"] = std::make_shared<blaze::TdfValue>("GAME", blaze::TdfType::Struct, game);
    setup["PROS"] = std::make_shared<blaze::TdfValue>("PROS", blaze::TdfType::List, pros);
    return setup;
}

static blaze::TdfStruct buildDedicatedNetAddr(int64_t ip, int64_t port) {
    return blaze::TdfBuilder()
        .beginStruct("EXIP").integer("IP", ip).integer("MACI", 0).integer("PORT", port).endStruct()
        .beginStruct("INIP").integer("IP", ip).integer("MACI", 0).integer("PORT", port).endStruct()
        .integer("MACI", 0)
        .build();
}

static std::shared_ptr<blaze::TdfValue> buildNetAddrList(const std::string& tag, const blaze::TdfStruct& addr) {
    blaze::TdfUnion u;
    u.arm = 0x02;  // IpPairAddress arm
    u.member = std::make_shared<blaze::TdfValue>("VALU", blaze::TdfType::Struct, addr);
    blaze::TdfList list;
    list.push_back(std::make_shared<blaze::TdfValue>("", blaze::TdfType::Struct, u));
    return std::make_shared<blaze::TdfValue>(tag, blaze::TdfType::List, list);
}

static blaze::TdfStruct buildDedicatedGameSetup() {
    std::vector<uint8_t> emptyBin;

    blaze::TdfStruct dedAddr = buildDedicatedNetAddr(config::gameServerIp, config::gameServerPort);

    // GAME = ReplicatedGameData (Blaze::GameManager::ReplicatedGameData).
    blaze::TdfStruct game = blaze::TdfBuilder()
        .intList("ADMN", { kDedHostId, config::blazeId })  // adminPlayerList
        .integer("APRS", 1)                            // ownsFirstPartyPresence — determines if a game session owns first party presence on the client (Xbox 360 only)
        .stringMap("ATTR", { {"census","PL_02"}, {"hash","3644673"}, {"playlist","PL_02"} }) // gameAttribs
        .intList("CAP", { 24, 0, 0, 0 })               // slotCapacities
        .integer("CCMD", 0)                            // (live-capture extra; not in client ReplicatedGameData)
        .string("COID", "")                            // externalSessionCorrelationId — DEPRECATED, use ExternalSessionIdentification.
        .string("CSID", "")                            // Identifier provided by CCS the first time a connection is made on a particular DC instance.
        .integer("CTIM", kCtim)                        // createTime
        .beginStruct("DHST").integer("CONG",kDedHostConnGrp).integer("CSID",0).integer("HPID",kDedHostId).integer("HSES",kDedHostSid).integer("HSLT",0).endStruct() // dedicatedServerHostInfo — The dedicated server host for the game, if there is one.
        .integer("DRTO", 0)                            // Overrides the player reservation timeout for disconnected players.
        .beginStruct("ESID")                           // esid — External Session identification.
            .beginStruct("PS4").string("NPSI","").endStruct()
            .beginStruct("XONE").string("COID","").string("ESNM","").string("STMN","").endStruct()
        .endStruct()
        .string("ESNM", "")                            // externalSessionName — DEPRECATED, use ExternalSessionIdentification.
        .integer("GGTY", 0)                            // gameType
        .integer("GID", kDedGameId)                    // gameId
        .integer("GMRG", 0)                            // gameModRegister
        .string("GNAM", "GW2")                         // gameName
        .integer("GPVH", kGpvh)                        // gameProtocolVersionHash
        .integer("GSET", 33038)                        // gameSettings (goodMatchmakingDump)
        .integer("GSID", kGsid)
        .integer("GSTA", 131)                          // gameState (goodMatchmakingDump)
        .string("GTYP", "")                            // gameReportName — Game Type used for game reporting as passed up in the request.
        .string("GURL", "")                            // gameStatusUrl
        .integer("MCAP", 24)                           // maxPlayerCapacity
        .integer("MNCP", 1)                            // minPlayerCapacity
        .string("NPSI", "")                            // npSessionId — DEPRECATED, use ExternalSessionIdentification.
        .beginStruct("NQOS").integer("BWHR",0).integer("DBPS",0).integer("NAHR",0).integer("NATT",0).integer("UBPS",0).endStruct() // networkQosData
        .integer("NRES", 0)                            // serverNotResetable — game is not resetable (CLIENT_SERVER_DEDICATED only; prevents ever entering RESETABLE).
        .integer("NTOP", 1)                            // networkTopology (1 = CLIENT_SERVER_DEDICATED)
        .string("PGID", "")                            // persistedGameId — used only when enablePersistedGameIds is true.
        .binary("PGSR", emptyBin)                      // persistedGameIdSecret — used only when enablePersistedGameIds is true.
        .beginStruct("PHST").integer("CONG",config::connGroupId).integer("CSID",1).integer("HPID",config::blazeId).integer("HSES",kDedHostSid).integer("HSLT",1).endStruct() // platformHostInfo — The platform specific host (ie. xbox presence session holder).
        .integer("PRES", 1)                            // presenceMode
        .integer("PRTO", 0)                            // Overrides the player reservation timeout for joining players.
        .string("PSAS", "bio-iad")                     // pingSiteAlias
        .integer("PSEU", 0)                            // (live-capture extra; not in client ReplicatedGameData)
        .integer("QCAP", 5)                            // queueCapacity
        .beginStruct("RNFO")                           // roleInformation — The roles and capacities, and criteria, supported in this game session.
            .beginMap("CRIT", "string", "struct")      // roleCriteriaMap — Players must pass these entry criteria to join.
                .string("")
                .beginStruct().integer("RCAP", 12).endStruct() // roleCapacity
            .endMap()
        .endStruct()
        .string("SCID", "")                            // scid — External Session service config identifier
        .integer("SEED", kSeed)                        // sharedSeed — a 32 bit number shared between clients
        .string("STMN", "")                            // externalSessionTemplateName — DEPRECATED, use ExternalSessionIdentification.
        .beginStruct("THST").integer("CONG",kDedHostConnGrp).integer("CSID",0).integer("HPID",kDedHostId).integer("HSES",kDedHostSid).integer("HSLT",0).endStruct() // topologyHostInfo — The topology host for the game (everyone connects to this person).
        .intList("TIDS", { 1, 2 })                     // teamIds
        .string("UUID", kDedGameUuid)                  // uUID
        .integer("VOIP", 1)                            // voipNetwork = 1 (live dedicated matches enable it)
        .string("VSTR", "66-3644673")                  // gameProtocolVersionString — must match client GVER (same as the hub setup)
        .build();

    game["DNET"] = buildNetAddrList("DNET", dedAddr); // dedicatedServerHostNetworkAddressList
    game["HNET"] = buildNetAddrList("HNET", dedAddr); // topologyHostNetworkAddressList

    // ReplicatedGamePlayer
    auto pnetMember = std::make_shared<blaze::TdfValue>("VALU", blaze::TdfType::Struct, buildDedicatedNetAddr(config::gameServerIp, 3659));
    blaze::TdfStruct player = blaze::TdfBuilder()
        .binary("BLOB", emptyBin)                      // customData
        .integer("CONG", config::connGroupId)          // connectionGroupId
        .integer("CSID", 1)                            // connectionSlotId
        .integer("DSUI", 0)                            // dirtySockUserIndex
        .binary("EXBL", emptyBin)                      // externalBlob
        .integer("EXID", config::nucleusId)            // externalId
        .integer("GID", kDedGameId)                    // gameId
        .integer("JFPS", 1)                            // joinedViaMatchmaking / joinFlags
        .integer("JVMM", 1)                            // joinedGameViaMatchmaking
        .integer("LOC", config::locale)                // accountLocale
        .string("NAME", config::persona)               // playerName
        .string("NASP", config::nasp)                  // personaNamespace
        .integer("PID", config::blazeId)               // playerId (BlazeId)
        .unionValue("PNET", 0x02, pnetMember)          // playerNetworkAddress
        .integer("PSET", 1)                            // playerSettings
        .integer("RCRE", 0)                            // reservationCreation
        .string("ROLE", "")                            // roleName
        .integer("SID", 1)                             // slotId
        .integer("SLOT", 0)                            // slotType
        .integer("STAT", 2)                            // playerState (2 = CONNECTING)
        .integer("TIDX", 0)                            // teamIndex
        .integer("TIME", 0)                            // joinedGameTimestamp
        .objectId("UGID", 0x7802, 0x0002, config::connGroupId) // userGroupId
        .integer("UID", config::userSessionId)         // userSessionId
        .string("UUID", kDedPlayerUuid)                // uUID
        .build();
    blaze::TdfList pros;
    pros.push_back(std::make_shared<blaze::TdfValue>("", blaze::TdfType::Struct, player));

    // MatchmakingSetupContext
    auto mmsc = std::make_shared<blaze::TdfValue>("MMSC", blaze::TdfType::Struct,
        blaze::TdfBuilder()
            .integer("FIT", 1073751774)                // fitScore
            .integer("GENT", 0)                        // gameEntryType
            .integer("MAXF", 1073751824)               // maxPossibleFitScore
            .integer("MSCD", kScenarioId)              // scenarioId
            .integer("MSID", kSessionId)               // sessionId
            .integer("RSLT", 1)                        // matchmakingResult (1 = SUCCESS_JOINED_NEW_GAME)
            .integer("TOUT", 15000000)                 // timeout
            .integer("TTM", 1000)                      // timeToMatch
            .integer("USID", config::userSessionId)    // userSessionId
            .build());

    // NotifyGameSetup
    blaze::TdfStruct setup = blaze::TdfBuilder()
        .integer("LFPJ", 0)                            // isLockableForPreferredJoins
        .string("MNAM", "playlist")                    // gameModeAttributeName
        .beginStruct("QOSS").integer("DURA",0).integer("INTV",0).integer("SIZE",0).endStruct() // qosSettings
        .integer("QOSV", 0)                            // performQosValidation
        .unionValue("REAS", 0x03, mmsc)                // gameSetupReason (arm 3 = matchmakingSetupContext)
        .integer("TELM", 20000000)                     // qosTelemetryInterval
        .build();
    setup["GAME"] = std::make_shared<blaze::TdfValue>("GAME", blaze::TdfType::Struct, game);
    setup["PROS"] = std::make_shared<blaze::TdfValue>("PROS", blaze::TdfType::List, pros);
    return setup;
}

static blaze::TdfStruct buildMatchmakingFinished() {
    return blaze::TdfBuilder()
        .beginStruct("CONV").integer("FCNT",0).integer("NTOP",0).integer("TIER",0).endStruct()
        .integer("DISP", 1)
        .integer("GID", kDedGameId)
        .objectId("GRID", 0, 0, 0)
        .integer("MSCD", kScenarioId)
        .integer("MSID", kSessionId)
        .build();
}

static blaze::TdfStruct buildMatchmakingAsyncStatus() {
    blaze::TdfStruct asilEntry = blaze::TdfBuilder()
        .beginStruct("CGS").integer("EVST",0).integer("MMSN",0).integer("NOMP",0).endStruct()
        .beginStruct("FGS").integer("GNUM",1).endStruct()
        .beginMap("GASM", "string", "struct")
            .string("playlistRule")
            .beginStruct().string("NAME","playlistRule").list("VALU", blaze::TdfType::String, {"PL_02"}).endStruct()
        .endMap()
        .beginStruct("GEOS").integer("DIST",0).endStruct()
        .beginStruct("HBRD").integer("BVAL",0).endStruct()
        .beginStruct("HVRD").integer("VVAL",3).endStruct()
        .beginStruct("PLCN").integer("PMAX",0).integer("PMIN",0).endStruct()
        .beginStruct("PLUT").integer("PMAX",100).integer("PMIN",0).endStruct()
        .beginStruct("PSRS").list("VALU", blaze::TdfType::String, {"bio-sjc","bio-iad"}).endStruct()
        .beginStruct("RRDA").integer("RVAL",0).endStruct()
        .beginStruct("TBRS").integer("SDIF",0).endStruct()
        .beginStruct("TCPS").string("NAME","").endStruct()
        .beginStruct("TMSS").integer("PCNT",0).endStruct()
        .beginStruct("TOTS").integer("PMAX",0).integer("PMIN",0).endStruct()
        .beginStruct("TPPS").integer("BDIF",0).integer("BOTN",0).string("NAME","").integer("TDIF",0).integer("TOPN",0).endStruct()
        .beginStruct("TUBS").integer("MUED",0).string("NAME","").integer("SDIF",0).endStruct()
        .beginStruct("VGRS").integer("VVAL",0).endStruct()
        .build();
    blaze::TdfList asil;
    asil.push_back(std::make_shared<blaze::TdfValue>("", blaze::TdfType::Struct, asilEntry));

    blaze::TdfStruct s = blaze::TdfBuilder()
        .integer("MSCD", kScenarioId)
        .integer("MSID", kSessionId)
        .integer("USID", config::userSessionId)
        .build();
    s["ASIL"] = std::make_shared<blaze::TdfValue>("ASIL", blaze::TdfType::List, asil);
    return s;
}

static blaze::TdfStruct buildGameStateChange() {
    return blaze::TdfBuilder().integer("GID", kGameId).integer("GSTA", 16).build();
}

static blaze::TdfStruct buildUserUpdated(int64_t id) {
    return blaze::TdfBuilder().integer("FLGS", 1).integer("ID", id).build();
}

static void sendNotif(const std::shared_ptr<ClientConnection> &client, blaze::ComponentId comp, uint16_t cmd, const blaze::TdfStruct& payload) {
    auto notif = std::make_unique<blaze::Packet>(comp, cmd, blaze::MessageType::Notification, 0);
    notif->setPayload(payload);
    client->sendPacket(std::move(notif));
}

uint64_t GameManager::createGameSession(const GameSession& session) {
    std::lock_guard lock(m_gamesMutex);

    const uint64_t gameId = m_nextGameId++;
    GameSession newSession = session;
    newSession.gameId = gameId;

    m_games[gameId] = newSession;

    return gameId;
}

bool GameManager::destroyGameSession(const uint64_t gameId) {
    std::lock_guard lock(m_gamesMutex);
    return m_games.erase(gameId) > 0;
}

GameSession* GameManager::getGameSession(uint64_t gameId) {
    std::lock_guard lock(m_gamesMutex);
    const auto it = m_games.find(gameId);
    if (it != m_games.end()) {
        return &it->second;
    }
    return nullptr;
}

std::vector<GameSession> GameManager::listGameSessions() {
    std::lock_guard lock(m_gamesMutex);

    std::vector<GameSession> result;
    result.reserve(m_games.size());

    for (const auto& [id, session] : m_games) {
        result.push_back(session);
    }

    return result;
}

GameManager::GameManager()
    : Component(blaze::ComponentId::GameManager, "GameManager")
{
}

std::unique_ptr<blaze::Packet> GameManager::handlePacket(const blaze::Packet& request, const std::shared_ptr<ClientConnection> client) {
    uint16_t command = request.getCommand();
    
    switch (static_cast<blaze::GameManagerCommand>(command)) {
        case blaze::GameManagerCommand::createGame:
            return handleCreateGame(request, client);

        case blaze::GameManagerCommand::destroyGame:
            return handleDestroyGame(request, client);

        case blaze::GameManagerCommand::joinGame:
            return handleJoinGame(request, client);

        case blaze::GameManagerCommand::getGameListSnapshot:
            return handleListGames(request, client);

        case blaze::GameManagerCommand::startMatchmaking:
        case blaze::GameManagerCommand::startMatchmakingScenario:
            return handleStartMatchmaking(request, client);

        case blaze::GameManagerCommand::cancelMatchmaking:
        case blaze::GameManagerCommand::cancelMatchmakingScenario:
            return handleCancelMatchmaking(request, client);

        case blaze::GameManagerCommand::updateMeshConnection: {
            client->sendPacket(*request.createReply());
            auto tdf = request.getPayloadAsTdf();
            int64_t gid = 0;
            if (auto g = tdf.find("GID"); g != tdf.end() && g->second && g->second->type == blaze::TdfType::Integer)
                gid = std::get<blaze::TdfInteger>(g->second->value);
            if (gid == kDedGameId) {
                sendNotif(client, blaze::ComponentId::GameManager, 0x74,
                    blaze::TdfBuilder().integer("GID", kDedGameId).integer("PID", config::blazeId).integer("STAT", 4).build());
                sendNotif(client, blaze::ComponentId::GameManager, 0x1e,
                    blaze::TdfBuilder().integer("GID", kDedGameId).integer("PID", config::blazeId).integer("TIME", kPtime).build());
                UserSessions::setDedicatedGame(static_cast<uint64_t>(kDedGameId));
                UserSessions::pushUserSessionExtendedDataUpdate(client);
                LOG_INFO("[game] updateMeshConnection dedicated -> player ACTIVE_CONNECTED (gid={})", gid);
            }
            return nullptr;
        }

        // Blaze::GameManager::LeaveGameByGroup (id=0x16): Remove a connection group from the game.
        case blaze::GameManagerCommand::leaveGameByGroup: {
            auto tdf = request.getPayloadAsTdf();
            int64_t gid = 0;
            if (auto g = tdf.find("GID"); g != tdf.end() && g->second && g->second->type == blaze::TdfType::Integer)
                gid = std::get<blaze::TdfInteger>(g->second->value);
            if (gid == kDedGameId) {
                UserSessions::setDedicatedGame(0);
                LOG_INFO("[game] leaveGameByGroup dedicated -> cleared membership (gid={})", gid);
            }
            return request.createReply();
        }

        // Blaze::GameManager::FinalizeGameCreation (id=0x0F): Finalize the game creation process
        case blaze::GameManagerCommand::finalizeGameCreation:
            return request.createReply();  // simple ack

        // Blaze::GameManager::AdvanceGameState (id=0x03): Advance the game to a new game state
        case blaze::GameManagerCommand::advanceGameState:
            client->sendPacket(*request.createReply());
            sendNotif(client, blaze::ComponentId::GameManager, 0x64, buildGameStateChange());
            return nullptr;

        // Blaze::GameManager::SetGameSettings (id=0x04): Updates the game settings for the game
        case blaze::GameManagerCommand::setGameSettings: {
            client->sendPacket(*request.createReply());
            auto tdf = request.getPayloadAsTdf();
            int64_t gid = kGameId, gset = 0;
            if (auto g = tdf.find("GID"); g != tdf.end() && g->second && g->second->type == blaze::TdfType::Integer)
                gid = std::get<blaze::TdfInteger>(g->second->value);
            if (auto s = tdf.find("GSET"); s != tdf.end() && s->second && s->second->type == blaze::TdfType::Integer)
                gset = std::get<blaze::TdfInteger>(s->second->value);
            auto _n = blaze::TdfBuilder().integer("ATTR", gset).integer("GID", gid).build();
            sendNotif(client, blaze::ComponentId::GameManager, 0x6E, _n);
            return nullptr;
        }

        // Blaze::GameManager::SetGameAttributes (id=0x07): Update the game's attributes
        case blaze::GameManagerCommand::setGameAttributes: {
            client->sendPacket(*request.createReply());
            auto tdf = request.getPayloadAsTdf();
            int64_t gid = kGameId;
            if (auto g = tdf.find("GID"); g != tdf.end() && g->second && g->second->type == blaze::TdfType::Integer)
                gid = std::get<blaze::TdfInteger>(g->second->value);
            std::map<std::string, std::string> attr;
            if (auto a = tdf.find("ATTR"); a != tdf.end() && a->second && a->second->type == blaze::TdfType::Map) {
                const auto& m = std::get<blaze::TdfMapWrapper>(a->second->value);
                for (const auto& [k, v] : m.data)
                    if (v && v->type == blaze::TdfType::String)
                        attr[k] = std::get<blaze::TdfString>(v->value);
            }
            auto _n = blaze::TdfBuilder().stringMap("ATTR", attr).integer("GID", gid).build();
            sendNotif(client, blaze::ComponentId::GameManager, 0x50, _n);
            return nullptr;
        }

        default:
            LOG_WARN("[GameManager] Unknown command: 0x{:04X}", command);
            return request.createReply();
    }
}

std::unique_ptr<blaze::Packet> GameManager::handleCreateGame(const blaze::Packet& request, const std::shared_ptr<ClientConnection> &client) {

    GameSession session;
    session.hostUserId = client->getUserId();
    session.currentPlayers = 1;
    session.maxPlayers = 4;
    session.players.push_back(client->getUserId());
    createGameSession(session);

    const auto reply = request.createReply();
    reply->setPayload(blaze::TdfBuilder().integer("GID", kGameId).build());  // must match GAME.GID
    client->sendPacket(*reply);

    sendNotif(client, blaze::ComponentId::GameManager,  0x0014, buildGameSetup());
    sendNotif(client, blaze::ComponentId::UserSessions, 0x0005, buildUserUpdated(628905009));
    sendNotif(client, blaze::ComponentId::UserSessions, 0x0005, buildUserUpdated(2011894521122));
    sendNotif(client, blaze::ComponentId::UserSessions, 0x0005, buildUserUpdated(2014390830599));

    LOG_INFO("[game] createGame -> sent NOTIFYGAMESETUP (built)");
    return nullptr;
}

std::unique_ptr<blaze::Packet> GameManager::handleDestroyGame(const blaze::Packet& request, std::shared_ptr<ClientConnection> client) {
    
    auto requestTdf = request.getPayloadAsTdf();
    
    uint64_t gameId = 0;
    if (const auto it = requestTdf.find("GID"); it != requestTdf.end()) {
        if (it->second && it->second->type == blaze::TdfType::Integer) {
            gameId = std::get<blaze::TdfInteger>(it->second->value);
        }
    }
    
    bool destroyed = destroyGameSession(gameId);
    
    if (!destroyed) {
        return createError(request, blaze::BlazeError::ERR_GAME_NOT_FOUND);
    }
    
    LOG_INFO("[game] destroyGame id={}", gameId);
    
    return request.createReply();
}

std::unique_ptr<blaze::Packet> GameManager::handleJoinGame(const blaze::Packet& request, std::shared_ptr<ClientConnection> client) {
    
    auto requestTdf = request.getPayloadAsTdf();
    
    uint64_t gameId = 0;
    if (const auto it = requestTdf.find("GID"); it != requestTdf.end()) {
        if (it->second && it->second->type == blaze::TdfType::Integer) {
            gameId = std::get<blaze::TdfInteger>(it->second->value);
        }
    }
    
    auto* session = getGameSession(gameId);
    if (!session) {
        return createError(request, blaze::BlazeError::ERR_GAME_NOT_FOUND);
    }
    
    if (session->currentPlayers >= session->maxPlayers) {
        return createError(request, blaze::BlazeError::ERR_GAME_FULL);
    }

    // Add player to game
    {
        std::lock_guard<std::mutex> lock(m_gamesMutex);
        session->players.push_back(client->getUserId());
        session->currentPlayers++;
    }
    
    // Build response with game info
    blaze::TdfBuilder builder;
    builder
        .integer("GID", gameId)
        .integer("HUID", session->hostUserId)
        .triple("HNET", session->hostIp, session->hostPort, 2)
        .integer("PCNT", session->currentPlayers);
    
    auto reply = request.createReply();
    reply->setPayload(builder.build());
    
    LOG_INFO("[game] joinGame id={} player={}", gameId, client->getPersonaName());
    
    return reply;
}

std::unique_ptr<blaze::Packet> GameManager::handleListGames(const blaze::Packet& request, std::shared_ptr<ClientConnection> client) {

    const auto games = listGameSessions();

    blaze::TdfBuilder builder;
    builder.integer("GNUM", games.size());  // Number of games
    
    // TODO: Build proper list of games
    
    auto reply = request.createReply();
    reply->setPayload(builder.build());
    
    LOG_INFO("[game] listGames count={}", games.size());
    
    return reply;
}

std::unique_ptr<blaze::Packet> GameManager::handleStartMatchmaking(const blaze::Packet& request, std::shared_ptr<ClientConnection> client) {

    // StartMatchmaking(Scenario)Response
    auto reply = request.createReply();
    reply->setPayload(blaze::TdfBuilder()
        .string("COID", "")
        .string("ESNM", "")
        .integer("MSID", kScenarioId)
        .string("SCID", "")
        .string("STMN", "")
        .build());
    client->sendPacket(std::move(reply));

    LOG_INFO("[game] startMatchmaking player={} -> dedicated gameserver {}.{}.{}.{}:{}",
             client->getPersonaName(),
             (config::gameServerIp >> 24) & 0xFF, (config::gameServerIp >> 16) & 0xFF,
             (config::gameServerIp >> 8) & 0xFF, config::gameServerIp & 0xFF,
             config::gameServerPort);

    // notifyGameSetup (0x14)
    sendNotif(client, blaze::ComponentId::GameManager, 0x14, buildDedicatedGameSetup());
    // notifyMatchmakingAsyncStatus (0x0c)
    sendNotif(client, blaze::ComponentId::GameManager, 0x0c, buildMatchmakingAsyncStatus());
    // notifyMatchmakingScenarioFinished (0x0b)
    sendNotif(client, blaze::ComponentId::GameManager, 0x0b, buildMatchmakingFinished());

    return nullptr;
}

std::unique_ptr<blaze::Packet> GameManager::handleCancelMatchmaking(const blaze::Packet& request, std::shared_ptr<ClientConnection> client) {
    auto reply = request.createReply();
    
    LOG_INFO("[game] cancelMatchmaking player={}", client->getPersonaName());

    return reply;
}

} // namespace gw2::components
