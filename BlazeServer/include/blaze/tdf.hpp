#pragma once

#include "blaze/types.hpp"
#include <vector>
#include <cstdint>
#include <memory>
#include <string>

namespace ds2::blaze {

/**
 * TDF (Tag Data Format) Encoder/Decoder
 * 
 * TDF is EA's binary serialization format used in the Blaze protocol.
 * Each field has a 3-byte compressed tag and a type byte, followed by the value.
 * 
 * Tag compression: 4 ASCII chars -> 3 bytes (6 bits per char, values 0-38)
 *   A-Z = 1-26, 0-9 = 27-36, _ = 37, space = 0
 */
class TdfEncoder {
public:
    TdfEncoder();
    
    // Encode a full TDF structure
    std::vector<uint8_t> encode(const TdfStruct& data);
    
    // Individual type encoders
    void encodeInteger(const std::string& tag, int64_t value);
    void encodeString(const std::string& tag, const std::string& value);
    void encodeBinary(const std::string& tag, const std::vector<uint8_t>& value);
    void encodeStruct(const std::string& tag, const TdfStruct& value);
    void encodeList(const std::string& tag, TdfType elementType, const TdfList& value);
    void encodeMap(const std::string& tag, TdfType keyType, TdfType valueType, const TdfMapWrapper& value);
    void encodeIntList(const std::string& tag, const TdfIntList& value);
    void encodePair(const std::string& tag, const TdfPair& value);
    void encodeTriple(const std::string& tag, const TdfTriple& value);
    void encodeFloat(const std::string& tag, float value);
    
    // Get encoded data
    std::vector<uint8_t> getData() const { return m_buffer; }
    
    // Reset encoder
    void reset() { m_buffer.clear(); }
    
private:
    std::vector<uint8_t> m_buffer;
    
    // Dispatch a single child by type (no tag prefix written here — each
    // type encoder writes its own tag).
    void encodeChild(const TdfValue& v);

    // Encode tag (4 chars -> 3 bytes)
    void encodeTag(const std::string& tag);
    
    // Encode variable-length integer
    void encodeVarInt(int64_t value);

    // Encode unsigned variable-length integer
    void encodeVarUInt(uint64_t value);

    // Encode string value without tag/type header (for map/list elements)
    void encodeRawString(const std::string& s);

    // Write raw bytes
    void writeBytes(const void* data, size_t size);
    void writeByte(uint8_t byte);
};

/**
 * TDF Decoder
 */
class TdfDecoder {
public:
    TdfDecoder(const std::vector<uint8_t>& data);
    TdfDecoder(const uint8_t* data, size_t size);
    
    // Decode entire structure
    TdfStruct decode();
    
    // Check if more data available
    bool hasMore() const { return m_pos < m_size; }
    
    // Get current position
    size_t getPosition() const { return m_pos; }
    
private:
    const uint8_t* m_data;
    size_t m_size;
    size_t m_pos;
    
    // Decode tag (3 bytes -> 4 chars)
    std::string decodeTag();
    
    // Decode type byte
    TdfType decodeType();
    
    // Decode variable-length integer
    int64_t decodeVarInt();
    uint64_t decodeVarUInt();
    
    // Decode specific types
    TdfInteger decodeInteger();
    TdfString decodeString();
    TdfBinary decodeBinary();
    TdfStruct decodeStruct();
    TdfList decodeList();
    TdfMapWrapper decodeMap();
    TdfIntList decodeIntList();
    TdfPair decodePair();
    TdfTriple decodeTriple();
    float decodeFloat();
    TdfStruct decodeUnion();
    
    // Read helpers
    uint8_t readByte();
    void readBytes(void* dest, size_t size);
};

/**
 * Helper to build TDF structures fluently
 */
class TdfBuilder {
public:
    TdfBuilder() = default;
    
    TdfBuilder& integer(const std::string& tag, int64_t value);

    // Signed integer variants
    TdfBuilder& int8(const std::string& tag, int8_t value);
    TdfBuilder& int16(const std::string& tag, int16_t value);
    TdfBuilder& int32(const std::string& tag, int32_t value);
    TdfBuilder& int64(const std::string& tag, int64_t value);

    // Unsigned integer variants
    TdfBuilder& uint8(const std::string& tag, uint8_t value);
    TdfBuilder& uint16(const std::string& tag, uint16_t value);
    TdfBuilder& uint32(const std::string& tag, uint32_t value);
    TdfBuilder& uint64(const std::string& tag, uint64_t value);

    // Boolean (encoded as 0/1 integer)
    TdfBuilder& boolean(const std::string& tag, bool value);

    // string(tag, value) — normal field
    TdfBuilder& string(const std::string& tag, const std::string& value);
    // string(key) — sets the current map entry key; only valid inside beginMap/endMap
    TdfBuilder& string(const std::string& key);

    TdfBuilder& binary(const std::string& tag, const std::vector<uint8_t>& value);
    TdfBuilder& pair(const std::string& tag, int64_t first, int64_t second);
    TdfBuilder& triple(const std::string& tag, uint32_t ip, uint16_t port, uint16_t protocol = 2);
    TdfBuilder& floatValue(const std::string& tag, float value);
    TdfBuilder& list(const std::string& tag, TdfType elementType, const std::vector<std::string>& values);
    TdfBuilder& intList(const std::string& tag, const std::vector<int64_t>& values);
    TdfBuilder& stringMap(const std::string& tag, const std::map<std::string, std::string>& values);

    // Typed map: keyType/valueType are "integer","string","struct","binary","float"
    TdfBuilder& beginMap(const std::string& tag, const std::string& keyType, const std::string& valueType);
    TdfBuilder& endMap();

    // Nested struct
    TdfBuilder& beginStruct(const std::string& tag);
    // beginStruct() with no tag: opens a struct as the value for the current map key
    TdfBuilder& beginStruct();
    TdfBuilder& endStruct();

    // Build final structure
    TdfStruct build();

    // Encode to bytes
    std::vector<uint8_t> encode();

private:
    TdfStruct m_root;

    struct StructFrame { TdfStruct* data; std::string tag; };
    struct MapFrame    { TdfMapWrapper* wrapper; std::string pendingKey; };
    using Frame = std::variant<StructFrame, MapFrame>;
    std::vector<Frame> m_frames;

    TdfStruct& current();
    bool       inMapContext() const;
};

// Render a decoded TdfStruct as indented XML for logging
std::string tdfToXml(const TdfStruct& tdf, int indent = 0);

} // namespace ds2::blaze
