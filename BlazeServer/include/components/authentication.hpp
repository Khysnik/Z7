#pragma once

#include "blaze/component.hpp"
#include "blaze/types.hpp"
#include "network/client_connection.hpp"
#include <string>
#include <map>
#include <mutex>

namespace ds2::components {

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
    
    uint64_t generateSessionId();
    std::string generateAuthToken();

    void sendUserAuthenticatedNotification(
        std::shared_ptr<network::ClientConnection> client,
        uint64_t userId,
        const std::string& sessionKey,
        const std::string& personaName
    );

    // Session storage
    std::map<uint64_t, std::weak_ptr<network::ClientConnection>> m_sessions;
    std::mutex m_sessionMutex;
    uint64_t m_nextSessionId = 1000;

    uint32_t m_nextNotifMsgNum = 1;
};

} // namespace ds2::components
