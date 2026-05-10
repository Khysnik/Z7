#include "blaze/tdf.hpp"
#include "utils/logger.hpp"
#include <cstring>
#include <stdexcept>

namespace ds2::blaze {

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

    // Struct terminator
    writeByte(0x00);

    return m_buffer;
}

void TdfEncoder::encodeTag(const std::string& tag) {
    // Pad or truncate tag to 4 characters
    std::string t = tag;
    while (t.size() < 4) t += ' ';
    if (t.size() > 4) t = t.substr(0, 4);
    
    // Convert 4 chars to 3 bytes (6 bits each)
    uint8_t v0 = charToTagValue(t[0]);
    uint8_t v1 = charToTagValue(t[1]);
    uint8_t v2 = charToTagValue(t[2]);
    uint8_t v3 = charToTagValue(t[3]);
    
    writeByte((v0 << 2) | (v1 >> 4));
    writeByte(((v1 & 0x0F) << 4) | (v2 >> 2));
    writeByte(((v2 & 0x03) << 6) | v3);
}

void TdfEncoder::encodeVarInt(int64_t value) {
    // BlazeSDK uses raw unsigned VLE, not zigzag
    encodeVarUInt(static_cast<uint64_t>(value));
}

void TdfEncoder::encodeVarUInt(uint64_t value) {
    do {
        uint8_t byte = value & 0x7F;
        value >>= 7;
        if (value != 0) {
            byte |= 0x80;
        }
        writeByte(byte);
    } while (value != 0);
}

void TdfEncoder::encodeInteger(const std::string& tag, int64_t value) {
    encodeTag(tag);
    writeByte(static_cast<uint8_t>(TdfType::Integer));
    encodeVarInt(value);
}

void TdfEncoder::encodeString(const std::string& tag, const std::string& value) {
    encodeTag(tag);
    writeByte(static_cast<uint8_t>(TdfType::String));
    encodeVarUInt(value.size() + 1);  // +1 for null terminator
    writeBytes(value.c_str(), value.size() + 1);
}

void TdfEncoder::encodeBinary(const std::string& tag, const std::vector<uint8_t>& value) {
    encodeTag(tag);
    writeByte(static_cast<uint8_t>(TdfType::Binary));
    encodeVarUInt(value.size());
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
        case TdfType::IntList:
            encodeIntList(v.tag, std::get<TdfIntList>(v.value));
            break;
        case TdfType::Pair:
            encodePair(v.tag, std::get<TdfPair>(v.value));
            break;
        case TdfType::Triple:
            encodeTriple(v.tag, std::get<TdfTriple>(v.value));
            break;
        case TdfType::Float:
            encodeFloat(v.tag, std::get<float>(v.value));
            break;
        default:
            break;
    }
}

void TdfEncoder::encodeList(const std::string& tag, TdfType elementType, const TdfList& value) {
    encodeTag(tag);
    writeByte(static_cast<uint8_t>(TdfType::List));
    writeByte(static_cast<uint8_t>(elementType));
    encodeVarUInt(value.size());
    
    for (const auto& elem : value) {
        if (!elem) continue;
        switch (elementType) {
            case TdfType::String:
                encodeRawString(std::get<TdfString>(elem->value));
                break;
            case TdfType::Integer:
                encodeVarInt(std::get<TdfInteger>(elem->value));
                break;
            case TdfType::Struct: {
                const auto& s = std::get<TdfStruct>(elem->value);
                for (const auto& [childTag, childValue] : s) {
                    if (!childValue) continue;
                    encodeChild(*childValue);
                }
                writeByte(0x00);
                break;
            }
            default:
                break;
        }
    }
}

void TdfEncoder::encodeRawString(const std::string& s) {
    encodeVarUInt(s.size() + 1);  // +1 for null terminator
    writeBytes(s.data(), s.size());
    writeByte(0x00);
}

