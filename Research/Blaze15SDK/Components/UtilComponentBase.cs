using Blaze.Core;
using Blaze15SDK.Blaze.Util;
using EATDF;
using EATDF.Types;

namespace Blaze15SDK.Components;

public static class UtilComponentBase
{
    public const ushort Id = 9;
    public const string Name = "UtilComponent";

    public enum Error : ushort
    {
        UTIL_CONFIG_NOT_FOUND = 100,
        UTIL_PSU_LIMIT_EXCEEDED = 101,
        UTIL_SERVICENAME_NOT_SPECIFIED = 102,
        UTIL_SERVICENAME_NOT_HOSTED = 103,
        UTIL_CALLER_PLATFORM_NOT_FOUND = 104,
        UTIL_CALLER_PLATFORM_MISMATCH = 105,
        UTIL_CALLER_PLATFORM_NOT_ALLOWED = 106,
        UTIL_TELEMETRY_NO_SERVERS_AVAILABLE = 150,
        UTIL_TELEMETRY_OUT_OF_MEMORY = 151,
        UTIL_TELEMETRY_KEY_TOO_LONG = 152,
        UTIL_TICKER_NO_SERVERS_AVAILABLE = 155,
        UTIL_TICKER_KEY_TOO_LONG = 156,
        UTIL_USS_RECORD_NOT_FOUND = 200,
        UTIL_USS_TOO_MANY_KEYS = 201,
        UTIL_USS_DB_ERROR = 202,
        UTIL_USS_USER_NO_EXTENDED_DATA = 250,
        UTIL_SUSPEND_PING_TIME_TOO_LARGE = 300,
        UTIL_SUSPEND_PING_TIME_TOO_SMALL = 301,
    }

    public enum UtilCommand : ushort
    {
        fetchClientConfig = 1,
        ping = 2,
        setClientData = 3,
        localizeStrings = 4,
        getTelemetryServer = 5,
        getTickerServer = 6,
        preAuth = 7,
        postAuth = 8,
        userSettingsLoad = 10,
        userSettingsSave = 11,
        userSettingsLoadAll = 12,
        userSettingsLoadAllForUserId = 13,
        deleteUserSettings = 14,
        userSettingsLoadMultiple = 15,
        filterForProfanity = 20,
        fetchQosConfig = 21,
        setClientMetrics = 22,
        setConnectionState = 23,
        getUserOptions = 25,
        setUserOptions = 26,
        suspendUserPing = 27,
        setClientState = 28,
        setClientUserMetrics = 29,
    }

    public enum UtilNotification : ushort
    {
    }

    public enum UtilEvent : ushort
    {
        UserSettingsSavedEvent = 1000,
    }

    public class Server : BlazeComponent
    {
        public override ushort Id => UtilComponentBase.Id;
        public override string Name => UtilComponentBase.Name;

        public virtual bool IsCommandSupported(UtilCommand command) => false;

        public class UtilException : BlazeRpcException
        {
            public UtilException(Error error) : base((ushort)error, null) { }
            public UtilException(ServerError error) : base(error.WithErrorPrefix(), null) { }
            public UtilException(Error error, Tdf? errorResponse) : base((ushort)error, errorResponse) { }
            public UtilException(ServerError error, Tdf? errorResponse) : base(error.WithErrorPrefix(), errorResponse) { }
            public UtilException(Error error, Tdf? errorResponse, string? message) : base((ushort)error, errorResponse, message) { }
            public UtilException(ServerError error, Tdf? errorResponse, string? message) : base(error.WithErrorPrefix(), errorResponse, message) { }
            public UtilException(Error error, Tdf? errorResponse, string? message, Exception? innerException) : base((ushort)error, errorResponse, message, innerException) { }
            public UtilException(ServerError error, Tdf? errorResponse, string? message, Exception? innerException) : base(error.WithErrorPrefix(), errorResponse, message, innerException) { }
        }

