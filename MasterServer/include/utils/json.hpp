#pragma once

#include <string>
#include <nlohmann/json.hpp>

namespace gw2::utils {

    template<typename J = nlohmann::json>
    J loadFile(const std::string& path);

    template<typename J = nlohmann::json>
    bool saveFile(const std::string& path, const J& data, int indent = 2);

    const nlohmann::json& dataSection(const std::string& key);

}
