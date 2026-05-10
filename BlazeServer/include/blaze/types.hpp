#pragma once

#include <cstdint>
#include <string>
#include <vector>
#include <map>
#include <variant>
#include <memory>

namespace ds2::blaze {

// =============================================================================
// Blaze Component IDs (from RE analysis)
// =============================================================================
enum class ComponentId : uint16_t {
    Authentication    = 0x01,   // Login, account management
    GameManager       = 0x04,   // Game sessions, matchmaking
    Redirector        = 0x05,   // Server redirection
    Stats             = 0x07,   // Statistics tracking
    Util              = 0x09,   // Utility functions (ping, config)
    Association       = 0x19,   // Friend lists, associations
    Playgroups        = 0x1E,   // Party/group management
    RSP               = 0x801,  // Room/session protocol
    Packs             = 0x0802, // Pack acquisition/opening
    UserSessions      = 0x7802, // User session management
};

// =============================================================================
// Blaze Message Types (Fire2 byte 13 bits [7:5])
// =============================================================================
enum class MessageType : uint8_t {
    Message      = 0,
    Reply        = 1,
    Notification = 2,
    ErrorReply   = 3,
    Ping         = 4,
    PingReply    = 5,
};

// =============================================================================
// Blaze Error Codes
// =============================================================================
enum class BlazeError : uint32_t {
    Success           = 0x00000000,
    
    // General errors
    ERR_SYSTEM        = 0x00010001,
    ERR_TIMEOUT       = 0x00010002,
    ERR_INVALID_PARAM = 0x00010003,
    ERR_NOT_FOUND     = 0x00010004,
    
    // Authentication errors
    ERR_AUTH_REQUIRED = 0x00020001,
    ERR_INVALID_TOKEN = 0x00020002,
    ERR_SESSION_EXPIRED = 0x00020003,
    ERR_INVALID_CREDENTIALS = 0x00020004,
    
