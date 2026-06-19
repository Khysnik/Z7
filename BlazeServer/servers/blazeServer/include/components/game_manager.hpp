#pragma once

#include "blaze/component.hpp"
#include "blaze/types.hpp"
#include "network/client_connection.hpp"
#include <map>
#include <vector>
#include <mutex>

namespace gw2::components {

using network::ClientConnection;

struct GameSession {
    uint64_t gameId;
    uint64_t hostUserId;
    std::string gameName;
    std::string mapName;
    std::string gameMode;
    uint32_t maxPlayers;
    uint32_t currentPlayers;
    std::vector<uint64_t> players;
    
    // Network info
    uint32_t hostIp;
    uint16_t hostPort;
};

class GameManager : public blaze::Component {
public:
    GameManager();
    
    std::unique_ptr<blaze::Packet> handlePacket(
        const blaze::Packet& request,
        std::shared_ptr<ClientConnection> client
    ) override;
    
private:
    std::unique_ptr<blaze::Packet> handleCreateGame(
        const blaze::Packet& request,
        std::shared_ptr<ClientConnection> client
    );
    
    std::unique_ptr<blaze::Packet> handleDestroyGame(
        const blaze::Packet& request,
        std::shared_ptr<ClientConnection> client
    );
    
    std::unique_ptr<blaze::Packet> handleJoinGame(
        const blaze::Packet& request,
        std::shared_ptr<ClientConnection> client
    );
    
    std::unique_ptr<blaze::Packet> handleListGames(
        const blaze::Packet& request,
        std::shared_ptr<ClientConnection> client
    );
    
    std::unique_ptr<blaze::Packet> handleStartMatchmaking(
        const blaze::Packet& request,
        std::shared_ptr<ClientConnection> client
    );
    
    std::unique_ptr<blaze::Packet> handleCancelMatchmaking(
        const blaze::Packet& request,
        std::shared_ptr<ClientConnection> client
    );
    
    // Game session management
    uint64_t createGameSession(const GameSession& session);
    bool destroyGameSession(uint64_t gameId);
    GameSession* getGameSession(uint64_t gameId);
    std::vector<GameSession> listGameSessions();
    
    std::map<uint64_t, GameSession> m_games;
    std::mutex m_gamesMutex;
    uint64_t m_nextGameId = 1;
};

} // namespace gw2::components
