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

void ensureLoaded() { 
    if (!store().loaded) loadInventory("data/inventory.json"); 
}

}

// Load inventory.json
void loadInventory(const std::string& path) {
    Store& stor = store();
    stor.loaded = true;
    stor.path = path;
    stor.items.clear(); stor.index.clear(); stor.unlocks.clear(); stor.unlockSet.clear();

    std::ifstream f(path, std::ios::binary);
    if (!f) { 
        LOG_WARN("[Inventory] {} missing; inventory empty", path); 
        return; 
    }
    nlohmann::json json;

    // Validate and parse the json
    try { 
        json = nlohmann::json::parse(f); 
    } catch (const std::exception& e) { 
        LOG_ERROR("[Inventory] {} parse error: {}", path, e.what()); 
        return; 
    }

    // Load items
    for (const auto& item : json.value("items", nlohmann::json::array())) {
        InventoryItem inventoryItem {
            item.value("ckey", ""), item.value("qant", (int64_t)0)
        };
        if (inventoryItem.ckey.empty()) continue;
        stor.index[inventoryItem.ckey] = stor.items.size();
        stor.items.push_back(std::move(inventoryItem));
    }

    // Load unlocks
    for (const auto& unlocks : json.value("unlocks", nlohmann::json::array())) {
        std::string unlockValue = unlocks.get<std::string>();
        if (stor.unlockSet.insert(unlockValue).second) stor.unlocks.push_back(std::move(unlockValue));
    }

    LOG_INFO("[Inventory] loaded {} items, {} unlocks from {}", stor.items.size(), stor.unlocks.size(), path);
}

bool saveInventory() {
    Store& stor = store();
    if (stor.path.empty()) return false;
    ordered_json j;
    ordered_json items = ordered_json::array();
    for (const auto& it : stor.items) items.push_back(ordered_json{{"ckey", it.ckey}, {"qant", it.qant}});
    j["items"]   = items;
    j["unlocks"] = stor.unlocks;

    std::ofstream f(stor.path, std::ios::binary | std::ios::trunc);
    if (!f) { LOG_ERROR("[Inventory] cannot write {}", stor.path); return false; }
    f << j.dump(2);
    return true;
}

const std::vector<InventoryItem>& getInventoryItems() { 
    ensureLoaded(); 
    return store().items; 
}

const std::vector<std::string>& getInventoryUnlocks() { 
    ensureLoaded(); 
    return store().unlocks; 
}

int64_t getInventoryQuantity(const std::string& ckey) {
    ensureLoaded();
    auto it = store().index.find(ckey);
    return it == store().index.end() ? 0 : store().items[it->second].qant;
}

int64_t addInventoryItem(const std::string& ckey, int64_t delta) {
    ensureLoaded();
    Store& stor = store();
    auto it = stor.index.find(ckey);
    if (it == stor.index.end()) {
        stor.index[ckey] = stor.items.size();
        stor.items.push_back({ckey, std::max<int64_t>(0, delta)});
        return stor.items.back().qant;
    }
    int64_t& q = stor.items[it->second].qant;
    q = std::max<int64_t>(0, q + delta);
    return q;
}

void addInventoryUnlock(const std::string& ckey) {
    ensureLoaded();
    Store& stor = store();
    if (stor.unlockSet.insert(ckey).second) stor.unlocks.push_back(ckey);
}

} // namespace gw2::data
