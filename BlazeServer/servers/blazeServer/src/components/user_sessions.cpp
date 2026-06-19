#include "components/user_sessions.hpp"
#include "blaze/tdf.hpp"
#include "network/client_connection.hpp"
#include "utils/logger.hpp"
#include "config.hpp"

namespace gw2::components {

namespace {

int64_t getIntField(const blaze::TdfStruct& tdf, const std::string& tag, int64_t fallback = 0) {
    auto it = tdf.find(tag);
    if (it == tdf.end() || !it->second) return fallback;
    if (it->second->type != blaze::TdfType::Integer) return fallback;
    return std::get<blaze::TdfInteger>(it->second->value);
}

} // namespace

UserSessions::UserSessions()
    : Component(blaze::ComponentId::UserSessions, "UserSessions")
{
}

std::unique_ptr<blaze::Packet> UserSessions::handlePacket(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    uint16_t command = request.getCommand();

    switch (static_cast<blaze::UserSessionsCommand>(command)) {
        case blaze::UserSessionsCommand::updateHardwareFlags:
            return handleUpdateHardwareFlags(request, client);

        case blaze::UserSessionsCommand::updateNetworkInfo:
            return handleUpdateNetworkInfo(request, client);

        default:
            LOG_WARN("[UserSessions] Unknown command: 0x{:04X}", command);
            return request.createReply();
    }
}

std::unique_ptr<blaze::Packet> UserSessions::handleUpdateHardwareFlags(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    auto requestTdf = request.getPayloadAsTdf();
    int64_t hardwareFlags = getIntField(requestTdf, "HWFG", 0);

    auto reply = request.createReply();
    client->sendPacket(*reply);

    pushUserSessionExtendedDataUpdate(client);

    return nullptr;
}

std::unique_ptr<blaze::Packet> UserSessions::handleUpdateNetworkInfo(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    auto requestTdf = request.getPayloadAsTdf();
    int64_t opts = getIntField(requestTdf, "OPTS", 0);

    // Copy current network info so we can update it incrementally
    network::ClientConnection::NetworkInfo netInfo = client->getNetworkInfo();

    auto infoIt = requestTdf.find("INFO");
    if (infoIt != requestTdf.end() && infoIt->second &&
        infoIt->second->type == blaze::TdfType::Struct)
    {
        auto& info = std::get<blaze::TdfStruct>(infoIt->second->value);

        // Extract ADDR union (always — needed for the initial notification)
        auto addrIt = info.find("ADDR");
        if (addrIt != info.end() && addrIt->second &&
            addrIt->second->type == blaze::TdfType::Union)
        {
            auto& u = std::get<blaze::TdfUnion>(addrIt->second->value);
            netInfo.addrArm = u.arm;
            if (u.member && u.member->type == blaze::TdfType::Struct) {
                auto& valu = std::get<blaze::TdfStruct>(u.member->value);
                auto exipIt = valu.find("EXIP");
                if (exipIt != valu.end() && exipIt->second &&
                    exipIt->second->type == blaze::TdfType::Struct)
                {
                    auto& exip = std::get<blaze::TdfStruct>(exipIt->second->value);
                    netInfo.exip = {
                        static_cast<uint64_t>(getIntField(exip, "IP")),
                        static_cast<uint64_t>(getIntField(exip, "MACI")),
                        static_cast<uint64_t>(getIntField(exip, "PORT"))
                    };
                }
                auto inipIt = valu.find("INIP");
                if (inipIt != valu.end() && inipIt->second &&
                    inipIt->second->type == blaze::TdfType::Struct)
                {
                    auto& inip = std::get<blaze::TdfStruct>(inipIt->second->value);
                    netInfo.inip = {
                        static_cast<uint64_t>(getIntField(inip, "IP")),
                        static_cast<uint64_t>(getIntField(inip, "MACI")),
                        static_cast<uint64_t>(getIntField(inip, "PORT"))
                    };
                }
                netInfo.addrMaci = static_cast<uint64_t>(getIntField(valu, "MACI"));
            }
            netInfo.addrReady = true;
        }

        // Extract QoS data only when complete (OPTS=4)
        if (opts == 4) {
            auto nlmpIt = info.find("NLMP");
            if (nlmpIt != info.end() && nlmpIt->second &&
                nlmpIt->second->type == blaze::TdfType::Map)
            {
                auto& m = std::get<blaze::TdfMapWrapper>(nlmpIt->second->value);
                int64_t minPing = INT64_MAX;
                for (auto& [k, v] : m.data) {
                    if (v && v->type == blaze::TdfType::Integer) {
                        int64_t ping = std::get<blaze::TdfInteger>(v->value);
                        netInfo.nlmp[k] = ping;
                        if (ping < minPing) { minPing = ping; netInfo.bps = k; }
                    }
                }
            }

            auto nqosIt = info.find("NQOS");
            if (nqosIt != info.end() && nqosIt->second &&
                nqosIt->second->type == blaze::TdfType::Struct)
            {
                auto& nqos = std::get<blaze::TdfStruct>(nqosIt->second->value);
                netInfo.bwhr = getIntField(nqos, "BWHR");
                netInfo.dbps = getIntField(nqos, "DBPS");
                netInfo.nahr = getIntField(nqos, "NAHR");
                netInfo.natt = getIntField(nqos, "NATT");
                netInfo.ubps = getIntField(nqos, "UBPS");
            }

            netInfo.ready = true;
            LOG_INFO("[UserSessions] stored QoS network info (bps={}, natt={})", netInfo.bps, netInfo.natt);
        }
    }

    client->setNetworkInfo(netInfo);
    return request.createReply();
}

void UserSessions::pushUserSessionExtendedDataUpdate(
    std::shared_ptr<network::ClientConnection> client
) {
    auto& net = client->getNetworkInfo();

    // Build ADDR union from stored address info
    std::shared_ptr<blaze::TdfValue> addrMember;
    uint8_t addrArm = 0x7F;
    if (net.addrReady && net.addrArm != 0x7F && net.addrArm != 0xFF) {
        addrArm = net.addrArm;
        auto valuStruct = blaze::TdfBuilder()
            .beginStruct("EXIP")
                .integer("IP",   static_cast<int64_t>(net.exip.ip))
                .integer("MACI", static_cast<int64_t>(net.exip.maci))
                .integer("PORT", static_cast<int64_t>(net.exip.port))
            .endStruct()
            .beginStruct("INIP")
                .integer("IP",   static_cast<int64_t>(net.inip.ip))
                .integer("MACI", static_cast<int64_t>(net.inip.maci))
                .integer("PORT", static_cast<int64_t>(net.inip.port))
            .endStruct()
            .integer("MACI", static_cast<int64_t>(net.addrMaci))
            .build();
        addrMember = std::make_shared<blaze::TdfValue>("VALU", blaze::TdfType::Struct, valuStruct);
    }

    // DMAP: first notification omits "269090817", second includes it
    blaze::TdfBuilder builder;
    auto& dataBuilder = builder
        .beginStruct("DATA")
            .unionValue("ADDR", addrArm, addrMember)
            .string("BPS", net.ready ? net.bps : "")
            .string("CTY", "")
            .variable("CVAR", nullptr);

    if (net.ready) {
        dataBuilder.integerMap("DMAP", {
            {"269090817",  1},
            {"269090818",  191},
            {"269090819",  37},
            {"4026793985", 0},
            {"917509",     94763},
            {"917510",     0},
            {"917511",     129},
        });
    } else {
        dataBuilder.integerMap("DMAP", {
            {"269090818",  191},
            {"269090819",  37},
            {"4026793985", 0},
            {"917509",     94763},
            {"917510",     0},
            {"917511",     129},
        });
    }

    dataBuilder.integer("HWFG", 0).string("ISP", "");

    if (net.ready) {
        static const std::vector<std::string> kServerOrder = {
            "bio-dub", "bio-iad", "bio-sjc", "bio-syd", "i3d-gru", "i3d-nrt"
        };
        std::vector<int64_t> pslm;
        for (auto& srv : kServerOrder) {
            auto it = net.nlmp.find(srv);
            pslm.push_back(it != net.nlmp.end() ? it->second : 1959);
        }
        dataBuilder.intList("PSLM", pslm);
    }

    dataBuilder
        .beginStruct("QDAT")
            .integer("BWHR", net.bwhr)
            .integer("DBPS", net.dbps)
            .integer("NAHR", net.nahr)
            .integer("NATT", net.natt)
            .integer("UBPS", net.ubps)
        .endStruct()
        .string("TZ", "")
        .integer("UATT", 0)
        .objectIdList("ULST", {{0xF002, 0x0002, static_cast<uint64_t>(config::connGroupId)}})
    .endStruct()
    .integer("SUBS", 1)
    .integer("USID", static_cast<int64_t>(client->getUserId()));

    auto notif = std::make_unique<blaze::Packet>(
        blaze::ComponentId::UserSessions,
        0x0001,
        blaze::MessageType::Notification,
        m_nextNotifMsgNum++
    );
    notif->setPayload(builder.build());
    client->sendPacket(std::move(notif));
}

} // namespace gw2::components
