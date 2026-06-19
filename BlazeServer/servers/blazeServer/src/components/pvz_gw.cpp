#include "components/pvz_gw.hpp"
#include "blaze/tdf.hpp"
#include "blaze/component.hpp"
#include "network/client_connection.hpp"
#include "components/user_sessions.hpp"
#include "data/licenses.hpp"
#include "utils/logger.hpp"
#include "utils/server_time.hpp"

#include <ctime>
#include <vector>

namespace gw2::components {

namespace {

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

blaze::TdfStruct buildBlackMarketSlot(const std::string& description,
                                      int64_t itemId,
                                      const std::string& name,
                                      int64_t price,
                                      bool hasBeenPurchased,
                                      int64_t slotId) {
    return blaze::TdfBuilder()
        .string("DESC", description)
        .integer("HBPT", hasBeenPurchased ? 1 : 0)
        .integer("ITID", itemId)
        .string("NAME", name)
        .integer("PRCE", price)
        .integer("SLID", slotId)
        .build();
}

blaze::TdfStruct buildBlackMarketData(int64_t blazeId) {
    blaze::TdfList slots;
    slots.push_back(std::make_shared<blaze::TdfValue>("", blaze::TdfType::Struct,
        buildBlackMarketSlot("Limited-time slot", 0, "Black Market", 0, false, 0)));

    blaze::TdfStruct result = blaze::TdfBuilder()
        .integer("ACID", 0)
        .integer("BLID", blazeId)
        .integer("IPID", 0)
        .integer("VIEW", 0)
        .build();
    result["SLTS"] = std::make_shared<blaze::TdfValue>("SLTS", blaze::TdfType::List, slots);
    return result;
}

} // namespace

PvzGwComponent::PvzGwComponent()
    : Component(static_cast<blaze::ComponentId>(0x0805), "PvzGwComponent")
{
}

std::unique_ptr<blaze::Packet> PvzGwComponent::handlePacket(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    uint16_t command = request.getCommand();

    switch (static_cast<blaze::PvzGwCommand>(command)) {
        case blaze::PvzGwCommand::checkUserMessages:
            return handleCheckUserMessages(request, client);
        case blaze::PvzGwCommand::setXPMultiplier:
            return handleSetXPMultiplier(request, client);
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

std::unique_ptr<blaze::Packet> PvzGwComponent::handleSetXPMultiplier(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    LOG_INFO("[PvzGw] setXPMultiplier from {}", client->getRemoteAddress());

    auto reply = request.createReply();
    reply->setPayload(blaze::TdfBuilder()
        .floatValue("LXPM", 1.0f)
        .floatValue("NXPM", 1.0f)
        .build());
    return reply;
}

std::unique_ptr<blaze::Packet> PvzGwComponent::handleGetDailyQuests(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    LOG_INFO("[PvzGw] getDailyQuests from {}", client->getRemoteAddress());

    auto reply = request.createReply();
    reply->setPayload(blaze::TdfBuilder()
        .int64("DQAT", blazeServerNow())                          // Blaze time = 2x unix seconds
        .int64("DQET", toBlazeTime(std::time(nullptr) + 24 * 60 * 60))
        .list("DQID", blaze::TdfType::String, {})
        .int64("DQTE", 24 * 60 * 60)
        .build());
    return reply;
}

std::unique_ptr<blaze::Packet> PvzGwComponent::handleGetPersistedLicenses(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    LOG_INFO("[PvzGw] getPersistedLicenses from {}", client->getRemoteAddress());

    auto reply = request.createReply();
    reply->setPayload(blaze::TdfBuilder()
        .list("LICS", blaze::TdfType::String, data::getPersistedLicenses())
        .build());
    client->sendPacket(*reply);

    auto userSessions = blaze::ComponentRegistry::instance().getComponent(blaze::ComponentId::UserSessions);
    if (userSessions) {
        static_cast<UserSessions*>(userSessions.get())->pushUserSessionExtendedDataUpdate(client);
    }

    return nullptr;
}

std::unique_ptr<blaze::Packet> PvzGwComponent::handleSetOnlineAccessEntitlements(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    LOG_INFO("[PvzGw] setOnlineAccessEntitlements from {}", client->getRemoteAddress());

    auto reply = request.createReply();
    reply->setPayload(blaze::TdfBuilder()
        .boolean("ANEG", false)
        .build());
    return reply;
}

std::unique_ptr<blaze::Packet> PvzGwComponent::handleGetClientSettings(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    auto requestTdf = request.getPayloadAsTdf();
    int64_t changelist = getIntField(requestTdf, "CHAN", 0);
    LOG_INFO("[PvzGw] getClientSettings from {} (CHAN={})",
             client->getRemoteAddress(), changelist);

    static const std::vector<std::pair<std::string, std::string>> kSettings = {
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

    blaze::TdfList gens;
    for (auto& [iden, valu] : kSettings) {
        gens.push_back(std::make_shared<blaze::TdfValue>("", blaze::TdfType::Struct,
            blaze::TdfBuilder()
                .string("IDEN", iden)
                .string("VALU", valu)
                .build()));
    }

    blaze::TdfStruct response;
    response["GENS"] = std::make_shared<blaze::TdfValue>("GENS", blaze::TdfType::List, gens);
    response["PACF"] = std::make_shared<blaze::TdfValue>("PACF", blaze::TdfType::Struct,
        blaze::TdfBuilder()
            .integer("CSUM", 0)
            .string("PRSF", "")
            .integer("PSTA", 0)
            .string("PURL", "")
            .build());

    auto reply = request.createReply();
    reply->setPayload(response);
    return reply;
}

std::unique_ptr<blaze::Packet> PvzGwComponent::handleGetCommunityAchievements(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    LOG_INFO("[PvzGw] getCommunityAchievements from {}", client->getRemoteAddress());
    return request.createReply();
}

std::unique_ptr<blaze::Packet> PvzGwComponent::handleClaimCommunityEventReward(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    LOG_INFO("[PvzGw] claimCommunityEventReward from {}", client->getRemoteAddress());
    return request.createReply();
}

std::unique_ptr<blaze::Packet> PvzGwComponent::handleGetBlackMarketData(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    auto requestTdf = request.getPayloadAsTdf();
    int64_t blazeId = getIntField(requestTdf, "BLID", client->getUserId());
    int64_t includePostInteractionData = getIntField(requestTdf, "IPID", 0);
    LOG_INFO("[PvzGw] getBlackMarketData from {} (BLID={}, IPID={})",
             client->getRemoteAddress(), blazeId, includePostInteractionData);

    auto reply = request.createReply();
    reply->setPayload(buildBlackMarketData(blazeId));
    return reply;
}

std::unique_ptr<blaze::Packet> PvzGwComponent::handlePurchaseBlackMarketItem(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    auto requestTdf = request.getPayloadAsTdf();
    LOG_INFO("[PvzGw] purchaseBlackMarketItem from {} (BLID={}, SLID={}, ACID={})",
             client->getRemoteAddress(),
             getIntField(requestTdf, "BLID", client->getUserId()),
             getIntField(requestTdf, "SLID", 0),
             getIntField(requestTdf, "ACID", 0));
    return request.createReply();
}

std::unique_ptr<blaze::Packet> PvzGwComponent::handleSetBlackMarketViewed(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    auto requestTdf = request.getPayloadAsTdf();
    LOG_INFO("[PvzGw] setBlackMarketViewed from {} (BLID={}, VIEW={})",
             client->getRemoteAddress(),
             getIntField(requestTdf, "BLID", client->getUserId()),
             getIntField(requestTdf, "VIEW", 0));
    return request.createReply();
}

std::unique_ptr<blaze::Packet> PvzGwComponent::handleGetCommunityPortalData(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    auto requestTdf = request.getPayloadAsTdf();
    int64_t blazeId = getIntField(requestTdf, "BLID", client->getUserId());
    LOG_INFO("[PvzGw] getCommunityPortalData from {} (BLID={})",
             client->getRemoteAddress(), blazeId);

    auto reply = request.createReply();
    reply->setPayload(blaze::TdfBuilder()
        .beginStruct("CPDT")
            .integer("ACID", 0)
            .integer("BLID", blazeId)
            .string("NIDI", "No portal events available")
            .integer("SCTU", blazeServerNow())  // Blaze time = 2x unix seconds
            .string("SVDI", "Community portal updates will appear here.")
            .integer("VIEW", 0)
        .endStruct()
        .integer("FNEV", 0)
        .build());
    return reply;
}

std::unique_ptr<blaze::Packet> PvzGwComponent::handleOpenCommunityPortalChest(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    auto requestTdf = request.getPayloadAsTdf();
    LOG_INFO("[PvzGw] openCommunityPortalChest from {} (ACID={}, BLID={})",
             client->getRemoteAddress(),
             getIntField(requestTdf, "ACID", 0),
             getIntField(requestTdf, "BLID", client->getUserId()));
    return request.createReply();
}

std::unique_ptr<blaze::Packet> PvzGwComponent::handleGetPlaylists(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    LOG_INFO("[PvzGw] getPlaylists from {}", client->getRemoteAddress());
    auto reply = request.createReply();
    reply->setPayload(blaze::TdfBuilder()
        .list("PLYT", blaze::TdfType::Struct, {})
        .build());
    return reply;
}

std::unique_ptr<blaze::Packet> PvzGwComponent::handleGetPlaylistRotation(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    LOG_INFO("[PvzGw] getPlaylistRotation from {}", client->getRemoteAddress());
    auto reply = request.createReply();
    reply->setPayload(blaze::TdfBuilder()
        .list("ROTA", blaze::TdfType::Struct, {})
        .build());
    return reply;
}

std::unique_ptr<blaze::Packet> PvzGwComponent::handleGetLoyaltyChallengeData(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    LOG_INFO("[PvzGw] getLoyaltyChallengeData from {}", client->getRemoteAddress());
    return request.createReply();
}

std::unique_ptr<blaze::Packet> PvzGwComponent::handleCheckUserMessages(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    LOG_INFO("[PvzGw] checkUserMessages from {}", client->getRemoteAddress());
    return request.createReply();
}

std::unique_ptr<blaze::Packet> PvzGwComponent::handleGetUserMessages(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    LOG_INFO("[PvzGw] getUserMessages from {}", client->getRemoteAddress());
    return request.createReply();
}

std::unique_ptr<blaze::Packet> PvzGwComponent::handleUpdateUserMessageStatus(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    auto requestTdf = request.getPayloadAsTdf();
    LOG_INFO("[PvzGw] updateUserMessageStatus from {} (MID={})",
             client->getRemoteAddress(),
             getIntField(requestTdf, "MID", 0));
    return request.createReply();
}

std::unique_ptr<blaze::Packet> PvzGwComponent::handleForceClientNotification(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    auto requestTdf = request.getPayloadAsTdf();
    int64_t blazeId = getIntField(requestTdf, "BLID", 0);
    int64_t type = getIntField(requestTdf, "TYPE", 0);
    LOG_INFO("[PvzGw] forceClientNotification from {} (BLID={}, TYPE={})",
             client->getRemoteAddress(), blazeId, type);

    return request.createReply();
}

} // namespace gw2::components
