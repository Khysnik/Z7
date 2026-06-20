#pragma once

#include "blaze/types.hpp"

#include <cstdint>
#include <string>
#include <unordered_map>
#include <utility>
#include <vector>

namespace gw2::data {

// Persistent player progression (the player_mpdefault stat group).
// Loaded from data/MPProfile.json at startup, served as the Stats Notif0x0032,
// and updated in place from the per-game GameReports the client submits.
class PlayerProfile {
public:
    static PlayerProfile& instance();

    bool load(const std::string& path);   // parse MPProfile.json
    bool loadAggregation(const std::string& path);  // stat name -> aggregation rule
    bool save() const;                     // write back to the loaded path
    bool loaded() const { return m_loaded; }

    // Build the Stats::Notif0x0032 payload (GRNM/KEY/LAST/STS{STAT[...]}/VID).
    blaze::TdfStruct buildStatsNotif() const;

    // Build the Stats::GetStatGroup schema response (column names), derived from
    // the same ordered stat list as the values so the two can never diverge.
    blaze::TdfStruct buildStatGroup() const;

    // Apply one per-game GameReport (statName -> per-game value) with
    // suffix-based aggregation, then persist to disk.
    void applyGameReport(const std::unordered_map<std::string, double>& report);

private:
    PlayerProfile() = default;

    std::string m_path;
    bool        m_loaded = false;

    std::string m_groupName = "player_mpdefault";
    std::string m_key       = "No_Scope_Defined";
    int64_t     m_last      = 1;
    int64_t     m_vid       = 1;

    int64_t     m_entityId      = 0;        // EID
    int64_t     m_etypComponent = 0x7802;   // ETYP component (UserSessions)
    int64_t     m_etypType      = 0x0001;   // ETYP type
    int64_t     m_periodOffset  = 0;        // POFF

    std::vector<std::pair<std::string, std::string>> m_stats;  // ordered (schema order)
    std::unordered_map<std::string, size_t>          m_index;  // name -> position

    // Per-stat aggregation rule from the persistence template:
    // Increment (add), Set (replace), High (max), Low (min).
    std::unordered_map<std::string, std::string>     m_aggregation;
};

} // namespace gw2::data
