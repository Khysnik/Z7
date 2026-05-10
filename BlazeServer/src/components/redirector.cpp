#include "components/redirector.hpp"
#include "blaze/tdf.hpp"
#include "network/client_connection.hpp"
#include "utils/logger.hpp"
#include <arpa/inet.h>

namespace ds2::components {

Redirector::Redirector()
    : Component(blaze::ComponentId::Redirector, "Redirector")
    , m_blazeHost("127.0.0.1")
    , m_blazePort(10041)
{
}

void Redirector::setBlazeServerAddress(const std::string& host, uint16_t port) {
    m_blazeHost = host;
    m_blazePort = port;
    LOG_INFO("Redirector will redirect to {}:{}", host, port);
}

std::unique_ptr<blaze::Packet> Redirector::handlePacket(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    uint16_t command = request.getCommand();
    
    switch (static_cast<blaze::RedirectorCommand>(command)) {
        case blaze::RedirectorCommand::getServerInstance:
            return handleGetServerInstance(request, client);
        
        default:
            LOG_WARN("[Redirector] Unknown command: 0x{:04X}", command);
            return request.createReply();
    }
}

std::unique_ptr<blaze::Packet> Redirector::handleGetServerInstance(
    const blaze::Packet& request,
    std::shared_ptr<network::ClientConnection> client
) {
    LOG_INFO("[Redirector] getServerInstance from {}", client->getRemoteAddress());
    
    // Parse request TDF to get client info
    auto requestTdf = request.getPayloadAsTdf();
    
    // Log client info
    for (const auto& [tag, value] : requestTdf) {
        if (value && value->type == blaze::TdfType::String) {
            LOG_DEBUG("  {}: {}", tag, std::get<blaze::TdfString>(value->value));
        }
    }
    
    // Convert host to IP address
    struct in_addr addr;
    inet_pton(AF_INET, m_blazeHost.c_str(), &addr);
    uint32_t ip = ntohl(addr.s_addr);
    
    // Build response
    // The client expects:
    // - ADDR: Server address (IP/port/protocol)
    // - SECU: Security (1 = SSL)
    // - XDNS: 0
    
    blaze::TdfBuilder builder;
    builder
        .beginStruct("ADDR")
            .triple("VALU", ip, m_blazePort, 2)  // 2 = TCP+SSL
        .endStruct()
        .integer("SECU", 1)   // SSL required
        .integer("XDNS", 0);  // No DNS lookup needed
    
    auto reply = request.createReply();
    reply->setPayload(builder.build());
    
    LOG_INFO("[Redirector] Redirecting to {}:{}", m_blazeHost, m_blazePort);
    
    return reply;
}

} // namespace ds2::components
