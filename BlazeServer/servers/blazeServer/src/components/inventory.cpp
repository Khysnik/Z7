#include "components/inventory.hpp"
#include "blaze/tdf.hpp"
#include "data/inventory.hpp"
#include "data/loot.hpp"
#include "utils/logger.hpp"

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
        default:
            LOG_WARN("[Inventory] Unknown command: 0x{:04X}", command);
            return request.createReply();
    }
}

std::unique_ptr<blaze::Packet> InventoryComponent::handleGetInventory(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    LOG_INFO("[Inventory] getInventory from {}", client->getRemoteAddress());

    // Make sure any sticker set the player has fully collected has its character
    // variant unlocked before we report inventory (covers sets completed before
    // variant grants existed, and is a no-op once reconciled).
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
