#pragma once

#include "blaze/component.hpp"
#include "blaze/types.hpp"
#include "network/client_connection.hpp"

namespace ds2::components {

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

    // Pushed after postAuth to populate UserSessionExtendedData on BlazeSDK,
    // which flips hasExtendedData() to true and lets the SDK transition to
    // LOGIN_STATE_AUTHENTICATED.
    void pushUserAddedNotification(std::shared_ptr<ClientConnection> client);
    // UserSessions::UserSessionExtendedDataUpdate (notif id=1). The SDK runs
    // setExtendedData() from this path AND dispatches
    // onExtendedUserDataInfoChanged, which is the trigger LocalUser is
    // listening for to fire onLocalUserAuthenticated. UserAdded only takes
    // that path when the user isn't already subscribed by index — pushing
    // this on its own removes that conditional dependency.
    void pushUserSessionExtendedDataUpdate(std::shared_ptr<ClientConnection> client);
    uint32_t m_nextNotifMsgNum = 1;
};

} // namespace ds2::components
