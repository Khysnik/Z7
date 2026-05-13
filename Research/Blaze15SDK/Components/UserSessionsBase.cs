using Blaze.Core;
using Blaze15SDK.Blaze;
using EATDF;
using EATDF.Types;

namespace Blaze15SDK.Components;

public static class UserSessionsBase
{
    public const ushort Id = 30722;
    public const string Name = "UserSessionsComponent";

    public enum Error : ushort
    {
        USER_ERR_USER_NOT_FOUND = 1,
        USER_ERR_SESSION_NOT_FOUND = 2,
        USER_ERR_DUPLICATE_SESSION = 3,
        USER_ERR_NO_EXTENDED_DATA = 4,
        USER_ERR_MAX_DATA_REACHED = 5,
        USER_ERR_KEY_NOT_FOUND = 6,
        USER_ERR_INVALID_SESSION_INSTANCE = 7,
        USER_ERR_INVALID_PARAM = 8,
        USER_ERR_MINIMUM_CHARACTERS = 9,
        ACCESS_GROUP_ERR_INVALID_GROUP = 10,
        ACCESS_GROUP_ERR_DEFAULT_GROUP = 11,
        ACCESS_GROUP_ERR_NOT_CURRENT_GROUP = 12,
        ACCESS_GROUP_ERR_CURRENT_GROUP = 13,
        ACCESS_GROUP_ERR_NO_GROUP_FOUND = 14,
        GEOIP_INCOMPLETE_PARAMETERS = 15,
        USER_ERR_GEOIP_LOOKUP_FAILED = 16,
        ERR_ENTITY_TYPE_NOT_FOUND = 17,
        ERR_ENTITY_NOT_FOUND = 18,
        ERR_NOT_SUPPORTED = 19,
        USER_ERR_EXISTS = 20,
        GEOIP_ERR_USER_OPTOUT = 23,
        USER_ERR_CROSS_PLATFORM_OPTOUT = 30,
        USER_ERR_DISALLOWED_PLATFORM = 31,
    }

    public enum UserSessionsCommand : ushort
    {
        validateSessionKey = 1,
        fetchExtendedData = 3,
        updateExtendedDataAttribute = 5,
        updateHardwareFlags = 8,
        getPermissions = 9,
        getAccessGroup = 10,
        lookupUser = 12,
        lookupUsers = 13,
        lookupUsersByPrefix = 14,
        lookupUsersIdentification = 15,
        updateNetworkInfo = 20,
        listDefaultAccessGroup = 21,
        listAuthorization = 22,
        lookupUserGeoIPData = 23,
        overrideUserGeoIPData = 24,
        updateUserSessionClientData = 25,
        setUserInfoAttribute = 26,
        resetUserGeoIPData = 27,
        lookupEntityByName = 28,
        lookupEntityById = 29,
        lookupEntitiesByIds = 30,
        registerRemoteSlaveSession = 31,
        lookupUserSessionId = 32,
        fetchLastLocaleUsedAndAuthError = 33,
        fetchUserFirstLastAuthTime = 34,
        listAllPermissions = 36,
        setUserGeoOptIn = 37,
        remoteLoadUserExtendedData = 38,
        remoteRefreshUserExtendedData = 39,
        forceSessionLogout = 40,
        enableUserAuditLogging = 41,
        disableUserAuditLogging = 42,
        requestUserExtendedDataProviderRegistration = 43,
        getUserGeoIpData = 44,
        getUniqueDeviceId = 45,
        getIpFilterByName = 46,
        lookupUsersByPrefixMultiNamespace = 47,
        lookupUsersByPersonaNameMultiNamespace = 48,
        lookupUsersByPersonaNamesMultiNamespace = 49,
        lookupUsersByPersonaNames = 50,
        checkConnectivity = 53,
        forceOwnSessionLogout = 54,
        updateLocalUserGroup = 55,
        fetchUsersAuditState = 56,
        refreshQosPingSiteMap = 57,
        getQosPingSites = 58,
        setUserCrossPlatformOptIn = 60,
        lookupUsersCrossPlatform = 61,
        getUedInformationMap = 100,
    }

    public enum UserSessionsMasterCommand : ushort
    {
        getCensusData = 1,
        updateExtendedDataMaster = 6,
        updateBlazeObjectIdList = 7,
        updateHardwareFlagsMaster = 12,
        updateNetworkInfoMaster = 20,
        updateGeoDataMaster = 21,
        updateUserSessionClientDataMaster = 22,
        getUserExtendedData = 25,
        getUserInfoData = 26,
        updateUserSessionServerAttributeMaster = 27,
        getUserSessionData = 28,
        insertUniqueDeviceIdMaster = 30,
        updateDirtySockUserIndexMaster = 31,
        getUserIpAddress = 46,
        addUserSessionSubscriber = 47,
        removeUserSessionSubscriber = 48,
        fetchUserSessions = 49,
        validateUserExistence = 51,
        forceSessionLogoutMaster = 52,
        refreshQosPingSiteMapMaster = 57,
    }

    public enum UserSessionsNotification : ushort
    {
        UserSessionExtendedDataUpdate = 1,
        UserAdded = 2,
        UserRemoved = 3,
        UserUpdated = 5,
        UserSubscriptionsUpdated = 6,
        SessionSubscriptionsUpdated = 7,
        UserAuthenticated = 8,
        UserUnauthenticated = 9,
        NotifyUserInfoUpdated = 10,
        NotifyQosSettingsUpdated = 11,
    }

