using Blaze.Core;
using Blaze15SDK.Blaze.Social.Friends;
using EATDF;
using EATDF.Types;

namespace Blaze15SDK.Components;

public static class FriendsComponentBase
{
    public const ushort Id = 1031;
    public const string Name = "FriendsComponent";

    public enum Error : ushort
    {
        FRIENDS_UNKNOWN_ERR = 504,
        FRIENDS_NO_SUCH_USER = 901,
        FRIENDS_INVALID_ARGUMENT_TYPE = 909,
        FRIENDS_API_KEY_INVALID = 11100,
        FRIENDS_NO_ID_SPECIFIED = 20006,
        FRIENDS_NO_FRIEND_SPECIFIED = 20007,
        FRIENDS_BAD_PRIVACY_SETTING = 20008,
        FRIENDS_FRIENDS_LIST_FULL = 22001,
        FRIENDS_NO_GROUP_NAME_SPECIFIED = 22002,
        FRIENDS_GLOBAL_GROUP_LOCKED = 22003,
        FRIENDS_AUTHTOKEN_INVALID = 22004,
        FRIENDS_AUTHTOKEN_INVALID_FOR_USER = 22005,
        FRIENDS_INVITATION_NOT_FOUND = 22006,
        FRIENDS_TARGET_NOT_FRIEND = 22007,
        FRIENDS_TARGET_IN_CUSTOM_GROUP = 22008,
        FRIENDS_INVITEE_BLOCKED_BY_INVITER = 22009,
        FRIENDS_INVITEE_ALREADY_FRIEND = 22010,
        FRIENDS_FRIEND_NOT_IN_GROUP = 22013,
        FRIENDS_BLOCKING_SELF = 22015,
        FRIENDS_BATCH_INVITE_ERROR = 22016,
        FRIENDS_INVITEE_IS_INVITER = 22017,
        FRIENDS_ID_LIST_EMPTY = 22018,
        FRIENDS_OUTBOUND_INVITES_FULL = 22020,
        FRIENDS_INBOUND_INVITES_FULL = 22021,
        FRIENDS_FAVORITING_SELF = 22022,
        FRIENDS_UNFAVORITING_SELF = 22023,
        FRIENDS_FAVORITING_NON_FRIEND = 22024,
        FRIENDS_UNFAVORITING_NON_FRIEND = 22025,
        FRIENDS_NAMESPACE_MISTMATCH = 22026,
    }

    public enum FriendsCommand : ushort
    {
        muteUser = 1,
        checkMuteStatus = 2,
        unmuteUser = 3,
    }

    public class Server : BlazeComponent
    {
        public override ushort Id => FriendsComponentBase.Id;
        public override string Name => FriendsComponentBase.Name;

        public virtual bool IsCommandSupported(FriendsCommand command) => false;

        public class FriendsException : BlazeRpcException
        {
            public FriendsException(Error error) : base((ushort)error, null) { }
            public FriendsException(ServerError error) : base(error.WithErrorPrefix(), null) { }
            public FriendsException(Error error, Tdf? errorResponse) : base((ushort)error, errorResponse) { }
            public FriendsException(ServerError error, Tdf? errorResponse) : base(error.WithErrorPrefix(), errorResponse) { }
            public FriendsException(Error error, Tdf? errorResponse, string? message) : base((ushort)error, errorResponse, message) { }
            public FriendsException(ServerError error, Tdf? errorResponse, string? message) : base(error.WithErrorPrefix(), errorResponse, message) { }
            public FriendsException(Error error, Tdf? errorResponse, string? message, Exception? innerException) : base((ushort)error, errorResponse, message, innerException) { }
            public FriendsException(ServerError error, Tdf? errorResponse, string? message, Exception? innerException) : base(error.WithErrorPrefix(), errorResponse, message, innerException) { }
        }

        public Server()
        {
            // Slave commands
            RegisterCommand(new RpcCommandFunc<MuteUserRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)FriendsCommand.muteUser,
                Name = "muteUser",
                IsSupported = IsCommandSupported(FriendsCommand.muteUser),
                Func = async (req, ctx) => await MuteUserAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<CheckMuteStatusRequest, PagedListUser, EmptyMessage>()
            {
                Id = (ushort)FriendsCommand.checkMuteStatus,
                Name = "checkMuteStatus",
                IsSupported = IsCommandSupported(FriendsCommand.checkMuteStatus),
                Func = async (req, ctx) => await CheckMuteStatusAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<MuteUserRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)FriendsCommand.unmuteUser,
                Name = "unmuteUser",
                IsSupported = IsCommandSupported(FriendsCommand.unmuteUser),
                Func = async (req, ctx) => await UnmuteUserAsync(req, ctx).ConfigureAwait(false)
            });

        }

        public override string GetErrorName(ushort errorCode)
        {
            return ((Error)errorCode).ToString();
        }

        // ===== Slave command handlers =====

        /// <summary>
        /// This method is called when server receives a <b>FriendsComponent::muteUser</b> command.<br/>
        /// Request type: <see cref="MuteUserRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> MuteUserAsync(MuteUserRequest request, BlazeRpcContext context)
        {
            throw new FriendsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>FriendsComponent::checkMuteStatus</b> command.<br/>
        /// Request type: <see cref="CheckMuteStatusRequest"/><br/>
        /// Response type: <see cref="PagedListUser"/><br/>
        /// </summary>
        public virtual Task<PagedListUser> CheckMuteStatusAsync(CheckMuteStatusRequest request, BlazeRpcContext context)
        {
            throw new FriendsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>FriendsComponent::unmuteUser</b> command.<br/>
        /// Request type: <see cref="MuteUserRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> UnmuteUserAsync(MuteUserRequest request, BlazeRpcContext context)
        {
            throw new FriendsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

    }

}
