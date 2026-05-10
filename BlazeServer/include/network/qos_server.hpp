#pragma once

#include "blaze/types.hpp"
#include <asio.hpp>
#include <memory>
#include <string>
#include <atomic>

namespace ds2::network {

using asio::ip::tcp;

/**
 * QoS (Quality of Service) Server
 * 
 * HTTP server for NAT detection and QoS endpoints.
 * The game client queries this to determine NAT type and network quality.
 * 
 * Listens on port 17502 by default.
 * Endpoints:
 *   GET /qos/qos?vers=<version>&qtyp=<type>
 *   - Returns NAT/firewall detection results
 */
class QoSServer : public std::enable_shared_from_this<QoSServer> {
public:
    QoSServer(asio::io_context& io_context, const std::string& host, uint16_t port);
    ~QoSServer();
    
    // Start server
    void start();
    
    // Stop server
    void stop();
    
    // Get port
    uint16_t getPort() const { return m_port; }
    
private:
    void doAccept();
    void handleClient(std::shared_ptr<tcp::socket> socket);
    std::string buildQoSResponse(const std::string& request);
    
    asio::io_context& m_io_context;
    tcp::acceptor m_acceptor;
    std::string m_host;
    uint16_t m_port;
    std::atomic<bool> m_running;
};

} // namespace ds2::network
