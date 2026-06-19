#pragma once

#include "blaze/types.hpp"
#include <vector>
#include <cstddef>
#include <cstdint>
#include <memory>
#include <string>

namespace gw2::blaze {

class TdfEncoder {
public:
    TdfEncoder();
    
    std::vector<uint8_t> encode(const TdfStruct& data);
    
    void encodeInteger(const std::string& tag, int64_t value);
    void encodeString(const std::string& tag, const std::string& value);
    void encodeBinary(const std::string& tag, const std::vector<uint8_t>& value);
    void encodeStruct(const std::string& tag, const TdfStruct& value);
    void encodeList(const std::string& tag, TdfType elementType, const TdfList& value);
    void encodeMap(const std::string& tag, TdfType keyType, TdfType valueType, const TdfMapWrapper& value);
    void encodeUnion(const std::string& tag, const TdfUnion& value);
    void encodeVariable(const std::string& tag, const TdfVariable& value, TdfType wireType = TdfType::Variable);
    void encodeIntList(const std::string& tag, const TdfIntList& value);
    void encodeObjectType(const std::string& tag, const TdfObjectType& value);
    void encodeObjectId(const std::string& tag, const TdfObjectId& value);
    void encodeFloat(const std::string& tag, float value);
    void encodeTimeValue(const std::string& tag, int64_t microseconds);
    
    std::vector<uint8_t> getData() const { return m_buffer; }
    
    void reset() { m_buffer.clear(); }
    
private:
    std::vector<uint8_t> m_buffer;

    void encodeChild(const TdfValue& v);

    void encodeTag(const std::string& tag);
    
    void encodeVarInt(int64_t value);

    void encodeVarUInt(uint64_t value);

    void encodeStrLen(uint64_t value);

    void encodeRawString(const std::string& s);

    void writeBytes(const void* data, size_t size);

    void writeByte(uint8_t byte);
};

class TdfDecoder {
public:
    TdfDecoder(const std::vector<uint8_t>& data);
    TdfDecoder(const uint8_t* data, size_t size);
    
    TdfStruct decode();
    
    bool hasMore() const { return m_pos < m_size; }
    
    size_t getPosition() const { return m_pos; }
    
private:
    const uint8_t* m_data;
    size_t m_size;
    size_t m_pos;
    
    std::string decodeTag();
    
    TdfType decodeType();
    
    int64_t decodeVarInt();
    uint64_t decodeVarUInt();
    uint64_t decodeStrLen();
    
    TdfInteger    decodeInteger();
    TdfString     decodeString();
    TdfBinary     decodeBinary();
    TdfStruct     decodeStruct();
    TdfList       decodeList();
    TdfMapWrapper decodeMap();
    TdfUnion      decodeUnion();
    TdfVariable   decodeVariable();
    TdfIntList    decodeIntList();
    TdfObjectType decodeObjectType();
    TdfObjectId   decodeObjectId();
    float         decodeFloat();
    int64_t       decodeTimeValue();
    
    uint8_t readByte();
    void readBytes(void* dest, size_t size);
};

class TdfBuilder {
public:
    TdfBuilder() = default;
    
    TdfBuilder& integer(const std::string& tag, int64_t value);

    TdfBuilder& int8(const std::string& tag, int8_t value);
    TdfBuilder& int16(const std::string& tag, int16_t value);
    TdfBuilder& int32(const std::string& tag, int32_t value);
    TdfBuilder& int64(const std::string& tag, int64_t value);

    TdfBuilder& uint8(const std::string& tag, uint8_t value);
    TdfBuilder& uint16(const std::string& tag, uint16_t value);
    TdfBuilder& uint32(const std::string& tag, uint32_t value);
    TdfBuilder& uint64(const std::string& tag, uint64_t value);

    TdfBuilder& boolean(const std::string& tag, bool value);

    TdfBuilder& string(const std::string& tag, const std::string& value);

    TdfBuilder& string(const std::string& key);

    TdfBuilder& binary(const std::string& tag, const std::vector<uint8_t>& value);
    TdfBuilder& objectType(const std::string& tag, uint64_t component, uint64_t type);
    TdfBuilder& pair(const std::string& tag, int64_t component, int64_t type);
    TdfBuilder& objectId(const std::string& tag, uint64_t component, uint64_t type, uint64_t id);
    TdfBuilder& triple(const std::string& tag, uint64_t a, uint64_t b, uint64_t c = 2);
    TdfBuilder& floatValue(const std::string& tag, float value);
    TdfBuilder& timeValue(const std::string& tag, int64_t microseconds);
    TdfBuilder& variable(const std::string& tag, uint64_t tdfId, const TdfStruct& data);
    TdfBuilder& variable(const std::string& tag, std::nullptr_t);
    TdfBuilder& genericType(const std::string& tag, uint64_t tdfId, const TdfStruct& data);
    TdfBuilder& unionValue(const std::string& tag, uint8_t arm, std::shared_ptr<TdfValue> member = nullptr);
    TdfBuilder& list(const std::string& tag, TdfType elementType, const std::vector<std::string>& values);
    TdfBuilder& intList(const std::string& tag, const std::vector<int64_t>& values);
    TdfBuilder& objectIdList(const std::string& tag, const std::vector<TdfObjectId>& values);
    TdfBuilder& stringMap(const std::string& tag, const std::map<std::string, std::string>& values);
    TdfBuilder& integerMap(const std::string& tag, const std::map<std::string, int64_t>& values);

    TdfBuilder& beginMap(const std::string& tag, const std::string& keyType, const std::string& valueType);
    TdfBuilder& endMap();

    TdfBuilder& beginStruct(const std::string& tag);
    TdfBuilder& beginStruct();
    TdfBuilder& endStruct();

    TdfStruct build();

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

std::string tdfToBlaze(const TdfStruct& tdf, int indent = 0);
std::string blazePacketName(uint16_t comp, uint16_t cmd, MessageType msgType);

} // namespace gw2::blaze
