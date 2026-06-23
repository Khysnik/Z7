#include "components/packs.hpp"
#include "blaze/tdf.hpp"
#include "network/client_connection.hpp"
#include "config.hpp"
#include "data/packs.hpp"
#include "data/loot.hpp"
#include "data/inventory.hpp"
#include "utils/logger.hpp"
#include "utils/server_time.hpp"

#include <map>
#include <string>

namespace gw2::components {

namespace {

std::string getStr(const blaze::TdfStruct& tdf, const std::string& tag) {
    auto it = tdf.find(tag);
    if (it == tdf.end() || !it->second || it->second->type != blaze::TdfType::String) return {};
    return std::get<blaze::TdfString>(it->second->value);
}

// Full pack catalog (DESL) from data/packs.json
blaze::TdfStruct buildTemplateResponse() {
    blaze::TdfBuilder b;
    b.beginList("DESL");
    for (const auto& p : data::getPackTemplates()) {
        b.beginStruct()
            .string("ADDT", p.addt)
            .integer("AUDL", p.audl)
            .string("CONS", p.cons)
            .string("DESC", p.desc)
            .string("GKEY", p.gkey)
            .string("IMGN", p.imgn)
            .string("PKEY", p.pkey)
            .integer("PRIC", p.pric)
            .integer("STID", p.stid)
            .integer("STRK", p.strk)
            .string("TITL", p.titl)
            .list("TYPE", blaze::TdfType::String, p.type)
        .endStruct();
    }
    return b.endList().build();
}

blaze::TdfStruct buildOpenResponse(const std::string& pkey, std::shared_ptr<ClientConnection> client) {
    auto loot = data::rollPack(pkey);
    int64_t balance;
    if (loot.valid) {
        // Deduct cost, grant consumables + unlocks, persist.
        balance = data::addInventoryItem("Coinz", -loot.cost);
        for (const auto& [ais, qty] : loot.consumables) data::addInventoryItem(ais, qty);
        for (const auto& key : loot.unlocks)            data::addInventoryUnlock(key);
        data::saveInventory();
        LOG_INFO("[Packs] opened '{}' ({}): -{} coinz -> {}, {} consumables, {} unlocks",
                 pkey, loot.name, loot.cost, balance, loot.consumables.size(), loot.unlocks.size());
    } else {
        balance = data::getInventoryQuantity("Coinz");
        LOG_WARN("[Packs] open undefined pack '{}' -> empty grant", pkey);
    }

    std::map<std::string, int64_t> cnsm(loot.consumables.begin(), loot.consumables.end());

    static int64_t s_packInstance = 1;
    int64_t now = blazeServerNow() * 1000000LL;
    blaze::TdfStruct resp = blaze::TdfBuilder()
        .integer("FFCB", balance)
        .string("FFCT", "Coinz")
        .beginStruct("ORSP")
            .integerMap("CNSM", cnsm)
            .beginStruct("PACK")
                .string("ADDT", "")
                .integer("AUDL", 65)
                .integer("COST", loot.cost)
                .string("DESC", "")
                .string("GKEY", "")
                .string("IMGN", "")
                .list("ITLI", blaze::TdfType::String, loot.itli)
                .integer("PID", 647607740 + s_packInstance++)
                .string("PKEY", pkey)
                .string("PUDS", "")
                .integer("SCAT", 2)
                .integer("TGEN", now)
                .string("TITL", "")
                .integer("TVAL", now)
                .list("TYPE", blaze::TdfType::String, {"CardPackType_Regular"})
                .integer("UID", config::blazeId)
            .endStruct()
        .endStruct()
        .build();

    // Push the live inventory update so coins/items/unlocks appear immediately
    if (loot.valid && client) {
        // ILST is the full inventory delta. Include the unlocks (cosmetics, sticker pieces, character licenses)
        std::map<std::string, int64_t> ilst = cnsm;
        for (const auto& key : loot.unlocks) ilst[key] = 1;

        auto notif = std::make_unique<blaze::Packet>(
            static_cast<blaze::ComponentId>(0x0803), 0x000B,
            blaze::MessageType::Notification, 0);
        notif->setPayload(blaze::TdfBuilder()
            .integer("FFCB", balance)
            .integerMap("ILST", ilst)
            .integer("UID", config::blazeId)
            .build());
        client->sendPacket(std::move(notif));
    }
    return resp;
}

// Owned-packs (GetPacks) response — currently a single placeholder test pack.
blaze::TdfStruct buildPacksResponse() {
    return blaze::TdfBuilder()
        .beginList("DESL")
            .beginStruct()
                .string("ADDT", "")
                .integer("AUDL", 0)
                .string("CONS", "")
                .string("DESC", "Test Pack")
                .string("GKEY", "test_pack")
                .string("IMGN", "pack_test")
                .string("PKEY", "test_pack")
                .integer("PRIC", 100)
                .integer("STID", 1)
                .integer("STRK", 0)
                .list("TAGS", blaze::TdfType::String, {})
                .string("TITL", "Test Pack")
                .list("TYPE", blaze::TdfType::String, {"standard"})
                .intList("UIDS", {})
            .endStruct()
        .endList()
        .build();
}

}

PacksComponent::PacksComponent()
    : Component(static_cast<blaze::ComponentId>(0x0802), "PacksComponent")
{
}

std::unique_ptr<blaze::Packet> PacksComponent::handlePacket(
    const blaze::Packet& request,
    std::shared_ptr<ClientConnection> client
) {
    uint16_t command = request.getCommand();

    switch (static_cast<blaze::PacksCommand>(command)) {
        case blaze::PacksCommand::getPacks:
            return handleGetPacks(request, client);
        case blaze::PacksCommand::grantPacks:
            return handleGrantPacks(request, client);
        case blaze::PacksCommand::redeemPack:
            return handleRedeemPack(request, client);
        case blaze::PacksCommand::openPack:
            return handleOpenPack(request, client);
        case blaze::PacksCommand::acquireCalendarPacks:
            return handleAcquireCalendarPacks(request, client);
        case blaze::PacksCommand::getTemplate:
            return handleGetTemplate(request, client);
        case blaze::PacksCommand::wipe:
            return handleWipe(request, client);
        case blaze::PacksCommand::purchaseAndOpenPack:
            return handlePurchaseAndOpenPack(request, client);
        case blaze::PacksCommand::debugGrant:
            return handleDebugGrant(request, client);
        case blaze::PacksCommand::grantAndOpenPacks:
            return handleGrantAndOpenPacks(request, client);
        default:
            LOG_WARN("[Packs] Unknown command: 0x{:04X}", command);
            return request.createReply();
    }
}

std::unique_ptr<blaze::Packet> PacksComponent::handleGetPacks(
    const blaze::Packet& request,
    std::shared_ptr<ClientConnection> client
) {
    LOG_INFO("[Packs] getPacks from {}", client->getRemoteAddress());

    auto reply = request.createReply();
    reply->setPayload(buildPacksResponse());
    return reply;
}

std::unique_ptr<blaze::Packet> PacksComponent::handleGrantPacks(
    const blaze::Packet& request,
    std::shared_ptr<ClientConnection> client
) {
    LOG_INFO("[Packs] grantPacks from {}", client->getRemoteAddress());
    return request.createReply();
}

std::unique_ptr<blaze::Packet> PacksComponent::handleRedeemPack(
    const blaze::Packet& request,
    std::shared_ptr<ClientConnection> client
) {
    LOG_INFO("[Packs] redeemPack from {}", client->getRemoteAddress());
    return request.createReply();
}

std::unique_ptr<blaze::Packet> PacksComponent::handleOpenPack(
    const blaze::Packet& request,
    std::shared_ptr<ClientConnection> client
) {
    std::string pid = getStr(request.getPayloadAsTdf(), "PID");
    LOG_INFO("[Packs] openPack '{}' from {}", pid, client->getRemoteAddress());

    int64_t cost = data::packCost(pid);
    int64_t balance = data::getInventoryQuantity("Coinz");
    if (cost > 0 && balance < cost) {
        LOG_WARN("[Packs] insufficient funds for '{}': have {}, need {}", pid, balance, cost);
        return request.createErrorReply(blaze::BlazeError::ERR_INSUFFICIENT_FUNDS);
    }

    auto reply = request.createReply();
    blaze::TdfStruct resp = buildOpenResponse(pid, client);
    if (!resp.empty()) reply->setPayload(resp);
    return reply;
}

std::unique_ptr<blaze::Packet> PacksComponent::handleAcquireCalendarPacks(
    const blaze::Packet& request,
    std::shared_ptr<ClientConnection> client
) {
    LOG_INFO("[Packs] acquireCalendarPacks from {}", client->getRemoteAddress());
    return request.createReply();
}

std::unique_ptr<blaze::Packet> PacksComponent::handleGetTemplate(
    const blaze::Packet& request,
    std::shared_ptr<ClientConnection> client
) {
    LOG_INFO("[Packs] getTemplate from {}", client->getRemoteAddress());

    auto reply = request.createReply();
    reply->setPayload(buildTemplateResponse());
    return reply;
}

std::unique_ptr<blaze::Packet> PacksComponent::handleWipe(
    const blaze::Packet& request,
    std::shared_ptr<ClientConnection> client
) {
    LOG_INFO("[Packs] wipe from {}", client->getRemoteAddress());
    return request.createReply();
}

std::unique_ptr<blaze::Packet> PacksComponent::handlePurchaseAndOpenPack(
    const blaze::Packet& request,
    std::shared_ptr<ClientConnection> client
) {
    std::string pid = getStr(request.getPayloadAsTdf(), "PID");
    LOG_INFO("[Packs] purchaseAndOpenPack '{}' from {}", pid, client->getRemoteAddress());

    int64_t cost = data::packCost(pid);
    int64_t balance = data::getInventoryQuantity("Coinz");
    if (cost > 0 && balance < cost) {
        LOG_WARN("[Packs] insufficient funds for '{}': have {}, need {}", pid, balance, cost);
        return request.createErrorReply(blaze::BlazeError::ERR_INSUFFICIENT_FUNDS);
    }

    auto reply = request.createReply();
    blaze::TdfStruct resp = buildOpenResponse(pid, client);
    if (!resp.empty()) reply->setPayload(resp);
    return reply;
}

std::unique_ptr<blaze::Packet> PacksComponent::handleDebugGrant(
    const blaze::Packet& request,
    std::shared_ptr<ClientConnection> client
) {
    LOG_INFO("[Packs] debugGrant from {}", client->getRemoteAddress());
    return request.createReply();
}

std::unique_ptr<blaze::Packet> PacksComponent::handleGrantAndOpenPacks(
    const blaze::Packet& request,
    std::shared_ptr<ClientConnection> client
) {
    LOG_INFO("[Packs] grantAndOpenPacks from {}", client->getRemoteAddress());
    return request.createReply();
}

} // namespace gw2::components
