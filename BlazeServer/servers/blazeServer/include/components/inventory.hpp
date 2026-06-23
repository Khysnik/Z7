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

    // 0x0002: client reports items picked up in-match (e.g. ground coins). Adds
    // them to the inventory and replies with the new balance so the live counter
    // updates. Separate from the end-of-match reward (game report c___ccz_g).
    std::unique_ptr<blaze::Packet> handleAddInventory(
        const blaze::Packet& request,
        std::shared_ptr<ClientConnection> client
    );

    // 0x0003: client used a consumable. Deducts the quantity and replies with the
    // remaining count (CNSU) so the HUD updates.
    std::unique_ptr<blaze::Packet> handleUseConsumable(
        const blaze::Packet& request,
        std::shared_ptr<ClientConnection> client
    );
};

} // namespace gw2::components
