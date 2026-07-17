cmake_minimum_required(VERSION 3.16)
project(gw2-server VERSION 0.1.0 LANGUAGES CXX C)

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
elseif(MSVC)
    add_compile_options(/W3 /permissive-)
    # Asio + Win headers: avoid min/max macros and bloated windows.h
    add_compile_definitions(WIN32_LEAN_AND_MEAN NOMINMAX _WIN32_WINNT=0x0601)
endif()


# Asio (standalone, header-only)
set(ASIO_INCLUDE_DIR ${CMAKE_CURRENT_SOURCE_DIR}/extern/asio/include)
if(NOT EXISTS ${ASIO_INCLUDE_DIR}/asio.hpp)
    message(FATAL_ERROR "Asio not found. Run: git submodule update --init")
endif()

# spdlog for logging
add_subdirectory(extern/spdlog)

include(ExternalProject)

set(OPENSSL_VERSION "1.1.1w")
set(OPENSSL_SHA256 "cf3098950cb4d853ad95c0841f1f9c6d3dc102dccfcacd521d93925208b76ac8")
set(OPENSSL_PREFIX ${CMAKE_BINARY_DIR}/openssl)
set(OPENSSL_INSTALL_DIR ${OPENSSL_PREFIX}/install)
set(OPENSSL_INCLUDE_DIR ${OPENSSL_INSTALL_DIR}/include)
set(OPENSSL_LIB_DIR ${OPENSSL_INSTALL_DIR}/lib)

file(MAKE_DIRECTORY ${OPENSSL_INCLUDE_DIR})

if(WIN32 AND MSVC)

    # OpenSSL 1.1.1 with MSVC installs static libs as libssl.lib / libcrypto.lib
    set(OPENSSL_SSL_LIB    ${OPENSSL_LIB_DIR}/libssl.lib)
    set(OPENSSL_CRYPTO_LIB ${OPENSSL_LIB_DIR}/libcrypto.lib)

    if(CMAKE_SIZEOF_VOID_P EQUAL 8)
        set(OPENSSL_TARGET VC-WIN64A)
    else()
        set(OPENSSL_TARGET VC-WIN32)
    endif()

    ExternalProject_Add(openssl_external
            URL https://www.openssl.org/source/openssl-${OPENSSL_VERSION}.tar.gz
            URL_HASH SHA256=${OPENSSL_SHA256}
            DOWNLOAD_EXTRACT_TIMESTAMP TRUE
            PREFIX ${OPENSSL_PREFIX}
            CONFIGURE_COMMAND perl <SOURCE_DIR>/Configure ${OPENSSL_TARGET}
            --prefix=${OPENSSL_INSTALL_DIR}
            --openssldir=${OPENSSL_INSTALL_DIR}/ssl
            no-shared
            no-tests
            enable-ssl3
            enable-ssl3-method
            enable-weak-ssl-ciphers
            BUILD_COMMAND nmake
            INSTALL_COMMAND nmake install_sw
            BUILD_IN_SOURCE 1
            BUILD_BYPRODUCTS ${OPENSSL_SSL_LIB} ${OPENSSL_CRYPTO_LIB}
            LOG_DOWNLOAD ON
            LOG_CONFIGURE ON
            LOG_BUILD ON
            LOG_INSTALL ON
    )

    set(OPENSSL_SYSTEM_LIBS ws2_32 crypt32 advapi32 user32)

elseif(WIN32)

    set(OPENSSL_SSL_LIB    ${OPENSSL_LIB_DIR}/libssl.a)
    set(OPENSSL_CRYPTO_LIB ${OPENSSL_LIB_DIR}/libcrypto.a)

    if(CMAKE_SIZEOF_VOID_P EQUAL 8)
        set(OPENSSL_TARGET mingw64)
    else()
        set(OPENSSL_TARGET mingw)
    endif()

    # Use the make program CMake found for this toolchain (mingw32-make).
    if(CMAKE_MAKE_PROGRAM AND CMAKE_MAKE_PROGRAM MATCHES "make")
        set(OPENSSL_MAKE ${CMAKE_MAKE_PROGRAM})
    else()
        set(OPENSSL_MAKE mingw32-make)
    endif()

    ExternalProject_Add(openssl_external
            URL https://www.openssl.org/source/openssl-${OPENSSL_VERSION}.tar.gz
            URL_HASH SHA256=${OPENSSL_SHA256}
            DOWNLOAD_EXTRACT_TIMESTAMP TRUE
            PREFIX ${OPENSSL_PREFIX}
            CONFIGURE_COMMAND perl <SOURCE_DIR>/Configure ${OPENSSL_TARGET}
            --prefix=${OPENSSL_INSTALL_DIR}
            --openssldir=${OPENSSL_INSTALL_DIR}/ssl
            no-shared
            no-tests
            enable-ssl3
            enable-ssl3-method
            enable-weak-ssl-ciphers
            BUILD_COMMAND ${OPENSSL_MAKE} -j4
            INSTALL_COMMAND ${OPENSSL_MAKE} install_sw
            BUILD_IN_SOURCE 1
            BUILD_BYPRODUCTS ${OPENSSL_SSL_LIB} ${OPENSSL_CRYPTO_LIB}
            LOG_DOWNLOAD ON
            LOG_CONFIGURE ON
            LOG_BUILD ON
            LOG_INSTALL ON
    )

    set(OPENSSL_SYSTEM_LIBS ws2_32 crypt32)