    // Game errors
    ERR_GAME_FULL     = 0x00040001,
    ERR_GAME_NOT_FOUND = 0x00040002,
};

// =============================================================================
// Redirector Commands
// =============================================================================
enum class RedirectorCommand : uint16_t {
    getServerInstance      = 0x01,  // Get Blaze server address
    getServerInstanceAddr  = 0x02,  // Get server address info
};

// =============================================================================
// Authentication Commands (GW2 / BlazeSDK 15.1.1.1.0)
// IDs confirmed from sub_1419ca8f0 binary analysis. DS2 IDs shown in comments.
// =============================================================================
enum class AuthCommand : uint16_t {
    login                  = 0x0A,   // DS2: 0x01
    trustedLogin           = 0x0B,
    updateAccount          = 0x14,
    listUserEntitlements2  = 0x1D,
    getAuthToken           = 0x24,   // DS2: 0x06
    expressLogin           = 0x3C,   // DS2: 0x14 — primary login for GW2
    logout                 = 0x46,   // DS2: 0x04
    getPersona             = 0x5A,   // DS2: 0x08
    listPersonas           = 0x64,
    expressCreateAccount   = 0x65,
    getOriginPersona       = 0x104,  // DS2: 0x1C
};

// =============================================================================
// Util Commands (GW2 / BlazeSDK 15.1.1.1.0)
// IDs confirmed from sub_1419b2550 binary analysis. DS2 IDs shown in comments.
// =============================================================================
enum class UtilCommand : uint16_t {
    fetchClientConfig      = 0x01,   // DS2: 0x0B
    ping                   = 0x02,   // DS2: 0x01
    setClientData          = 0x03,   // DS2: 0x02
    localizeStrings        = 0x04,
    getTelemetryServer     = 0x05,
    getTickerServer        = 0x06,
    preAuth                = 0x07,   // same as DS2
    postAuth               = 0x08,   // same as DS2
    userSettingsLoad       = 0x0A,   // DS2: 0x09
    userSettingsSave       = 0x0B,   // DS2: 0x0A
    userSettingsLoadAll    = 0x0C,
    deleteUserSettings     = 0x0E,
    filterForProfanity     = 0x14,
    fetchQosConfig         = 0x15,
    setClientMetrics       = 0x16,
    setConnectionState     = 0x17,
    getUserOptions         = 0x19,
    setUserOptions         = 0x1A,
    suspendUserPing        = 0x1B,
    setClientState         = 0x1C,
};

// =============================================================================
// UserSessions Commands (GW2 / BlazeSDK 15.1.1.1.0)
// =============================================================================
enum class UserSessionsCommand : uint16_t {
    updateHardwareFlags    = 0x08,
    updateNetworkInfo      = 0x14,
};

// =============================================================================
// PvzGwComponent Commands (GW2)
// =============================================================================
enum class PvzGwCommand : uint16_t {
    checkUserMessages        = 0x02,
    setSurveyCompleted       = 0x03,
    getStoreItemList         = 0x04,
    getPersistedLicenses     = 0x05,
    grantPersistedLicensesForEntitlements = 0x06,
    setXPMultiplier         = 0x07,
    clearPersistedLicenses   = 0x08,
    getDailyQuests          = 0x09,
    grantPersistedLicensesForContent = 0x0B,
    setOnlineAccessEntitlements = 0x0C,
    getUserMessages         = 0x0E,
    grantPersistedLicensesForUserEvents = 0x0F,
    updateUserMessageStatus  = 0x10,
    getClientSettings       = 0x11,
    getCommunityAchievements = 0x12,
    claimCommunityEventReward = 0x13,
    getBlackMarketData      = 0x14,
    purchaseBlackMarketItem  = 0x15,
    setBlackMarketViewed    = 0x16,
    getCommunityPortalData  = 0x17,
    openCommunityPortalChest = 0x18,
    forceClientNotification = 0x19,
    getPlaylists            = 0x1E,
    getPlaylistRotation     = 0x1F,
    getLoyaltyChallengeData = 0x3C,
};

// =============================================================================
// Packs Commands (GW2 / component 0x0802)
// IDs confirmed from sub_140e69330 binary analysis.
// =============================================================================
enum class PacksCommand : uint16_t {
    getPacks              = 0x01,
    grantPacks            = 0x02,
    redeemPack            = 0x03,
    openPack              = 0x04,
    acquireCalendarPacks  = 0x05,
    getTemplate           = 0x06,
    wipe                  = 0x07,
    purchaseAndOpenPack   = 0x1F4,
    debugGrant            = 0x1F5,
    grantAndOpenPacks     = 0x1F9,
};

// =============================================================================
// GameManager Commands (GW2 / BlazeSDK 15.1.1.1.0)
// IDs confirmed from sub_1419a8510 binary analysis.
// =============================================================================
enum class GameManagerCommand : uint16_t {
    createGame             = 0x01,
    destroyGame            = 0x02,
    advanceGameState       = 0x03,
    setGameSettings        = 0x04,
    setPlayerCapacity      = 0x05,
    setPresenceMode        = 0x06,
    setGameAttributes      = 0x07,
    setPlayerAttributes    = 0x08,
    joinGame               = 0x09,
    removePlayer           = 0x0B,   // DS2: 0x0A (no 0x0A in GW2)
    startMatchmaking       = 0x0D,   // DS2: 0x0B
    cancelMatchmaking      = 0x0E,   // DS2: 0x0C
    finalizeGameCreation   = 0x0F,   // DS2: 0x0D
    startMatchmakingScenario = 0x10,
    cancelMatchmakingScenario = 0x11,
    setPlayerCustomData    = 0x12,
    replayGame             = 0x13,
    returnDedicatedServerToPool = 0x14,
    leaveGameByGroup       = 0x16,
    migrateGame            = 0x17,
    updateGameHostMigrationStatus = 0x18,
    resetDedicatedServer   = 0x19,
    updateGameSession      = 0x1A,
    banPlayer              = 0x1B,
    updateMeshConnection   = 0x1D,
    joinGameByUserList     = 0x1E,
    removePlayerFromBannedList = 0x1F,
    clearBannedList        = 0x20,
    getBannedList          = 0x21,
    addQueuedPlayerToGame  = 0x26,
    updateGameName         = 0x27,
    ejectHost              = 0x28,
    createOrJoinGame       = 0x2D,
    requestPlatformHost    = 0x2E,
    getGameListSnapshot    = 0x64,
    getGameListSubscription = 0x65,
    destroyGameList        = 0x66,
    getFullGameData        = 0x67,
    getMatchmakingConfig   = 0x68,
    getGameDataFromId      = 0x69,
    getGameListSnapshotSync = 0x98,
    reportTelemetry        = 0xAB,
    updatePrimaryExternalSessionForUser = 0xB1,
};

// =============================================================================
// Connection States (from RE analysis - 12 states)
// =============================================================================
enum class ConnectionState : uint8_t {
    DEACTIVATED         = 0,
    CONNECTING          = 1,
    CONNECTED           = 2,
    REDIRECTING         = 3,
    AUTHENTICATING      = 4,
    AUTHENTICATED       = 5,
    POST_AUTH           = 6,
    READY               = 7,
    DISCONNECTING       = 8,
    DISCONNECTED        = 9,
    RECONNECTING        = 10,
    ERROR_STATE         = 11,
};

// =============================================================================
// TDF (Tag Data Format) Types
// =============================================================================
enum class TdfType : uint8_t {
    Integer       = 0x00,  // Variable-length integer
    String        = 0x01,  // Null-terminated string
    Binary        = 0x02,  // Binary blob
    Struct        = 0x03,  // Nested structure
    List          = 0x04,  // List of elements
    Map           = 0x05,  // Key-value map
    Union         = 0x06,  // Tagged union
    IntList       = 0x07,  // List of integers
    Pair          = 0x08,  // Pair of integers (ObjectId/ObjectType)
    Triple        = 0x09,  // Triple (IP, Port, Protocol)
    Float         = 0x0A,  // Float value
};

// =============================================================================
// TDF Value Types
// =============================================================================
struct TdfValue;

using TdfInteger = int64_t;
using TdfString = std::string;
using TdfBinary = std::vector<uint8_t>;
using TdfList = std::vector<std::shared_ptr<TdfValue>>;
using TdfStruct = std::map<std::string, std::shared_ptr<TdfValue>>;
using TdfIntList = std::vector<int64_t>;

// Wrapper to distinguish Map from Struct in variant
struct TdfMapWrapper {
    TdfType keyType   = TdfType::String;
    TdfType valueType = TdfType::String;
    std::map<std::string, std::shared_ptr<TdfValue>> data;
};

struct TdfPair {
    int64_t first;
    int64_t second;
};

struct TdfTriple {
    uint32_t ip;
    uint16_t port;
    uint16_t protocol;
};

// Variant to hold any TDF value
using TdfVariant = std::variant<
    TdfInteger,
    TdfString,
    TdfBinary,
    TdfStruct,
    TdfList,
    TdfMapWrapper,
    TdfIntList,
    TdfPair,
    TdfTriple,
    float
>;

struct TdfValue {
    std::string tag;      // 4-character tag (compressed to 3 bytes)
    TdfType type;
    TdfVariant value;
    
