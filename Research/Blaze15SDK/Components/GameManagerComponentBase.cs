using Blaze.Core;
using Blaze15SDK.Blaze.GameManager;
using EATDF;
using EATDF.Types;

namespace Blaze15SDK.Components;

public static class GameManagerComponentBase
{
    public const ushort Id = 4;
    public const string Name = "GameManagerComponent";

    public enum Error : ushort
    {
        GAMEMANAGER_ERR_INVALID_GAME_SETTINGS = 1,
        GAMEMANAGER_ERR_INVALID_GAME_ID = 2,
        GAMEMANAGER_ERR_JOIN_METHOD_NOT_SUPPORTED = 3,
        GAMEMANAGER_ERR_PARTICIPANT_SLOTS_FULL = 4,
        GAMEMANAGER_ERR_INVALID_GAME_STATE_TRANSITION = 5,
        GAMEMANAGER_ERR_INVALID_GAME_STATE_ACTION = 6,
        GAMEMANAGER_ERR_FAILED_IN_GAME_DESTROY = 7,
        GAMEMANAGER_ERR_QUEUE_FULL = 8,
        GAMEMANAGER_ERR_INVALID_GAME_ENTRY_CRITERIA = 9,
        GAMEMANAGER_ERR_GAME_PROTOCOL_VERSION_MISMATCH = 10,
        GAMEMANAGER_ERR_GAME_IN_PROGRESS = 11,
        GAMEMANAGER_ERR_RESERVED_GAME_ID_INVALID = 12,
        GAMEMANAGER_ERR_INVALID_JOIN_METHOD = 13,
        GAMEMANAGER_ERR_SLOT_OCCUPIED = 14,
        GAMEMANAGER_ERR_NOT_VIRTUAL_GAME = 15,
        GAMEMANAGER_ERR_NOT_TOPOLOGY_HOST = 16,
        GAMEMANAGER_ERR_TOPOLOGY_NOT_SUPPORTED = 17,
        GAMEMANAGER_ERR_GAME_BUSY = 18,
        GAMEMANAGER_ERR_SESSION_TEMPLATE_NOT_SUPPORTED = 19,
        GAMEMANAGER_ERR_GAME_MODE_ATTRIBUTE_MISSING = 20,
        GAMEMANAGER_ERR_FAILED_DUE_TO_FRIENDS_ONLY_RESTRICTION = 21,
        GAMEMANAGER_ERR_FAILED_DUE_TO_PRESENCE_MODE_RESTRICTION = 22,
        GAMEMANAGER_ERR_SPECTATOR_SLOTS_FULL = 23,
        GAMEMANAGER_ERR_UNRESPONSIVE_GAME_STATE = 24,
        GAMEMANAGER_ERR_GAME_DESTROYED_BY_CONNECTION_UPDATE = 25,
        GAMEMANAGER_ERR_INVALID_PING_SITE_ALIAS = 26,
        GAMEMANAGER_ERR_CROSSPLAY_DISABLED = 27,
        GAMEMANAGER_ERR_CROSSPLAY_DISABLED_USER = 28,
        GAMEMANAGER_ERR_PERMISSION_DENIED = 30,
        GAMEMANAGER_ERR_ALREADY_ADMIN = 31,
        GAMEMANAGER_ERR_NOT_IN_ADMIN_LIST = 32,
        GAMEMANAGER_ERR_DEDICATED_SERVER_HOST = 33,
        GAMEMANAGER_ERR_INVALID_QUEUE_METHOD = 50,
        GAMEMANAGER_ERR_PLAYER_NOT_IN_QUEUE = 51,
        GAMEMANAGER_ERR_DEQUEUE_WHILE_MIGRATING = 52,
        GAMEMANAGER_ERR_DEQUEUE_WHILE_IN_PROGRESS = 53,
        GAMEMANAGER_ERR_PLAYER_NOT_FOUND = 101,
        GAMEMANAGER_ERR_ALREADY_GAME_MEMBER = 103,
        GAMEMANAGER_ERR_REMOVE_PLAYER_FAILED = 104,
        GAMEMANAGER_ERR_INVALID_PLAYER_PASSEDIN = 107,
        GAMEMANAGER_ERR_JOIN_PLAYER_FAILED = 108,
        GAMEMANAGER_ERR_MISSING_PRIMARY_LOCAL_PLAYER = 109,
        GAMEMANAGER_ERR_PLAYER_BANNED = 110,
        GAMEMANAGER_ERR_GAME_ENTRY_CRITERIA_FAILED = 111,
        GAMEMANAGER_ERR_ALREADY_IN_QUEUE = 112,
        GAMEMANAGER_ERR_ENFORCING_SINGLE_GROUP_JOINS = 113,
        GAMEMANAGER_ERR_BANNED_PLAYER_NOT_FOUND = 114,
        GAMEMANAGER_ERR_BANNED_LIST_MAX = 115,
        GAMEMANAGER_ERR_FAILED_REPUTATION_CHECK = 117,
        GAMEMANAGER_ERR_RESERVATION_ALREADY_EXISTS = 120,
        GAMEMANAGER_ERR_NO_RESERVATION_FOUND = 121,
        GAMEMANAGER_ERR_INVALID_GAME_ENTRY_TYPE = 122,
        GAMEMANAGER_ERR_INVALID_GROUP_ID = 151,
        GAMEMANAGER_ERR_PLAYER_NOT_IN_GROUP = 152,
        GAMEMANAGER_ERR_INVALID_MATCHMAKING_CRITERIA = 200,
        GAMEMANAGER_ERR_UNKNOWN_MATCHMAKING_SESSION_ID = 201,
        GAMEMANAGER_ERR_NOT_MATCHMAKING_SESSION_OWNER = 202,
        GAMEMANAGER_ERR_MATCHMAKING_NO_JOINABLE_GAMES = 203,
        GAMEMANAGER_ERR_MATCHMAKING_USERSESSION_NOT_FOUND = 205,
        GAMEMANAGER_ERR_MATCHMAKING_EXCEEDED_MAX_REQUESTS = 206,
        GAMEMANAGER_ERR_MATCHMAKING_USER_GROUP_EXCEEDED_MAX_REQUESTS = 207,
        GAMEMANAGER_ERR_PLAYER_CAPACITY_TOO_SMALL = 230,
        GAMEMANAGER_ERR_PLAYER_CAPACITY_TOO_LARGE = 231,
        GAMEMANAGER_ERR_PLAYER_CAPACITY_IS_ZERO = 232,
        GAMEMANAGER_ERR_MAX_PLAYER_CAPACITY_TOO_LARGE = 233,
        GAMEMANAGER_ERR_INVALID_TEAM_CAPACITIES_VECTOR_SIZE = 250,
        GAMEMANAGER_ERR_DUPLICATE_TEAM_CAPACITY = 251,
        GAMEMANAGER_ERR_INVALID_TEAM_ID_IN_TEAM_CAPACITIES_VECTOR = 252,
        GAMEMANAGER_ERR_TEAM_NOT_ALLOWED = 253,
        GAMEMANAGER_ERR_TOTAL_TEAM_CAPACITY_INVALID = 254,
        GAMEMANAGER_ERR_TEAM_FULL = 255,
        GAMEMANAGER_ERR_PLAYER_CAPACITY_NOT_EVENLY_DIVISIBLE_BY_TEAMS = 257,
        GAMEMANAGER_ERR_EMPTY_ROLE_CAPACITIES = 270,
        GAMEMANAGER_ERR_ROLE_CAPACITY_TOO_SMALL = 271,
        GAMEMANAGER_ERR_ROLE_CAPACITY_TOO_LARGE = 272,
        GAMEMANAGER_ERR_ROLE_NOT_ALLOWED = 273,
        GAMEMANAGER_ERR_ROLE_FULL = 274,
        GAMEMANAGER_ERR_ROLE_CRITERIA_INVALID = 275,
        GAMEMANAGER_ERR_ROLE_CRITERIA_FAILED = 276,
        GAMEMANAGER_ERR_MULTI_ROLE_CRITERIA_INVALID = 277,
        GAMEMANAGER_ERR_MULTI_ROLE_CRITERIA_FAILED = 278,
        GAMEMANAGER_ERR_NO_DEDICATED_SERVER_FOUND = 301,
        GAMEMANAGER_ERR_DEDICATED_SERVER_ONLY_ACTION = 302,
        GAMEMANAGER_ERR_DEDICATED_SERVER_HOST_CANNOT_JOIN = 303,
        GAMEMANGER_ERR_MACHINE_ID_LIST_EMPTY = 304,
        GAMEMANAGER_ERR_NO_HOSTS_AVAILABLE_FOR_INJECTION = 308,
        GAMEBROWSER_ERR_INVALID_CRITERIA = 401,
        GAMEBROWSER_ERR_INVALID_CAPACITY = 402,
        GAMEBROWSER_ERR_INVALID_LIST_ID = 403,
        GAMEBROWSER_ERR_NOT_LIST_OWNER = 404,
        GAMEBROWSER_ERR_INVALID_LIST_CONFIG_NAME = 405,
        GAMEBROWSER_ERR_CANNOT_GET_USERSET = 406,
        GAMEBROWSER_ERR_EXCEED_MAX_SYNC_SIZE = 407,
        GAMEBROWSER_ERR_EXCEEDED_MAX_REQUESTS = 408,
        GAMEBROWSER_ERR_SEARCH_ERR_OVERLOADED = 409,
        GAMEBROWSER_ERR_CROSS_PLATFORM_OPTOUT = 410,
        GAMEBROWSER_ERR_DISALLOWED_PLATFORM = 411,
        GAMEMANAGER_ERR_GAME_CAPACITY_TOO_SMALL = 502,
        GAMEMANAGER_ERR_INVALID_ACTION_FOR_GROUP = 503,
        GAMEMANAGER_ERR_MIGRATION_NOT_SUPPORTED = 505,
        GAMEMANAGER_ERR_INVALID_NEWHOST = 506,
        GAMEMANAGER_ERR_INVALID_PERSISTED_GAME_ID_OR_SECRET = 508,
        GAMEMANAGER_ERR_PERSISTED_GAME_ID_IN_USE = 509,
        GAMEMANAGER_ERR_USER_NOT_FOUND = 510,
        GAMEMANAGER_ERR_USER_ALREADY_AUDITED = 511,
        GAMEMANAGER_ERR_USER_CURRENTLY_AUDITED = 512,
        GAMEMANAGER_ERR_INVALID_SCENARIO_NAME = 600,
        GAMEMANAGER_ERR_INVALID_SCENARIO_ID = 601,
        GAMEMANAGER_ERR_NOT_SCENARIO_OWNER = 602,
        GAMEMANAGER_ERR_MISSING_SCENARIO_ATTRIBUTE = 603,
        GAMEMANAGER_ERR_INVALID_SCENARIO_ATTRIBUTE = 604,
        GAMEMANAGER_ERR_INVALID_SCENARIO_VARIANT = 605,
        GAMEMANAGER_ERR_INVALID_TEMPLATE_NAME = 610,
        GAMEMANAGER_ERR_MISSING_TEMPLATE_ATTRIBUTE = 611,
        GAMEMANAGER_ERR_INVALID_TEMPLATE_ATTRIBUTE = 612,
        GAMEMANAGER_ERR_EXTERNAL_SERVICE_BUSY = 700,
        GAMEMANAGER_ERR_EXTERNAL_SESSION_IMAGE_INVALID = 701,
        GAMEMANAGER_ERR_EXTERNAL_SESSION_CUSTOM_DATA_TOO_LARGE = 702,
        GAMEMANAGER_ERR_EXTERNAL_SESSION_ERROR = 703,
        GAMEMANAGER_ERR_EXTERNALSESSION_INVALID_SANDBOX_ID = 704
    }

