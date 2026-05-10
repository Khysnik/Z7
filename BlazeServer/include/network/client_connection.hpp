#pragma once

#include "blaze/types.hpp"
#include "blaze/packet.hpp"
#include <asio.hpp>
#include <asio/ssl.hpp>
#include <memory>
#include <string>
#include <queue>
#include <mutex>
#include <atomic>

namespace ds2::network {

using asio::ip::tcp;

/**
 * Client Connection
 * 
 * Represents a connected Blaze client.
 * Handles packet framing, reading, and writing.
 */
class ClientConnection : public std::enable_shared_from_this<ClientConnection> {
public:
    using PacketHandler = std::function<void(std::shared_ptr<ClientConnection>, std::unique_ptr<blaze::Packet>)>;
    using DisconnectHandler = std::function<void(std::shared_ptr<ClientConnection>)>;
    
    ClientConnection(std::shared_ptr<asio::ssl::stream<tcp::socket>> socket,
                     uint64_t connectionId);
    ~ClientConnection();
    
    // Start reading packets
    void start();

    // Start reading when the 12-byte header has already been read externally
    void startWithBuffer(std::vector<uint8_t> prefetchedHeader);
    
    // Stop and close connection
    void stop();
    
    // Send a packet
    void sendPacket(const blaze::Packet& packet);
    void sendPacket(std::unique_ptr<blaze::Packet> packet);
    
    // Set handlers
    void setPacketHandler(PacketHandler handler);
    void setDisconnectHandler(DisconnectHandler handler);
    
    // Get connection info
    uint64_t getId() const { return m_connectionId; }
    std::string getRemoteAddress() const;
    uint16_t getRemotePort() const;
    
    // Session data
    void setSessionId(uint64_t sessionId) { m_sessionId = sessionId; }
    uint64_t getSessionId() const { return m_sessionId; }
    
    void setUserId(uint64_t userId) { m_userId = userId; }
    uint64_t getUserId() const { return m_userId; }
    
    void setPersonaName(const std::string& name) { m_personaName = name; }
    const std::string& getPersonaName() const { return m_personaName; }
    
    void setConnectionState(blaze::ConnectionState state) { m_state = state; }
    blaze::ConnectionState getConnectionState() const { return m_state; }
    
    bool isAuthenticated() const { return m_state >= blaze::ConnectionState::AUTHENTICATED; }
    
private:
    void doReadHeader();
    void doReadPayload(size_t length);
    void doWrite();
    
    void handleReadHeader(const asio::error_code& error, size_t bytes_transferred);
    void handleReadPayload(const asio::error_code& error, size_t bytes_transferred);
    void handleWrite(const asio::error_code& error, size_t bytes_transferred);
    
    std::shared_ptr<asio::ssl::stream<tcp::socket>> m_socket;
    uint64_t m_connectionId;
    std::atomic<bool> m_running;
    
    // Read buffer
    std::vector<uint8_t> m_headerBuffer;
    std::vector<uint8_t> m_payloadBuffer;
    
    // Write queue
    std::queue<std::vector<uint8_t>> m_writeQueue;
    std::mutex m_writeMutex;
    bool m_writing;
    
    // Handlers
    PacketHandler m_packetHandler;
    DisconnectHandler m_disconnectHandler;
    
    // Session state
    uint64_t m_sessionId = 0;
    uint64_t m_userId = 0;
    std::string m_personaName;
    blaze::ConnectionState m_state = blaze::ConnectionState::CONNECTED;
};

} // namespace ds2::network
