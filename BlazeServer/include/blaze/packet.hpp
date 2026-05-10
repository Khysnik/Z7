#pragma once

#include "blaze/types.hpp"
#include <vector>
#include <cstdint>
#include <memory>

namespace ds2::blaze {

/**
 * Blaze Packet
 * 
 * Represents a complete Blaze protocol packet with header and payload.
 * The payload is TDF-encoded data.
 */
class Packet {
public:
    Packet();
    Packet(ComponentId component, uint16_t command, MessageType msgType, uint16_t msgId = 0);
    
    // Parse a packet from raw bytes
    static std::unique_ptr<Packet> parse(const std::vector<uint8_t>& data);
    
    // Serialize packet to bytes
    std::vector<uint8_t> serialize() const;
    
    // Header access
    PacketHeader& header() { return m_header; }
    const PacketHeader& header() const { return m_header; }
    
    // Payload access
    std::vector<uint8_t>& payload() { return m_payload; }
    const std::vector<uint8_t>& payload() const { return m_payload; }
    
    // Set payload from TDF structure
    void setPayload(const TdfStruct& data);
    
    // Get payload as TDF structure
    TdfStruct getPayloadAsTdf() const;
    
    // Convenience setters
    void setComponent(ComponentId component);
    void setCommand(uint16_t command);
    void setMessageType(MessageType type);
    void setMessageId(uint16_t id);
    void setMsgNum(uint32_t n);
    void setError(BlazeError error);

    // Convenience getters
    ComponentId getComponent() const;
    uint16_t getCommand() const;
    MessageType getMessageType() const;
    uint16_t getMessageId() const;
    uint32_t getMsgNum() const;
    uint16_t getMetadataSize() const;
    uint32_t getPayloadSize() const;
    BlazeError getError() const;

    // Create reply packet
    std::unique_ptr<Packet> createReply() const;
    std::unique_ptr<Packet> createErrorReply(BlazeError error) const;
    std::unique_ptr<Packet> createPingReply() const;

private:
    PacketHeader m_header;
    std::vector<uint8_t> m_payload;
};

} // namespace ds2::blaze
