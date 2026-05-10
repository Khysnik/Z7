cmake_minimum_required(VERSION 3.16)
project(ds2-server VERSION 0.1.0 LANGUAGES CXX C)

set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)
set(CMAKE_EXPORT_COMPILE_COMMANDS ON)

# Build type
if(NOT CMAKE_BUILD_TYPE)
    set(CMAKE_BUILD_TYPE Debug)
endif()

# Compiler flags
if(CMAKE_CXX_COMPILER_ID MATCHES "GNU|Clang")
    add_compile_options(-Wall -Wextra -Wpedantic)
    if(CMAKE_BUILD_TYPE STREQUAL "Debug")
        add_compile_options(-g -O0)
    else()
        add_compile_options(-O2)
    endif()
endif()

# =============================================================================
# Dependencies (git submodules)
# =============================================================================

# Asio (standalone, header-only)
set(ASIO_INCLUDE_DIR ${CMAKE_CURRENT_SOURCE_DIR}/extern/asio/asio/include)
if(NOT EXISTS ${ASIO_INCLUDE_DIR}/asio.hpp)
    message(FATAL_ERROR "Asio not found. Run: git submodule update --init")
endif()

# spdlog for logging
add_subdirectory(extern/spdlog)

# =============================================================================
# OpenSSL 1.1.1 (Bundled for TLS 1.0 compatibility with Garden Warfare 2)
# =============================================================================
# Garden Warfare 2 (2011) uses OpenSSL 1.0.0b which requires TLS 1.0 support.
# Modern OpenSSL 3.x disables TLS 1.0/1.1 by default.
# We bundle OpenSSL 1.1.1w (last LTS release) which natively supports TLS 1.0.

include(ExternalProject)

set(OPENSSL_VERSION "1.1.1w")
set(OPENSSL_PREFIX ${CMAKE_BINARY_DIR}/openssl)
set(OPENSSL_INSTALL_DIR ${OPENSSL_PREFIX}/install)
set(OPENSSL_INCLUDE_DIR ${OPENSSL_INSTALL_DIR}/include)
set(OPENSSL_LIB_DIR ${OPENSSL_INSTALL_DIR}/lib)

# Download and build OpenSSL 1.1.1
# IMPORTANT: We must enable SSLv3 because Garden Warfare 2 uses SSLv3 protocol
# This is insecure but required for compatibility with this 2011 game
ExternalProject_Add(openssl_external
    URL https://www.openssl.org/source/openssl-${OPENSSL_VERSION}.tar.gz
    URL_HASH SHA256=cf3098950cb4d853ad95c0841f1f9c6d3dc102dccfcacd521d93925208b76ac8
    DOWNLOAD_EXTRACT_TIMESTAMP TRUE
    PREFIX ${OPENSSL_PREFIX}
    CONFIGURE_COMMAND <SOURCE_DIR>/config
        --prefix=${OPENSSL_INSTALL_DIR}
        --openssldir=${OPENSSL_INSTALL_DIR}/ssl
        no-shared
        no-tests
        enable-ssl3
        enable-ssl3-method
        enable-weak-ssl-ciphers
        -fPIC
    BUILD_COMMAND make -j4
    INSTALL_COMMAND make install_sw
    BUILD_IN_SOURCE 1
    LOG_DOWNLOAD ON
    LOG_CONFIGURE ON
    LOG_BUILD ON
    LOG_INSTALL ON
)

