#pragma once

#include "blaze/types.hpp"
#include "network/ssl_server.hpp"
#include "network/qos_udp_server.hpp"
#include <asio.hpp>
#include <memory>
#include <atomic>
#include <thread>
#include <vector>

namespace gw2 {

class Server {
public:
    Server();
    ~Server();

    bool init(const blaze::ServerConfig& config);

    void start();

    void stop();

    void run();

    bool isRunning() const { return m_running; }
    
private:
    void setupComponents();
    void handleBlazeConnection(std::shared_ptr<asio::ssl::stream<asio::ip::tcp::socket>> socket);
    
    blaze::ServerConfig m_config;
    asio::io_context m_io_context;

    std::shared_ptr<network::SSLServer> m_blazeServer;
    std::unique_ptr<network::QosUdpServer> m_qosServer;

    std::vector<std::thread> m_threads;
    std::atomic<bool> m_running;

    uint64_t m_nextConnectionId = 1;
};

} // namespace ds2
