#pragma once

#include <cstdint>
#include <string>

namespace gosredirector {

struct ParsedHttpRequest {
    std::string method;
    std::string target;
    std::string version;
};

void logInfo(const std::string& message);
void logWarn(const std::string& message);
void logError(const std::string& message);

std::string buildServerInstanceXmlBody();

bool parseHttpRequest(const std::string& headers, ParsedHttpRequest& request);

} // namespace gosredirector
