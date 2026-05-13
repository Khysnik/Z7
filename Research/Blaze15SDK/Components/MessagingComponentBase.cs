using Blaze.Core;
using Blaze15SDK.Blaze.Messaging;
using EATDF;
using EATDF.Types;

namespace Blaze15SDK.Components;

public static class MessagingComponentBase
{
    public const ushort Id = 15;
    public const string Name = "MessagingComponent";

    public enum Error : ushort {
        MESSAGING_ERR_UNKNOWN = 1,
        MESSAGING_ERR_MAX_ATTR_EXCEEDED = 2,
        MESSAGING_ERR_DATABASE = 3,
        MESSAGING_ERR_TARGET_NOT_FOUND = 4,
        MESSAGING_ERR_TARGET_TYPE_INVALID = 5,
        MESSAGING_ERR_TARGET_INBOX_FULL = 6,
        MESSAGING_ERR_MATCH_NOT_FOUND = 7,
        MESSAGING_ERR_FEATURE_DISABLED = 8,
        MESSAGING_ERR_INVALID_PARAM = 9,
        MESSAGING_ERR_PROFANITY_SYSTEM = 10,
    }

    public enum MessagingCommand : ushort
    {
        sendMessage = 1,
        fetchMessages = 2,
        purgeMessages = 3,
        touchMessages = 4,
        getMessages = 5,
        sendSourceMessage = 6,
        sendGlobalMessage = 7,
    }

    public enum MessagingNotification : ushort
    {
        NotifyMessage = 1,
    }

    public enum MessagingEvent : ushort
    {
        MessageSentEvent = 1000,
    }

    public class Server : BlazeComponent
    {
        public override ushort Id => MessagingComponentBase.Id;
        public override string Name => MessagingComponentBase.Name;

        public virtual bool IsCommandSupported(MessagingCommand command) => false;

        public class MessagingException : BlazeRpcException
        {
            public MessagingException(Error error) : base((ushort)error, null) { }
            public MessagingException(ServerError error) : base(error.WithErrorPrefix(), null) { }
            public MessagingException(Error error, Tdf? errorResponse) : base((ushort)error, errorResponse) { }
            public MessagingException(ServerError error, Tdf? errorResponse) : base(error.WithErrorPrefix(), errorResponse) { }
            public MessagingException(Error error, Tdf? errorResponse, string? message) : base((ushort)error, errorResponse, message) { }
            public MessagingException(ServerError error, Tdf? errorResponse, string? message) : base(error.WithErrorPrefix(), errorResponse, message) { }
            public MessagingException(Error error, Tdf? errorResponse, string? message, Exception? innerException) : base((ushort)error, errorResponse, message, innerException) { }
            public MessagingException(ServerError error, Tdf? errorResponse, string? message, Exception? innerException) : base(error.WithErrorPrefix(), errorResponse, message, innerException) { }
        }

