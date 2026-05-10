#include "components/user_sessions.hpp"
#include "blaze/tdf.hpp"
#include "network/client_connection.hpp"
#include "utils/logger.hpp"

namespace ds2::components {

namespace {

int64_t getIntField(const blaze::TdfStruct& tdf, const std::string& tag, int64_t fallback = 0) {
    auto it = tdf.find(tag);
    if (it == tdf.end() || !it->second) {
        return fallback;
    }
    if (it->second->type != blaze::TdfType::Integer) {
        return fallback;
    }
    return std::get<blaze::TdfInteger>(it->second->value);
}

} // namespace

UserSessions::UserSessions()
    : Component(blaze::ComponentId::UserSessions, "UserSessions")
{
}

std::unique_ptr<blaze::Packet> UserSessions::handlePacket(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    uint16_t command = request.getCommand();
    
    switch (static_cast<blaze::UserSessionsCommand>(command)) {
        case blaze::UserSessionsCommand::updateHardwareFlags:
            return handleUpdateHardwareFlags(request, client);
        
        case blaze::UserSessionsCommand::updateNetworkInfo:
            return handleUpdateNetworkInfo(request, client);
        
        default:
            LOG_WARN("[UserSessions] Unknown command: 0x{:04X}", command);
            return request.createReply();
    }
}

std::unique_ptr<blaze::Packet> UserSessions::handleUpdateHardwareFlags(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    auto requestTdf = request.getPayloadAsTdf();
    int64_t hardwareFlags = getIntField(requestTdf, "HWFG", 0);

    LOG_INFO("[UserSessions] updateHardwareFlags from {} (HWFG={})",
             client->getRemoteAddress(), hardwareFlags);

    // UpdateHardwareFlagsRequest: HWFG (bitfield/int)
    // Response: empty reply.
    auto reply = request.createReply();
    return reply;
}

std::unique_ptr<blaze::Packet> UserSessions::handleUpdateNetworkInfo(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    LOG_INFO("[UserSessions] updateNetworkInfo from {}", client->getRemoteAddress());
    
    // Response is empty.
    auto reply = request.createReply();
    return reply;
}

} // namespace ds2::components
