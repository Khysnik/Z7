#pragma once

#include <cstdint>
#include <string>
#include <vector>
#include <map>
#include <variant>
#include <memory>

namespace gw2::blaze {

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
    Inventory         = 0x0803, // Inventory / consumables
    UserSessions      = 0x7802, // User session management
};

enum class MessageType : uint8_t {
    Message      = 0,
    Reply        = 1,
    Notification = 2,
    ErrorReply   = 3,
    Ping         = 4,
    PingReply    = 5,
};

enum class BlazeError : uint32_t {
    Success           = 0x00000000,
    
    ERR_SYSTEM        = 0x00010001,
    ERR_TIMEOUT       = 0x00010002,
    ERR_INVALID_PARAM = 0x00010003,
    ERR_NOT_FOUND     = 0x00010004,
    
    ERR_AUTH_REQUIRED = 0x00020001,
    ERR_INVALID_TOKEN = 0x00020002,
    ERR_SESSION_EXPIRED = 0x00020003,
    ERR_INVALID_CREDENTIALS = 0x00020004,
    
    ERR_GAME_FULL     = 0x00040001,
    ERR_GAME_NOT_FOUND = 0x00040002,

    ERR_INSUFFICIENT_FUNDS = 0x08020001,  // PvzGw/Packs: not enough Coinz
};

enum class RedirectorCommand : uint16_t {
    getServerInstance      = 0x01,  // Get Blaze server address
    getServerInstanceAddr  = 0x02,  // Get server address info
};

enum class AuthCommand : uint16_t {
    login                  = 0x0A,
    trustedLogin           = 0x0B,
    updateAccount          = 0x14,
    listUserEntitlements2  = 0x1D,
    getAuthToken           = 0x24,
    expressLogin           = 0x3C,
    logout                 = 0x46,
    getPersona             = 0x5A,
    listPersonas           = 0x64,
    expressCreateAccount   = 0x65,
    getOriginPersona       = 0x104,
};

enum class UtilCommand : uint16_t {
    fetchClientConfig      = 0x01,
    ping                   = 0x02,
    setClientData          = 0x03,
    localizeStrings        = 0x04,
    getTelemetryServer     = 0x05,
    getTickerServer        = 0x06,
    preAuth                = 0x07,
    postAuth               = 0x08,
    userSettingsLoad       = 0x0A,
    userSettingsSave       = 0x0B,
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

enum class UserSessionsCommand : uint16_t {
    updateHardwareFlags    = 0x08,
    updateNetworkInfo      = 0x14,
};

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
    removePlayer           = 0x0B,
    startMatchmaking       = 0x0D,
    cancelMatchmaking      = 0x0E,
    finalizeGameCreation   = 0x0F,
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

enum class TdfType : uint8_t {
    Integer     = 0x00,
    String      = 0x01,
    Binary      = 0x02,
    Struct      = 0x03,
    List        = 0x04,
    Map         = 0x05,
    Union       = 0x06,
    Variable    = 0x07,
    ObjectType  = 0x08,
    ObjectId    = 0x09,
    Float       = 0x0A,
    TimeValue   = 0x0B,
    GenericType = 0x0C,
    IntList     = 0xFE,
};

struct TdfValue;

using TdfInteger = int64_t;
using TdfString  = std::string;
using TdfBinary  = std::vector<uint8_t>;
using TdfList    = std::vector<std::shared_ptr<TdfValue>>;
using TdfStruct  = std::map<std::string, std::shared_ptr<TdfValue>>;
using TdfIntList = std::vector<int64_t>;

struct TdfMapWrapper {
    TdfType keyType   = TdfType::String;
    TdfType valueType = TdfType::String;
    std::map<std::string, std::shared_ptr<TdfValue>> data;
};

struct TdfObjectType {
    uint64_t component;
    uint64_t type;
};

struct TdfObjectId {
    uint64_t component;
    uint64_t type;
    uint64_t id;
};

struct TdfVariable {
    bool      valid = false;
    uint64_t  tdfId = 0;
    TdfStruct data;
};

struct TdfUnion {
    uint8_t   arm    = 0x7F;
    std::shared_ptr<TdfValue> member;
};

using TdfVariant = std::variant<
    TdfInteger,
    TdfString,
    TdfBinary,
    TdfStruct,
    TdfList,
    TdfMapWrapper,
    TdfVariable,
    TdfIntList,
    TdfObjectType,
    TdfObjectId,
    float,
    TdfUnion
>;

struct TdfValue {
    std::string tag;
    TdfType type;
    TdfVariant value;
    
    TdfValue() = default;
    TdfValue(const std::string& t, TdfType ty, TdfVariant v)
        : tag(t), type(ty), value(std::move(v)) {}
};

#pragma pack(push, 1)
struct PacketHeader {
    uint8_t raw[16];
};
#pragma pack(pop)

static_assert(sizeof(PacketHeader) == 16, "PacketHeader must be 16 bytes");

struct ServerConfig {
    std::string blaze_host = "0.0.0.0";
    uint16_t blaze_port = 10041;

    std::string ssl_cert_path = "certs/server.crt";
    std::string ssl_key_path = "certs/server.key";
    
    std::string server_name = "gw2-Emulator";
    std::string server_version = "0.1.0";
};

} // namespace gw2::blaze
