#include "server.hpp"
#include "config.hpp"
#include "utils/logger.hpp"
#include "utils/config.hpp"
#include "utils/editorial.hpp"

#include <cstdlib>
#include <string>

static gw2::Server* g_server = nullptr;

int main(int argc, char** argv) {

    gw2::utils::Logger::init("gw2-server.log");

    LOG_INFO("===========================================");
    LOG_INFO("  Garden Warfare 2 Server Emulator");
    LOG_INFO("  Version 0.9.9");
    LOG_INFO("===========================================");

    for (int i = 1; i < argc; ++i) {
        std::string arg = argv[i];
        if (arg == "-online" && i + 1 < argc) {
            gw2::utils::kEditorialBase = "https://" + std::string(argv[++i]);
            gw2::config::onlineMode = true;
            LOG_INFO("[online] editorial server -> {}", gw2::utils::kEditorialBase);
        } else if (arg == "-pid" && i + 1 < argc) {
            gw2::config::blazeId = std::strtoll(argv[++i], nullptr, 10);
            LOG_INFO("[identity] blazeId (PID) -> {}", gw2::config::blazeId);
        } else if (arg == "-nid" && i + 1 < argc) {
            gw2::config::nucleusId = std::strtoll(argv[++i], nullptr, 10);
            LOG_INFO("[identity] nucleusId -> {}", gw2::config::nucleusId);
        } else if (arg == "-name" && i + 1 < argc) {
            gw2::config::persona = argv[++i];
            LOG_INFO("[identity] persona -> {}", gw2::config::persona);
        } else if (arg == "-disableCC") {
            gw2::config::disableCommunityChallenge = true;
            LOG_INFO("[offline] community challenge disabled (-disableCC)");
        } else if (arg == "-disableCP") {
            gw2::config::disableCommunityPortal = true;
            LOG_INFO("[offline] community portal disabled (-disableCP)");
        }
    }

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
