#pragma once
#include <cstdint>
#include <map>
#include <string>
#include <vector>

namespace gw2::data {

// Result of opening one pack: consumables to grant (ais -> qty), license/sticker
// unlocks to grant, the full reveal list (ITLI), and the coin cost.
struct LootResult {
    bool        valid = false;
    std::string name;
    int64_t     cost = 0;
    std::map<std::string, int64_t> consumables;  // ais key -> quantity
    std::vector<std::string>       unlocks;      // Cus*/stk* keys
    std::vector<std::string>       itli;         // every rolled card key (reveal order)
};

// Roll a pack by its PKEY using data/pack_tables.json + data/items.json.
LootResult rollPack(const std::string& pkey);

// Coin cost of a pack, or -1 if the PKEY is not in the loot tables.
int64_t packCost(const std::string& pkey);

// Grant the character-variant license for any sticker set whose pieces the player
// already fully owns but whose variant license is missing (e.g. sets completed
// before variant unlocks were granted). Returns how many variants were granted.
int reconcileVariants();

} // namespace gw2::data