    public enum GameManagerCommand : ushort
    {
        createGame = 1,
        destroyGame = 2,
        advanceGameState = 3,
        setGameSettings = 4,
        setPlayerCapacity = 5,
        setPresenceMode = 6,
        setGameAttributes = 7,
        setPlayerAttributes = 8,
        joinGame = 9,
        createGameTemplate = 10,
        removePlayer = 11,
        startMatchmaking = 13,
        cancelMatchmaking = 14,
        finalizeGameCreation = 15,
        startMatchmakingScenario = 16,
        cancelMatchmakingScenario = 17,
        setPlayerCustomData = 18,
        replayGame = 19,
        returnDedicatedServerToPool = 20,
        joinGameByGroup = 21,
        leaveGameByGroup = 22,
        migrateGame = 23,
        updateGameHostMigrationStatus = 24,
        resetDedicatedServer = 25,
        updateGameSession = 26,
        banPlayer = 27,
        matchmakingDedicatedServerOverride = 28,
        updateMeshConnection = 29,
        joinGameByUserList = 30,
        removePlayerFromBannedList = 31,
        clearBannedList = 32,
        getBannedList = 33,
        matchmakingFillServersOverride = 35,
        addQueuedPlayerToGame = 38,
        updateGameName = 39,
        ejectHost = 40,
        setGameModRegister = 41,
        setGameEntryCriteria = 42,
        preferredJoinOptOut = 43,
        destroyGames = 44,
        createOrJoinGame = 45,
        requestPlatformHost = 46,
        demoteReservedPlayerToQueue = 47,
        createPseudoGame = 50,
        createPseudoGames = 51,
        isGameCaptureDone = 55,
        meshEndpointsConnected = 65,
        meshEndpointsDisconnected = 66,
        meshEndpointsConnectionLost = 67,
        requestConnectivityViaCCS = 68,
        freeConnectivityViaCCS = 69,
        requestLeaseExtensionViaCCS = 70,
        createTournamentGame = 90,
        cancelTournamentGame = 91,
        getTournamentGameStatus = 92,
        getGameListByScenario = 99,
        getGameListSnapshot = 100,
        getGameListSubscription = 101,
        destroyGameList = 102,
        getFullGameData = 103,
        getMatchmakingConfig = 104,
        getGameDataFromId = 105,
        addAdminPlayer = 106,
        removeAdminPlayer = 107,
        setPlayerTeam = 108,
        changeGameTeamId = 109,
        migrateAdminPlayer = 110,
        getUserSetGameListSubscription = 111,
        swapPlayers = 112,
        getGameDataByUser = 113,
        registerDynamicDedicatedServerCreator = 150,
        unregisterDynamicDedicatedServerCreator = 151,
        getGameListSnapshotSync = 152,
        findDedicatedServers = 153,
        reportTelemetry = 171,
        addUsersToConnectionMetricAudit = 172,
        removeUsersFromConnectionMetricAudit = 173,
        fetchAuditedUsers = 174,
        fetchAuditedUserData = 175,
        deleteUserAuditMetricData = 176,
        updatePrimaryExternalSessionForUser = 177,
        startPseudoMatchmakingScenario = 200,
        updateExternalSessionImage = 201,
        updateExternalSessionStatus = 202,
        setUserScenarioVariant = 203,
        getScenarioVariants = 204,
        clearUserScenarioVariant = 205
    }

    public enum GameManagerMasterCommand : ushort
    {
        createGameMaster = 1,
        destroyGameMaster = 2,
        advanceGameStateMaster = 4,
        setPlayerCapacityMaster = 5,
        setPresenceModeMaster = 6,
        setGameAttributesMaster = 7,
        setPlayerAttributesMaster = 8,
        joinGameMaster = 9,
        removePlayerMaster = 11,
        finalizeGameCreationMaster = 15,
        setGameSettingsMaster = 16,
        setPlayerCustomDataMaster = 18,
        replayGameMaster = 19,
        returnDedicatedServerToPoolMaster = 20,
        joinGameByGroupMaster = 21,
        leaveGameByGroupMaster = 22,
        migrateGameMaster = 23,
        updateGameHostMigrationStatusMaster = 24,
        resetDedicatedServerMaster = 25,
        updateGameSessionMaster = 26,
        addAdminPlayerMaster = 30,
        removeAdminPlayerMaster = 31,
        banPlayerMaster = 32,
        changeGameTeamIdMaster = 36,
        migrateAdminPlayerMaster = 37,
        addQueuedPlayerToGameMaster = 38,
        removePlayerFromBannedListMaster = 39,
        clearBannedListMaster = 40,
        getBannedListMaster = 41,
        updateGameNameMaster = 42,
        swapPlayersMaster = 43,
        ejectHostMaster = 44,
        setGameModRegisterMaster = 45,
        setGameEntryCriteriaMaster = 46,
        preferredJoinOptOutMaster = 47,
        hasGameMappedByPersistedIdMaster = 48,
        requestPlatformHostMaster = 49,
        reservePersistedIdMaster = 50,
        isGameCaptureDoneMaster = 52,
        meshEndpointsConnectedMaster = 55,
        meshEndpointsDisconnectedMaster = 56,
        meshEndpointsConnectionLostMaster = 57,
        getGameInfoSnapshot = 58,
        demoteReservedPlayerToQueueMaster = 59,
        matchmakingFoundGame = 60,
        matchmakingCreateGame = 61,
        matchmakingCreateGameWithPrivilegedId = 62,
        chooseHostForInjection = 75,
        resyncExternalSessionMembers = 76,
        setGameResponsiveMaster = 77,
        processTelemetryReport = 78,
        addUsersToConnectionMetricAuditMaster = 79,
        removeUsersFromConnectionMetricAuditMaster = 80,
        getPSUByGameNetworkTopology = 82,
        ccsConnectivityResultsAvailable = 95,
        ccsLeaseExtensionResultsAvailable = 96,
        getPktReceivedMetrics = 97,
        trackExtSessionMembershipMaster = 100,
        untrackExtSessionMembershipMaster = 101,
        getExternalSessionInfoMaster = 102,
        updateExternalSessionStatusMaster = 103,
        getExternalDataSourceApiListByGameId = 104
    }

    public enum GameManagerNotification : ushort
    {
        NotifyMatchmakingScenarioPseudoSuccess = 3,
        NotifyMatchmakingFailed = 10,
        NotifyMatchmakingSessionConnectionValidated = 11,
        NotifyMatchmakingAsyncStatus = 12,
        NotifyMatchmakingPseudoSuccess = 13,
        NotifyRemoteMatchmakingStarted = 14,
        NotifyRemoteMatchmakingEnded = 15,
        NotifyGameRemoved = 16,
        NotifyGameSetup = 20,
        NotifyPlayerJoining = 21,
        NotifyJoiningPlayerInitiateConnections = 22,
        NotifyPlayerJoiningQueue = 23,
        NotifyPlayerPromotedFromQueue = 24,
        NotifyPlayerClaimingReservation = 25,
        NotifyPlayerDemotedToQueue = 26,
        NotifyPlayerJoinCompleted = 30,
        NotifyPlayerRemoved = 40,
        NotifyRemoteJoinFailed = 41,
        NotifyHostMigrationFinished = 60,
        NotifyHostMigrationStart = 70,
        NotifyPlatformHostInitialized = 71,
        NotifyGameAttribChange = 80,
        NotifyPlayerAttribChange = 90,
        NotifyPlayerCustomDataChange = 95,
        NotifyGameStateChange = 100,
        NotifyGameSettingsChange = 110,
        NotifyGameCapacityChange = 111,
        NotifyGameReset = 112,
        NotifyGameReportingIdChange = 113,
        NotifyGamePresenceChanged = 114,
        NotifyGameSessionUpdated = 115,
        NotifyGamePlayerStateChange = 116,
        NotifyGamePlayerTeamRoleSlotChange = 117,
        NotifyGameTeamIdChange = 118,
        NotifyProcessQueue = 119,
        NotifyPresenceModeChanged = 120,
        NotifyQueueChanged = 121,
        NotifyGameModRegisterChanged = 123,
        NotifyGameEntryCriteriaChanged = 124,
        NotifyMatchmakingReservedExternalPlayers = 125,
        NotifyHostedConnectivityAvailable = 135,
        NotifyGameListUpdate = 201,
        NotifyAdminListChange = 202,
        NotifyGameNameChange = 230,
        UserScenarioVariantUpdate = 300,
        ClearUserScenarioVariant = 301
    }

    public enum GameManagerEvent : ushort
    {
        PlayerJoinedGameSessionEvent = 1000,
        PlayerLeftGameSessionEvent = 1001,
        CreatedGameSessionEvent = 1002,
        DestroyedGameSessionEvent = 1003,
        GameSessionStateChangedEvent = 1004,
        PlayerReservedGameSessionEvent = 1005,
        PlayerQueuedGameSessionEvent = 1006,
        PlayerDequeuedGameSessionEvent = 1007,
        UpdatedGameSessionDataEvent = 1008,
        SuccesfulMatchmakingSessionEvent = 1010,
        FailedMatchmakingSessionEvent = 1011,
        NotifyGamePresenceChanged = 1114,
        NotifyGameSessionUpdated = 1115
    }

    public class Server : BlazeComponent
    {
        public override ushort Id => GameManagerComponentBase.Id;
        public override string Name => GameManagerComponentBase.Name;

        public virtual bool IsCommandSupported(GameManagerCommand command) => false;

        public class GameManagerException : BlazeRpcException
        {
            public GameManagerException(Error error) : base((ushort)error, null) { }
            public GameManagerException(ServerError error) : base(error.WithErrorPrefix(), null) { }
            public GameManagerException(Error error, Tdf? errorResponse) : base((ushort)error, errorResponse) { }
            public GameManagerException(ServerError error, Tdf? errorResponse) : base(error.WithErrorPrefix(), errorResponse) { }
            public GameManagerException(Error error, Tdf? errorResponse, string? message) : base((ushort)error, errorResponse, message) { }
            public GameManagerException(ServerError error, Tdf? errorResponse, string? message) : base(error.WithErrorPrefix(), errorResponse, message) { }
            public GameManagerException(Error error, Tdf? errorResponse, string? message, Exception? innerException) : base((ushort)error, errorResponse, message, innerException) { }
            public GameManagerException(ServerError error, Tdf? errorResponse, string? message, Exception? innerException) : base(error.WithErrorPrefix(), errorResponse, message, innerException) { }
        }

