#pragma once

#include "blaze/types.hpp"

#include <nlohmann/json.hpp>

#include <cstdint>
#include <string>
#include <unordered_map>
#include <utility>
#include <vector>

namespace gw2::data {

// Persistent player progression
class PlayerProfile {
public:
    static PlayerProfile& instance();

    bool load(const std::string& path);   // parse MPProfile.json
    bool loadAggregation(const nlohmann::json& rules);  // stat name -> aggregation rule
    bool save() const;                     // write back to the loaded path
    bool loaded() const { return m_loaded; }

    blaze::TdfStruct buildStatsNotif() const;

    blaze::TdfStruct buildStatGroup() const;

    void applyGameReport(const std::unordered_map<std::string, double>& report);

    double getStat(const std::string& name, double fallback = 0.0) const;

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

    // Per-stat aggregation rule from the persistence template: Increment (add), Set (replace), High (max), Low (min).
    std::unordered_map<std::string, std::string>     m_aggregation;
};

} // namespace gw2::data
