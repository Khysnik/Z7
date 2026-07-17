#pragma once

#include <asio.hpp>
#include <array>
#include <cstdint>
#include <memory>
#include <vector>

namespace gw2::network {

class QosUdpServer {
public:
    QosUdpServer(asio::io_context& io, const std::string& host, uint16_t probePort);

    void start();
    void stop();

private:
    struct Endpoint {
        asio::ip::udp::socket        socket;
        bool                         isProbe;
        uint16_t                     port;
        asio::ip::udp::endpoint      sender;
        std::array<uint8_t, 2048>    buf{};
        explicit Endpoint(asio::io_context& io) : socket(io) {}
    };

    void bindPort(uint16_t port, bool isProbe);
    void receive(const std::shared_ptr<Endpoint>& endpoint);

    asio::io_context&                         m_io;
    std::string                               m_host;
    uint16_t                                  m_probePort;
    std::vector<std::shared_ptr<Endpoint>>    m_endpoints;
};

} // namespace gw2::network
