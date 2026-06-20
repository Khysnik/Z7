#include "server.hpp"
#include "blaze/component.hpp"
#include "blaze/tdf.hpp"
#include "network/client_connection.hpp"
#include "components/authentication.hpp"
#include "components/util.hpp"
#include "components/game_manager.hpp"
#include "components/stats.hpp"
#include "components/user_sessions.hpp"
#include "components/pvz_gw.hpp"
#include "components/packs.hpp"
#include "components/inventory.hpp"
#include "components/game_reporting.hpp"
#include "data/player_profile.hpp"
#include "data/inventory.hpp"
#include "utils/logger.hpp"
#ifdef _WIN32
#include <winsock2.h>
#else
#include <arpa/inet.h>
#endif
#include <sstream>

namespace gw2 {

Server::Server() : m_running(false){
}

Server::~Server() {
    stop();
}

bool Server::init(const blaze::ServerConfig& config) {
    m_config = config;
    
    LOG_INFO("Starting - blaze={}:{}", config.blaze_host, config.blaze_port);

    setupComponents();
    
    m_blazeServer = std::make_shared<network::SSLServer>(m_io_context, config.blaze_host, config.blaze_port);
    
    if (!m_blazeServer->configureSsl(config.ssl_cert_path, config.ssl_key_path)) {
        LOG_ERROR("Failed to configure SSL for blaze server");
        return false;
    }
    
    m_blazeServer->setConnectionHandler([this](auto socket) { 
        handleBlazeConnection(socket);
    });
    
    return true;
}

void Server::setupComponents() {
    auto& registry = blaze::ComponentRegistry::instance();
    
    registry.registerComponent(std::make_shared<components::Authentication>());
    registry.registerComponent(std::make_shared<components::Util>());
    registry.registerComponent(std::make_shared<components::UserSessions>());
    registry.registerComponent(std::make_shared<components::PvzGwComponent>());
    registry.registerComponent(std::make_shared<components::PacksComponent>());
    registry.registerComponent(std::make_shared<components::InventoryComponent>());
    registry.registerComponent(std::make_shared<components::GameManager>());
    registry.registerComponent(std::make_shared<components::StatsComponent>());
    registry.registerComponent(std::make_shared<components::GameReportingComponent>());

    data::PlayerProfile::instance().load("data/MPProfile.json");
    data::PlayerProfile::instance().loadAggregation("data/stat_aggregation.json");

    data::loadInventory("data/inventory.json");

}

void Server::start() {
    if (m_running) return;
    
    m_running = true;
    
    m_blazeServer->start();

    LOG_INFO("Server started");
}
void Server::stop() {
    if (!m_running) return;
    
    LOG_DEBUG("Stopping server...");
    
    m_running = false;
    m_io_context.stop();

    if (m_blazeServer) m_blazeServer->stop();

    for (auto& thread : m_threads) {
        if (thread.joinable()) {
            thread.join();
        }
    }
    m_threads.clear();
    
    LOG_DEBUG("Server stopped");
}

void Server::run() {
    unsigned int numThreads = std::thread::hardware_concurrency();
    if (numThreads < 2) numThreads = 2;
    
    LOG_DEBUG("Starting {} worker threads", numThreads);
    
    for (unsigned int i = 0; i < numThreads; i++) {
        m_threads.emplace_back([this]() {
            m_io_context.run();
        });
    }

    for (auto& thread : m_threads) {
        if (thread.joinable()) {
            thread.join();
        }
    }
}

void Server::handleBlazeConnection(std::shared_ptr<asio::ssl::stream<asio::ip::tcp::socket>> socket) {
    uint64_t connId = m_nextConnectionId++;
    
    auto client = std::make_shared<network::ClientConnection>(socket, connId);
    
    client->setPacketHandler([this](auto client, auto packet) {
        if (!packet) return;

        if (packet->getMessageType() == blaze::MessageType::Ping) {
            client->sendPacket(packet->createPingReply());
            return;
        }

        auto& registry = blaze::ComponentRegistry::instance();

        if (auto response = registry.routePacket(*packet, client)) {
            client->sendPacket(std::move(response));
        }
    });
    
    client->setDisconnectHandler([](auto client) {
        LOG_INFO("Blaze client {} disconnected", client->getId());
    });
    
    client->start();
}

} // namespace ds2
