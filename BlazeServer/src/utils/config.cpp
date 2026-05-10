#include "utils/config.hpp"
#include <fstream>
#include <sstream>

namespace ds2::utils {

Config& Config::instance() {
    static Config config;
    return config;
}

bool Config::loadFromFile(const std::string& path) {
    std::ifstream file(path);
    if (!file.is_open()) {
        return false;
    }
    
    std::string line;
    while (std::getline(file, line)) {
        // Skip comments and empty lines
        if (line.empty() || line[0] == '#') {
            continue;
        }
        
        size_t eqPos = line.find('=');
        if (eqPos == std::string::npos) {
            continue;
        }
        
        std::string key = line.substr(0, eqPos);
        std::string value = line.substr(eqPos + 1);
        
        // Trim whitespace
        auto trim = [](std::string& s) {
            s.erase(0, s.find_first_not_of(" \t"));
            s.erase(s.find_last_not_of(" \t") + 1);
        };
        trim(key);
        trim(value);
        
        // Parse known keys
        if (key == "redirector_host") {
            m_config.redirector_host = value;
        }
        else if (key == "redirector_port") {
            m_config.redirector_port = static_cast<uint16_t>(std::stoi(value));
        }
        else if (key == "blaze_host") {
            m_config.blaze_host = value;
        }
        else if (key == "blaze_port") {
            m_config.blaze_port = static_cast<uint16_t>(std::stoi(value));
        }
        else if (key == "qos_host") {
            m_config.qos_host = value;
        }
        else if (key == "qos_port") {
            m_config.qos_port = static_cast<uint16_t>(std::stoi(value));
        }
        else if (key == "ssl_cert") {
            m_config.ssl_cert_path = value;
        }
        else if (key == "ssl_key") {
            m_config.ssl_key_path = value;
        }
    }
    
    return true;
}

bool Config::saveToFile(const std::string& path) {
    std::ofstream file(path);
    if (!file.is_open()) {
        return false;
    }
    
    file << "# Garden Warfare 2 Server Configuration\n\n";
    
    file << "# Blaze Server (main game server)\n";
    file << "blaze_host = " << m_config.blaze_host << "\n";
    file << "blaze_port = " << m_config.blaze_port << "\n\n";
    
    file << "# QoS Server (NAT detection)\n";
    file << "qos_host = " << m_config.qos_host << "\n";
    file << "qos_port = " << m_config.qos_port << "\n\n";
    
    file << "# SSL Configuration\n";
    file << "ssl_cert = " << m_config.ssl_cert_path << "\n";
    file << "ssl_key = " << m_config.ssl_key_path << "\n";
    
    return true;
}

} // namespace ds2::utils
