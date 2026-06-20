#include "server.hpp"
#include "utils/logger.hpp"
#include "utils/config.hpp"

static gw2::Server* g_server = nullptr;

int main() {

    gw2::utils::Logger::init("gw2-server.log");
    
    LOG_INFO("===========================================");
    LOG_INFO("  Garden Warfare 2 Server Emulator");
    LOG_INFO("  Version 0.9.9");
    LOG_INFO("===========================================");
    
    auto& config = gw2::utils::Config::instance();

    gw2::Server server;
    g_server = &server;
    
    if (!server.init(config.getServerConfig())) {
        LOG_ERROR("Failed to initialize server");
        gw2::utils::Logger::shutdown();
        return 1;
    }
    
    server.start();
    
    LOG_INFO("Server is running. Press Ctrl+C to stop.");
    
    server.run();
    
    return 0;
}
