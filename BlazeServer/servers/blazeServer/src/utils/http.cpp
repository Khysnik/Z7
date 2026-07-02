#include "utils/http.hpp"

#define CPPHTTPLIB_OPENSSL_SUPPORT
#include <httplib.h>

#include <stdexcept>
#include <string>
#include <utility>

namespace gw2::utils {

namespace {

std::pair<std::string, std::string> splitUrl(const std::string& url) {
    const auto schemeEnd = url.find("://");
    if (schemeEnd == std::string::npos)
        throw std::runtime_error("http: url has no scheme: " + url);
    const auto pathStart = url.find('/', schemeEnd + 3);
    if (pathStart == std::string::npos)
        return { url, "/" };
    return { url.substr(0, pathStart), url.substr(pathStart) };
}

httplib::Client makeClient(const std::string& base) {
    httplib::Client cli(base);
    cli.enable_server_certificate_verification(false);
    cli.set_connection_timeout(5, 0);
    cli.set_read_timeout(5, 0);
    return cli;
}

void checkResult(const httplib::Result& res, const char* what, const std::string& url) {
    if (!res)
        throw std::runtime_error(std::string(what) + ": request failed ("
                                 + httplib::to_string(res.error()) + ") for " + url);
    if (res->status < 200 || res->status >= 300)
        throw std::runtime_error(std::string(what) + ": HTTP status "
                                 + std::to_string(res->status) + " for " + url);
}

} // namespace

nlohmann::json httpGet(const std::string& url) {
    auto [base, path] = splitUrl(url);
    auto cli = makeClient(base);
    auto res = cli.Get(path);
    checkResult(res, "httpGet", url);
    return nlohmann::json::parse(res->body);
}

nlohmann::json httpPost(const std::string& url, const nlohmann::json& body) {
    auto [base, path] = splitUrl(url);
    auto cli = makeClient(base);
    auto res = cli.Post(path, body.dump(), "application/json");
    checkResult(res, "httpPost", url);
    return res->body.empty() ? nlohmann::json::object() : nlohmann::json::parse(res->body);
}

} // namespace gw2::utils
