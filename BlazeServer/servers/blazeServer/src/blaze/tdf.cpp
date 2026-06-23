#include "blaze/tdf.hpp"
#include "utils/logger.hpp"
#include <cstring>
#include <stdexcept>
#include <unordered_map>

namespace gw2::blaze {

static uint8_t charToTagValue(char c) {
    unsigned char uc = static_cast<unsigned char>(c);
    if (uc < 0x20 || uc > 0x5F) return 0;
    return uc - 0x20;
}

static char tagValueToChar(uint8_t v) {
    return static_cast<char>(v + 0x20);
}


TdfEncoder::TdfEncoder() {
    m_buffer.reserve(256);
}

std::vector<uint8_t> TdfEncoder::encode(const TdfStruct& data) {
    m_buffer.clear();

    for (const auto& [tag, value] : data) {
        if (!value) continue;
        encodeChild(*value);
    }

    return m_buffer;
}

void TdfEncoder::encodeTag(const std::string& tag) {
    // Pad tag to 4 characters
    std::string t = tag;
    while (t.size() < 4) t += ' ';
    if (t.size() > 4) t = t.substr(0, 4);

    uint8_t v0 = charToTagValue(t[0]);
    uint8_t v1 = charToTagValue(t[1]);
    uint8_t v2 = charToTagValue(t[2]);
    uint8_t v3 = charToTagValue(t[3]);
    
    writeByte((v0 << 2) | (v1 >> 4));
    writeByte(((v1 & 0x0F) << 4) | (v2 >> 2));
    writeByte(((v2 & 0x03) << 6) | v3);
}

void TdfEncoder::encodeVarInt(int64_t value) {
    bool negative = value < 0;
    uint64_t mag  = negative ? static_cast<uint64_t>(-(value + 1)) + 1
                             : static_cast<uint64_t>(value);
    uint8_t first = mag & 0x3F;          // low 6 bits
    if (negative)   first |= 0x40;       // sign bit
    mag >>= 6;
    if (mag != 0)   first |= 0x80;       // continuation
    writeByte(first);
    while (mag != 0) {
        uint8_t byte = mag & 0x7F;
        mag >>= 7;
        if (mag != 0) byte |= 0x80;
        writeByte(byte);
    }
}

void TdfEncoder::encodeVarUInt(uint64_t value) {
    uint8_t first = value & 0x3F;
    value >>= 6;
    if (value != 0) first |= 0x80;
    writeByte(first);
    while (value != 0) {
        uint8_t byte = value & 0x7F;
        value >>= 7;
        if (value != 0) byte |= 0x80;
        writeByte(byte);
    }
}

void TdfEncoder::encodeStrLen(uint64_t value) {
    uint8_t first = value & 0x3F;   // low 6 bits in the first byte
    value >>= 6;
    if (value != 0) {
        first |= 0x80;              // more bytes follow
    }
    writeByte(first);
    while (value != 0) {
        uint8_t byte = value & 0x7F;
        value >>= 7;
        if (value != 0) {
            byte |= 0x80;
        }
        writeByte(byte);
    }
}

void TdfEncoder::encodeInteger(const std::string& tag, int64_t value) {
    encodeTag(tag);
    writeByte(static_cast<uint8_t>(TdfType::Integer));
    encodeVarInt(value);
}

void TdfEncoder::encodeString(const std::string& tag, const std::string& value) {
    encodeTag(tag);
    writeByte(static_cast<uint8_t>(TdfType::String));
    encodeStrLen(value.size() + 1);  // +1 for null terminator
    writeBytes(value.c_str(), value.size() + 1);
}

void TdfEncoder::encodeBinary(const std::string& tag, const std::vector<uint8_t>& value) {
    encodeTag(tag);
    writeByte(static_cast<uint8_t>(TdfType::Binary));
    encodeStrLen(value.size());
    writeBytes(value.data(), value.size());
}

void TdfEncoder::encodeStruct(const std::string& tag, const TdfStruct& value) {
    encodeTag(tag);
    writeByte(static_cast<uint8_t>(TdfType::Struct));

    for (const auto& [childTag, childValue] : value) {
        if (!childValue) continue;
        encodeChild(*childValue);
    }

    writeByte(0x00);
}

// waow that's a lot of types
void TdfEncoder::encodeChild(const TdfValue& v) {
    switch (v.type) {
        case TdfType::Integer:
            encodeInteger(v.tag, std::get<TdfInteger>(v.value));
            break;
        case TdfType::String:
            encodeString(v.tag, std::get<TdfString>(v.value));
            break;
        case TdfType::Binary:
            encodeBinary(v.tag, std::get<TdfBinary>(v.value));
            break;
        case TdfType::Struct:
            encodeStruct(v.tag, std::get<TdfStruct>(v.value));
            break;
        case TdfType::List: {
            const auto& lst = std::get<TdfList>(v.value);
            TdfType elemType = lst.empty() ? TdfType::Struct : lst.front()->type;
            encodeList(v.tag, elemType, lst);
            break;
        }
        case TdfType::Map: {
            const auto& m = std::get<TdfMapWrapper>(v.value);
            encodeMap(v.tag, m.keyType, m.valueType, m);
            break;
        }
        case TdfType::Union:
            encodeUnion(v.tag, std::get<TdfUnion>(v.value));
            break;
        case TdfType::Variable:
            encodeVariable(v.tag, std::get<TdfVariable>(v.value), TdfType::Variable);
            break;
        case TdfType::IntList:
            encodeIntList(v.tag, std::get<TdfIntList>(v.value));
            break;
        case TdfType::ObjectType:
            encodeObjectType(v.tag, std::get<TdfObjectType>(v.value));
            break;
        case TdfType::ObjectId:
            encodeObjectId(v.tag, std::get<TdfObjectId>(v.value));
            break;
        case TdfType::Float:
            encodeFloat(v.tag, std::get<float>(v.value));
            break;
        case TdfType::TimeValue:
            encodeTimeValue(v.tag, std::get<TdfInteger>(v.value));
            break;
        case TdfType::GenericType:
            encodeVariable(v.tag, std::get<TdfVariable>(v.value), TdfType::GenericType);
            break;
        default:
            break;
    }
}

void TdfEncoder::encodeList(const std::string& tag, TdfType elementType, const TdfList& value) {
    encodeTag(tag);
    writeByte(static_cast<uint8_t>(TdfType::List));
    writeByte(static_cast<uint8_t>(elementType));
    encodeStrLen(value.size());

    for (const auto& elem : value) {
        if (!elem) continue;
        switch (elementType) {
            case TdfType::String:
                encodeRawString(std::get<TdfString>(elem->value));
                break;
            case TdfType::Integer:
            case TdfType::TimeValue:
                encodeVarInt(std::get<TdfInteger>(elem->value));
                break;
            case TdfType::Struct: {
                if (std::holds_alternative<TdfUnion>(elem->value)) {
                    const auto& u = std::get<TdfUnion>(elem->value);
                    writeByte(u.arm);
                    if (u.member && std::holds_alternative<TdfStruct>(u.member->value)) {
                        for (const auto& [childTag, childValue] : std::get<TdfStruct>(u.member->value)) {
                            if (!childValue) continue;
                            encodeChild(*childValue);
                        }
                    }
                    writeByte(0x00);
                    break;
                }
                const auto& s = std::get<TdfStruct>(elem->value);
                for (const auto& [childTag, childValue] : s) {
                    if (!childValue) continue;
                    encodeChild(*childValue);
                }
                writeByte(0x00);
                break;
            }
            case TdfType::ObjectType: {
                const auto& ot = std::get<TdfObjectType>(elem->value);
                encodeVarUInt(ot.component);
                encodeVarUInt(ot.type);
                break;
            }
            case TdfType::ObjectId: {
                const auto& oid = std::get<TdfObjectId>(elem->value);
                encodeVarUInt(oid.component);
                encodeVarUInt(oid.type);
                encodeVarUInt(oid.id);
                break;
            }
            case TdfType::Float: {
                float f = std::get<float>(elem->value);
                writeBytes(&f, sizeof(float));
                break;
            }
            default:
                break;
        }
    }
}

void TdfEncoder::encodeRawString(const std::string& s) {
    encodeStrLen(s.size() + 1);  // +1 for null terminator
    writeBytes(s.data(), s.size());
    writeByte(0x00);
}

void TdfEncoder::encodeMap(const std::string& tag, TdfType keyType, TdfType valueType, const TdfMapWrapper& map) {
    encodeTag(tag);
    writeByte(static_cast<uint8_t>(TdfType::Map));
    writeByte(static_cast<uint8_t>(keyType));
    writeByte(static_cast<uint8_t>(valueType));
    encodeStrLen(map.data.size());

    for (const auto& [k, v] : map.data) {
        if (keyType == TdfType::String)
            encodeRawString(k);
        else if (keyType == TdfType::Integer || keyType == TdfType::TimeValue)
            encodeVarInt(std::stoll(k));  // keys are held as strings; emit as varint

        if (v) {
            switch (valueType) {
                case TdfType::String:
                    encodeRawString(std::get<TdfString>(v->value));
                    break;
                case TdfType::Integer:
                case TdfType::TimeValue:
                    encodeVarInt(std::get<TdfInteger>(v->value));
                    break;
                case TdfType::Struct: {
                    for (const auto& [childTag, childValue] : std::get<TdfStruct>(v->value)) {
                        if (!childValue) continue;
                        encodeChild(*childValue);
                    }
                    writeByte(0x00);
                    break;
                }
                case TdfType::ObjectType: {
                    const auto& ot = std::get<TdfObjectType>(v->value);
                    encodeVarUInt(ot.component);
                    encodeVarUInt(ot.type);
                    break;
                }
                case TdfType::ObjectId: {
                    const auto& oid = std::get<TdfObjectId>(v->value);
                    encodeVarUInt(oid.component);
                    encodeVarUInt(oid.type);
                    encodeVarUInt(oid.id);
                    break;
                }
                case TdfType::Float: {
                    float f = std::get<float>(v->value);
                    writeBytes(&f, sizeof(float));
                    break;
                }
                default:
                    break;
            }
        }
    }
}

void TdfEncoder::encodeIntList(const std::string& tag, const TdfIntList& value) {
    encodeTag(tag);
    writeByte(static_cast<uint8_t>(TdfType::List));     // HEAT_TYPE_LIST = 0x04
    writeByte(static_cast<uint8_t>(TdfType::Integer));  // element type: HEAT_TYPE_INTEGER = 0x00
    encodeStrLen(value.size());
    for (int64_t v : value) {
        encodeVarInt(v);
    }
}

void TdfEncoder::encodeUnion(const std::string& tag, const TdfUnion& value) {
    encodeTag(tag);
    writeByte(static_cast<uint8_t>(TdfType::Union));
    writeByte(value.arm);
    if (value.arm != 0x7F && value.arm != 0xFF && value.member) {
        encodeChild(*value.member);
    }
}

void TdfEncoder::encodeVariable(const std::string& tag, const TdfVariable& value, TdfType wireType) {
    encodeTag(tag);
    writeByte(static_cast<uint8_t>(wireType));
    if (!value.valid) {
        writeByte(0x00);
        return;
    }
    writeByte(0x01);  // valid
    encodeVarUInt(value.tdfId);
    for (const auto& [childTag, childValue] : value.data) {
        if (!childValue) continue;
        encodeChild(*childValue);
    }
    writeByte(0x00);
}

void TdfEncoder::encodeObjectType(const std::string& tag, const TdfObjectType& value) {
    encodeTag(tag);
    writeByte(static_cast<uint8_t>(TdfType::ObjectType));
    encodeVarUInt(value.component);
    encodeVarUInt(value.type);
}

void TdfEncoder::encodeObjectId(const std::string& tag, const TdfObjectId& value) {
    encodeTag(tag);
    writeByte(static_cast<uint8_t>(TdfType::ObjectId));
    encodeVarUInt(value.component);
    encodeVarUInt(value.type);
    encodeVarUInt(value.id);
}

void TdfEncoder::encodeFloat(const std::string& tag, float value) {
    encodeTag(tag);
    writeByte(static_cast<uint8_t>(TdfType::Float));
    writeBytes(&value, sizeof(float));
}

void TdfEncoder::encodeTimeValue(const std::string& tag, int64_t microseconds) {
    encodeTag(tag);
    writeByte(static_cast<uint8_t>(TdfType::TimeValue));
    encodeVarInt(microseconds);
}

void TdfEncoder::writeBytes(const void* data, size_t size) {
    const uint8_t* p = static_cast<const uint8_t*>(data);
    m_buffer.insert(m_buffer.end(), p, p + size);
}

void TdfEncoder::writeByte(uint8_t byte) {
    m_buffer.push_back(byte);
}


TdfDecoder::TdfDecoder(const std::vector<uint8_t>& data)
    : m_data(data.data()), m_size(data.size()), m_pos(0) {}

TdfDecoder::TdfDecoder(const uint8_t* data, size_t size)
    : m_data(data), m_size(size), m_pos(0) {}

TdfStruct TdfDecoder::decode() {
    TdfStruct result;
    
    while (hasMore()) {
        // Check for struct terminator
        if (m_data[m_pos] == 0x00) {
            m_pos++;
            break;
        }
        
        std::string tag = decodeTag();
        TdfType type = decodeType();
        
        auto value = std::make_shared<TdfValue>();
        value->tag = tag;
        value->type = type;
        
        switch (type) {
            case TdfType::Integer:
                value->value = decodeInteger();
                break;
            case TdfType::String:
                value->value = decodeString();
                break;
            case TdfType::Binary:
                value->value = decodeBinary();
                break;
            case TdfType::Struct:
                value->value = decodeStruct();
                break;
            case TdfType::List:
                value->value = decodeList();
                break;
            case TdfType::Map:
                value->value = decodeMap();
                break;
            case TdfType::Union:
                value->value = decodeUnion();
                break;
            case TdfType::Variable:
            case TdfType::GenericType:
                value->value = decodeVariable();
                break;
            case TdfType::ObjectType:
                value->value = decodeObjectType();
                break;
            case TdfType::ObjectId:
                value->value = decodeObjectId();
                break;
            case TdfType::Float:
                value->value = decodeFloat();
                break;
            case TdfType::TimeValue:
                value->value = decodeTimeValue();
                break;
            default:
                // We can't know an unknown type's length, so continuing would read
                // mid-value and drift (garbage tags -> read past the buffer -> throw).
                // Stop here and return what we have; the caller only needs a best effort.
                LOG_WARN("Unknown TDF type: 0x{:02X} (stopping decode)", static_cast<int>(type));
                return result;
        }

        result[tag] = value;
    }

    return result;
}

std::string TdfDecoder::decodeTag() {
    if (m_pos + 3 > m_size) {
        throw std::runtime_error("TDF: Not enough data for tag");
    }
    
    uint8_t b0 = readByte();
    uint8_t b1 = readByte();
    uint8_t b2 = readByte();

    uint8_t v0 = b0 >> 2;
    uint8_t v1 = ((b0 & 0x03) << 4) | (b1 >> 4);
    uint8_t v2 = ((b1 & 0x0F) << 2) | (b2 >> 6);
    uint8_t v3 = b2 & 0x3F;
    
    std::string tag;
    tag += tagValueToChar(v0);
    tag += tagValueToChar(v1);
    tag += tagValueToChar(v2);
    tag += tagValueToChar(v3);
    
    // Trim trailing spaces
    while (!tag.empty() && tag.back() == ' ') {
        tag.pop_back();
    }
    
    return tag;
}

TdfType TdfDecoder::decodeType() {
    return static_cast<TdfType>(readByte());
}

int64_t TdfDecoder::decodeVarInt() {
    if (!hasMore()) return 0;
    uint8_t first = readByte();
    bool negative = (first & 0x40) != 0;
    uint64_t result = first & 0x3F;
    int shift = 6;
    if (first & 0x80) {
        while (hasMore()) {
            uint8_t byte = readByte();
            result |= static_cast<uint64_t>(byte & 0x7F) << shift;
            shift += 7;
            if ((byte & 0x80) == 0) break;
            if (shift > 63) throw std::runtime_error("TDF: VarInt too large");
        }
    }
    return negative ? -static_cast<int64_t>(result) : static_cast<int64_t>(result);
}

uint64_t TdfDecoder::decodeVarUInt() {
    if (!hasMore()) return 0;
    uint8_t first = readByte();
    uint64_t result = first & 0x3F;       // bit 6 (sign) unused for unsigned values
    int shift = 6;
    if (first & 0x80) {
        while (hasMore()) {
            uint8_t byte = readByte();
            result |= static_cast<uint64_t>(byte & 0x7F) << shift;
            shift += 7;
            if ((byte & 0x80) == 0) break;
            if (shift > 63) throw std::runtime_error("TDF: VarInt too large");
        }
    }
    return result;
}

uint64_t TdfDecoder::decodeStrLen() {
    uint8_t  b0     = readByte();
    uint64_t result = b0 & 0x3F;
    if ((b0 & 0x80) == 0) return result;
    int shift = 6;
    while (hasMore()) {
        uint8_t b = readByte();
        result |= static_cast<uint64_t>(b & 0x7F) << shift;
        if ((b & 0x80) == 0) break;
        shift += 7;
        if (shift > 57) throw std::runtime_error("TDF: StrLen VarInt overflow");
    }
    return result;
}

TdfInteger TdfDecoder::decodeInteger() {
    return decodeVarInt();
}

TdfString TdfDecoder::decodeString() {
    uint64_t length = decodeStrLen();
    if (m_pos + length > m_size) {
        throw std::runtime_error("TDF: Not enough data for string");
    }

    std::string result(reinterpret_cast<const char*>(m_data + m_pos), length - 1);  // -1 for null
    m_pos += length;
    return result;
}

TdfBinary TdfDecoder::decodeBinary() {
    uint64_t length = decodeStrLen();
    if (m_pos + length > m_size) {
        throw std::runtime_error("TDF: Not enough data for binary");
    }
    
    TdfBinary result(m_data + m_pos, m_data + m_pos + length);
    m_pos += length;
    return result;
}

TdfStruct TdfDecoder::decodeStruct() {
    return decode();  // Recursively decode struct contents
}

TdfList TdfDecoder::decodeList() {
    TdfType elementType = decodeType();
    uint64_t count = decodeStrLen();
    
    TdfList result;
    result.reserve(count);
    
    for (uint64_t i = 0; i < count; i++) {
        auto elem = std::make_shared<TdfValue>();
        elem->type = elementType;
        
        switch (elementType) {
            case TdfType::Integer:
            case TdfType::TimeValue:  elem->value = decodeInteger();    break;
            case TdfType::String:     elem->value = decodeString();     break;
            case TdfType::Binary:     elem->value = decodeBinary();     break;
            case TdfType::Struct:     elem->value = decodeStruct();     break;
            case TdfType::ObjectType: elem->value = decodeObjectType(); break;
            case TdfType::ObjectId:   elem->value = decodeObjectId();   break;
            case TdfType::Float:      elem->value = decodeFloat();      break;
            case TdfType::Variable:
            case TdfType::GenericType: elem->value = decodeVariable();  break;
            default: break;
        }
        
        result.push_back(elem);
    }
    
    return result;
}

TdfMapWrapper TdfDecoder::decodeMap() {
    TdfType keyType   = decodeType();
    TdfType valueType = decodeType();
    uint64_t count    = decodeStrLen();

    TdfMapWrapper result;
    result.keyType   = keyType;
    result.valueType = valueType;

    for (uint64_t i = 0; i < count; i++) {
        std::string key;
        if (keyType == TdfType::String)
            key = decodeString();
        else if (keyType == TdfType::Integer || keyType == TdfType::TimeValue)
            key = std::to_string(decodeInteger());  // held as string in the wrapper
        else
            break;

        auto val = std::make_shared<TdfValue>();
        val->tag  = key;
        val->type = valueType;

        switch (valueType) {
            case TdfType::Integer:
            case TdfType::TimeValue:   val->value = decodeInteger();    break;
            case TdfType::String:      val->value = decodeString();     break;
            case TdfType::Binary:      val->value = decodeBinary();     break;
            case TdfType::Struct:      val->value = decodeStruct();     break;
            case TdfType::ObjectType:  val->value = decodeObjectType(); break;
            case TdfType::ObjectId:    val->value = decodeObjectId();   break;
            case TdfType::Float:       val->value = decodeFloat();      break;
            case TdfType::Variable:
            case TdfType::GenericType: val->value = decodeVariable();   break;
            default: break;
        }

        result.data[key] = val;
    }

    return result;
}

TdfIntList TdfDecoder::decodeIntList() {
    uint64_t count = decodeStrLen();
    
    TdfIntList result;
    result.reserve(count);
    
    for (uint64_t i = 0; i < count; i++) {
        result.push_back(decodeVarInt());
    }
    
    return result;
}

TdfObjectType TdfDecoder::decodeObjectType() {
    TdfObjectType result;
    result.component = decodeVarUInt();
    result.type      = decodeVarUInt();
    return result;
}

TdfObjectId TdfDecoder::decodeObjectId() {
    TdfObjectId result;
    result.component = decodeVarUInt();
    result.type      = decodeVarUInt();
    result.id        = decodeVarUInt();
    return result;
}

float TdfDecoder::decodeFloat() {
    float result;
    readBytes(&result, sizeof(float));
    return result;
}

int64_t TdfDecoder::decodeTimeValue() {
    return decodeVarInt();
}

TdfUnion TdfDecoder::decodeUnion() {
    TdfUnion result;
    result.arm = readByte();
    if (result.arm == 0x7F || result.arm == 0xFF) {
        return result;
    }
    std::string memberTag = decodeTag();
    TdfType memberType    = decodeType();
    auto member = std::make_shared<TdfValue>();
    member->tag  = memberTag;
    member->type = memberType;
    switch (memberType) {
        case TdfType::Integer:
        case TdfType::TimeValue:   member->value = decodeInteger();    break;
        case TdfType::String:      member->value = decodeString();     break;
        case TdfType::Binary:      member->value = decodeBinary();     break;
        case TdfType::Struct:      member->value = decodeStruct();     break;
        case TdfType::List:        member->value = decodeList();       break;
        case TdfType::Map:         member->value = decodeMap();        break;
        case TdfType::Union:       member->value = decodeUnion();      break;
        case TdfType::Variable:
        case TdfType::GenericType: member->value = decodeVariable();   break;
        case TdfType::ObjectType:  member->value = decodeObjectType(); break;
        case TdfType::ObjectId:    member->value = decodeObjectId();   break;
        case TdfType::Float:       member->value = decodeFloat();      break;
        default: break;
    }
    result.member = member;
    return result;
}

TdfVariable TdfDecoder::decodeVariable() {
    TdfVariable result;
    uint8_t valid = readByte();
    if (!valid) {
        return result;
    }
    result.valid = true;
    result.tdfId = decodeVarUInt();
    result.data  = decode();  // reads fields until 0x00 terminator
    return result;
}

uint8_t TdfDecoder::readByte() {
    if (m_pos >= m_size) {
        throw std::runtime_error("TDF: Unexpected end of data");
    }
    return m_data[m_pos++];
}

void TdfDecoder::readBytes(void* dest, size_t size) {
    if (m_pos + size > m_size) {
        throw std::runtime_error("TDF: Not enough data");
    }
    std::memcpy(dest, m_data + m_pos, size);
    m_pos += size;
}


static TdfType parseTdfTypeStr(const std::string& s) {
    if (s == "integer"    || s == "int")   return TdfType::Integer;
    if (s == "string"     || s == "str")   return TdfType::String;
    if (s == "binary"     || s == "blob")  return TdfType::Binary;
    if (s == "struct"     || s == "tdf")   return TdfType::Struct;
    if (s == "list")                       return TdfType::List;
    if (s == "map")                        return TdfType::Map;
    if (s == "union")                      return TdfType::Union;
    if (s == "variable")                   return TdfType::Variable;
    if (s == "objecttype" || s == "pair")  return TdfType::ObjectType;
    if (s == "objectid"   || s == "triple") return TdfType::ObjectId;
    if (s == "float")                      return TdfType::Float;
    if (s == "timevalue"  || s == "time")  return TdfType::TimeValue;
    if (s == "generictype")                return TdfType::GenericType;
    return TdfType::Integer;
}

TdfStruct& TdfBuilder::current() {
    for (auto it = m_frames.rbegin(); it != m_frames.rend(); ++it) {
        if (auto* sf = std::get_if<StructFrame>(&*it))
            return *sf->data;
    }
    return m_root;
}

bool TdfBuilder::inMapContext() const {
    return !m_frames.empty() && std::holds_alternative<MapFrame>(m_frames.back());
}

TdfBuilder& TdfBuilder::integer(const std::string& tag, int64_t value) {
    auto tdf = std::make_shared<TdfValue>(tag, TdfType::Integer, value);
    current()[tag] = tdf;
    return *this;
}

TdfBuilder& TdfBuilder::int8(const std::string& tag, int8_t value) {
    return integer(tag, static_cast<int64_t>(value));
}

TdfBuilder& TdfBuilder::int16(const std::string& tag, int16_t value) {
    return integer(tag, static_cast<int64_t>(value));
}

TdfBuilder& TdfBuilder::int32(const std::string& tag, int32_t value) {
    return integer(tag, static_cast<int64_t>(value));
}

TdfBuilder& TdfBuilder::int64(const std::string& tag, int64_t value) {
    return integer(tag, value);
}

TdfBuilder& TdfBuilder::uint8(const std::string& tag, uint8_t value) {
    return integer(tag, static_cast<int64_t>(value));
}

TdfBuilder& TdfBuilder::uint16(const std::string& tag, uint16_t value) {
    return integer(tag, static_cast<int64_t>(value));
}

TdfBuilder& TdfBuilder::uint32(const std::string& tag, uint32_t value) {
    return integer(tag, static_cast<int64_t>(value));
}

TdfBuilder& TdfBuilder::uint64(const std::string& tag, uint64_t value) {
    // Store bit pattern as int64; encodeVarInt re-casts to uint64 before VLE encoding
    return integer(tag, static_cast<int64_t>(value));
}


TdfBuilder& TdfBuilder::boolean(const std::string& tag, bool value) {
    return integer(tag, value ? 1 : 0);
}

TdfBuilder& TdfBuilder::string(const std::string& tag, const std::string& value) {
    auto tdf = std::make_shared<TdfValue>(tag, TdfType::String, value);
    current()[tag] = tdf;
    return *this;
}

TdfBuilder& TdfBuilder::string(const std::string& key) {
    auto& mf = std::get<MapFrame>(m_frames.back());
    mf.pendingKey = key;
    return *this;
}

TdfBuilder& TdfBuilder::binary(const std::string& tag, const std::vector<uint8_t>& value) {
    auto tdf = std::make_shared<TdfValue>(tag, TdfType::Binary, value);
    current()[tag] = tdf;
    return *this;
}

TdfBuilder& TdfBuilder::objectType(const std::string& tag, uint64_t component, uint64_t type) {
    TdfObjectType ot{component, type};
    current()[tag] = std::make_shared<TdfValue>(tag, TdfType::ObjectType, ot);
    return *this;
}

TdfBuilder& TdfBuilder::pair(const std::string& tag, int64_t component, int64_t type) {
    return objectType(tag, static_cast<uint64_t>(component), static_cast<uint64_t>(type));
}

TdfBuilder& TdfBuilder::objectId(const std::string& tag, uint64_t component, uint64_t type, uint64_t id) {
    TdfObjectId oid{component, type, id};
    current()[tag] = std::make_shared<TdfValue>(tag, TdfType::ObjectId, oid);
    return *this;
}

TdfBuilder& TdfBuilder::triple(const std::string& tag, uint64_t a, uint64_t b, uint64_t c) {
    return objectId(tag, a, b, c);
}

TdfBuilder& TdfBuilder::timeValue(const std::string& tag, int64_t microseconds) {
    current()[tag] = std::make_shared<TdfValue>(tag, TdfType::TimeValue, TdfInteger(microseconds));
    return *this;
}

TdfBuilder& TdfBuilder::variable(const std::string& tag, uint64_t tdfId, const TdfStruct& data) {
    TdfVariable v{true, tdfId, data};
    current()[tag] = std::make_shared<TdfValue>(tag, TdfType::Variable, v);
    return *this;
}

TdfBuilder& TdfBuilder::variable(const std::string& tag, std::nullptr_t) {
    current()[tag] = std::make_shared<TdfValue>(tag, TdfType::Variable, TdfVariable{false, 0, {}});
    return *this;
}

TdfBuilder& TdfBuilder::genericType(const std::string& tag, uint64_t tdfId, const TdfStruct& data) {
    TdfVariable v{true, tdfId, data};
    current()[tag] = std::make_shared<TdfValue>(tag, TdfType::GenericType, v);
    return *this;
}

TdfBuilder& TdfBuilder::unionValue(const std::string& tag, uint8_t arm, std::shared_ptr<TdfValue> member) {
    TdfUnion u{arm, member};
    current()[tag] = std::make_shared<TdfValue>(tag, TdfType::Union, u);
    return *this;
}

TdfBuilder& TdfBuilder::floatValue(const std::string& tag, float value) {
    auto tdf = std::make_shared<TdfValue>(tag, TdfType::Float, value);
    current()[tag] = tdf;
    return *this;
}

TdfBuilder& TdfBuilder::list(const std::string& tag, TdfType elementType, const std::vector<std::string>& values) {
    TdfList lst;
    lst.reserve(values.size());
    for (const auto& s : values)
        lst.push_back(std::make_shared<TdfValue>("", elementType, TdfString(s)));
    current()[tag] = std::make_shared<TdfValue>(tag, TdfType::List, std::move(lst));
    return *this;
}


TdfBuilder& TdfBuilder::intList(const std::string& tag, const std::vector<int64_t>& values) {
    auto tdf = std::make_shared<TdfValue>(tag, TdfType::IntList, values);
    current()[tag] = tdf;
    return *this;
}

TdfBuilder& TdfBuilder::objectIdList(const std::string& tag, const std::vector<TdfObjectId>& values) {
    TdfList lst;
    lst.reserve(values.size());
    for (const auto& oid : values)
        lst.push_back(std::make_shared<TdfValue>("", TdfType::ObjectId, oid));
    current()[tag] = std::make_shared<TdfValue>(tag, TdfType::List, std::move(lst));
    return *this;
}

TdfBuilder& TdfBuilder::stringMap(const std::string& tag, const std::map<std::string, std::string>& values) {
    TdfMapWrapper wrapper;
    wrapper.keyType   = TdfType::String;
    wrapper.valueType = TdfType::String;
    for (const auto& [k, v] : values) {
        wrapper.data[k] = std::make_shared<TdfValue>(k, TdfType::String, TdfString(v));
    }
    current()[tag] = std::make_shared<TdfValue>(tag, TdfType::Map, TdfVariant(wrapper));
    return *this;
}

TdfBuilder& TdfBuilder::integerMap(const std::string& tag, const std::map<std::string, int64_t>& values) {
    TdfMapWrapper wrapper;
    wrapper.keyType   = TdfType::String;
    wrapper.valueType = TdfType::Integer;
    for (const auto& [k, v] : values) {
        wrapper.data[k] = std::make_shared<TdfValue>(k, TdfType::Integer, TdfInteger(v));
    }
    current()[tag] = std::make_shared<TdfValue>(tag, TdfType::Map, TdfVariant(wrapper));
    return *this;
}

TdfBuilder& TdfBuilder::intKeyStringMap(const std::string& tag, const std::map<std::string, std::string>& values) {
    TdfMapWrapper wrapper;
    wrapper.keyType   = TdfType::Integer;   // keys held as numeric strings, emitted as varints
    wrapper.valueType = TdfType::String;
    for (const auto& [k, v] : values) {
        wrapper.data[k] = std::make_shared<TdfValue>(k, TdfType::String, v);
    }
    current()[tag] = std::make_shared<TdfValue>(tag, TdfType::Map, TdfVariant(wrapper));
    return *this;
}

TdfBuilder& TdfBuilder::beginMap(const std::string& tag, const std::string& keyType, const std::string& valueType) {
    TdfMapWrapper wrapper;
    wrapper.keyType   = parseTdfTypeStr(keyType);
    wrapper.valueType = parseTdfTypeStr(valueType);

    auto tdf = std::make_shared<TdfValue>(tag, TdfType::Map, TdfVariant(wrapper));
    current()[tag] = tdf;

    TdfMapWrapper* ptr = &std::get<TdfMapWrapper>(tdf->value);
    m_frames.push_back(MapFrame{ptr, {}});
    return *this;
}

TdfBuilder& TdfBuilder::endMap() {
    if (inMapContext())
        m_frames.pop_back();
    return *this;
}

TdfBuilder& TdfBuilder::beginStruct(const std::string& tag) {
    auto tdf = std::make_shared<TdfValue>(tag, TdfType::Struct, TdfStruct{});
    current()[tag] = tdf;
    m_frames.push_back(StructFrame{&std::get<TdfStruct>(tdf->value), tag});
    return *this;
}

TdfBuilder& TdfBuilder::beginStruct() {
    // List element: append an (untagged) struct to the enclosing list.
    if (!m_frames.empty() && std::holds_alternative<ListFrame>(m_frames.back())) {
        auto& lf = std::get<ListFrame>(m_frames.back());
        auto tdf = std::make_shared<TdfValue>("", TdfType::Struct, TdfStruct{});
        lf.list->push_back(tdf);
        m_frames.push_back(StructFrame{&std::get<TdfStruct>(tdf->value), ""});
        return *this;
    }
    // Map value: struct keyed by the pending key.
    auto& mf = std::get<MapFrame>(m_frames.back());
    const std::string& key = mf.pendingKey;

    auto tdf = std::make_shared<TdfValue>(key, TdfType::Struct, TdfStruct{});
    mf.wrapper->data[key] = tdf;
    m_frames.push_back(StructFrame{&std::get<TdfStruct>(tdf->value), key});
    return *this;
}

TdfBuilder& TdfBuilder::beginList(const std::string& tag) {
    auto tdf = std::make_shared<TdfValue>(tag, TdfType::List, TdfList{});
    current()[tag] = tdf;
    m_frames.push_back(ListFrame{&std::get<TdfList>(tdf->value)});
    return *this;
}

TdfBuilder& TdfBuilder::endList() {
    if (!m_frames.empty() && std::holds_alternative<ListFrame>(m_frames.back()))
        m_frames.pop_back();
    return *this;
}

TdfBuilder& TdfBuilder::endStruct() {
    if (!m_frames.empty() && std::holds_alternative<StructFrame>(m_frames.back())) {
        m_frames.pop_back();
        // If we just closed a map-value struct, clear the pending key
        if (inMapContext())
            std::get<MapFrame>(m_frames.back()).pendingKey.clear();
    }
    return *this;
}

TdfStruct TdfBuilder::build() {
    return m_root;
}

std::vector<uint8_t> TdfBuilder::encode() {
    TdfEncoder encoder;
    return encoder.encode(m_root);
}


static std::string fmtBlazeInt(int64_t v) {
    uint64_t u = static_cast<uint64_t>(v);
    char buf[64];
    if (u <= 0xFFFFu)
        snprintf(buf, sizeof(buf), "%lld (0x%04llX)", (long long)v, (unsigned long long)u);
    else if (u <= 0xFFFFFFFFu)
        snprintf(buf, sizeof(buf), "%lld (0x%08llX)", (long long)v, (unsigned long long)u);
    else
        snprintf(buf, sizeof(buf), "%lld (0x%016llX)", (long long)v, (unsigned long long)u);
    return buf;
}

static std::string blazeIndent(int n) { return std::string(n * 2, ' '); }

static std::string renderBlazeValue(const TdfValue& v, int indent, const std::string& label);

static std::string renderBlazeStruct(const TdfStruct& s, int indent) {
    std::string out;
    for (const auto& [tag, child] : s) {
        if (!child) continue;
        out += renderBlazeValue(*child, indent, child->tag);
    }
    return out;
}

static std::string renderBlazeValue(const TdfValue& v, int indent, const std::string& label) {
    const std::string ind = blazeIndent(indent);
    std::string out;

    switch (v.type) {
        case TdfType::Integer:
            out = ind + label + " = " + fmtBlazeInt(std::get<TdfInteger>(v.value)) + "\n";
            break;
        case TdfType::String:
            out = ind + label + " = \"" + std::get<TdfString>(v.value) + "\"\n";
            break;
        case TdfType::Binary: {
            const auto& b = std::get<TdfBinary>(v.value);
            std::string hex;
            char tmp[3];
            for (uint8_t byte : b) { snprintf(tmp, sizeof(tmp), "%02X", byte); hex += tmp; }
            out = ind + label + " = [" + hex + "]\n";
            break;
        }
        case TdfType::Struct:
            out  = ind + label + " = {\n";
            out += renderBlazeStruct(std::get<TdfStruct>(v.value), indent + 1);
            out += ind + "}\n";
            break;
        case TdfType::Union: {
            const auto& u = std::get<TdfUnion>(v.value);
            if (u.arm == 0x7F || u.arm == 0xFF || !u.member) {
                out = ind + label + " = union{inactive}\n";
            } else {
                out  = ind + label + " = union[arm=" + std::to_string(u.arm) + "] {\n";
                out += renderBlazeValue(*u.member, indent + 1, u.member->tag);
                out += ind + "}\n";
            }
            break;
        }
        case TdfType::Variable:
        case TdfType::GenericType: {
            const auto& var = std::get<TdfVariable>(v.value);
            if (!var.valid) {
                out = ind + label + " = variable{invalid}\n";
            } else {
                out  = ind + label + " = variable[tdfId=" + std::to_string(var.tdfId) + "] {\n";
                out += renderBlazeStruct(var.data, indent + 1);
                out += ind + "}\n";
            }
            break;
        }
        case TdfType::List: {
            const auto& list = std::get<TdfList>(v.value);
            out = ind + label + " = [\n";
            for (size_t i = 0; i < list.size(); ++i) {
                if (!list[i]) continue;
                out += renderBlazeValue(*list[i], indent + 1, "[" + std::to_string(i) + "]");
            }
            out += ind + "]\n";
            break;
        }
        case TdfType::Map: {
            const auto& m = std::get<TdfMapWrapper>(v.value);
            out = ind + label + " = [\n";
            for (const auto& [k, val] : m.data) {
                if (!val) {
                    out += ind + "  (\"" + k + "\", ?)\n";
                } else if (val->type == TdfType::Struct) {
                    out += ind + "  (\"" + k + "\", {\n";
                    out += renderBlazeStruct(std::get<TdfStruct>(val->value), indent + 2);
                    out += ind + "  })\n";
                } else if (val->type == TdfType::String) {
                    out += ind + "  (\"" + k + "\", \"" + std::get<TdfString>(val->value) + "\")\n";
                } else if (val->type == TdfType::Integer) {
                    out += ind + "  (\"" + k + "\", " + fmtBlazeInt(std::get<TdfInteger>(val->value)) + ")\n";
                } else {
                    out += ind + "  (\"" + k + "\", ?)\n";
                }
            }
            out += ind + "]\n";
            break;
        }
        case TdfType::IntList: {
            const auto& il = std::get<TdfIntList>(v.value);
            out = ind + label + " = [\n";
            for (size_t i = 0; i < il.size(); ++i)
                out += ind + "  [" + std::to_string(i) + "] = " + fmtBlazeInt(il[i]) + "\n";
            out += ind + "]\n";
            break;
        }
        case TdfType::ObjectType: {
            const auto& ot = std::get<TdfObjectType>(v.value);
            char buf[64];
            snprintf(buf, sizeof(buf), "(comp=0x%04llX, type=0x%04llX)",
                (unsigned long long)ot.component, (unsigned long long)ot.type);
            out = ind + label + " = " + buf + "\n";
            break;
        }
        case TdfType::ObjectId: {
            const auto& oid = std::get<TdfObjectId>(v.value);
            char buf[64];
            snprintf(buf, sizeof(buf), "(comp=0x%04llX, type=0x%04llX, id=%llu)",
                (unsigned long long)oid.component, (unsigned long long)oid.type,
                (unsigned long long)oid.id);
            out = ind + label + " = " + buf + "\n";
            break;
        }
        case TdfType::Float:
            out = ind + label + " = " + std::to_string(std::get<float>(v.value)) + "\n";
            break;
        case TdfType::TimeValue:
            out = ind + label + " = " + std::to_string(std::get<TdfInteger>(v.value)) + "us\n";
            break;
        default:
            out = ind + label + " = ?\n";
            break;
    }
    return out;
}

std::string tdfToBlaze(const TdfStruct& tdf, int indent) {
    return renderBlazeStruct(tdf, indent);
}

std::string blazePacketName(uint16_t comp, uint16_t cmd, MessageType msgType) {
    static const std::unordered_map<uint32_t, const char*> kNotifTable = {
        // UserSessions (0x7802) — binary 1419bd6b0
        { 0x78020001, "UserSessionExtendedDataUpdate" },
        { 0x78020002, "UserAdded"                     },
        { 0x78020003, "UserRemoved"                   },
        { 0x78020005, "UserUpdated"                   },
        { 0x78020008, "UserAuthenticated"             },
        { 0x78020009, "UserUnauthenticated"           },
        { 0x7802000C, "ServerDraining"                },
    };

    struct Names { 
        const char* req; 
        const char* rsp; 
    };

    // big-ass table of req/res names
    static const std::unordered_map<uint32_t, Names> kTable = {
        // Util (0x0009)
        { 0x00090001, { "FetchClientConfigRequest",                  "FetchConfigResponse"                  } },
        { 0x00090002, { "Ping",                                       "PingResponse"                         } },
        { 0x00090003, { "ClientData",                                 nullptr                                } },
        { 0x00090004, { "LocalizeStringsRequest",                     "LocalizeStringsResponse"              } },
        { 0x00090005, { "GetTelemetryServerRequest",                  "GetTelemetryServerResponse"           } },
        { 0x00090006, { nullptr,                                      "GetTickerServerResponse"              } },
        { 0x00090007, { "PreAuthRequest",                             "PreAuthResponse"                      } },
        { 0x00090008, { "PostAuthRequest",                            "PostAuthResponse"                     } },
        { 0x0009000A, { "UserSettingsLoadRequest",                    "UserSettingsResponse"                 } },
        { 0x0009000B, { "UserSettingsSaveRequest",                    nullptr                                } },
        { 0x0009000C, { nullptr,                                      "UserSettingsLoadAllResponse"          } },
        { 0x0009000D, { "UserSettingsLoadAllRequest",                 "UserSettingsLoadAllResponse"          } },
        { 0x0009000E, { "DeleteUserSettingsRequest",                  nullptr                                } },
        { 0x00090015, { nullptr,                                      "QosConfigInfo"                        } },
        { 0x00090016, { "ClientMetrics",                              nullptr                                } },
        { 0x00090017, { "SetConnectionStateRequest",                  nullptr                                } },
        { 0x00090019, { "GetUserOptionsRequest",                      "UserOptions"                          } },
        { 0x0009001A, { "UserOptions",                                nullptr                                } },
        { 0x0009001B, { "SuspendUserPingRequest",                     nullptr                                } },
        { 0x0009001C, { "ClientState",                                nullptr                                } },
        // Authentication (0x0001)
        { 0x0001000A, { "LoginRequest",                               "LoginResponse"                        } },
        { 0x0001000B, { "TrustedLoginRequest",                        "LoginResponse"                        } },
        { 0x00010014, { "UpdateAccountRequest",                       "UpdateAccountResponse"                } },
        { 0x0001001D, { "ListUserEntitlements2Request",               "Entitlements"                         } },
        { 0x0001001E, { nullptr,                                      "AccountInfo"                          } },
        { 0x0001001F, { "PostEntitlementRequest",                     nullptr                                } },
        { 0x00010020, { "ListEntitlementsRequest",                    "Entitlements"                         } },
        { 0x00010027, { "GrantEntitlement2Request",                   "GrantEntitlement2Response"            } },
        { 0x0001002B, { "ModifyEntitlement2Request",                  nullptr                                } },
        { 0x0001002F, { "GetLegalDocContentRequest",                  "GetLegalDocContentResponse"           } },
        { 0x00010030, { "ListPersonaEntitlements2Request",            "Entitlements"                         } },
        { 0x0001003C, { "ExpressLoginRequest",                        "LoginResponse"                        } },
        { 0x0001003D, { "StressLoginRequest",                         "LoginResponse"                        } },
        { 0x00010046, { nullptr,                                      nullptr                                } },
        { 0x0001005A, { nullptr,                                      "GetPersonaResponse"                   } },
        { 0x00010064, { nullptr,                                      "ListPersonasResponse"                 } },
        { 0x00010104, { "GetOriginPersonaRequest",                    "GetPersonaResponse"                   } },
        { 0x00010122, { nullptr,                                      "SessionInfo"                          } },
        { 0x0001012C, { "GetDecryptedBlazeIdsRequest",                "GetDecryptedBlazeIdsResponse"         } },
        // Redirector (0x0005)
        { 0x00050001, { "ServerInstanceRequest",                      "ServerInstanceInfo"                   } },
        // Stats (0x0007)
        { 0x00070001, { "GetStatDescsRequest",                        "StatDescs"                            } },
        { 0x00070002, { "GetStatsRequest",                            "GetStatsResponse"                     } },
        { 0x00070003, { nullptr,                                      "StatGroupList"                        } },
        { 0x00070004, { "GetStatGroupRequest",                        "StatGroupResponse"                    } },
        { 0x00070005, { "GetStatsByGroupRequest",                     "GetStatsResponse"                     } },
        { 0x00070008, { "UpdateStatsRequest",                         nullptr                                } },
        { 0x00070009, { "WipeStatsRequest",                           nullptr                                } },
        { 0x0007000A, { "LeaderboardGroupRequest",                    "LeaderboardGroupResponse"             } },
        { 0x0007000C, { "LeaderboardStatsRequest",                    "LeaderboardStatValues"                } },
        { 0x0007000D, { "CenteredLeaderboardStatsRequest",            "LeaderboardStatValues"                } },
        { 0x0007000E, { "FilteredLeaderboardStatsRequest",            "LeaderboardStatValues"                } },
        // GameManager (0x0004)
        { 0x00040001, { "CreateGameRequest",                          "CreateGameResponse"                   } },
        { 0x00040002, { "DestroyGameRequest",                         "DestroyGameResponse"                  } },
        { 0x00040003, { "AdvanceGameStateRequest",                    nullptr                                } },
        { 0x00040004, { "SetGameSettingsRequest",                     nullptr                                } },
        { 0x00040005, { "SetPlayerCapacityRequest",                   nullptr                                } },
        { 0x00040006, { "SetPresenceModeRequest",                     nullptr                                } },
        { 0x00040007, { "SetGameAttributesRequest",                   nullptr                                } },
        { 0x00040008, { "SetPlayerAttributesRequest",                 nullptr                                } },
        { 0x00040009, { "JoinGameRequest",                            "JoinGameResponse"                     } },
        { 0x0004000B, { "RemovePlayerRequest",                        nullptr                                } },
        { 0x0004000F, { "FinalizeGameCreationRequest",                nullptr                                } },
        { 0x00040010, { "StartMatchmakingScenarioRequest",            "StartMatchmakingScenarioResponse"     } },
        { 0x00040011, { "CancelMatchmakingScenarioRequest",           nullptr                                } },
        { 0x00040012, { "SetPlayerCustomDataRequest",                 nullptr                                } },
        { 0x00040013, { "ReplayGameRequest",                          nullptr                                } },
        { 0x00040014, { "ReturnDedicatedServerToPoolRequest",         nullptr                                } },
        { 0x00040016, { "LeaveGameByGroupRequest",                    nullptr                                } },
        { 0x00040017, { "MigrateHostRequest",                         nullptr                                } },
        { 0x00040018, { "UpdateGameHostMigrationStatusRequest",       nullptr                                } },
        { 0x00040019, { "ResetDedicatedServerRequest",                "JoinGameResponse"                     } },
        { 0x0004001A, { "UpdateGameSessionRequest",                   nullptr                                } },
        { 0x0004001B, { "BanPlayerRequest",                           nullptr                                } },
        { 0x0004001D, { "UpdateMeshConnectionRequest",                nullptr                                } },
        { 0x0004001E, { "JoinGameByUserListRequest",                  "JoinGameResponse"                     } },
        { 0x00040026, { "AddQueuedPlayerToGameRequest",               nullptr                                } },
        { 0x00040027, { "UpdateGameNameRequest",                      nullptr                                } },
        { 0x00040028, { "EjectHostRequest",                           nullptr                                } },
        { 0x0004002D, { "CreateOrJoinGameRequest",                    "JoinGameResponse"                     } },
        { 0x00040064, { "GetGameListRequest",                         "GetGameListResponse"                  } },
        { 0x00040065, { "GetGameListRequest",                         "GetGameListResponse"                  } },
        { 0x00040066, { "DestroyGameListRequest",                     nullptr                                } },
        { 0x00040067, { "GetFullGameDataRequest",                     "GetFullGameDataResponse"              } },
        { 0x00040069, { "GetGameDataFromIdRequest",                   "GameBrowserDataList"                  } },
        { 0x00040098, { "GetGameListRequest",                         "GetGameListSyncResponse"              } },
        { 0x000400AB, { "TelemetryReportRequest",                     nullptr                                } },
        { 0x000400B1, { "UpdatePrimaryExternalSessionForUserRequest",  "UpdatePrimaryExternalSessionForUserResponse" } },
        // PvzGw (0x0805)
        { 0x08050002, { "CheckUserMessagesRequest",                   "CheckUserMessagesResponse"            } },
        { 0x08050003, { "SetSurveyCompletedRequest",                  nullptr                                } },
        { 0x08050004, { "GetStoreItemListRequest",                    "GetStoreItemListResponse"             } },
        { 0x08050005, { "GetPersistedLicensesRequest",                "GetPersistedLicensesResponse"         } },
        { 0x08050006, { "GrantPersistedLicensesForEntitlementsRequest", nullptr                              } },
        { 0x08050007, { "SetXPMultiplierRequest",                     nullptr                                } },
        { 0x08050008, { "ClearPersistedLicensesRequest",              nullptr                                } },
        { 0x08050009, { "GetDailyQuestsRequest",                      "GetDailyQuestsResponse"               } },
        { 0x0805000B, { "GrantPersistedLicensesForContentRequest",    nullptr                                } },
        { 0x0805000C, { "SetOnlineAccessEntitlementsRequest",         nullptr                                } },
        { 0x0805000E, { "GetUserMessagesRequest",                     "GetUserMessagesResponse"              } },
        { 0x0805000F, { "GrantPersistedLicensesForUserEventsRequest", nullptr                                } },
        { 0x08050010, { "UpdateUserMessageStatusRequest",             nullptr                                } },
        { 0x08050011, { "GetClientSettingsRequest",                   "GetClientSettingsResponse"            } },
        { 0x08050012, { "GetCommunityAchievementsRequest",            "GetCommunityAchievementsResponse"     } },
        { 0x08050013, { "ClaimCommunityEventRewardRequest",           nullptr                                } },
        { 0x08050014, { "GetBlackMarketDataRequest",                  "GetBlackMarketDataResponse"           } },
        { 0x08050015, { "PurchaseBlackMarketItemRequest",             nullptr                                } },
        { 0x08050016, { "SetBlackMarketViewedRequest",                nullptr                                } },
        { 0x08050017, { "GetCommunityPortalDataRequest",              "GetCommunityPortalDataResponse"       } },
        { 0x08050018, { "OpenCommunityPortalChestRequest",            nullptr                                } },
        { 0x08050019, { "ForceClientNotificationRequest",             nullptr                                } },
        { 0x0805001E, { "GetPlaylistsRequest",                        "GetPlaylistsResponse"                 } },
        { 0x0805001F, { "GetPlaylistRotationRequest",                 "GetPlaylistRotationResponse"          } },
        { 0x0805003C, { "GetLoyaltyChallengeDataRequest",             "GetLoyaltyChallengeDataResponse"      } },
        // UserSessions (0x7802)
        { 0x78020001, { "ValidateSessionKeyRequest",                  "SessionInfo"                          } },
        { 0x78020003, { "FetchExtendedDataRequest",                   "UserSessionExtendedData"              } },
        { 0x78020005, { "UpdateExtendedDataAttributeRequest",         nullptr                                } },
        { 0x78020008, { "UpdateHardwareFlagsRequest",                 nullptr                                } },
        { 0x7802000C, { "UserIdentification",                         "UserData"                             } },
        { 0x7802000D, { "LookupUsersRequest",                         "UserDataResponse"                     } },
        { 0x7802000E, { "LookupUsersByPrefixRequest",                 "UserDataResponse"                     } },
        { 0x78020014, { "UpdateNetworkInfoRequest",                   nullptr                                } },
        { 0x78020017, { "UserIdentification",                         "GeoLocationData"                      } },
        { 0x78020019, { "UpdateUserSessionClientDataRequest",         nullptr                                } },
        { 0x7802001A, { "SetUserInfoAttributeRequest",                nullptr                                } },
        { 0x7802001B, { "ResetUserGeoIPDataRequest",                  nullptr                                } },
    };

    static auto compName = [](uint16_t c) -> const char* {
        switch (c) {
            case 0x0001: return "Authentication";
            case 0x0004: return "GameManager";
            case 0x0005: return "Redirector";
            case 0x0007: return "Stats";
            case 0x0009: return "Util";
            case 0x0019: return "AssociationLists";
            case 0x0801: return "RSP";
            case 0x0802: return "Packs";
            case 0x0805: return "PvzGw";
            case 0x7802: return "UserSessions";
            default:     return nullptr;
        }
    };

    char compBuf[16];
    const char* cn = compName(comp);
    if (!cn) { snprintf(compBuf, sizeof(compBuf), "Comp0x%04X", comp); cn = compBuf; }

    uint32_t key = ((uint32_t)comp << 16) | cmd;

    if (msgType == MessageType::Notification) {
        auto it = kNotifTable.find(key);
        if (it != kNotifTable.end())
            return std::string("Blaze::") + cn + "::" + it->second;
        char fallback[32];
        snprintf(fallback, sizeof(fallback), "Notif0x%04X", cmd);
        return std::string("Blaze::") + cn + "::" + fallback;
    }

    bool isReply = (msgType != MessageType::Message);
    auto it = kTable.find(key);
    if (it != kTable.end()) {
        const char* name = isReply ? it->second.rsp : it->second.req;
        if (name) return std::string("Blaze::") + cn + "::" + name;
    }

    char fallback[32];
    snprintf(fallback, sizeof(fallback), "Cmd0x%04X%s", cmd, isReply ? "Response" : "Request");
    return std::string("Blaze::") + cn + "::" + fallback;
}

} // namespace gw2::blaze
