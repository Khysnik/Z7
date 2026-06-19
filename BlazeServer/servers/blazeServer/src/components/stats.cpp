#include "components/stats.hpp"
#include "blaze/tdf.hpp"
#include "network/client_connection.hpp"
#include "utils/logger.hpp"

#include <fstream>
#include <iterator>
#include <vector>

namespace gw2::components {

static std::vector<uint8_t> loadFile(const char* path) {
    std::ifstream f(path, std::ios::binary);
    if (!f) { LOG_WARN("[Stats] file missing: {}", path); return {}; }
    return std::vector<uint8_t>((std::istreambuf_iterator<char>(f)),
                                 std::istreambuf_iterator<char>());
}

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
            // EA metadata, replayed from a bin file because of the large size
            auto reply = request.createReply();
            auto bytes = loadFile("data/stats_getstatgroup.bin");
            if (!bytes.empty()) reply->payload() = bytes;
            LOG_INFO("[Stats] getStatGroup -> {} bytes", bytes.size());
            return reply;
        }
        case 0x0010: {
            // Player progression (Notif0x0032). Currently a captured blob; TODO:
            // generate this from the bytevault PlayerProfile so it stays
            // in sync with saved data.
            client->sendPacket(*request.createReply());
            auto bytes = loadFile("data/stats_progression.bin");
            if (!bytes.empty()) {
                auto notif = std::make_unique<blaze::Packet>(
                    blaze::ComponentId::Stats, 0x0032, blaze::MessageType::Notification, 0);
                notif->payload() = bytes;
                client->sendPacket(std::move(notif));
                LOG_INFO("[Stats] getStats -> pushed Notif0x0032 ({} bytes progression)", bytes.size());
            }
            return nullptr;
        }
        default:
            LOG_WARN("[Stats] Unknown command: 0x{:04X}", command);
            return request.createReply();
    }
}

} // namespace gw2::components
