#pragma once

#include "blaze/component.hpp"
#include "blaze/types.hpp"
#include "network/client_connection.hpp"

namespace ds2::components {

using network::ClientConnection;

class PacksComponent : public blaze::Component {
public:
    PacksComponent();

    std::unique_ptr<blaze::Packet> handlePacket(
        const blaze::Packet& request,
        std::shared_ptr<ClientConnection> client
    ) override;

private:
    std::unique_ptr<blaze::Packet> handleGetPacks(
        const blaze::Packet& request,
        std::shared_ptr<ClientConnection> client
    );

    std::unique_ptr<blaze::Packet> handleGrantPacks(
        const blaze::Packet& request,
        std::shared_ptr<ClientConnection> client
    );

    std::unique_ptr<blaze::Packet> handleRedeemPack(
        const blaze::Packet& request,
        std::shared_ptr<ClientConnection> client
    );

    std::unique_ptr<blaze::Packet> handleOpenPack(
        const blaze::Packet& request,
        std::shared_ptr<ClientConnection> client
    );

    std::unique_ptr<blaze::Packet> handleAcquireCalendarPacks(
        const blaze::Packet& request,
        std::shared_ptr<ClientConnection> client
    );

    std::unique_ptr<blaze::Packet> handleGetTemplate(
        const blaze::Packet& request,
        std::shared_ptr<ClientConnection> client
    );

    std::unique_ptr<blaze::Packet> handleWipe(
        const blaze::Packet& request,
        std::shared_ptr<ClientConnection> client
    );

    std::unique_ptr<blaze::Packet> handlePurchaseAndOpenPack(
        const blaze::Packet& request,
        std::shared_ptr<ClientConnection> client
    );

    std::unique_ptr<blaze::Packet> handleDebugGrant(
        const blaze::Packet& request,
        std::shared_ptr<ClientConnection> client
    );

    std::unique_ptr<blaze::Packet> handleGrantAndOpenPacks(
        const blaze::Packet& request,
        std::shared_ptr<ClientConnection> client
    );
};

} // namespace ds2::components