        public Server()
        {
            // Slave commands
            RegisterCommand(new RpcCommandFunc<FetchClientConfigRequest, FetchConfigResponse, EmptyMessage>()
            {
                Id = (ushort)UtilCommand.fetchClientConfig,
                Name = "fetchClientConfig",
                IsSupported = IsCommandSupported(UtilCommand.fetchClientConfig),
                Func = async (req, ctx) => await FetchClientConfigAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, PingResponse, EmptyMessage>()
            {
                Id = (ushort)UtilCommand.ping,
                Name = "ping",
                IsSupported = IsCommandSupported(UtilCommand.ping),
                Func = async (req, ctx) => await PingAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<ClientData, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UtilCommand.setClientData,
                Name = "setClientData",
                IsSupported = IsCommandSupported(UtilCommand.setClientData),
                Func = async (req, ctx) => await SetClientDataAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<LocalizeStringsRequest, LocalizeStringsResponse, EmptyMessage>()
            {
                Id = (ushort)UtilCommand.localizeStrings,
                Name = "localizeStrings",
                IsSupported = IsCommandSupported(UtilCommand.localizeStrings),
                Func = async (req, ctx) => await LocalizeStringsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetTelemetryServerRequest, GetTelemetryServerResponse, EmptyMessage>()
            {
                Id = (ushort)UtilCommand.getTelemetryServer,
                Name = "getTelemetryServer",
                IsSupported = IsCommandSupported(UtilCommand.getTelemetryServer),
                Func = async (req, ctx) => await GetTelemetryServerAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, GetTickerServerResponse, EmptyMessage>()
            {
                Id = (ushort)UtilCommand.getTickerServer,
                Name = "getTickerServer",
                IsSupported = IsCommandSupported(UtilCommand.getTickerServer),
                Func = async (req, ctx) => await GetTickerServerAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<PreAuthRequest, PreAuthResponse, EmptyMessage>()
            {
                Id = (ushort)UtilCommand.preAuth,
                Name = "preAuth",
                IsSupported = IsCommandSupported(UtilCommand.preAuth),
                Func = async (req, ctx) => await PreAuthAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<PostAuthRequest, PostAuthResponse, EmptyMessage>()
            {
                Id = (ushort)UtilCommand.postAuth,
                Name = "postAuth",
                IsSupported = IsCommandSupported(UtilCommand.postAuth),
                Func = async (req, ctx) => await PostAuthAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UserSettingsLoadRequest, UserSettingsResponse, EmptyMessage>()
            {
                Id = (ushort)UtilCommand.userSettingsLoad,
                Name = "userSettingsLoad",
                IsSupported = IsCommandSupported(UtilCommand.userSettingsLoad),
                Func = async (req, ctx) => await UserSettingsLoadAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UserSettingsSaveRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UtilCommand.userSettingsSave,
                Name = "userSettingsSave",
                IsSupported = IsCommandSupported(UtilCommand.userSettingsSave),
                Func = async (req, ctx) => await UserSettingsSaveAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, UserSettingsLoadAllResponse, EmptyMessage>()
            {
                Id = (ushort)UtilCommand.userSettingsLoadAll,
                Name = "userSettingsLoadAll",
                IsSupported = IsCommandSupported(UtilCommand.userSettingsLoadAll),
                Func = async (req, ctx) => await UserSettingsLoadAllAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UserSettingsLoadAllRequest, UserSettingsLoadAllResponse, EmptyMessage>()
            {
                Id = (ushort)UtilCommand.userSettingsLoadAllForUserId,
                Name = "userSettingsLoadAllForUserId",
                IsSupported = IsCommandSupported(UtilCommand.userSettingsLoadAllForUserId),
                Func = async (req, ctx) => await UserSettingsLoadAllForUserIdAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<DeleteUserSettingsRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UtilCommand.deleteUserSettings,
                Name = "deleteUserSettings",
                IsSupported = IsCommandSupported(UtilCommand.deleteUserSettings),
                Func = async (req, ctx) => await DeleteUserSettingsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UserSettingsLoadMultipleRequest, UserSettingsLoadMultipleResponse, EmptyMessage>()
            {
                Id = (ushort)UtilCommand.userSettingsLoadMultiple,
                Name = "userSettingsLoadMultiple",
                IsSupported = IsCommandSupported(UtilCommand.userSettingsLoadMultiple),
                Func = async (req, ctx) => await UserSettingsLoadMultipleAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<FilterUserTextResponse, FilterUserTextResponse, EmptyMessage>()
            {
                Id = (ushort)UtilCommand.filterForProfanity,
                Name = "filterForProfanity",
                IsSupported = IsCommandSupported(UtilCommand.filterForProfanity),
                Func = async (req, ctx) => await FilterForProfanityAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, Blaze15SDK.Blaze.QosConfigInfo, EmptyMessage>()
            {
                Id = (ushort)UtilCommand.fetchQosConfig,
                Name = "fetchQosConfig",
                IsSupported = IsCommandSupported(UtilCommand.fetchQosConfig),
                Func = async (req, ctx) => await FetchQosConfigAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<Blaze15SDK.Blaze.ClientMetrics, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UtilCommand.setClientMetrics,
                Name = "setClientMetrics",
                IsSupported = IsCommandSupported(UtilCommand.setClientMetrics),
                Func = async (req, ctx) => await SetClientMetricsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<SetConnectionStateRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UtilCommand.setConnectionState,
                Name = "setConnectionState",
                IsSupported = IsCommandSupported(UtilCommand.setConnectionState),
                Func = async (req, ctx) => await SetConnectionStateAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetUserOptionsRequest, UserOptions, EmptyMessage>()
            {
                Id = (ushort)UtilCommand.getUserOptions,
                Name = "getUserOptions",
                IsSupported = IsCommandSupported(UtilCommand.getUserOptions),
                Func = async (req, ctx) => await GetUserOptionsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UserOptions, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UtilCommand.setUserOptions,
                Name = "setUserOptions",
                IsSupported = IsCommandSupported(UtilCommand.setUserOptions),
                Func = async (req, ctx) => await SetUserOptionsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<SuspendUserPingRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UtilCommand.suspendUserPing,
                Name = "suspendUserPing",
                IsSupported = IsCommandSupported(UtilCommand.suspendUserPing),
                Func = async (req, ctx) => await SuspendUserPingAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<Blaze15SDK.Blaze.ClientState, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UtilCommand.setClientState,
                Name = "setClientState",
                IsSupported = IsCommandSupported(UtilCommand.setClientState),
                Func = async (req, ctx) => await SetClientStateAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<Blaze15SDK.Blaze.ClientUserMetrics, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)UtilCommand.setClientUserMetrics,
                Name = "setClientUserMetrics",
                IsSupported = IsCommandSupported(UtilCommand.setClientUserMetrics),
                Func = async (req, ctx) => await SetClientUserMetricsAsync(req, ctx).ConfigureAwait(false)
            });

        }

        public override string GetErrorName(ushort errorCode)
        {
            return ((Error)errorCode).ToString();
        }

        // ===== Slave command handlers =====

        /// <summary>
        /// This method is called when server receives a <b>UtilComponent::fetchClientConfig</b> command.<br/>
        /// Request type: <see cref="FetchClientConfigRequest"/><br/>
        /// Response type: <see cref="FetchConfigResponse"/><br/>
        /// </summary>
        public virtual Task<FetchConfigResponse> FetchClientConfigAsync(FetchClientConfigRequest request, BlazeRpcContext context)
        {
            throw new UtilException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UtilComponent::ping</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="PingResponse"/><br/>
        /// </summary>
        public virtual Task<PingResponse> PingAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new UtilException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UtilComponent::setClientData</b> command.<br/>
        /// Request type: <see cref="ClientData"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> SetClientDataAsync(ClientData request, BlazeRpcContext context)
        {
            throw new UtilException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UtilComponent::localizeStrings</b> command.<br/>
        /// Request type: <see cref="LocalizeStringsRequest"/><br/>
        /// Response type: <see cref="LocalizeStringsResponse"/><br/>
        /// </summary>
        public virtual Task<LocalizeStringsResponse> LocalizeStringsAsync(LocalizeStringsRequest request, BlazeRpcContext context)
        {
            throw new UtilException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UtilComponent::getTelemetryServer</b> command.<br/>
        /// Request type: <see cref="GetTelemetryServerRequest"/><br/>
        /// Response type: <see cref="GetTelemetryServerResponse"/><br/>
        /// </summary>
        public virtual Task<GetTelemetryServerResponse> GetTelemetryServerAsync(GetTelemetryServerRequest request, BlazeRpcContext context)
        {
            throw new UtilException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UtilComponent::getTickerServer</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="GetTickerServerResponse"/><br/>
        /// </summary>
        public virtual Task<GetTickerServerResponse> GetTickerServerAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new UtilException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UtilComponent::preAuth</b> command.<br/>
        /// Request type: <see cref="PreAuthRequest"/><br/>
        /// Response type: <see cref="PreAuthResponse"/><br/>
        /// </summary>
        public virtual Task<PreAuthResponse> PreAuthAsync(PreAuthRequest request, BlazeRpcContext context)
        {
            throw new UtilException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UtilComponent::postAuth</b> command.<br/>
        /// Request type: <see cref="PostAuthRequest"/><br/>
        /// Response type: <see cref="PostAuthResponse"/><br/>
        /// </summary>
        public virtual Task<PostAuthResponse> PostAuthAsync(PostAuthRequest request, BlazeRpcContext context)
        {
            throw new UtilException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UtilComponent::userSettingsLoad</b> command.<br/>
        /// Request type: <see cref="UserSettingsLoadRequest"/><br/>
        /// Response type: <see cref="UserSettingsResponse"/><br/>
        /// </summary>
        public virtual Task<UserSettingsResponse> UserSettingsLoadAsync(UserSettingsLoadRequest request, BlazeRpcContext context)
        {
            throw new UtilException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UtilComponent::userSettingsSave</b> command.<br/>
        /// Request type: <see cref="UserSettingsSaveRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> UserSettingsSaveAsync(UserSettingsSaveRequest request, BlazeRpcContext context)
        {
            throw new UtilException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UtilComponent::userSettingsLoadAll</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="UserSettingsLoadAllResponse"/><br/>
        /// </summary>
        public virtual Task<UserSettingsLoadAllResponse> UserSettingsLoadAllAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new UtilException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UtilComponent::userSettingsLoadAllForUserId</b> command.<br/>
        /// Request type: <see cref="UserSettingsLoadAllRequest"/><br/>
        /// Response type: <see cref="UserSettingsLoadAllResponse"/><br/>
        /// </summary>
        public virtual Task<UserSettingsLoadAllResponse> UserSettingsLoadAllForUserIdAsync(UserSettingsLoadAllRequest request, BlazeRpcContext context)
        {
            throw new UtilException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UtilComponent::deleteUserSettings</b> command.<br/>
        /// Request type: <see cref="DeleteUserSettingsRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> DeleteUserSettingsAsync(DeleteUserSettingsRequest request, BlazeRpcContext context)
        {
            throw new UtilException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UtilComponent::userSettingsLoadMultiple</b> command.<br/>
        /// Request type: <see cref="UserSettingsLoadMultipleRequest"/><br/>
        /// Response type: <see cref="UserSettingsLoadMultipleResponse"/><br/>
        /// </summary>
        public virtual Task<UserSettingsLoadMultipleResponse> UserSettingsLoadMultipleAsync(UserSettingsLoadMultipleRequest request, BlazeRpcContext context)
        {
            throw new UtilException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UtilComponent::filterForProfanity</b> command.<br/>
        /// Request type: <see cref="FilterUserTextResponse"/><br/>
        /// Response type: <see cref="FilterUserTextResponse"/><br/>
        /// </summary>
        public virtual Task<FilterUserTextResponse> FilterForProfanityAsync(FilterUserTextResponse request, BlazeRpcContext context)
        {
            throw new UtilException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UtilComponent::fetchQosConfig</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="Blaze15SDK.Blaze.QosConfigInfo"/><br/>
        /// </summary>
        public virtual Task<Blaze15SDK.Blaze.QosConfigInfo> FetchQosConfigAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new UtilException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UtilComponent::setClientMetrics</b> command.<br/>
        /// Request type: <see cref="Blaze15SDK.Blaze.ClientMetrics"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> SetClientMetricsAsync(Blaze15SDK.Blaze.ClientMetrics request, BlazeRpcContext context)
        {
            throw new UtilException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UtilComponent::setConnectionState</b> command.<br/>
        /// Request type: <see cref="SetConnectionStateRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> SetConnectionStateAsync(SetConnectionStateRequest request, BlazeRpcContext context)
        {
            throw new UtilException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UtilComponent::getUserOptions</b> command.<br/>
        /// Request type: <see cref="GetUserOptionsRequest"/><br/>
        /// Response type: <see cref="UserOptions"/><br/>
        /// </summary>
        public virtual Task<UserOptions> GetUserOptionsAsync(GetUserOptionsRequest request, BlazeRpcContext context)
        {
            throw new UtilException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UtilComponent::setUserOptions</b> command.<br/>
        /// Request type: <see cref="UserOptions"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> SetUserOptionsAsync(UserOptions request, BlazeRpcContext context)
        {
            throw new UtilException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UtilComponent::suspendUserPing</b> command.<br/>
        /// Request type: <see cref="SuspendUserPingRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> SuspendUserPingAsync(SuspendUserPingRequest request, BlazeRpcContext context)
        {
            throw new UtilException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UtilComponent::setClientState</b> command.<br/>
        /// Request type: <see cref="Blaze15SDK.Blaze.ClientState"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> SetClientStateAsync(Blaze15SDK.Blaze.ClientState request, BlazeRpcContext context)
        {
            throw new UtilException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>UtilComponent::setClientUserMetrics</b> command.<br/>
        /// Request type: <see cref="Blaze15SDK.Blaze.ClientUserMetrics"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> SetClientUserMetricsAsync(Blaze15SDK.Blaze.ClientUserMetrics request, BlazeRpcContext context)
        {
            throw new UtilException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        // ===== Notification helpers =====

        // No slave notifications for this component.

        // ===== Event helpers =====

        /// <summary>
        /// Call this method when you want to send a <b>UtilComponent::UserSettingsSavedEvent</b> event.<br/>
        /// Event type: <see cref="EmptyMessage"/> (server-only type UserSettingsSaved)<br/>
        /// </summary>
        public static Task NotifyUserSettingsSavedEventAsync(BlazeRpcConnection connection, EmptyMessage notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = UtilComponentBase.Id;
                frame.Command = (ushort)UtilEvent.UserSettingsSavedEvent;
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
