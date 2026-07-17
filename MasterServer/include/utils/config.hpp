#pragma once

#include "blaze/types.hpp"
#include <string>
#include <optional>

namespace gw2::utils {

class Config {
public:
    static Config& instance();

    const blaze::ServerConfig& getServerConfig() const { return m_config; }
    blaze::ServerConfig& getServerConfig() { return m_config; }

private:
    Config() = default;
    blaze::ServerConfig m_config;
};

} // namespace gw2::utils
