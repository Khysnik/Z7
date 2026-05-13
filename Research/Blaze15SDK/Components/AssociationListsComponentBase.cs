using Blaze.Core;
using Blaze15SDK.Blaze.Association;
using EATDF;
using EATDF.Types;

namespace Blaze15SDK.Components;

public static class AssociationListsComponentBase
{
    public const ushort Id = 25;
    public const string Name = "AssociationListsComponent";

    public enum Error : ushort {
        ASSOCIATIONLIST_ERR_USER_NOT_FOUND = 1,
        ASSOCIATIONLIST_ERR_DUPLICATE_USER_FOUND = 2,
        ASSOCIATIONLIST_ERR_CANNOT_INCLUDE_SELF = 3,
        ASSOCIATIONLIST_ERR_INVALID_USER = 4,
        ASSOCIATIONLIST_ERR_MEMBER_ALREADY_IN_THE_LIST = 5,
        ASSOCIATIONLIST_ERR_MEMBER_NOT_FOUND_IN_THE_LIST = 6,
        ASSOCIATIONLIST_ERR_LIST_NOT_FOUND = 10,
        ASSOCIATIONLIST_ERR_LIST_IS_FULL_OR_TOO_MANY_USERS = 11,
        ASSOCIATIONLIST_ERR_PAIRED_LIST_MODIFICATION_NOT_SUPPORTED = 16,
        ASSOCIATIONLIST_ERR_PAIRED_LIST_IS_FULL_OR_TOO_MANY_USERS = 17,
        ASSOCIATIONLIST_ERR_SUBSCRIBE_USER_LIST_NOT_SUPPORTED = 18,
    }

    public enum AssociationListsCommand : ushort
    {
        addUsersToList = 1,
        removeUsersFromList = 2,
        clearLists = 3,
        setUsersToList = 4,
        getListForUser = 5,
        getLists = 6,
        subscribeToLists = 7,
        unsubscribeFromLists = 8,
        getConfigListsInfo = 9,
        getMemberHash = 10,
        checkListMembership = 11,
        checkListContainsMembers = 12,
        setUsersAttributesInList = 13,
    }

    public enum AssociationListsNotification : ushort
    {
        NotifyUpdateListMembership = 100,
        NotifyUpdateListMembershipSlave = 101,
    }

    public enum AssociationListsEvent : ushort
    {
        ListUpdatedEvent = 1000,
    }

    public class Server : BlazeComponent
    {
        public override ushort Id => AssociationListsComponentBase.Id;
        public override string Name => AssociationListsComponentBase.Name;

        public virtual bool IsCommandSupported(AssociationListsCommand command) => false;

        public class AssociationListsException : BlazeRpcException
        {
            public AssociationListsException(Error error) : base((ushort)error, null) { }
            public AssociationListsException(ServerError error) : base(error.WithErrorPrefix(), null) { }
            public AssociationListsException(Error error, Tdf? errorResponse) : base((ushort)error, errorResponse) { }
            public AssociationListsException(ServerError error, Tdf? errorResponse) : base(error.WithErrorPrefix(), errorResponse) { }
            public AssociationListsException(Error error, Tdf? errorResponse, string? message) : base((ushort)error, errorResponse, message) { }
            public AssociationListsException(ServerError error, Tdf? errorResponse, string? message) : base(error.WithErrorPrefix(), errorResponse, message) { }
            public AssociationListsException(Error error, Tdf? errorResponse, string? message, Exception? innerException) : base((ushort)error, errorResponse, message, innerException) { }
            public AssociationListsException(ServerError error, Tdf? errorResponse, string? message, Exception? innerException) : base(error.WithErrorPrefix(), errorResponse, message, innerException) { }
        }

