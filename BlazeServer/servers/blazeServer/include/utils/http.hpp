#pragma once

#include <string>
#include <nlohmann/json.hpp>

namespace gw2::utils {

    // Blocking HTTP(S) GET that returns the parsed JSON response body.
    //   nlohmann::json data = httpGet("https://127.0.0.1:42220/gw2/live/blackmarket");
    // Throws std::runtime_error on any transport / status / parse failure, so
    // callers can try/catch and fall back to local data. HTTPS connects with
    // certificate verification disabled (the editorial server uses a self-signed
    // cert locally; a real cert on a VPS works too).
    nlohmann::json httpGet(const std::string& url);

    // POST a JSON body and return the parsed JSON response (empty object if the
    // response has no body). Same TLS/error/throw behavior as httpGet.
    nlohmann::json httpPost(const std::string& url, const nlohmann::json& body);

}
