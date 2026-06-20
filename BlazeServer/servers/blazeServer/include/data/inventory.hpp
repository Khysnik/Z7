#pragma once
#include <vector>
#include <string>
#include <cstdint>

namespace gw2::data {

struct InventoryItem { std::string ckey; int64_t qant; };

// Player inventory (consumables/currency in "items", customization unlocks in
// "unlocks"), loaded from data/inventory.json and written back on change.
const std::vector<InventoryItem>& getInventoryItems();
const std::vector<std::string>&   getInventoryUnlocks();

void    loadInventory(const std::string& path);   // explicit load at startup
bool    saveInventory();                          // persist to the loaded path

int64_t getInventoryQuantity(const std::string& ckey);
// Add (or create) an item by a delta; returns the new quantity. Negative deltas
// decrement (clamped at 0). "Coinz" is the spendable currency balance.
int64_t addInventoryItem(const std::string& ckey, int64_t delta);
void    addInventoryUnlock(const std::string& ckey);

} // namespace gw2::data
