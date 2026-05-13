using Blaze.Core;
using Blaze15SDK.Blaze.Redirector;
using EATDF;
using EATDF.Types;

namespace Blaze15SDK.Components;

public static class RedirectorComponentBase
{
    public const ushort Id = 5;
    public const string Name = "RedirectorComponent";

    public enum Error : ushort
    {
        REDIRECTOR_SERVER_NOT_FOUND = 1,
        REDIRECTOR_NO_SERVER_CAPACITY = 2,
        REDIRECTOR_NO_MATCHING_INSTANCE = 3,
        REDIRECTOR_SERVER_NAME_ALREADY_IN_USE = 4,
        REDIRECTOR_CLIENT_NOT_COMPATIBLE = 5,
        REDIRECTOR_CLIENT_UNKNOWN = 6,
        REDIRECTOR_UNKNOWN_CONNECTION_PROFILE = 7,
        REDIRECTOR_SERVER_SUNSET = 8,
        REDIRECTOR_SERVER_DOWN = 9,
        REDIRECTOR_INVALID_PARAMETER = 10,
        REDIRECTOR_UNKNOWN_SERVICE_NAME = 11,
        REDIRECTOR_PAST_EVENT = 12,
        REDIRECTOR_UNKNOWN_SCHEDULE_ID = 13,
        REDIRECTOR_MISSING_SERVICE_NAME = 14,
    }

    public enum RedirectorCommand : ushort
    {
        getServerInstance = 1,
        getServerAddressMap = 7,
        getCACertificates = 9,
        findCACertificates = 10,
        publishServerInstanceInfo = 11,
        updateServerInstanceInfo = 12,
        getPeerServerAddress = 14,
        getCAResolverMetrics = 15,
        purgeServerRegistration = 16,
        getServerInstanceHttp = 17,
    }

    public enum RedirectorMasterCommand : ushort
    {
        updateServerInfo = 1,
        purgeServerRegistrationMaster = 2,
    }

    public enum RedirectorNotification : ushort
    {
    }

    public enum RedirectorMasterNotification : ushort
    {
        NotifyPurgeServerRegistration = 1,
    }

    public enum RedirectorEvent : ushort
    {
        ServiceAddedEvent = 1000,
        ServiceRemovedEvent = 1001,
        InstanceAddedEvent = 1002,
        InstanceRemovedEvent = 1004,
    }

    public class Server : BlazeComponent
    {
        public override ushort Id => RedirectorComponentBase.Id;
        public override string Name => RedirectorComponentBase.Name;

        public virtual bool IsCommandSupported(RedirectorCommand command) => false;

        public class RedirectorException : BlazeRpcException
        {
            public RedirectorException(Error error) : base((ushort)error, null) { }
            public RedirectorException(ServerError error) : base(error.WithErrorPrefix(), null) { }
            public RedirectorException(Error error, Tdf? errorResponse) : base((ushort)error, errorResponse) { }
            public RedirectorException(ServerError error, Tdf? errorResponse) : base(error.WithErrorPrefix(), errorResponse) { }
            public RedirectorException(Error error, Tdf? errorResponse, string? message) : base((ushort)error, errorResponse, message) { }
            public RedirectorException(ServerError error, Tdf? errorResponse, string? message) : base(error.WithErrorPrefix(), errorResponse, message) { }
            public RedirectorException(Error error, Tdf? errorResponse, string? message, Exception? innerException) : base((ushort)error, errorResponse, message, innerException) { }
            public RedirectorException(ServerError error, Tdf? errorResponse, string? message, Exception? innerException) : base(error.WithErrorPrefix(), errorResponse, message, innerException) { }
        }

