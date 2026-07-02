#pragma once
#include <cstdint>
#include <map>
#include <string>
#include <vector>

namespace gw2::data {

struct LootResult {
    bool        valid = false;
    std::string name;
    int64_t     cost = 0;
    std::map<std::string, int64_t> consumables;  // ais key -> quantity
    std::vector<std::string>       unlocks;      // Cus*/stk* keys
    std::vector<std::string>       itli;         // every rolled card key (reveal order)
};

LootResult rollPack(const std::string& pkey);

int64_t packCost(const std::string& pkey);

int reconcileVariants();

} // namespace gw2::data
