using Blaze.Core;
using Blaze15SDK.Blaze.SponsoredEvents;
using EATDF;
using EATDF.Types;

namespace Blaze15SDK.Components;

public static class SponsoredEventsComponentBase
{
    public const ushort Id = 2076; // custom/28 = (1 << 11) | 28 = 0x081C
    public const string Name = "SponsoredEventsComponent";

    public enum Error : ushort
    {
        SPONSORED_EVENTS_ERR_GENERAL = 1,
        SPONSORED_EVENTS_ERR_DB = 2,
        SPONSORED_EVENTS_ERR_INVALID_PARAMS = 3,
        SPONSORED_EVENTS_ERR_NO_UPDATE = 4,
    }

    public enum SponsoredEventsCommand : ushort
    {
        checkUserRegistration = 1,
        registerUser = 2,
        getEventsURL = 3,
        banUser = 4,
        wipeUserStats = 5,
        updateUserFlags = 6,
        numUsers = 7,
        removeUser = 8,
        updateEventData = 9,
        getDbCredentials = 10,
        returnUsers = 11,
    }

    public enum SponsoredEventsNotification : ushort
    {
    }

    public class Server : BlazeComponent
    {
        public override ushort Id => SponsoredEventsComponentBase.Id;
        public override string Name => SponsoredEventsComponentBase.Name;

        public virtual bool IsCommandSupported(SponsoredEventsCommand command) => false;

        public class SponsoredEventsException : BlazeRpcException
        {
            public SponsoredEventsException(Error error) : base((ushort)error, null) { }
            public SponsoredEventsException(ServerError error) : base(error.WithErrorPrefix(), null) { }
            public SponsoredEventsException(Error error, Tdf? errorResponse) : base((ushort)error, errorResponse) { }
            public SponsoredEventsException(ServerError error, Tdf? errorResponse) : base(error.WithErrorPrefix(), errorResponse) { }
            public SponsoredEventsException(Error error, Tdf? errorResponse, string? message) : base((ushort)error, errorResponse, message) { }
            public SponsoredEventsException(ServerError error, Tdf? errorResponse, string? message) : base(error.WithErrorPrefix(), errorResponse, message) { }
            public SponsoredEventsException(Error error, Tdf? errorResponse, string? message, Exception? innerException) : base((ushort)error, errorResponse, message, innerException) { }
            public SponsoredEventsException(ServerError error, Tdf? errorResponse, string? message, Exception? innerException) : base(error.WithErrorPrefix(), errorResponse, message, innerException) { }
        }