void TdfEncoder::encodeMap(const std::string& tag, TdfType keyType, TdfType valueType, const TdfMapWrapper& map) {
    encodeTag(tag);
    writeByte(static_cast<uint8_t>(TdfType::Map));
    writeByte(static_cast<uint8_t>(keyType));
    writeByte(static_cast<uint8_t>(valueType));
    encodeVarUInt(map.data.size());

    for (const auto& [k, v] : map.data) {
        if (keyType == TdfType::String)
            encodeRawString(k);

        if (v) {
            if (valueType == TdfType::String && v->type == TdfType::String)
                encodeRawString(std::get<TdfString>(v->value));
            else if (valueType == TdfType::Integer && v->type == TdfType::Integer)
                encodeVarInt(std::get<TdfInteger>(v->value));
            else if (valueType == TdfType::Struct && v->type == TdfType::Struct) {
                for (const auto& [childTag, childValue] : std::get<TdfStruct>(v->value)) {
                    if (!childValue) continue;
                    encodeChild(*childValue);
                }
                writeByte(0x00);
            }
        }
    }
}

void TdfEncoder::encodeIntList(const std::string& tag, const TdfIntList& value) {
    encodeTag(tag);
    writeByte(static_cast<uint8_t>(TdfType::List));     // HEAT_TYPE_LIST = 0x04
    writeByte(static_cast<uint8_t>(TdfType::Integer));  // element type: HEAT_TYPE_INTEGER = 0x00
    encodeVarUInt(value.size());
    for (int64_t v : value) {
        encodeVarInt(v);
    }
}

void TdfEncoder::encodePair(const std::string& tag, const TdfPair& value) {
    encodeTag(tag);
    writeByte(static_cast<uint8_t>(TdfType::Pair));
    encodeVarUInt(static_cast<uint64_t>(value.first));
    encodeVarUInt(static_cast<uint64_t>(value.second));
}

void TdfEncoder::encodeTriple(const std::string& tag, const TdfTriple& value) {
    encodeTag(tag);
    writeByte(static_cast<uint8_t>(TdfType::Triple));
    encodeVarUInt(value.ip);
    encodeVarUInt(value.port);
    encodeVarUInt(value.protocol);
}

void TdfEncoder::encodeFloat(const std::string& tag, float value) {
    encodeTag(tag);
    writeByte(static_cast<uint8_t>(TdfType::Float));
    writeBytes(&value, sizeof(float));
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
            case TdfType::IntList:
                value->value = decodeIntList();
                break;
            case TdfType::Pair:
                value->value = decodePair();
                break;
            case TdfType::Triple:
                value->value = decodeTriple();
                break;
            case TdfType::Float:
                value->value = decodeFloat();
                break;
            case TdfType::Union:
                value->value = decodeUnion();
                break;
            default:
                LOG_WARN("Unknown TDF type: {}", static_cast<int>(type));
                break;
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
    
    // Unpack: 3 bytes -> 4 x 6-bit values
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
    return static_cast<int64_t>(decodeVarUInt());
}

uint64_t TdfDecoder::decodeVarUInt() {
    uint64_t result = 0;
    int shift = 0;
    
    while (hasMore()) {
        uint8_t byte = readByte();
        result |= static_cast<uint64_t>(byte & 0x7F) << shift;
        
        if ((byte & 0x80) == 0) {
            break;
        }
        
        shift += 7;
        if (shift > 63) {
            throw std::runtime_error("TDF: VarInt too large");
        }
    }
    
    return result;
}

TdfInteger TdfDecoder::decodeInteger() {
    return decodeVarInt();
}

TdfString TdfDecoder::decodeString() {
    uint64_t length = decodeVarUInt();
    if (m_pos + length > m_size) {
        throw std::runtime_error("TDF: Not enough data for string");
    }
    
    std::string result(reinterpret_cast<const char*>(m_data + m_pos), length - 1);  // -1 for null
    m_pos += length;
    return result;
}

TdfBinary TdfDecoder::decodeBinary() {
    uint64_t length = decodeVarUInt();
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
    uint64_t count = decodeVarUInt();
    
    TdfList result;
    result.reserve(count);
    
    for (uint64_t i = 0; i < count; i++) {
        auto elem = std::make_shared<TdfValue>();
        elem->type = elementType;
        
        switch (elementType) {
            case TdfType::Integer:
                elem->value = decodeInteger();
                break;
            case TdfType::String:
                elem->value = decodeString();
                break;
            case TdfType::Struct:
                elem->value = decodeStruct();
                break;
            default:
                break;
        }
        
        result.push_back(elem);
    }
    
    return result;
}

