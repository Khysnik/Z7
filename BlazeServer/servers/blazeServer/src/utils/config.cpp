#include "utils/config.hpp"

namespace gw2::utils {

Config& Config::instance() {
    static Config config;
    return config;
}

} // namespace gw2::utils
