#pragma once

#include "blaze/component.hpp"
#include "blaze/types.hpp"
#include "network/client_connection.hpp"

namespace ds2::components {

using network::ClientConnection;

class UserSessions : public blaze::Component {
public:
    UserSessions();
    
    std::unique_ptr<blaze::Packet> handlePacket(
        const blaze::Packet& request,
        std::shared_ptr<ClientConnection> client
    ) override;
    
private:
    std::unique_ptr<blaze::Packet> handleUpdateHardwareFlags(
        const blaze::Packet& request,
        std::shared_ptr<ClientConnection> client
    );
    
    std::unique_ptr<blaze::Packet> handleUpdateNetworkInfo(
        const blaze::Packet& request,
        std::shared_ptr<ClientConnection> client
    );
};

} // namespace ds2::components