    TdfValue() = default;
    TdfValue(const std::string& t, TdfType ty, TdfVariant v)
        : tag(t), type(ty), value(std::move(v)) {}
};

// =============================================================================
// Fire2 Packet Header (16 bytes) - BlazeSDK 15.x / GW2 format
// Byte layout (from fire2frame.h):
//   [0-3]  payload size (uint32 big-endian)
//   [4-5]  metadata size (uint16 big-endian, usually 0)
//   [6-7]  component id (uint16 big-endian)
//   [8-9]  command id (uint16 big-endian)
//   [10-12] message number (24-bit big-endian)
//   [13]   (msgType << 5) | userIndex  — 3-bit type in bits [7:5], 5-bit userIndex in [4:0]
//   [14]   options
//   [15]   reserved
// =============================================================================
#pragma pack(push, 1)
struct PacketHeader {
    uint8_t raw[16];
};
#pragma pack(pop)

static_assert(sizeof(PacketHeader) == 16, "PacketHeader must be 16 bytes");

// =============================================================================
// Server Configuration
// =============================================================================
struct ServerConfig {
    // Redirector settings
    std::string redirector_host = "0.0.0.0";
    uint16_t redirector_port = 42231;
    
    // Blaze server settings
    std::string blaze_host = "0.0.0.0";
    uint16_t blaze_port = 10041;
    
    // QoS server settings  
    std::string qos_host = "0.0.0.0";
    uint16_t qos_port = 17502;
    
    // SSL certificate paths
    std::string ssl_cert_path = "certs/server.crt";
    std::string ssl_key_path = "certs/server.key";
    
    // Server identification
    std::string server_name = "DS2-Emulator";
    std::string server_version = "0.1.0";
};

} // namespace ds2::blaze
