#include "components/stats.hpp"
#include "blaze/tdf.hpp"
#include "data/player_profile.hpp"
#include "network/client_connection.hpp"
#include "utils/logger.hpp"

namespace gw2::components {

StatsComponent::StatsComponent()
    : Component(blaze::ComponentId::Stats, "Stats")
{
}

std::unique_ptr<blaze::Packet> StatsComponent::handlePacket(
    const blaze::Packet& request,
    std::shared_ptr<ClientConnection> client
) {
    uint16_t command = request.getCommand();

    switch (command) {
        case 0x0004: {
            auto reply = request.createReply();
            reply->setPayload(data::PlayerProfile::instance().buildStatGroup());
            LOG_INFO("[Stats] getStatGroup -> built from MPProfile schema");
            return reply;
        }
        case 0x0010: {
            // Player progression, built from data/MPProfile.json
            client->sendPacket(*request.createReply());
            auto notif = std::make_unique<blaze::Packet>(
                blaze::ComponentId::Stats, 0x0032, blaze::MessageType::Notification, 0);

            auto& profile = data::PlayerProfile::instance();
            notif->setPayload(profile.buildStatsNotif());
            LOG_INFO("[Stats] getStats -> pushed Notif0x0032 from MPProfile.json");
            client->sendPacket(std::move(notif));
            return nullptr;
        }
        default:
            LOG_WARN("[Stats] Unknown command: 0x{:04X}", command);
            return request.createReply();
    }
}

} // namespace gw2::components
