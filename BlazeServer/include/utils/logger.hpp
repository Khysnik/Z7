#pragma once

#include <spdlog/spdlog.h>
#include <spdlog/sinks/stdout_color_sinks.h>
#include <spdlog/sinks/rotating_file_sink.h>
#include <memory>
#include <string>

namespace ds2::utils {

/**
 * Logger utility
 * 
 * Provides a centralized logging interface using spdlog.
 */
class Logger {
public:
    /// Initialize logging.
    /// @param logFile rotating file destination — always captures DEBUG.
    /// @param debug   if true, console also shows DEBUG (raw byte dumps,
    ///                dispatcher boilerplate). If false, console shows INFO
    ///                and above (parsed command names + TDF bodies).
    static void init(const std::string& logFile = "ds2-server.log",
                     bool debug = false);
    static void shutdown();
    
    static std::shared_ptr<spdlog::logger> get() { return s_logger; }
    
    // Convenience logging functions
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

// Shorthand macro for logging with component name
#define LOG_TRACE(...) ds2::utils::Logger::trace(__VA_ARGS__)
#define LOG_DEBUG(...) ds2::utils::Logger::debug(__VA_ARGS__)
#define LOG_INFO(...)  ds2::utils::Logger::info(__VA_ARGS__)
#define LOG_WARN(...)  ds2::utils::Logger::warn(__VA_ARGS__)
#define LOG_ERROR(...) ds2::utils::Logger::error(__VA_ARGS__)
#define LOG_CRITICAL(...) ds2::utils::Logger::critical(__VA_ARGS__)

} // namespace ds2::utils
