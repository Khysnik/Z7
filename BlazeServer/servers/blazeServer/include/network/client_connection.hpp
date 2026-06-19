#pragma once

#include "blaze/types.hpp"
#include "blaze/packet.hpp"
#include <asio.hpp>
#include <asio/ssl.hpp>
#include <memory>
#include <string>
#include <map>
#include <queue>
#include <mutex>
#include <atomic>

namespace gw2::network {

using asio::ip::tcp;

class ClientConnection : public std::enable_shared_from_this<ClientConnection> {
public:
    using PacketHandler = std::function<void(std::shared_ptr<ClientConnection>, std::unique_ptr<blaze::Packet>)>;
    using DisconnectHandler = std::function<void(std::shared_ptr<ClientConnection>)>;
    
    ClientConnection(std::shared_ptr<asio::ssl::stream<tcp::socket>> socket,
                     uint64_t connectionId);
    ~ClientConnection();
    
    void start();

    void startWithBuffer(std::vector<uint8_t> prefetchedHeader);
    
    void stop();
    
    void sendPacket(const blaze::Packet& packet);
    void sendPacket(std::unique_ptr<blaze::Packet> packet);
    
    void setPacketHandler(PacketHandler handler);
    void setDisconnectHandler(DisconnectHandler handler);
    
    uint64_t getId() const { return m_connectionId; }
    std::string getRemoteAddress() const;
    uint16_t getRemotePort() const;
    
    void setSessionId(uint64_t sessionId) { m_sessionId = sessionId; }
    uint64_t getSessionId() const { return m_sessionId; }
    
    void setUserId(uint64_t userId) { m_userId = userId; }
    uint64_t getUserId() const { return m_userId; }
    
    void setPersonaName(const std::string& name) { m_personaName = name; }
    const std::string& getPersonaName() const { return m_personaName; }
    
    void setConnectionState(blaze::ConnectionState state) { m_state = state; }
    blaze::ConnectionState getConnectionState() const { return m_state; }

    bool isAuthenticated() const { return m_state >= blaze::ConnectionState::AUTHENTICATED; }

    struct AddrEndpoint { uint64_t ip = 0; uint64_t maci = 0; uint64_t port = 0; };
    struct NetworkInfo {
        std::map<std::string, int64_t> nlmp;
        int64_t bwhr = 0, dbps = 0, nahr = 0, natt = 0, ubps = 0;
        std::string bps;
        uint8_t addrArm = 0x7F;
        AddrEndpoint exip, inip;
        uint64_t addrMaci = 0;
        bool addrReady = false;
        bool ready = false;
    };
    void setNetworkInfo(const NetworkInfo& info) { m_networkInfo = info; }
    const NetworkInfo& getNetworkInfo() const { return m_networkInfo; }
    
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
    
    std::vector<uint8_t> m_headerBuffer;
    std::vector<uint8_t> m_payloadBuffer;
    
    std::queue<std::vector<uint8_t>> m_writeQueue;
    std::mutex m_writeMutex;
    bool m_writing;
    
    PacketHandler m_packetHandler;
    DisconnectHandler m_disconnectHandler;
    
    uint64_t m_sessionId = 0;
    uint64_t m_userId = 0;
    std::string m_personaName;
    blaze::ConnectionState m_state = blaze::ConnectionState::CONNECTED;
    NetworkInfo m_networkInfo;
};

} // namespace gw2::network
