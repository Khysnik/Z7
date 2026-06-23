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

std::unique_ptr<blaze::Packet> InventoryComponent::handlePacket(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    uint16_t command = request.getCommand();

    switch (command) {
        case 0x0001:
            return handleGetInventory(request, client);
        case 0x0002:
            return handleAddInventory(request, client);
        case 0x0003:
            return handleUseConsumable(request, client);
        default:
            LOG_WARN("[Inventory] Unknown command: 0x{:04X}", command);
            return request.createReply();
    }
}

std::unique_ptr<blaze::Packet> InventoryComponent::handleAddInventory(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
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
        .integer("FFCB", balance)
        .integerMap("ILST", items)
        .integer("UID", config::blazeId)
        .build();

    // The live coin counter updates from the Inventory Notif0x000C push
    auto notif = std::make_unique<blaze::Packet>(
        static_cast<blaze::ComponentId>(0x0803), 0x000C,
        blaze::MessageType::Notification, 0);
    notif->setPayload(payload);
    client->sendPacket(std::move(notif));

    auto reply = request.createReply();
    reply->setPayload(payload);
    return reply;
}

std::unique_ptr<blaze::Packet> InventoryComponent::handleUseConsumable(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
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
        .beginStruct("CNSU")
            .integer("ACTT", blazeServerNow())
            .string("CKEY", ikey)
            .integer("DURA", 1)
            .integer("QANT", remaining)
        .endStruct()
        .integer("UID", config::blazeId)
        .build();

    auto reply = request.createReply();
    reply->setPayload(payload);
    return reply;
}

std::unique_ptr<blaze::Packet> InventoryComponent::handleGetInventory(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    LOG_INFO("[Inventory] getInventory from {}", client->getRemoteAddress());

    // Make sure any sticker set the player has fully collected has its character variant unlocked before we report inventory
    if (int n = data::reconcileVariants(); n > 0) {
        data::saveInventory();
        LOG_INFO("[Inventory] reconciled {} character variant(s) from owned pieces", n);
    }

    blaze::TdfBuilder b;
    b.beginStruct("INVT")
        .beginList("CLST");
    for (auto& item : data::getInventoryItems()) {
        b.beginStruct()
            .integer("ACTT", 0)
            .string("CKEY", item.ckey)
            .integer("DURA", 1)
            .integer("QANT", item.qant)
        .endStruct();
    }
    b.endList()
        .list("ULST", blaze::TdfType::String, data::getInventoryUnlocks())
     .endStruct()
     .beginStruct("TINV")
        .list("ULST", blaze::TdfType::String, { "abchrb" })
     .endStruct();

    auto reply = request.createReply();
    reply->setPayload(b.build());
    return reply;
}

} // namespace gw2::components
