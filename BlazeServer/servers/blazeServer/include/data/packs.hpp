#pragma once
#include <string>
#include <vector>
#include <cstdint>

namespace gw2::data {

// A pack template (a DESL entry in Packs::GetTemplate); the catalog of all
// card packs. Loaded from data/packs.json.
struct PackTemplate {
    std::string pkey, cons, titl, desc, addt, gkey, imgn;
    int64_t     audl = 0, pric = 0, stid = 0, strk = 0;
    std::vector<std::string> type;
};

const std::vector<PackTemplate>& getPackTemplates();

} // namespace gw2::data
