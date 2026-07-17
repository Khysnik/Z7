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

uint64_t UserSessions::s_dedicatedGameId = 0;

void UserSessions::setDedicatedGame(uint64_t gameId) {
    s_dedicatedGameId = gameId;
}

void UserSessions::pushUserSessionExtendedDataUpdate(std::shared_ptr<network::ClientConnection> client) {
    auto& net = client->getNetworkInfo();

    std::vector<blaze::TdfObjectId> ulst = {
        {0x7802, 0x0002, static_cast<uint64_t>(config::connGroupId)},
        {0x0004, 0x0002, 35204832001283ULL},   // the persistent hub game (kGameId)
    };
    if (s_dedicatedGameId != 0)
        ulst.push_back({0x0004, 0x0001, s_dedicatedGameId});  // dedicated match membership

    // Build ADDR union from stored address info
    std::shared_ptr<blaze::TdfValue> addrMember;
    uint8_t addrArm = 0x7F;
    if (net.addrReady && net.addrArm != 0x7F && net.addrArm != 0xFF) {
        addrArm = net.addrArm;
        // VALU = IpPairAddress { EXIP externalAddress, INIP internalAddress, MACI machineId }.
        // EXIP/INIP are IpAddress { IP "ipv4 address", MACI machineId, PORT "ipv4 port" }.
        auto valuStruct = blaze::TdfBuilder()
            .beginStruct("EXIP")                                        // externalAddress (IpAddress)
                .integer("IP",   static_cast<int64_t>(net.exip.ip))     // ip — ipv4 address
                .integer("MACI", static_cast<int64_t>(net.exip.maci))   // machineId
                .integer("PORT", static_cast<int64_t>(net.exip.port))   // port — ipv4 port
            .endStruct()
            .beginStruct("INIP")                                        // internalAddress (IpAddress)
                .integer("IP",   static_cast<int64_t>(net.inip.ip))     // ip — ipv4 address
                .integer("MACI", static_cast<int64_t>(net.inip.maci))   // machineId
                .integer("PORT", static_cast<int64_t>(net.inip.port))   // port — ipv4 port
            .endStruct()
            .integer("MACI", static_cast<int64_t>(net.addrMaci))        // machineId
            .build();
        addrMember = std::make_shared<blaze::TdfValue>("VALU", blaze::TdfType::Struct, valuStruct);
    }

    // UserSessionExtendedDataUpdate { DATA UserSessionExtendedData, SUBS, USID }
    blaze::TdfBuilder builder;
    auto& dataBuilder = builder
        .beginStruct("DATA")                              // DATA = UserSessionExtendedData
            .unionValue("ADDR", addrArm, addrMember)      // address — Network address of the user as determined by QOS and desired gameport.
            .string("BPS", net.ready ? net.bps : "")      // bestPingSiteAlias — The best available ping site.
            .string("CTY", "")                            // country — Country Code provided by the GeoIp database.
            .variable("CVAR", nullptr);                   // clientData — A variable tdf set by the client.

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

    // DMAP — An integer keyed map of data specific to this user (UED keyed by (component<<16)|key).
    dataBuilder.integer("HWFG", 1)   // hardwareFlags — Hardware flags.
               .string("ISP", "");    // ISP — ISP provided by the GeoIP database.

    if (net.ready) {
        static const std::vector<std::string> kServerOrder = {
            "bio-dub", "bio-iad", "bio-sjc", "bio-syd", "i3d-gru", "i3d-nrt"
        };
        std::vector<int64_t> pslm;
        for (auto& srv : kServerOrder) {
            auto it = net.nlmp.find(srv);
            pslm.push_back(it != net.nlmp.end() ? it->second : 1959);
        }
        dataBuilder.intList("PSLM", pslm);   // latencyList — DEPRECATED list of ping site latency the user session owns.
    }

    dataBuilder
        .beginStruct("QDAT")                              // qosData (NetworkQosData) — Bandwidth and NAT type info.
            .integer("BWHR", net.bwhr)                    // bandwidthErrorCode — [DEPRECATED] (QoS 1.0) hResult of determining the client's Bandwidth.
            .integer("DBPS", net.dbps)                    // downstreamBitsPerSecond — The client's downstream network bandwidth (in bits per second).
            .integer("NAHR", net.nahr)                    // natErrorCode — [DEPRECATED] (QoS 1.0) hResult of determining the client's NAT type.
            .integer("NATT", net.natt)                    // natType — The client's NAT type (aka firewall/router type).
            .integer("UBPS", net.ubps)                    // upstreamBitsPerSecond — The client's upstream network bandwidth (in bits per second).
        .endStruct()
        .string("TZ", "")                                 // timeZone — Time zone provided by the GeoIP database.
        .integer("UATT", 0)                               // userInfoAttribute — Custom user info attribute.
        .objectIdList("ULST", ulst)                       // blazeObjectIdList — A list of BlazeObjectIds that the user session belongs to.
    .endStruct()
    .integer("SUBS", 1)                                   // subscribed — True if this UED update is sent/received due to a subscription
    .integer("USID", static_cast<int64_t>(client->getUserId())); // userSessionId — The user's unique blaze Id

    auto _eda = builder.build();

    auto notif = std::make_unique<blaze::Packet>(
        blaze::ComponentId::UserSessions,
        0x0001,
        blaze::MessageType::Notification,
        0   // notifications use msgNum 0 (static: no per-instance counter)
    );
    notif->setPayload(_eda);
    client->sendPacket(std::move(notif));
}

UserSessions::UserSessions()
    : Component(blaze::ComponentId::UserSessions, "UserSessions")
{
}

std::unique_ptr<blaze::Packet> UserSessions::handlePacket(const blaze::Packet& request, std::shared_ptr<network::ClientConnection> client) {
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

// Blaze::UserSessions::UpdateHardwareFlags (id=0x08): Update the hardware flags for the current user's session
std::unique_ptr<blaze::Packet> UserSessions::handleUpdateHardwareFlags(const blaze::Packet& request, std::shared_ptr<network::ClientConnection> client) {
    auto requestTdf = request.getPayloadAsTdf();
    int64_t hardwareFlags = getIntField(requestTdf, "HWFG", 0);

    auto reply = request.createReply();
    client->sendPacket(*reply);

    pushUserSessionExtendedDataUpdate(client);

    return nullptr;
}

// Blaze::UserSessions::UpdateNetworkInfo (id=0x14): Sets the QOS data, latency information, and network address for the current user.
std::unique_ptr<blaze::Packet> UserSessions::handleUpdateNetworkInfo(const blaze::Packet& request, std::shared_ptr<network::ClientConnection> client) {
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

} // namespace gw2::components
