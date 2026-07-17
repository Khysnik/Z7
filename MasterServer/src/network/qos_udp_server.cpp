#include "network/qos_udp_server.hpp"
#include "utils/logger.hpp"

namespace gw2::network {

using asio::ip::udp;

QosUdpServer::QosUdpServer(asio::io_context& io, const std::string& host, uint16_t probePort) : m_io(io), m_host(host), m_probePort(probePort) {}

void QosUdpServer::start() {
    bindPort(m_probePort, true);
    bindPort(m_probePort + 1, false);
    bindPort(m_probePort + 2, false);
}

void QosUdpServer::stop() {
    for (auto& ep : m_endpoints) {
        asio::error_code ec;
        ep->socket.close(ec);
    }
    m_endpoints.clear();
}

void QosUdpServer::bindPort(uint16_t port, bool isProbe) {
    auto endpoint = std::make_shared<Endpoint>(m_io);
    endpoint->isProbe = isProbe;
    endpoint->port = port;
    asio::error_code err;
    endpoint->socket.open(udp::v4(), err);
    if (!err) endpoint->socket.bind(udp::endpoint(asio::ip::make_address(m_host, err), port), err);
    if (err) {
        LOG_ERROR("[qos] UDP bind {}:{} failed: {}", m_host, port, err.message());
        return;
    }
    LOG_INFO("[qos] UDP {} listening on {}:{}", isProbe ? "probe" : "firewall", m_host, port);
    m_endpoints.push_back(endpoint);
    receive(endpoint);
}

void QosUdpServer::receive(const std::shared_ptr<Endpoint>& endpoint) {
    endpoint->socket.async_receive_from(
        asio::buffer(endpoint->buf), endpoint->sender,
        [this, endpoint](const asio::error_code& err, std::size_t n) {
            if (err) {
                if (err != asio::error::operation_aborted)
                    LOG_WARN("[qos] UDP recv error on {}: {}", endpoint->port, err.message());
                return;
            }

            if (endpoint->isProbe && n >= 20) {
                std::array<uint8_t, 30> reply{};
                std::copy_n(endpoint->buf.begin(), 20, reply.begin());
                if (endpoint->sender.address().is_v4()) {
                    auto octets = endpoint->sender.address().to_v4().to_bytes();
                    reply[20] = octets[0];
                    reply[21] = octets[1];
                    reply[22] = octets[2];
                    reply[23] = octets[3];
                }
                uint16_t sport = endpoint->sender.port();
                reply[24] = static_cast<uint8_t>(sport >> 8);
                reply[25] = static_cast<uint8_t>(sport & 0xFF);
                asio::error_code sec;
                endpoint->socket.send_to(asio::buffer(reply), endpoint->sender, 0, sec);
                LOG_DEBUG("[qos] probe from {}:{} ({} bytes)",
                          endpoint->sender.address().to_string(), endpoint->sender.port(), n);
            } else {
                LOG_DEBUG("[qos] firewall probe on {} from {}:{} ({} bytes)",
                          endpoint->port, endpoint->sender.address().to_string(), endpoint->sender.port(), n);
            }

            receive(endpoint);
        });
}

} // namespace gw2::network
