using Blaze.Core;
using Blaze15SDK.Blaze.DynamicInetFilter;
using EATDF;
using EATDF.Types;

namespace Blaze15SDK.Components;

public static class DynamicInetFilterComponentBase
{
    public const ushort Id = 30732;
    public const string Name = "DynamicInetFilterComponent";

    public enum Error : ushort {
        DYNAMICINETFILTER_ERR_ROW_NOT_FOUND = 1,
        DYNAMICINETFILTER_ERR_INVALID_GROUP = 2,
        DYNAMICINETFILTER_ERR_INVALID_OWNER = 3,
        DYNAMICINETFILTER_ERR_INVALID_SUBNET = 4,
        DYNAMICINETFILTER_ERR_INVALID_COMMENT = 5,
    }

    public enum DynamicInetFilterCommand : ushort
    {
        add = 1,
        update = 2,
        remove = 3,
        listInfo = 4,
    }

    public enum DynamicInetFilterNotification : ushort
    {
        AddOrUpdateNotification = 1,
        RemoveNotification = 2,
    }

    public class Server : BlazeComponent
    {
        public override ushort Id => DynamicInetFilterComponentBase.Id;
        public override string Name => DynamicInetFilterComponentBase.Name;

        public virtual bool IsCommandSupported(DynamicInetFilterCommand command) => false;

        public class DynamicInetFilterException : BlazeRpcException
        {
            public DynamicInetFilterException(Error error) : base((ushort)error, null) { }
            public DynamicInetFilterException(ServerError error) : base(error.WithErrorPrefix(), null) { }
            public DynamicInetFilterException(Error error, Tdf? errorResponse) : base((ushort)error, errorResponse) { }
            public DynamicInetFilterException(ServerError error, Tdf? errorResponse) : base(error.WithErrorPrefix(), errorResponse) { }
            public DynamicInetFilterException(Error error, Tdf? errorResponse, string? message) : base((ushort)error, errorResponse, message) { }
            public DynamicInetFilterException(ServerError error, Tdf? errorResponse, string? message) : base(error.WithErrorPrefix(), errorResponse, message) { }
            public DynamicInetFilterException(Error error, Tdf? errorResponse, string? message, Exception? innerException) : base((ushort)error, errorResponse, message, innerException) { }
            public DynamicInetFilterException(ServerError error, Tdf? errorResponse, string? message, Exception? innerException) : base(error.WithErrorPrefix(), errorResponse, message, innerException) { }
        }

        public Server()
        {
            // Slave commands
            RegisterCommand(new RpcCommandFunc<AddRequest, AddResponse, EmptyMessage>()
            {
                Id = (ushort)DynamicInetFilterCommand.add,
                Name = "add",
                IsSupported = IsCommandSupported(DynamicInetFilterCommand.add),
                Func = async (req, ctx) => await AddAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UpdateRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)DynamicInetFilterCommand.update,
                Name = "update",
                IsSupported = IsCommandSupported(DynamicInetFilterCommand.update),
                Func = async (req, ctx) => await UpdateAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<RemoveRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)DynamicInetFilterCommand.remove,
                Name = "remove",
                IsSupported = IsCommandSupported(DynamicInetFilterCommand.remove),
                Func = async (req, ctx) => await RemoveAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<ListRequest, ListResponse, EmptyMessage>()
            {
                Id = (ushort)DynamicInetFilterCommand.listInfo,
                Name = "listInfo",
                IsSupported = IsCommandSupported(DynamicInetFilterCommand.listInfo),
                Func = async (req, ctx) => await ListInfoAsync(req, ctx).ConfigureAwait(false)
            });

        }

        public override string GetErrorName(ushort errorCode)
        {
            return ((Error)errorCode).ToString();
        }

        // ===== Slave command handlers =====

        /// <summary>
        /// This method is called when server receives a <b>DynamicInetFilterComponent::add</b> command.<br/>
        /// Request type: <see cref="AddRequest"/><br/>
        /// Response type: <see cref="AddResponse"/><br/>
        /// </summary>
        public virtual Task<AddResponse> AddAsync(AddRequest request, BlazeRpcContext context)
        {
            throw new DynamicInetFilterException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>DynamicInetFilterComponent::update</b> command.<br/>
        /// Request type: <see cref="UpdateRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> UpdateAsync(UpdateRequest request, BlazeRpcContext context)
        {
            throw new DynamicInetFilterException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>DynamicInetFilterComponent::remove</b> command.<br/>
        /// Request type: <see cref="RemoveRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> RemoveAsync(RemoveRequest request, BlazeRpcContext context)
        {
            throw new DynamicInetFilterException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>DynamicInetFilterComponent::listInfo</b> command.<br/>
        /// Request type: <see cref="ListRequest"/><br/>
        /// Response type: <see cref="ListResponse"/><br/>
        /// </summary>
        public virtual Task<ListResponse> ListInfoAsync(ListRequest request, BlazeRpcContext context)
        {
            throw new DynamicInetFilterException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        // ===== Notification helpers =====

        /// <summary>
        /// Call this method when you want to send to client a <b>DynamicInetFilterComponent::AddOrUpdateNotification</b> notification.<br/>
        /// Notification type: <see cref="Entry"/><br/>
        /// </summary>
        public static Task NotifyAddOrUpdateNotificationAsync(BlazeRpcConnection connection, Entry notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = DynamicInetFilterComponentBase.Id;
                frame.Command = (ushort)DynamicInetFilterNotification.AddOrUpdateNotification;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>DynamicInetFilterComponent::RemoveNotification</b> notification.<br/>
        /// Notification type: <see cref="RemoveRequest"/><br/>
        /// </summary>
        public static Task NotifyRemoveNotificationAsync(BlazeRpcConnection connection, RemoveRequest notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = DynamicInetFilterComponentBase.Id;
                frame.Command = (ushort)DynamicInetFilterNotification.RemoveNotification;
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
