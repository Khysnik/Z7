#pragma once
#include <vector>
#include <string>
#include <cstdint>

namespace gw2::data {

struct InventoryItem { std::string ckey; int64_t qant; };

const std::vector<InventoryItem>& getInventoryItems();
const std::vector<std::string>&   getInventoryUnlocks();

void    loadInventory(const std::string& path);   // explicit load at startup
bool    saveInventory();                          // persist to the loaded path

int64_t getInventoryQuantity(const std::string& ckey);
int64_t addInventoryItem(const std::string& ckey, int64_t delta);
void    addInventoryUnlock(const std::string& ckey);

} // namespace gw2::data