TdfMapWrapper TdfDecoder::decodeMap() {
    TdfType keyType   = decodeType();
    TdfType valueType = decodeType();
    uint64_t count    = decodeVarUInt();

    TdfMapWrapper result;
    result.keyType   = keyType;
    result.valueType = valueType;

    for (uint64_t i = 0; i < count; i++) {
        std::string key;
        if (keyType == TdfType::String)
            key = decodeString();
        else
            break; // non-string keys not supported

        auto val = std::make_shared<TdfValue>();
        val->tag  = key;
        val->type = valueType;

        if (valueType == TdfType::String)
            val->value = decodeString();
        else if (valueType == TdfType::Integer)
            val->value = decodeInteger();

        result.data[key] = val;
    }

    return result;
}

TdfIntList TdfDecoder::decodeIntList() {
    uint64_t count = decodeVarUInt();
    
    TdfIntList result;
    result.reserve(count);
    
    for (uint64_t i = 0; i < count; i++) {
        result.push_back(decodeVarInt());
    }
    
    return result;
}

TdfPair TdfDecoder::decodePair() {
    TdfPair result;
    result.first = static_cast<int64_t>(decodeVarUInt());
    result.second = static_cast<int64_t>(decodeVarUInt());
    return result;
}

TdfTriple TdfDecoder::decodeTriple() {
    TdfTriple result;
    result.ip = static_cast<uint32_t>(decodeVarUInt());
    result.port = static_cast<uint16_t>(decodeVarUInt());
    result.protocol = static_cast<uint16_t>(decodeVarUInt());
    return result;
}

float TdfDecoder::decodeFloat() {
    float result;
    readBytes(&result, sizeof(float));
    return result;
}

