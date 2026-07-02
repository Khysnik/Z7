#pragma once

#include <string>
#include <nlohmann/json.hpp>

namespace gw2::utils {

    // Load + parse a json file. Returns an empty json on missing/empty/parse
    // error (logged). Defaults to nlohmann::json; pass nlohmann::ordered_json to
    // preserve key order.
    template<typename J = nlohmann::json>
    J loadFile(const std::string& path);

    // Atomically write a json file (temp file + rename, so a crash mid-write
    // can't corrupt it). Returns false on error (logged).
    template<typename J = nlohmann::json>
    bool saveFile(const std::string& path, const J& data, int indent = 2);

    // Read-only consolidated catalog: returns the named section of data/data.json
    // (loaded and cached once on first use). Returns a static empty object if the
    // section is absent. The reference stays valid for the process lifetime.
    const nlohmann::json& dataSection(const std::string& key);

}
