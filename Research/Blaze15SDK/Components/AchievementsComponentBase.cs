using Blaze.Core;
using Blaze15SDK.Blaze.Achievements;
using EATDF;
using EATDF.Types;

namespace Blaze15SDK.Components;

public static class AchievementsComponentBase
{
    public const ushort Id = 33;
    public const string Name = "AchievementsComponent";

    public enum Error : ushort {
        ACHIEVEMENTS_ERR_INVALID_PARAMETER = 1,
        ACHIEVEMENTS_ERR_INVALID_CREDENTIALS = 2,
        ACHIEVEMENTS_ERR_NOT_AUTHORIZED = 3,
        ACHIEVEMENTS_ERR_NOT_ALLOWED = 4,
    }

    public enum AchievementsCommand : ushort
    {
        getAchievements = 1,
        grantAchievement = 3,
        postEvents = 6,
    }

    public class Server : BlazeComponent
    {
        public override ushort Id => AchievementsComponentBase.Id;
        public override string Name => AchievementsComponentBase.Name;

        public virtual bool IsCommandSupported(AchievementsCommand command) => false;

        public class AchievementsException : BlazeRpcException
        {
            public AchievementsException(Error error) : base((ushort)error, null) { }
            public AchievementsException(ServerError error) : base(error.WithErrorPrefix(), null) { }
            public AchievementsException(Error error, Tdf? errorResponse) : base((ushort)error, errorResponse) { }
            public AchievementsException(ServerError error, Tdf? errorResponse) : base(error.WithErrorPrefix(), errorResponse) { }
            public AchievementsException(Error error, Tdf? errorResponse, string? message) : base((ushort)error, errorResponse, message) { }
            public AchievementsException(ServerError error, Tdf? errorResponse, string? message) : base(error.WithErrorPrefix(), errorResponse, message) { }
            public AchievementsException(Error error, Tdf? errorResponse, string? message, Exception? innerException) : base((ushort)error, errorResponse, message, innerException) { }
            public AchievementsException(ServerError error, Tdf? errorResponse, string? message, Exception? innerException) : base(error.WithErrorPrefix(), errorResponse, message, innerException) { }
        }

        public Server()
        {
            // Slave commands
            RegisterCommand(new RpcCommandFunc<GetAchievementsRequest, GetAchievementsResponse, EmptyMessage>()
            {
                Id = (ushort)AchievementsCommand.getAchievements,
                Name = "getAchievements",
                IsSupported = IsCommandSupported(AchievementsCommand.getAchievements),
                Func = async (req, ctx) => await GetAchievementsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GrantAchievementRequest, AchievementData, EmptyMessage>()
            {
                Id = (ushort)AchievementsCommand.grantAchievement,
                Name = "grantAchievement",
                IsSupported = IsCommandSupported(AchievementsCommand.grantAchievement),
                Func = async (req, ctx) => await GrantAchievementAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<PostEventsRequest, GetAchievementsResponse, EmptyMessage>()
            {
                Id = (ushort)AchievementsCommand.postEvents,
                Name = "postEvents",
                IsSupported = IsCommandSupported(AchievementsCommand.postEvents),
                Func = async (req, ctx) => await PostEventsAsync(req, ctx).ConfigureAwait(false)
            });

        }

        public override string GetErrorName(ushort errorCode)
        {
            return ((Error)errorCode).ToString();
        }

        // ===== Slave command handlers =====

        /// <summary>
        /// This method is called when server receives a <b>AchievementsComponent::getAchievements</b> command.<br/>
        /// Request type: <see cref="GetAchievementsRequest"/><br/>
        /// Response type: <see cref="GetAchievementsResponse"/><br/>
        /// </summary>
        public virtual Task<GetAchievementsResponse> GetAchievementsAsync(GetAchievementsRequest request, BlazeRpcContext context)
        {
            throw new AchievementsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>AchievementsComponent::grantAchievement</b> command.<br/>
        /// Request type: <see cref="GrantAchievementRequest"/><br/>
        /// Response type: <see cref="AchievementData"/><br/>
        /// </summary>
        public virtual Task<AchievementData> GrantAchievementAsync(GrantAchievementRequest request, BlazeRpcContext context)
        {
            throw new AchievementsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>AchievementsComponent::postEvents</b> command.<br/>
        /// Request type: <see cref="PostEventsRequest"/><br/>
        /// Response type: <see cref="GetAchievementsResponse"/><br/>
        /// </summary>
        public virtual Task<GetAchievementsResponse> PostEventsAsync(PostEventsRequest request, BlazeRpcContext context)
        {
            throw new AchievementsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

    }

}
