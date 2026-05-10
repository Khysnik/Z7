#include "server.hpp"
#include "blaze/component.hpp"
#include "blaze/tdf.hpp"
#include "network/client_connection.hpp"
#include "components/redirector.hpp"
#include "components/authentication.hpp"
#include "components/util.hpp"
#include "components/game_manager.hpp"
#include "components/user_sessions.hpp"
#include "components/pvz_gw.hpp"
#include "components/packs.hpp"
#include "utils/logger.hpp"
#include <arpa/inet.h>
#include <sstream>

namespace ds2 {

Server::Server()
    : m_running(false)
{
}

Server::~Server() {
    stop();
}

bool Server::init(const blaze::ServerConfig& config) {
    m_config = config;
    
    LOG_INFO("Starting — blaze={}:{} qos={}:{}",
        config.blaze_host, config.blaze_port,
        config.qos_host, config.qos_port);
    
    // Register components
    setupComponents();
    
    // Create servers
    
    m_blazeServer = std::make_shared<network::SSLServer>(
        m_io_context, config.blaze_host, config.blaze_port);
    
    m_qosServer = std::make_shared<network::QoSServer>(
        m_io_context, config.qos_host, config.qos_port);
    
    // Configure SSL
    
    if (!m_blazeServer->configureSsl(config.ssl_cert_path, config.ssl_key_path)) {
        LOG_ERROR("Failed to configure SSL for blaze server");
        return false;
    }
    
    m_blazeServer->setConnectionHandler(
        [this](auto socket) { handleBlazeConnection(socket); });
    
    return true;
}

void Server::setupComponents() {
    auto& registry = blaze::ComponentRegistry::instance();
    
    // Create and configure Redirector
    auto redirector = std::make_shared<components::Redirector>();
    redirector->setBlazeServerAddress(m_config.blaze_host, m_config.blaze_port);
    registry.registerComponent(redirector);
    
    // Create Authentication
    registry.registerComponent(std::make_shared<components::Authentication>());
    
    // Create Util
    registry.registerComponent(std::make_shared<components::Util>());
    
    // Create UserSessions
    registry.registerComponent(std::make_shared<components::UserSessions>());

    registry.registerComponent(std::make_shared<components::PvzGwComponent>());
    registry.registerComponent(std::make_shared<components::PacksComponent>());

    // Create GameManager
    registry.registerComponent(std::make_shared<components::GameManager>());
    
}

void Server::start() {
    if (m_running) return;
    
    m_running = true;
    
    // Start all servers
    m_blazeServer->start();
    m_qosServer->start();
    
    LOG_DEBUG("All servers started");
}

void Server::stop() {
    if (!m_running) return;
    
    LOG_DEBUG("Stopping servers...");
    
    m_running = false;
    m_io_context.stop();
    
    // Stop servers
    if (m_blazeServer) m_blazeServer->stop();
    if (m_qosServer) m_qosServer->stop();
    
    // Wait for worker threads
    for (auto& thread : m_threads) {
        if (thread.joinable()) {
            thread.join();
        }
    }
    m_threads.clear();
    
    LOG_DEBUG("Servers stopped");
}

void Server::run() {
    // Start worker threads
    unsigned int numThreads = std::thread::hardware_concurrency();
    if (numThreads < 2) numThreads = 2;
    
    LOG_DEBUG("Starting {} worker threads", numThreads);
    
    for (unsigned int i = 0; i < numThreads; i++) {
        m_threads.emplace_back([this]() {
            m_io_context.run();
        });
    }
    
    // Wait for threads
    for (auto& thread : m_threads) {
        if (thread.joinable()) {
            thread.join();
        }
    }
}

void Server::handleRedirectorConnection(
    std::shared_ptr<asio::ssl::stream<asio::ip::tcp::socket>> socket
) {
    uint64_t connId = m_nextConnectionId++;

    // Peek the first 16 bytes (full Fire2 header size) to detect HTTP vs binary.
    // GW2 and newer BlazeSDK games use HTTP POST over TLS; DS2-era games use raw binary Blaze.
    auto peekBuf = std::make_shared<std::vector<uint8_t>>(sizeof(blaze::PacketHeader));

    asio::async_read(*socket, asio::buffer(*peekBuf),
        [this, socket, peekBuf, connId](const asio::error_code& ec, size_t) {
            if (ec) {
                LOG_ERROR("[Conn:{}] Redirector peek error: {}", connId, ec.message());
                return;
            }

            const auto& b = *peekBuf;
            bool isPost = (b[0] == 'P' && b[1] == 'O' && b[2] == 'S' && b[3] == 'T' && b[4] == ' ');
            bool isGet  = (b[0] == 'G' && b[1] == 'E' && b[2] == 'T' && b[3] == ' ');

            if (isPost) {
                handleRedirectorHttpConnection(socket, connId, *peekBuf);
            } else if (isGet) {
                handleConnectAuthGetConnection(socket, connId, *peekBuf);
            } else {
                auto client = std::make_shared<network::ClientConnection>(socket, connId);
                client->setPacketHandler([this](auto client, auto packet) {
                    if (!packet) return;
                    if (packet->getMessageType() == blaze::MessageType::Ping) {
                        client->sendPacket(packet->createPingReply());
                        return;
                    }
                    auto& registry = blaze::ComponentRegistry::instance();
                    auto response = registry.routePacket(*packet, client);
                    if (response) client->sendPacket(std::move(response));
                });
                client->setDisconnectHandler([](auto c) {
                    LOG_DEBUG("Redirector client {} disconnected", c->getId());
                });
                client->startWithBuffer(*peekBuf);
            }
        }
    );
}

void Server::handleRedirectorHttpConnection(
    std::shared_ptr<asio::ssl::stream<asio::ip::tcp::socket>> socket,
    uint64_t connId,
    std::vector<uint8_t> peeked
) {
    // Use asio::streambuf to accumulate data; seed it with already-peeked bytes.
    auto buf = std::make_shared<asio::streambuf>();
    std::ostream seedStream(buf.get());
    seedStream.write(reinterpret_cast<const char*>(peeked.data()), peeked.size());

    // Read until end-of-headers marker.
    asio::async_read_until(*socket, *buf, "\r\n\r\n",
        [this, socket, connId, buf](const asio::error_code& ec, size_t headerBytes) {
            if (ec) {
                LOG_ERROR("[Conn:{}] HTTP header read error: {}", connId, ec.message());
                return;
            }

            // Extract header block as string.
            std::string headers(
                asio::buffers_begin(buf->data()),
                asio::buffers_begin(buf->data()) + headerBytes);
            buf->consume(headerBytes);

            // Route Nucleus token requests before reading body.
            if (headers.find("/connect/token") != std::string::npos) {
                size_t contentLen = 0;
                auto clPos = headers.find("Content-Length: ");
                if (clPos == std::string::npos) clPos = headers.find("content-length: ");
                if (clPos != std::string::npos)
                    contentLen = std::stoul(headers.substr(clPos + 16));
                LOG_INFO("[nucleus] POST /connect/token from conn {}", connId);
                handleNucleusPostConnection(socket, connId, headers, contentLen);
                return;
            }

            // Parse Content-Length.
            size_t contentLen = 0;
            auto clPos = headers.find("Content-Length: ");
            if (clPos == std::string::npos) clPos = headers.find("content-length: ");
            if (clPos != std::string::npos) {
                contentLen = std::stoul(headers.substr(clPos + 16));
            }

            // How much of the body do we still need?
            size_t bodyHave = buf->size();
            size_t bodyNeed = contentLen > bodyHave ? contentLen - bodyHave : 0;

            asio::async_read(*socket, *buf, asio::transfer_exactly(bodyNeed),
                [this, socket, connId, buf, contentLen](const asio::error_code& ec, size_t) {
                    if (ec && ec != asio::error::eof) {
                        LOG_ERROR("[Conn:{}] HTTP body read error: {}", connId, ec.message());
                        return;
                    }

                    size_t bodySize = std::min(contentLen, buf->size());
                    std::vector<uint8_t> body(
                        asio::buffers_begin(buf->data()),
                        asio::buffers_begin(buf->data()) + bodySize);

                    std::string bodyStr(body.begin(), body.end());
                    LOG_INFO("[redirector] request ({} bytes)\n{}", body.size(), bodyStr);

                    sendRedirectorHttpResponse(socket, connId, body);
                }
            );
        }
    );
}

void Server::sendRedirectorHttpResponse(
    std::shared_ptr<asio::ssl::stream<asio::ip::tcp::socket>> socket,
    uint64_t connId,
    const std::vector<uint8_t>& /*requestBody*/
) {
    const char* blazeHost = (m_config.blaze_host == "0.0.0.0") ? "127.0.0.1"
                                                                 : m_config.blaze_host.c_str();

    struct in_addr addr;
    inet_pton(AF_INET, blazeHost, &addr);
    uint32_t ipDecimal = ntohl(addr.s_addr);

    std::string xmlBody =
        "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n"
        "<serverinstanceinfo>\n"
        "  <address>\n"
        "    <ipAddress>\n"
        "      <ip>" + std::to_string(ipDecimal) + "</ip>\n"
        "      <port>" + std::to_string(m_config.blaze_port) + "</port>\n"
        "    </ipAddress>\n"
        "  </address>\n"
        "  <secure>1</secure>\n"
        "  <messages>\n"
        "    <warnMessage>Garden Warfare 2 Master Server v1.0.0</warnMessage>\n"
        "  </messages>\n"
        "</serverinstanceinfo>\n";

    std::string httpResponse =
        "HTTP/1.1 200 OK\r\n"
        "Content-Type: application/xml\r\n"
        "Content-Length: " + std::to_string(xmlBody.size()) + "\r\n"
        "Connection: close\r\n"
        "\r\n";

    LOG_INFO("[redirector] reply ({} bytes)\n{}", xmlBody.size(), xmlBody);

    auto responseBuf = std::make_shared<std::vector<uint8_t>>();
    responseBuf->insert(responseBuf->end(), httpResponse.begin(), httpResponse.end());
    responseBuf->insert(responseBuf->end(), xmlBody.begin(), xmlBody.end());

    asio::async_write(*socket, asio::buffer(*responseBuf),
        [socket, connId, responseBuf](const asio::error_code& ec, size_t) {
            if (ec) {
                LOG_ERROR("[redirector] write error: {}", ec.message());
            }
            asio::error_code sec;
            socket->lowest_layer().shutdown(asio::ip::tcp::socket::shutdown_both, sec);
        }
    );
}

void Server::handleConnectAuthGetConnection(
    std::shared_ptr<asio::ssl::stream<asio::ip::tcp::socket>> socket,
    uint64_t connId,
    std::vector<uint8_t> peeked
) {
    auto buf = std::make_shared<asio::streambuf>();
    std::ostream seedStream(buf.get());
    seedStream.write(reinterpret_cast<const char*>(peeked.data()), peeked.size());

    asio::async_read_until(*socket, *buf, "\r\n\r\n",
        [this, socket, connId, buf](const asio::error_code& ec, size_t headerBytes) {
            if (ec) {
                LOG_ERROR("[Conn:{}] GET header read error: {}", connId, ec.message());
                return;
            }

            std::string headers(
                asio::buffers_begin(buf->data()),
                asio::buffers_begin(buf->data()) + headerBytes);
            buf->consume(headerBytes);

            LOG_INFO("[nucleus] GET /connect/auth from conn {}", connId);

            // Extract redirect_uri from query string (URL-decoded).
            // Fall back to the value we advertise in IdentityParams.
            std::string redirectUri = "http://127.0.0.1/callback";
            auto ruPos = headers.find("redirect_uri=");
            if (ruPos != std::string::npos) {
                ruPos += 13;
                auto ruEnd = headers.find_first_of("& \r\n", ruPos);
                std::string encoded = (ruEnd == std::string::npos)
                    ? headers.substr(ruPos)
                    : headers.substr(ruPos, ruEnd - ruPos);
                // Simple percent-decode for the common case (%3A→: %2F→/)
                std::string decoded;
                for (size_t i = 0; i < encoded.size(); ++i) {
                    if (encoded[i] == '%' && i + 2 < encoded.size()) {
                        int val = 0;
                        sscanf(encoded.c_str() + i + 1, "%2x", &val);
                        decoded += static_cast<char>(val);
                        i += 2;
                    } else if (encoded[i] == '+') {
                        decoded += ' ';
                    } else {
                        decoded += encoded[i];
                    }
                }
                if (!decoded.empty()) redirectUri = decoded;
            }

            sendConnectAuthRedirect(socket, connId, redirectUri);
        }
    );
}

void Server::sendConnectAuthRedirect(
    std::shared_ptr<asio::ssl::stream<asio::ip::tcp::socket>> socket,
    uint64_t connId,
    const std::string& redirectUri
) {
    // BlazeSDK's LoginStateInitConsole::idleNucleusLogin expects a 302 with
    // Location: {redirect_uri}?code=VALUE
    // It then parses "code" from the query string.
    std::string location = redirectUri + "?code=FAKE_AUTH_CODE";

    std::string response =
        "HTTP/1.1 302 Found\r\n"
        "Location: " + location + "\r\n"
        "Content-Length: 0\r\n"
        "Connection: close\r\n"
        "\r\n";

    LOG_INFO("[nucleus] 302 /connect/auth redirect → {}", location);

    auto responseBuf = std::make_shared<std::string>(response);
    asio::async_write(*socket, asio::buffer(*responseBuf),
        [socket, connId, responseBuf](const asio::error_code& ec, size_t) {
            if (ec) {
                LOG_ERROR("[nucleus] GET write error {}: {}", connId, ec.message());
            }
            asio::error_code sec;
            socket->lowest_layer().shutdown(asio::ip::tcp::socket::shutdown_both, sec);
        }
    );
}

void Server::handleNucleusPostConnection(
    std::shared_ptr<asio::ssl::stream<asio::ip::tcp::socket>> socket,
    uint64_t connId,
    std::string headers,
    size_t contentLen
) {
    size_t bodyNeed = contentLen;

    auto buf = std::make_shared<asio::streambuf>();
    asio::async_read(*socket, *buf, asio::transfer_exactly(bodyNeed),
        [this, socket, connId, buf](const asio::error_code& ec, size_t) {
            if (ec && ec != asio::error::eof) {
                LOG_ERROR("[nucleus] body read error {}: {}", connId, ec.message());
                return;
            }
            size_t bodySize = buf->size();
            std::string body(asio::buffers_begin(buf->data()),
                             asio::buffers_begin(buf->data()) + bodySize);
            LOG_INFO("[nucleus] POST /connect/token body: {}", body);
            sendNucleusTokenResponse(socket, connId);
        }
    );
}

void Server::sendNucleusTokenResponse(
    std::shared_ptr<asio::ssl::stream<asio::ip::tcp::socket>> socket,
    uint64_t connId
) {
    // Binary parses exactly: "access_token" : "TOKEN" (spaces around colon, space after)
    const std::string body = "{\"access_token\" : \"FAKE_S2S_TOKEN\"}";
    std::string response =
        "HTTP/1.1 200 OK\r\n"
        "Content-Type: application/json\r\n"
        "Content-Length: " + std::to_string(body.size()) + "\r\n"
        "Connection: close\r\n"
        "\r\n" + body;

    LOG_INFO("[nucleus] 200 token response ({} bytes)", body.size());

    auto responseBuf = std::make_shared<std::string>(response);
    asio::async_write(*socket, asio::buffer(*responseBuf),
        [socket, connId, responseBuf](const asio::error_code& ec, size_t) {
            if (ec) {
                LOG_ERROR("[nucleus] write error {}: {}", connId, ec.message());
            }
            asio::error_code sec;
            socket->lowest_layer().shutdown(asio::ip::tcp::socket::shutdown_both, sec);
        }
    );
}

void Server::handleBlazeConnection(
    std::shared_ptr<asio::ssl::stream<asio::ip::tcp::socket>> socket
) {
    uint64_t connId = m_nextConnectionId++;
    
    auto client = std::make_shared<network::ClientConnection>(socket, connId);
    
    client->setPacketHandler([this](auto client, auto packet) {
        if (!packet) return;

        // Fire2 PING is framework-level; respond before component dispatch.
        // Without this BlazeSDK times out at 20s post-auth and reconnects in a loop.
        if (packet->getMessageType() == blaze::MessageType::Ping) {
            client->sendPacket(packet->createPingReply());
            return;
        }

        auto& registry = blaze::ComponentRegistry::instance();
        auto response = registry.routePacket(*packet, client);

        if (response) {
            client->sendPacket(std::move(response));
        }
    });
    
    client->setDisconnectHandler([](auto client) {
        LOG_INFO("Blaze client {} disconnected", client->getId());
    });
    
    client->start();
}

} // namespace ds2
