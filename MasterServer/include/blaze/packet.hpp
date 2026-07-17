#pragma once

#include "blaze/types.hpp"
#include <vector>
#include <cstdint>
#include <memory>

namespace gw2::blaze {

class Packet {
public:
    Packet();
    Packet(ComponentId component, uint16_t command, MessageType msgType, uint16_t msgId = 0);
    
    static std::unique_ptr<Packet> parse(const std::vector<uint8_t>& data);
    
    std::vector<uint8_t> serialize() const;
    
    PacketHeader& header() { return m_header; }
    const PacketHeader& header() const { return m_header; }
    
    std::vector<uint8_t>& payload() { return m_payload; }
    const std::vector<uint8_t>& payload() const { return m_payload; }
    
    void setPayload(const TdfStruct& data);
    
    TdfStruct getPayloadAsTdf() const;
    
    void setComponent(ComponentId component);
    void setCommand(uint16_t command);
    void setMessageType(MessageType type);
    void setMessageId(uint16_t id);
    void setMsgNum(uint32_t n);
    void setError(BlazeError error);

    ComponentId getComponent() const;
    uint16_t getCommand() const;
    MessageType getMessageType() const;
    uint16_t getMessageId() const;
    uint32_t getMsgNum() const;
    uint16_t getMetadataSize() const;
    uint32_t getPayloadSize() const;
    BlazeError getError() const;

    std::unique_ptr<Packet> createReply() const;
    std::unique_ptr<Packet> createErrorReply(BlazeError error) const;
    std::unique_ptr<Packet> createPingReply() const;

private:
    PacketHeader m_header;
    std::vector<uint8_t> m_payload;
};

} // namespace gw2::blaze