        public Server()
        {
            RegisterCommand(new RpcCommandFunc<CheckUserRegistrationRequest, CheckUserRegistrationResponse, EmptyMessage>()
            {
                Id = (ushort)SponsoredEventsCommand.checkUserRegistration,
                Name = "checkUserRegistration",
                IsSupported = IsCommandSupported(SponsoredEventsCommand.checkUserRegistration),
                Func = async (req, ctx) => await CheckUserRegistrationAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<RegisterUserRequest, RegisterUserResponse, EmptyMessage>()
            {
                Id = (ushort)SponsoredEventsCommand.registerUser,
                Name = "registerUser",
                IsSupported = IsCommandSupported(SponsoredEventsCommand.registerUser),
                Func = async (req, ctx) => await RegisterUserAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, URLResponse, EmptyMessage>()
            {
                Id = (ushort)SponsoredEventsCommand.getEventsURL,
                Name = "getEventsURL",
                IsSupported = IsCommandSupported(SponsoredEventsCommand.getEventsURL),
                Func = async (req, ctx) => await GetEventsURLAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<BanUserRequest, BanUserResponse, EmptyMessage>()
            {
                Id = (ushort)SponsoredEventsCommand.banUser,
                Name = "banUser",
                IsSupported = IsCommandSupported(SponsoredEventsCommand.banUser),
                Func = async (req, ctx) => await BanUserAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<WipeUserStatsRequest, WipeUserStatsResponse, EmptyMessage>()
            {
                Id = (ushort)SponsoredEventsCommand.wipeUserStats,
                Name = "wipeUserStats",
                IsSupported = IsCommandSupported(SponsoredEventsCommand.wipeUserStats),
                Func = async (req, ctx) => await WipeUserStatsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UpdateUserFlagsRequest, UpdateUserFlagsResponse, EmptyMessage>()
            {
                Id = (ushort)SponsoredEventsCommand.updateUserFlags,
                Name = "updateUserFlags",
                IsSupported = IsCommandSupported(SponsoredEventsCommand.updateUserFlags),
                Func = async (req, ctx) => await UpdateUserFlagsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<NumUsersRequest, NumUsersResponse, EmptyMessage>()
            {
                Id = (ushort)SponsoredEventsCommand.numUsers,
                Name = "numUsers",
                IsSupported = IsCommandSupported(SponsoredEventsCommand.numUsers),
                Func = async (req, ctx) => await NumUsersAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<RemoveUserRequest, RemoveUserResponse, EmptyMessage>()
            {
                Id = (ushort)SponsoredEventsCommand.removeUser,
                Name = "removeUser",
                IsSupported = IsCommandSupported(SponsoredEventsCommand.removeUser),
                Func = async (req, ctx) => await RemoveUserAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UpdateEventDataRequest, UpdateEventDataResponse, EmptyMessage>()
            {
                Id = (ushort)SponsoredEventsCommand.updateEventData,
                Name = "updateEventData",
                IsSupported = IsCommandSupported(SponsoredEventsCommand.updateEventData),
                Func = async (req, ctx) => await UpdateEventDataAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, DbCredentialsResponse, EmptyMessage>()
            {
                Id = (ushort)SponsoredEventsCommand.getDbCredentials,
                Name = "getDbCredentials",
                IsSupported = IsCommandSupported(SponsoredEventsCommand.getDbCredentials),
                Func = async (req, ctx) => await GetDbCredentialsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<ReturnUsersRequest, ReturnUsersResponse, EmptyMessage>()
            {
                Id = (ushort)SponsoredEventsCommand.returnUsers,
                Name = "returnUsers",
                IsSupported = IsCommandSupported(SponsoredEventsCommand.returnUsers),
                Func = async (req, ctx) => await ReturnUsersAsync(req, ctx).ConfigureAwait(false)
            });
        }

        public override string GetErrorName(ushort errorCode)
        {
            return ((Error)errorCode).ToString();
        }

        public virtual Task<CheckUserRegistrationResponse> CheckUserRegistrationAsync(CheckUserRegistrationRequest request, BlazeRpcContext context)
        {
            throw new SponsoredEventsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        public virtual Task<RegisterUserResponse> RegisterUserAsync(RegisterUserRequest request, BlazeRpcContext context)
        {
            throw new SponsoredEventsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        public virtual Task<URLResponse> GetEventsURLAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new SponsoredEventsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        public virtual Task<BanUserResponse> BanUserAsync(BanUserRequest request, BlazeRpcContext context)
        {
            throw new SponsoredEventsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        public virtual Task<WipeUserStatsResponse> WipeUserStatsAsync(WipeUserStatsRequest request, BlazeRpcContext context)
        {
            throw new SponsoredEventsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        public virtual Task<UpdateUserFlagsResponse> UpdateUserFlagsAsync(UpdateUserFlagsRequest request, BlazeRpcContext context)
        {
            throw new SponsoredEventsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        public virtual Task<NumUsersResponse> NumUsersAsync(NumUsersRequest request, BlazeRpcContext context)
        {
            throw new SponsoredEventsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        public virtual Task<RemoveUserResponse> RemoveUserAsync(RemoveUserRequest request, BlazeRpcContext context)
        {
            throw new SponsoredEventsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        public virtual Task<UpdateEventDataResponse> UpdateEventDataAsync(UpdateEventDataRequest request, BlazeRpcContext context)
        {
            throw new SponsoredEventsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        public virtual Task<DbCredentialsResponse> GetDbCredentialsAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new SponsoredEventsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        public virtual Task<ReturnUsersResponse> ReturnUsersAsync(ReturnUsersRequest request, BlazeRpcContext context)
        {
            throw new SponsoredEventsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }
    }
}
