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
    const json bm = fetchLive("/gw2/live/blackmarket", "blackmarket.json");
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
    b.beginStruct("DATA")
        .string("ACID", bm.value("acid", std::string("z7blackmarket")))   // activationId
        .list("IVDI", blaze::TdfType::String, have)                                          // initialViewDialogue
        .list("NIDI", blaze::TdfType::String, none)                                          // noAvailableItemsDialogue
        .integer("SCTU", sctu);                                                              // stateChangeTimeUnix (future)
    if (includeSlots) {
        b.beginList("SLTS");                                                                     // slots (BlackMarketSlotData)
        for (const auto& s : bm.value("slots", json::array())) {
            b.beginStruct()
                .string("DESC", s.value("desc", std::string("")))        // description
                .string("ITID", s.value("itid", std::string("")))        // itemId
                .string("NAME", s.value("name", std::string("")))        // name
                .integer("PRIC", s.value("price", (int64_t)0))                          // price
                .integer("PURC", 0)                                                    // hasBeenPurchased
                .string("SLID", s.value("slid", std::string("")))        // slotId
            .endStruct();
        }
        b.endList();
    }
    b.list("SVDI", blaze::TdfType::String, still)                                           // subsequentViewDialogue
     .integer("VIEW", 0)                                                               // hasBeenViewed
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
        case blaze::PvzGwCommand::checkUserMessages:
            return handleCheckUserMessages(request, client);
        case blaze::PvzGwCommand::setXPMultiplier:
            return handleSetXPMultiplier(request, client);
        case blaze::PvzGwCommand::getStoreItemList:
            return handleGetStoreItemList(request, client);
        case blaze::PvzGwCommand::getDailyQuests:
            return handleGetDailyQuests(request, client);
        case blaze::PvzGwCommand::getUserMessages:
            return handleGetUserMessages(request, client);
        case blaze::PvzGwCommand::updateUserMessageStatus:
            return handleUpdateUserMessageStatus(request, client);
        case blaze::PvzGwCommand::getClientSettings:
            return handleGetClientSettings(request, client);
        case blaze::PvzGwCommand::getCommunityAchievements:
            return handleGetCommunityAchievements(request, client);
        case blaze::PvzGwCommand::claimCommunityEventReward:
            return handleClaimCommunityEventReward(request, client);
        case blaze::PvzGwCommand::getBlackMarketData:
            return handleGetBlackMarketData(request, client);
        case blaze::PvzGwCommand::purchaseBlackMarketItem:
            return handlePurchaseBlackMarketItem(request, client);
        case blaze::PvzGwCommand::setBlackMarketViewed:
            return handleSetBlackMarketViewed(request, client);
        case blaze::PvzGwCommand::getCommunityPortalData:
            return handleGetCommunityPortalData(request, client);
        case blaze::PvzGwCommand::openCommunityPortalChest:
            return handleOpenCommunityPortalChest(request, client);
        case blaze::PvzGwCommand::forceClientNotification:
            return handleForceClientNotification(request, client);
        case blaze::PvzGwCommand::getPlaylists:
            return handleGetPlaylists(request, client);
        case blaze::PvzGwCommand::getPlaylistRotation:
            return handleGetPlaylistRotation(request, client);
        case blaze::PvzGwCommand::getLoyaltyChallengeData:
            return handleGetLoyaltyChallengeData(request, client);
        case blaze::PvzGwCommand::getPersistedLicenses:
            return handleGetPersistedLicenses(request, client);
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
    b.beginList("SLST");                                                // storeItemList (StoreItem)
    for (const auto& p : kPacks) {
        b.beginStruct()
            .beginList("SIFL");                                         // customFields (StoreItemCustomField{CFNA name, CFDA data})
        auto field = [&](const char* name, const char* data) {
            b.beginStruct()
                .string("CFDA", data)
                .string("CFNA", name)
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
            .string("STDI", p.stid)                          // storeItemDisplayId
            .string("STID", p.stid)                          // storeItemId
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

    // Just a note on killswitches, the killswitch licenses defined below don't enable the feature. The licenses below must be added to handleGetPersistedLicenses to enable the killswitched feature
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
        b.beginStruct().string("IDEN", iden).string("VALU", valu).endStruct();   // identifier / value
    b.endList()
     .beginStruct("PACF")                                                                             // patchSettings (ServerPatchSettings)
        .integer("CSUM", 0)                                                                  // checksum
        .string("PRSF", "")                                                               // protocolSuffix
        .integer("PSTA", 0)                                                                  // status
        .string("PURL", "")                                                               // (anon - patch URL)
     .endStruct();

    auto reply = request.createReply();
    reply->setPayload(b.build());
    return reply;
}

std::unique_ptr<blaze::Packet> PvzGwComponent::handleGetCommunityAchievements(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    LOG_INFO("[PvzGw] getCommunityAchievements from {}", client->getRemoteAddress());

    // Offline mode (-disableCC): reply with no payload so the client sees no challenge.
    if (config::disableCommunityChallenge) {
        LOG_INFO("[PvzGw] community challenge disabled -> empty reply");
        return request.createReply();
    }

    const json c = fetchLive("/gw2/live/communitychallenge?user=" + std::to_string(config::blazeId), nullptr);

    auto reply = request.createReply();
    reply->setPayload(blaze::TdfBuilder()
        .beginStruct("ACDA")
            .string ("AHID", c.value("achievementId", std::string("z7cc1")))       // achievementId
            .string ("NAME", c.value("name", std::string("Elemental Clash Community Challenge")))
            .string ("DESC", c.value("desc", std::string("Select your element and get involved!")))
            .string ("IMG",  c.value("image", std::string("")))                    // image url
            .string ("PEHD", c.value("personalHeader", std::string("Your Vanquishes")))
            .string ("REHD", c.value("rewardHeader", std::string("Community Goals:")))
            .integer("CHAC", c.value("challengeActive",    (int64_t)1))            // challengeActive
            .integer("COAC", c.value("contributionActive", (int64_t)1))            // contributionActive
            .integer("PTS",  c.value("communityProgress",  (int64_t)0))            // communityProgress
            .integer("BRTD", c.value("bronzeThreshold", (int64_t)15000000))        // bronzeThreshold
            .integer("SLTD", c.value("silverThreshold", (int64_t)30000000))        // silverThreshold
            .integer("GDTD", c.value("goldThreshold",   (int64_t)50000000))        // goldThreshold
            .string ("BHOW", c.value("bronzeReward", std::string("Wondrous Pack of Greatness")))
            .string ("SHOW", c.value("silverReward", std::string("Infinity Pack")))
            .string ("GHOW", c.value("goldReward",   std::string("Legendary Item")))
            .integer("BRCL", c.value("bronzeCollected", (int64_t)0))               // bronzeRewardCollected
            .integer("SLCL", c.value("silverCollected", (int64_t)0))               // silverRewardCollected
            .integer("GOCL", c.value("goldCollected",   (int64_t)0))               // goldRewardCollected
            .integer("URTD", c.value("userThreshold", (int64_t)1000))              // userThreshold
            .integer("USPS", c.value("userProgress",  (int64_t)0))                 // userProgress
            .integer("SCFS", c.value("secondsFromStart",         (int64_t)0))      // secondsFromStart
            .integer("NEST", c.value("secondsToNextEvent",       (int64_t)0))      // secondsToNextEvent
            .integer("STCE", c.value("secondsToCollectionExpiry",(int64_t)(7*86400))) // secondsToCollectionExpiry
            .integer("STRE", c.value("secondsToRewardExpiry",    (int64_t)(7*86400))) // secondsToRewardExpiry
            .integer("RRSC", c.value("refreshRateSeconds",       (int64_t)60))     // refreshRateSeconds
            .integer("PCSC", c.value("proximityCooldownSeconds", (int64_t)30))     // proximityCooldownSeconds
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

    const json bm = fetchLive("/gw2/live/blackmarket", "blackmarket.json");
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

    const json ev = fetchLive("/gw2/live/communityevent", "community.json");
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
        .beginStruct("CPDA")                                                                       // communityPortalData
            .integer("COFI", flag).integer("COFO", flag).integer("COOE", flag)   // crazyOption5/4/1
            .integer("COTH", flag).integer("COTW", flag).integer("COZE", flag)   // crazyOption3/2/0
            .string ("DESC", ev.value("description", std::string("")))      // eventDesc
            .integer("EVAC", feat ? 1 : 0)                                                     // eventActive
            .integer("EVET", et)                                                               // eventEndTime
            .string ("EVID", ev.value("eventId", std::string("")))          // eventId (e.g. "CP215")
            .integer("EVST", st)                                                               // eventStartTime
            .integer("FCPR", ev.value("firstChestScore",  (int64_t)10))                    // firstChestPrice
            .integer("FRCH", 0)                                                           // firstChestOpened
            .integer("GPAC", feat ? 1 : 0)                                                     // gracePeriodActive
            .integer("GPET", gpe)                                                              // gracePeriodEndTime
            .string ("IMUL", ev.value("imageUrl", std::string("")))         // imageUrl
            .string ("NAME", ev.value("name", std::string("")))             // eventName
            .integer("PCSC", 15).integer("RRSC", 30)                          // proximityCooldownSeconds / refreshRateSeconds
            .integer("SCCH", 0)                                                           // secondChestOpened
            .integer("SCPR", ev.value("secondChestScore", (int64_t)30))                    // secondChestPrice
            .integer("TCPR", ev.value("thirdChestScore",  (int64_t)50))                    // thirdChestPrice
            .integer("THCD", 0)                                                           // thirdChestOpened
        .endStruct()
        .integer("FOEV", feat ? 1 : 0)                                                         // foundEvent
        .build());
    return reply;
}

std::unique_ptr<blaze::Packet> PvzGwComponent::handleOpenCommunityPortalChest(const blaze::Packet& request, std::shared_ptr<network::ClientConnection> client) {
    LOG_INFO("[PvzGw] openCommunityPortalChest from {}", client->getRemoteAddress());

    // roll a pack and push the inventory update
    const json& ev = content("community.json");
    std::string pk = ev.value("chestReward", std::string(""));
    std::map<std::string,int64_t> ilst;
    if (!pk.empty()) {
        auto loot = data::rollPack(pk);
        if (loot.valid) {
            for (const auto& [ais, qty] : loot.consumables) {
                data::addInventoryItem(ais, qty);
                ilst[ais]+=qty;
            }
            for (const auto& key : loot.unlocks) {
                data::addInventoryUnlock(key);
                ilst[key]=1;
            }
            data::saveInventory();
            LOG_INFO("[PvzGw] portal chest '{}': {} consumables, {} unlocks", pk, loot.consumables.size(), loot.unlocks.size());
        }
    }
    if (!ilst.empty()) pushInventoryNotif(client, ilst);
    return request.createReply();
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
