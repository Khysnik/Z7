#include "network/qos_server.hpp"
#include "utils/logger.hpp"
#include <sstream>
#include <regex>

namespace ds2::network {

QoSServer::QoSServer(asio::io_context& io_context, const std::string& host, uint16_t port)
    : m_io_context(io_context)
    , m_acceptor(io_context)
    , m_host(host)
    , m_port(port)
    , m_running(false)
{
}

QoSServer::~QoSServer() {
    stop();
}

void QoSServer::start() {
    if (m_running) return;
    
    try {
        tcp::endpoint endpoint(asio::ip::make_address(m_host), m_port);
        
        m_acceptor.open(endpoint.protocol());
        m_acceptor.set_option(tcp::acceptor::reuse_address(true));
        m_acceptor.bind(endpoint);
        m_acceptor.listen();
        
        m_running = true;
        LOG_INFO("QoS Server listening on {}:{}", m_host, m_port);
        
        doAccept();
    }
    catch (const std::exception& e) {
        LOG_ERROR("Failed to start QoS server: {}", e.what());
        throw;
    }
}

void QoSServer::stop() {
    if (!m_running) return;
    
    m_running = false;
    
    asio::error_code ec;
    m_acceptor.close(ec);
    
    LOG_INFO("QoS Server stopped");
}

void QoSServer::doAccept() {
    if (!m_running) return;
    
    auto socket = std::make_shared<tcp::socket>(m_io_context);
    
    m_acceptor.async_accept(
        *socket,
        [this, socket](const asio::error_code& error) {
            if (!error && m_running) {
                try {
                    auto remoteAddr = socket->remote_endpoint().address().to_string();
                    auto remotePort = socket->remote_endpoint().port();
                    LOG_INFO("QoS connection from {}:{}", remoteAddr, remotePort);
                } catch (...) {
                    LOG_INFO("QoS connection from <unknown>");
                }
                handleClient(socket);
            } else if (error) {
                LOG_ERROR("QoS accept error: {}", error.message());
            }
            doAccept();
        }
    );
}

void QoSServer::handleClient(std::shared_ptr<tcp::socket> socket) {
    auto self = shared_from_this();
    
    // Read HTTP request
    auto buffer = std::make_shared<std::vector<char>>(4096);
    
    socket->async_read_some(
        asio::buffer(*buffer),
        [this, self, socket, buffer](const asio::error_code& error, size_t bytes) {
            if (error) {
                LOG_DEBUG("QoS read error: {}", error.message());
                LOG_INFO("QoS disconnect from {}: {}", 
                    socket->is_open() ? socket->remote_endpoint().address().to_string() : "<unknown>",
                    error.message());
                return;
            }
            std::string request(buffer->data(), bytes);
            LOG_DEBUG("QoS Request:\n{}", request);
            std::string response = buildQoSResponse(request);
            // Send response
            auto responseData = std::make_shared<std::string>(response);
            asio::async_write(
                *socket,
                asio::buffer(*responseData),
                [socket, responseData](const asio::error_code& err, size_t) {
                    if (err) {
                        LOG_DEBUG("QoS write error: {}", err.message());
                    }
                    // Close connection after response
                    asio::error_code ec;
                    std::string addr = "<unknown>";
                    try {
                        if (socket->is_open()) addr = socket->remote_endpoint().address().to_string();
                    } catch (...) {}
                    LOG_INFO("QoS disconnect from {}", addr);
                    socket->shutdown(tcp::socket::shutdown_both, ec);
                }
            );
        }
    );
}

std::string QoSServer::buildQoSResponse(const std::string& /*request*/) {
    // TODO: Parse request to extract client IP for proper NAT detection
    // The QoS response tells the client their external IP/port for NAT detection
    
    // Default response - indicates open NAT
    std::stringstream body;
    body << "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n";
    body << "<qos>\r\n";
    body << "  <numprobes>0</numprobes>\r\n";
    body << "  <qosport>17502</qosport>\r\n";
    body << "  <probesize>0</probesize>\r\n";
    body << "  <qosip>127.0.0.1</qosip>\r\n";
    body << "  <requestid>1</requestid>\r\n";
    body << "  <reqsecret>0</reqsecret>\r\n";
    body << "</qos>\r\n";
    
    std::string bodyStr = body.str();
    
    std::stringstream response;
    response << "HTTP/1.1 200 OK\r\n";
    response << "Content-Type: application/xml\r\n";
    response << "Content-Length: " << bodyStr.size() << "\r\n";
    response << "Connection: close\r\n";
    response << "\r\n";
    response << bodyStr;
    
    return response.str();
}

} // namespace ds2::network
