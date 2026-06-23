#include "components/game_manager.hpp"
#include "blaze/tdf.hpp"
#include "network/client_connection.hpp"
#include "utils/logger.hpp"
#include "config.hpp"

namespace gw2::components {

namespace {
    constexpr int64_t kGameId = 35204832001283LL;
    constexpr int64_t kGsid   = 3520483201LL;
    constexpr int64_t kGpvh   = 5177781531LL;
    constexpr int64_t kSeed   = 1142158229LL;
    constexpr int64_t kTid    = 131006LL;
    constexpr int64_t kCtim   = 3557264724342786LL;  // game create time (2x-unix microseconds)
    constexpr int64_t kPtime  = 3557264724343085LL;  // player join time
}

static blaze::TdfStruct buildNetAddr() {
    return blaze::TdfBuilder()
        .beginStruct("EXIP").integer("IP", 2499368075LL).integer("MACI", 0).integer("PORT", 7307).endStruct()
        .beginStruct("INIP").integer("IP", 6464471575LL).integer("MACI", 0).integer("PORT", 7307).endStruct()
        .integer("MACI", 3310897674LL)
        .build();
}

// Big-ass game setup data

static blaze::TdfStruct buildGameSetup() {
    std::vector<uint8_t> emptyBin;

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

static blaze::TdfStruct buildGameStateChange() {
    return blaze::TdfBuilder().integer("GID", kGameId).integer("GSTA", 16).build();
}

static blaze::TdfStruct buildGameAttrChange() {
    return blaze::TdfBuilder()
        .stringMap("ATTR", { {"level","Level_FE_Hub"}, {"mode","FrontEnd"} })
        .integer("GID", kGameId)
        .build();
}

static blaze::TdfStruct buildUserUpdated(int64_t id) {
    return blaze::TdfBuilder().integer("FLGS", 1).integer("ID", id).build();
}

static void sendNotif(std::shared_ptr<network::ClientConnection> client,
                      blaze::ComponentId comp, uint16_t cmd, const blaze::TdfStruct& payload) {
    auto notif = std::make_unique<blaze::Packet>(comp, cmd, blaze::MessageType::Notification, 0);
    notif->setPayload(payload);
    client->sendPacket(std::move(notif));
}

GameManager::GameManager()
    : Component(blaze::ComponentId::GameManager, "GameManager")
{
}

std::unique_ptr<blaze::Packet> GameManager::handlePacket(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
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
            return handleStartMatchmaking(request, client);

        case blaze::GameManagerCommand::cancelMatchmaking:
            return handleCancelMatchmaking(request, client);

        case blaze::GameManagerCommand::updateMeshConnection:
        case blaze::GameManagerCommand::finalizeGameCreation:
            return request.createReply();  // simple ack

        case blaze::GameManagerCommand::advanceGameState:
            client->sendPacket(*request.createReply());
            sendNotif(client, blaze::ComponentId::GameManager, 0x64, buildGameStateChange());
            return nullptr;

        case blaze::GameManagerCommand::setGameAttributes:
            client->sendPacket(*request.createReply());
            sendNotif(client, blaze::ComponentId::GameManager, 0x50, buildGameAttrChange());  // -> Level_FE_Hub
            return nullptr;

        default:
            LOG_WARN("[GameManager] Unknown command: 0x{:04X}", command);
            return request.createReply();
    }
}

std::unique_ptr<blaze::Packet> GameManager::handleCreateGame(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {

    GameSession session;
    session.hostUserId = client->getUserId();
    session.currentPlayers = 1;
    session.maxPlayers = 4;
    session.players.push_back(client->getUserId());
    createGameSession(session);

    auto reply = request.createReply();
    reply->setPayload(blaze::TdfBuilder().integer("GID", kGameId).build());  // must match GAME.GID
    client->sendPacket(*reply);

    sendNotif(client, blaze::ComponentId::GameManager,  0x0014, buildGameSetup());
    sendNotif(client, blaze::ComponentId::UserSessions, 0x0005, buildUserUpdated(628905009LL));
    sendNotif(client, blaze::ComponentId::UserSessions, 0x0005, buildUserUpdated(2011894521122LL));
    sendNotif(client, blaze::ComponentId::UserSessions, 0x0005, buildUserUpdated(2014390830599LL));

    LOG_INFO("[game] createGame -> sent NOTIFYGAMESETUP (built)");
    return nullptr;
}

std::unique_ptr<blaze::Packet> GameManager::handleDestroyGame(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    
    auto requestTdf = request.getPayloadAsTdf();
    
    uint64_t gameId = 0;
    if (auto it = requestTdf.find("GID"); it != requestTdf.end()) {
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

std::unique_ptr<blaze::Packet> GameManager::handleJoinGame(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    
    auto requestTdf = request.getPayloadAsTdf();
    
    uint64_t gameId = 0;
    if (auto it = requestTdf.find("GID"); it != requestTdf.end()) {
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

std::unique_ptr<blaze::Packet> GameManager::handleListGames(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    
    auto games = listGameSessions();

    blaze::TdfBuilder builder;
    builder.integer("GNUM", games.size());  // Number of games
    
    // TODO: Build proper list of games
    
    auto reply = request.createReply();
    reply->setPayload(builder.build());
    
    LOG_INFO("[game] listGames count={}", games.size());
    
    return reply;
}

std::unique_ptr<blaze::Packet> GameManager::handleStartMatchmaking(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {

    auto requestTdf = request.getPayloadAsTdf();
    
    // For now, just acknowledge matchmaking started
    
    blaze::TdfBuilder builder;
    builder
        .integer("MSID", 1)  // Matchmaking session ID
        .integer("USID", client->getUserId());
    
    auto reply = request.createReply();
    reply->setPayload(builder.build());
    
    LOG_INFO("[game] startMatchmaking player={}", client->getPersonaName());
    
    // TODO: Send async notification when match found
    
    return reply;
}

std::unique_ptr<blaze::Packet> GameManager::handleCancelMatchmaking(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    auto reply = request.createReply();
    
    LOG_INFO("[game] cancelMatchmaking player={}", client->getPersonaName());
    
    return reply;
}

uint64_t GameManager::createGameSession(const GameSession& session) {
    std::lock_guard<std::mutex> lock(m_gamesMutex);
    
    uint64_t gameId = m_nextGameId++;
    GameSession newSession = session;
    newSession.gameId = gameId;
    
    m_games[gameId] = newSession;
    
    return gameId;
}

bool GameManager::destroyGameSession(uint64_t gameId) {
    std::lock_guard<std::mutex> lock(m_gamesMutex);
    return m_games.erase(gameId) > 0;
}

GameSession* GameManager::getGameSession(uint64_t gameId) {
    std::lock_guard<std::mutex> lock(m_gamesMutex);
    auto it = m_games.find(gameId);
    if (it != m_games.end()) {
        return &it->second;
    }
    return nullptr;
}

std::vector<GameSession> GameManager::listGameSessions() {
    std::lock_guard<std::mutex> lock(m_gamesMutex);
    
    std::vector<GameSession> result;
    result.reserve(m_games.size());
    
    for (const auto& [id, session] : m_games) {
        result.push_back(session);
    }
    
    return result;
}

} // namespace gw2::components