        public Server()
        {
            // Slave commands
            RegisterCommand(new RpcCommandFunc<ClientMessage, SendMessageResponse, EmptyMessage>()
            {
                Id = (ushort)MessagingCommand.sendMessage,
                Name = "sendMessage",
                IsSupported = IsCommandSupported(MessagingCommand.sendMessage),
                Func = async (req, ctx) => await SendMessageAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<FetchMessageRequest, FetchMessageResponse, EmptyMessage>()
            {
                Id = (ushort)MessagingCommand.fetchMessages,
                Name = "fetchMessages",
                IsSupported = IsCommandSupported(MessagingCommand.fetchMessages),
                Func = async (req, ctx) => await FetchMessagesAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<PurgeMessageRequest, PurgeMessageResponse, EmptyMessage>()
            {
                Id = (ushort)MessagingCommand.purgeMessages,
                Name = "purgeMessages",
                IsSupported = IsCommandSupported(MessagingCommand.purgeMessages),
                Func = async (req, ctx) => await PurgeMessagesAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<TouchMessageRequest, TouchMessageResponse, EmptyMessage>()
            {
                Id = (ushort)MessagingCommand.touchMessages,
                Name = "touchMessages",
                IsSupported = IsCommandSupported(MessagingCommand.touchMessages),
                Func = async (req, ctx) => await TouchMessagesAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<FetchMessageRequest, GetMessagesResponse, EmptyMessage>()
            {
                Id = (ushort)MessagingCommand.getMessages,
                Name = "getMessages",
                IsSupported = IsCommandSupported(MessagingCommand.getMessages),
                Func = async (req, ctx) => await GetMessagesAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<SendSourceMessageRequest, SendMessageResponse, EmptyMessage>()
            {
                Id = (ushort)MessagingCommand.sendSourceMessage,
                Name = "sendSourceMessage",
                IsSupported = IsCommandSupported(MessagingCommand.sendSourceMessage),
                Func = async (req, ctx) => await SendSourceMessageAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<SendGlobalMessageRequest, SendGlobalMessageResponse, EmptyMessage>()
            {
                Id = (ushort)MessagingCommand.sendGlobalMessage,
                Name = "sendGlobalMessage",
                IsSupported = IsCommandSupported(MessagingCommand.sendGlobalMessage),
                Func = async (req, ctx) => await SendGlobalMessageAsync(req, ctx).ConfigureAwait(false)
            });

        }

        public override string GetErrorName(ushort errorCode)
        {
            return ((Error)errorCode).ToString();
        }

        // ===== Slave command handlers =====

        /// <summary>
        /// This method is called when server receives a <b>MessagingComponent::sendMessage</b> command.<br/>
        /// Request type: <see cref="ClientMessage"/><br/>
        /// Response type: <see cref="SendMessageResponse"/><br/>
        /// </summary>
        public virtual Task<SendMessageResponse> SendMessageAsync(ClientMessage request, BlazeRpcContext context)
        {
            throw new MessagingException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>MessagingComponent::fetchMessages</b> command.<br/>
        /// Request type: <see cref="FetchMessageRequest"/><br/>
        /// Response type: <see cref="FetchMessageResponse"/><br/>
        /// </summary>
        public virtual Task<FetchMessageResponse> FetchMessagesAsync(FetchMessageRequest request, BlazeRpcContext context)
        {
            throw new MessagingException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>MessagingComponent::purgeMessages</b> command.<br/>
        /// Request type: <see cref="PurgeMessageRequest"/><br/>
        /// Response type: <see cref="PurgeMessageResponse"/><br/>
        /// </summary>
        public virtual Task<PurgeMessageResponse> PurgeMessagesAsync(PurgeMessageRequest request, BlazeRpcContext context)
        {
            throw new MessagingException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>MessagingComponent::touchMessages</b> command.<br/>
        /// Request type: <see cref="TouchMessageRequest"/><br/>
        /// Response type: <see cref="TouchMessageResponse"/><br/>
        /// </summary>
        public virtual Task<TouchMessageResponse> TouchMessagesAsync(TouchMessageRequest request, BlazeRpcContext context)
        {
            throw new MessagingException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>MessagingComponent::getMessages</b> command.<br/>
        /// Request type: <see cref="FetchMessageRequest"/><br/>
        /// Response type: <see cref="GetMessagesResponse"/><br/>
        /// </summary>
        public virtual Task<GetMessagesResponse> GetMessagesAsync(FetchMessageRequest request, BlazeRpcContext context)
        {
            throw new MessagingException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>MessagingComponent::sendSourceMessage</b> command.<br/>
        /// Request type: <see cref="SendSourceMessageRequest"/><br/>
        /// Response type: <see cref="SendMessageResponse"/><br/>
        /// </summary>
        public virtual Task<SendMessageResponse> SendSourceMessageAsync(SendSourceMessageRequest request, BlazeRpcContext context)
        {
            throw new MessagingException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>MessagingComponent::sendGlobalMessage</b> command.<br/>
        /// Request type: <see cref="SendGlobalMessageRequest"/><br/>
        /// Response type: <see cref="SendGlobalMessageResponse"/><br/>
        /// </summary>
        public virtual Task<SendGlobalMessageResponse> SendGlobalMessageAsync(SendGlobalMessageRequest request, BlazeRpcContext context)
        {
            throw new MessagingException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        // ===== Notification helpers =====

        /// <summary>
        /// Call this method when you want to send to client a <b>MessagingComponent::NotifyMessage</b> notification.<br/>
        /// Notification type: <see cref="ServerMessage"/><br/>
        /// </summary>
        public static Task NotifyMessageAsync(BlazeRpcConnection connection, ServerMessage notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = MessagingComponentBase.Id;
                frame.Command = (ushort)MessagingNotification.NotifyMessage;
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
        /// Call this method when you want to send a <b>MessagingComponent::MessageSentEvent</b> event.<br/>
        /// Event type: <see cref="EmptyMessage"/> (server-only type)<br/>
        /// </summary>
        public static Task NotifyMessageSentEventAsync(BlazeRpcConnection connection, EmptyMessage notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = MessagingComponentBase.Id;
                frame.Command = (ushort)MessagingEvent.MessageSentEvent;
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
