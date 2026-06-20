#pragma once

#include "blaze/component.hpp"
#include "blaze/types.hpp"
#include "network/client_connection.hpp"

namespace gw2::components {

using network::ClientConnection;

// GameReporting (component 0x001C). Handles SubmitGameReport (0x0002): the client
// uploads per-game stats at the end of a game/mission; we apply them to the
// persistent PlayerProfile (data/MPProfile.json).
class GameReportingComponent : public blaze::Component {
public:
    GameReportingComponent();

    std::unique_ptr<blaze::Packet> handlePacket(
        const blaze::Packet& request,
        std::shared_ptr<ClientConnection> client
    ) override;
};

} // namespace gw2::components
