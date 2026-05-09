#include "utils.hpp"

#include <cctype>
#include <ctime>
#include <iomanip>
#include <iostream>
#include <sstream>

namespace gosredirector {
namespace {

std::string nowString() {
    std::time_t raw = std::time(nullptr);
    std::tm tm{};
#if defined(_WIN32)
    localtime_s(&tm, &raw);
#else
    localtime_r(&raw, &tm);
#endif
    std::ostringstream out;
    out << std::put_time(&tm, "%Y-%m-%d %H:%M:%S");
    return out.str();
}

void logLine(const char* level, const std::string& message) {
    std::cout << "[" << nowString() << "] [" << level << "] " << message << std::endl;
}

} // namespace

void logInfo(const std::string& message) { logLine("info", message); }
void logWarn(const std::string& message) { logLine("warn", message); }
void logError(const std::string& message) { logLine("error", message); }

std::string buildServerInstanceXmlBody() {
    return R"(<?xml version="1.0" encoding="UTF-8"?>
<serverinstanceinfo>
  <address>
    <ipAddress>
      <ip>2130706433</ip>
      <port>10041</port>
    </ipAddress>
  </address>
  <secure>1</secure>
  <messages>
    <warnMessage>Garden Warfare 2 Master Server v1.0.0</warnMessage>
  </messages>
</serverinstanceinfo>
)";
}

bool parseHttpRequest(const std::string& headers, ParsedHttpRequest& request) {
    std::istringstream input(headers);
    std::string firstLine;
    if (!std::getline(input, firstLine)) {
        return false;
    }
    if (!firstLine.empty() && firstLine.back() == '\r') {
        firstLine.pop_back();
    }

    std::istringstream lineStream(firstLine);
    lineStream >> request.method >> request.target >> request.version;
    return !request.method.empty() && !request.target.empty() && !request.version.empty();
}

} // namespace gosredirector