TdfStruct TdfDecoder::decodeUnion() {
    uint8_t arm = readByte();
    if (arm == 0x7F || arm == 0xFF) {
        return TdfStruct{};
    }
    std::string memberTag = decodeTag();
    TdfType memberType = decodeType();
    auto member = std::make_shared<TdfValue>();
    member->tag = memberTag;
    member->type = memberType;
    switch (memberType) {
        case TdfType::Integer: member->value = decodeInteger(); break;
        case TdfType::String:  member->value = decodeString();  break;
        case TdfType::Binary:  member->value = decodeBinary();  break;
        case TdfType::Struct:  member->value = decodeStruct();  break;
        case TdfType::List:    member->value = decodeList();    break;
        case TdfType::Map:     member->value = decodeMap();     break;
        case TdfType::Union:   member->value = decodeUnion();   break;
        case TdfType::IntList: member->value = decodeIntList(); break;
        case TdfType::Pair:    member->value = decodePair();    break;
        case TdfType::Triple:  member->value = decodeTriple();  break;
        case TdfType::Float:   member->value = decodeFloat();   break;
        default: break;
    }
    TdfStruct result;
    result[memberTag] = member;
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

// =============================================================================
// TdfBuilder Implementation
// =============================================================================

static TdfType parseTdfTypeStr(const std::string& s) {
    if (s == "integer" || s == "int") return TdfType::Integer;
    if (s == "string"  || s == "str") return TdfType::String;
    if (s == "binary"  || s == "blob") return TdfType::Binary;
    if (s == "struct")  return TdfType::Struct;
    if (s == "list")    return TdfType::List;
    if (s == "map")     return TdfType::Map;
    if (s == "float")   return TdfType::Float;
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

TdfBuilder& TdfBuilder::pair(const std::string& tag, int64_t first, int64_t second) {
    TdfPair p{first, second};
    auto tdf = std::make_shared<TdfValue>(tag, TdfType::Pair, p);
    current()[tag] = tdf;
    return *this;
}

TdfBuilder& TdfBuilder::triple(const std::string& tag, uint32_t ip, uint16_t port, uint16_t protocol) {
    TdfTriple t{ip, port, protocol};
    auto tdf = std::make_shared<TdfValue>(tag, TdfType::Triple, t);
    current()[tag] = tdf;
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
    auto& mf = std::get<MapFrame>(m_frames.back());
    const std::string& key = mf.pendingKey;

    auto tdf = std::make_shared<TdfValue>(key, TdfType::Struct, TdfStruct{});
    mf.wrapper->data[key] = tdf;
    m_frames.push_back(StructFrame{&std::get<TdfStruct>(tdf->value), key});
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


static std::string xmlIndent(int n) { return std::string(n * 2, ' '); }

static std::string renderValue(const TdfValue& v, int indent);

static std::string renderStruct(const TdfStruct& s, int indent) {
    std::string out;
    for (const auto& [tag, child] : s) {
        if (!child) continue;
        out += renderValue(*child, indent);
    }
    return out;
}

static std::string renderValue(const TdfValue& v, int indent) {
    std::string ind = xmlIndent(indent);
    std::string out;

    switch (v.type) {
        case TdfType::Integer:
            out += ind + "<" + v.tag + " t=\"int\">" +
                   std::to_string(std::get<TdfInteger>(v.value)) +
                   "</" + v.tag + ">\n";
            break;
        case TdfType::String:
            out += ind + "<" + v.tag + " t=\"str\">" +
                   std::get<TdfString>(v.value) +
                   "</" + v.tag + ">\n";
            break;
        case TdfType::Binary: {
            const auto& b = std::get<TdfBinary>(v.value);
            std::string hex;
            char tmp[3];
            for (uint8_t byte : b) { snprintf(tmp, sizeof(tmp), "%02X", byte); hex += tmp; }
            out += ind + "<" + v.tag + " t=\"blob\">" + hex + "</" + v.tag + ">\n";
            break;
        }
        case TdfType::Struct:
            out += ind + "<" + v.tag + " t=\"struct\">\n";
            out += renderStruct(std::get<TdfStruct>(v.value), indent + 1);
            out += ind + "</" + v.tag + ">\n";
            break;
        case TdfType::List: {
            const auto& list = std::get<TdfList>(v.value);
            out += ind + "<" + v.tag + " t=\"list\" n=\"" + std::to_string(list.size()) + "\">\n";
            for (const auto& elem : list) {
                if (elem) out += renderValue(*elem, indent + 1);
            }
            out += ind + "</" + v.tag + ">\n";
            break;
        }
        case TdfType::Map: {
            const auto& m = std::get<TdfMapWrapper>(v.value);
            out += ind + "<" + v.tag + " t=\"map\" n=\"" + std::to_string(m.data.size()) + "\">\n";
            for (const auto& [k, val] : m.data) {
                out += ind + "  <entry key=\"" + k + "\">";
                if (val && val->type == TdfType::String)
                    out += std::get<TdfString>(val->value);
                else if (val && val->type == TdfType::Integer)
                    out += std::to_string(std::get<TdfInteger>(val->value));
                out += "</entry>\n";
            }
            out += ind + "</" + v.tag + ">\n";
            break;
        }
        case TdfType::IntList: {
            const auto& il = std::get<TdfIntList>(v.value);
            out += ind + "<" + v.tag + " t=\"intlist\">[";
            for (size_t i = 0; i < il.size(); ++i) {
                if (i) out += ",";
                out += std::to_string(il[i]);
            }
            out += "]</" + v.tag + ">\n";
            break;
        }
        case TdfType::Pair: {
            const auto& p = std::get<TdfPair>(v.value);
            out += ind + "<" + v.tag + " t=\"pair\">" +
                   std::to_string(p.first) + "," + std::to_string(p.second) +
                   "</" + v.tag + ">\n";
            break;
        }
        case TdfType::Triple: {
            const auto& t = std::get<TdfTriple>(v.value);
            out += ind + "<" + v.tag + " t=\"triple\">" +
                   std::to_string(t.ip) + "," + std::to_string(t.port) + "," +
                   std::to_string(t.protocol) +
                   "</" + v.tag + ">\n";
            break;
        }
        case TdfType::Float:
            out += ind + "<" + v.tag + " t=\"float\">" +
                   std::to_string(std::get<float>(v.value)) +
                   "</" + v.tag + ">\n";
            break;
        case TdfType::Union:
            out += ind + "<" + v.tag + " t=\"union\">\n";
            out += renderStruct(std::get<TdfStruct>(v.value), indent + 1);
            out += ind + "</" + v.tag + ">\n";
            break;
        default:
            out += ind + "<" + v.tag + " t=\"?\"/>\n";
            break;
    }
    return out;
}

std::string tdfToXml(const TdfStruct& tdf, int indent) {
    return renderStruct(tdf, indent);
}

} // namespace ds2::blaze
