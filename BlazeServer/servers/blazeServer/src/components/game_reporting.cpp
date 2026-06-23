#include "components/game_reporting.hpp"
#include "blaze/tdf.hpp"
#include "config.hpp"
#include "data/player_profile.hpp"
#include "data/inventory.hpp"
#include "utils/logger.hpp"

#include <cstring>
#include <map>
#include <unordered_map>
#include <vector>

namespace gw2::components {

namespace {

uint64_t readVle(const std::vector<uint8_t>& b, size_t& i) {
    uint64_t val = 0; int shift = 6;
    uint8_t c = b[i++];
    val = c & 0x3F;
    while (c & 0x80) {
        c = b[i++];
        val |= static_cast<uint64_t>(c & 0x7F) << shift;
        shift += 7;
    }
    return val;
}

float readFloatBE(const std::vector<uint8_t>& b, size_t i) {
    uint8_t le[4] = { b[i + 3], b[i + 2], b[i + 1], b[i] };  // wire is big-endian
    float f; std::memcpy(&f, le, 4);
    return f;
}

std::unordered_map<std::string, double> parseReportStats(const std::vector<uint8_t>& p) {
    std::unordered_map<std::string, double> out;
    for (size_t m = 0; m + 3 < p.size(); ++m) {
        if (p[m] != 0x05 || p[m + 1] != 0x01 || p[m + 2] != 0x0A) continue;
        size_t i = m + 3;
        uint64_t count = readVle(p, i);
        bool ok = true;
        for (uint64_t e = 0; e < count; ++e) {
            if (i >= p.size()) { ok = false; break; }
            uint64_t klen = readVle(p, i);
            if (klen == 0 || i + klen > p.size() || i + klen + 4 > p.size()) { ok = false; break; }
            std::string key(reinterpret_cast<const char*>(&p[i]), klen - 1);  // drop null terminator
            i += klen;
            out[key] = readFloatBE(p, i);
            i += 4;
        }
        if (ok && !out.empty()) break;  // first valid player STAT map only
        out.clear();
    }
    return out;
}

}

GameReportingComponent::GameReportingComponent()
    : Component(static_cast<blaze::ComponentId>(0x001C), "GameReporting")
{
}

std::unique_ptr<blaze::Packet> GameReportingComponent::handlePacket(
    const blaze::Packet& request,
    std::shared_ptr<ClientConnection> client
) {
    uint16_t command = request.getCommand();

    if (command == 0x0002) {  // SubmitGameReport
        auto stats = parseReportStats(request.payload());
        LOG_INFO("[GameReporting] submitGameReport from {} ({} per-game stats)",
                 client->getRemoteAddress(), stats.size());
        if (!stats.empty()) {
            data::PlayerProfile::instance().applyGameReport(stats);

            // NOTE: do NOT grant coins here. will result in doubled coins
        }
        return request.createReply();  // empty reply
    }

    LOG_WARN("[GameReporting] Unhandled command: 0x{:04X}", command);
    return request.createReply();
}

} // namespace gw2::components
