#include "server.hpp"
#include "utils/logger.hpp"
#include "utils/config.hpp"

#include <iostream>
#include <csignal>
#include <atomic>

static std::atomic<bool> g_running{true};
static ds2::Server* g_server = nullptr;

void signalHandler(int signal) {
    std::cout << "\nReceived signal " << signal << ", shutting down...\n";
    g_running = false;
    if (g_server) {
        g_server->stop();
    }
}

void printUsage(const char* program) {
    std::cout << "Garden Warfare 2 Server Emulator\n";
    std::cout << "Usage: " << program << " [options]\n\n";
    std::cout << "Options:\n";
    std::cout << "  -c, --config <file>   Load configuration from file\n";
    std::cout << "  -d, --debug           Verbose console output (raw bytes, dispatcher boilerplate).\n";
    std::cout << "                        File log always captures full detail.\n";
    std::cout << "  -h, --help            Show this help message\n";
    std::cout << "\n";
    std::cout << "Ports:\n";
    std::cout << "  10041  - Blaze server (SSL)\n";
    std::cout << "  17502  - QoS server (HTTP)\n";
    std::cout << "\n";
    std::cout << "SSL Certificates:\n";
    std::cout << "  Create self-signed certificates in certs/ directory.\n";
    std::cout << "  See README.md for instructions.\n";
}

int main(int argc, char* argv[]) {
    // Parse arguments
    std::string configFile;
    bool debug = false;

    for (int i = 1; i < argc; i++) {
        std::string arg = argv[i];

        if (arg == "-h" || arg == "--help") {
            printUsage(argv[0]);
            return 0;
        }
        else if (arg == "-d" || arg == "--debug") {
            debug = true;
        }
        else if (arg == "-c" || arg == "--config") {
            if (i + 1 < argc) {
                configFile = argv[++i];
            }
            else {
                std::cerr << "Error: --config requires a filename\n";
                return 1;
            }
        }
    }

    // Initialize logging
    ds2::utils::Logger::init("gw2-server.log", debug);
    
    LOG_INFO("===========================================");
    LOG_INFO("  Garden Warfare 2 Server Emulator");
    LOG_INFO("  Version 0.1.0");
    LOG_INFO("===========================================");
    
    // Load configuration
    auto& config = ds2::utils::Config::instance();
    
    if (!configFile.empty()) {
        if (config.loadFromFile(configFile)) {
            LOG_INFO("Loaded configuration from {}", configFile);
        }
        else {
            LOG_WARN("Failed to load config file: {}", configFile);
        }
    }
    
    // Set up signal handlers
    std::signal(SIGINT, signalHandler);
    std::signal(SIGTERM, signalHandler);
    
    // Create and initialize server
    ds2::Server server;
    g_server = &server;
    
    if (!server.init(config.getServerConfig())) {
        LOG_ERROR("Failed to initialize server");
        ds2::utils::Logger::shutdown();
        return 1;
    }
    
    // Start server
    server.start();
    
    LOG_INFO("Server is running. Press Ctrl+C to stop.");
    
    // Run server (blocking)
    server.run();
    
    LOG_INFO("Server shutdown complete");
    
    g_server = nullptr;
    ds2::utils::Logger::shutdown();
    
    return 0;
}
