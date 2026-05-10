#pragma once

#include "blaze/types.hpp"
#include "network/ssl_server.hpp"
#include "network/qos_server.hpp"
#include <asio.hpp>
#include <memory>
#include <atomic>
#include <thread>
#include <vector>

namespace ds2 {

/**
 * Main Server Class
 * 
 * Orchestrates all server components:
 * - Redirector SSL server (port 42127)
 * - Blaze SSL server (port 10041)
 * - QoS HTTP server (port 17502)
 */
class Server {
public:
    Server();
    ~Server();
    
    // Initialize server with configuration
    bool init(const blaze::ServerConfig& config);
    
    // Start all servers
    void start();
    
    // Stop all servers
    void stop();
    
    // Run server (blocking)
    void run();
    
    // Check if running
    bool isRunning() const { return m_running; }
    
private:
    void setupComponents();
    void handleRedirectorConnection(std::shared_ptr<asio::ssl::stream<asio::ip::tcp::socket>> socket);
    void handleBlazeConnection(std::shared_ptr<asio::ssl::stream<asio::ip::tcp::socket>> socket);

    // HTTP redirector path (GW2 and newer BlazeSDK games use HTTP POST over TLS)
    void handleRedirectorHttpConnection(
        std::shared_ptr<asio::ssl::stream<asio::ip::tcp::socket>> socket,
        uint64_t connId,
        std::vector<uint8_t> peeked);

    void sendRedirectorHttpResponse(
        std::shared_ptr<asio::ssl::stream<asio::ip::tcp::socket>> socket,
        uint64_t connId,
        const std::vector<uint8_t>& requestBody);

    // Console login flow: handles GET /connect/auth and returns 302 with auth code
    void handleConnectAuthGetConnection(
        std::shared_ptr<asio::ssl::stream<asio::ip::tcp::socket>> socket,
        uint64_t connId,
        std::vector<uint8_t> peeked);

    void sendConnectAuthRedirect(
        std::shared_ptr<asio::ssl::stream<asio::ip::tcp::socket>> socket,
        uint64_t connId,
        const std::string& redirectUri);

    // Fake Nucleus S2S endpoint: handles POST /connect/token and returns access_token JSON
    void handleNucleusPostConnection(
        std::shared_ptr<asio::ssl::stream<asio::ip::tcp::socket>> socket,
        uint64_t connId,
        std::string headers,
        size_t contentLen);

    void sendNucleusTokenResponse(
        std::shared_ptr<asio::ssl::stream<asio::ip::tcp::socket>> socket,
        uint64_t connId);
    
    blaze::ServerConfig m_config;
    asio::io_context m_io_context;
    
    // Servers
    std::shared_ptr<network::SSLServer> m_redirectorServer;
    std::shared_ptr<network::SSLServer> m_blazeServer;
    std::shared_ptr<network::QoSServer> m_qosServer;
    
    // Worker threads
    std::vector<std::thread> m_threads;
    std::atomic<bool> m_running;
    
    // Connection tracking
    uint64_t m_nextConnectionId = 1;
};

} // namespace ds2
