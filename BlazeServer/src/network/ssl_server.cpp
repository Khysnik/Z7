#include "network/ssl_server.hpp"
#include "utils/logger.hpp"
#include <openssl/ssl.h>
#include <openssl/err.h>

namespace ds2::network {

// Only log SSL alerts and fatal errors — handshake state chatter goes to DEBUG.
static void ssl_info_callback(const SSL* ssl, int where, int ret) {
    if (where & SSL_CB_LOOP) {
        int w = where & ~SSL_ST_MASK;
        const char* str = (w & SSL_ST_CONNECT) ? "SSL_connect"
                        : (w & SSL_ST_ACCEPT)  ? "SSL_accept"
                        :                        "SSL";
        //LOG_DEBUG("SSL {}: {}", str, SSL_state_string_long(ssl));
    }
    else if (where & SSL_CB_ALERT) {
        const char* dir = (where & SSL_CB_READ) ? "read" : "write";
        LOG_WARN("SSL alert {}: {} {}",
            dir, SSL_alert_type_string_long(ret), SSL_alert_desc_string_long(ret));
    }
    else if ((where & SSL_CB_EXIT) && ret <= 0) {
        int w = where & ~SSL_ST_MASK;
        const char* str = (w & SSL_ST_CONNECT) ? "SSL_connect"
                        : (w & SSL_ST_ACCEPT)  ? "SSL_accept"
                        :                        "SSL";
        //LOG_ERROR("SSL {}: {} in {}", str, ret == 0 ? "failed" : "error",
        //    SSL_state_string_long(ssl));
    }
}

SSLServer::SSLServer(asio::io_context& io_context, const std::string& host, uint16_t port)
    : m_io_context(io_context)
    , m_ssl_context(asio::ssl::context::sslv23)
    , m_acceptor(io_context)
    , m_host(host)
    , m_port(port)
    , m_running(false)
{
}

SSLServer::~SSLServer() {
    stop();
}

bool SSLServer::configureSsl(const std::string& certPath, const std::string& keyPath) {
    try {
        // Garden Warfare 2 originally used OpenSSL 1.0.0b (2011) but EA may have updated it
        // We bundle OpenSSL 1.1.1 which supports SSLv3/TLS 1.0/1.1/1.2/1.3
        // Note: OpenSSL 1.1.1 completely removed SSLv2 support
        
        // Get the native OpenSSL context handle
        SSL_CTX* ctx = m_ssl_context.native_handle();
        
        SSL_CTX_set_info_callback(ctx, ssl_info_callback);

        SSL_CTX_clear_options(ctx,
            SSL_OP_NO_SSLv3 | SSL_OP_NO_TLSv1 | SSL_OP_NO_TLSv1_1 |
            SSL_OP_NO_TLSv1_2 | SSL_OP_NO_TLSv1_3);

        if (!SSL_CTX_set_min_proto_version(ctx, 0))
            SSL_CTX_set_min_proto_version(ctx, SSL3_VERSION);
        if (!SSL_CTX_set_max_proto_version(ctx, 0))
            SSL_CTX_set_max_proto_version(ctx, TLS1_3_VERSION);

        SSL_CTX_set_options(ctx, SSL_OP_ALL | SSL_OP_SINGLE_DH_USE);
        SSL_CTX_clear_options(ctx,
            SSL_OP_NO_SSLv3 | SSL_OP_NO_TLSv1 | SSL_OP_NO_TLSv1_1 | SSL_OP_NO_TLSv1_2);

        const char* cipherList = "ALL:COMPLEMENTOFALL";
        if (!SSL_CTX_set_cipher_list(ctx, cipherList))
            if (!SSL_CTX_set_cipher_list(ctx, "ALL"))
                SSL_CTX_set_cipher_list(ctx, "DEFAULT");

        m_ssl_context.use_certificate_chain_file(certPath);
        m_ssl_context.use_private_key_file(keyPath, asio::ssl::context::pem);

        LOG_INFO("SSL configured: cert={}", certPath);
        
        return true;
    }
    catch (const std::exception& e) {
        LOG_ERROR("SSL configuration failed: {}", e.what());
        return false;
    }
}

void SSLServer::setConnectionHandler(ConnectionHandler handler) {
    m_connectionHandler = handler;
}

void SSLServer::start() {
    if (m_running) return;
    
    try {
        tcp::endpoint endpoint(asio::ip::make_address(m_host), m_port);
        
        m_acceptor.open(endpoint.protocol());
        m_acceptor.set_option(tcp::acceptor::reuse_address(true));
        m_acceptor.bind(endpoint);
        m_acceptor.listen();
        
        m_running = true;
        LOG_INFO("SSL Server listening on {}:{}", m_host, m_port);
        
        doAccept();
    }
    catch (const std::exception& e) {
        LOG_ERROR("Failed to start SSL server: {}", e.what());
        throw;
    }
}

void SSLServer::stop() {
    if (!m_running) return;
    
    m_running = false;
    
    asio::error_code ec;
    m_acceptor.close(ec);
    
    LOG_DEBUG("SSL Server stopped on port {}", m_port);
}

void SSLServer::doAccept() {
    if (!m_running) return;
    
    auto socket = std::make_shared<asio::ssl::stream<tcp::socket>>(m_io_context, m_ssl_context);
    
    m_acceptor.async_accept(
        socket->lowest_layer(),
        [this, socket](const asio::error_code& error) {
            handleAccept(socket, error);
        }
    );
}

void SSLServer::handleAccept(
    std::shared_ptr<asio::ssl::stream<tcp::socket>> socket,
    const asio::error_code& error
) {
    if (!m_running) return;
    
    if (!error) {
        std::string remoteAddr = socket->lowest_layer().remote_endpoint().address().to_string();
        uint16_t remotePort = socket->lowest_layer().remote_endpoint().port();
        LOG_INFO("New connection from {}:{}", remoteAddr, remotePort);
        
        // Start SSL handshake
        socket->async_handshake(
            asio::ssl::stream_base::server,
            [this, socket](const asio::error_code& hsError) {
                handleHandshake(socket, hsError);
            }
        );
    }
    else {
        LOG_ERROR("Accept error: {}", error.message());
    }
    
    // Continue accepting
    doAccept();
}

void SSLServer::handleHandshake(
    std::shared_ptr<asio::ssl::stream<tcp::socket>> socket,
    const asio::error_code& error
) {
    if (!error) {
        if (m_connectionHandler)
            m_connectionHandler(socket);
    }
    else {
        LOG_ERROR("SSL handshake failed: {}", error.message());
    }
}

} // namespace ds2::network
