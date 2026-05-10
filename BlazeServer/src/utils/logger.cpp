#include "utils/logger.hpp"

namespace ds2::utils {

std::shared_ptr<spdlog::logger> Logger::s_logger;

void Logger::init(const std::string& logFile, bool debug) {
    try {
        // Console: INFO by default (parsed command names + TDF bodies),
        // DEBUG when --debug is passed (adds raw byte dumps + dispatcher boilerplate).
        auto console_sink = std::make_shared<spdlog::sinks::stdout_color_sink_mt>();
        console_sink->set_level(debug ? spdlog::level::debug : spdlog::level::info);
        console_sink->set_pattern("[%H:%M:%S.%e] [%^%l%$] %v");

        // File: always captures DEBUG so a post-mortem has full detail.
        auto file_sink = std::make_shared<spdlog::sinks::rotating_file_sink_mt>(
            logFile, 1024 * 1024 * 5, 3); // 5MB, 3 files
        file_sink->set_level(spdlog::level::trace);
        file_sink->set_pattern("[%Y-%m-%d %H:%M:%S.%e] [%l] [%t] %v");

        s_logger = std::make_shared<spdlog::logger>("ds2",
            spdlog::sinks_init_list{console_sink, file_sink});

        // Logger-level filter must be the most permissive of any sink.
        s_logger->set_level(spdlog::level::trace);
        s_logger->flush_on(spdlog::level::warn);

        spdlog::register_logger(s_logger);
        spdlog::set_default_logger(s_logger);
    }
    catch (const spdlog::spdlog_ex& ex) {
        fprintf(stderr, "Logger init failed: %s\n", ex.what());
    }
}

void Logger::shutdown() {
    if (s_logger) {
        s_logger->flush();
    }
    spdlog::shutdown();
}

} // namespace ds2::utils
