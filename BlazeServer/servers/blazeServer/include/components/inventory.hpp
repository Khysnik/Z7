#pragma once

#include "blaze/component.hpp"
#include "blaze/types.hpp"
#include "network/client_connection.hpp"

namespace gw2::components {

using network::ClientConnection;

class InventoryComponent : public blaze::Component {
public:
    InventoryComponent();

    std::unique_ptr<blaze::Packet> handlePacket(
        const blaze::Packet& request,
        std::shared_ptr<ClientConnection> client
    ) override;

private:
    std::unique_ptr<blaze::Packet> handleGetInventory(
        const blaze::Packet& request,
        std::shared_ptr<ClientConnection> client
    );
};

} // namespace gw2::components
