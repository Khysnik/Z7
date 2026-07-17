#pragma once

#include "blaze/component.hpp"
#include "blaze/types.hpp"
#include "network/client_connection.hpp"

namespace gw2::components {

using network::ClientConnection;

class UserSessions : public blaze::Component {
public:
    UserSessions();
    
    std::unique_ptr<blaze::Packet> handlePacket(
        const blaze::Packet& request,
        std::shared_ptr<ClientConnection> client
    ) override;

    static void pushUserSessionExtendedDataUpdate(std::shared_ptr<ClientConnection> client);

    static void setDedicatedGame(uint64_t gameId);

private:
    static uint64_t s_dedicatedGameId;
    std::unique_ptr<blaze::Packet> handleUpdateHardwareFlags(
        const blaze::Packet& request,
        std::shared_ptr<ClientConnection> client
    );

    std::unique_ptr<blaze::Packet> handleUpdateNetworkInfo(
        const blaze::Packet& request,
        std::shared_ptr<ClientConnection> client
    );

    uint32_t m_nextNotifMsgNum = 1;
};

} // namespace gw2::components