    public enum UserSessionsEvent : ushort
    {
        LoginEvent = 1000,
        LoginFailureEvent = 1001,
        LogoutEvent = 1002,
        DisconnectEvent = 1003,
    }

    public class Server : BlazeComponent
    {
        public override ushort Id => UserSessionsBase.Id;
        public override string Name => UserSessionsBase.Name;

        public virtual bool IsCommandSupported(UserSessionsCommand command) => false;

        public class UserSessionsException : BlazeRpcException
        {
            public UserSessionsException(Error error) : base((ushort)error, null) { }
            public UserSessionsException(ServerError error) : base(error.WithErrorPrefix(), null) { }
            public UserSessionsException(Error error, Tdf? errorResponse) : base((ushort)error, errorResponse) { }
            public UserSessionsException(ServerError error, Tdf? errorResponse) : base(error.WithErrorPrefix(), errorResponse) { }
            public UserSessionsException(Error error, Tdf? errorResponse, string? message) : base((ushort)error, errorResponse, message) { }
            public UserSessionsException(ServerError error, Tdf? errorResponse, string? message) : base(error.WithErrorPrefix(), errorResponse, message) { }
            public UserSessionsException(Error error, Tdf? errorResponse, string? message, Exception? innerException) : base((ushort)error, errorResponse, message, innerException) { }
            public UserSessionsException(ServerError error, Tdf? errorResponse, string? message, Exception? innerException) : base(error.WithErrorPrefix(), errorResponse, message, innerException) { }
        }

