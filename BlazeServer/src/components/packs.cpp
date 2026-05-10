#include "components/packs.hpp"
#include "blaze/tdf.hpp"
#include "network/client_connection.hpp"
#include "utils/logger.hpp"

namespace ds2::components {

namespace {

blaze::TdfList buildTestDesL() {
    blaze::TdfStruct tmpl = blaze::TdfBuilder()
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
        .build();

    blaze::TdfList desl;
    desl.push_back(std::make_shared<blaze::TdfValue>("", blaze::TdfType::Struct, std::move(tmpl)));
    return desl;
}

} // namespace

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

    blaze::TdfList desl = buildTestDesL();
    blaze::TdfStruct response;
    response["DESL"] = std::make_shared<blaze::TdfValue>("DESL", blaze::TdfType::List, std::move(desl));
    auto reply = request.createReply();
    reply->setPayload(response);
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
    LOG_INFO("[Packs] openPack from {}", client->getRemoteAddress());
    return request.createReply();
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

    blaze::TdfList desl = buildTestDesL();
    blaze::TdfStruct response;
    response["DESL"] = std::make_shared<blaze::TdfValue>("DESL", blaze::TdfType::List, std::move(desl));
    auto reply = request.createReply();
    reply->setPayload(response);
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
    LOG_INFO("[Packs] purchaseAndOpenPack from {}", client->getRemoteAddress());
    return request.createReply();
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

} // namespace ds2::components
