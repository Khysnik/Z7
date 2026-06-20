#include "data/packs.hpp"
#include "utils/logger.hpp"

#include <nlohmann/json.hpp>

#include <fstream>

namespace gw2::data {

namespace {

const std::vector<PackTemplate>& load() {
    static std::vector<PackTemplate> packs;
    static bool loaded = false;
    if (loaded) return packs;
    loaded = true;

    std::ifstream f("data/packs.json", std::ios::binary);
    if (!f) { LOG_WARN("[Packs] data/packs.json missing; pack catalog empty"); return packs; }
    nlohmann::json j;
    try { j = nlohmann::json::parse(f); }
    catch (const std::exception& e) { LOG_ERROR("[Packs] data/packs.json parse error: {}", e.what()); return packs; }

    for (const auto& e : j.value("packs", nlohmann::json::array())) {
        PackTemplate p;
        p.pkey = e.value("PKEY", "");
        p.cons = e.value("CONS", "");
        p.titl = e.value("TITL", "");
        p.desc = e.value("DESC", "");
        p.addt = e.value("ADDT", "");
        p.gkey = e.value("GKEY", "");
        p.imgn = e.value("IMGN", "");
        p.audl = e.value("AUDL", (int64_t)0);
        p.pric = e.value("PRIC", (int64_t)0);
        p.stid = e.value("STID", (int64_t)0);
        p.strk = e.value("STRK", (int64_t)0);
        for (const auto& t : e.value("TYPE", nlohmann::json::array())) p.type.push_back(t.get<std::string>());
        packs.push_back(std::move(p));
    }
    LOG_INFO("[Packs] loaded {} pack templates from data/packs.json", packs.size());
    return packs;
}

} // namespace

const std::vector<PackTemplate>& getPackTemplates() { return load(); }

} // namespace gw2::data