        public Server()
        {
            // Slave commands
            RegisterCommand(new RpcCommandFunc<UpdateListMembersRequest, UpdateListMembersResponse, EmptyMessage>()
            {
                Id = (ushort)AssociationListsCommand.addUsersToList,
                Name = "addUsersToList",
                IsSupported = IsCommandSupported(AssociationListsCommand.addUsersToList),
                Func = async (req, ctx) => await AddUsersToListAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UpdateListMembersRequest, UpdateListMembersResponse, EmptyMessage>()
            {
                Id = (ushort)AssociationListsCommand.removeUsersFromList,
                Name = "removeUsersFromList",
                IsSupported = IsCommandSupported(AssociationListsCommand.removeUsersFromList),
                Func = async (req, ctx) => await RemoveUsersFromListAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UpdateListsRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)AssociationListsCommand.clearLists,
                Name = "clearLists",
                IsSupported = IsCommandSupported(AssociationListsCommand.clearLists),
                Func = async (req, ctx) => await ClearListsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UpdateListMembersRequest, UpdateListMembersResponse, EmptyMessage>()
            {
                Id = (ushort)AssociationListsCommand.setUsersToList,
                Name = "setUsersToList",
                IsSupported = IsCommandSupported(AssociationListsCommand.setUsersToList),
                Func = async (req, ctx) => await SetUsersToListAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetListForUserRequest, ListMembers, EmptyMessage>()
            {
                Id = (ushort)AssociationListsCommand.getListForUser,
                Name = "getListForUser",
                IsSupported = IsCommandSupported(AssociationListsCommand.getListForUser),
                Func = async (req, ctx) => await GetListForUserAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetListsRequest, Lists, EmptyMessage>()
            {
                Id = (ushort)AssociationListsCommand.getLists,
                Name = "getLists",
                IsSupported = IsCommandSupported(AssociationListsCommand.getLists),
                Func = async (req, ctx) => await GetListsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UpdateListsRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)AssociationListsCommand.subscribeToLists,
                Name = "subscribeToLists",
                IsSupported = IsCommandSupported(AssociationListsCommand.subscribeToLists),
                Func = async (req, ctx) => await SubscribeToListsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UpdateListsRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)AssociationListsCommand.unsubscribeFromLists,
                Name = "unsubscribeFromLists",
                IsSupported = IsCommandSupported(AssociationListsCommand.unsubscribeFromLists),
                Func = async (req, ctx) => await UnsubscribeFromListsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, ConfigLists, EmptyMessage>()
            {
                Id = (ushort)AssociationListsCommand.getConfigListsInfo,
                Name = "getConfigListsInfo",
                IsSupported = IsCommandSupported(AssociationListsCommand.getConfigListsInfo),
                Func = async (req, ctx) => await GetConfigListsInfoAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetMemberHashRequest, GetMemberHashResponse, EmptyMessage>()
            {
                Id = (ushort)AssociationListsCommand.getMemberHash,
                Name = "getMemberHash",
                IsSupported = IsCommandSupported(AssociationListsCommand.getMemberHash),
                Func = async (req, ctx) => await GetMemberHashAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<CheckListMembershipRequest, ListBlazeIds, EmptyMessage>()
            {
                Id = (ushort)AssociationListsCommand.checkListMembership,
                Name = "checkListMembership",
                IsSupported = IsCommandSupported(AssociationListsCommand.checkListMembership),
                Func = async (req, ctx) => await CheckListMembershipAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<CheckListContainsMembersRequest, ListBlazeIds, EmptyMessage>()
            {
                Id = (ushort)AssociationListsCommand.checkListContainsMembers,
                Name = "checkListContainsMembers",
                IsSupported = IsCommandSupported(AssociationListsCommand.checkListContainsMembers),
                Func = async (req, ctx) => await CheckListContainsMembersAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UpdateListMembersRequest, UpdateListMembersResponse, EmptyMessage>()
            {
                Id = (ushort)AssociationListsCommand.setUsersAttributesInList,
                Name = "setUsersAttributesInList",
                IsSupported = IsCommandSupported(AssociationListsCommand.setUsersAttributesInList),
                Func = async (req, ctx) => await SetUsersAttributesInListAsync(req, ctx).ConfigureAwait(false)
            });

        }

        public override string GetErrorName(ushort errorCode)
        {
            return ((Error)errorCode).ToString();
        }

        // ===== Slave command handlers =====

        /// <summary>
        /// This method is called when server receives a <b>AssociationListsComponent::addUsersToList</b> command.<br/>
        /// Request type: <see cref="UpdateListMembersRequest"/><br/>
        /// Response type: <see cref="UpdateListMembersResponse"/><br/>
        /// </summary>
        public virtual Task<UpdateListMembersResponse> AddUsersToListAsync(UpdateListMembersRequest request, BlazeRpcContext context)
        {
            throw new AssociationListsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>AssociationListsComponent::removeUsersFromList</b> command.<br/>
        /// Request type: <see cref="UpdateListMembersRequest"/><br/>
        /// Response type: <see cref="UpdateListMembersResponse"/><br/>
        /// </summary>
        public virtual Task<UpdateListMembersResponse> RemoveUsersFromListAsync(UpdateListMembersRequest request, BlazeRpcContext context)
        {
            throw new AssociationListsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>AssociationListsComponent::clearLists</b> command.<br/>
        /// Request type: <see cref="UpdateListsRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> ClearListsAsync(UpdateListsRequest request, BlazeRpcContext context)
        {
            throw new AssociationListsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>AssociationListsComponent::setUsersToList</b> command.<br/>
        /// Request type: <see cref="UpdateListMembersRequest"/><br/>
        /// Response type: <see cref="UpdateListMembersResponse"/><br/>
        /// </summary>
        public virtual Task<UpdateListMembersResponse> SetUsersToListAsync(UpdateListMembersRequest request, BlazeRpcContext context)
        {
            throw new AssociationListsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>AssociationListsComponent::getListForUser</b> command.<br/>
        /// Request type: <see cref="GetListForUserRequest"/><br/>
        /// Response type: <see cref="ListMembers"/><br/>
        /// </summary>
        public virtual Task<ListMembers> GetListForUserAsync(GetListForUserRequest request, BlazeRpcContext context)
        {
            throw new AssociationListsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>AssociationListsComponent::getLists</b> command.<br/>
        /// Request type: <see cref="GetListsRequest"/><br/>
        /// Response type: <see cref="Lists"/><br/>
        /// </summary>
        public virtual Task<Lists> GetListsAsync(GetListsRequest request, BlazeRpcContext context)
        {
            throw new AssociationListsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>AssociationListsComponent::subscribeToLists</b> command.<br/>
        /// Request type: <see cref="UpdateListsRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> SubscribeToListsAsync(UpdateListsRequest request, BlazeRpcContext context)
        {
            throw new AssociationListsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>AssociationListsComponent::unsubscribeFromLists</b> command.<br/>
        /// Request type: <see cref="UpdateListsRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> UnsubscribeFromListsAsync(UpdateListsRequest request, BlazeRpcContext context)
        {
            throw new AssociationListsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>AssociationListsComponent::getConfigListsInfo</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="ConfigLists"/><br/>
        /// </summary>
        public virtual Task<ConfigLists> GetConfigListsInfoAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new AssociationListsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>AssociationListsComponent::getMemberHash</b> command.<br/>
        /// Request type: <see cref="GetMemberHashRequest"/><br/>
        /// Response type: <see cref="GetMemberHashResponse"/><br/>
        /// </summary>
        public virtual Task<GetMemberHashResponse> GetMemberHashAsync(GetMemberHashRequest request, BlazeRpcContext context)
        {
            throw new AssociationListsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>AssociationListsComponent::checkListMembership</b> command.<br/>
        /// Request type: <see cref="CheckListMembershipRequest"/><br/>
        /// Response type: <see cref="ListBlazeIds"/><br/>
        /// </summary>
        public virtual Task<ListBlazeIds> CheckListMembershipAsync(CheckListMembershipRequest request, BlazeRpcContext context)
        {
            throw new AssociationListsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>AssociationListsComponent::checkListContainsMembers</b> command.<br/>
        /// Request type: <see cref="CheckListContainsMembersRequest"/><br/>
        /// Response type: <see cref="ListBlazeIds"/><br/>
        /// </summary>
        public virtual Task<ListBlazeIds> CheckListContainsMembersAsync(CheckListContainsMembersRequest request, BlazeRpcContext context)
        {
            throw new AssociationListsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>AssociationListsComponent::setUsersAttributesInList</b> command.<br/>
        /// Request type: <see cref="UpdateListMembersRequest"/><br/>
        /// Response type: <see cref="UpdateListMembersResponse"/><br/>
        /// </summary>
        public virtual Task<UpdateListMembersResponse> SetUsersAttributesInListAsync(UpdateListMembersRequest request, BlazeRpcContext context)
        {
            throw new AssociationListsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        // ===== Notification helpers =====

        /// <summary>
        /// Call this method when you want to send to client a <b>AssociationListsComponent::NotifyUpdateListMembership</b> notification.<br/>
        /// Notification type: <see cref="UpdateListMembersResponse"/><br/>
        /// </summary>
        public static Task NotifyUpdateListMembershipAsync(BlazeRpcConnection connection, UpdateListMembersResponse notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = AssociationListsComponentBase.Id;
                frame.Command = (ushort)AssociationListsNotification.NotifyUpdateListMembership;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>AssociationListsComponent::NotifyUpdateListMembershipSlave</b> notification.<br/>
        /// Notification type: <see cref="UpdateListMembersResponse"/><br/>
        /// </summary>
        public static Task NotifyUpdateListMembershipSlaveAsync(BlazeRpcConnection connection, UpdateListMembersResponse notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = AssociationListsComponentBase.Id;
                frame.Command = (ushort)AssociationListsNotification.NotifyUpdateListMembershipSlave;
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
