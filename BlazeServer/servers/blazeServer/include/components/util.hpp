#pragma once

#include "blaze/component.hpp"
#include "blaze/types.hpp"
#include "network/client_connection.hpp"

namespace gw2::components {

using network::ClientConnection;

class Util : public blaze::Component {
public:
    Util();
    
    std::unique_ptr<blaze::Packet> handlePacket(
        const blaze::Packet& request,
        std::shared_ptr<ClientConnection> client
    ) override;
    
private:
    std::unique_ptr<blaze::Packet> handlePing(
        const blaze::Packet& request,
        std::shared_ptr<ClientConnection> client
    );
    
    std::unique_ptr<blaze::Packet> handlePreAuth(
        const blaze::Packet& request,
        std::shared_ptr<ClientConnection> client
    );
    
    std::unique_ptr<blaze::Packet> handlePostAuth(
        const blaze::Packet& request,
        std::shared_ptr<ClientConnection> client
    );
    
    std::unique_ptr<blaze::Packet> handleFetchClientConfig(
        const blaze::Packet& request,
        std::shared_ptr<ClientConnection> client
    );

    std::unique_ptr<blaze::Packet> handleGetTelemetryServer(
        const blaze::Packet& request,
        std::shared_ptr<ClientConnection> client
    );

    std::unique_ptr<blaze::Packet> handleSetClientState(
        const blaze::Packet& request,
        std::shared_ptr<ClientConnection> client
    );

    void pushUserAddedNotification(std::shared_ptr<ClientConnection> client);

    void pushUserSessionExtendedDataUpdate(std::shared_ptr<ClientConnection> client);
    uint32_t m_nextNotifMsgNum = 1;
};

} // namespace gw2::components
