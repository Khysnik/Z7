#pragma once

#include "blaze/component.hpp"
#include "blaze/types.hpp"
#include "network/client_connection.hpp"

#include <cstdint>
#include <map>
#include <mutex>
#include <string>

namespace gw2::components {

using network::ClientConnection;

class StatsComponent : public blaze::Component {
public:
    StatsComponent();

    std::unique_ptr<blaze::Packet> handlePacket(
        const blaze::Packet& request,
        std::shared_ptr<ClientConnection> client
    ) override;

private:
    std::unique_ptr<blaze::Packet> handleLeaderboardGroup(const blaze::Packet& request, std::shared_ptr<ClientConnection> client);
    std::unique_ptr<blaze::Packet> handleLeaderboard(const blaze::Packet& request, std::shared_ptr<ClientConnection> client);
    std::unique_ptr<blaze::Packet> handleEntityCount(const blaze::Packet& request, std::shared_ptr<ClientConnection> client);

    std::string boardName(const blaze::Packet& request, std::shared_ptr<ClientConnection> client);

    std::mutex m_boardMutex;
    std::map<uint64_t, std::string> m_lastBoard;
};

} // namespace gw2::components
