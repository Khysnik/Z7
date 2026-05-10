#include "components/pvz_gw.hpp"
#include "blaze/tdf.hpp"
#include "network/client_connection.hpp"
#include "utils/logger.hpp"

#include <ctime>
#include <vector>

namespace ds2::components {

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
        .int64("DQAT", static_cast<int64_t>(std::time(nullptr)))
        .int64("DQET", static_cast<int64_t>(std::time(nullptr)) + 24 * 60 * 60)
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
        .stringMap("LICS", {})
        .build());
    return reply;
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

    blaze::TdfList gens;
    gens.push_back(std::make_shared<blaze::TdfValue>("", blaze::TdfType::Struct,
        blaze::TdfBuilder()
            .string("IDEN", "masterDisabled")
            .string("VALU", "0")
            .build()));

    blaze::TdfStruct response;
    response["GENS"] = std::make_shared<blaze::TdfValue>("GENS", blaze::TdfType::List, gens);
    response["PACF"] = std::make_shared<blaze::TdfValue>("PACF", blaze::TdfType::Struct,
        blaze::TdfBuilder()
            .string("CSUM", "")
            .string("PRSF", "pc")
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
            .integer("SCTU", static_cast<int64_t>(std::time(nullptr)))
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

    auto reply = request.createReply();
    reply->setPayload(blaze::TdfBuilder()
        .string("ACID", "available")
        .integer("SCTU", 0)
        .integer("VIEW", 0)
        .build());
    return reply;
}

} // namespace ds2::components
