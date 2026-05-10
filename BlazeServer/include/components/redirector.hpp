#pragma once

#include "blaze/component.hpp"
#include "blaze/types.hpp"
#include "network/client_connection.hpp"
#include <string>

namespace ds2::components {

using network::ClientConnection;

class Redirector : public blaze::Component {
public:
    Redirector();
    
    // Set the Blaze server address to redirect clients to
    void setBlazeServerAddress(const std::string& host, uint16_t port);
    
    std::unique_ptr<blaze::Packet> handlePacket(
        const blaze::Packet& request,
        std::shared_ptr<ClientConnection> client
    ) override;
    
private:
    std::unique_ptr<blaze::Packet> handleGetServerInstance(
        const blaze::Packet& request,
        std::shared_ptr<ClientConnection> client
    );
    
    std::string m_blazeHost;
    uint16_t m_blazePort;
};

} // namespace ds2::components
