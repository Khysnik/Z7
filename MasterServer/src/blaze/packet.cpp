#include "blaze/packet.hpp"
#include "blaze/tdf.hpp"
#include "utils/logger.hpp"
#include <cstring>

namespace gw2::blaze {

Packet::Packet() {
    std::memset(&m_header, 0, sizeof(m_header));
}

Packet::Packet(ComponentId component, uint16_t command, MessageType msgType, uint16_t msgNum) {
    std::memset(&m_header, 0, sizeof(m_header));
    setComponent(component);
    setCommand(command);
    setMsgNum(msgNum);
    setMessageType(msgType);
}

std::unique_ptr<Packet> Packet::parse(const std::vector<uint8_t>& data) {
    if (data.size() < sizeof(PacketHeader)) {
        LOG_ERROR("Packet too small: {} bytes", data.size());
        return nullptr;
    }

    auto packet = std::make_unique<Packet>();
    std::memcpy(packet->m_header.raw, data.data(), sizeof(PacketHeader));

    uint32_t payloadLen  = packet->getPayloadSize();
    uint16_t metadataLen = packet->getMetadataSize();
    size_t   headerTotal = sizeof(PacketHeader) + metadataLen;

    if (data.size() < headerTotal + payloadLen) {
        LOG_ERROR("Packet payload incomplete: have {}, need {}",
                  data.size() - sizeof(PacketHeader), (size_t)payloadLen + metadataLen);
        return nullptr;
    }

    if (payloadLen > 0) {
        packet->m_payload.assign(
            data.begin() + headerTotal,
            data.begin() + headerTotal + payloadLen
        );
    }

    return packet;
}

std::vector<uint8_t> Packet::serialize() const {
    std::vector<uint8_t> data;
    data.reserve(sizeof(PacketHeader) + m_payload.size());

    PacketHeader header = m_header;
    uint32_t len = static_cast<uint32_t>(m_payload.size());
    header.raw[0] = (len >> 24) & 0xFF;
    header.raw[1] = (len >> 16) & 0xFF;
    header.raw[2] = (len >>  8) & 0xFF;
    header.raw[3] =  len        & 0xFF;

    data.insert(data.end(), header.raw, header.raw + sizeof(PacketHeader));
    data.insert(data.end(), m_payload.begin(), m_payload.end());

    return data;
}

void Packet::setPayload(const TdfStruct& tdf) {
    TdfEncoder encoder;
    m_payload = encoder.encode(tdf);
}

TdfStruct Packet::getPayloadAsTdf() const {
    if (m_payload.empty()) return TdfStruct{};
    TdfDecoder decoder(m_payload.data(), m_payload.size());
    return decoder.decode();
}

// --- Setters ---

void Packet::setComponent(ComponentId component) {
    uint16_t c = static_cast<uint16_t>(component);
    m_header.raw[6] = (c >> 8) & 0xFF;
    m_header.raw[7] =  c       & 0xFF;
}

void Packet::setCommand(uint16_t command) {
    m_header.raw[8] = (command >> 8) & 0xFF;
    m_header.raw[9] =  command       & 0xFF;
}

void Packet::setMessageType(MessageType type) {
    uint8_t userIndex = m_header.raw[13] & 0x1F;
    m_header.raw[13]  = (static_cast<uint8_t>(type) << 5) | userIndex;
}

void Packet::setMessageId(uint16_t id) {
    m_header.raw[11] = (id >> 8) & 0xFF;
    m_header.raw[12] =  id       & 0xFF;
}

void Packet::setMsgNum(uint32_t n) {
    m_header.raw[10] = (n >> 16) & 0xFF;
    m_header.raw[11] = (n >>  8) & 0xFF;
    m_header.raw[12] =  n        & 0xFF;
}

void Packet::setError(BlazeError /*error*/) {

}


ComponentId Packet::getComponent() const {
    return static_cast<ComponentId>(((uint16_t)m_header.raw[6] << 8) | m_header.raw[7]);
}

uint16_t Packet::getCommand() const {
    return ((uint16_t)m_header.raw[8] << 8) | m_header.raw[9];
}

MessageType Packet::getMessageType() const {
    return static_cast<MessageType>(m_header.raw[13] >> 5);
}

uint16_t Packet::getMessageId() const {
    return ((uint16_t)m_header.raw[11] << 8) | m_header.raw[12];
}

uint32_t Packet::getMsgNum() const {
    return ((uint32_t)m_header.raw[10] << 16)
         | ((uint32_t)m_header.raw[11] <<  8)
         |  m_header.raw[12];
}

uint16_t Packet::getMetadataSize() const {
    return ((uint16_t)m_header.raw[4] << 8) | m_header.raw[5];
}

uint32_t Packet::getPayloadSize() const {
    return ((uint32_t)m_header.raw[0] << 24) | ((uint32_t)m_header.raw[1] << 16)
         | ((uint32_t)m_header.raw[2] <<  8) |  m_header.raw[3];
}

BlazeError Packet::getError() const {
    return BlazeError::Success;
}


std::unique_ptr<Packet> Packet::createReply() const {
    auto reply = std::make_unique<Packet>();
    reply->m_header = m_header;
    uint8_t userIndex = m_header.raw[13] & 0x1F;
    reply->m_header.raw[13] = (static_cast<uint8_t>(MessageType::Reply) << 5) | userIndex;
    return reply;
}

std::unique_ptr<Packet> Packet::createErrorReply(BlazeError error) const {
    auto reply = std::make_unique<Packet>();
    reply->m_header = m_header;
    reply->m_header.raw[4] = 0;   // no metadata, no payload in the error reply
    reply->m_header.raw[5] = 0;
    uint8_t userIndex = m_header.raw[13] & 0x1F;
    reply->m_header.raw[13] = (static_cast<uint8_t>(MessageType::ErrorReply) << 5) | userIndex;
    reply->m_header.raw[14] = (static_cast<uint32_t>(error) >> 8) & 0xFF;
    reply->m_header.raw[15] =  static_cast<uint32_t>(error)       & 0xFF;
    return reply;
}

std::unique_ptr<Packet> Packet::createPingReply() const {
    auto reply = std::make_unique<Packet>();
    reply->m_header = m_header;
    uint8_t userIndex = m_header.raw[13] & 0x1F;
    reply->m_header.raw[13] = (static_cast<uint8_t>(MessageType::PingReply) << 5) | userIndex;
    return reply;
}

} // namespace gw2::blaze
