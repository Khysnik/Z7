#include "components/pvz_gw.hpp"
#include "blaze/tdf.hpp"
#include "blaze/component.hpp"
#include "network/client_connection.hpp"
#include "components/user_sessions.hpp"
#include "data/licenses.hpp"
#include "data/inventory.hpp"
#include "data/loot.hpp"
#include "config.hpp"
#include "utils/logger.hpp"
#include "utils/json.hpp"
#include "utils/http.hpp"
#include "utils/editorial.hpp"
#include "utils/server_time.hpp"
#include <nlohmann/json.hpp>
#include <algorithm>
#include <map>
#include <string>
#include <fstream>
#include <ctime>
#include <vector>

namespace gw2::components {

namespace {

using nlohmann::json;

int64_t getIntField(const blaze::TdfStruct& tdf, const std::string& tag, int64_t fallback = 0) {
    auto it = tdf.find(tag);
    if (it == tdf.end() || !it->second) {
        return fallback;
    }
    if (it->second->type != blaze::TdfType::Integer) {
        return fallback;
    }
    return std::get<blaze::TdfInteger>(it->second->value);
}

std::string getStrField(const blaze::TdfStruct& tdf, const std::string& tag) {
    auto it = tdf.find(tag);
    if (it == tdf.end() || !it->second || it->second->type != blaze::TdfType::String) return {};
    return std::get<blaze::TdfString>(it->second->value);
}

const json& content(const char* file) {
    std::string key = file;
    const auto dot = key.rfind(".json");
    if (dot != std::string::npos) key.erase(dot);
    return utils::dataSection(key);
}

json fetchLive(const std::string& path, const char* fallbackFile) {
    try {
        return utils::httpGet(utils::kEditorialBase + path);
    } catch (const std::exception& e) {
        LOG_WARN("[PvzGw] editorial {} unavailable ({}); using {}",
                 path, e.what(), fallbackFile ? fallbackFile : "defaults");
        return fallbackFile ? content(fallbackFile) : json::object();
    }
}

// Black market data source. Online -> the shared editorial server (rotating tiers).
// Offline -> the local data.json "blackmarket" section, which is what the launcher's
// black-market editor writes; querying the (local) editorial there would serve its own
// rotation file and silently ignore the launcher's edits.
json blackMarketData() {
    return config::onlineMode ? fetchLive("/gw2/live/blackmarket", "blackmarket.json")
                              : content("blackmarket.json");
}

// Community portal event data. Same online/offline split as the black market: offline
// uses the local data.json "community" section (the launcher's Community Portal editor),
// online uses the shared editorial server.
json communityEventData() {
    return config::onlineMode ? fetchLive("/gw2/live/communityevent", "community.json")
                              : content("community.json");
}

void grantItem(const std::string& key, int64_t qty = 1) {
    const std::string& k = key;
    if (k.rfind("Cus",0)==0 || k.rfind("stk",0)==0 || k.rfind("aschar",0)==0 || k.rfind("socha",0)==0 || k.rfind("ab",0)==0)
        data::addInventoryUnlock(k);
    else
        data::addInventoryItem(k, qty);
}

void pushInventoryNotif(std::shared_ptr<network::ClientConnection> client, const std::map<std::string,int64_t>& ilst) {
    auto notif = std::make_unique<blaze::Packet>(static_cast<blaze::ComponentId>(0x0803), 0x000B, blaze::MessageType::Notification, 0);
    notif->setPayload(blaze::TdfBuilder()
        .integer("FFCB", data::getInventoryQuantity("Coinz"))
        .integerMap("ILST", ilst)
        .integer("UID", config::blazeId)
        .build());
    client->sendPacket(std::move(notif));
}

blaze::TdfStruct buildBlackMarketData(int64_t blazeId, bool includeSlots) {
    (void)blazeId;
    const json bm = blackMarketData();
    std::vector<std::string> have, none, still;
    for (const auto& s : bm.value("haveItemsDialog", json::array()))
        have.push_back(s.get<std::string>());
    for (const auto& s : bm.value("noItemsDialog",   json::array()))
        none.push_back(s.get<std::string>());
    for (const auto& s : bm.value("stillDecidingDialog", json::array()))
        still.push_back(s.get<std::string>());
    if (still.empty())
        still.push_back("Having trouble making a decision?");

    int64_t sctu;
    if (bm.contains("stateChangeTimeUnix")) {
        sctu = bm.value("stateChangeTimeUnix", (int64_t)0);
    } else {
        const int64_t bmBase   = bm.value("rotationBaseUnix",    (int64_t)1782126000); // 2026-06-22 11:00 UTC
        const int64_t bmPeriod = bm.value("rotationPeriodSecs",  (int64_t)1209600);    // 14 days
        const int64_t now = blazeServerNow();
        sctu = bmBase + ((now - bmBase) / bmPeriod) * bmPeriod;
        while (sctu <= now)
            sctu += bmPeriod;
    }

    // DATA = BlackMarketData (Blaze::PvzGw::BlackMarketData)
    blaze::TdfBuilder b;
    // DATA = BlackMarketData — Rux's Black Market shop.
    b.beginStruct("DATA")
        .string("ACID", bm.value("acid", std::string("z7blackmarket")))   // activationId — Black Market activation/campaign id.
        .list("IVDI", blaze::TdfType::String, have)                                          // initialViewDialogue — Rux's dialogue shown on first view (items available).
        .list("NIDI", blaze::TdfType::String, none)                                          // noAvailableItemsDialogue — Dialogue shown when no items are available.
        .integer("SCTU", sctu);                                                              // stateChangeTimeUnix — Unix time of the next shop rotation.
    if (includeSlots) {
        b.beginList("SLTS");                                                                     // slots (BlackMarketSlotData) — The purchasable item slots.
        for (const auto& s : bm.value("slots", json::array())) {
            b.beginStruct()
                .string("DESC", s.value("desc", std::string("")))        // description — Item description.
                .string("ITID", s.value("itid", std::string("")))        // itemId — Catalog item id.
                .string("NAME", s.value("name", std::string("")))        // name — Item display name.
                .integer("PRIC", s.value("price", (int64_t)0))                          // price — Cost in Coinz.
                .integer("PURC", 0)                                                    // hasBeenPurchased — 1 if already bought.
                .string("SLID", s.value("slid", std::string("")))        // slotId — Slot id (used to purchase).
            .endStruct();
        }
        b.endList();
    }
    b.list("SVDI", blaze::TdfType::String, still)                                           // subsequentViewDialogue — Rux's dialogue on subsequent views.
     .integer("VIEW", 0)                                                               // hasBeenViewed — 1 if the market has been viewed.
    .endStruct();
    return b.build();
}

void addUserMessage(blaze::TdfBuilder& b, int64_t prio, int64_t mgid, int64_t time, const std::map<std::string, std::string>& attr) {
    b.beginStruct()
        .integer("IFMG", 0)                                             // isForcedMessage
        .integer("IPNM", 0)                                             // isPinnedMessage
        .integer("PRIO", prio)                                               // sortingPriority
        .beginStruct("SMSG")                                                     // serverMessage
            .integer("FLAG", 1)                                         // flags
            .integer("MGID", mgid)                                           // messageId
            .beginStruct("PYLD")                                                 // payload (ClientMessage)
                .intKeyStringMap("ATTR", attr)                               // attrMap
                .integer("FLAG", 9)                                     // flags
                .integer("STAT", 13)                                    // status
                .integer("TAG", 0)                                      // (anon)
                .intList("TIDS", { config::blazeId })               // targetIds
                .objectType("TTYP", 0x7802, 0x0001)            // targetType
                .integer("TYPE", 2)                                     // type
            .endStruct()
            .objectId("SRCE", 0x7802, 0x0001, config::blazeId) // source
            .integer("TIME", time)                                           // timestamp
            .beginStruct("USER")                                                 // sourceIdent (UserIdentification)
                .binary("EXBB", {})                                  // externalBlob
                .integer("EXID", config::nucleusId)                     // externalId
                .integer("ID", config::blazeId)                         // blazeId
                .string("NAME", config::persona)                        // name
                .string("NASP", config::nasp)                           // personaNamespace
            .endStruct()
        .endStruct()
    .endStruct();
}

} // namespace

PvzGwComponent::PvzGwComponent()
    : Component(static_cast<blaze::ComponentId>(0x0805), "PvzGwComponent")
{
}

std::unique_ptr<blaze::Packet> PvzGwComponent::handlePacket(const blaze::Packet& request, std::shared_ptr<network::ClientConnection> client) {
    uint16_t command = request.getCommand();

    switch (static_cast<blaze::PvzGwCommand>(command)) {
        // Blaze::PvzGw::CheckUserMessages (id=0x02): Returns whether the player has unread server messages.
        case blaze::PvzGwCommand::checkUserMessages:
            return handleCheckUserMessages(request, client);
        // Blaze::PvzGw::SetXPMultiplier (id=0x07): Sets the player's XP multiplier (event/debug).
        case blaze::PvzGwCommand::setXPMultiplier:
            return handleSetXPMultiplier(request, client);
        // Blaze::PvzGw::GetStoreItemList (id=0x04): Returns the in-game store catalog (buyable items).
        case blaze::PvzGwCommand::getStoreItemList:
            return handleGetStoreItemList(request, client);
        // Blaze::PvzGw::GetDailyQuests (id=0x09): Returns the player's daily quests.
        case blaze::PvzGwCommand::getDailyQuests:
            return handleGetDailyQuests(request, client);
        // Blaze::PvzGw::GetUserMessages (id=0x0E): Returns the player's server messages (news/mail).
        case blaze::PvzGwCommand::getUserMessages:
            return handleGetUserMessages(request, client);
        // Blaze::PvzGw::UpdateUserMessageStatus (id=0x10): Marks server messages as read/acknowledged.
        case blaze::PvzGwCommand::updateUserMessageStatus:
            return handleUpdateUserMessageStatus(request, client);
        // Blaze::PvzGw::GetClientSettings (id=0x11): Returns game-wide client settings / tuning values.
        case blaze::PvzGwCommand::getClientSettings:
            return handleGetClientSettings(request, client);
        // Blaze::PvzGw::GetCommunityAchievements (id=0x12): Returns community challenge progress (bronze/silver/gold tiers).
        case blaze::PvzGwCommand::getCommunityAchievements:
            return handleGetCommunityAchievements(request, client);
        // Blaze::PvzGw::ClaimCommunityEventReward (id=0x13): Claims the reward chest for a completed community event.
        case blaze::PvzGwCommand::claimCommunityEventReward:
            return handleClaimCommunityEventReward(request, client);
        // Blaze::PvzGw::GetBlackMarketData (id=0x14): Returns Rux's Black Market shop (rotating items).
        case blaze::PvzGwCommand::getBlackMarketData:
            return handleGetBlackMarketData(request, client);
        // Blaze::PvzGw::PurchaseBlackMarketItem (id=0x15): Purchases an item from the Black Market.
        case blaze::PvzGwCommand::purchaseBlackMarketItem:
            return handlePurchaseBlackMarketItem(request, client);
        // Blaze::PvzGw::SetBlackMarketViewed (id=0x16): Marks the Black Market as viewed.
        case blaze::PvzGwCommand::setBlackMarketViewed:
            return handleSetBlackMarketViewed(request, client);
        // Blaze::PvzGw::GetCommunityPortalData (id=0x17): Returns the Community Portal state (chests, prices, goals).
        case blaze::PvzGwCommand::getCommunityPortalData:
            return handleGetCommunityPortalData(request, client);
        // Blaze::PvzGw::OpenCommunityPortalChest (id=0x18): Opens a Community Portal mega-chest.
        case blaze::PvzGwCommand::openCommunityPortalChest:
            return handleOpenCommunityPortalChest(request, client);
        // Blaze::PvzGw::ForceClientNotification (id=0x19): Pushes a forced client notification (e.g. Black Market availability).
        case blaze::PvzGwCommand::forceClientNotification:
            return handleForceClientNotification(request, client);
        // Blaze::PvzGw::GetPlaylists (id=0x1E): Returns the multiplayer playlist definitions.
        case blaze::PvzGwCommand::getPlaylists:
            return handleGetPlaylists(request, client);
        // Blaze::PvzGw::GetPlaylistRotation (id=0x1F): Returns the current playlist map/mode rotation.
        case blaze::PvzGwCommand::getPlaylistRotation:
            return handleGetPlaylistRotation(request, client);
        // Blaze::PvzGw::GetLoyaltyChallengeData (id=0x3C): Returns the loyalty challenge data.
        case blaze::PvzGwCommand::getLoyaltyChallengeData:
            return handleGetLoyaltyChallengeData(request, client);
        // Blaze::PvzGw::GetPersistedLicenses (id=0x05): Returns the player's persisted content licenses (owned characters/variants).
        case blaze::PvzGwCommand::getPersistedLicenses:
            return handleGetPersistedLicenses(request, client);
        // Blaze::PvzGw::SetOnlineAccessEntitlements (id=0x0C): Sets the player's online-access entitlements.
        case blaze::PvzGwCommand::setOnlineAccessEntitlements:
            return handleSetOnlineAccessEntitlements(request, client);
        default:
            LOG_WARN("[PvzGw] Unknown command: 0x{:04X}", command);
            return request.createReply();
    }
}

std::unique_ptr<blaze::Packet> PvzGwComponent::handleGetUserMessages(const blaze::Packet& request, std::shared_ptr<network::ClientConnection> client) {
    LOG_INFO("[PvzGw] getUserMessages from {}", client->getRemoteAddress());

    blaze::TdfBuilder b;
    b.beginList("MSLT");                                                 // messages

    // [0] PvZ: Battle for Neighborville promotion (inbox message)
    addUserMessage(b, 7000, 102793108, 1693150944LL, {
        {"65280", "Z7 News Test!"},
        {"65282", R"(
A new battle is blooming – are you ready to take it on?

This used to be an advertisement for BFN, but now it can be whatever you want thanks to the Z7 emulator!)"},
        {"777777", utils::kEditorialBase + "/PlantsVsZombies/GW2/image/userinbox/promotion/PvZGW2_BFN_StandardEdition.jpg"},
        {"777780", "0"},
        {"777781", ""},
        {"777782", "_qPPhIUcQbkGYTf"},
    });

    // [1] Rux's black-market promotion (inbox message)
    addUserMessage(b, 3000, 172228458, 1778625531LL, {
        {"65280", "Psstt…glorious gadgets abound!"},
        {"65282", R"(
Hey-hey-heyo! Rolling through with the mad rad doodads.

Drop by and buy before I go bye bye and you cry!

See you in the Plant or Zombie base!

Tro-lo-lo!)"},
        {"777777", utils::kEditorialBase + "/PlantsVsZombies/GW2/image/userinbox/blackmarket/rux.jpg"},
        {"777780", "0"},
        {"777781", ""},
        {"777782", "_LHbVSdWOGkG-KW"},
    });

    b.endList();
    auto reply = request.createReply();
    reply->setPayload(b.build());
    return reply;
}

std::unique_ptr<blaze::Packet> PvzGwComponent::handleSetXPMultiplier(const blaze::Packet& request, std::shared_ptr<network::ClientConnection> client) {
    LOG_INFO("[PvzGw] setXPMultiplier from {}", client->getRemoteAddress());

    auto reply = request.createReply();
    reply->setPayload(blaze::TdfBuilder()
        .floatValue("LXPM", 1.0f)                       // lastXPMultiplier
        .floatValue("NXPM", 1.0f)                       // newXPMultiplier
        .build());
    return reply;
}

std::unique_ptr<blaze::Packet> PvzGwComponent::handleGetStoreItemList(const blaze::Packet& request, std::shared_ptr<network::ClientConnection> client) {
    LOG_INFO("[PvzGw] getStoreItemList from {}", client->getRemoteAddress());

    struct Pack {
        const char* stid;
        const char* title;
        const char* grant;
        const char* image;
        const char* hideOn;
        const char* showOn;
    };
    static const Pack kPacks[] = {
        {"2", "Handy Coins Pack",     "56000",   "UI/Assets/Screens/PurchaseCoins/CoinPack1", "",               ""},
        {"3", "Modest Coins Pack",    "120000",  "UI/Assets/Screens/PurchaseCoins/CoinPack1", "",               ""},
        {"4", "Incredi-coins Pack",   "280000",  "UI/Assets/Screens/PurchaseCoins/CoinPack2", "",               ""},
        {"5", "Ultimate Coins Pack",  "455000",  "UI/Assets/Screens/PurchaseCoins/CoinPack3", "canadanottrial", ""},
        {"6", "Epic Coins Pack",      "630000",  "UI/Assets/Screens/PurchaseCoins/CoinPack3", "",               ""},
        {"7", "",                     "1645000", "",                                          "",               ""},
        {"9", "Mega Coins Pack",      "1500000", "UI/Assets/Screens/PurchaseCoins/CoinPack4", "",               "45_coins_rel"},
        {"8", "Humongous Coins Pack", "2500000", "UI/Assets/Screens/PurchaseCoins/CoinPack5", "",               "65_coins_rel"},
        {"1", "Humongous Coins Pack", "2250000", "UI/Assets/Screens/PurchaseCoins/CoinPack5", "",               "65_coins_test"},
    };

    blaze::TdfBuilder b;
    b.beginList("SLST");                                                // storeItemList (StoreItem) — The buyable store catalog.
    for (const auto& p : kPacks) {
        b.beginStruct()
            .beginList("SIFL");                                         // customFields (StoreItemCustomField) — Per-item name/value fields.
        auto field = [&](const char* name, const char* data) {
            b.beginStruct()
                .string("CFDA", data)                                   // fieldData — Field value.
                .string("CFNA", name)                                   // fieldName — Field name (title, image, price, ...).
            .endStruct();
        };
        field("currencyCode",        "");
        field("categoryId",          "ingamecurrency");
        field("consumableGrantCount", p.grant);
        field("hideOnLicense",       p.hideOn);
        field("showOnLicense",       p.showOn);
        field("title",               p.title);
        field("tagStringId",         "");
        field("image",               p.image);
        b.endList()
            .string("STDI", p.stid)                          // storeItemDisplayId — Display id.
            .string("STID", p.stid)                          // storeItemId — Store catalog id.
        .endStruct();
    }
    b.endList();

    auto reply = request.createReply();
    reply->setPayload(b.build());
    return reply;
}

std::unique_ptr<blaze::Packet> PvzGwComponent::handleGetDailyQuests(const blaze::Packet& request, std::shared_ptr<network::ClientConnection> client) {
    LOG_INFO("[PvzGw] getDailyQuests from {}", client->getRemoteAddress());

    // Quest set + rotation window come from the editorial server (synchronized).
    const json dq = fetchLive("/gw2/live/dailyquests", nullptr);
    const int64_t now = blazeServerNow();
    std::vector<std::string> quests;
    for (const auto& q : dq.value("quests", json::array()))
        quests.push_back(q.get<std::string>());
    const int64_t actAt = dq.value("activationUnix", now);
    const int64_t expAt = dq.value("expiryUnix", now + 3600);

    auto reply = request.createReply();
    reply->setPayload(blaze::TdfBuilder()
        .int64("DQAT", actAt)                                     // activationTime (unix seconds)
        .int64("DQET", expAt)                                     // expiryTime
        .list("DQID", blaze::TdfType::String, quests)            // dailyQuests
        .int64("DQTE", expAt - now)                               // timeToExpiry (seconds until refill)
        .build());
    return reply;
}

std::unique_ptr<blaze::Packet> PvzGwComponent::handleGetPersistedLicenses(const blaze::Packet& request, std::shared_ptr<network::ClientConnection> client) {
    LOG_INFO("[PvzGw] getPersistedLicenses from {}", client->getRemoteAddress());

    std::vector<std::string> lics = data::getPersistedLicenses();

    auto grant = [&](const std::string& s) {
        if (!s.empty() && std::find(lics.begin(), lics.end(), s) == lics.end())
            lics.push_back(s);
    };

    //blackmarket killswitch
    const json& bm = content("blackmarket.json");
    for (const auto& l : bm.value("ownedLicenses", json::array()))
        grant(l.get<std::string>());

    // Killswitches from data/extra_licenses.json.
    for (const auto& l : content("extra_licenses.json").value("licenses", json::array()))
        grant(l.get<std::string>());

    auto reply = request.createReply();
    reply->setPayload(blaze::TdfBuilder()
        .list("LICS", blaze::TdfType::String, lics)                           // licenses
        .build());
    client->sendPacket(*reply);

    auto userSessions = blaze::ComponentRegistry::instance().getComponent(blaze::ComponentId::UserSessions);
    if (userSessions) {
        static_cast<UserSessions*>(userSessions.get())->pushUserSessionExtendedDataUpdate(client);
    }

    return nullptr;
}

std::unique_ptr<blaze::Packet> PvzGwComponent::handleSetOnlineAccessEntitlements(const blaze::Packet& request, std::shared_ptr<network::ClientConnection> client) {
    LOG_INFO("[PvzGw] setOnlineAccessEntitlements from {}", client->getRemoteAddress());

    auto reply = request.createReply();
    reply->setPayload(blaze::TdfBuilder()
        .boolean("ANEG", false)                                        // anyNewEntitlementsGranted
        .build());
    return reply;
}

std::unique_ptr<blaze::Packet> PvzGwComponent::handleGetClientSettings(const blaze::Packet& request, std::shared_ptr<network::ClientConnection> client) {
    auto requestTdf = request.getPayloadAsTdf();
    int64_t changelist = getIntField(requestTdf, "CHAN", 0);
    LOG_INFO("[PvzGw] getClientSettings from {} (CHAN={})",
             client->getRemoteAddress(), changelist);

    // note on killswitches, the killswitch licenses defined below don't enable the feature. The licenses below must be added to handleGetPersistedLicenses to enable the killswitched feature
    static const std::vector<std::pair<std::string, std::string>> kSettings = {
        {"Game.LogFileEnable", "true"},
        {"Online.LogLevel", "LogLevel_Debug"},
        {"Online.BlazeLogLevel", "3"},
        {"Online.TelemetryPinSettings.ServerAddress",   "https://pin-river.data.ea.com"},
        {"Online.TelemetryPinSettings.ServerPort",      "443"},
        {"Online.TelemetryPinSettings.Environment",     "prod"},
        {"Online.TelemetryPinSettings.TitleIdType",     "projectid"},
        {"Online.TelemetryPinSettings.TitleId",         "310695"},
        {"Online.TelemetryPinSettings.ReleaseType",     "prod"},
        {"Online.TelemetryPinSettings.EnableTelemetry", "true"},
        {"Online.TelemetryPinSettings.EnableEventType", "[('boot_start',0,true),('boot_end',1,true),('login',2,true),('logout',3,true),('error',4,false),('custom_error',5,false),('connection',6,true),('game_start',7,true),('game_end',8,true),('player_level',10,true),('milestone',11,true),('mp_match_info',12,true)]"},
        {"Online.BugSentrySettings.EnablePinTelemetry", "true"},
        {"Online.MandatedVersion.Required",             "110"},
        {"Online.MandatedVersion.ExactMatch",           "false"},
        {"Online.BugSentrySettings.EnableKillswitchForCrashDumps", "true"},
        {"Online.BugSentrySettings.EnableCrashDumps",  "true"},
        {"Online.ForceDisableTrial",                    "false"},
        {"Online.ForceEndTrial",                        "false"},
        {"Online.TrialComingSoon",                      "false"},
        {"Online.PurchaseCoinsButtonEnabled",           "true"},
        {"Online.PurchaseCoinsButtonEnabled",           "true"},
        {"Online.PurchaseCoinsButtonLicenseOverride",   "_ejEWJ6TYgkKX9T"},
        {"Network.MLUREnabled",                         "true"},
        {"PVZServerGame.KillSwitches.MenchiesChallengeLicense",         "menchiesenabled"},
        {"PVZServerGame.KillSwitches.BlackMarketGnomeLicense",          "_aLcHB0sy_kyJdb"},
        {"PVZServerGame.KillSwitches.PlantTimeTrialsLicense",           "ptimechallenge"},
        {"PVZServerGame.KillSwitches.ZombieTimeTrialsLicense",          "ztimechallenge"},
        {"PVZServerGame.KillSwitches.PlantGnomeTargetsMinigameLicense", "_aLcHB0sy_kyJdb"},
        {"PVZServerGame.KillSwitches.ZombieGnomeTargetsMinigameLicense","_aLcHB0sy_kyJdb"},
        {"PVZServerGame.KillSwitches.GnomeTargetsLeaderboardLicense",   "_aLcHB0sy_kyJdb"},
        {"PVZServerGame.KillSwitches.Halloween2016License",             "doom2016"},
        {"PVZServerGame.KillSwitches.Halloween2017License",             "doom2017"},
        {"PVZServerGame.KillSwitches.Festivus2016License",              "festive2016"},
        {"PVZServerGame.KillSwitches.Festivus2017License",              "festive2017"},
        {"PVZServerGame.KillSwitches.Springening2017License",           "springen2017"},
        {"PVZServerGame.KillSwitches.LuckOZombie2017License",           "lucko2017"},
        {"PVZServerGame.KillSwitches.FestivalWeek1",                    "2019festweek1"},
        {"PVZServerGame.KillSwitches.FestivalWeek2",                    "2019festweek2"},
        {"PVZServerGame.KillSwitches.FestivalWeek3",                    "2019festweek3"},
        {"PVZServerGame.KillSwitches.FestivalWeek4",                    "2019festweek4"},
        {"PVZServerGame.KillSwitches.FestivalWeek5",                    "2019festweek5"},
        {"PVZServerGame.KillSwitches.FestivalWeek6",                    "2019festweek6"},
        {"PVZServerGame.KillSwitches.FestivalWeek7",                    "2019festweek7"},
        {"PVZServerGame.KillSwitches.FestivalWeek8",                    "2019festweek8"},
        {"PVZServerGame.KillSwitches.UnderageLicense",                  "underagedisable"},
        {"PVZServerGame.KillSwitches.GdprStopProcessLicense",           "gdprdisable"},
        {"PVZServerGame.KillSwitches.MarketingOptOutLicense",           "marketdisable"},
        {"PVZServerGame.KillSwitches.UpsellDisable",                    "upselldisable"},
        {"PVZServerGame.KillSwitches.LoyaltyDisable",                   "loyaltydisable"},
        {"PVZServerGame.KillSwitches.AccessDisable",                    "accessdisable"},
        {"PVZServerGame.ConsumableLimits.RainbowStarLimit",             "50"},
        {"PVZServerGame.ConsumableConversionRates.StarToRainbowStar",   "7"},
        {"PVZServerGame.CommunityPortal.UnderworldMegaChestCost",       "15"},
        {"Online.TournamentSettings.TournamentGameRequestMaxRetry",     "1"},
        {"Online.TournamentSettings.TournamentGameRequestMaxTimeout",   "5"},
        {"Online.TournamentSettings.TournamentGlobalKillSwitch",        "false"},
    };

    blaze::TdfBuilder b;
    b.beginList("GENS");                                                                              // generalSettingsList
    for (auto& [iden, valu] : kSettings)
        b.beginStruct().string("IDEN", iden).string("VALU", valu).endStruct();                        // identifier / value
    b.endList()
     .beginStruct("PACF")                                                                             // patchSettings (ServerPatchSettings)
        .integer("CSUM", 0)                                                                           // checksum
        .string("PRSF", "")                                                                           // protocolSuffix
        .integer("PSTA", 0)                                                                           // status
        .string("PURL", "")                                                                           // (anon - patch URL)
     .endStruct();

    /*
    140e520e9            data_142c075d0 = "PatchStatusType_NotFound"
    140e520f0            data_142c075d8 = 0
    140e52101            data_142c075f0 = "PatchStatusType_ChangelistMismatch_Lower"
    140e52108            data_142c075f8 = 1
    140e52119            data_142c07610 = "PatchStatusType_ChangelistMismatch_Higher"
    140e52120            data_142c07618 = 2
    140e52131            data_142c07630 = "PatchStatusType_Required"
    140e52138            data_142c07638 = 3

     */

    auto reply = request.createReply();
    reply->setPayload(b.build());
    return reply;
}

std::unique_ptr<blaze::Packet> PvzGwComponent::handleGetCommunityAchievements(const blaze::Packet& request, std::shared_ptr<network::ClientConnection> client) {
    LOG_INFO("[PvzGw] getCommunityAchievements from {}", client->getRemoteAddress());

    // Offline mode (-disableCC): reply with no payload so the client sees no challenge.
    if (config::disableCommunityChallenge) {
        LOG_INFO("[PvzGw] community challenge disabled -> empty reply");
        return request.createReply();
    }

    const json c = fetchLive("/gw2/live/communitychallenge?user=" + std::to_string(config::blazeId), nullptr);

    auto reply = request.createReply();
    reply->setPayload(blaze::TdfBuilder()
        // ACDA = PVZGWAchivementData — the community challenge (bronze/silver/gold community goal).
        .beginStruct("ACDA")
            .string ("AHID", c.value("achievementId", std::string("z7cc1")))       // achievementId — Community challenge id.
            .string ("NAME", c.value("name", std::string("Elemental Clash Community Challenge"))) // name — Challenge display name.
            .string ("DESC", c.value("desc", std::string("Select your element and get involved!"))) // description — Challenge description.
            .string ("IMG",  c.value("image", std::string("")))                    // image — Challenge banner image url.
            .string ("PEHD", c.value("personalHeader", std::string("Your Vanquishes"))) // personalHeader — Header for the player's contribution.
            .string ("REHD", c.value("rewardHeader", std::string("Community Goals:"))) // rewardHeader — Header for the community goals.
            .integer("CHAC", c.value("challengeActive",    (int64_t)1))            // challengeActive — 1 if the challenge is active.
            .integer("COAC", c.value("contributionActive", (int64_t)1))            // contributionActive — 1 if contributions are accepted.
            .integer("PTS",  c.value("communityProgress",  (int64_t)0))            // communityProgress — Total community progress so far.
            .integer("BRTD", c.value("bronzeThreshold", (int64_t)15000000))        // bronzeThreshold — Community score for the bronze goal.
            .integer("SLTD", c.value("silverThreshold", (int64_t)30000000))        // silverThreshold — Community score for the silver goal.
            .integer("GDTD", c.value("goldThreshold",   (int64_t)50000000))        // goldThreshold — Community score for the gold goal.
            .string ("BHOW", c.value("bronzeReward", std::string("Wondrous Pack of Greatness"))) // bronzeReward — Bronze-tier reward description.
            .string ("SHOW", c.value("silverReward", std::string("Infinity Pack"))) // silverReward — Silver-tier reward description.
            .string ("GHOW", c.value("goldReward",   std::string("Legendary Item"))) // goldReward — Gold-tier reward description.
            .integer("BRCL", c.value("bronzeCollected", (int64_t)0))               // bronzeRewardCollected — 1 if the bronze reward was collected.
            .integer("SLCL", c.value("silverCollected", (int64_t)0))               // silverRewardCollected — 1 if the silver reward was collected.
            .integer("GOCL", c.value("goldCollected",   (int64_t)0))               // goldRewardCollected — 1 if the gold reward was collected.
            .integer("URTD", c.value("userThreshold", (int64_t)1000))              // userThreshold — Personal contribution needed to qualify.
            .integer("USPS", c.value("userProgress",  (int64_t)0))                 // userProgress — The player's personal contribution.
            .integer("SCFS", c.value("secondsFromStart",         (int64_t)0))      // secondsFromStart — Seconds elapsed since the challenge started.
            .integer("NEST", c.value("secondsToNextEvent",       (int64_t)0))      // secondsToNextEvent — Seconds until the next event.
            .integer("STCE", c.value("secondsToCollectionExpiry",(int64_t)(7*86400))) // secondsToCollectionExpiry — Seconds until collection expires.
            .integer("STRE", c.value("secondsToRewardExpiry",    (int64_t)(7*86400))) // secondsToRewardExpiry — Seconds until rewards expire.
            .integer("RRSC", c.value("refreshRateSeconds",       (int64_t)60))     // refreshRateSeconds — Client refresh interval.
            .integer("PCSC", c.value("proximityCooldownSeconds", (int64_t)30))     // proximityCooldownSeconds — Proximity interaction cooldown.
        .endStruct()
        .build());
    return reply;
}

std::unique_ptr<blaze::Packet> PvzGwComponent::handleClaimCommunityEventReward(const blaze::Packet& request, std::shared_ptr<network::ClientConnection> client) {
    const int64_t tier = getIntField(request.getPayloadAsTdf(), "TIER", 0);
    LOG_INFO("[PvzGw] claimCommunityEventReward from {} (TIER={})", client->getRemoteAddress(), tier);

    const json& ev = content("community.json");
    const std::string pk = ev.value("eventReward", std::string("dynpk272"));
    auto loot = data::rollChest();

    std::map<std::string, int64_t> ilst;
    if (loot.valid) {
        for (const auto& [ais, qty] : loot.consumables) { data::addInventoryItem(ais, qty); ilst[ais] += qty; }
        for (const auto& key : loot.unlocks)            { data::addInventoryUnlock(key);   ilst[key] = 1;    }
        data::saveInventory();
        LOG_INFO("[PvzGw] event reward chest (tier {}): {} consumables, {} unlocks",
                 tier, loot.consumables.size(), loot.unlocks.size());
    }
    if (!ilst.empty())
        pushInventoryNotif(client, ilst);

    std::map<std::string, int64_t> cnsm(loot.consumables.begin(), loot.consumables.end());
    const std::string aulk = loot.unlocks.empty() ? std::string() : loot.unlocks.front();
    static int64_t s_evInstance = 1;
    const int64_t now = blazeServerNow() * 1000000LL;

    auto reply = request.createReply();
    reply->setPayload(blaze::TdfBuilder()
        .boolean("CCSC", true)                          // success
        .beginList("GOPR")                              // openPackResponseList
            .beginStruct()                              // Packs::OpenPackResponse
                .string ("AULK", aulk)                  // additionalUnlocks
                .integerMap("CNSM", cnsm)               // consumables granted
                .string ("LOUT", "")                    // logOutput
                .beginStruct("PACK")
                    .string ("ADDT", "")
                    .integer("AUDL", -1)
                    .integer("COST", 0)                 // free reward
                    .string ("DESC", "")
                    .string ("GKEY", "")
                    .string ("IMGN", "")
                    .list   ("ITLI", blaze::TdfType::String, loot.itli)   // rolled cards (reveal order)
                    .integer("PID", 647607740 + s_evInstance++)
                    .string ("PKEY", pk)
                    .string ("PUDS", "")
                    .integer("SCAT", 2)
                    .integer("TGEN", now)
                    .string ("TITL", "")
                    .integer("TVAL", now)
                    .list   ("TYPE", blaze::TdfType::String, { "CardPackType_CommunityEventReward" })
                    .integer("UID", config::blazeId)
                .endStruct()
            .endStruct()
        .endList()
        .build());
    return reply;
}

std::unique_ptr<blaze::Packet> PvzGwComponent::handleGetBlackMarketData(const blaze::Packet& request,std::shared_ptr<network::ClientConnection> client) {
    auto requestTdf = request.getPayloadAsTdf();
    int64_t blazeId = getIntField(requestTdf, "BLID", client->getUserId());
    int64_t includePostInteractionData = getIntField(requestTdf, "IPID", 0);
    LOG_INFO("[PvzGw] getBlackMarketData from {} (BLID={}, IPID={})", client->getRemoteAddress(), blazeId, includePostInteractionData);

    auto reply = request.createReply();
    reply->setPayload(buildBlackMarketData(blazeId, includePostInteractionData != 0));
    return reply;
}

std::unique_ptr<blaze::Packet> PvzGwComponent::handlePurchaseBlackMarketItem(const blaze::Packet& request,std::shared_ptr<network::ClientConnection> client) {
    auto requestTdf = request.getPayloadAsTdf();
    std::string slid = getStrField(requestTdf, "SLID");
    LOG_INFO("[PvzGw] purchaseBlackMarketItem from {} (SLID={})", client->getRemoteAddress(), slid);

    const json bm = blackMarketData();
    int64_t price = 0;
    std::string itid, kind;
    bool found = false;
    for (const auto& s : bm.value("slots", json::array())) {
        if (s.value("slid", std::string("")) == slid) {
            price = s.value("price", (int64_t)0);
            itid  = s.value("itid", std::string(""));
            kind  = s.value("kind", std::string(""));
            found = true;
            break;
        }
    }
    if (!found) {
        return request.createReply();  // unknown slot -> ack, no grant
    }
    if (data::getInventoryQuantity("Coinz") < price) {
        LOG_WARN("[PvzGw] black market: insufficient funds for {} ({} coins)", itid, price);
        return request.createErrorReply(blaze::BlazeError::ERR_INSUFFICIENT_FUNDS);
    }

    const bool consumable = !kind.empty()
        ? (kind == "consumable")
        : !(itid.rfind("Cus",0)==0 || itid.rfind("stk",0)==0 || itid.rfind("aschar",0)==0
            || itid.rfind("socha",0)==0 || itid.rfind("ab",0)==0 || itid.rfind("piab",0)==0
            || itid.rfind("ziab",0)==0);

    int64_t balance = data::addInventoryItem("Coinz", -price);
    if (consumable) data::addInventoryItem(itid, 1);
    else            data::addInventoryUnlock(itid);
    data::saveInventory();
    LOG_INFO("[PvzGw] black market: bought {} ({}) for {} -> Coinz {}",
             itid, kind.empty() ? (consumable ? "consumable" : "unlock") : kind, price, balance);

    // HUD update
    pushInventoryNotif(client, { {itid, 1} });


    std::map<std::string, int64_t> cnsm;
    if (consumable) cnsm[itid] = 1;   // consumables reveal via CNSM too

    static int64_t s_bmInstance = 1;
    const int64_t now = blazeServerNow() * 1000000LL;

    blaze::TdfBuilder b;
    b.beginList("OPRL")
        .beginStruct()
            .integer("FFCB", balance)                       // finalCoinBalance
            .string("FFCT", "Coinz")                        // finalCoinType
            .beginStruct("ORSP")
                .integerMap("CNSM", cnsm)                   // consumables granted
                .beginStruct("PACK")
                    .string("ADDT", "")
                    .integer("AUDL", 65)
                    .integer("COST", price)
                    .string("DESC", "")
                    .string("GKEY", "")
                    .string("IMGN", "")
                    .list("ITLI", blaze::TdfType::String, { itid })   // items granted
                    .integer("PID", 647607740 + s_bmInstance++)
                    .string("PKEY", "z7blackmarket")
                    .string("PUDS", "")
                    .integer("SCAT", 2)
                    .integer("TGEN", now)
                    .string("TITL", "")
                    .integer("TVAL", now)
                    .list("TYPE", blaze::TdfType::String, { "CardPackType_Regular" })
                    .integer("UID", config::blazeId)
                .endStruct()
            .endStruct()
        .endStruct()
    .endList()
    .integer("SUCC", 1);                                    // success

    auto reply = request.createReply();
    reply->setPayload(b.build());
    return reply;
}

std::unique_ptr<blaze::Packet> PvzGwComponent::handleSetBlackMarketViewed(const blaze::Packet& request, std::shared_ptr<network::ClientConnection> client) {
    LOG_INFO("[PvzGw] setBlackMarketViewed from {}", client->getRemoteAddress());

    auto reply = request.createReply();
    reply->setPayload(blaze::TdfBuilder().integer("SUCC", 1).build());  // success
    return reply;
}

std::unique_ptr<blaze::Packet> PvzGwComponent::handleGetCommunityPortalData(const blaze::Packet& request, std::shared_ptr<network::ClientConnection> client) {
    LOG_INFO("[PvzGw] getCommunityPortalData from {}", client->getRemoteAddress());

    // Offline mode (-disableCP): reply with no payload so the client sees no portal.
    if (config::disableCommunityPortal) {
        LOG_INFO("[PvzGw] community portal disabled -> empty reply");
        return request.createReply();
    }

    const json ev = communityEventData();
    bool feat = ev.value("featured", false);
    int64_t now = blazeServerNow();
    int64_t st  = ev.contains("startUnix") ? ev.value("startUnix", (int64_t)0)
                                           : now + (int64_t)(ev.value("startOffsetDays", -1.0) * 86400.0);
    int64_t et  = ev.contains("endUnix")   ? ev.value("endUnix",   (int64_t)0)
                                           : now + (int64_t)(ev.value("endOffsetDays",   14.0) * 86400.0);
    int64_t gpe = ev.contains("grandPrizeEndUnix") ? ev.value("grandPrizeEndUnix", (int64_t)0)
                                           : now + (int64_t)(ev.value("grandPrizeOffsetDays", 14.0) * 86400.0);
    int64_t flag = feat ? -1 : 65;   // crazyOption slots: -1 active, 65 idle

    auto reply = request.createReply();
    reply->setPayload(blaze::TdfBuilder()
        // CPDA = CommunityPortalData — the Community Portal event state.
        .beginStruct("CPDA")
            .integer("COFI", flag).integer("COFO", flag).integer("COOE", flag)   // crazyOption5/4/1 — Crazy Dave option slot state (-1 active / 65 idle).
            .integer("COTH", flag).integer("COTW", flag).integer("COZE", flag)   // crazyOption3/2/0 — Crazy Dave option slot state.
            .string ("DESC", ev.value("description", std::string("")))      // eventDesc — Community event description.
            .integer("EVAC", feat ? 1 : 0)                                                     // eventActive — 1 if the event is active.
            .integer("EVET", et)                                                               // eventEndTime — Event end time (unix).
            .string ("EVID", ev.value("eventId", std::string("")))          // eventId — Event id (e.g. "CP215").
            .integer("EVST", st)                                                               // eventStartTime — Event start time (unix).
            .integer("FCPR", ev.value("firstChestScore",  (int64_t)10))                    // firstChestPrice — Community score required for the first chest.
            .integer("FRCH", 0)                                                           // firstChestOpened — 1 if the first chest has been opened.
            .integer("GPAC", feat ? 1 : 0)                                                     // gracePeriodActive — 1 if the grand-prize grace period is active.
            .integer("GPET", gpe)                                                              // gracePeriodEndTime — Grace period end time (unix).
            .string ("IMUL", ev.value("imageUrl", std::string("")))         // imageUrl — Event banner image URL.
            .string ("NAME", ev.value("name", std::string("")))             // eventName — Event display name.
            .integer("PCSC", 15).integer("RRSC", 30)                          // proximityCooldownSeconds / refreshRateSeconds.
            .integer("SCCH", 0)                                                           // secondChestOpened — 1 if the second chest has been opened.
            .integer("SCPR", ev.value("secondChestScore", (int64_t)30))                    // secondChestPrice — Community score required for the second chest.
            .integer("TCPR", ev.value("thirdChestScore",  (int64_t)50))                    // thirdChestPrice — Community score required for the third chest.
            .integer("THCD", 0)                                                           // thirdChestOpened — 1 if the third chest has been opened.
        .endStruct()
        .integer("FOEV", feat ? 1 : 0)                                                         // foundEvent — 1 if an active event was found.
        .build());
    return reply;
}

std::unique_ptr<blaze::Packet> PvzGwComponent::handleOpenCommunityPortalChest(const blaze::Packet& request, std::shared_ptr<network::ClientConnection> client) {
    const int64_t chin = getIntField(request.getPayloadAsTdf(), "CHIN", 0);   // chest index 0/1/2
    LOG_INFO("[PvzGw] openCommunityPortalChest from {} (CHIN={})", client->getRemoteAddress(), chin);

    const json& ev = content("community.json");
    const json chests = ev.value("portalChests", json::array());
    json chest = (chin >= 0 && chin < (int64_t)chests.size()) ? chests[chin] : json::object();
    const std::string pkey  = chest.value("pkey",  std::string("dynpk2932"));
    const std::string gkey  = chest.value("gkey",  std::string("pk_evnt001_brnz"));
    const std::string title = chest.value("title", std::string("Mystery Portal Pack"));
    const std::string desc  = chest.value("desc",  std::string("Contains one awesome Mystery Portal item!"));

    data::addGiftPack(pkey);
    data::saveInventory();
    LOG_INFO("[PvzGw] portal chest {} awarded gift pack '{}' ({})", chin, pkey, title);

    static int64_t s_cpInstance = 1;
    const int64_t balance = data::getInventoryQuantity("RainbowStar");

    // POPR = list of { FFCB, FFCT, ORSP:{ PACK } } (one per awarded pack).
    auto reply = request.createReply();
    reply->setPayload(blaze::TdfBuilder()
        .beginList("POPR")
            .beginStruct()
                .integer("FFCB", balance)                   // finalCoinBalance (RainbowStar)
                .string ("FFCT", "RainbowStar")             // currency type
                .beginStruct("ORSP")                        // Packs::OpenPackResponse
                    .beginStruct("PACK")
                        .string ("ADDT", "")
                        .integer("AUDL", -1)
                        .integer("COST", 0)
                        .string ("DESC", desc)
                        .string ("GKEY", gkey)              // gift key (identifies the pack in the store)
                        .string ("IMGN", "")
                        .integer("PID", 95465292 + s_cpInstance++)
                        .string ("PKEY", pkey)              // the pack awarded
                        .string ("PUDS", "")
                        .integer("SCAT", 2)
                        .integer("TGEN", 0)
                        .string ("TITL", title)             // "Yellow/Blue/... Mystery Portal Pack"
                        .integer("TVAL", 0)
                        .list   ("TYPE", blaze::TdfType::String, { "CardPackType_CommunityPortalReward" })
                        .integer("UID", 0)
                    .endStruct()
                .endStruct()
            .endStruct()
        .endList()
        .build());
    return reply;
}

std::unique_ptr<blaze::Packet> PvzGwComponent::handleGetPlaylists(const blaze::Packet& request, std::shared_ptr<network::ClientConnection> client) {
    LOG_INFO("[PvzGw] getPlaylists from {}", client->getRemoteAddress());

    const json& pj = content("playlists.json");
    blaze::TdfBuilder b;
    b.beginList("PL");                                                                        // sections
    for (const auto& sec : pj.value("sections", json::array())) {
        b.beginStruct()
            .string("ID", sec.value("id", std::string("")))            // section id
            .beginList("PL");                                                                 // playlists
        for (const auto& pl : sec.value("playlists", json::array())) {
            b.beginStruct()
                .string("DESC", pl.value("desc", std::string("")))     // description
                .string("ID",   pl.value("id", std::string("")))       // playlist id
                .string("LOAD", pl.value("load", std::string("")))     // loadScreenOverride
                .string("NAME", pl.value("name", std::string("")));    // displayName
            std::vector<std::string> psc;
            for (const auto& s : pl.value("scenarios", json::array()))
                psc.push_back(s.get<std::string>());
            if (!psc.empty()) b.list("SCEN", blaze::TdfType::String, psc);                // scenarioOverrides
            b.endStruct();
        }
        b.endList();
        std::vector<std::string> ssc;
        for (const auto& s : sec.value("scenarios", json::array()))
            ssc.push_back(s.get<std::string>());
        b.list("SCEN", blaze::TdfType::String, ssc)                                      // section scenarios
        .endStruct();
    }
    b.endList();

    auto reply = request.createReply();
    reply->setPayload(b.build());
    return reply;
}

std::unique_ptr<blaze::Packet> PvzGwComponent::handleGetPlaylistRotation(const blaze::Packet& request, std::shared_ptr<network::ClientConnection> client) {
    LOG_INFO("[PvzGw] getPlaylistRotation from {}", client->getRemoteAddress());

    auto reply = request.createReply();
    reply->setPayload(blaze::TdfBuilder()
        .list("ROTA", blaze::TdfType::Struct, {})                       // empty
        .build());
    return reply;
}

std::unique_ptr<blaze::Packet> PvzGwComponent::handleGetLoyaltyChallengeData(const blaze::Packet& request, std::shared_ptr<network::ClientConnection> client) {
    LOG_INFO("[PvzGw] getLoyaltyChallengeData from {}", client->getRemoteAddress());
    return request.createReply();
}

std::unique_ptr<blaze::Packet> PvzGwComponent::handleCheckUserMessages(const blaze::Packet& request, std::shared_ptr<network::ClientConnection> client) {
    LOG_INFO("[PvzGw] checkUserMessages from {}", client->getRemoteAddress());

    auto reply = request.createReply();
    reply->setPayload(blaze::TdfBuilder()
        .integer("PEID", 0)                                            // result
        .build());
    return reply;
}

std::unique_ptr<blaze::Packet> PvzGwComponent::handleUpdateUserMessageStatus(const blaze::Packet& request, std::shared_ptr<network::ClientConnection> client) {
    auto requestTdf = request.getPayloadAsTdf();
    LOG_INFO("[PvzGw] updateUserMessageStatus from {} (MID={})",
             client->getRemoteAddress(),
             getIntField(requestTdf, "MID", 0));
    return request.createReply();
}

std::unique_ptr<blaze::Packet> PvzGwComponent::handleForceClientNotification(const blaze::Packet& request, std::shared_ptr<network::ClientConnection> client) {
    auto requestTdf = request.getPayloadAsTdf();
    int64_t blazeId = getIntField(requestTdf, "BLID", 0);
    int64_t type = getIntField(requestTdf, "TYPE", 0);
    LOG_INFO("[PvzGw] forceClientNotification from {} (BLID={}, TYPE={})",
             client->getRemoteAddress(), blazeId, type);

    // TYPE 1002 = ClientNotifierMessageType_BlackMarketAvailability
    if (type == 1002) {
        const json& bm = content("blackmarket.json");
        const int64_t base   = bm.value("rotationBaseUnix",   (int64_t)1782126000);
        const int64_t period = bm.value("rotationPeriodSecs", (int64_t)1209600);
        int64_t now = blazeServerNow();
        int64_t end = base + ((now - base) / period) * period;
        while (end <= now) end += period;
        int64_t start = end - period;

        std::map<std::string, std::string> attr = {
            {"888002", bm.value("availabilityToken", std::string("hRZI7QNrRpci8YfCCjdkocFwKI5FcEiXRPi$MAG531"))},
            {"888003", std::to_string(end)},     // rotation end (SCTU)
            {"888004", "0"},
            {"888005", "10"},
            {"888006", std::to_string(start)},   // rotation start
        };

        // ServerMessage (Blaze::Messaging::ServerMessage); PYLD = ClientMessage.
        blaze::TdfStruct payload = blaze::TdfBuilder()
            .integer("FLAG", 0)                                         // flags
            .integer("MGID", 174088773)                                 // messageId
            .beginStruct("PYLD")                                                 // payload
                .intKeyStringMap("ATTR", attr)                               // attrMap
                .integer("FLAG", 0)                                     // flags
                .integer("STAT", 0)                                     // status
                .integer("TAG", 0)
                .intList("TIDS", { config::blazeId })               // targetIds
                .objectType("TTYP", 0x7802, 0x0001)            // targetType
                .integer("TYPE", 1002)                                  // BlackMarketAvailability
            .endStruct()
            .objectId("SRCE", 0, 0, 0)                      // source
            .integer("TIME", now)                                            // timestamp
            .beginStruct("USER")                                                 // sourceIdent (empty)
                .binary("EXBB", {})
                .integer("EXID", 0)
                .integer("ID", 0)
                .string("NAME", "")
                .string("NASP", "")
            .endStruct()
            .build();

        auto notif = std::make_unique<blaze::Packet>(
            static_cast<blaze::ComponentId>(0x000f), 0x0001,
            blaze::MessageType::Notification, m_nextNotifMsgNum++);
        notif->setPayload(payload);
        client->sendPacket(std::move(notif));
        LOG_INFO("[PvzGw] pushed BlackMarketAvailability notif (window {}..{})", start, end);
    }

    return request.createReply();
}

} // namespace gw2::components
