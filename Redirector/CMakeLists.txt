cmake_minimum_required(VERSION 3.16)
project(GOSredirector VERSION 0.1.0 LANGUAGES CXX)

set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)
set(CMAKE_EXPORT_COMPILE_COMMANDS ON)

if(NOT CMAKE_BUILD_TYPE)
    set(CMAKE_BUILD_TYPE Release)
endif()

find_path(ASIO_INCLUDE_DIR asio.hpp REQUIRED)
find_package(OpenSSL REQUIRED)
find_package(Threads REQUIRED)

add_executable(GOSredirector
    src/main.cpp
    src/utils.cpp
)

target_include_directories(GOSredirector PRIVATE
    ${ASIO_INCLUDE_DIR}
)

target_link_libraries(GOSredirector PRIVATE
    OpenSSL::SSL
    OpenSSL::Crypto
    Threads::Threads
)

target_compile_definitions(GOSredirector PRIVATE
    ASIO_STANDALONE
    ASIO_NO_DEPRECATED
)

if (CMAKE_CXX_COMPILER_ID MATCHES "GNU|Clang")
    target_compile_options(GOSredirector PRIVATE -Wall -Wextra -Wpedantic)
endif()
