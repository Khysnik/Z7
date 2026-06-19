#pragma once

#include "blaze/component.hpp"
#include "blaze/types.hpp"
#include "network/client_connection.hpp"
#include <string>

namespace gw2::components {

using network::ClientConnection;

class Authentication : public blaze::Component {
public:
    Authentication();

    std::unique_ptr<blaze::Packet> handlePacket(
        const blaze::Packet& request,
        std::shared_ptr<ClientConnection> client
    ) override;

private:
    // Command handlers
    std::unique_ptr<blaze::Packet> handleLogin(
        const blaze::Packet& request,
        std::shared_ptr<ClientConnection> client
    );

    std::unique_ptr<blaze::Packet> handleExpressLogin(
        const blaze::Packet& request,
        std::shared_ptr<ClientConnection> client
    );

    std::unique_ptr<blaze::Packet> handleGetAuthToken(
        const blaze::Packet& request,
        std::shared_ptr<ClientConnection> client
    );

    std::unique_ptr<blaze::Packet> handleLogout(
        const blaze::Packet& request,
        std::shared_ptr<ClientConnection> client
    );

    std::unique_ptr<blaze::Packet> handleGetPersona(
        const blaze::Packet& request,
        std::shared_ptr<ClientConnection> client
    );

    std::unique_ptr<blaze::Packet> handleListPersonas(
        const blaze::Packet& request,
        std::shared_ptr<ClientConnection> client
    );

    std::unique_ptr<blaze::Packet> handleGetOriginPersona(
        const blaze::Packet& request,
        std::shared_ptr<ClientConnection> client
    );
    
    void sendUserAuthenticatedNotification(
        std::shared_ptr<network::ClientConnection> client
    );

    void sendUserAddedNotification(
        std::shared_ptr<network::ClientConnection> client
    );

    uint32_t m_nextNotifMsgNum = 1;
};

} // namespace gw2::components