else()

    set(OPENSSL_SSL_LIB    ${OPENSSL_LIB_DIR}/libssl.a)
    set(OPENSSL_CRYPTO_LIB ${OPENSSL_LIB_DIR}/libcrypto.a)

    ExternalProject_Add(openssl_external
            URL https://www.openssl.org/source/openssl-${OPENSSL_VERSION}.tar.gz
            URL_HASH SHA256=${OPENSSL_SHA256}
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
            BUILD_BYPRODUCTS ${OPENSSL_SSL_LIB} ${OPENSSL_CRYPTO_LIB}
            LOG_DOWNLOAD ON
            LOG_CONFIGURE ON
            LOG_BUILD ON
            LOG_INSTALL ON
    )

    # System libs OpenSSL needs on Linux
    set(OPENSSL_SYSTEM_LIBS pthread dl)
endif()

# Create openssl.cnf after OpenSSL is built
file(WRITE ${CMAKE_BINARY_DIR}/openssl.cnf.in
        "# Minimal OpenSSL configuration for server certificate generation
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
        IMPORTED_LOCATION ${OPENSSL_SSL_LIB}
        INTERFACE_INCLUDE_DIRECTORIES ${OPENSSL_INCLUDE_DIR}
)

set_target_properties(OpenSSL::Crypto PROPERTIES
        IMPORTED_LOCATION ${OPENSSL_CRYPTO_LIB}
        INTERFACE_INCLUDE_DIRECTORIES ${OPENSSL_INCLUDE_DIR}
)

# Ensure OpenSSL is built before anything that links it
add_dependencies(OpenSSL::SSL openssl_external)
add_dependencies(OpenSSL::Crypto openssl_external)


set(SERVER_SOURCES
        src/main.cpp
        src/server.cpp
        src/config.cpp

        src/blaze/packet.cpp
        src/blaze/tdf.cpp
        src/blaze/component.cpp

        src/network/ssl_server.cpp
        src/network/client_connection.cpp
        src/network/qos_udp_server.cpp

        src/components/authentication.cpp
        src/components/util.cpp
        src/components/user_sessions.cpp
        src/components/pvz_gw.cpp
        src/components/packs.cpp
        src/components/inventory.cpp
        src/components/game_manager.cpp
        src/components/stats.cpp
        src/components/game_reporting.cpp

        src/data/player_profile.cpp
        src/data/inventory.cpp
        src/data/packs.cpp
        src/data/loot.cpp

        src/utils/logger.cpp
        src/utils/config.cpp
        src/utils/json.cpp
        src/utils/http.cpp
)

set(SERVER_HEADERS
        include/server.hpp
        include/config.hpp

        include/blaze/packet.hpp
        include/blaze/tdf.hpp
        include/blaze/component.hpp
        include/blaze/types.hpp

        include/network/ssl_server.hpp
        include/network/client_connection.hpp
        include/network/qos_udp_server.hpp

        include/components/authentication.hpp
        include/components/util.hpp
        include/components/user_sessions.hpp
        include/components/pvz_gw.hpp
        include/components/packs.hpp
        include/components/inventory.hpp
        include/components/game_manager.hpp
        include/components/stats.hpp
        include/components/game_reporting.hpp

        include/data/licenses.hpp
        include/data/inventory.hpp
        include/data/player_profile.hpp
        include/data/packs.hpp
        include/data/loot.hpp

        include/utils/logger.hpp
        include/utils/config.hpp
        include/utils/json.hpp
        include/utils/http.hpp
        include/utils/editorial.hpp
)

add_executable(gw2-server ${SERVER_SOURCES} ${SERVER_HEADERS})

# Ensure OpenSSL is built first
add_dependencies(gw2-server openssl_external)

target_include_directories(gw2-server PRIVATE
        ${CMAKE_CURRENT_SOURCE_DIR}/include
        ${CMAKE_CURRENT_SOURCE_DIR}/extern
        ${ASIO_INCLUDE_DIR}
        ${OPENSSL_INCLUDE_DIR}
)

target_link_libraries(gw2-server PRIVATE
        OpenSSL::SSL
        OpenSSL::Crypto
        spdlog::spdlog
        ${OPENSSL_SYSTEM_LIBS}
)

# Define ASIO_STANDALONE to use without Boost
target_compile_definitions(gw2-server PRIVATE
        ASIO_STANDALONE
        ASIO_NO_DEPRECATED
)

# Seed runtime data next to the executable so relative "data/..." paths resolve
# when running from the build directory. Static files are refreshed every build;
# mutable save files (inventory/bytevault/profile) are seeded only if missing so
# a rebuild never wipes the live save. See cmake/seed_data.cmake.
add_custom_command(TARGET gw2-server POST_BUILD
        COMMAND ${CMAKE_COMMAND}
            -DSRC_DIR=${CMAKE_CURRENT_SOURCE_DIR}/data
            -DDST_DIR=$<TARGET_FILE_DIR:gw2-server>/data
            -P ${CMAKE_CURRENT_SOURCE_DIR}/cmake/seed_data.cmake
        COMMENT "Seeding data/ next to gw2-server (preserving live saves)")

install(TARGETS gw2-server DESTINATION bin)
install(DIRECTORY data/ DESTINATION bin/data)

message(STATUS "")
message(STATUS "Garden Warfare 2 Server Emulator")
message(STATUS "  Version:      ${PROJECT_VERSION}")
message(STATUS "  Build type:   ${CMAKE_BUILD_TYPE}")
message(STATUS "  C++ Standard: ${CMAKE_CXX_STANDARD}")
message(STATUS "  Platform:     ${CMAKE_SYSTEM_NAME}")
if(WIN32 AND MSVC)
    message(STATUS "  OpenSSL tgt:  ${OPENSSL_TARGET} (needs perl + nasm + nmake on PATH)")
elseif(WIN32)
    message(STATUS "  OpenSSL tgt:  ${OPENSSL_TARGET} (needs perl + mingw32-make on PATH)")
endif()
message(STATUS "")
