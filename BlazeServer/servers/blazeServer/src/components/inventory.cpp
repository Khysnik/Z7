#include "components/inventory.hpp"
#include "blaze/tdf.hpp"
#include "data/inventory.hpp"
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

    blaze::TdfList clst;
    for (auto& item : data::getInventoryItems()) {
        clst.push_back(std::make_shared<blaze::TdfValue>("", blaze::TdfType::Struct,
            blaze::TdfBuilder()
                .integer("ACTT", 0)
                .string("CKEY", item.ckey)
                .integer("DURA", 1)
                .integer("QANT", item.qant)
                .build()));
    }

    blaze::TdfStruct invt;
    invt["CLST"] = std::make_shared<blaze::TdfValue>("CLST", blaze::TdfType::List, clst);
    invt["ULST"] = std::make_shared<blaze::TdfValue>("ULST", blaze::TdfType::List,
        [&]() {
            blaze::TdfList ul;
            for (auto& s : data::getInventoryUnlocks()) {
                ul.push_back(std::make_shared<blaze::TdfValue>("", blaze::TdfType::String, s));
            }
            return ul;
        }());

    blaze::TdfList tinvUlst;
    tinvUlst.push_back(std::make_shared<blaze::TdfValue>("", blaze::TdfType::String, std::string("abchrb")));

    blaze::TdfStruct tinv;
    tinv["ULST"] = std::make_shared<blaze::TdfValue>("ULST", blaze::TdfType::List, tinvUlst);

    blaze::TdfStruct response;
    response["INVT"] = std::make_shared<blaze::TdfValue>("INVT", blaze::TdfType::Struct, invt);
    response["TINV"] = std::make_shared<blaze::TdfValue>("TINV", blaze::TdfType::Struct, tinv);

    auto reply = request.createReply();
    reply->setPayload(response);
    return reply;
}

} // namespace gw2::components
