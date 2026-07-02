//
// Created by user on 6/28/2026.
//
#include "utils/json.hpp"
#include "utils/logger.hpp"

#include <cstdio>
#include <fstream>
#include <string>

namespace gw2::utils {

template<typename J>
J loadFile(const std::string& path) {
    std::ifstream file(path, std::ios::binary);
    if (!file) {
        LOG_ERROR("[util] {} missing", path);
        return J::object();
    }
    if (file.peek() == std::ifstream::traits_type::eof()) {
        LOG_ERROR("[util] {} is empty", path);
        return J::object();
    }
    try {
        return J::parse(file);
    }
    catch (const std::exception& e) {
        LOG_ERROR("[util] {} parse error: {}", path, e.what());
        return J::object();
    }
}

template<typename J>
bool saveFile(const std::string& path, const J& data, int indent) {
    const std::string tmp = path + ".tmp";
    {
        std::ofstream file(tmp, std::ios::binary | std::ios::trunc);
        if (!file) {
            LOG_ERROR("[util] cannot open {} for writing", path);
            return false;
        }
        file << data.dump(indent);
        if (!file) {
            LOG_ERROR("[util] write error for {}", path);
            return false;
        }
    }
    std::remove(path.c_str());
    if (std::rename(tmp.c_str(), path.c_str()) != 0) {
        LOG_ERROR("[util] rename failed for {}", path);
        return false;
    }
    return true;
}

const nlohmann::json& dataSection(const std::string& key) {
    static const nlohmann::json root  = loadFile<nlohmann::json>("data/data.json");
    static const nlohmann::json empty = nlohmann::json::object();
    auto it = root.find(key);
    if (it == root.end()) {
        LOG_ERROR("[util] data.json has no section '{}'", key);
        return empty;
    }
    return *it;
}

template nlohmann::json         loadFile<nlohmann::json>(const std::string&);
template nlohmann::ordered_json loadFile<nlohmann::ordered_json>(const std::string&);
template bool saveFile<nlohmann::json>(const std::string&, const nlohmann::json&, int);
template bool saveFile<nlohmann::ordered_json>(const std::string&, const nlohmann::ordered_json&, int);

}
