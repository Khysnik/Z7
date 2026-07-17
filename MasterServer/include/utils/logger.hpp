#pragma once

#include <spdlog/spdlog.h>
#include <spdlog/sinks/stdout_color_sinks.h>
#include <spdlog/sinks/rotating_file_sink.h>
#include <memory>
#include <string>

namespace gw2::utils {

class Logger {
public:
    static void init(const std::string& logFile = "gw2-server.log",
                     bool debug = false);
    static void shutdown();
    
    static std::shared_ptr<spdlog::logger> get() { return s_logger; }
    
    template<typename... Args>
    static void trace(fmt::format_string<Args...> fmt, Args&&... args) {
        s_logger->trace(fmt, std::forward<Args>(args)...);
    }
    
    template<typename... Args>
    static void debug(fmt::format_string<Args...> fmt, Args&&... args) {
        s_logger->debug(fmt, std::forward<Args>(args)...);
    }
    
    template<typename... Args>
    static void info(fmt::format_string<Args...> fmt, Args&&... args) {
        s_logger->info(fmt, std::forward<Args>(args)...);
    }
    
    template<typename... Args>
    static void warn(fmt::format_string<Args...> fmt, Args&&... args) {
        s_logger->warn(fmt, std::forward<Args>(args)...);
    }
    
    template<typename... Args>
    static void error(fmt::format_string<Args...> fmt, Args&&... args) {
        s_logger->error(fmt, std::forward<Args>(args)...);
    }
    
    template<typename... Args>
    static void critical(fmt::format_string<Args...> fmt, Args&&... args) {
        s_logger->critical(fmt, std::forward<Args>(args)...);
    }
    
private:
    static std::shared_ptr<spdlog::logger> s_logger;
};

#define LOG_TRACE(...) gw2::utils::Logger::trace(__VA_ARGS__)
#define LOG_DEBUG(...) gw2::utils::Logger::debug(__VA_ARGS__)
#define LOG_INFO(...)  gw2::utils::Logger::info(__VA_ARGS__)
#define LOG_WARN(...)  gw2::utils::Logger::warn(__VA_ARGS__)
#define LOG_ERROR(...) gw2::utils::Logger::error(__VA_ARGS__)
#define LOG_CRITICAL(...) gw2::utils::Logger::critical(__VA_ARGS__)

} // namespace gw2::utils
