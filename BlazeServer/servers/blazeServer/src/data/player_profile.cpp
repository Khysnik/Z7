#include "data/player_profile.hpp"
#include "blaze/tdf.hpp"
#include "utils/logger.hpp"
#include "utils/json.hpp"

#include <nlohmann/json.hpp>

#include <algorithm>
#include <cmath>
#include <cstdlib>
#include <cstring>
#include <fstream>
#include <sstream>

namespace gw2::data {

namespace {

using nlohmann::ordered_json;

bool endsWith(const std::string& s, const char* suffix) {
    size_t n = std::strlen(suffix);
    return s.size() >= n && s.compare(s.size() - n, n, suffix) == 0;
}

// Format a stat value as either an integer or a float with 3 decimal places
std::string formatValue(double v, bool asFloat) {
    if (asFloat) {
        std::ostringstream os;
        os.setf(std::ios::fixed);
        os.precision(3);
        os << v;
        return os.str();
    }
    return std::to_string(static_cast<long long>(std::llround(v)));
}

} // namespace

PlayerProfile& PlayerProfile::instance() {
    static PlayerProfile inst;
    return inst;
}

// Load the player profile from a json file
bool PlayerProfile::load(const std::string& path) {
    ordered_json j = utils::loadFile<ordered_json>(path);
    if (j.empty()) return false;   // missing / empty / parse error (already logged)

    m_path = path;
    m_stats.clear();
    m_index.clear();

    // Load basic profile info and stats
    m_groupName = j.value("groupName", m_groupName);
    m_key       = j.value("key", m_key);
    m_last      = j.value("last", m_last);
    m_vid       = j.value("vid", m_vid);

    if (j.contains("entity")) {
        const auto& e = j["entity"];
        m_entityId     = e.value("id", m_entityId);
        m_periodOffset = e.value("periodOffset", m_periodOffset);
        if (e.contains("type")) {
            m_etypComponent = e["type"].value("component", m_etypComponent);
            m_etypType      = e["type"].value("type", m_etypType);
        }
    }
    if (j.contains("stats"))
        for (const auto& [name, val] : j["stats"].items()) {
            m_index[name] = m_stats.size();
            m_stats.emplace_back(name, val.get<std::string>());
        }

    m_loaded = true;

    LOG_INFO("[PlayerProfile] loaded {} stats from {} (group={}, eid={})", m_stats.size(), path, m_groupName, m_entityId);
    return true;
}

// Load stat aggregation rules from json, setting the save mode for each stat (e.g. "Set", "Increment", "Low", "High")
bool PlayerProfile::loadAggregation(const nlohmann::json& rules) {
    if (rules.empty()) return false;

    m_aggregation.clear();
    for (const auto& [k, v] : rules.items()) m_aggregation[k] = v.get<std::string>();
    LOG_INFO("[PlayerProfile] loaded {} stat aggregation rules", m_aggregation.size());
    return true;
}

// Save the player profile to its json file
bool PlayerProfile::save() const {
    if (m_path.empty()) return false;
    ordered_json j;
    j["groupName"] = m_groupName;
    j["key"]       = m_key;
    j["last"]      = m_last;
    j["vid"]       = m_vid;
    ordered_json ent;
    ent["id"]   = m_entityId;
    ent["type"] = ordered_json{{"component", m_etypComponent}, {"type", m_etypType}};
    ent["periodOffset"] = m_periodOffset;
    j["entity"] = ent;
    ordered_json stats = ordered_json::object();
    for (const auto& [n, v] : m_stats) stats[n] = v;
    j["stats"] = stats;

    return utils::saveFile(m_path, j);
}

blaze::TdfStruct PlayerProfile::buildStatsNotif() const {
    std::vector<std::string> values;
    values.reserve(m_stats.size());
    for (const auto& [name, val] : m_stats) values.push_back(val);

    return blaze::TdfBuilder()
        .string("GRNM", m_groupName)
        .string("KEY", m_key)
        .integer("LAST", m_last)
        .beginStruct("STS")
            .beginList("STAT")
                .beginStruct()
                    .integer("EID", m_entityId)
                    .objectType("ETYP", static_cast<uint64_t>(m_etypComponent), static_cast<uint64_t>(m_etypType))
                    .integer("POFF", m_periodOffset)
                    .list("STAT", blaze::TdfType::String, values)
                .endStruct()
            .endList()
        .endStruct()
        .integer("VID", m_vid)
        .build();
}

blaze::TdfStruct PlayerProfile::buildStatGroup() const {
    blaze::TdfBuilder b;
    b.string("CNAM", "player_awards")
     .string("DESC", m_groupName)
     .objectType("ETYP", static_cast<uint64_t>(m_etypComponent), static_cast<uint64_t>(m_etypType))
     .string("META", "")
     .string("NAME", m_groupName)
     .beginList("STAT");
    for (const auto& [name, val] : m_stats)
        b.beginStruct().string("LDSC", "").string("NAME", name).endStruct();
    return b.endList().build();
}

void PlayerProfile::applyGameReport(const std::unordered_map<std::string, double>& report) {
    if (!m_loaded) return;
    constexpr double kSentinel = 3.0e38;

    int changed = 0;
    for (const auto& [name, value] : report) {
        auto it = m_index.find(name);
        if (it == m_index.end()) continue;
        std::string& cur = m_stats[it->second].second;
        const bool asFloat = cur.find('.') != std::string::npos;
        const double old = std::strtod(cur.c_str(), nullptr);

        std::string rule;
        auto ar = m_aggregation.find(name);
        if (ar != m_aggregation.end()) rule = ar->second;
        else if (endsWith(name, "_glva")) rule = "Low";
        else if (endsWith(name, "_ghva")) rule = "High";
        else                              rule = "Increment";

        double nv;
        if (rule == "Set") nv = value;
        else if (rule == "Low")  {
            if (value >= kSentinel) continue;
            // A "low value" (best time) stat sitting at <= 0 is unset, not a real
            // record — take the new value instead of min()'ing against an unbeatable 0.
            nv = (old <= 0.0) ? value : std::min(old, value);
        }
        else if (rule == "High") { 
            if (value <= -kSentinel) continue; 
            nv = std::max(old, value); 
        }
        else nv = old + value;
        cur = formatValue(nv, asFloat);
        ++changed;
    }
    LOG_INFO("[PlayerProfile] applied game report: {} stats updated", changed);
    save();
}

double PlayerProfile::getStat(const std::string& name, double fallback) const {
    auto it = m_index.find(name);
    if (it == m_index.end()) return fallback;
    return std::strtod(m_stats[it->second].second.c_str(), nullptr);
}

} // namespace gw2::data