        public Server()
        {
            // Slave commands
            RegisterCommand(new RpcCommandFunc<ValidateSessionKeyRequest, SessionInfo, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.validateSessionKey,
                Name = "validateSessionKey",
                IsSupported = IsCommandSupported(UserSessionsCommand.validateSessionKey),
                Func = async (req, ctx) => await ValidateSessionKeyAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<FetchExtendedDataRequest, UserSessionExtendedData, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.fetchExtendedData,
                Name = "fetchExtendedData",
                IsSupported = IsCommandSupported(UserSessionsCommand.fetchExtendedData),
                Func = async (req, ctx) => await FetchExtendedDataAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UpdateExtendedDataAttributeRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.updateExtendedDataAttribute,
                Name = "updateExtendedDataAttribute",
                IsSupported = IsCommandSupported(UserSessionsCommand.updateExtendedDataAttribute),
                Func = async (req, ctx) => await UpdateExtendedDataAttributeAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UpdateHardwareFlagsRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.updateHardwareFlags,
                Name = "updateHardwareFlags",
                IsSupported = IsCommandSupported(UserSessionsCommand.updateHardwareFlags),
                Func = async (req, ctx) => await UpdateHardwareFlagsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.getPermissions,
                Name = "getPermissions",
                IsSupported = IsCommandSupported(UserSessionsCommand.getPermissions),
                Func = async (req, ctx) => await GetPermissionsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.getAccessGroup,
                Name = "getAccessGroup",
                IsSupported = IsCommandSupported(UserSessionsCommand.getAccessGroup),
                Func = async (req, ctx) => await GetAccessGroupAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UserIdentification, UserData, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.lookupUser,
                Name = "lookupUser",
                IsSupported = IsCommandSupported(UserSessionsCommand.lookupUser),
                Func = async (req, ctx) => await LookupUserAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<LookupUsersRequest, UserDataResponse, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.lookupUsers,
                Name = "lookupUsers",
                IsSupported = IsCommandSupported(UserSessionsCommand.lookupUsers),
                Func = async (req, ctx) => await LookupUsersAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<LookupUsersByPrefixRequest, UserDataResponse, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.lookupUsersByPrefix,
                Name = "lookupUsersByPrefix",
                IsSupported = IsCommandSupported(UserSessionsCommand.lookupUsersByPrefix),
                Func = async (req, ctx) => await LookupUsersByPrefixAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<LookupUsersRequest, LookupUsersResponse, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.lookupUsersIdentification,
                Name = "lookupUsersIdentification",
                IsSupported = IsCommandSupported(UserSessionsCommand.lookupUsersIdentification),
                Func = async (req, ctx) => await LookupUsersIdentificationAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UpdateNetworkInfoRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.updateNetworkInfo,
                Name = "updateNetworkInfo",
                IsSupported = IsCommandSupported(UserSessionsCommand.updateNetworkInfo),
                Func = async (req, ctx) => await UpdateNetworkInfoAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.listDefaultAccessGroup,
                Name = "listDefaultAccessGroup",
                IsSupported = IsCommandSupported(UserSessionsCommand.listDefaultAccessGroup),
                Func = async (req, ctx) => await ListDefaultAccessGroupAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.listAuthorization,
                Name = "listAuthorization",
                IsSupported = IsCommandSupported(UserSessionsCommand.listAuthorization),
                Func = async (req, ctx) => await ListAuthorizationAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UserIdentification, GeoLocationData, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.lookupUserGeoIPData,
                Name = "lookupUserGeoIPData",
                IsSupported = IsCommandSupported(UserSessionsCommand.lookupUserGeoIPData),
                Func = async (req, ctx) => await LookupUserGeoIPDataAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GeoLocationData, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.overrideUserGeoIPData,
                Name = "overrideUserGeoIPData",
                IsSupported = IsCommandSupported(UserSessionsCommand.overrideUserGeoIPData),
                Func = async (req, ctx) => await OverrideUserGeoIPDataAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UpdateUserSessionClientDataRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.updateUserSessionClientData,
                Name = "updateUserSessionClientData",
                IsSupported = IsCommandSupported(UserSessionsCommand.updateUserSessionClientData),
                Func = async (req, ctx) => await UpdateUserSessionClientDataAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<SetUserInfoAttributeRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.setUserInfoAttribute,
                Name = "setUserInfoAttribute",
                IsSupported = IsCommandSupported(UserSessionsCommand.setUserInfoAttribute),
                Func = async (req, ctx) => await SetUserInfoAttributeAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<ResetUserGeoIPDataRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.resetUserGeoIPData,
                Name = "resetUserGeoIPData",
                IsSupported = IsCommandSupported(UserSessionsCommand.resetUserGeoIPData),
                Func = async (req, ctx) => await ResetUserGeoIPDataAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EntityLookupRequest, EntityLookupResponse, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.lookupEntityByName,
                Name = "lookupEntityByName",
                IsSupported = IsCommandSupported(UserSessionsCommand.lookupEntityByName),
                Func = async (req, ctx) => await LookupEntityByNameAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EntityLookupByIdRequest, EntityLookupByIdResponse, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.lookupEntityById,
                Name = "lookupEntityById",
                IsSupported = IsCommandSupported(UserSessionsCommand.lookupEntityById),
                Func = async (req, ctx) => await LookupEntityByIdAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EntitiesLookupByIdsRequest, EntitiesLookupByIdsResponse, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.lookupEntitiesByIds,
                Name = "lookupEntitiesByIds",
                IsSupported = IsCommandSupported(UserSessionsCommand.lookupEntitiesByIds),
                Func = async (req, ctx) => await LookupEntitiesByIdsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.registerRemoteSlaveSession,
                Name = "registerRemoteSlaveSession",
                IsSupported = IsCommandSupported(UserSessionsCommand.registerRemoteSlaveSession),
                Func = async (req, ctx) => await RegisterRemoteSlaveSessionAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<LookupUserSessionIdRequest, LookupUserSessionIdResponse, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.lookupUserSessionId,
                Name = "lookupUserSessionId",
                IsSupported = IsCommandSupported(UserSessionsCommand.lookupUserSessionId),
                Func = async (req, ctx) => await LookupUserSessionIdAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<FetchLastLocaleUsedAndAuthErrorRequest, FetchLastLocaleUsedAndAuthErrorResponse, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.fetchLastLocaleUsedAndAuthError,
                Name = "fetchLastLocaleUsedAndAuthError",
                IsSupported = IsCommandSupported(UserSessionsCommand.fetchLastLocaleUsedAndAuthError),
                Func = async (req, ctx) => await FetchLastLocaleUsedAndAuthErrorAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<FetchUserFirstLastAuthTimeRequest, FetchUserFirstLastAuthTimeResponse, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.fetchUserFirstLastAuthTime,
                Name = "fetchUserFirstLastAuthTime",
                IsSupported = IsCommandSupported(UserSessionsCommand.fetchUserFirstLastAuthTime),
                Func = async (req, ctx) => await FetchUserFirstLastAuthTimeAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<ListAllPermissionsRequest, ListAllPermissionsResponse, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.listAllPermissions,
                Name = "listAllPermissions",
                IsSupported = IsCommandSupported(UserSessionsCommand.listAllPermissions),
                Func = async (req, ctx) => await ListAllPermissionsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<OptInRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.setUserGeoOptIn,
                Name = "setUserGeoOptIn",
                IsSupported = IsCommandSupported(UserSessionsCommand.setUserGeoOptIn),
                Func = async (req, ctx) => await SetUserGeoOptInAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, UserSessionExtendedData, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.remoteLoadUserExtendedData,
                Name = "remoteLoadUserExtendedData",
                IsSupported = IsCommandSupported(UserSessionsCommand.remoteLoadUserExtendedData),
                Func = async (req, ctx) => await RemoteLoadUserExtendedDataAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, UserSessionExtendedData, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.remoteRefreshUserExtendedData,
                Name = "remoteRefreshUserExtendedData",
                IsSupported = IsCommandSupported(UserSessionsCommand.remoteRefreshUserExtendedData),
                Func = async (req, ctx) => await RemoteRefreshUserExtendedDataAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.forceSessionLogout,
                Name = "forceSessionLogout",
                IsSupported = IsCommandSupported(UserSessionsCommand.forceSessionLogout),
                Func = async (req, ctx) => await ForceSessionLogoutAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EnableUserAuditLoggingRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.enableUserAuditLogging,
                Name = "enableUserAuditLogging",
                IsSupported = IsCommandSupported(UserSessionsCommand.enableUserAuditLogging),
                Func = async (req, ctx) => await EnableUserAuditLoggingAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<DisableUserAuditLoggingRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.disableUserAuditLogging,
                Name = "disableUserAuditLogging",
                IsSupported = IsCommandSupported(UserSessionsCommand.disableUserAuditLogging),
                Func = async (req, ctx) => await DisableUserAuditLoggingAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.requestUserExtendedDataProviderRegistration,
                Name = "requestUserExtendedDataProviderRegistration",
                IsSupported = IsCommandSupported(UserSessionsCommand.requestUserExtendedDataProviderRegistration),
                Func = async (req, ctx) => await RequestUserExtendedDataProviderRegistrationAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetUserGeoIpDataRequest, GeoLocationData, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.getUserGeoIpData,
                Name = "getUserGeoIpData",
                IsSupported = IsCommandSupported(UserSessionsCommand.getUserGeoIpData),
                Func = async (req, ctx) => await GetUserGeoIpDataAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, GetUniqueDeviceIdResponse, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.getUniqueDeviceId,
                Name = "getUniqueDeviceId",
                IsSupported = IsCommandSupported(UserSessionsCommand.getUniqueDeviceId),
                Func = async (req, ctx) => await GetUniqueDeviceIdAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.getIpFilterByName,
                Name = "getIpFilterByName",
                IsSupported = IsCommandSupported(UserSessionsCommand.getIpFilterByName),
                Func = async (req, ctx) => await GetIpFilterByNameAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<LookupUsersByPrefixMultiNamespaceRequest, UserDataResponse, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.lookupUsersByPrefixMultiNamespace,
                Name = "lookupUsersByPrefixMultiNamespace",
                IsSupported = IsCommandSupported(UserSessionsCommand.lookupUsersByPrefixMultiNamespace),
                Func = async (req, ctx) => await LookupUsersByPrefixMultiNamespaceAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<LookupUsersByPersonaNameMultiNamespaceRequest, UserDataResponse, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.lookupUsersByPersonaNameMultiNamespace,
                Name = "lookupUsersByPersonaNameMultiNamespace",
                IsSupported = IsCommandSupported(UserSessionsCommand.lookupUsersByPersonaNameMultiNamespace),
                Func = async (req, ctx) => await LookupUsersByPersonaNameMultiNamespaceAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<LookupUsersByPersonaNamesMultiNamespaceRequest, UserDataResponse, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.lookupUsersByPersonaNamesMultiNamespace,
                Name = "lookupUsersByPersonaNamesMultiNamespace",
                IsSupported = IsCommandSupported(UserSessionsCommand.lookupUsersByPersonaNamesMultiNamespace),
                Func = async (req, ctx) => await LookupUsersByPersonaNamesMultiNamespaceAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<LookupUsersByPersonaNamesRequest, UserDataResponse, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.lookupUsersByPersonaNames,
                Name = "lookupUsersByPersonaNames",
                IsSupported = IsCommandSupported(UserSessionsCommand.lookupUsersByPersonaNames),
                Func = async (req, ctx) => await LookupUsersByPersonaNamesAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.checkConnectivity,
                Name = "checkConnectivity",
                IsSupported = IsCommandSupported(UserSessionsCommand.checkConnectivity),
                Func = async (req, ctx) => await CheckConnectivityAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<ForceOwnSessionLogoutRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.forceOwnSessionLogout,
                Name = "forceOwnSessionLogout",
                IsSupported = IsCommandSupported(UserSessionsCommand.forceOwnSessionLogout),
                Func = async (req, ctx) => await ForceOwnSessionLogoutAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UpdateLocalUserGroupRequest, UpdateLocalUserGroupResponse, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.updateLocalUserGroup,
                Name = "updateLocalUserGroup",
                IsSupported = IsCommandSupported(UserSessionsCommand.updateLocalUserGroup),
                Func = async (req, ctx) => await UpdateLocalUserGroupAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.fetchUsersAuditState,
                Name = "fetchUsersAuditState",
                IsSupported = IsCommandSupported(UserSessionsCommand.fetchUsersAuditState),
                Func = async (req, ctx) => await FetchUsersAuditStateAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<RefreshQosPingSiteMapRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.refreshQosPingSiteMap,
                Name = "refreshQosPingSiteMap",
                IsSupported = IsCommandSupported(UserSessionsCommand.refreshQosPingSiteMap),
                Func = async (req, ctx) => await RefreshQosPingSiteMapAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, QosPingSitesResponse, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.getQosPingSites,
                Name = "getQosPingSites",
                IsSupported = IsCommandSupported(UserSessionsCommand.getQosPingSites),
                Func = async (req, ctx) => await GetQosPingSitesAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<OptInRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.setUserCrossPlatformOptIn,
                Name = "setUserCrossPlatformOptIn",
                IsSupported = IsCommandSupported(UserSessionsCommand.setUserCrossPlatformOptIn),
                Func = async (req, ctx) => await SetUserCrossPlatformOptInAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<LookupUsersCrossPlatformRequest, UserDataResponse, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.lookupUsersCrossPlatform,
                Name = "lookupUsersCrossPlatform",
                IsSupported = IsCommandSupported(UserSessionsCommand.lookupUsersCrossPlatform),
                Func = async (req, ctx) => await LookupUsersCrossPlatformAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UserSessionsCommand.getUedInformationMap,
                Name = "getUedInformationMap",
                IsSupported = IsCommandSupported(UserSessionsCommand.getUedInformationMap),
                Func = async (req, ctx) => await GetUedInformationMapAsync(req, ctx).ConfigureAwait(false)
            });

