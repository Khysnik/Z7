#include "data/inventory.hpp"
#include "utils/logger.hpp"

#include <nlohmann/json.hpp>

#include <algorithm>
#include <fstream>
#include <unordered_map>
#include <unordered_set>

namespace gw2::data {

namespace {

using nlohmann::ordered_json;

struct Store {
    std::vector<InventoryItem>               items;
    std::unordered_map<std::string, size_t>  index;     // ckey -> items position
    std::vector<std::string>                 unlocks;
    std::unordered_set<std::string>          unlockSet;
    std::string path;
    bool loaded = false;
};

Store& store() { static Store st; return st; }

void ensureLoaded() { if (!store().loaded) loadInventory("data/inventory.json"); }

} // namespace

void loadInventory(const std::string& path) {
    Store& st = store();
    st.loaded = true;
    st.path = path;
    st.items.clear(); st.index.clear(); st.unlocks.clear(); st.unlockSet.clear();

    std::ifstream f(path, std::ios::binary);
    if (!f) { LOG_WARN("[Inventory] {} missing; inventory empty", path); return; }
    nlohmann::json j;
    try { j = nlohmann::json::parse(f); }
    catch (const std::exception& e) { LOG_ERROR("[Inventory] {} parse error: {}", path, e.what()); return; }

    for (const auto& it : j.value("items", nlohmann::json::array())) {
        InventoryItem item{it.value("ckey", ""), it.value("qant", (int64_t)0)};
        if (item.ckey.empty()) continue;
        st.index[item.ckey] = st.items.size();
        st.items.push_back(std::move(item));
    }
    for (const auto& u : j.value("unlocks", nlohmann::json::array())) {
        std::string k = u.get<std::string>();
        if (st.unlockSet.insert(k).second) st.unlocks.push_back(std::move(k));
    }
    LOG_INFO("[Inventory] loaded {} items, {} unlocks from {}", st.items.size(), st.unlocks.size(), path);
}

bool saveInventory() {
    Store& st = store();
    if (st.path.empty()) return false;
    ordered_json j;
    ordered_json items = ordered_json::array();
    for (const auto& it : st.items) items.push_back(ordered_json{{"ckey", it.ckey}, {"qant", it.qant}});
    j["items"]   = items;
    j["unlocks"] = st.unlocks;

    std::ofstream f(st.path, std::ios::binary | std::ios::trunc);
    if (!f) { LOG_ERROR("[Inventory] cannot write {}", st.path); return false; }
    f << j.dump(2);
    return true;
}

const std::vector<InventoryItem>& getInventoryItems()   { ensureLoaded(); return store().items; }
const std::vector<std::string>&   getInventoryUnlocks() { ensureLoaded(); return store().unlocks; }

int64_t getInventoryQuantity(const std::string& ckey) {
    ensureLoaded();
    auto it = store().index.find(ckey);
    return it == store().index.end() ? 0 : store().items[it->second].qant;
}

int64_t addInventoryItem(const std::string& ckey, int64_t delta) {
    ensureLoaded();
    Store& st = store();
    auto it = st.index.find(ckey);
    if (it == st.index.end()) {
        st.index[ckey] = st.items.size();
        st.items.push_back({ckey, std::max<int64_t>(0, delta)});
        return st.items.back().qant;
    }
    int64_t& q = st.items[it->second].qant;
    q = std::max<int64_t>(0, q + delta);
    return q;
}

void addInventoryUnlock(const std::string& ckey) {
    ensureLoaded();
    Store& st = store();
    if (st.unlockSet.insert(ckey).second) st.unlocks.push_back(ckey);
}

} // namespace gw2::data
