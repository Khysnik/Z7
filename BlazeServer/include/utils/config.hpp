#pragma once

#include "blaze/types.hpp"
#include <string>
#include <optional>

namespace ds2::utils {

/**
 * Configuration manager
 * 
 * Loads and manages server configuration from file or defaults.
 */
class Config {
public:
    static Config& instance();
    
    // Load config from file
    bool loadFromFile(const std::string& path);
    
    // Save config to file
    bool saveToFile(const std::string& path);
    
    // Get server config
    const blaze::ServerConfig& getServerConfig() const { return m_config; }
    blaze::ServerConfig& getServerConfig() { return m_config; }
    
    // Individual settings
    void setRedirectorPort(uint16_t port) { m_config.redirector_port = port; }
    void setBlazePort(uint16_t port) { m_config.blaze_port = port; }
    void setQoSPort(uint16_t port) { m_config.qos_port = port; }
    
    void setSslCertPath(const std::string& path) { m_config.ssl_cert_path = path; }
    void setSslKeyPath(const std::string& path) { m_config.ssl_key_path = path; }
    
private:
    Config() = default;
    blaze::ServerConfig m_config;
};

} // namespace ds2::utils
