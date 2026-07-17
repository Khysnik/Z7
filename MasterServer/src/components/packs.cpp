#include "components/packs.hpp"
#include "blaze/tdf.hpp"
#include "network/client_connection.hpp"
#include "config.hpp"
#include "data/packs.hpp"
#include "data/loot.hpp"
#include "data/inventory.hpp"
#include "utils/logger.hpp"
#include "utils/json.hpp"
#include "utils/server_time.hpp"

#include <nlohmann/json.hpp>

#include <algorithm>
#include <functional>
#include <map>
#include <string>

namespace gw2::components {

namespace {

std::string getStr(const blaze::TdfStruct& tdf, const std::string& tag) {
    auto it = tdf.find(tag);
    if (it == tdf.end() || !it->second || it->second->type != blaze::TdfType::String) return {};
    return std::get<blaze::TdfString>(it->second->value);
}

int64_t getInt(const blaze::TdfStruct& tdf, const std::string& tag, int64_t fallback = 0) {
    auto it = tdf.find(tag);
    if (it == tdf.end() || !it->second || it->second->type != blaze::TdfType::Integer) return fallback;
    return std::get<blaze::TdfInteger>(it->second->value);
}

blaze::TdfStruct buildTemplateResponse() {
    const nlohmann::json& fixed = utils::dataSection("fixed_packs");
    blaze::TdfBuilder b;
    b.beginList("DESL");                              // packDefinitionList — The buyable pack catalog.
    for (const auto& p : data::getPackTemplates()) {
        if (fixed.is_object() && fixed.contains(p.pkey)) continue;   // gift-only, not buyable
        b.beginStruct()
            .string("ADDT", p.addt)                   // additionalData — Extra pack metadata.
            .integer("AUDL", p.audl)                  // audienceLevel — Targeting / availability level.
            .string("CONS", p.cons)                   // contents — Pack contents descriptor.
            .string("DESC", p.desc)                   // description — Pack description text.
            .string("GKEY", p.gkey)                   // groupKey — Pack group key.
            .string("IMGN", p.imgn)                   // imageName — Store image asset name.
            .string("PKEY", p.pkey)                   // packKey — Unique pack key.
            .integer("PRIC", p.pric)                  // price — Cost in currency (Coinz/Stars).
            .integer("STID", p.stid)                  // storeId — Store catalog id.
            .integer("STRK", p.strk)                  // storeRank — Store sort rank.
            .string("TITL", p.titl)                   // title — Pack display title.
            .list("TYPE", blaze::TdfType::String, p.type) // typeList — Pack type tags.
        .endStruct();
    }
    return b.endList().build();
}

bool isChestPack(const std::string& pid) { return pid.rfind("chestpk", 0) == 0; }

bool isGiftPack(const std::string& pid) {
    const auto& gp = data::getGiftPacks();
    return std::find(gp.begin(), gp.end(), pid) != gp.end();
}

enum class OpenKind { Coin, Chest, Gift };

blaze::TdfStruct buildOpenResponse(const std::string& pkey, std::shared_ptr<ClientConnection> client, OpenKind kind = OpenKind::Coin) {
    const bool isChest = kind == OpenKind::Chest;
    const bool isGift  = kind == OpenKind::Gift;
    const bool chestRoll = isChest || isGift;   // both roll the chest reward table

    const std::string currency = isChest ? "Star" : "Coinz";

    auto loot = isChest ? (pkey.rfind("chestpkhub", 0) == 0 ? data::rollHubChest() : data::rollChest())
              : chestRoll ? data::rollChest()
                          : data::rollPack(pkey);

    if (isChest) loot.cost = 3;
    else if (isGift) loot.cost = 0;             // community reward is free
    int64_t balance;
    if (loot.valid) {

        balance = data::addInventoryItem(currency, -loot.cost);
        for (const auto& [ais, qty] : loot.consumables) data::addInventoryItem(ais, qty);
        for (const auto& key : loot.unlocks)            data::addInventoryUnlock(key);
        if (isGift) data::removeGiftPack(pkey);   // consume the opened gift pack
        data::saveInventory();
        LOG_INFO("[Packs] {} '{}' ({}): -{} {} -> {}, {} consumables, {} unlocks",
                 isChest ? "opened chest" : isGift ? "opened gift pack" : "opened", pkey, loot.name,
                 loot.cost, currency, balance, loot.consumables.size(), loot.unlocks.size());
    } else {
        balance = data::getInventoryQuantity(currency);
        LOG_WARN("[Packs] open undefined pack '{}' -> empty grant", pkey);
    }

    std::map<std::string, int64_t> cnsm(loot.consumables.begin(), loot.consumables.end());

    static int64_t s_packInstance = 1;
    int64_t now = blazeServerNow() * 1000000LL;
    const char* packType = isChest ? "CardPackType_ForChests"
                         : isGift  ? "CardPackType_CommunityPortalReward"
                                   : "CardPackType_Regular";
    blaze::TdfStruct resp = blaze::TdfBuilder()
        .integer("FFCB", balance)                     // finalCoinBalance — Currency balance after the open.
        .string("FFCT", currency)                     // finalCoinType — Currency spent (Coinz/Star).
        .beginStruct("ORSP")                          // openResponse — The opened-pack result wrapper.
            .integerMap("CNSM", cnsm)                 // consumables — Consumable items granted (key -> qty).
            .beginStruct("PACK")                      // pack — The opened pack instance.
                .string("ADDT", "")                   // additionalData — Extra pack metadata.
                .integer("AUDL", chestRoll ? -1 : 65) // audienceLevel — Targeting / availability level.
                .integer("COST", loot.cost)           // cost — Amount charged for this open.
                .string("DESC", "")                   // description — Pack description text.
                .string("GKEY", "")                   // groupKey — Pack group key.
                .string("IMGN", "")                   // imageName — Store image asset name.
                .list("ITLI", blaze::TdfType::String, loot.itli) // itemList — The revealed items.
                .integer("PID", 647607740 + s_packInstance++)    // packInstanceId — Unique opened-pack instance id.
                .string("PKEY", pkey)                 // packKey — The opened pack's key.
                .string("PUDS", "")                   // packUserData — Per-user pack display string.
                .integer("SCAT", 2)                   // storeCategory — Store category id.
                .integer("TGEN", now)                 // timeGenerated — When the pack instance was generated (server time).
                .string("TITL", "")                   // title — Pack display title.
                .integer("TVAL", now)                 // timeValue — Pack timestamp / valid-from.
                .list("TYPE", blaze::TdfType::String, { packType }) // typeList — Card pack type (e.g. CardPackType_Regular).
                .integer("UID", config::blazeId)      // userId — The owning player's Blaze id.
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

blaze::TdfStruct buildGiftOpenReply(const std::string& pkey, std::shared_ptr<ClientConnection> client) {

    auto loot = data::rollFixedPack(pkey);
    if (!loot.valid) loot = data::rollPack(pkey);
    if (!loot.valid) {
        int items = 1;
        for (const auto& c : utils::dataSection("community").value("portalChests", nlohmann::json::array()))
            if (c.value("pkey", std::string()) == pkey) { items = c.value("items", 1); break; }
        loot = data::rollCosmeticPack(items);
    }
    std::map<std::string, int64_t> ilst;
    if (loot.valid) {
        for (const auto& [ais, qty] : loot.consumables) { data::addInventoryItem(ais, qty); ilst[ais] += qty; }
        for (const auto& key : loot.unlocks)            { data::addInventoryUnlock(key);    ilst[key] = 1; }
    }
    data::removeGiftPack(pkey);          // consume the opened gift pack
    data::saveInventory();
    LOG_INFO("[Packs] opened gift pack '{}' ({}): {} items revealed",
             pkey, loot.name, loot.itli.size());

    static int64_t s_giftReveal = 327126312;   // fresh reveal-instance id per open
    const int64_t now = blazeServerNow() * 1000000LL;
    blaze::TdfStruct resp = blaze::TdfBuilder()
        .beginStruct("PACK")                          // pack — The opened gift-pack instance (bare, no TYPE).
            .string ("ADDT", "")                      // additionalData — Extra pack metadata.
            .integer("AUDL", -1)                      // audienceLevel — Targeting / availability level.
            .integer("COST", 0)                       // cost — Free for gift packs.
            .string ("DESC", "")                      // description — Pack description text.
            .string ("GKEY", "")                      // groupKey — Pack group key.
            .string ("IMGN", "")                      // imageName — Store image asset name.
            .list   ("ITLI", blaze::TdfType::String, loot.itli)   // itemList — The revealed items.
            .integer("PID",  ++s_giftReveal)          // packInstanceId — Fresh reveal-instance id per open.
            .string ("PKEY", pkey)                    // packKey — The opened pack's key.
            .string ("PUDS", "")                      // packUserData — Per-user pack display string.
            .integer("SCAT", 2)                       // storeCategory — Store category id.
            .integer("TGEN", now)                     // timeGenerated — When the pack instance was generated (server time).
            .string ("TITL", "")                      // title — Pack display title.
            .integer("TVAL", now)                     // timeValue — Pack timestamp / valid-from.
            .integer("UID",  config::blazeId)         // userId — The owning player's Blaze id.
        .endStruct()
        .build();

    // Push the live inventory update so the revealed items appear immediately.
    if (loot.valid && client && !ilst.empty()) {
        auto notif = std::make_unique<blaze::Packet>(
            static_cast<blaze::ComponentId>(0x0803), 0x000B, blaze::MessageType::Notification, 0);
        notif->setPayload(blaze::TdfBuilder()
            .integer("FFCB", data::getInventoryQuantity("Coinz"))
            .integerMap("ILST", ilst)
            .integer("UID", config::blazeId)
            .build());
        client->sendPacket(std::move(notif));
    }
    return resp;
}

int64_t giftPackPid(const std::string& pkey) {
    const size_t p = pkey.find_first_of("0123456789");
    if (p != std::string::npos) {
        try { return 95462360LL + std::stoll(pkey.substr(p)); } catch (...) {}
    }
    return static_cast<int64_t>(std::hash<std::string>{}(pkey) & 0x7FFFFFFF);
}

std::string giftPackByPid(int64_t pid) {
    for (const auto& pkey : data::getGiftPacks())
        if (giftPackPid(pkey) == pid) return pkey;
    return {};
}

blaze::TdfStruct buildPacksResponse() {
    // pkey -> real pack definition from the catalog
    std::map<std::string, const data::PackTemplate*> defs;
    for (const auto& p : data::getPackTemplates()) defs[p.pkey] = &p;

    const int64_t now = blazeServerNow() * 1000000LL;
    blaze::TdfBuilder b;
    b.beginList("PLST");                              // packList — The packs the player currently owns (gift packs).
    for (const auto& pkey : data::getGiftPacks()) {
        auto it = defs.find(pkey);
        const data::PackTemplate* d = it != defs.end() ? it->second : nullptr;
        const std::string title = d ? d->titl : pkey;
        const std::string desc  = d ? d->desc : std::string();
        const std::string gkey  = d ? d->gkey : std::string();
        const std::string imgn  = d ? d->imgn : std::string();
        const std::string addt  = d ? d->addt : std::string();
        const int64_t     audl  = d ? d->audl : 65;
        const std::vector<std::string> type =
            (d && !d->type.empty()) ? d->type : std::vector<std::string>{ "CardPackType_Regular" };
        // PACK element — same fields as documented in buildOpenResponse's PACK.
        b.beginStruct()
            .string ("ADDT", addt)                          // additionalData
            .integer("AUDL", audl)                          // audienceLevel
            .integer("COST", 0)                             // cost — gift -> free to open
            .string ("DESC", desc)                          // description
            .string ("GKEY", gkey)                          // groupKey
            .string ("IMGN", imgn)                          // imageName — pack's own icon
            .list   ("ITLI", blaze::TdfType::String, {})    // itemList — contents hidden until opened
            .integer("PID",  giftPackPid(pkey))             // packInstanceId
            .string ("PKEY", pkey)                          // packKey
            .string ("PUDS", "")                            // packUserData
            .integer("SCAT", 2)                             // storeCategory
            .integer("TGEN", 0)                             // timeGenerated
            .string ("TITL", title)                         // title
            .integer("TVAL", now)                           // timeValue
            .list   ("TYPE", blaze::TdfType::String, type)  // typeList — pack's real type
            .integer("UID",  config::blazeId)               // userId
        .endStruct();
    }
    b.endList();
    return b.build();
}

}

PacksComponent::PacksComponent()
    : Component(static_cast<blaze::ComponentId>(0x0802), "PacksComponent")
{
}

std::unique_ptr<blaze::Packet> PacksComponent::handlePacket(const blaze::Packet& request, std::shared_ptr<ClientConnection> client) {
    uint16_t command = request.getCommand();

    // Packs (component 0x0802)
    switch (static_cast<blaze::PacksCommand>(command)) {
        // Blaze::Packs::GetPacks (id=0x01): Returns the packs the player currently owns (grantable/openable).
        case blaze::PacksCommand::getPacks:
            return handleGetPacks(request, client);
        // Blaze::Packs::GrantPacks (id=0x02): Grants one or more packs to the player.
        case blaze::PacksCommand::grantPacks:
            return handleGrantPacks(request, client);
        // Blaze::Packs::RedeemPack (id=0x03): Redeems a pack from a code/entitlement.
        case blaze::PacksCommand::redeemPack:
            return handleRedeemPack(request, client);
        // Blaze::Packs::OpenPack (id=0x04): Opens an owned pack and rolls its reward table.
        case blaze::PacksCommand::openPack:
            return handleOpenPack(request, client);
        // Blaze::Packs::AcquireCalendarPacks (id=0x05): Acquires the daily/calendar packs the player is entitled to.
        case blaze::PacksCommand::acquireCalendarPacks:
            return handleAcquireCalendarPacks(request, client);
        // Blaze::Packs::GetTemplate (id=0x06): Returns the pack template/catalog definitions.
        case blaze::PacksCommand::getTemplate:
            return handleGetTemplate(request, client);
        // Blaze::Packs::Wipe (id=0x07): Clears the player's pack inventory (debug).
        case blaze::PacksCommand::wipe:
            return handleWipe(request, client);
        // Blaze::Packs::PurchaseAndOpenPack (id=0x1F4): Purchases a pack with currency and opens it in one step (store flow).
        case blaze::PacksCommand::purchaseAndOpenPack:
            return handlePurchaseAndOpenPack(request, client);
        // Blaze::Packs::DebugGrant (id=0x1F5): Debug — grant arbitrary packs/items.
        case blaze::PacksCommand::debugGrant:
            return handleDebugGrant(request, client);
        // Blaze::Packs::GrantAndOpenPacks (id=0x1F9): Grants and immediately opens packs (e.g. reward chests).
        case blaze::PacksCommand::grantAndOpenPacks:
            return handleGrantAndOpenPacks(request, client);
        default:
            LOG_WARN("[Packs] Unknown command: 0x{:04X}", command);
            return request.createReply();
    }
}

std::unique_ptr<blaze::Packet> PacksComponent::handleGetPacks(const blaze::Packet& request, std::shared_ptr<ClientConnection> client) {
    LOG_INFO("[Packs] getPacks from {}", client->getRemoteAddress());

    auto reply = request.createReply();
    reply->setPayload(buildPacksResponse());
    return reply;
}

std::unique_ptr<blaze::Packet> PacksComponent::handleGrantPacks(const blaze::Packet& request, std::shared_ptr<ClientConnection> client) {
    LOG_INFO("[Packs] grantPacks from {}", client->getRemoteAddress());
    return request.createReply();
}

std::unique_ptr<blaze::Packet> PacksComponent::handleRedeemPack(const blaze::Packet& request, std::shared_ptr<ClientConnection> client) {
    LOG_INFO("[Packs] redeemPack from {}", client->getRemoteAddress());
    return request.createReply();
}

std::unique_ptr<blaze::Packet> PacksComponent::handleOpenPack(const blaze::Packet& request, std::shared_ptr<ClientConnection> client) {
    const blaze::TdfStruct& tdf = request.getPayloadAsTdf();

    const int64_t pidNum = getInt(tdf, "PID");
    std::string pid = giftPackByPid(pidNum);
    if (pid.empty()) pid = getStr(tdf, "PID");
    LOG_INFO("[Packs] openPack '{}' (pid={}) from {}", pid, pidNum, client->getRemoteAddress());

    if (isGiftPack(pid)) {
        auto reply = request.createReply();
        reply->setPayload(buildGiftOpenReply(pid, client));
        return reply;
    }

    if (isChestPack(pid)) {
        auto reply = request.createReply();
        blaze::TdfStruct resp = buildOpenResponse(pid, client, OpenKind::Chest);
        if (!resp.empty()) reply->setPayload(resp);
        return reply;
    }

    int64_t cost = data::packCost(pid);
    int64_t balance = data::getInventoryQuantity("Coinz");
    if (cost > 0 && balance < cost) {
        LOG_WARN("[Packs] insufficient funds for '{}': have {}, need {}", pid, balance, cost);
        return request.createErrorReply(blaze::BlazeError::ERR_INSUFFICIENT_FUNDS);
    }

    if (!data::packHasReward(pid)) {
        LOG_WARN("[Packs] '{}' has no unowned reward (all characters unlocked) -> refusing open", pid);
        return request.createErrorReply(blaze::BlazeError::ERR_INSUFFICIENT_FUNDS);
    }

    auto reply = request.createReply();
    blaze::TdfStruct resp = buildOpenResponse(pid, client);
    if (!resp.empty()) reply->setPayload(resp);
    return reply;
}

std::unique_ptr<blaze::Packet> PacksComponent::handleAcquireCalendarPacks(const blaze::Packet& request, std::shared_ptr<ClientConnection> client) {
    LOG_INFO("[Packs] acquireCalendarPacks from {}", client->getRemoteAddress());
    return request.createReply();
}

std::unique_ptr<blaze::Packet> PacksComponent::handleGetTemplate(const blaze::Packet& request, std::shared_ptr<ClientConnection> client) {
    LOG_INFO("[Packs] getTemplate from {}", client->getRemoteAddress());

    auto reply = request.createReply();
    reply->setPayload(buildTemplateResponse());
    return reply;
}

std::unique_ptr<blaze::Packet> PacksComponent::handleWipe(const blaze::Packet& request, std::shared_ptr<ClientConnection> client) {
    LOG_INFO("[Packs] wipe from {}", client->getRemoteAddress());
    return request.createReply();
}

std::unique_ptr<blaze::Packet> PacksComponent::handlePurchaseAndOpenPack(const blaze::Packet& request, std::shared_ptr<ClientConnection> client) {
    std::string pid = getStr(request.getPayloadAsTdf(), "PID");
    LOG_INFO("[Packs] purchaseAndOpenPack '{}' from {}", pid, client->getRemoteAddress());

    if (isChestPack(pid)) {
        auto reply = request.createReply();
        blaze::TdfStruct resp = buildOpenResponse(pid, client, OpenKind::Chest);
        if (!resp.empty()) reply->setPayload(resp);
        return reply;
    }

    if (isGiftPack(pid)) {
        auto reply = request.createReply();
        blaze::TdfStruct resp = buildOpenResponse(pid, client, OpenKind::Gift);
        if (!resp.empty()) reply->setPayload(resp);
        return reply;
    }

    int64_t cost = data::packCost(pid);
    int64_t balance = data::getInventoryQuantity("Coinz");
    if (cost > 0 && balance < cost) {
        LOG_WARN("[Packs] insufficient funds for '{}': have {}, need {}", pid, balance, cost);
        return request.createErrorReply(blaze::BlazeError::ERR_INSUFFICIENT_FUNDS);
    }

    if (!data::packHasReward(pid)) {
        LOG_WARN("[Packs] '{}' has no unowned reward (all characters unlocked) -> refusing open", pid);
        return request.createErrorReply(blaze::BlazeError::ERR_INSUFFICIENT_FUNDS);
    }

    auto reply = request.createReply();
    blaze::TdfStruct resp = buildOpenResponse(pid, client);
    if (!resp.empty()) reply->setPayload(resp);
    return reply;
}

std::unique_ptr<blaze::Packet> PacksComponent::handleDebugGrant(const blaze::Packet& request, std::shared_ptr<ClientConnection> client) {
    LOG_INFO("[Packs] debugGrant from {}", client->getRemoteAddress());
    return request.createReply();
}

std::unique_ptr<blaze::Packet> PacksComponent::handleGrantAndOpenPacks(const blaze::Packet& request, std::shared_ptr<ClientConnection> client) {
    LOG_INFO("[Packs] grantAndOpenPacks from {}", client->getRemoteAddress());
    return request.createReply();
}

} // namespace gw2::components
