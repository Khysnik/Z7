#include "components/game_manager.hpp"
#include "blaze/tdf.hpp"
#include "network/client_connection.hpp"
#include "utils/logger.hpp"

namespace ds2::components {

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
        
        default:
            LOG_WARN("[GameManager] Unknown command: 0x{:04X}", command);
            return request.createReply();
    }
}

std::unique_ptr<blaze::Packet> GameManager::handleCreateGame(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    
    auto requestTdf = request.getPayloadAsTdf();
    
    // Create new game session
    GameSession session;
    session.hostUserId = client->getUserId();
    session.currentPlayers = 1;
    session.maxPlayers = 4;
    session.gameName = client->getPersonaName() + "'s Game";
    session.gameMode = "Survival";
    
    // Get host network info
    // TODO: Parse from client data
    session.hostIp = 0x7F000001;  // 127.0.0.1
    session.hostPort = 3659;      // Default game port
    
    session.players.push_back(client->getUserId());
    
    uint64_t gameId = createGameSession(session);
    
    // Build response
    blaze::TdfBuilder builder;
    builder
        .integer("GID", gameId)        // Game ID
        .integer("HUID", client->getUserId())  // Host user ID
        .integer("PCNT", 1)            // Player count
        .integer("MCNT", session.maxPlayers);  // Max count
    
    auto reply = request.createReply();
    reply->setPayload(builder.build());
    
    LOG_INFO("[game] createGame id={} host={}", gameId, client->getPersonaName());
    
    return reply;
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
    
    // Build response with game list
    // For now, return empty list structure
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
    
    // Parse matchmaking criteria from request
    auto requestTdf = request.getPayloadAsTdf();
    
    // For now, just acknowledge matchmaking started
    // Real implementation would queue the player for matching
    
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
    
    // Acknowledge cancellation
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

} // namespace ds2::components