        public Server()
        {
            // Slave commands
            RegisterCommand(new RpcCommandFunc<ServerInstanceRequest, ServerInstanceInfo, ServerInstanceError>()
            {
                Id = (ushort)RedirectorCommand.getServerInstance,
                Name = "getServerInstance",
                IsSupported = IsCommandSupported(RedirectorCommand.getServerInstance),
                Func = async (req, ctx) => await GetServerInstanceAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)RedirectorCommand.getServerAddressMap,
                Name = "getServerAddressMap",
                IsSupported = IsCommandSupported(RedirectorCommand.getServerAddressMap),
                Func = async (req, ctx) => await GetServerAddressMapAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<CACertificateRequest, CACertificateResponse, EmptyMessage>()
            {
                Id = (ushort)RedirectorCommand.getCACertificates,
                Name = "getCACertificates",
                IsSupported = IsCommandSupported(RedirectorCommand.getCACertificates),
                Func = async (req, ctx) => await GetCACertificatesAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<X509MatchCriteria, CACertificateResponse, EmptyMessage>()
            {
                Id = (ushort)RedirectorCommand.findCACertificates,
                Name = "findCACertificates",
                IsSupported = IsCommandSupported(RedirectorCommand.findCACertificates),
                Func = async (req, ctx) => await FindCACertificatesAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)RedirectorCommand.publishServerInstanceInfo,
                Name = "publishServerInstanceInfo",
                IsSupported = IsCommandSupported(RedirectorCommand.publishServerInstanceInfo),
                Func = async (req, ctx) => await PublishServerInstanceInfoAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)RedirectorCommand.updateServerInstanceInfo,
                Name = "updateServerInstanceInfo",
                IsSupported = IsCommandSupported(RedirectorCommand.updateServerInstanceInfo),
                Func = async (req, ctx) => await UpdateServerInstanceInfoAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)RedirectorCommand.getPeerServerAddress,
                Name = "getPeerServerAddress",
                IsSupported = IsCommandSupported(RedirectorCommand.getPeerServerAddress),
                Func = async (req, ctx) => await GetPeerServerAddressAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)RedirectorCommand.getCAResolverMetrics,
                Name = "getCAResolverMetrics",
                IsSupported = IsCommandSupported(RedirectorCommand.getCAResolverMetrics),
                Func = async (req, ctx) => await GetCAResolverMetricsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)RedirectorCommand.purgeServerRegistration,
                Name = "purgeServerRegistration",
                IsSupported = IsCommandSupported(RedirectorCommand.purgeServerRegistration),
                Func = async (req, ctx) => await PurgeServerRegistrationAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<ServerInstanceRequest, ServerInstanceInfo, ServerInstanceError>()
            {
                Id = (ushort)RedirectorCommand.getServerInstanceHttp,
                Name = "getServerInstanceHttp",
                IsSupported = IsCommandSupported(RedirectorCommand.getServerInstanceHttp),
                Func = async (req, ctx) => await GetServerInstanceHttpAsync(req, ctx).ConfigureAwait(false)
            });

            // Master commands
            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)RedirectorMasterCommand.updateServerInfo,
                Name = "updateServerInfo",
                IsSupported = true,
                Func = async (req, ctx) => await UpdateServerInfoAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)RedirectorMasterCommand.purgeServerRegistrationMaster,
                Name = "purgeServerRegistrationMaster",
                IsSupported = true,
                Func = async (req, ctx) => await PurgeServerRegistrationMasterAsync(req, ctx).ConfigureAwait(false)
            });

        }

        public override string GetErrorName(ushort errorCode)
        {
            return ((Error)errorCode).ToString();
        }

        // ===== Slave command handlers =====

        /// <summary>
        /// This method is called when server receives a <b>RedirectorComponent::getServerInstance</b> command.<br/>
        /// Request type: <see cref="ServerInstanceRequest"/><br/>
        /// Response type: <see cref="ServerInstanceInfo"/><br/>
        /// Error response type: <see cref="ServerInstanceError"/><br/>
        /// </summary>
        public virtual Task<ServerInstanceInfo> GetServerInstanceAsync(ServerInstanceRequest request, BlazeRpcContext context)
        {
            throw new RedirectorException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>RedirectorComponent::getServerAddressMap</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/> (server-only type)<br/>
        /// Response type: <see cref="EmptyMessage"/> (server-only type)<br/>
        /// </summary>
        public virtual Task<EmptyMessage> GetServerAddressMapAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new RedirectorException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>RedirectorComponent::getCACertificates</b> command.<br/>
        /// Request type: <see cref="CACertificateRequest"/><br/>
        /// Response type: <see cref="CACertificateResponse"/><br/>
        /// </summary>
        public virtual Task<CACertificateResponse> GetCACertificatesAsync(CACertificateRequest request, BlazeRpcContext context)
        {
            throw new RedirectorException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>RedirectorComponent::findCACertificates</b> command.<br/>
        /// Request type: <see cref="X509MatchCriteria"/><br/>
        /// Response type: <see cref="CACertificateResponse"/><br/>
        /// </summary>
        public virtual Task<CACertificateResponse> FindCACertificatesAsync(X509MatchCriteria request, BlazeRpcContext context)
        {
            throw new RedirectorException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>RedirectorComponent::publishServerInstanceInfo</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/> (server-only type)<br/>
        /// Response type: <see cref="EmptyMessage"/> (server-only type)<br/>
        /// </summary>
        public virtual Task<EmptyMessage> PublishServerInstanceInfoAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new RedirectorException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>RedirectorComponent::updateServerInstanceInfo</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/> (server-only type)<br/>
        /// Response type: <see cref="EmptyMessage"/> (server-only type)<br/>
        /// </summary>
        public virtual Task<EmptyMessage> UpdateServerInstanceInfoAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new RedirectorException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>RedirectorComponent::getPeerServerAddress</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/> (server-only type)<br/>
        /// Response type: <see cref="EmptyMessage"/> (server-only type)<br/>
        /// </summary>
        public virtual Task<EmptyMessage> GetPeerServerAddressAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new RedirectorException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>RedirectorComponent::getCAResolverMetrics</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/> (server-only type)<br/>
        /// </summary>
        public virtual Task<EmptyMessage> GetCAResolverMetricsAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new RedirectorException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>RedirectorComponent::purgeServerRegistration</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/> (server-only type)<br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> PurgeServerRegistrationAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new RedirectorException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>RedirectorComponent::getServerInstanceHttp</b> command.<br/>
        /// Request type: <see cref="ServerInstanceRequest"/><br/>
        /// Response type: <see cref="ServerInstanceInfo"/><br/>
        /// Error response type: <see cref="ServerInstanceError"/><br/>
        /// </summary>
        public virtual Task<ServerInstanceInfo> GetServerInstanceHttpAsync(ServerInstanceRequest request, BlazeRpcContext context)
        {
            throw new RedirectorException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        // ===== Master command handlers =====

        /// <summary>
        /// This method is called when server receives a <b>RedirectorComponent::updateServerInfo</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/> (server-only type)<br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> UpdateServerInfoAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new RedirectorException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>RedirectorComponent::purgeServerRegistrationMaster</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/> (server-only type)<br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> PurgeServerRegistrationMasterAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new RedirectorException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        // ===== Notification helpers =====

        // No slave notifications for this component.

        // ===== Master notification helpers =====

        /// <summary>
        /// Call this method when you want to send a <b>RedirectorComponent::NotifyPurgeServerRegistration</b> master notification.<br/>
        /// Notification type: <see cref="EmptyMessage"/> (server-only type)<br/>
        /// </summary>
        public static Task NotifyPurgeServerRegistrationMasterAsync(BlazeRpcConnection connection, EmptyMessage notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = RedirectorComponentBase.Id;
                frame.Command = (ushort)RedirectorMasterNotification.NotifyPurgeServerRegistration;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        // ===== Event helpers =====

        /// <summary>
        /// Call this method when you want to send a <b>RedirectorComponent::ServiceAddedEvent</b> event.<br/>
        /// Event type: <see cref="ServerInfoData"/><br/>
        /// </summary>
        public static Task NotifyServiceAddedEventAsync(BlazeRpcConnection connection, ServerInfoData notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = RedirectorComponentBase.Id;
                frame.Command = (ushort)RedirectorEvent.ServiceAddedEvent;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send a <b>RedirectorComponent::ServiceRemovedEvent</b> event.<br/>
        /// Event type: <see cref="ServiceRemoveInfo"/><br/>
        /// </summary>
        public static Task NotifyServiceRemovedEventAsync(BlazeRpcConnection connection, ServiceRemoveInfo notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = RedirectorComponentBase.Id;
                frame.Command = (ushort)RedirectorEvent.ServiceRemovedEvent;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send a <b>RedirectorComponent::InstanceAddedEvent</b> event.<br/>
        /// Event type: <see cref="SlaveInfo"/><br/>
        /// </summary>
        public static Task NotifyInstanceAddedEventAsync(BlazeRpcConnection connection, SlaveInfo notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = RedirectorComponentBase.Id;
                frame.Command = (ushort)RedirectorEvent.InstanceAddedEvent;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send a <b>RedirectorComponent::InstanceRemovedEvent</b> event.<br/>
        /// Event type: <see cref="InstanceRemoveInfo"/><br/>
        /// </summary>
        public static Task NotifyInstanceRemovedEventAsync(BlazeRpcConnection connection, InstanceRemoveInfo notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = RedirectorComponentBase.Id;
                frame.Command = (ushort)RedirectorEvent.InstanceRemovedEvent;
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
