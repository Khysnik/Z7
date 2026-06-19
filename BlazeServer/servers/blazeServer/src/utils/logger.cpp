#include "utils/logger.hpp"

namespace gw2::utils {

std::shared_ptr<spdlog::logger> Logger::s_logger;

void Logger::init(const std::string& logFile, bool debug) {
    try {
        auto console = std::make_shared<spdlog::sinks::stdout_color_sink_mt>();
        console->set_level(debug ? spdlog::level::debug : spdlog::level::info);
        console->set_pattern("[%H:%M:%S.%e] [%^%l%$] %v");

        auto file = std::make_shared<spdlog::sinks::rotating_file_sink_mt>(logFile, 1024 * 1024 * 5, 3);
        file->set_level(spdlog::level::trace);
        file->set_pattern("[%Y-%m-%d %H:%M:%S.%e] [%l] [%t] %v");

        s_logger = std::make_shared<spdlog::logger>("gw2", spdlog::sinks_init_list{ console, file });
        s_logger->set_level(spdlog::level::trace);
        s_logger->flush_on(spdlog::level::warn);

        spdlog::register_logger(s_logger);
        spdlog::set_default_logger(s_logger);
    } catch (const spdlog::spdlog_ex& ex) {
        fprintf(stderr, "Logger init failed: %s\n", ex.what());
    }
}

void Logger::shutdown() {
    if (s_logger) s_logger->flush();
    spdlog::shutdown();
}

} // namespace gw2::utils