# Create minimal openssl.cnf after OpenSSL is built (needed for cert generation)
file(WRITE ${CMAKE_BINARY_DIR}/openssl.cnf.in 
"# Minimal OpenSSL configuration for DS2 server certificate generation
[ req ]
default_bits       = 2048
distinguished_name = req_distinguished_name
x509_extensions    = v3_ca

[ req_distinguished_name ]

[ v3_ca ]
basicConstraints = CA:TRUE
")

add_custom_command(TARGET openssl_external POST_BUILD
    COMMAND ${CMAKE_COMMAND} -E make_directory ${OPENSSL_INSTALL_DIR}/ssl
    COMMAND ${CMAKE_COMMAND} -E copy_if_different 
        ${CMAKE_BINARY_DIR}/openssl.cnf.in 
        ${OPENSSL_INSTALL_DIR}/ssl/openssl.cnf
    COMMENT "Creating minimal openssl.cnf for certificate generation"
)

# Create imported targets for OpenSSL
add_library(OpenSSL::SSL STATIC IMPORTED GLOBAL)
add_library(OpenSSL::Crypto STATIC IMPORTED GLOBAL)

set_target_properties(OpenSSL::SSL PROPERTIES
    IMPORTED_LOCATION ${OPENSSL_LIB_DIR}/libssl.a
    INTERFACE_INCLUDE_DIRECTORIES ${OPENSSL_INCLUDE_DIR}
)

set_target_properties(OpenSSL::Crypto PROPERTIES
    IMPORTED_LOCATION ${OPENSSL_LIB_DIR}/libcrypto.a
    INTERFACE_INCLUDE_DIRECTORIES ${OPENSSL_INCLUDE_DIR}
)

# Ensure OpenSSL is built before the main target
add_dependencies(OpenSSL::SSL openssl_external)
add_dependencies(OpenSSL::Crypto openssl_external)

# =============================================================================
# Main Server Library
# =============================================================================

set(SERVER_SOURCES
    # Core
    src/main.cpp
    src/server.cpp
    
    # Blaze Protocol
    src/blaze/packet.cpp
    src/blaze/tdf.cpp
    src/blaze/component.cpp
    
    # Network
    src/network/ssl_server.cpp
    src/network/client_connection.cpp
    src/network/qos_server.cpp
    
    # Components
    src/components/redirector.cpp
    src/components/authentication.cpp
    src/components/util.cpp
    src/components/user_sessions.cpp
    src/components/pvz_gw.cpp
    src/components/packs.cpp
    src/components/game_manager.cpp
    
    # Utils
    src/utils/logger.cpp
    src/utils/config.cpp
)

set(SERVER_HEADERS
    # Core
    include/server.hpp
    
    # Blaze Protocol
    include/blaze/packet.hpp
    include/blaze/tdf.hpp
    include/blaze/component.hpp
    include/blaze/types.hpp
    
    # Network
    include/network/ssl_server.hpp
    include/network/client_connection.hpp
    include/network/qos_server.hpp
    
    # Components
    include/components/redirector.hpp
    include/components/authentication.hpp
    include/components/util.hpp
    include/components/user_sessions.hpp
    include/components/pvz_gw.hpp
    include/components/packs.hpp
    include/components/game_manager.hpp
    
    # Utils
    include/utils/logger.hpp
    include/utils/config.hpp
)

add_executable(ds2-server ${SERVER_SOURCES} ${SERVER_HEADERS})

# Ensure OpenSSL is built first
add_dependencies(ds2-server openssl_external)

target_include_directories(ds2-server PRIVATE
    ${CMAKE_CURRENT_SOURCE_DIR}/include
    ${ASIO_INCLUDE_DIR}
    ${OPENSSL_INCLUDE_DIR}
)

target_link_libraries(ds2-server PRIVATE
    ${OPENSSL_LIB_DIR}/libssl.a
    ${OPENSSL_LIB_DIR}/libcrypto.a
    spdlog::spdlog
    pthread
    dl  # Required by OpenSSL on Linux
)

# Define ASIO_STANDALONE to use without Boost
target_compile_definitions(ds2-server PRIVATE
    ASIO_STANDALONE
    ASIO_NO_DEPRECATED
)


# =============================================================================
# Install
# =============================================================================

install(TARGETS ds2-server DESTINATION bin)

# =============================================================================
# Info
# =============================================================================

message(STATUS "")
message(STATUS "Garden Warfare 2 Server Emulator")
message(STATUS "  Version: ${PROJECT_VERSION}")
message(STATUS "  Build type: ${CMAKE_BUILD_TYPE}")
message(STATUS "  C++ Standard: ${CMAKE_CXX_STANDARD}")
message(STATUS "")
