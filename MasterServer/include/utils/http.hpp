#pragma once

#include <string>
#include <nlohmann/json.hpp>

namespace gw2::utils {

    nlohmann::json httpGet(const std::string& url);

    nlohmann::json httpPost(const std::string& url, const nlohmann::json& body);

}
