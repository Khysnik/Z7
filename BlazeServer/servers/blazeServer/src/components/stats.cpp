#include "components/stats.hpp"
#include "blaze/tdf.hpp"
#include "data/player_profile.hpp"
#include "network/client_connection.hpp"
#include "config.hpp"
#include "utils/logger.hpp"
#include "utils/json.hpp"
#include "utils/http.hpp"
#include "utils/editorial.hpp"

#include <nlohmann/json.hpp>

#include <algorithm>
#include <cstdint>
#include <cstdio>
#include <string>
#include <vector>

namespace gw2::components {

namespace {

using nlohmann::json;

std::string strField(const blaze::TdfStruct& tdf, const std::string& tag) {
    auto it = tdf.find(tag);
    if (it == tdf.end() || !it->second || it->second->type != blaze::TdfType::String) return {};
    return std::get<blaze::TdfString>(it->second->value);
}

const json& leaderboards() {
    return utils::dataSection("leaderboards");
}

const json* findBoard(const std::string& name) {
    const json& lb = leaderboards();
    if (!lb.contains("boards")) return nullptr;
    const json& boards = lb["boards"];
    auto it = boards.find(name);
    return it != boards.end() ? &(*it) : nullptr;
}

// A GR time stat of 1000000 means "no time set" (ranks last).
constexpr double kNoTime = 1000000.0;

struct Column { std::string ldsc; std::string name; };

struct Schema {
    std::vector<Column> columns;
    std::string         primary;       // SNAM
    bool                global = false;
};

Schema schemaFor(const json& board) {
    if (board.value("global", false))
        return { { {"ID_LEADERBOARD_GLOBAL_TIME_GUNRANGE", "gr_ob"},
                   {"ID_LEADERBOARD_GLOBAL_CHAR_GUNRANGE", "gr_ob_char"} },
                 "gr_ob", true };
    const std::string statKey = board.value("statKey", std::string());
    return { { { board.value("ldsc", std::string()), statKey } }, statKey, false };
}

// The player's best GR time
struct PlayerScore { double value = kNoTime; int character = 0; };

PlayerScore playerScore(const json& board) {
    auto& profile = data::PlayerProfile::instance();
    if (!board.value("global", false))
        return { profile.getStat(board.value("statKey", std::string()), kNoTime), 0 };

    PlayerScore best;
    for (const auto& [name, b] : leaderboards()["boards"].items()) {
        if (!b.contains("statKey")) continue;
        double v = profile.getStat(b.value("statKey", std::string()), kNoTime);
        if (v < best.value) best = { v, b.value("charId", 0) };
    }
    return best;
}

std::string decimal(double v) {
    char buf[32];
    std::snprintf(buf, sizeof(buf), "%.3f", v);
    return buf;
}

// Submit the player's times to editorial
void submitLeaderboardScores() {
    const json& lb = leaderboards();
    if (!lb.contains("boards")) return;
    json scores = json::object();
    for (const auto& [name, board] : lb["boards"].items()) {
        const PlayerScore sc = playerScore(board);
        if (sc.value >= kNoTime || sc.value <= 0.0) continue;   // unset/invalid time -> don't submit
        const int character = board.value("global", false) ? sc.character : board.value("charId", 0);
        scores[name] = { {"value", sc.value}, {"character", character} };
    }
    if (scores.empty()) {
        LOG_INFO("[Stats] leaderboard submit skipped: no GR times set (all boards >= kNoTime)");
        return;
    }
    LOG_INFO("[Stats] leaderboard submit: {} board score(s) -> {}", scores.size(), utils::kEditorialBase);
    json payload = { {"user", config::blazeId}, {"name", config::persona}, {"scores", scores} };
    try {
        utils::httpPost(utils::kEditorialBase + std::string("/gw2/live/leaderboard/submit"), payload);
    } catch (const std::exception& e) {
        LOG_WARN("[Stats] leaderboard submit failed: {}", e.what());
    }
}

// Ranked entries for a board from editorial
json fetchLeaderboardEntries(const std::string& name, const json* board) {
    try {
        json r = utils::httpGet(utils::kEditorialBase + std::string("/gw2/live/leaderboard?board=") + name);
        return r.value("entries", json::array());
    } catch (const std::exception& e) {
        LOG_WARN("[Stats] leaderboard fetch failed ({}); showing local only", e.what());
        json entries = json::array();
        if (board) {
            const PlayerScore sc = playerScore(*board);
            if (sc.value < kNoTime) {
                const int character = board->value("global", false) ? sc.character : board->value("charId", 0);
                entries.push_back({ {"rank", 1}, {"user", config::blazeId}, {"name", config::persona},
                                    {"value", sc.value}, {"character", character} });
            }
        }
        return entries;
    }
}

blaze::TdfStruct buildGroup(const std::string& name, const json* board, int64_t size) {
    const Schema schema = board ? schemaFor(*board) : Schema{};
    blaze::TdfBuilder b;
    b.integer("ASCD", 1)                            // ascending (lower time better)
     .string ("BNAM", name)
     .string ("DESC", name)
     .objectType("ETYP", 0xF002, 0x0001)           // leaderboard entity type
     .integer("LBSZ", size)                         // ranked entry count
     .beginList("LIST");                            // stat columns
    for (const auto& col : schema.columns)
        b.beginStruct().string("LDSC", col.ldsc).string("NAME", col.name).endStruct();
    b.endList()
     .string ("META", "")
     .string ("NAME", name)
     .string ("SNAM", schema.primary);              // primary stat
    return b.build();
}

blaze::TdfStruct buildValues(const json* board, const json& entries) {
    blaze::TdfBuilder b;
    b.beginList("LDLS");                            // rows
    if (board) {
        const Schema schema = schemaFor(*board);
        for (const auto& e : entries) {
            const double value = e.value("value", kNoTime);
            const std::string timeStr = decimal(value);

            std::vector<std::string> stat = { timeStr };
            if (schema.global) stat.push_back(decimal(e.value("character", 0)));

            int64_t uid = 0;                        // editorial stores user id as a string key
            if (e.contains("user")) {
                if (e["user"].is_string()) { try { uid = std::stoll(e["user"].get<std::string>()); } catch (...) {} }
                else uid = e["user"].get<int64_t>();
            }
            const std::string uname = e.value("name", std::string("Player"));
            const int64_t rank = e.value("rank", (int64_t)1);

            b.beginStruct()
                .integer("RANK", rank - 1)                     // 0-based position (client shows list order)
                .string ("RSTA", timeStr)                      // value as decimal string (client formats)
                .integer("RWFG", 0)                            // isRawStats = false
                .unionValue("RWST", 0x7F)                      // empty union (no raw stats)
                .list   ("STAT", blaze::TdfType::String, stat)
                .integer("UATT", 0)
                .beginStruct("USER")                           // CoreIdentification
                    .binary ("EXBB", {})
                    .integer("EXID", uid == config::blazeId ? config::nucleusId : uid)
                    .integer("ID",   uid)
                    .string ("NAME", uname)
                    .string ("NASP", config::nasp)
                .endStruct()
            .endStruct();
        }
    }
    b.endList();
    return b.build();
}

} // namespace

StatsComponent::StatsComponent()
    : Component(blaze::ComponentId::Stats, "Stats")
{
}

std::string StatsComponent::boardName(const blaze::Packet& request, std::shared_ptr<ClientConnection> client) {
    std::string name = strField(request.getPayloadAsTdf(), "NAME");
    const uint64_t cid = client->getId();
    std::lock_guard<std::mutex> lock(m_boardMutex);
    if (!name.empty()) { m_lastBoard[cid] = name; return name; }
    auto it = m_lastBoard.find(cid);
    return it != m_lastBoard.end() ? it->second : std::string();
}

std::unique_ptr<blaze::Packet> StatsComponent::handleLeaderboardGroup(const blaze::Packet& request, std::shared_ptr<ClientConnection> client) {
    std::string name = boardName(request, client);
    const json* board = findBoard(name);
    // Opening a board: register this player's times, then size the board.
    if (board) submitLeaderboardScores();
    const json entries = board ? fetchLeaderboardEntries(name, board) : json::array();
    LOG_INFO("[Stats] getLeaderboardGroup '{}' ({} entries)", name, entries.size());
    auto reply = request.createReply();
    reply->setPayload(buildGroup(name, board, static_cast<int64_t>(entries.size())));
    return reply;
}

std::unique_ptr<blaze::Packet> StatsComponent::handleLeaderboard(const blaze::Packet& request, std::shared_ptr<ClientConnection> client) {
    std::string name = boardName(request, client);
    const json* board = findBoard(name);
    // Also register the player's times here: the client may fetch a board via
    // getLeaderboard/getCentered/getFiltered without ever calling getLeaderboardGroup.
    if (board) submitLeaderboardScores();
    const json entries = board ? fetchLeaderboardEntries(name, board) : json::array();
    LOG_INFO("[Stats] getLeaderboard '{}' ({} entries)", name, entries.size());
    auto reply = request.createReply();
    reply->setPayload(buildValues(board, entries));
    return reply;
}

std::unique_ptr<blaze::Packet> StatsComponent::handleEntityCount(const blaze::Packet& request, std::shared_ptr<ClientConnection> client) {
    std::string name = boardName(request, client);
    const json* board = findBoard(name);
    const json entries = board ? fetchLeaderboardEntries(name, board) : json::array();
    auto reply = request.createReply();
    reply->setPayload(blaze::TdfBuilder().integer("CNT", static_cast<int64_t>(entries.size())).build());
    return reply;
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
        case 0x000A:   // getLeaderboardGroup
            return handleLeaderboardGroup(request, client);
        case 0x000C:   // getLeaderboard
        case 0x000D:   // getCenteredLeaderboard
        case 0x000E:   // getFilteredLeaderboard
            return handleLeaderboard(request, client);
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
        case 0x0012:   // getLeaderboardEntityCount
            return handleEntityCount(request, client);
        default:
            LOG_WARN("[Stats] Unknown command: 0x{:04X}", command);
            return request.createReply();
    }
}

} // namespace gw2::components
