#include <asio.hpp>
#include <asio/ssl.hpp>

#include <arpa/inet.h>

#include <cstdint>
#include <cstring>
#include <iostream>
#include <memory>
#include <sstream>
#include <string>
#include <thread>
#include <utility>
#include <vector>

#include "utils.hpp"

namespace gosredirector
{

    static constexpr const char *kListenHost = "127.0.0.1";
    static constexpr uint16_t kListenPort = 42230;
    static constexpr const char *kCertPath = "certs/server.crt";
    static constexpr const char *kKeyPath = "certs/server.key";

    class RedirectorServer
    {
    public:
        RedirectorServer()
            : sslContext_(asio::ssl::context::tls_server), acceptor_(ioContext_)
        {
        }

        bool init()
        {
            if (!configureSsl())
            {
                return false;
            }

            asio::ip::tcp::endpoint endpoint(asio::ip::make_address(kListenHost), kListenPort);

            acceptor_.open(endpoint.protocol());
            acceptor_.set_option(asio::ip::tcp::acceptor::reuse_address(true));
            acceptor_.bind(endpoint);
            acceptor_.listen();
            return true;
        }

        void run()
        {
            logInfo("Redirector listening on 0.0.0.0:42230");

            for (;;)
            {
                asio::ip::tcp::socket rawSocket(ioContext_);
                asio::error_code acceptError;
                acceptor_.accept(rawSocket, acceptError);
                if (acceptError)
                {
                    logError("Accept failed: " + acceptError.message());
                    continue;
                }

                auto socket = std::make_shared<asio::ssl::stream<asio::ip::tcp::socket>>(std::move(rawSocket), sslContext_);
                std::thread(&RedirectorServer::handleSession, this, std::move(socket)).detach();
            }
        }

    private:
        bool configureSsl()
        {
            asio::error_code ec;
            sslContext_.set_options(
                asio::ssl::context::default_workarounds |
                    asio::ssl::context::no_sslv2 |
                    asio::ssl::context::no_sslv3 |
                    asio::ssl::context::single_dh_use,
                ec);
            if (ec)
            {
                logError("SSL context options failed: " + ec.message());
                return false;
            }

            sslContext_.use_certificate_chain_file(kCertPath, ec);
            if (ec)
            {
                logError("Failed to load certificate chain from certs/server.crt: " + ec.message());
                return false;
            }

            sslContext_.use_private_key_file(kKeyPath, asio::ssl::context::pem, ec);
            if (ec)
            {
                logError("Failed to load private key from certs/server.key: " + ec.message());
                return false;
            }

#if defined(SSL_CTX_set_min_proto_version)
            if (auto *ctx = sslContext_.native_handle())
            {
                SSL_CTX_set_min_proto_version(ctx, TLS1_VERSION);
                SSL_CTX_set_max_proto_version(ctx, TLS1_3_VERSION);
                SSL_CTX_set_security_level(ctx, 0);
                SSL_CTX_set_cipher_list(ctx, "DEFAULT:@SECLEVEL=0");
#if defined(TLS1_3_VERSION)
                SSL_CTX_set_ciphersuites(ctx, "DEFAULT:@SECLEVEL=0");
#endif
            }
#endif
            return true;
        }

        void handleSession(std::shared_ptr<asio::ssl::stream<asio::ip::tcp::socket>> socket)
        {
            asio::error_code ec;
            socket->handshake(asio::ssl::stream_base::server, ec);
            if (ec)
            {
                logError("TLS handshake failed from " + remoteAddress(*socket) + ": " + ec.message());
                return;
            }

            std::string requestHeaders = readHeaders(*socket, ec);
            if (ec)
            {
                logError("Failed to read request headers: " + ec.message());
                shutdownSocket(*socket);
                return;
            }

            ParsedHttpRequest request;
            if (!parseHttpRequest(requestHeaders, request))
            {
                logWarn("Received malformed redirector request");
                sendResponse(*socket, "400 Bad Request", "text/plain", "bad request");
                return;
            }

            if (request.method == "POST" && request.target == "/redirector/getServerInstance")
            {
                handleGetServerInstance(*socket, request);
            }
            else
            {
                logWarn("Unhandled request: " + request.method + " " + request.target);
                sendResponse(*socket, "404 Not Found", "text/plain", "not found");
            }
        }

        std::string readHeaders(asio::ssl::stream<asio::ip::tcp::socket> &socket, asio::error_code &ec)
        {
            asio::streambuf buffer;
            asio::read_until(socket, buffer, "\r\n\r\n", ec);
            if (ec)
            {
                return {};
            }

            std::string headers(
                asio::buffers_begin(buffer.data()),
                asio::buffers_begin(buffer.data()) + static_cast<std::ptrdiff_t>(buffer.size()));

            auto marker = headers.find("\r\n\r\n");
            if (marker != std::string::npos)
            {
                headers.resize(marker + 4);
            }

            return headers;
        }

        void handleGetServerInstance(asio::ssl::stream<asio::ip::tcp::socket> &socket, const ParsedHttpRequest &request)
        {
            (void)request;
            const std::string xmlBody = buildServerInstanceXmlBody();

            logInfo("Sending XML redirector response:");
            std::cout << xmlBody << std::endl;
            logInfo("Sending HTTP redirector response (XML body " + std::to_string(xmlBody.size()) + " bytes)");

            sendResponse(socket, "200 OK", "application/xml", xmlBody);
            logInfo("HTTP redirector response sent");
        }

        void sendResponse(asio::ssl::stream<asio::ip::tcp::socket> &socket,
                          const std::string &status,
                          const std::string &contentType,
                          const std::string &body)
        {
            std::string response =
                "HTTP/1.1 " + status + "\r\n"
                                       "Content-Type: " +
                contentType + "\r\n"
                              "Content-Length: " +
                std::to_string(body.size()) + "\r\n"
                                              "Connection: close\r\n"
                                              "\r\n" +
                body;

            asio::error_code ec;
            asio::write(socket, asio::buffer(response), ec);
            if (ec)
            {
                logError("Write failed: " + ec.message());
            }
            shutdownSocket(socket);
        }

        void shutdownSocket(asio::ssl::stream<asio::ip::tcp::socket> &socket)
        {
            asio::error_code ignored;
            socket.shutdown(ignored);
            socket.lowest_layer().shutdown(asio::ip::tcp::socket::shutdown_both, ignored);
            socket.lowest_layer().close(ignored);
        }

        std::string remoteAddress(asio::ssl::stream<asio::ip::tcp::socket> &socket)
        {
            asio::error_code ec;
            auto endpoint = socket.lowest_layer().remote_endpoint(ec);
            if (ec)
            {
                return "unknown";
            }
            return endpoint.address().to_string() + ":" + std::to_string(endpoint.port());
        }

        asio::io_context ioContext_;
        asio::ssl::context sslContext_;
        asio::ip::tcp::acceptor acceptor_;
    };

} // namespace gosredirector

int main()
{
    using namespace gosredirector;

    try
    {
        RedirectorServer server;
        if (!server.init())
        {
            return 1;
        }
        server.run();
    }
    catch (const std::exception &ex)
    {
        logError(ex.what());
        return 1;
    }

    return 0;
}
