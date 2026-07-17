#include "components/game_reporting.hpp"
#include "components/user_sessions.hpp"
#include "blaze/tdf.hpp"
#include "config.hpp"
#include "data/player_profile.hpp"
#include "utils/logger.hpp"
#include "utils/http.hpp"
#include "utils/editorial.hpp"
#include <nlohmann/json.hpp>
#include <map>
#include <regex>
#include <unordered_map>
#include <vector>

namespace gw2::components {

namespace {

uint64_t readValue(const std::vector<uint8_t>& bytes, size_t& offset) {
    uint64_t value = 0;
    int shift = 6;
    uint8_t byte = bytes[offset++];
    value = byte & 0x3F;
    while (byte & 0x80) {
        byte = bytes[offset++];
        value |= static_cast<uint64_t>(byte & 0x7F) << shift;
        shift += 7;
    }
    return value;
}

float readFloatBE(const std::vector<uint8_t>& bytes, const size_t offset) {
    const uint8_t littleEndian[4] = {
        bytes[offset + 3],
        bytes[offset + 2],
        bytes[offset + 1],
        bytes[offset]
    };  // wire is big-endian
    float result; std::memcpy(&result, littleEndian, 4);
    return result;
}

std::unordered_map<std::string, double> parseReportStats(const std::vector<uint8_t>& payload) {
    std::unordered_map<std::string, double> stats;
    for (size_t i = 0; i + 3 < payload.size(); ++i) {
        if (payload[i] != 0x05 || payload[i + 1] != 0x01 || payload[i + 2] != 0x0A) continue;
        size_t offset = i + 3;
        const uint64_t count = readValue(payload, offset);
        bool valid = true;

        for (uint64_t entryIndex = 0; entryIndex < count; ++entryIndex) {
            if (offset >= payload.size()) {
                valid = false;
                break;
            }
            uint64_t keyLen = readValue(payload, offset);
            if (keyLen == 0 || offset + keyLen > payload.size() || offset + keyLen + 4 > payload.size()) {
                valid = false;
                break;
            }
            std::string key(reinterpret_cast<const char*>(&payload[offset]), keyLen - 1);  // drop null terminator
            offset += keyLen;
            stats[key] = readFloatBE(payload, offset);
            offset += 4;
        }

        if (valid && !stats.empty()) break;  // first valid player STAT map only
        stats.clear();
    }
    return stats;
}

// Forward the vanquish deltas to editorial
void postChallengeContribution(const std::unordered_map<std::string, double>& stats) {
    static const std::regex reElement(R"(^c_Any(.+?)Character___kscx_g$)"); // ex. c_AnyFireCharacter___kscx_g
    static const std::regex reVariant(R"(^c_([A-Za-z0-9]+)__kscx_g$)");     // ex. c_CiW23__kscx_g
    static const std::regex reChar   (R"(^c_([A-Za-z0-9]+)__ks_g$)");       // ex. c_imp__ks_g

    nlohmann::json elements   = nlohmann::json::object();
    nlohmann::json characters = nlohmann::json::object();
    nlohmann::json variants   = nlohmann::json::object();
    std::smatch match;

    for (const auto& [key, value] : stats) {
        if (value <= 0) continue;
        if (std::regex_match(key, match, reElement))
            elements[match[1].str()]   = elements.value(match[1].str(), 0.0) + value;
        else if (std::regex_match(key, match, reVariant))
            variants[match[1].str()]   = variants.value(match[1].str(), 0.0) + value;
        else if (std::regex_match(key, match, reChar))
            characters[match[1].str()] = characters.value(match[1].str(), 0.0) + value;
    }

    if (elements.empty() && characters.empty() && variants.empty()) return;

    const nlohmann::json payload = {
        {"user", config::blazeId},
        {"elements", elements},
        {"characters", characters},
        {"variants", variants},
    };

    try {
        const auto response = utils::httpPost(utils::kEditorialBase + std::string("/gw2/live/challenge/contribute"), payload);
        LOG_INFO("[GameReporting] challenge contribute -> matched {}, community {}", response.value("matched", 0.0), response.value("community", 0.0));
    } catch (const std::exception& ex) {
        LOG_WARN("[GameReporting] challenge contribute failed: {}", ex.what());
    }
}

}

GameReportingComponent::GameReportingComponent() : Component(static_cast<blaze::ComponentId>(0x001C), "GameReporting")
{
}

std::unique_ptr<blaze::Packet> GameReportingComponent::handlePacket(const blaze::Packet& request, std::shared_ptr<ClientConnection> client) {
    uint16_t command = request.getCommand();

    switch (static_cast<blaze::GameReportingCommand>(command)) {
        case blaze::GameReportingCommand::submitGameReport:
            return handleSubmitGameReport(request, client);

        case blaze::GameReportingCommand::startGameSession:
            return handleStartGameSession(request, client);

        default:
            LOG_WARN("[GameReporting] Unhandled command: 0x{:04X}", command);
            return request.createReply();
    }
}

// Blaze::GameReporting::SubmitGameReport (id=0x02): Receives updated values for mpprofile.
std::unique_ptr<blaze::Packet> GameReportingComponent::handleSubmitGameReport(const blaze::Packet& request, std::shared_ptr<ClientConnection> client) {
    auto stats = parseReportStats(request.payload());
    LOG_INFO("[GameReporting] submitGameReport from {} ({} per-game stats)",
             client->getRemoteAddress(), stats.size());

    for (const auto& [key, value] : stats)
        if (key.find("gr_char") != std::string::npos && value < 3.0e38)
            LOG_INFO("[GameReporting]   GR best time: {} = {:.3f}", key, value);
    if (!stats.empty()) {
        data::PlayerProfile::instance().applyGameReport(stats);
        postChallengeContribution(stats);   // forward stat deltas to editorial
    }

    client->sendPacket(*request.createReply());
    UserSessions::pushUserSessionExtendedDataUpdate(client);

    static uint32_t s_reportId = 0xD1D6A015;
    const int64_t reportId = ++s_reportId;

    auto resultPayload = blaze::TdfBuilder()
        .variable("DATA", nullptr)   // resultList — empty report-result list
        .integer("EROR", 0)          // errorCode — Error in processing Game Reports or Error_OK (0) if successful.
        .integer("FNL",  1)          // isFinalResult — True if this is the final result notification for this game.
        .integer("GHID", reportId)   // gameHistoryId — The unique value assigned for game history query.
        .integer("GRID", reportId)   // gameReportingId — The unique value assigned at the beginning of the game.
        .build();

    auto notif = std::make_unique<blaze::Packet>(static_cast<blaze::ComponentId>(0x001C), 0x0072, blaze::MessageType::Notification, 0);
    notif->setPayload(resultPayload);
    client->sendPacket(std::move(notif));
    return nullptr;
}

// Blaze::GameReporting::StartGameSession (id=0x64): The frostbite_multiplayer 'session started' report
std::unique_ptr<blaze::Packet> GameReportingComponent::handleStartGameSession(const blaze::Packet& request, std::shared_ptr<ClientConnection> client) {
    int64_t reportId = 0;
    auto requestTdf = request.getPayloadAsTdf();
    auto reportIter = requestTdf.find("RPRT");

    if (reportIter != requestTdf.end() && reportIter->second && reportIter->second->type == blaze::TdfType::Struct) {
        auto& report = std::get<blaze::TdfStruct>(reportIter->second->value);
        auto gridIter = report.find("GRID");
        if (gridIter != report.end() && gridIter->second &&
            gridIter->second->type == blaze::TdfType::Integer)
            reportId = std::get<blaze::TdfInteger>(gridIter->second->value);
    }

    LOG_INFO("[GameReporting] startGameSession from {} (GRID={:#x}) -> ack + Notif0x0072", client->getRemoteAddress(), static_cast<uint64_t>(reportId));

    client->sendPacket(*request.createReply());

    // NotifyGameReportResult (0x0072) = ResultNotification.
    auto resultPayload = blaze::TdfBuilder()
        .variable("DATA", nullptr)   // resultList — empty report-result list
        .integer("EROR", 0)          // errorCode — Error_OK (0) if successful.
        .integer("FNL",  0)          // isFinalResult — session started (NOT finalized)
        .integer("GHID", reportId)   // gameHistoryId — echo the client's report id
        .integer("GRID", reportId)   // gameReportingId — assigned at the beginning of the game
        .build();

    auto notif = std::make_unique<blaze::Packet>(static_cast<blaze::ComponentId>(0x001C), 0x0072, blaze::MessageType::Notification, 0);
    notif->setPayload(resultPayload);
    client->sendPacket(std::move(notif));
    return nullptr;
}

} // namespace gw2::components