        public Server()
        {
            // Slave commands
            RegisterCommand(new RpcCommandFunc<CreateGameRequest, CreateGameResponse, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.createGame,
                Name = "createGame",
                IsSupported = IsCommandSupported(GameManagerCommand.createGame),
                Func = async (req, ctx) => await CreateGameAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<DestroyGameRequest, DestroyGameResponse, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.destroyGame,
                Name = "destroyGame",
                IsSupported = IsCommandSupported(GameManagerCommand.destroyGame),
                Func = async (req, ctx) => await DestroyGameAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<AdvanceGameStateRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.advanceGameState,
                Name = "advanceGameState",
                IsSupported = IsCommandSupported(GameManagerCommand.advanceGameState),
                Func = async (req, ctx) => await AdvanceGameStateAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<SetGameSettingsRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.setGameSettings,
                Name = "setGameSettings",
                IsSupported = IsCommandSupported(GameManagerCommand.setGameSettings),
                Func = async (req, ctx) => await SetGameSettingsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<SetPlayerCapacityRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.setPlayerCapacity,
                Name = "setPlayerCapacity",
                IsSupported = IsCommandSupported(GameManagerCommand.setPlayerCapacity),
                Func = async (req, ctx) => await SetPlayerCapacityAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<SetPresenceModeRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.setPresenceMode,
                Name = "setPresenceMode",
                IsSupported = IsCommandSupported(GameManagerCommand.setPresenceMode),
                Func = async (req, ctx) => await SetPresenceModeAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<SetGameAttributesRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.setGameAttributes,
                Name = "setGameAttributes",
                IsSupported = IsCommandSupported(GameManagerCommand.setGameAttributes),
                Func = async (req, ctx) => await SetGameAttributesAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<SetPlayerAttributesRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.setPlayerAttributes,
                Name = "setPlayerAttributes",
                IsSupported = IsCommandSupported(GameManagerCommand.setPlayerAttributes),
                Func = async (req, ctx) => await SetPlayerAttributesAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<JoinGameRequest, JoinGameResponse, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.joinGame,
                Name = "joinGame",
                IsSupported = IsCommandSupported(GameManagerCommand.joinGame),
                Func = async (req, ctx) => await JoinGameAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<CreateGameTemplateRequest, JoinGameResponse, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.createGameTemplate,
                Name = "createGameTemplate",
                IsSupported = IsCommandSupported(GameManagerCommand.createGameTemplate),
                Func = async (req, ctx) => await CreateGameTemplateAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<RemovePlayerRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.removePlayer,
                Name = "removePlayer",
                IsSupported = IsCommandSupported(GameManagerCommand.removePlayer),
                Func = async (req, ctx) => await RemovePlayerAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<StartMatchmakingRequest, StartMatchmakingResponse, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.startMatchmaking,
                Name = "startMatchmaking",
                IsSupported = IsCommandSupported(GameManagerCommand.startMatchmaking),
                Func = async (req, ctx) => await StartMatchmakingAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<CancelMatchmakingRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.cancelMatchmaking,
                Name = "cancelMatchmaking",
                IsSupported = IsCommandSupported(GameManagerCommand.cancelMatchmaking),
                Func = async (req, ctx) => await CancelMatchmakingAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UpdateGameSessionRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.finalizeGameCreation,
                Name = "finalizeGameCreation",
                IsSupported = IsCommandSupported(GameManagerCommand.finalizeGameCreation),
                Func = async (req, ctx) => await FinalizeGameCreationAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<StartMatchmakingScenarioRequest, StartMatchmakingScenarioResponse, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.startMatchmakingScenario,
                Name = "startMatchmakingScenario",
                IsSupported = IsCommandSupported(GameManagerCommand.startMatchmakingScenario),
                Func = async (req, ctx) => await StartMatchmakingScenarioAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<CancelMatchmakingScenarioRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.cancelMatchmakingScenario,
                Name = "cancelMatchmakingScenario",
                IsSupported = IsCommandSupported(GameManagerCommand.cancelMatchmakingScenario),
                Func = async (req, ctx) => await CancelMatchmakingScenarioAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<SetPlayerCustomDataRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.setPlayerCustomData,
                Name = "setPlayerCustomData",
                IsSupported = IsCommandSupported(GameManagerCommand.setPlayerCustomData),
                Func = async (req, ctx) => await SetPlayerCustomDataAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<ReplayGameRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.replayGame,
                Name = "replayGame",
                IsSupported = IsCommandSupported(GameManagerCommand.replayGame),
                Func = async (req, ctx) => await ReplayGameAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<ReturnDedicatedServerToPoolRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.returnDedicatedServerToPool,
                Name = "returnDedicatedServerToPool",
                IsSupported = IsCommandSupported(GameManagerCommand.returnDedicatedServerToPool),
                Func = async (req, ctx) => await ReturnDedicatedServerToPoolAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<JoinGameRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.joinGameByGroup,
                Name = "joinGameByGroup",
                IsSupported = IsCommandSupported(GameManagerCommand.joinGameByGroup),
                Func = async (req, ctx) => await JoinGameByGroupAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<RemovePlayerRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.leaveGameByGroup,
                Name = "leaveGameByGroup",
                IsSupported = IsCommandSupported(GameManagerCommand.leaveGameByGroup),
                Func = async (req, ctx) => await LeaveGameByGroupAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<MigrateHostRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.migrateGame,
                Name = "migrateGame",
                IsSupported = IsCommandSupported(GameManagerCommand.migrateGame),
                Func = async (req, ctx) => await MigrateGameAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UpdateGameHostMigrationStatusRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.updateGameHostMigrationStatus,
                Name = "updateGameHostMigrationStatus",
                IsSupported = IsCommandSupported(GameManagerCommand.updateGameHostMigrationStatus),
                Func = async (req, ctx) => await UpdateGameHostMigrationStatusAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<CreateGameRequest, JoinGameResponse, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.resetDedicatedServer,
                Name = "resetDedicatedServer",
                IsSupported = IsCommandSupported(GameManagerCommand.resetDedicatedServer),
                Func = async (req, ctx) => await ResetDedicatedServerAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UpdateGameSessionRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.updateGameSession,
                Name = "updateGameSession",
                IsSupported = IsCommandSupported(GameManagerCommand.updateGameSession),
                Func = async (req, ctx) => await UpdateGameSessionAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<BanPlayerRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.banPlayer,
                Name = "banPlayer",
                IsSupported = IsCommandSupported(GameManagerCommand.banPlayer),
                Func = async (req, ctx) => await BanPlayerAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<MatchmakingDedicatedServerOverrideRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.matchmakingDedicatedServerOverride,
                Name = "matchmakingDedicatedServerOverride",
                IsSupported = IsCommandSupported(GameManagerCommand.matchmakingDedicatedServerOverride),
                Func = async (req, ctx) => await MatchmakingDedicatedServerOverrideAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.updateMeshConnection,
                Name = "updateMeshConnection",
                IsSupported = IsCommandSupported(GameManagerCommand.updateMeshConnection),
                Func = async (req, ctx) => await UpdateMeshConnectionAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<JoinGameByUserListRequest, JoinGameResponse, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.joinGameByUserList,
                Name = "joinGameByUserList",
                IsSupported = IsCommandSupported(GameManagerCommand.joinGameByUserList),
                Func = async (req, ctx) => await JoinGameByUserListAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<RemovePlayerFromBannedListRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.removePlayerFromBannedList,
                Name = "removePlayerFromBannedList",
                IsSupported = IsCommandSupported(GameManagerCommand.removePlayerFromBannedList),
                Func = async (req, ctx) => await RemovePlayerFromBannedListAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<BannedListRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.clearBannedList,
                Name = "clearBannedList",
                IsSupported = IsCommandSupported(GameManagerCommand.clearBannedList),
                Func = async (req, ctx) => await ClearBannedListAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<BannedListRequest, BannedListResponse, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.getBannedList,
                Name = "getBannedList",
                IsSupported = IsCommandSupported(GameManagerCommand.getBannedList),
                Func = async (req, ctx) => await GetBannedListAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<MatchmakingFillServersOverrideList, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.matchmakingFillServersOverride,
                Name = "matchmakingFillServersOverride",
                IsSupported = IsCommandSupported(GameManagerCommand.matchmakingFillServersOverride),
                Func = async (req, ctx) => await MatchmakingFillServersOverrideAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<AddQueuedPlayerToGameRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.addQueuedPlayerToGame,
                Name = "addQueuedPlayerToGame",
                IsSupported = IsCommandSupported(GameManagerCommand.addQueuedPlayerToGame),
                Func = async (req, ctx) => await AddQueuedPlayerToGameAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UpdateGameNameRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.updateGameName,
                Name = "updateGameName",
                IsSupported = IsCommandSupported(GameManagerCommand.updateGameName),
                Func = async (req, ctx) => await UpdateGameNameAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EjectHostRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.ejectHost,
                Name = "ejectHost",
                IsSupported = IsCommandSupported(GameManagerCommand.ejectHost),
                Func = async (req, ctx) => await EjectHostAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<SetGameModRegisterRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.setGameModRegister,
                Name = "setGameModRegister",
                IsSupported = IsCommandSupported(GameManagerCommand.setGameModRegister),
                Func = async (req, ctx) => await SetGameModRegisterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<SetGameEntryCriteriaRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.setGameEntryCriteria,
                Name = "setGameEntryCriteria",
                IsSupported = IsCommandSupported(GameManagerCommand.setGameEntryCriteria),
                Func = async (req, ctx) => await SetGameEntryCriteriaAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<PreferredJoinOptOutRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.preferredJoinOptOut,
                Name = "preferredJoinOptOut",
                IsSupported = IsCommandSupported(GameManagerCommand.preferredJoinOptOut),
                Func = async (req, ctx) => await PreferredJoinOptOutAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<DestroyGamesRequest, DestroyGamesResponse, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.destroyGames,
                Name = "destroyGames",
                IsSupported = IsCommandSupported(GameManagerCommand.destroyGames),
                Func = async (req, ctx) => await DestroyGamesAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<CreateGameRequest, JoinGameResponse, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.createOrJoinGame,
                Name = "createOrJoinGame",
                IsSupported = IsCommandSupported(GameManagerCommand.createOrJoinGame),
                Func = async (req, ctx) => await CreateOrJoinGameAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<MigrateHostRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.requestPlatformHost,
                Name = "requestPlatformHost",
                IsSupported = IsCommandSupported(GameManagerCommand.requestPlatformHost),
                Func = async (req, ctx) => await RequestPlatformHostAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<DemoteReservedPlayerToQueueRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.demoteReservedPlayerToQueue,
                Name = "demoteReservedPlayerToQueue",
                IsSupported = IsCommandSupported(GameManagerCommand.demoteReservedPlayerToQueue),
                Func = async (req, ctx) => await DemoteReservedPlayerToQueueAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<CreateGameRequest, CreateGameResponse, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.createPseudoGame,
                Name = "createPseudoGame",
                IsSupported = IsCommandSupported(GameManagerCommand.createPseudoGame),
                Func = async (req, ctx) => await CreatePseudoGameAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<CreatePseudoGamesRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.createPseudoGames,
                Name = "createPseudoGames",
                IsSupported = IsCommandSupported(GameManagerCommand.createPseudoGames),
                Func = async (req, ctx) => await CreatePseudoGamesAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GameCaptureResponse, IsGameCaptureDoneResponse, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.isGameCaptureDone,
                Name = "isGameCaptureDone",
                IsSupported = IsCommandSupported(GameManagerCommand.isGameCaptureDone),
                Func = async (req, ctx) => await IsGameCaptureDoneAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<MeshEndpointsConnectedRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.meshEndpointsConnected,
                Name = "meshEndpointsConnected",
                IsSupported = IsCommandSupported(GameManagerCommand.meshEndpointsConnected),
                Func = async (req, ctx) => await MeshEndpointsConnectedAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<MeshEndpointsDisconnectedRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.meshEndpointsDisconnected,
                Name = "meshEndpointsDisconnected",
                IsSupported = IsCommandSupported(GameManagerCommand.meshEndpointsDisconnected),
                Func = async (req, ctx) => await MeshEndpointsDisconnectedAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<MeshEndpointsConnectionLostRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.meshEndpointsConnectionLost,
                Name = "meshEndpointsConnectionLost",
                IsSupported = IsCommandSupported(GameManagerCommand.meshEndpointsConnectionLost),
                Func = async (req, ctx) => await MeshEndpointsConnectionLostAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.requestConnectivityViaCCS,
                Name = "requestConnectivityViaCCS",
                IsSupported = IsCommandSupported(GameManagerCommand.requestConnectivityViaCCS),
                Func = async (req, ctx) => await RequestConnectivityViaCCSAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.freeConnectivityViaCCS,
                Name = "freeConnectivityViaCCS",
                IsSupported = IsCommandSupported(GameManagerCommand.freeConnectivityViaCCS),
                Func = async (req, ctx) => await FreeConnectivityViaCCSAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.requestLeaseExtensionViaCCS,
                Name = "requestLeaseExtensionViaCCS",
                IsSupported = IsCommandSupported(GameManagerCommand.requestLeaseExtensionViaCCS),
                Func = async (req, ctx) => await RequestLeaseExtensionViaCCSAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<CreateGameTemplateRequest, CreateTournamentGameResponse, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.createTournamentGame,
                Name = "createTournamentGame",
                IsSupported = IsCommandSupported(GameManagerCommand.createTournamentGame),
                Func = async (req, ctx) => await CreateTournamentGameAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<CancelTournamentGameRequest, DestroyGameResponse, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.cancelTournamentGame,
                Name = "cancelTournamentGame",
                IsSupported = IsCommandSupported(GameManagerCommand.cancelTournamentGame),
                Func = async (req, ctx) => await CancelTournamentGameAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetTournamentGameStatusRequest, GetTournamentGameStatusResult, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.getTournamentGameStatus,
                Name = "getTournamentGameStatus",
                IsSupported = IsCommandSupported(GameManagerCommand.getTournamentGameStatus),
                Func = async (req, ctx) => await GetTournamentGameStatusAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetGameListScenarioRequest, GetGameListResponse, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.getGameListByScenario,
                Name = "getGameListByScenario",
                IsSupported = IsCommandSupported(GameManagerCommand.getGameListByScenario),
                Func = async (req, ctx) => await GetGameListByScenarioAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetGameListRequest, GetGameListResponse, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.getGameListSnapshot,
                Name = "getGameListSnapshot",
                IsSupported = IsCommandSupported(GameManagerCommand.getGameListSnapshot),
                Func = async (req, ctx) => await GetGameListSnapshotAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetGameListRequest, GetGameListResponse, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.getGameListSubscription,
                Name = "getGameListSubscription",
                IsSupported = IsCommandSupported(GameManagerCommand.getGameListSubscription),
                Func = async (req, ctx) => await GetGameListSubscriptionAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<DestroyGameListRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.destroyGameList,
                Name = "destroyGameList",
                IsSupported = IsCommandSupported(GameManagerCommand.destroyGameList),
                Func = async (req, ctx) => await DestroyGameListAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetFullGameDataRequest, GetFullGameDataResponse, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.getFullGameData,
                Name = "getFullGameData",
                IsSupported = IsCommandSupported(GameManagerCommand.getFullGameData),
                Func = async (req, ctx) => await GetFullGameDataAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, GetMatchmakingConfigResponse, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.getMatchmakingConfig,
                Name = "getMatchmakingConfig",
                IsSupported = IsCommandSupported(GameManagerCommand.getMatchmakingConfig),
                Func = async (req, ctx) => await GetMatchmakingConfigAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetGameDataFromIdRequest, GameBrowserDataList, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.getGameDataFromId,
                Name = "getGameDataFromId",
                IsSupported = IsCommandSupported(GameManagerCommand.getGameDataFromId),
                Func = async (req, ctx) => await GetGameDataFromIdAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UpdateAdminListRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.addAdminPlayer,
                Name = "addAdminPlayer",
                IsSupported = IsCommandSupported(GameManagerCommand.addAdminPlayer),
                Func = async (req, ctx) => await AddAdminPlayerAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UpdateAdminListRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.removeAdminPlayer,
                Name = "removeAdminPlayer",
                IsSupported = IsCommandSupported(GameManagerCommand.removeAdminPlayer),
                Func = async (req, ctx) => await RemoveAdminPlayerAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<SetPlayerTeamRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.setPlayerTeam,
                Name = "setPlayerTeam",
                IsSupported = IsCommandSupported(GameManagerCommand.setPlayerTeam),
                Func = async (req, ctx) => await SetPlayerTeamAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<ChangeTeamIdRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.changeGameTeamId,
                Name = "changeGameTeamId",
                IsSupported = IsCommandSupported(GameManagerCommand.changeGameTeamId),
                Func = async (req, ctx) => await ChangeGameTeamIdAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UpdateAdminListRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.migrateAdminPlayer,
                Name = "migrateAdminPlayer",
                IsSupported = IsCommandSupported(GameManagerCommand.migrateAdminPlayer),
                Func = async (req, ctx) => await MigrateAdminPlayerAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetUserSetGameListSubscriptionRequest, GetGameListResponse, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.getUserSetGameListSubscription,
                Name = "getUserSetGameListSubscription",
                IsSupported = IsCommandSupported(GameManagerCommand.getUserSetGameListSubscription),
                Func = async (req, ctx) => await GetUserSetGameListSubscriptionAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<SwapPlayersRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.swapPlayers,
                Name = "swapPlayers",
                IsSupported = IsCommandSupported(GameManagerCommand.swapPlayers),
                Func = async (req, ctx) => await SwapPlayersAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetGameDataByUserRequest, GameBrowserDataList, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.getGameDataByUser,
                Name = "getGameDataByUser",
                IsSupported = IsCommandSupported(GameManagerCommand.getGameDataByUser),
                Func = async (req, ctx) => await GetGameDataByUserAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<RegisterDynamicDedicatedServerCreatorRequest, RegisterDynamicDedicatedServerCreatorResponse, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.registerDynamicDedicatedServerCreator,
                Name = "registerDynamicDedicatedServerCreator",
                IsSupported = IsCommandSupported(GameManagerCommand.registerDynamicDedicatedServerCreator),
                Func = async (req, ctx) => await RegisterDynamicDedicatedServerCreatorAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UnregisterDynamicDedicatedServerCreatorRequest, UnregisterDynamicDedicatedServerCreatorResponse, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.unregisterDynamicDedicatedServerCreator,
                Name = "unregisterDynamicDedicatedServerCreator",
                IsSupported = IsCommandSupported(GameManagerCommand.unregisterDynamicDedicatedServerCreator),
                Func = async (req, ctx) => await UnregisterDynamicDedicatedServerCreatorAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetGameListRequest, GetGameListSyncResponse, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.getGameListSnapshotSync,
                Name = "getGameListSnapshotSync",
                IsSupported = IsCommandSupported(GameManagerCommand.getGameListSnapshotSync),
                Func = async (req, ctx) => await GetGameListSnapshotSyncAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.findDedicatedServers,
                Name = "findDedicatedServers",
                IsSupported = IsCommandSupported(GameManagerCommand.findDedicatedServers),
                Func = async (req, ctx) => await FindDedicatedServersAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<TelemetryReportRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.reportTelemetry,
                Name = "reportTelemetry",
                IsSupported = IsCommandSupported(GameManagerCommand.reportTelemetry),
                Func = async (req, ctx) => await ReportTelemetryAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.addUsersToConnectionMetricAudit,
                Name = "addUsersToConnectionMetricAudit",
                IsSupported = IsCommandSupported(GameManagerCommand.addUsersToConnectionMetricAudit),
                Func = async (req, ctx) => await AddUsersToConnectionMetricAuditAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.removeUsersFromConnectionMetricAudit,
                Name = "removeUsersFromConnectionMetricAudit",
                IsSupported = IsCommandSupported(GameManagerCommand.removeUsersFromConnectionMetricAudit),
                Func = async (req, ctx) => await RemoveUsersFromConnectionMetricAuditAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.fetchAuditedUsers,
                Name = "fetchAuditedUsers",
                IsSupported = IsCommandSupported(GameManagerCommand.fetchAuditedUsers),
                Func = async (req, ctx) => await FetchAuditedUsersAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.fetchAuditedUserData,
                Name = "fetchAuditedUserData",
                IsSupported = IsCommandSupported(GameManagerCommand.fetchAuditedUserData),
                Func = async (req, ctx) => await FetchAuditedUserDataAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.deleteUserAuditMetricData,
                Name = "deleteUserAuditMetricData",
                IsSupported = IsCommandSupported(GameManagerCommand.deleteUserAuditMetricData),
                Func = async (req, ctx) => await DeleteUserAuditMetricDataAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UpdateExternalSessionPresenceForUserRequest, UpdateExternalSessionPresenceForUserResponse, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.updatePrimaryExternalSessionForUser,
                Name = "updatePrimaryExternalSessionForUser",
                IsSupported = IsCommandSupported(GameManagerCommand.updatePrimaryExternalSessionForUser),
                Func = async (req, ctx) => await UpdatePrimaryExternalSessionForUserAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, StartMatchmakingScenarioResponse, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.startPseudoMatchmakingScenario,
                Name = "startPseudoMatchmakingScenario",
                IsSupported = IsCommandSupported(GameManagerCommand.startPseudoMatchmakingScenario),
                Func = async (req, ctx) => await StartPseudoMatchmakingScenarioAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UpdateExternalSessionImageRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.updateExternalSessionImage,
                Name = "updateExternalSessionImage",
                IsSupported = IsCommandSupported(GameManagerCommand.updateExternalSessionImage),
                Func = async (req, ctx) => await UpdateExternalSessionImageAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UpdateExternalSessionStatusRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.updateExternalSessionStatus,
                Name = "updateExternalSessionStatus",
                IsSupported = IsCommandSupported(GameManagerCommand.updateExternalSessionStatus),
                Func = async (req, ctx) => await UpdateExternalSessionStatusAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<SetUserScenarioVariantRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.setUserScenarioVariant,
                Name = "setUserScenarioVariant",
                IsSupported = IsCommandSupported(GameManagerCommand.setUserScenarioVariant),
                Func = async (req, ctx) => await SetUserScenarioVariantAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetScenarioVariantsRequest, GetScenarioVariantsResponse, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.getScenarioVariants,
                Name = "getScenarioVariants",
                IsSupported = IsCommandSupported(GameManagerCommand.getScenarioVariants),
                Func = async (req, ctx) => await GetScenarioVariantsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<ClearUserScenarioVariantRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerCommand.clearUserScenarioVariant,
                Name = "clearUserScenarioVariant",
                IsSupported = IsCommandSupported(GameManagerCommand.clearUserScenarioVariant),
                Func = async (req, ctx) => await ClearUserScenarioVariantAsync(req, ctx).ConfigureAwait(false)
            });