            // Master commands
            RegisterCommand(new RpcCommandFunc<EmptyMessage, UserManagerCensusData, EmptyMessage>()
            {
                Id = (ushort)UserSessionsMasterCommand.getCensusData,
                Name = "getCensusData",
                IsSupported = true,
                Func = async (req, ctx) => await GetCensusDataAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UserSessionsMasterCommand.updateExtendedDataMaster,
                Name = "updateExtendedDataMaster",
                IsSupported = true,
                Func = async (req, ctx) => await UpdateExtendedDataMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UserSessionsMasterCommand.updateBlazeObjectIdList,
                Name = "updateBlazeObjectIdList",
                IsSupported = true,
                Func = async (req, ctx) => await UpdateBlazeObjectIdListAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UserSessionsMasterCommand.updateHardwareFlagsMaster,
                Name = "updateHardwareFlagsMaster",
                IsSupported = true,
                Func = async (req, ctx) => await UpdateHardwareFlagsMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UserSessionsMasterCommand.updateNetworkInfoMaster,
                Name = "updateNetworkInfoMaster",
                IsSupported = true,
                Func = async (req, ctx) => await UpdateNetworkInfoMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UserSessionsMasterCommand.updateGeoDataMaster,
                Name = "updateGeoDataMaster",
                IsSupported = true,
                Func = async (req, ctx) => await UpdateGeoDataMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UserSessionsMasterCommand.updateUserSessionClientDataMaster,
                Name = "updateUserSessionClientDataMaster",
                IsSupported = true,
                Func = async (req, ctx) => await UpdateUserSessionClientDataMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UserSessionsMasterCommand.getUserExtendedData,
                Name = "getUserExtendedData",
                IsSupported = true,
                Func = async (req, ctx) => await GetUserExtendedDataAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UserSessionsMasterCommand.getUserInfoData,
                Name = "getUserInfoData",
                IsSupported = true,
                Func = async (req, ctx) => await GetUserInfoDataAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UserSessionsMasterCommand.updateUserSessionServerAttributeMaster,
                Name = "updateUserSessionServerAttributeMaster",
                IsSupported = true,
                Func = async (req, ctx) => await UpdateUserSessionServerAttributeMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UserSessionsMasterCommand.getUserSessionData,
                Name = "getUserSessionData",
                IsSupported = true,
                Func = async (req, ctx) => await GetUserSessionDataAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UserSessionsMasterCommand.insertUniqueDeviceIdMaster,
                Name = "insertUniqueDeviceIdMaster",
                IsSupported = true,
                Func = async (req, ctx) => await InsertUniqueDeviceIdMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UserSessionsMasterCommand.updateDirtySockUserIndexMaster,
                Name = "updateDirtySockUserIndexMaster",
                IsSupported = true,
                Func = async (req, ctx) => await UpdateDirtySockUserIndexMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UserSessionsMasterCommand.getUserIpAddress,
                Name = "getUserIpAddress",
                IsSupported = true,
                Func = async (req, ctx) => await GetUserIpAddressAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UserSessionsMasterCommand.addUserSessionSubscriber,
                Name = "addUserSessionSubscriber",
                IsSupported = true,
                Func = async (req, ctx) => await AddUserSessionSubscriberAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UserSessionsMasterCommand.removeUserSessionSubscriber,
                Name = "removeUserSessionSubscriber",
                IsSupported = true,
                Func = async (req, ctx) => await RemoveUserSessionSubscriberAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UserSessionsMasterCommand.fetchUserSessions,
                Name = "fetchUserSessions",
                IsSupported = true,
                Func = async (req, ctx) => await FetchUserSessionsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UserSessionsMasterCommand.validateUserExistence,
                Name = "validateUserExistence",
                IsSupported = true,
                Func = async (req, ctx) => await ValidateUserExistenceAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UserSessionsMasterCommand.forceSessionLogoutMaster,
                Name = "forceSessionLogoutMaster",
                IsSupported = true,
                Func = async (req, ctx) => await ForceSessionLogoutMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UserSessionsMasterCommand.refreshQosPingSiteMapMaster,
                Name = "refreshQosPingSiteMapMaster",
                IsSupported = true,
                Func = async (req, ctx) => await RefreshQosPingSiteMapMasterAsync(req, ctx).ConfigureAwait(false)
            });

        }

