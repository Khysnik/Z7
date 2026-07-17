#include "components/inventory.hpp"
#include "blaze/tdf.hpp"
#include "config.hpp"
#include "data/inventory.hpp"
#include "data/loot.hpp"
#include "utils/logger.hpp"
#include "utils/server_time.hpp"

#include <map>
#include <string>

namespace gw2::components {

InventoryComponent::InventoryComponent()
    : Component(blaze::ComponentId::Inventory, "Inventory")
{
}

// Inventory (component 0x0803): the player's persistent inventory
std::unique_ptr<blaze::Packet> InventoryComponent::handlePacket(const blaze::Packet& request, std::shared_ptr<network::ClientConnection> client) {
    uint16_t command = request.getCommand();

    switch (command) {
        // Blaze::Inventory::GetInventory (id=0x01): Returns the player's owned unlocks and consumables.
        case 0x0001:
            return handleGetInventory(request, client);
        // Blaze::Inventory::AddInventory (id=0x02): Adds picked-up items (usually Coinz) to the inventory.
        case 0x0002:
            return handleAddInventory(request, client);
        // Blaze::Inventory::UseConsumable (id=0x03): Consumes a quantity of a consumable and returns the remainder.
        case 0x0003:
            return handleUseConsumable(request, client);
        default:
            LOG_WARN("[Inventory] Unknown command: 0x{:04X}", command);
            return request.createReply();
    }
}

std::unique_ptr<blaze::Packet> InventoryComponent::handleAddInventory(const blaze::Packet& request, std::shared_ptr<network::ClientConnection> client) {
    // Request carries ILST = map<String,Integer> of items picked up (usually Coinz).
    blaze::TdfStruct tdf = request.getPayloadAsTdf();
    std::map<std::string, int64_t> items;
    auto it = tdf.find("ILST");
    if (it != tdf.end() && it->second && it->second->type == blaze::TdfType::Map) {
        const auto& m = std::get<blaze::TdfMapWrapper>(it->second->value);
        for (const auto& [k, v] : m.data) {
            if (v && v->type == blaze::TdfType::Integer)
                items[k] = std::get<blaze::TdfInteger>(v->value);
        }
    }

    for (const auto& [key, qty] : items) data::addInventoryItem(key, qty);
    if (!items.empty()) data::saveInventory();

    int64_t balance = data::getInventoryQuantity("Coinz");
    LOG_INFO("[Inventory] addInventory from {}: {} item(s) -> Coinz {}",
             client->getRemoteAddress(), items.size(), balance);

    blaze::TdfStruct payload = blaze::TdfBuilder()
        .integer("FFCB", balance)                     // finalCoinBalance — Coinz balance after the items were added.
        .integerMap("ILST", items)                    // itemList — The items that were added (key -> quantity).
        .integer("UID", config::blazeId)              // userId — The owning player's Blaze id.
        .build();

    // Notif0x000C = inventory update push: the live coin counter updates from this.
    auto notif = std::make_unique<blaze::Packet>(
        static_cast<blaze::ComponentId>(0x0803), 0x000C,
        blaze::MessageType::Notification, 0);
    notif->setPayload(payload);
    client->sendPacket(std::move(notif));

    auto reply = request.createReply();
    reply->setPayload(payload);
    return reply;
}

std::unique_ptr<blaze::Packet> InventoryComponent::handleUseConsumable(const blaze::Packet& request, std::shared_ptr<network::ClientConnection> client) {
    blaze::TdfStruct tdf = request.getPayloadAsTdf();

    std::string ikey;
    if (auto it = tdf.find("IKEY");
        it != tdf.end() && it->second && it->second->type == blaze::TdfType::String)
        ikey = std::get<blaze::TdfString>(it->second->value);

    int64_t used = 1;
    if (auto it = tdf.find("QANT");
        it != tdf.end() && it->second && it->second->type == blaze::TdfType::Integer)
        used = std::get<blaze::TdfInteger>(it->second->value);

    // Deduct what was consumed
    int64_t remaining = ikey.empty() ? 0 : data::addInventoryItem(ikey, -used);
    if (!ikey.empty()) data::saveInventory();

    LOG_INFO("[Inventory] useConsumable from {}: {} -{} -> {} remaining",
             client->getRemoteAddress(), ikey, used, remaining);

    // Reply CNSU = { ACTT, CKEY, DURA, QANT(remaining) }, UID.
    blaze::TdfStruct payload = blaze::TdfBuilder()
        .beginStruct("CNSU")                          // consumable — The updated consumable record.
            .integer("ACTT", blazeServerNow())        // activationTime — When the consumable was last activated (server time).
            .string("CKEY", ikey)                     // consumableKey — The consumable item key.
            .integer("DURA", 1)                       // duration — Consumable duration.
            .integer("QANT", remaining)               // quantity — Quantity remaining after use.
        .endStruct()
        .integer("UID", config::blazeId)              // userId — The owning player's Blaze id.
        .build();

    auto reply = request.createReply();
    reply->setPayload(payload);
    return reply;
}

std::unique_ptr<blaze::Packet> InventoryComponent::handleGetInventory(const blaze::Packet& request, std::shared_ptr<network::ClientConnection> client) {
    LOG_INFO("[Inventory] getInventory from {}", client->getRemoteAddress());

    // Make sure any sticker set the player has fully collected has its character variant unlocked before we report inventory
    if (int n = data::reconcileVariants(); n > 0) {
        data::saveInventory();
        LOG_INFO("[Inventory] reconciled {} character variant(s) from owned pieces", n);
    }

    int64_t rtyp = 1;
    auto reqTdf = request.getPayloadAsTdf();
    if (auto it = reqTdf.find("RTYP"); it != reqTdf.end() && it->second && it->second->type == blaze::TdfType::Integer)
        rtyp = std::get<blaze::TdfInteger>(it->second->value);

    blaze::TdfBuilder b;
    if (rtyp == 2) {

        constexpr size_t kCatalogSize = 11313;
        const std::vector<int64_t> blst(kCatalogSize, 1);

        b.beginStruct("INVT")                         // inventory — The player's main inventory.
            .intList("BLST", blst)                    // bitList — Per-catalog-item ownership bitmap (0/1 per item).
            .beginList("CLST");                        // consumableList — Owned consumables.
        for (auto& item : data::getInventoryItems()) {
            b.beginStruct()
                .integer("ACTT", 0).string("CKEY", item.ckey)  // activationTime, consumableKey
                .integer("DURA", 1).integer("QANT", item.qant) // duration, quantity
            .endStruct();
        }
        b.endList()
         .endStruct()
         .beginStruct("TINV")                          // trialInventory — Trial/temporary inventory.
            .intList("BLST", blst)                    // bitList — Ownership bitmap.
         .endStruct();
    } else {
        b.beginStruct("INVT")                         // inventory — The player's main inventory.
            .beginList("CLST");                        // consumableList — Owned consumables.
        for (auto& item : data::getInventoryItems()) {
            b.beginStruct()
                .integer("ACTT", 0).string("CKEY", item.ckey)  // activationTime, consumableKey
                .integer("DURA", 1).integer("QANT", item.qant) // duration, quantity
            .endStruct();
        }
        b.endList()
            .list("ULST", blaze::TdfType::String, data::getInventoryUnlocks()) // unlockList — Owned customization unlock keys.
         .endStruct()
         .beginStruct("TINV")                          // trialInventory — Trial/temporary inventory.
            .list("ULST", blaze::TdfType::String, { "abchrb" }) // unlockList — Trial unlock keys.
         .endStruct();
    }

    auto _st = b.build();
    auto reply = request.createReply();
    reply->setPayload(_st);
    return reply;
}

} // namespace gw2::components
