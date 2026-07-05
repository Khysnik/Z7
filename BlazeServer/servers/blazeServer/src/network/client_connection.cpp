#include "network/client_connection.hpp"
#include "blaze/tdf.hpp"
#include "utils/logger.hpp"
#ifdef _WIN32
#include <winsock2.h>
#else
#include <arpa/inet.h>
#endif

// god i hate networking

namespace gw2::network {

// ClientConnection constructor, registers a socket and connection ID
ClientConnection::ClientConnection(std::shared_ptr<asio::ssl::stream<tcp::socket>> socket, uint64_t connectionId)
    : m_socket(socket)
    , m_strand(asio::make_strand(asio::any_io_executor(socket->get_executor())))
    , m_connectionId(connectionId)
    , m_running(false)
    , m_writing(false)
{
    m_headerBuffer.resize(sizeof(blaze::PacketHeader));
}

// ClientConnection destructor, called on program exit
ClientConnection::~ClientConnection() {
    stop();
}

// Start reading from the client socket
void ClientConnection::start() {
    if (m_running) return;

    m_running = true;
    LOG_DEBUG("[blaze] connection {} from {}", m_connectionId, getRemoteAddress());

    doReadHeader();
}

void ClientConnection::startWithBuffer(std::vector<uint8_t> prefetchedHeader) {
    if (m_running) return;

    m_running = true;
    LOG_DEBUG("[blaze] connection {} from {} (binary)", m_connectionId, getRemoteAddress());

    m_headerBuffer = std::move(prefetchedHeader);
    m_headerBuffer.resize(sizeof(blaze::PacketHeader));
    handleReadHeader({}, sizeof(blaze::PacketHeader));
}

// Stop the client connection and close the socket
void ClientConnection::stop() {
    if (!m_running.exchange(false)) return;

    asio::error_code ec;
    m_socket->lowest_layer().shutdown(tcp::socket::shutdown_both, ec);
    m_socket->lowest_layer().close(ec);
    LOG_INFO("[blaze] disconnected {}", m_connectionId);

    if (m_disconnectHandler) {
        // shared_from_this throws if we're here from the destructor; ignore.
        try { m_disconnectHandler(shared_from_this()); }
        catch (const std::bad_weak_ptr&) {}
    }
}

// Send a fire2 serialized packet to the client
void ClientConnection::sendPacket(const blaze::Packet& packet) {
    if (!m_running) return;
    
    std::vector<uint8_t> data = packet.serialize();
    
    {
        auto msgType = packet.getMessageType();
        bool isNotif = (msgType == blaze::MessageType::Notification);
        std::string name = blaze::blazePacketName(
            static_cast<uint16_t>(packet.getComponent()), packet.getCommand(), msgType);
        std::string body;
        try {
            auto tdf = packet.getPayloadAsTdf();
            if (!tdf.empty()) {
                body = "\n" + blaze::tdfToBlaze(tdf);
                if (body.size() > 2000) {
                    body.resize(2000);
                    body += "\n... (truncated)";
                }
            }
        } catch (const std::exception& e) {
            body = std::string(" <undecodable payload: ") + e.what() + ">";
        }
        //LOG_INFO("[{}] << {}{}", isNotif ? "notif" : "reply", name, body);
        LOG_INFO("[{}] << {}", isNotif ? "notif" : "reply", name);
    }
    
    auto self = shared_from_this();
    asio::post(m_strand, [this, self, data = std::move(data)]() mutable {
        m_writeQueue.push(std::move(data));
        if (!m_writing) doWrite();
    });
}

void ClientConnection::sendPacket(std::unique_ptr<blaze::Packet> packet) {
    if (packet) {
        sendPacket(*packet);
    }
}

void ClientConnection::setPacketHandler(PacketHandler handler) {
    m_packetHandler = handler;
}

void ClientConnection::setDisconnectHandler(DisconnectHandler handler) {
    m_disconnectHandler = handler;
}

std::string ClientConnection::getRemoteAddress() const {
    try {
        return m_socket->lowest_layer().remote_endpoint().address().to_string();
    }
    catch (...) {
        return "unknown";
    }
}

uint16_t ClientConnection::getRemotePort() const {
    try {
        return m_socket->lowest_layer().remote_endpoint().port();
    }
    catch (...) {
        return 0;
    }
}

void ClientConnection::doReadHeader() {
    if (!m_running) return;
    
    auto self = shared_from_this();
    
    asio::async_read(
        *m_socket,
        asio::buffer(m_headerBuffer),
        asio::bind_executor(m_strand,
            [this, self](const asio::error_code& error, size_t bytes_transferred) {
                handleReadHeader(error, bytes_transferred);
            })
    );
}

void ClientConnection::handleReadHeader(const asio::error_code& error, size_t bytes_transferred) {
    if (!m_running) return;
    
    if (error) {
        if (error != asio::error::eof && error != asio::error::operation_aborted) {
            LOG_ERROR("[Conn:{}] Read header error: {}", m_connectionId, error.message());
        }
        stop();
        return;
    }
    
    if (bytes_transferred != sizeof(blaze::PacketHeader)) {
        LOG_ERROR("[Conn:{}] Incomplete header", m_connectionId);
        stop();
        return;
    }
    
    // Parse header — Fire2 16-byte layout
    const uint8_t* header = m_headerBuffer.data();
    uint32_t payloadLen  = ((uint32_t)header[0] << 24) | ((uint32_t)header[1] << 16) | ((uint32_t)header[2] <<  8) |  header[3];
    uint16_t metadataLen = ((uint16_t)header[4] << 8) | header[5];
    uint16_t component   = ((uint16_t)header[6] << 8) | header[7];
    uint16_t command     = ((uint16_t)header[8] << 8) | header[9];
    uint32_t msgNum      = ((uint32_t)header[10] << 16) | ((uint32_t)header[11] << 8) | header[12];
    uint8_t  msgType     = header[13] >> 5;

    // Log raw bytes at the DEBUG level for... debugging?
    {
        char hexbuf[16 * 3 + 1];
        size_t pos = 0;
        for (size_t i = 0; i < 16; ++i)
            pos += snprintf(hexbuf + pos, sizeof(hexbuf) - pos, "%02X ", header[i]);
        LOG_DEBUG("[blaze] >> raw: {}", hexbuf);
    }

    (void)msgNum;

    // Ignore incoming sketchy shit
    uint32_t totalPayload = (uint32_t)metadataLen + payloadLen;
    if (totalPayload > 65536) {
        LOG_ERROR("[Conn:{}] Payload length {} suspiciously large, dropping", m_connectionId, totalPayload);
        stop();
        return;
    }

    // read payload if present
    if (totalPayload > 0) {
        doReadPayload(totalPayload);
    } else {
        // No payload present, header only
        LOG_INFO("[recv] >> {}", blaze::blazePacketName(component, command, blaze::MessageType::Message));
        auto packet = blaze::Packet::parse(m_headerBuffer);
        if (packet && m_packetHandler) {
            m_packetHandler(shared_from_this(), std::move(packet));
        }
        doReadHeader();
    }
}

// Read packet payload
void ClientConnection::doReadPayload(size_t length) {
    if (!m_running) return;
    
    m_payloadBuffer.resize(length);
    auto self = shared_from_this();
    
    asio::async_read(
        *m_socket,
        asio::buffer(m_payloadBuffer),
        asio::bind_executor(m_strand,
            [this, self](const asio::error_code& error, size_t bytes_transferred) {
                handleReadPayload(error, bytes_transferred);
            })
    );
}

// Handle completed payload read, parse the full packet, and send to the packet handler
void ClientConnection::handleReadPayload(const asio::error_code& error, size_t /*bytes_transferred*/) {
    if (!m_running) return;
    
    if (error) {
        LOG_ERROR("[Conn:{}] Read payload error: {}", m_connectionId, error.message());
        stop();
        return;
    }
    
    // Combine header + payload
    std::vector<uint8_t> fullPacket;
    fullPacket.reserve(m_headerBuffer.size() + m_payloadBuffer.size());
    fullPacket.insert(fullPacket.end(), m_headerBuffer.begin(), m_headerBuffer.end());
    fullPacket.insert(fullPacket.end(), m_payloadBuffer.begin(), m_payloadBuffer.end());

    {
        size_t total = m_payloadBuffer.size();
        size_t dumpLen = std::min(total, size_t(256));
        std::string hexout;
        hexout.reserve(dumpLen * 3);
        char tmp[4];
        for (size_t i = 0; i < dumpLen; ++i) {
            snprintf(tmp, sizeof(tmp), "%02X ", m_payloadBuffer[i]);
            hexout += tmp;
            if ((i + 1) % 16 == 0) hexout += '\n';
        }
        if (total > 256) hexout += "...";
        LOG_DEBUG("[Conn:{}] Payload ({} bytes):\n{}", m_connectionId, total, hexout);
    }

    // Parse and handle packet
    auto packet = blaze::Packet::parse(fullPacket);
    if (packet) {
        std::string name = blaze::blazePacketName(static_cast<uint16_t>(packet->getComponent()), packet->getCommand(), blaze::MessageType::Message);
        std::string body;
        try {
            auto tdf = packet->getPayloadAsTdf();
            if (!tdf.empty()) {
                body = "\n" + blaze::tdfToBlaze(tdf);
                if (body.size() > 2000) {
                    body.resize(2000);
                    body += "\n... (truncated)";
                }
            }
        } catch (const std::exception& e) {
            body = std::string(" <undecodable payload: ") + e.what() + ">";
        }
        LOG_DEBUG("[recv] >> {}{}", name, body);
        if (m_packetHandler) {
            m_packetHandler(shared_from_this(), std::move(packet));
        }
    }
    
    // Continue reading
    doReadHeader();
}

// Write the next packet in the queue to the client
void ClientConnection::doWrite() {
    if (!m_running) return;

    if (m_writeQueue.empty()) {
        m_writing = false;
        return;
    }

    m_writing = true;
    auto self = shared_from_this();

    // Front stays in the queue until the write completes.
    const auto& data = m_writeQueue.front();

    asio::async_write(
        *m_socket,
        asio::buffer(data),
        asio::bind_executor(m_strand, [this, self](const asio::error_code& error, size_t bytes_transferred) {
            handleWrite(error, bytes_transferred);
        })
    );
}

void ClientConnection::handleWrite(const asio::error_code& error, size_t /*bytes_transferred*/) {
    // On m_strand.
    if (error) {
        if (error != asio::error::operation_aborted)
            LOG_ERROR("[Conn:{}] Write error: {}", m_connectionId, error.message());
        m_writing = false;
        stop();
        return;
    }

    m_writeQueue.pop();
    doWrite();   // continue with any queued writes
}

} // namespace gw2::network