        public override string GetErrorName(ushort errorCode)
        {
            return ((Error)errorCode).ToString();
        }

        // ===== Slave command handlers =====

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::validateSessionKey</b> command.<br/>
        /// Request type: <see cref="ValidateSessionKeyRequest"/><br/>
        /// Response type: <see cref="SessionInfo"/><br/>
        /// </summary>
        public virtual Task<SessionInfo> ValidateSessionKeyAsync(ValidateSessionKeyRequest request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::fetchExtendedData</b> command.<br/>
        /// Request type: <see cref="FetchExtendedDataRequest"/><br/>
        /// Response type: <see cref="UserSessionExtendedData"/><br/>
        /// </summary>
        public virtual Task<UserSessionExtendedData> FetchExtendedDataAsync(FetchExtendedDataRequest request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::updateExtendedDataAttribute</b> command.<br/>
        /// Request type: <see cref="UpdateExtendedDataAttributeRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> UpdateExtendedDataAttributeAsync(UpdateExtendedDataAttributeRequest request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::updateHardwareFlags</b> command.<br/>
        /// Request type: <see cref="UpdateHardwareFlagsRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> UpdateHardwareFlagsAsync(UpdateHardwareFlagsRequest request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::getPermissions</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> GetPermissionsAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::getAccessGroup</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> GetAccessGroupAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::lookupUser</b> command.<br/>
        /// Request type: <see cref="UserIdentification"/><br/>
        /// Response type: <see cref="UserData"/><br/>
        /// </summary>
        public virtual Task<UserData> LookupUserAsync(UserIdentification request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::lookupUsers</b> command.<br/>
        /// Request type: <see cref="LookupUsersRequest"/><br/>
        /// Response type: <see cref="UserDataResponse"/><br/>
        /// </summary>
        public virtual Task<UserDataResponse> LookupUsersAsync(LookupUsersRequest request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::lookupUsersByPrefix</b> command.<br/>
        /// Request type: <see cref="LookupUsersByPrefixRequest"/><br/>
        /// Response type: <see cref="UserDataResponse"/><br/>
        /// </summary>
        public virtual Task<UserDataResponse> LookupUsersByPrefixAsync(LookupUsersByPrefixRequest request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::lookupUsersIdentification</b> command.<br/>
        /// Request type: <see cref="LookupUsersRequest"/><br/>
        /// Response type: <see cref="LookupUsersResponse"/><br/>
        /// </summary>
        public virtual Task<LookupUsersResponse> LookupUsersIdentificationAsync(LookupUsersRequest request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::updateNetworkInfo</b> command.<br/>
        /// Request type: <see cref="UpdateNetworkInfoRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> UpdateNetworkInfoAsync(UpdateNetworkInfoRequest request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::listDefaultAccessGroup</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> ListDefaultAccessGroupAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::listAuthorization</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> ListAuthorizationAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::lookupUserGeoIPData</b> command.<br/>
        /// Request type: <see cref="UserIdentification"/><br/>
        /// Response type: <see cref="GeoLocationData"/><br/>
        /// </summary>
        public virtual Task<GeoLocationData> LookupUserGeoIPDataAsync(UserIdentification request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::overrideUserGeoIPData</b> command.<br/>
        /// Request type: <see cref="GeoLocationData"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> OverrideUserGeoIPDataAsync(GeoLocationData request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::updateUserSessionClientData</b> command.<br/>
        /// Request type: <see cref="UpdateUserSessionClientDataRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> UpdateUserSessionClientDataAsync(UpdateUserSessionClientDataRequest request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::setUserInfoAttribute</b> command.<br/>
        /// Request type: <see cref="SetUserInfoAttributeRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> SetUserInfoAttributeAsync(SetUserInfoAttributeRequest request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::resetUserGeoIPData</b> command.<br/>
        /// Request type: <see cref="ResetUserGeoIPDataRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> ResetUserGeoIPDataAsync(ResetUserGeoIPDataRequest request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::lookupEntityByName</b> command.<br/>
        /// Request type: <see cref="EntityLookupRequest"/><br/>
        /// Response type: <see cref="EntityLookupResponse"/><br/>
        /// </summary>
        public virtual Task<EntityLookupResponse> LookupEntityByNameAsync(EntityLookupRequest request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::lookupEntityById</b> command.<br/>
        /// Request type: <see cref="EntityLookupByIdRequest"/><br/>
        /// Response type: <see cref="EntityLookupByIdResponse"/><br/>
        /// </summary>
        public virtual Task<EntityLookupByIdResponse> LookupEntityByIdAsync(EntityLookupByIdRequest request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::lookupEntitiesByIds</b> command.<br/>
        /// Request type: <see cref="EntitiesLookupByIdsRequest"/><br/>
        /// Response type: <see cref="EntitiesLookupByIdsResponse"/><br/>
        /// </summary>
        public virtual Task<EntitiesLookupByIdsResponse> LookupEntitiesByIdsAsync(EntitiesLookupByIdsRequest request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::registerRemoteSlaveSession</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> RegisterRemoteSlaveSessionAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::lookupUserSessionId</b> command.<br/>
        /// Request type: <see cref="LookupUserSessionIdRequest"/><br/>
        /// Response type: <see cref="LookupUserSessionIdResponse"/><br/>
        /// </summary>
        public virtual Task<LookupUserSessionIdResponse> LookupUserSessionIdAsync(LookupUserSessionIdRequest request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::fetchLastLocaleUsedAndAuthError</b> command.<br/>
        /// Request type: <see cref="FetchLastLocaleUsedAndAuthErrorRequest"/><br/>
        /// Response type: <see cref="FetchLastLocaleUsedAndAuthErrorResponse"/><br/>
        /// </summary>
        public virtual Task<FetchLastLocaleUsedAndAuthErrorResponse> FetchLastLocaleUsedAndAuthErrorAsync(FetchLastLocaleUsedAndAuthErrorRequest request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::fetchUserFirstLastAuthTime</b> command.<br/>
        /// Request type: <see cref="FetchUserFirstLastAuthTimeRequest"/><br/>
        /// Response type: <see cref="FetchUserFirstLastAuthTimeResponse"/><br/>
        /// </summary>
        public virtual Task<FetchUserFirstLastAuthTimeResponse> FetchUserFirstLastAuthTimeAsync(FetchUserFirstLastAuthTimeRequest request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::listAllPermissions</b> command.<br/>
        /// Request type: <see cref="ListAllPermissionsRequest"/><br/>
        /// Response type: <see cref="ListAllPermissionsResponse"/><br/>
        /// </summary>
        public virtual Task<ListAllPermissionsResponse> ListAllPermissionsAsync(ListAllPermissionsRequest request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::setUserGeoOptIn</b> command.<br/>
        /// Request type: <see cref="OptInRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> SetUserGeoOptInAsync(OptInRequest request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::remoteLoadUserExtendedData</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="UserSessionExtendedData"/><br/>
        /// </summary>
        public virtual Task<UserSessionExtendedData> RemoteLoadUserExtendedDataAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::remoteRefreshUserExtendedData</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="UserSessionExtendedData"/><br/>
        /// </summary>
        public virtual Task<UserSessionExtendedData> RemoteRefreshUserExtendedDataAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::forceSessionLogout</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> ForceSessionLogoutAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::enableUserAuditLogging</b> command.<br/>
        /// Request type: <see cref="EnableUserAuditLoggingRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> EnableUserAuditLoggingAsync(EnableUserAuditLoggingRequest request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::disableUserAuditLogging</b> command.<br/>
        /// Request type: <see cref="DisableUserAuditLoggingRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> DisableUserAuditLoggingAsync(DisableUserAuditLoggingRequest request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::requestUserExtendedDataProviderRegistration</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> RequestUserExtendedDataProviderRegistrationAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::getUserGeoIpData</b> command.<br/>
        /// Request type: <see cref="GetUserGeoIpDataRequest"/><br/>
        /// Response type: <see cref="GeoLocationData"/><br/>
        /// </summary>
        public virtual Task<GeoLocationData> GetUserGeoIpDataAsync(GetUserGeoIpDataRequest request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::getUniqueDeviceId</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="GetUniqueDeviceIdResponse"/><br/>
        /// </summary>
        public virtual Task<GetUniqueDeviceIdResponse> GetUniqueDeviceIdAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::getIpFilterByName</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> GetIpFilterByNameAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::lookupUsersByPrefixMultiNamespace</b> command.<br/>
        /// Request type: <see cref="LookupUsersByPrefixMultiNamespaceRequest"/><br/>
        /// Response type: <see cref="UserDataResponse"/><br/>
        /// </summary>
        public virtual Task<UserDataResponse> LookupUsersByPrefixMultiNamespaceAsync(LookupUsersByPrefixMultiNamespaceRequest request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::lookupUsersByPersonaNameMultiNamespace</b> command.<br/>
        /// Request type: <see cref="LookupUsersByPersonaNameMultiNamespaceRequest"/><br/>
        /// Response type: <see cref="UserDataResponse"/><br/>
        /// </summary>
        public virtual Task<UserDataResponse> LookupUsersByPersonaNameMultiNamespaceAsync(LookupUsersByPersonaNameMultiNamespaceRequest request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::lookupUsersByPersonaNamesMultiNamespace</b> command.<br/>
        /// Request type: <see cref="LookupUsersByPersonaNamesMultiNamespaceRequest"/><br/>
        /// Response type: <see cref="UserDataResponse"/><br/>
        /// </summary>
        public virtual Task<UserDataResponse> LookupUsersByPersonaNamesMultiNamespaceAsync(LookupUsersByPersonaNamesMultiNamespaceRequest request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::lookupUsersByPersonaNames</b> command.<br/>
        /// Request type: <see cref="LookupUsersByPersonaNamesRequest"/><br/>
        /// Response type: <see cref="UserDataResponse"/><br/>
        /// </summary>
        public virtual Task<UserDataResponse> LookupUsersByPersonaNamesAsync(LookupUsersByPersonaNamesRequest request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::checkConnectivity</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> CheckConnectivityAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::forceOwnSessionLogout</b> command.<br/>
        /// Request type: <see cref="ForceOwnSessionLogoutRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> ForceOwnSessionLogoutAsync(ForceOwnSessionLogoutRequest request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::updateLocalUserGroup</b> command.<br/>
        /// Request type: <see cref="UpdateLocalUserGroupRequest"/><br/>
        /// Response type: <see cref="UpdateLocalUserGroupResponse"/><br/>
        /// </summary>
        public virtual Task<UpdateLocalUserGroupResponse> UpdateLocalUserGroupAsync(UpdateLocalUserGroupRequest request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::fetchUsersAuditState</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> FetchUsersAuditStateAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::refreshQosPingSiteMap</b> command.<br/>
        /// Request type: <see cref="RefreshQosPingSiteMapRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> RefreshQosPingSiteMapAsync(RefreshQosPingSiteMapRequest request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::getQosPingSites</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="QosPingSitesResponse"/><br/>
        /// </summary>
        public virtual Task<QosPingSitesResponse> GetQosPingSitesAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::setUserCrossPlatformOptIn</b> command.<br/>
        /// Request type: <see cref="OptInRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> SetUserCrossPlatformOptInAsync(OptInRequest request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::lookupUsersCrossPlatform</b> command.<br/>
        /// Request type: <see cref="LookupUsersCrossPlatformRequest"/><br/>
        /// Response type: <see cref="UserDataResponse"/><br/>
        /// </summary>
        public virtual Task<UserDataResponse> LookupUsersCrossPlatformAsync(LookupUsersCrossPlatformRequest request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::getUedInformationMap</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> GetUedInformationMapAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        // ===== Master command handlers =====

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::getCensusData</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="UserManagerCensusData"/><br/>
        /// </summary>
        public virtual Task<UserManagerCensusData> GetCensusDataAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::updateExtendedDataMaster</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> UpdateExtendedDataMasterAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::updateBlazeObjectIdList</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> UpdateBlazeObjectIdListAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::updateHardwareFlagsMaster</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> UpdateHardwareFlagsMasterAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::updateNetworkInfoMaster</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> UpdateNetworkInfoMasterAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::updateGeoDataMaster</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> UpdateGeoDataMasterAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::updateUserSessionClientDataMaster</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> UpdateUserSessionClientDataMasterAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::getUserExtendedData</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> GetUserExtendedDataAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::getUserInfoData</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> GetUserInfoDataAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::updateUserSessionServerAttributeMaster</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> UpdateUserSessionServerAttributeMasterAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::getUserSessionData</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> GetUserSessionDataAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::insertUniqueDeviceIdMaster</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> InsertUniqueDeviceIdMasterAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::updateDirtySockUserIndexMaster</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> UpdateDirtySockUserIndexMasterAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::getUserIpAddress</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> GetUserIpAddressAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::addUserSessionSubscriber</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> AddUserSessionSubscriberAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::removeUserSessionSubscriber</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> RemoveUserSessionSubscriberAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::fetchUserSessions</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> FetchUserSessionsAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::validateUserExistence</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> ValidateUserExistenceAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::forceSessionLogoutMaster</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> ForceSessionLogoutMasterAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UserSessionsComponent::refreshQosPingSiteMapMaster</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> RefreshQosPingSiteMapMasterAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new UserSessionsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        // ===== Notification helpers =====

        /// <summary>
        /// Call this method when you want to send to client a <b>UserSessionsComponent::UserSessionExtendedDataUpdate</b> notification.<br/>
        /// Notification type: <see cref="UserSessionExtendedDataUpdate"/><br/>
        /// </summary>
        public static Task NotifyUserSessionExtendedDataUpdateAsync(BlazeRpcConnection connection, UserSessionExtendedDataUpdate notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = UserSessionsBase.Id;
                frame.Command = (ushort)UserSessionsNotification.UserSessionExtendedDataUpdate;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>UserSessionsComponent::UserAdded</b> notification.<br/>
        /// Notification type: <see cref="NotifyUserAdded"/><br/>
        /// </summary>
        public static Task NotifyUserAddedAsync(BlazeRpcConnection connection, NotifyUserAdded notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = UserSessionsBase.Id;
                frame.Command = (ushort)UserSessionsNotification.UserAdded;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>UserSessionsComponent::UserRemoved</b> notification.<br/>
        /// Notification type: <see cref="NotifyUserRemoved"/><br/>
        /// </summary>
        public static Task NotifyUserRemovedAsync(BlazeRpcConnection connection, NotifyUserRemoved notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = UserSessionsBase.Id;
                frame.Command = (ushort)UserSessionsNotification.UserRemoved;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>UserSessionsComponent::UserUpdated</b> notification.<br/>
        /// Notification type: <see cref="UserStatus"/><br/>
        /// </summary>
        public static Task NotifyUserUpdatedAsync(BlazeRpcConnection connection, UserStatus notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = UserSessionsBase.Id;
                frame.Command = (ushort)UserSessionsNotification.UserUpdated;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>UserSessionsComponent::UserSubscriptionsUpdated</b> notification.<br/>
        /// Notification type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public static Task NotifyUserSubscriptionsUpdatedAsync(BlazeRpcConnection connection, EmptyMessage notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = UserSessionsBase.Id;
                frame.Command = (ushort)UserSessionsNotification.UserSubscriptionsUpdated;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>UserSessionsComponent::SessionSubscriptionsUpdated</b> notification.<br/>
        /// Notification type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public static Task NotifySessionSubscriptionsUpdatedAsync(BlazeRpcConnection connection, EmptyMessage notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = UserSessionsBase.Id;
                frame.Command = (ushort)UserSessionsNotification.SessionSubscriptionsUpdated;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>UserSessionsComponent::UserAuthenticated</b> notification.<br/>
        /// Notification type: <see cref="UserSessionLoginInfo"/><br/>
        /// </summary>
        public static Task NotifyUserAuthenticatedAsync(BlazeRpcConnection connection, UserSessionLoginInfo notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = UserSessionsBase.Id;
                frame.Command = (ushort)UserSessionsNotification.UserAuthenticated;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>UserSessionsComponent::UserUnauthenticated</b> notification.<br/>
        /// Notification type: <see cref="UserSessionLogoutInfo"/><br/>
        /// </summary>
        public static Task NotifyUserUnauthenticatedAsync(BlazeRpcConnection connection, UserSessionLogoutInfo notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = UserSessionsBase.Id;
                frame.Command = (ushort)UserSessionsNotification.UserUnauthenticated;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>UserSessionsComponent::NotifyUserInfoUpdated</b> notification.<br/>
        /// Notification type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public static Task NotifyUserInfoUpdatedAsync(BlazeRpcConnection connection, EmptyMessage notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = UserSessionsBase.Id;
                frame.Command = (ushort)UserSessionsNotification.NotifyUserInfoUpdated;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>UserSessionsComponent::NotifyQosSettingsUpdated</b> notification.<br/>
        /// Notification type: <see cref="QosConfigInfo"/><br/>
        /// </summary>
        public static Task NotifyQosSettingsUpdatedAsync(BlazeRpcConnection connection, QosConfigInfo notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = UserSessionsBase.Id;
                frame.Command = (ushort)UserSessionsNotification.NotifyQosSettingsUpdated;
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
        /// Call this method when you want to send a <b>UserSessionsComponent::LoginEvent</b> event.<br/>
        /// Event type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public static Task NotifyLoginEventAsync(BlazeRpcConnection connection, EmptyMessage notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = UserSessionsBase.Id;
                frame.Command = (ushort)UserSessionsEvent.LoginEvent;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send a <b>UserSessionsComponent::LoginFailureEvent</b> event.<br/>
        /// Event type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public static Task NotifyLoginFailureEventAsync(BlazeRpcConnection connection, EmptyMessage notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = UserSessionsBase.Id;
                frame.Command = (ushort)UserSessionsEvent.LoginFailureEvent;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send a <b>UserSessionsComponent::LogoutEvent</b> event.<br/>
        /// Event type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public static Task NotifyLogoutEventAsync(BlazeRpcConnection connection, EmptyMessage notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = UserSessionsBase.Id;
                frame.Command = (ushort)UserSessionsEvent.LogoutEvent;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send a <b>UserSessionsComponent::DisconnectEvent</b> event.<br/>
        /// Event type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public static Task NotifyDisconnectEventAsync(BlazeRpcConnection connection, EmptyMessage notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = UserSessionsBase.Id;
                frame.Command = (ushort)UserSessionsEvent.DisconnectEvent;
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