            // Master commands
            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.createGameMaster,
                Name = "createGameMaster",
                IsSupported = true,
                Func = async (req, ctx) => await CreateGameMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<DestroyGameRequest, DestroyGameResponse, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.destroyGameMaster,
                Name = "destroyGameMaster",
                IsSupported = true,
                Func = async (req, ctx) => await DestroyGameMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<AdvanceGameStateRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.advanceGameStateMaster,
                Name = "advanceGameStateMaster",
                IsSupported = true,
                Func = async (req, ctx) => await AdvanceGameStateMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<SetPlayerCapacityRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.setPlayerCapacityMaster,
                Name = "setPlayerCapacityMaster",
                IsSupported = true,
                Func = async (req, ctx) => await SetPlayerCapacityMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<SetPresenceModeRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.setPresenceModeMaster,
                Name = "setPresenceModeMaster",
                IsSupported = true,
                Func = async (req, ctx) => await SetPresenceModeMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<SetGameAttributesRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.setGameAttributesMaster,
                Name = "setGameAttributesMaster",
                IsSupported = true,
                Func = async (req, ctx) => await SetGameAttributesMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<SetPlayerAttributesRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.setPlayerAttributesMaster,
                Name = "setPlayerAttributesMaster",
                IsSupported = true,
                Func = async (req, ctx) => await SetPlayerAttributesMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.joinGameMaster,
                Name = "joinGameMaster",
                IsSupported = true,
                Func = async (req, ctx) => await JoinGameMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.removePlayerMaster,
                Name = "removePlayerMaster",
                IsSupported = true,
                Func = async (req, ctx) => await RemovePlayerMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UpdateGameSessionRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.finalizeGameCreationMaster,
                Name = "finalizeGameCreationMaster",
                IsSupported = true,
                Func = async (req, ctx) => await FinalizeGameCreationMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<SetGameSettingsRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.setGameSettingsMaster,
                Name = "setGameSettingsMaster",
                IsSupported = true,
                Func = async (req, ctx) => await SetGameSettingsMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<SetPlayerCustomDataRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.setPlayerCustomDataMaster,
                Name = "setPlayerCustomDataMaster",
                IsSupported = true,
                Func = async (req, ctx) => await SetPlayerCustomDataMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<ReplayGameRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.replayGameMaster,
                Name = "replayGameMaster",
                IsSupported = true,
                Func = async (req, ctx) => await ReplayGameMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<ReturnDedicatedServerToPoolRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.returnDedicatedServerToPoolMaster,
                Name = "returnDedicatedServerToPoolMaster",
                IsSupported = true,
                Func = async (req, ctx) => await ReturnDedicatedServerToPoolMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.joinGameByGroupMaster,
                Name = "joinGameByGroupMaster",
                IsSupported = true,
                Func = async (req, ctx) => await JoinGameByGroupMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.leaveGameByGroupMaster,
                Name = "leaveGameByGroupMaster",
                IsSupported = true,
                Func = async (req, ctx) => await LeaveGameByGroupMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<MigrateHostRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.migrateGameMaster,
                Name = "migrateGameMaster",
                IsSupported = true,
                Func = async (req, ctx) => await MigrateGameMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UpdateGameHostMigrationStatusRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.updateGameHostMigrationStatusMaster,
                Name = "updateGameHostMigrationStatusMaster",
                IsSupported = true,
                Func = async (req, ctx) => await UpdateGameHostMigrationStatusMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.resetDedicatedServerMaster,
                Name = "resetDedicatedServerMaster",
                IsSupported = true,
                Func = async (req, ctx) => await ResetDedicatedServerMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UpdateGameSessionRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.updateGameSessionMaster,
                Name = "updateGameSessionMaster",
                IsSupported = true,
                Func = async (req, ctx) => await UpdateGameSessionMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UpdateAdminListRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.addAdminPlayerMaster,
                Name = "addAdminPlayerMaster",
                IsSupported = true,
                Func = async (req, ctx) => await AddAdminPlayerMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UpdateAdminListRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.removeAdminPlayerMaster,
                Name = "removeAdminPlayerMaster",
                IsSupported = true,
                Func = async (req, ctx) => await RemoveAdminPlayerMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.banPlayerMaster,
                Name = "banPlayerMaster",
                IsSupported = true,
                Func = async (req, ctx) => await BanPlayerMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<ChangeTeamIdRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.changeGameTeamIdMaster,
                Name = "changeGameTeamIdMaster",
                IsSupported = true,
                Func = async (req, ctx) => await ChangeGameTeamIdMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UpdateAdminListRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.migrateAdminPlayerMaster,
                Name = "migrateAdminPlayerMaster",
                IsSupported = true,
                Func = async (req, ctx) => await MigrateAdminPlayerMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<AddQueuedPlayerToGameRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.addQueuedPlayerToGameMaster,
                Name = "addQueuedPlayerToGameMaster",
                IsSupported = true,
                Func = async (req, ctx) => await AddQueuedPlayerToGameMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.removePlayerFromBannedListMaster,
                Name = "removePlayerFromBannedListMaster",
                IsSupported = true,
                Func = async (req, ctx) => await RemovePlayerFromBannedListMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<BannedListRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.clearBannedListMaster,
                Name = "clearBannedListMaster",
                IsSupported = true,
                Func = async (req, ctx) => await ClearBannedListMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<BannedListRequest, BannedListResponse, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.getBannedListMaster,
                Name = "getBannedListMaster",
                IsSupported = true,
                Func = async (req, ctx) => await GetBannedListMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UpdateGameNameRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.updateGameNameMaster,
                Name = "updateGameNameMaster",
                IsSupported = true,
                Func = async (req, ctx) => await UpdateGameNameMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<SwapPlayersRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.swapPlayersMaster,
                Name = "swapPlayersMaster",
                IsSupported = true,
                Func = async (req, ctx) => await SwapPlayersMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EjectHostRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.ejectHostMaster,
                Name = "ejectHostMaster",
                IsSupported = true,
                Func = async (req, ctx) => await EjectHostMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<SetGameModRegisterRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.setGameModRegisterMaster,
                Name = "setGameModRegisterMaster",
                IsSupported = true,
                Func = async (req, ctx) => await SetGameModRegisterMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<SetGameEntryCriteriaRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.setGameEntryCriteriaMaster,
                Name = "setGameEntryCriteriaMaster",
                IsSupported = true,
                Func = async (req, ctx) => await SetGameEntryCriteriaMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.preferredJoinOptOutMaster,
                Name = "preferredJoinOptOutMaster",
                IsSupported = true,
                Func = async (req, ctx) => await PreferredJoinOptOutMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.hasGameMappedByPersistedIdMaster,
                Name = "hasGameMappedByPersistedIdMaster",
                IsSupported = true,
                Func = async (req, ctx) => await HasGameMappedByPersistedIdMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<MigrateHostRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.requestPlatformHostMaster,
                Name = "requestPlatformHostMaster",
                IsSupported = true,
                Func = async (req, ctx) => await RequestPlatformHostMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.reservePersistedIdMaster,
                Name = "reservePersistedIdMaster",
                IsSupported = true,
                Func = async (req, ctx) => await ReservePersistedIdMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GameCaptureResponse, IsGameCaptureDoneResponse, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.isGameCaptureDoneMaster,
                Name = "isGameCaptureDoneMaster",
                IsSupported = true,
                Func = async (req, ctx) => await IsGameCaptureDoneMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.meshEndpointsConnectedMaster,
                Name = "meshEndpointsConnectedMaster",
                IsSupported = true,
                Func = async (req, ctx) => await MeshEndpointsConnectedMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.meshEndpointsDisconnectedMaster,
                Name = "meshEndpointsDisconnectedMaster",
                IsSupported = true,
                Func = async (req, ctx) => await MeshEndpointsDisconnectedMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.meshEndpointsConnectionLostMaster,
                Name = "meshEndpointsConnectionLostMaster",
                IsSupported = true,
                Func = async (req, ctx) => await MeshEndpointsConnectionLostMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.getGameInfoSnapshot,
                Name = "getGameInfoSnapshot",
                IsSupported = true,
                Func = async (req, ctx) => await GetGameInfoSnapshotAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<DemoteReservedPlayerToQueueRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.demoteReservedPlayerToQueueMaster,
                Name = "demoteReservedPlayerToQueueMaster",
                IsSupported = true,
                Func = async (req, ctx) => await DemoteReservedPlayerToQueueMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.matchmakingFoundGame,
                Name = "matchmakingFoundGame",
                IsSupported = true,
                Func = async (req, ctx) => await MatchmakingFoundGameAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.matchmakingCreateGame,
                Name = "matchmakingCreateGame",
                IsSupported = true,
                Func = async (req, ctx) => await MatchmakingCreateGameAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.matchmakingCreateGameWithPrivilegedId,
                Name = "matchmakingCreateGameWithPrivilegedId",
                IsSupported = true,
                Func = async (req, ctx) => await MatchmakingCreateGameWithPrivilegedIdAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.chooseHostForInjection,
                Name = "chooseHostForInjection",
                IsSupported = true,
                Func = async (req, ctx) => await ChooseHostForInjectionAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.resyncExternalSessionMembers,
                Name = "resyncExternalSessionMembers",
                IsSupported = true,
                Func = async (req, ctx) => await ResyncExternalSessionMembersAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.setGameResponsiveMaster,
                Name = "setGameResponsiveMaster",
                IsSupported = true,
                Func = async (req, ctx) => await SetGameResponsiveMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.processTelemetryReport,
                Name = "processTelemetryReport",
                IsSupported = true,
                Func = async (req, ctx) => await ProcessTelemetryReportAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.addUsersToConnectionMetricAuditMaster,
                Name = "addUsersToConnectionMetricAuditMaster",
                IsSupported = true,
                Func = async (req, ctx) => await AddUsersToConnectionMetricAuditMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.removeUsersFromConnectionMetricAuditMaster,
                Name = "removeUsersFromConnectionMetricAuditMaster",
                IsSupported = true,
                Func = async (req, ctx) => await RemoveUsersFromConnectionMetricAuditMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.getPSUByGameNetworkTopology,
                Name = "getPSUByGameNetworkTopology",
                IsSupported = true,
                Func = async (req, ctx) => await GetPSUByGameNetworkTopologyAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.ccsConnectivityResultsAvailable,
                Name = "ccsConnectivityResultsAvailable",
                IsSupported = true,
                Func = async (req, ctx) => await CcsConnectivityResultsAvailableAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.ccsLeaseExtensionResultsAvailable,
                Name = "ccsLeaseExtensionResultsAvailable",
                IsSupported = true,
                Func = async (req, ctx) => await CcsLeaseExtensionResultsAvailableAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.getPktReceivedMetrics,
                Name = "getPktReceivedMetrics",
                IsSupported = true,
                Func = async (req, ctx) => await GetPktReceivedMetricsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.trackExtSessionMembershipMaster,
                Name = "trackExtSessionMembershipMaster",
                IsSupported = true,
                Func = async (req, ctx) => await TrackExtSessionMembershipMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.untrackExtSessionMembershipMaster,
                Name = "untrackExtSessionMembershipMaster",
                IsSupported = true,
                Func = async (req, ctx) => await UntrackExtSessionMembershipMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.getExternalSessionInfoMaster,
                Name = "getExternalSessionInfoMaster",
                IsSupported = true,
                Func = async (req, ctx) => await GetExternalSessionInfoMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UpdateExternalSessionStatusRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.updateExternalSessionStatusMaster,
                Name = "updateExternalSessionStatusMaster",
                IsSupported = true,
                Func = async (req, ctx) => await UpdateExternalSessionStatusMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)GameManagerMasterCommand.getExternalDataSourceApiListByGameId,
                Name = "getExternalDataSourceApiListByGameId",
                IsSupported = true,
                Func = async (req, ctx) => await GetExternalDataSourceApiListByGameIdAsync(req, ctx).ConfigureAwait(false)
            });

        }

        public override string GetErrorName(ushort errorCode)
        {
            return ((Error)errorCode).ToString();
        }

        // ===== Slave command handlers =====

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::createGame</b> command.<br/>
        /// Request type: <see cref="CreateGameRequest"/><br/>
        /// Response type: <see cref="CreateGameResponse"/><br/>
        /// </summary>
        public virtual Task<CreateGameResponse> CreateGameAsync(CreateGameRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::destroyGame</b> command.<br/>
        /// Request type: <see cref="DestroyGameRequest"/><br/>
        /// Response type: <see cref="DestroyGameResponse"/><br/>
        /// </summary>
        public virtual Task<DestroyGameResponse> DestroyGameAsync(DestroyGameRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::advanceGameState</b> command.<br/>
        /// Request type: <see cref="AdvanceGameStateRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> AdvanceGameStateAsync(AdvanceGameStateRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::setGameSettings</b> command.<br/>
        /// Request type: <see cref="SetGameSettingsRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> SetGameSettingsAsync(SetGameSettingsRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::setPlayerCapacity</b> command.<br/>
        /// Request type: <see cref="SetPlayerCapacityRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> SetPlayerCapacityAsync(SetPlayerCapacityRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::setPresenceMode</b> command.<br/>
        /// Request type: <see cref="SetPresenceModeRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> SetPresenceModeAsync(SetPresenceModeRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::setGameAttributes</b> command.<br/>
        /// Request type: <see cref="SetGameAttributesRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> SetGameAttributesAsync(SetGameAttributesRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::setPlayerAttributes</b> command.<br/>
        /// Request type: <see cref="SetPlayerAttributesRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> SetPlayerAttributesAsync(SetPlayerAttributesRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::joinGame</b> command.<br/>
        /// Request type: <see cref="JoinGameRequest"/><br/>
        /// Response type: <see cref="JoinGameResponse"/><br/>
        /// </summary>
        public virtual Task<JoinGameResponse> JoinGameAsync(JoinGameRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::createGameTemplate</b> command.<br/>
        /// Request type: <see cref="CreateGameTemplateRequest"/><br/>
        /// Response type: <see cref="JoinGameResponse"/><br/>
        /// </summary>
        public virtual Task<JoinGameResponse> CreateGameTemplateAsync(CreateGameTemplateRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::removePlayer</b> command.<br/>
        /// Request type: <see cref="RemovePlayerRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> RemovePlayerAsync(RemovePlayerRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::finalizeGameCreation</b> command.<br/>
        /// Request type: <see cref="UpdateGameSessionRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> FinalizeGameCreationAsync(UpdateGameSessionRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::startMatchmaking</b> command.<br/>
        /// Request type: <see cref="StartMatchmakingRequest"/><br/>
        /// Response type: <see cref="StartMatchmakingResponse"/><br/>
        /// </summary>
        public virtual Task<StartMatchmakingResponse> StartMatchmakingAsync(StartMatchmakingRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::cancelMatchmaking</b> command.<br/>
        /// Request type: <see cref="CancelMatchmakingRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> CancelMatchmakingAsync(CancelMatchmakingRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::startMatchmakingScenario</b> command.<br/>
        /// Request type: <see cref="StartMatchmakingScenarioRequest"/><br/>
        /// Response type: <see cref="StartMatchmakingScenarioResponse"/><br/>
        /// </summary>
        public virtual Task<StartMatchmakingScenarioResponse> StartMatchmakingScenarioAsync(StartMatchmakingScenarioRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::cancelMatchmakingScenario</b> command.<br/>
        /// Request type: <see cref="CancelMatchmakingScenarioRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> CancelMatchmakingScenarioAsync(CancelMatchmakingScenarioRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::setPlayerCustomData</b> command.<br/>
        /// Request type: <see cref="SetPlayerCustomDataRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> SetPlayerCustomDataAsync(SetPlayerCustomDataRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::replayGame</b> command.<br/>
        /// Request type: <see cref="ReplayGameRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> ReplayGameAsync(ReplayGameRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::returnDedicatedServerToPool</b> command.<br/>
        /// Request type: <see cref="ReturnDedicatedServerToPoolRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> ReturnDedicatedServerToPoolAsync(ReturnDedicatedServerToPoolRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::joinGameByGroup</b> command.<br/>
        /// Request type: <see cref="JoinGameRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> JoinGameByGroupAsync(JoinGameRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::leaveGameByGroup</b> command.<br/>
        /// Request type: <see cref="RemovePlayerRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> LeaveGameByGroupAsync(RemovePlayerRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::migrateGame</b> command.<br/>
        /// Request type: <see cref="MigrateHostRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> MigrateGameAsync(MigrateHostRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::updateGameHostMigrationStatus</b> command.<br/>
        /// Request type: <see cref="UpdateGameHostMigrationStatusRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> UpdateGameHostMigrationStatusAsync(UpdateGameHostMigrationStatusRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::resetDedicatedServer</b> command.<br/>
        /// Request type: <see cref="CreateGameRequest"/><br/>
        /// Response type: <see cref="JoinGameResponse"/><br/>
        /// </summary>
        public virtual Task<JoinGameResponse> ResetDedicatedServerAsync(CreateGameRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::updateGameSession</b> command.<br/>
        /// Request type: <see cref="UpdateGameSessionRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> UpdateGameSessionAsync(UpdateGameSessionRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::banPlayer</b> command.<br/>
        /// Request type: <see cref="BanPlayerRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> BanPlayerAsync(BanPlayerRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::matchmakingDedicatedServerOverride</b> command.<br/>
        /// Request type: <see cref="MatchmakingDedicatedServerOverrideRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> MatchmakingDedicatedServerOverrideAsync(MatchmakingDedicatedServerOverrideRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::updateMeshConnection</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> UpdateMeshConnectionAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::joinGameByUserList</b> command.<br/>
        /// Request type: <see cref="JoinGameByUserListRequest"/><br/>
        /// Response type: <see cref="JoinGameResponse"/><br/>
        /// </summary>
        public virtual Task<JoinGameResponse> JoinGameByUserListAsync(JoinGameByUserListRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::removePlayerFromBannedList</b> command.<br/>
        /// Request type: <see cref="RemovePlayerFromBannedListRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> RemovePlayerFromBannedListAsync(RemovePlayerFromBannedListRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::clearBannedList</b> command.<br/>
        /// Request type: <see cref="BannedListRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> ClearBannedListAsync(BannedListRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::getBannedList</b> command.<br/>
        /// Request type: <see cref="BannedListRequest"/><br/>
        /// Response type: <see cref="BannedListResponse"/><br/>
        /// </summary>
        public virtual Task<BannedListResponse> GetBannedListAsync(BannedListRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::matchmakingFillServersOverride</b> command.<br/>
        /// Request type: <see cref="MatchmakingFillServersOverrideList"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> MatchmakingFillServersOverrideAsync(MatchmakingFillServersOverrideList request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::addQueuedPlayerToGame</b> command.<br/>
        /// Request type: <see cref="AddQueuedPlayerToGameRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> AddQueuedPlayerToGameAsync(AddQueuedPlayerToGameRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::updateGameName</b> command.<br/>
        /// Request type: <see cref="UpdateGameNameRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> UpdateGameNameAsync(UpdateGameNameRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::ejectHost</b> command.<br/>
        /// Request type: <see cref="EjectHostRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> EjectHostAsync(EjectHostRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::setGameModRegister</b> command.<br/>
        /// Request type: <see cref="SetGameModRegisterRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> SetGameModRegisterAsync(SetGameModRegisterRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::setGameEntryCriteria</b> command.<br/>
        /// Request type: <see cref="SetGameEntryCriteriaRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> SetGameEntryCriteriaAsync(SetGameEntryCriteriaRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::preferredJoinOptOut</b> command.<br/>
        /// Request type: <see cref="PreferredJoinOptOutRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> PreferredJoinOptOutAsync(PreferredJoinOptOutRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::destroyGames</b> command.<br/>
        /// Request type: <see cref="DestroyGamesRequest"/><br/>
        /// Response type: <see cref="DestroyGamesResponse"/><br/>
        /// </summary>
        public virtual Task<DestroyGamesResponse> DestroyGamesAsync(DestroyGamesRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::createOrJoinGame</b> command.<br/>
        /// Request type: <see cref="CreateGameRequest"/><br/>
        /// Response type: <see cref="JoinGameResponse"/><br/>
        /// </summary>
        public virtual Task<JoinGameResponse> CreateOrJoinGameAsync(CreateGameRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::requestPlatformHost</b> command.<br/>
        /// Request type: <see cref="MigrateHostRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> RequestPlatformHostAsync(MigrateHostRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::demoteReservedPlayerToQueue</b> command.<br/>
        /// Request type: <see cref="DemoteReservedPlayerToQueueRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> DemoteReservedPlayerToQueueAsync(DemoteReservedPlayerToQueueRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::createPseudoGame</b> command.<br/>
        /// Request type: <see cref="CreateGameRequest"/><br/>
        /// Response type: <see cref="CreateGameResponse"/><br/>
        /// </summary>
        public virtual Task<CreateGameResponse> CreatePseudoGameAsync(CreateGameRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::createPseudoGames</b> command.<br/>
        /// Request type: <see cref="CreatePseudoGamesRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> CreatePseudoGamesAsync(CreatePseudoGamesRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::isGameCaptureDone</b> command.<br/>
        /// Request type: <see cref="GameCaptureResponse"/><br/>
        /// Response type: <see cref="IsGameCaptureDoneResponse"/><br/>
        /// </summary>
        public virtual Task<IsGameCaptureDoneResponse> IsGameCaptureDoneAsync(GameCaptureResponse request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::meshEndpointsConnected</b> command.<br/>
        /// Request type: <see cref="MeshEndpointsConnectedRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> MeshEndpointsConnectedAsync(MeshEndpointsConnectedRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::meshEndpointsDisconnected</b> command.<br/>
        /// Request type: <see cref="MeshEndpointsDisconnectedRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> MeshEndpointsDisconnectedAsync(MeshEndpointsDisconnectedRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::meshEndpointsConnectionLost</b> command.<br/>
        /// Request type: <see cref="MeshEndpointsConnectionLostRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> MeshEndpointsConnectionLostAsync(MeshEndpointsConnectionLostRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::requestConnectivityViaCCS</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> RequestConnectivityViaCCSAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::freeConnectivityViaCCS</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> FreeConnectivityViaCCSAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::requestLeaseExtensionViaCCS</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> RequestLeaseExtensionViaCCSAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::createTournamentGame</b> command.<br/>
        /// Request type: <see cref="CreateGameTemplateRequest"/><br/>
        /// Response type: <see cref="CreateTournamentGameResponse"/><br/>
        /// </summary>
        public virtual Task<CreateTournamentGameResponse> CreateTournamentGameAsync(CreateGameTemplateRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::cancelTournamentGame</b> command.<br/>
        /// Request type: <see cref="CancelTournamentGameRequest"/><br/>
        /// Response type: <see cref="DestroyGameResponse"/><br/>
        /// </summary>
        public virtual Task<DestroyGameResponse> CancelTournamentGameAsync(CancelTournamentGameRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::getTournamentGameStatus</b> command.<br/>
        /// Request type: <see cref="GetTournamentGameStatusRequest"/><br/>
        /// Response type: <see cref="GetTournamentGameStatusResult"/><br/>
        /// </summary>
        public virtual Task<GetTournamentGameStatusResult> GetTournamentGameStatusAsync(GetTournamentGameStatusRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::getGameListByScenario</b> command.<br/>
        /// Request type: <see cref="GetGameListScenarioRequest"/><br/>
        /// Response type: <see cref="GetGameListResponse"/><br/>
        /// </summary>
        public virtual Task<GetGameListResponse> GetGameListByScenarioAsync(GetGameListScenarioRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::getGameListSnapshot</b> command.<br/>
        /// Request type: <see cref="GetGameListRequest"/><br/>
        /// Response type: <see cref="GetGameListResponse"/><br/>
        /// </summary>
        public virtual Task<GetGameListResponse> GetGameListSnapshotAsync(GetGameListRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::getGameListSubscription</b> command.<br/>
        /// Request type: <see cref="GetGameListRequest"/><br/>
        /// Response type: <see cref="GetGameListResponse"/><br/>
        /// </summary>
        public virtual Task<GetGameListResponse> GetGameListSubscriptionAsync(GetGameListRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::destroyGameList</b> command.<br/>
        /// Request type: <see cref="DestroyGameListRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> DestroyGameListAsync(DestroyGameListRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::getFullGameData</b> command.<br/>
        /// Request type: <see cref="GetFullGameDataRequest"/><br/>
        /// Response type: <see cref="GetFullGameDataResponse"/><br/>
        /// </summary>
        public virtual Task<GetFullGameDataResponse> GetFullGameDataAsync(GetFullGameDataRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::getMatchmakingConfig</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="GetMatchmakingConfigResponse"/><br/>
        /// </summary>
        public virtual Task<GetMatchmakingConfigResponse> GetMatchmakingConfigAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::getGameDataFromId</b> command.<br/>
        /// Request type: <see cref="GetGameDataFromIdRequest"/><br/>
        /// Response type: <see cref="GameBrowserDataList"/><br/>
        /// </summary>
        public virtual Task<GameBrowserDataList> GetGameDataFromIdAsync(GetGameDataFromIdRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::addAdminPlayer</b> command.<br/>
        /// Request type: <see cref="UpdateAdminListRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> AddAdminPlayerAsync(UpdateAdminListRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::removeAdminPlayer</b> command.<br/>
        /// Request type: <see cref="UpdateAdminListRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> RemoveAdminPlayerAsync(UpdateAdminListRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::setPlayerTeam</b> command.<br/>
        /// Request type: <see cref="SetPlayerTeamRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> SetPlayerTeamAsync(SetPlayerTeamRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::changeGameTeamId</b> command.<br/>
        /// Request type: <see cref="ChangeTeamIdRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> ChangeGameTeamIdAsync(ChangeTeamIdRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::migrateAdminPlayer</b> command.<br/>
        /// Request type: <see cref="UpdateAdminListRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> MigrateAdminPlayerAsync(UpdateAdminListRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::getUserSetGameListSubscription</b> command.<br/>
        /// Request type: <see cref="GetUserSetGameListSubscriptionRequest"/><br/>
        /// Response type: <see cref="GetGameListResponse"/><br/>
        /// </summary>
        public virtual Task<GetGameListResponse> GetUserSetGameListSubscriptionAsync(GetUserSetGameListSubscriptionRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::swapPlayers</b> command.<br/>
        /// Request type: <see cref="SwapPlayersRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> SwapPlayersAsync(SwapPlayersRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::getGameDataByUser</b> command.<br/>
        /// Request type: <see cref="GetGameDataByUserRequest"/><br/>
        /// Response type: <see cref="GameBrowserDataList"/><br/>
        /// </summary>
        public virtual Task<GameBrowserDataList> GetGameDataByUserAsync(GetGameDataByUserRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::registerDynamicDedicatedServerCreator</b> command.<br/>
        /// Request type: <see cref="RegisterDynamicDedicatedServerCreatorRequest"/><br/>
        /// Response type: <see cref="RegisterDynamicDedicatedServerCreatorResponse"/><br/>
        /// </summary>
        public virtual Task<RegisterDynamicDedicatedServerCreatorResponse> RegisterDynamicDedicatedServerCreatorAsync(RegisterDynamicDedicatedServerCreatorRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::unregisterDynamicDedicatedServerCreator</b> command.<br/>
        /// Request type: <see cref="UnregisterDynamicDedicatedServerCreatorRequest"/><br/>
        /// Response type: <see cref="UnregisterDynamicDedicatedServerCreatorResponse"/><br/>
        /// </summary>
        public virtual Task<UnregisterDynamicDedicatedServerCreatorResponse> UnregisterDynamicDedicatedServerCreatorAsync(UnregisterDynamicDedicatedServerCreatorRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::getGameListSnapshotSync</b> command.<br/>
        /// Request type: <see cref="GetGameListRequest"/><br/>
        /// Response type: <see cref="GetGameListSyncResponse"/><br/>
        /// </summary>
        public virtual Task<GetGameListSyncResponse> GetGameListSnapshotSyncAsync(GetGameListRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::findDedicatedServers</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> FindDedicatedServersAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::reportTelemetry</b> command.<br/>
        /// Request type: <see cref="TelemetryReportRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> ReportTelemetryAsync(TelemetryReportRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::addUsersToConnectionMetricAudit</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> AddUsersToConnectionMetricAuditAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::removeUsersFromConnectionMetricAudit</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> RemoveUsersFromConnectionMetricAuditAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::fetchAuditedUsers</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> FetchAuditedUsersAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::fetchAuditedUserData</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> FetchAuditedUserDataAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::deleteUserAuditMetricData</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> DeleteUserAuditMetricDataAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::updatePrimaryExternalSessionForUser</b> command.<br/>
        /// Request type: <see cref="UpdateExternalSessionPresenceForUserRequest"/><br/>
        /// Response type: <see cref="UpdateExternalSessionPresenceForUserResponse"/><br/>
        /// </summary>
        public virtual Task<UpdateExternalSessionPresenceForUserResponse> UpdatePrimaryExternalSessionForUserAsync(UpdateExternalSessionPresenceForUserRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::startPseudoMatchmakingScenario</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="StartMatchmakingScenarioResponse"/><br/>
        /// </summary>
        public virtual Task<StartMatchmakingScenarioResponse> StartPseudoMatchmakingScenarioAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::updateExternalSessionImage</b> command.<br/>
        /// Request type: <see cref="UpdateExternalSessionImageRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> UpdateExternalSessionImageAsync(UpdateExternalSessionImageRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::updateExternalSessionStatus</b> command.<br/>
        /// Request type: <see cref="UpdateExternalSessionStatusRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> UpdateExternalSessionStatusAsync(UpdateExternalSessionStatusRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::setUserScenarioVariant</b> command.<br/>
        /// Request type: <see cref="SetUserScenarioVariantRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> SetUserScenarioVariantAsync(SetUserScenarioVariantRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::getScenarioVariants</b> command.<br/>
        /// Request type: <see cref="GetScenarioVariantsRequest"/><br/>
        /// Response type: <see cref="GetScenarioVariantsResponse"/><br/>
        /// </summary>
        public virtual Task<GetScenarioVariantsResponse> GetScenarioVariantsAsync(GetScenarioVariantsRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::clearUserScenarioVariant</b> command.<br/>
        /// Request type: <see cref="ClearUserScenarioVariantRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> ClearUserScenarioVariantAsync(ClearUserScenarioVariantRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        // ===== Master command handlers =====

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::createGameMaster</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> CreateGameMasterAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::destroyGameMaster</b> master command.<br/>
        /// Request type: <see cref="DestroyGameRequest"/><br/>
        /// Response type: <see cref="DestroyGameResponse"/><br/>
        /// </summary>
        public virtual Task<DestroyGameResponse> DestroyGameMasterAsync(DestroyGameRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::advanceGameStateMaster</b> master command.<br/>
        /// Request type: <see cref="AdvanceGameStateRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> AdvanceGameStateMasterAsync(AdvanceGameStateRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::setPlayerCapacityMaster</b> master command.<br/>
        /// Request type: <see cref="SetPlayerCapacityRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> SetPlayerCapacityMasterAsync(SetPlayerCapacityRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::setPresenceModeMaster</b> master command.<br/>
        /// Request type: <see cref="SetPresenceModeRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> SetPresenceModeMasterAsync(SetPresenceModeRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::setGameAttributesMaster</b> master command.<br/>
        /// Request type: <see cref="SetGameAttributesRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> SetGameAttributesMasterAsync(SetGameAttributesRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::setPlayerAttributesMaster</b> master command.<br/>
        /// Request type: <see cref="SetPlayerAttributesRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> SetPlayerAttributesMasterAsync(SetPlayerAttributesRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::joinGameMaster</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> JoinGameMasterAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::removePlayerMaster</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> RemovePlayerMasterAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::finalizeGameCreationMaster</b> master command.<br/>
        /// Request type: <see cref="UpdateGameSessionRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> FinalizeGameCreationMasterAsync(UpdateGameSessionRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::setGameSettingsMaster</b> master command.<br/>
        /// Request type: <see cref="SetGameSettingsRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> SetGameSettingsMasterAsync(SetGameSettingsRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::setPlayerCustomDataMaster</b> master command.<br/>
        /// Request type: <see cref="SetPlayerCustomDataRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> SetPlayerCustomDataMasterAsync(SetPlayerCustomDataRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::replayGameMaster</b> master command.<br/>
        /// Request type: <see cref="ReplayGameRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> ReplayGameMasterAsync(ReplayGameRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::returnDedicatedServerToPoolMaster</b> master command.<br/>
        /// Request type: <see cref="ReturnDedicatedServerToPoolRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> ReturnDedicatedServerToPoolMasterAsync(ReturnDedicatedServerToPoolRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::joinGameByGroupMaster</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> JoinGameByGroupMasterAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::leaveGameByGroupMaster</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> LeaveGameByGroupMasterAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::migrateGameMaster</b> master command.<br/>
        /// Request type: <see cref="MigrateHostRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> MigrateGameMasterAsync(MigrateHostRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::updateGameHostMigrationStatusMaster</b> master command.<br/>
        /// Request type: <see cref="UpdateGameHostMigrationStatusRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> UpdateGameHostMigrationStatusMasterAsync(UpdateGameHostMigrationStatusRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::resetDedicatedServerMaster</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> ResetDedicatedServerMasterAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::updateGameSessionMaster</b> master command.<br/>
        /// Request type: <see cref="UpdateGameSessionRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> UpdateGameSessionMasterAsync(UpdateGameSessionRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::addAdminPlayerMaster</b> master command.<br/>
        /// Request type: <see cref="UpdateAdminListRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> AddAdminPlayerMasterAsync(UpdateAdminListRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::removeAdminPlayerMaster</b> master command.<br/>
        /// Request type: <see cref="UpdateAdminListRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> RemoveAdminPlayerMasterAsync(UpdateAdminListRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::banPlayerMaster</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> BanPlayerMasterAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::changeGameTeamIdMaster</b> master command.<br/>
        /// Request type: <see cref="ChangeTeamIdRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> ChangeGameTeamIdMasterAsync(ChangeTeamIdRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::migrateAdminPlayerMaster</b> master command.<br/>
        /// Request type: <see cref="UpdateAdminListRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> MigrateAdminPlayerMasterAsync(UpdateAdminListRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::addQueuedPlayerToGameMaster</b> master command.<br/>
        /// Request type: <see cref="AddQueuedPlayerToGameRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> AddQueuedPlayerToGameMasterAsync(AddQueuedPlayerToGameRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::removePlayerFromBannedListMaster</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> RemovePlayerFromBannedListMasterAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::clearBannedListMaster</b> master command.<br/>
        /// Request type: <see cref="BannedListRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> ClearBannedListMasterAsync(BannedListRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::getBannedListMaster</b> master command.<br/>
        /// Request type: <see cref="BannedListRequest"/><br/>
        /// Response type: <see cref="BannedListResponse"/><br/>
        /// </summary>
        public virtual Task<BannedListResponse> GetBannedListMasterAsync(BannedListRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::updateGameNameMaster</b> master command.<br/>
        /// Request type: <see cref="UpdateGameNameRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> UpdateGameNameMasterAsync(UpdateGameNameRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::swapPlayersMaster</b> master command.<br/>
        /// Request type: <see cref="SwapPlayersRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> SwapPlayersMasterAsync(SwapPlayersRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::ejectHostMaster</b> master command.<br/>
        /// Request type: <see cref="EjectHostRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> EjectHostMasterAsync(EjectHostRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::setGameModRegisterMaster</b> master command.<br/>
        /// Request type: <see cref="SetGameModRegisterRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> SetGameModRegisterMasterAsync(SetGameModRegisterRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::setGameEntryCriteriaMaster</b> master command.<br/>
        /// Request type: <see cref="SetGameEntryCriteriaRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> SetGameEntryCriteriaMasterAsync(SetGameEntryCriteriaRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::preferredJoinOptOutMaster</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> PreferredJoinOptOutMasterAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::hasGameMappedByPersistedIdMaster</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> HasGameMappedByPersistedIdMasterAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::requestPlatformHostMaster</b> master command.<br/>
        /// Request type: <see cref="MigrateHostRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> RequestPlatformHostMasterAsync(MigrateHostRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::reservePersistedIdMaster</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> ReservePersistedIdMasterAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::isGameCaptureDoneMaster</b> master command.<br/>
        /// Request type: <see cref="GameCaptureResponse"/><br/>
        /// Response type: <see cref="IsGameCaptureDoneResponse"/><br/>
        /// </summary>
        public virtual Task<IsGameCaptureDoneResponse> IsGameCaptureDoneMasterAsync(GameCaptureResponse request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::meshEndpointsConnectedMaster</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> MeshEndpointsConnectedMasterAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::meshEndpointsDisconnectedMaster</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> MeshEndpointsDisconnectedMasterAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::meshEndpointsConnectionLostMaster</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> MeshEndpointsConnectionLostMasterAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::getGameInfoSnapshot</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> GetGameInfoSnapshotAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::demoteReservedPlayerToQueueMaster</b> master command.<br/>
        /// Request type: <see cref="DemoteReservedPlayerToQueueRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> DemoteReservedPlayerToQueueMasterAsync(DemoteReservedPlayerToQueueRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::matchmakingFoundGame</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> MatchmakingFoundGameAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::matchmakingCreateGame</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> MatchmakingCreateGameAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::matchmakingCreateGameWithPrivilegedId</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> MatchmakingCreateGameWithPrivilegedIdAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::chooseHostForInjection</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> ChooseHostForInjectionAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::resyncExternalSessionMembers</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> ResyncExternalSessionMembersAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::setGameResponsiveMaster</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> SetGameResponsiveMasterAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::processTelemetryReport</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> ProcessTelemetryReportAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::addUsersToConnectionMetricAuditMaster</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> AddUsersToConnectionMetricAuditMasterAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::removeUsersFromConnectionMetricAuditMaster</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> RemoveUsersFromConnectionMetricAuditMasterAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::getPSUByGameNetworkTopology</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> GetPSUByGameNetworkTopologyAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::ccsConnectivityResultsAvailable</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> CcsConnectivityResultsAvailableAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::ccsLeaseExtensionResultsAvailable</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> CcsLeaseExtensionResultsAvailableAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::getPktReceivedMetrics</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> GetPktReceivedMetricsAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::trackExtSessionMembershipMaster</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> TrackExtSessionMembershipMasterAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::untrackExtSessionMembershipMaster</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> UntrackExtSessionMembershipMasterAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::getExternalSessionInfoMaster</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> GetExternalSessionInfoMasterAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::updateExternalSessionStatusMaster</b> master command.<br/>
        /// Request type: <see cref="UpdateExternalSessionStatusRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> UpdateExternalSessionStatusMasterAsync(UpdateExternalSessionStatusRequest request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>GameManagerComponent::getExternalDataSourceApiListByGameId</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> GetExternalDataSourceApiListByGameIdAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new GameManagerException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        // ===== Notification helpers =====

        /// <summary>
        /// Call this method when you want to send to client a <b>GameManagerComponent::NotifyMatchmakingScenarioPseudoSuccess</b> notification.<br/>
        /// Notification type: <see cref="NotifyMatchmakingScenarioPseudoSuccess"/><br/>
        /// </summary>
        public static Task NotifyMatchmakingScenarioPseudoSuccessAsync(BlazeRpcConnection connection, NotifyMatchmakingScenarioPseudoSuccess notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerNotification.NotifyMatchmakingScenarioPseudoSuccess;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>GameManagerComponent::NotifyMatchmakingFailed</b> notification.<br/>
        /// Notification type: <see cref="NotifyMatchmakingFinished"/><br/>
        /// </summary>
        public static Task NotifyMatchmakingFailedAsync(BlazeRpcConnection connection, NotifyMatchmakingFinished notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerNotification.NotifyMatchmakingFailed;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>GameManagerComponent::NotifyMatchmakingSessionConnectionValidated</b> notification.<br/>
        /// Notification type: <see cref="NotifyMatchmakingSessionConnectionValidated"/><br/>
        /// </summary>
        public static Task NotifyMatchmakingSessionConnectionValidatedAsync(BlazeRpcConnection connection, NotifyMatchmakingSessionConnectionValidated notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerNotification.NotifyMatchmakingSessionConnectionValidated;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>GameManagerComponent::NotifyMatchmakingAsyncStatus</b> notification.<br/>
        /// Notification type: <see cref="NotifyMatchmakingAsyncStatus"/><br/>
        /// </summary>
        public static Task NotifyMatchmakingAsyncStatusAsync(BlazeRpcConnection connection, NotifyMatchmakingAsyncStatus notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerNotification.NotifyMatchmakingAsyncStatus;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>GameManagerComponent::NotifyMatchmakingPseudoSuccess</b> notification.<br/>
        /// Notification type: <see cref="NotifyMatchmakingPseudoSuccess"/><br/>
        /// </summary>
        public static Task NotifyMatchmakingPseudoSuccessAsync(BlazeRpcConnection connection, NotifyMatchmakingPseudoSuccess notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerNotification.NotifyMatchmakingPseudoSuccess;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>GameManagerComponent::NotifyRemoteMatchmakingStarted</b> notification.<br/>
        /// Notification type: <see cref="NotifyRemoteMatchmakingStarted"/><br/>
        /// </summary>
        public static Task NotifyRemoteMatchmakingStartedAsync(BlazeRpcConnection connection, NotifyRemoteMatchmakingStarted notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerNotification.NotifyRemoteMatchmakingStarted;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>GameManagerComponent::NotifyRemoteMatchmakingEnded</b> notification.<br/>
        /// Notification type: <see cref="NotifyRemoteMatchmakingEnded"/><br/>
        /// </summary>
        public static Task NotifyRemoteMatchmakingEndedAsync(BlazeRpcConnection connection, NotifyRemoteMatchmakingEnded notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerNotification.NotifyRemoteMatchmakingEnded;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>GameManagerComponent::NotifyGameRemoved</b> notification.<br/>
        /// Notification type: <see cref="NotifyGameRemoved"/><br/>
        /// </summary>
        public static Task NotifyGameRemovedAsync(BlazeRpcConnection connection, NotifyGameRemoved notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerNotification.NotifyGameRemoved;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>GameManagerComponent::NotifyGameSetup</b> notification.<br/>
        /// Notification type: <see cref="NotifyGameSetup"/><br/>
        /// </summary>
        public static Task NotifyGameSetupAsync(BlazeRpcConnection connection, NotifyGameSetup notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerNotification.NotifyGameSetup;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>GameManagerComponent::NotifyPlayerJoining</b> notification.<br/>
        /// Notification type: <see cref="NotifyPlayerJoining"/><br/>
        /// </summary>
        public static Task NotifyPlayerJoiningAsync(BlazeRpcConnection connection, NotifyPlayerJoining notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerNotification.NotifyPlayerJoining;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>GameManagerComponent::NotifyJoiningPlayerInitiateConnections</b> notification.<br/>
        /// Notification type: <see cref="NotifyGameSetup"/><br/>
        /// </summary>
        public static Task NotifyJoiningPlayerInitiateConnectionsAsync(BlazeRpcConnection connection, NotifyGameSetup notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerNotification.NotifyJoiningPlayerInitiateConnections;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>GameManagerComponent::NotifyPlayerJoiningQueue</b> notification.<br/>
        /// Notification type: <see cref="NotifyPlayerJoining"/><br/>
        /// </summary>
        public static Task NotifyPlayerJoiningQueueAsync(BlazeRpcConnection connection, NotifyPlayerJoining notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerNotification.NotifyPlayerJoiningQueue;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>GameManagerComponent::NotifyPlayerPromotedFromQueue</b> notification.<br/>
        /// Notification type: <see cref="NotifyPlayerJoining"/><br/>
        /// </summary>
        public static Task NotifyPlayerPromotedFromQueueAsync(BlazeRpcConnection connection, NotifyPlayerJoining notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerNotification.NotifyPlayerPromotedFromQueue;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>GameManagerComponent::NotifyPlayerClaimingReservation</b> notification.<br/>
        /// Notification type: <see cref="NotifyPlayerJoining"/><br/>
        /// </summary>
        public static Task NotifyPlayerClaimingReservationAsync(BlazeRpcConnection connection, NotifyPlayerJoining notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerNotification.NotifyPlayerClaimingReservation;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>GameManagerComponent::NotifyPlayerDemotedToQueue</b> notification.<br/>
        /// Notification type: <see cref="NotifyPlayerJoining"/><br/>
        /// </summary>
        public static Task NotifyPlayerDemotedToQueueAsync(BlazeRpcConnection connection, NotifyPlayerJoining notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerNotification.NotifyPlayerDemotedToQueue;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>GameManagerComponent::NotifyPlayerJoinCompleted</b> notification.<br/>
        /// Notification type: <see cref="NotifyPlayerJoinCompleted"/><br/>
        /// </summary>
        public static Task NotifyPlayerJoinCompletedAsync(BlazeRpcConnection connection, NotifyPlayerJoinCompleted notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerNotification.NotifyPlayerJoinCompleted;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>GameManagerComponent::NotifyPlayerRemoved</b> notification.<br/>
        /// Notification type: <see cref="NotifyPlayerRemoved"/><br/>
        /// </summary>
        public static Task NotifyPlayerRemovedAsync(BlazeRpcConnection connection, NotifyPlayerRemoved notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerNotification.NotifyPlayerRemoved;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>GameManagerComponent::NotifyRemoteJoinFailed</b> notification.<br/>
        /// Notification type: <see cref="NotifyRemoteJoinFailed"/><br/>
        /// </summary>
        public static Task NotifyRemoteJoinFailedAsync(BlazeRpcConnection connection, NotifyRemoteJoinFailed notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerNotification.NotifyRemoteJoinFailed;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>GameManagerComponent::NotifyHostMigrationFinished</b> notification.<br/>
        /// Notification type: <see cref="NotifyHostMigrationFinished"/><br/>
        /// </summary>
        public static Task NotifyHostMigrationFinishedAsync(BlazeRpcConnection connection, NotifyHostMigrationFinished notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerNotification.NotifyHostMigrationFinished;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>GameManagerComponent::NotifyHostMigrationStart</b> notification.<br/>
        /// Notification type: <see cref="NotifyHostMigrationStart"/><br/>
        /// </summary>
        public static Task NotifyHostMigrationStartAsync(BlazeRpcConnection connection, NotifyHostMigrationStart notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerNotification.NotifyHostMigrationStart;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>GameManagerComponent::NotifyPlatformHostInitialized</b> notification.<br/>
        /// Notification type: <see cref="NotifyPlatformHostInitialized"/><br/>
        /// </summary>
        public static Task NotifyPlatformHostInitializedAsync(BlazeRpcConnection connection, NotifyPlatformHostInitialized notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerNotification.NotifyPlatformHostInitialized;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>GameManagerComponent::NotifyGameAttribChange</b> notification.<br/>
        /// Notification type: <see cref="NotifyGameAttribChange"/><br/>
        /// </summary>
        public static Task NotifyGameAttribChangeAsync(BlazeRpcConnection connection, NotifyGameAttribChange notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerNotification.NotifyGameAttribChange;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>GameManagerComponent::NotifyPlayerAttribChange</b> notification.<br/>
        /// Notification type: <see cref="NotifyPlayerAttribChange"/><br/>
        /// </summary>
        public static Task NotifyPlayerAttribChangeAsync(BlazeRpcConnection connection, NotifyPlayerAttribChange notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerNotification.NotifyPlayerAttribChange;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>GameManagerComponent::NotifyPlayerCustomDataChange</b> notification.<br/>
        /// Notification type: <see cref="NotifyPlayerCustomDataChange"/><br/>
        /// </summary>
        public static Task NotifyPlayerCustomDataChangeAsync(BlazeRpcConnection connection, NotifyPlayerCustomDataChange notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerNotification.NotifyPlayerCustomDataChange;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>GameManagerComponent::NotifyGameStateChange</b> notification.<br/>
        /// Notification type: <see cref="NotifyGameStateChange"/><br/>
        /// </summary>
        public static Task NotifyGameStateChangeAsync(BlazeRpcConnection connection, NotifyGameStateChange notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerNotification.NotifyGameStateChange;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>GameManagerComponent::NotifyGameSettingsChange</b> notification.<br/>
        /// Notification type: <see cref="NotifyGameSettingsChange"/><br/>
        /// </summary>
        public static Task NotifyGameSettingsChangeAsync(BlazeRpcConnection connection, NotifyGameSettingsChange notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerNotification.NotifyGameSettingsChange;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>GameManagerComponent::NotifyGameCapacityChange</b> notification.<br/>
        /// Notification type: <see cref="NotifyGameCapacityChange"/><br/>
        /// </summary>
        public static Task NotifyGameCapacityChangeAsync(BlazeRpcConnection connection, NotifyGameCapacityChange notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerNotification.NotifyGameCapacityChange;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>GameManagerComponent::NotifyGameReset</b> notification.<br/>
        /// Notification type: <see cref="NotifyGameReset"/><br/>
        /// </summary>
        public static Task NotifyGameResetAsync(BlazeRpcConnection connection, NotifyGameReset notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerNotification.NotifyGameReset;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>GameManagerComponent::NotifyGameReportingIdChange</b> notification.<br/>
        /// Notification type: <see cref="NotifyGameReportingIdChange"/><br/>
        /// </summary>
        public static Task NotifyGameReportingIdChangeAsync(BlazeRpcConnection connection, NotifyGameReportingIdChange notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerNotification.NotifyGameReportingIdChange;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>GameManagerComponent::NotifyGamePresenceChanged</b> notification.<br/>
        /// Notification type: <see cref="GameSessionUpdatedNotification"/><br/>
        /// </summary>
        public static Task NotifyGamePresenceChangedAsync(BlazeRpcConnection connection, GameSessionUpdatedNotification notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerNotification.NotifyGamePresenceChanged;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>GameManagerComponent::NotifyGameSessionUpdated</b> notification.<br/>
        /// Notification type: <see cref="GameSessionUpdatedNotification"/><br/>
        /// </summary>
        public static Task NotifyGameSessionUpdatedAsync(BlazeRpcConnection connection, GameSessionUpdatedNotification notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerNotification.NotifyGameSessionUpdated;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>GameManagerComponent::NotifyGamePlayerStateChange</b> notification.<br/>
        /// Notification type: <see cref="NotifyGamePlayerStateChange"/><br/>
        /// </summary>
        public static Task NotifyGamePlayerStateChangeAsync(BlazeRpcConnection connection, NotifyGamePlayerStateChange notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerNotification.NotifyGamePlayerStateChange;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>GameManagerComponent::NotifyGamePlayerTeamRoleSlotChange</b> notification.<br/>
        /// Notification type: <see cref="NotifyGamePlayerTeamRoleSlotChange"/><br/>
        /// </summary>
        public static Task NotifyGamePlayerTeamRoleSlotChangeAsync(BlazeRpcConnection connection, NotifyGamePlayerTeamRoleSlotChange notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerNotification.NotifyGamePlayerTeamRoleSlotChange;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>GameManagerComponent::NotifyGameTeamIdChange</b> notification.<br/>
        /// Notification type: <see cref="NotifyGameTeamIdChange"/><br/>
        /// </summary>
        public static Task NotifyGameTeamIdChangeAsync(BlazeRpcConnection connection, NotifyGameTeamIdChange notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerNotification.NotifyGameTeamIdChange;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>GameManagerComponent::NotifyProcessQueue</b> notification.<br/>
        /// Notification type: <see cref="NotifyProcessQueue"/><br/>
        /// </summary>
        public static Task NotifyProcessQueueAsync(BlazeRpcConnection connection, NotifyProcessQueue notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerNotification.NotifyProcessQueue;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>GameManagerComponent::NotifyPresenceModeChanged</b> notification.<br/>
        /// Notification type: <see cref="NotifyPresenceModeChanged"/><br/>
        /// </summary>
        public static Task NotifyPresenceModeChangedAsync(BlazeRpcConnection connection, NotifyPresenceModeChanged notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerNotification.NotifyPresenceModeChanged;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>GameManagerComponent::NotifyQueueChanged</b> notification.<br/>
        /// Notification type: <see cref="NotifyQueueChanged"/><br/>
        /// </summary>
        public static Task NotifyQueueChangedAsync(BlazeRpcConnection connection, NotifyQueueChanged notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerNotification.NotifyQueueChanged;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>GameManagerComponent::NotifyGameModRegisterChanged</b> notification.<br/>
        /// Notification type: <see cref="NotifyGameModRegisterChanged"/><br/>
        /// </summary>
        public static Task NotifyGameModRegisterChangedAsync(BlazeRpcConnection connection, NotifyGameModRegisterChanged notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerNotification.NotifyGameModRegisterChanged;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>GameManagerComponent::NotifyGameEntryCriteriaChanged</b> notification.<br/>
        /// Notification type: <see cref="NotifyGameEntryCriteriaChanged"/><br/>
        /// </summary>
        public static Task NotifyGameEntryCriteriaChangedAsync(BlazeRpcConnection connection, NotifyGameEntryCriteriaChanged notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerNotification.NotifyGameEntryCriteriaChanged;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>GameManagerComponent::NotifyMatchmakingReservedExternalPlayers</b> notification.<br/>
        /// Notification type: <see cref="NotifyMatchmakingReservedExternalPlayers"/><br/>
        /// </summary>
        public static Task NotifyMatchmakingReservedExternalPlayersAsync(BlazeRpcConnection connection, NotifyMatchmakingReservedExternalPlayers notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerNotification.NotifyMatchmakingReservedExternalPlayers;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>GameManagerComponent::NotifyHostedConnectivityAvailable</b> notification.<br/>
        /// Notification type: <see cref="NotifyHostedConnectivityAvailable"/><br/>
        /// </summary>
        public static Task NotifyHostedConnectivityAvailableAsync(BlazeRpcConnection connection, NotifyHostedConnectivityAvailable notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerNotification.NotifyHostedConnectivityAvailable;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>GameManagerComponent::NotifyGameListUpdate</b> notification.<br/>
        /// Notification type: <see cref="NotifyGameListUpdate"/><br/>
        /// </summary>
        public static Task NotifyGameListUpdateAsync(BlazeRpcConnection connection, NotifyGameListUpdate notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerNotification.NotifyGameListUpdate;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>GameManagerComponent::NotifyAdminListChange</b> notification.<br/>
        /// Notification type: <see cref="NotifyAdminListChange"/><br/>
        /// </summary>
        public static Task NotifyAdminListChangeAsync(BlazeRpcConnection connection, NotifyAdminListChange notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerNotification.NotifyAdminListChange;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>GameManagerComponent::NotifyGameNameChange</b> notification.<br/>
        /// Notification type: <see cref="NotifyGameNameChange"/><br/>
        /// </summary>
        public static Task NotifyGameNameChangeAsync(BlazeRpcConnection connection, NotifyGameNameChange notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerNotification.NotifyGameNameChange;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>GameManagerComponent::UserScenarioVariantUpdate</b> notification.<br/>
        /// Notification type: <see cref="SetUserScenarioVariantRequest"/><br/>
        /// </summary>
        public static Task NotifyUserScenarioVariantUpdateAsync(BlazeRpcConnection connection, SetUserScenarioVariantRequest notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerNotification.UserScenarioVariantUpdate;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>GameManagerComponent::ClearUserScenarioVariant</b> notification.<br/>
        /// Notification type: <see cref="ClearUserScenarioVariantRequest"/><br/>
        /// </summary>
        public static Task NotifyClearUserScenarioVariantAsync(BlazeRpcConnection connection, ClearUserScenarioVariantRequest notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerNotification.ClearUserScenarioVariant;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        // ===== Event notification helpers =====

        /// <summary>
        /// Call this method when you want to send a <b>GameManagerComponent::PlayerJoinedGameSessionEvent</b> event.<br/>
        /// Event type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public static Task NotifyPlayerJoinedGameSessionEventAsync(BlazeRpcConnection connection, EmptyMessage notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerEvent.PlayerJoinedGameSessionEvent;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send a <b>GameManagerComponent::PlayerLeftGameSessionEvent</b> event.<br/>
        /// Event type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public static Task NotifyPlayerLeftGameSessionEventAsync(BlazeRpcConnection connection, EmptyMessage notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerEvent.PlayerLeftGameSessionEvent;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send a <b>GameManagerComponent::CreatedGameSessionEvent</b> event.<br/>
        /// Event type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public static Task NotifyCreatedGameSessionEventAsync(BlazeRpcConnection connection, EmptyMessage notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerEvent.CreatedGameSessionEvent;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send a <b>GameManagerComponent::DestroyedGameSessionEvent</b> event.<br/>
        /// Event type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public static Task NotifyDestroyedGameSessionEventAsync(BlazeRpcConnection connection, EmptyMessage notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerEvent.DestroyedGameSessionEvent;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send a <b>GameManagerComponent::GameSessionStateChangedEvent</b> event.<br/>
        /// Event type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public static Task NotifyGameSessionStateChangedEventAsync(BlazeRpcConnection connection, EmptyMessage notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerEvent.GameSessionStateChangedEvent;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send a <b>GameManagerComponent::PlayerReservedGameSessionEvent</b> event.<br/>
        /// Event type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public static Task NotifyPlayerReservedGameSessionEventAsync(BlazeRpcConnection connection, EmptyMessage notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerEvent.PlayerReservedGameSessionEvent;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send a <b>GameManagerComponent::PlayerQueuedGameSessionEvent</b> event.<br/>
        /// Event type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public static Task NotifyPlayerQueuedGameSessionEventAsync(BlazeRpcConnection connection, EmptyMessage notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerEvent.PlayerQueuedGameSessionEvent;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send a <b>GameManagerComponent::PlayerDequeuedGameSessionEvent</b> event.<br/>
        /// Event type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public static Task NotifyPlayerDequeuedGameSessionEventAsync(BlazeRpcConnection connection, EmptyMessage notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerEvent.PlayerDequeuedGameSessionEvent;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send a <b>GameManagerComponent::UpdatedGameSessionDataEvent</b> event.<br/>
        /// Event type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public static Task NotifyUpdatedGameSessionDataEventAsync(BlazeRpcConnection connection, EmptyMessage notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerEvent.UpdatedGameSessionDataEvent;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send a <b>GameManagerComponent::SuccesfulMatchmakingSessionEvent</b> event.<br/>
        /// Event type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public static Task NotifySuccesfulMatchmakingSessionEventAsync(BlazeRpcConnection connection, EmptyMessage notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerEvent.SuccesfulMatchmakingSessionEvent;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send a <b>GameManagerComponent::FailedMatchmakingSessionEvent</b> event.<br/>
        /// Event type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public static Task NotifyFailedMatchmakingSessionEventAsync(BlazeRpcConnection connection, EmptyMessage notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerEvent.FailedMatchmakingSessionEvent;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send a <b>GameManagerComponent::NotifyGamePresenceChanged</b> event.<br/>
        /// Event type: <see cref="GameSessionUpdatedNotification"/><br/>
        /// </summary>
        public static Task NotifyNotifyGamePresenceChangedEventAsync(BlazeRpcConnection connection, GameSessionUpdatedNotification notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerEvent.NotifyGamePresenceChanged;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send a <b>GameManagerComponent::NotifyGameSessionUpdated</b> event.<br/>
        /// Event type: <see cref="GameSessionUpdatedNotification"/><br/>
        /// </summary>
        public static Task NotifyNotifyGameSessionUpdatedEventAsync(BlazeRpcConnection connection, GameSessionUpdatedNotification notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = GameManagerComponentBase.Id;
                frame.Command = (ushort)GameManagerEvent.NotifyGameSessionUpdated;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

    }

}
