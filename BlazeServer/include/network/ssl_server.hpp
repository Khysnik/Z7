#pragma once

#include "blaze/types.hpp"
#include <asio.hpp>
#include <asio/ssl.hpp>
#include <memory>
#include <string>
#include <functional>
#include <atomic>

namespace ds2::network {

using asio::ip::tcp;

/**
 * SSL Server
 * 
 * Handles SSL/TLS connections for Blaze protocol.
 * Used for both Redirector (port 42127) and main Blaze server (port 10041).
 */
class SSLServer : public std::enable_shared_from_this<SSLServer> {
public:
    using ConnectionHandler = std::function<void(std::shared_ptr<asio::ssl::stream<tcp::socket>>)>;
    
    SSLServer(asio::io_context& io_context, const std::string& host, uint16_t port);
    ~SSLServer();
    
    // Configure SSL context
    bool configureSsl(const std::string& certPath, const std::string& keyPath);
    
    // Set connection handler
    void setConnectionHandler(ConnectionHandler handler);
    
    // Start accepting connections
    void start();
    
    // Stop server
    void stop();
    
    // Get port
    uint16_t getPort() const { return m_port; }
    
private:
    void doAccept();
    void handleAccept(std::shared_ptr<asio::ssl::stream<tcp::socket>> socket,
                      const asio::error_code& error);
    void handleHandshake(std::shared_ptr<asio::ssl::stream<tcp::socket>> socket,
                         const asio::error_code& error);
    
    asio::io_context& m_io_context;
    asio::ssl::context m_ssl_context;
    tcp::acceptor m_acceptor;
    std::string m_host;
    uint16_t m_port;
    std::atomic<bool> m_running;
    ConnectionHandler m_connectionHandler;
};

} // namespace ds2::network
