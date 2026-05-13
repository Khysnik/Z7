using Blaze.Core;
using Blaze15SDK.Blaze.Stats;
using EATDF;
using EATDF.Types;

namespace Blaze15SDK.Components;

public static class StatsComponentBase
{
    public const ushort Id = 7;
    public const string Name = "StatsComponent";

    public enum Error : ushort
    {
        STATS_ERR_CONFIG_NOTAVAILABLE = 1,
        STATS_ERR_INVALID_LEADERBOARD_ID = 2,
        STATS_ERR_INVALID_FOLDER_ID = 3,
        STATS_ERR_UNKNOWN_CATEGORY = 4,
        STATS_ERR_STAT_NOT_FOUND = 5,
        STATS_ERR_BAD_PERIOD_TYPE = 6,
        STATS_ERR_NO_DB_CONNECTION = 7,
        STATS_ERR_DB_DATA_NOT_AVAILABLE = 8,
        STATS_ERR_UNKNOWN_STAT_GROUP = 9,
        STATS_ERR_DB_TRANSACTION_ERROR = 10,
        STATS_ERR_INVALID_UPDATE_TYPE = 11,
        STATS_ERR_DB_QUERY_FAILED = 13,
        STATS_ERR_RANK_OUT_OF_RANGE = 14,
        STATS_ERR_BAD_PERIOD_OFFSET = 15,
        STATS_ERR_BAD_SCOPE_INFO = 16,
        STATS_ERR_INVALID_FOLDER_NAME = 17,
        STATS_ERR_OPERATION_IN_PROGRESS = 18,
        STATS_ERR_INVALID_OPERATION = 20,
        STATS_ERR_INVALID_OBJECT_ID = 21,
        STATS_ERR_BAD_PERIOD_COUNTER = 22,
        STATS_ERR_LEADERBOARD_NOT_IN_MEMORY = 23
    }

    public enum StatsCommand : ushort
    {
        getStatDescs = 1,
        getStats = 2,
        getStatGroupList = 3,
        getStatGroup = 4,
        getStatsByGroup = 5,
        getDateRange = 6,
        getEntityCount = 7,
        updateStats = 8,
        wipeStats = 9,
        getLeaderboardGroup = 10,
        getLeaderboardFolderGroup = 11,
        getLeaderboard = 12,
        getCenteredLeaderboard = 13,
        getFilteredLeaderboard = 14,
        getKeyScopesMap = 15,
        getStatsByGroupAsync = 16,
        getLeaderboardTreeAsync = 17,
        getLeaderboardEntityCount = 18,
        getStatCategoryList = 19,
        getPeriodIds = 20,
        getLeaderboardRaw = 21,
        getCenteredLeaderboardRaw = 22,
        getFilteredLeaderboardRaw = 23,
        changeKeyscopeValue = 24,
        getEntityRank = 25,
        initializeStatsTransaction = 26,
        commitStatsTransaction = 27,
        abortStatsTransaction = 28,
        calcDerivedStats = 29,
        retrieveValuesStats = 30,
        getStatsByGroupAsync2 = 162,
        getLeaderboardTreeAsync2 = 172,
        getLeaderboardRaw2 = 212,
        getCenteredLeaderboardRaw2 = 222,
        getFilteredLeaderboardRaw2 = 232
    }

    public enum StatsMasterCommand : ushort
    {
        getPeriodIdsMaster = 3,
        populateLeaderboardIndex = 4,
        reportGlobalCacheInstructionExecutionResult = 6,
        initializeGlobalCache = 10
    }

    public enum StatsNotification : ushort
    {
        UpdateCacheNotification = 1,
        UpdateGlobalStatsNotification = 2,
        GetStatsAsyncNotification = 50,
        GetLeaderboardTreeNotification = 51
    }

    public enum StatsMasterNotification : ushort
    {
        TrimPeriodNotification = 4,
        SetPeriodIdsNotification = 5,
        ExecuteGlobalCacheInstruction = 6
    }

    public class Server : BlazeComponent
    {
        public override ushort Id => StatsComponentBase.Id;
        public override string Name => StatsComponentBase.Name;

        public virtual bool IsCommandSupported(StatsCommand command) => false;

        public class StatsException : BlazeRpcException
        {
            public StatsException(Error error) : base((ushort)error, null) { }
            public StatsException(ServerError error) : base(error.WithErrorPrefix(), null) { }
            public StatsException(Error error, Tdf? errorResponse) : base((ushort)error, errorResponse) { }
            public StatsException(ServerError error, Tdf? errorResponse) : base(error.WithErrorPrefix(), errorResponse) { }
            public StatsException(Error error, Tdf? errorResponse, string? message) : base((ushort)error, errorResponse, message) { }
            public StatsException(ServerError error, Tdf? errorResponse, string? message) : base(error.WithErrorPrefix(), errorResponse, message) { }
            public StatsException(Error error, Tdf? errorResponse, string? message, Exception? innerException) : base((ushort)error, errorResponse, message, innerException) { }
            public StatsException(ServerError error, Tdf? errorResponse, string? message, Exception? innerException) : base(error.WithErrorPrefix(), errorResponse, message, innerException) { }
        }

        public Server()
        {
            // Slave commands
            RegisterCommand(new RpcCommandFunc<GetStatDescsRequest, StatDescs, EmptyMessage>()
            {
                Id = (ushort)StatsCommand.getStatDescs,
                Name = "getStatDescs",
                IsSupported = IsCommandSupported(StatsCommand.getStatDescs),
                Func = async (req, ctx) => await GetStatDescsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetStatsRequest, GetStatsResponse, EmptyMessage>()
            {
                Id = (ushort)StatsCommand.getStats,
                Name = "getStats",
                IsSupported = IsCommandSupported(StatsCommand.getStats),
                Func = async (req, ctx) => await GetStatsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, StatGroupList, EmptyMessage>()
            {
                Id = (ushort)StatsCommand.getStatGroupList,
                Name = "getStatGroupList",
                IsSupported = IsCommandSupported(StatsCommand.getStatGroupList),
                Func = async (req, ctx) => await GetStatGroupListAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetStatGroupRequest, StatGroupResponse, EmptyMessage>()
            {
                Id = (ushort)StatsCommand.getStatGroup,
                Name = "getStatGroup",
                IsSupported = IsCommandSupported(StatsCommand.getStatGroup),
                Func = async (req, ctx) => await GetStatGroupAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetStatsByGroupRequest, GetStatsResponse, EmptyMessage>()
            {
                Id = (ushort)StatsCommand.getStatsByGroup,
                Name = "getStatsByGroup",
                IsSupported = IsCommandSupported(StatsCommand.getStatsByGroup),
                Func = async (req, ctx) => await GetStatsByGroupAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetDateRangeRequest, DateRange, EmptyMessage>()
            {
                Id = (ushort)StatsCommand.getDateRange,
                Name = "getDateRange",
                IsSupported = IsCommandSupported(StatsCommand.getDateRange),
                Func = async (req, ctx) => await GetDateRangeAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetEntityCountRequest, EntityCount, EmptyMessage>()
            {
                Id = (ushort)StatsCommand.getEntityCount,
                Name = "getEntityCount",
                IsSupported = IsCommandSupported(StatsCommand.getEntityCount),
                Func = async (req, ctx) => await GetEntityCountAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UpdateStatsRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)StatsCommand.updateStats,
                Name = "updateStats",
                IsSupported = IsCommandSupported(StatsCommand.updateStats),
                Func = async (req, ctx) => await UpdateStatsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<WipeStatsRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)StatsCommand.wipeStats,
                Name = "wipeStats",
                IsSupported = IsCommandSupported(StatsCommand.wipeStats),
                Func = async (req, ctx) => await WipeStatsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<LeaderboardGroupRequest, LeaderboardGroupResponse, EmptyMessage>()
            {
                Id = (ushort)StatsCommand.getLeaderboardGroup,
                Name = "getLeaderboardGroup",
                IsSupported = IsCommandSupported(StatsCommand.getLeaderboardGroup),
                Func = async (req, ctx) => await GetLeaderboardGroupAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<LeaderboardFolderGroupRequest, LeaderboardFolderGroup, EmptyMessage>()
            {
                Id = (ushort)StatsCommand.getLeaderboardFolderGroup,
                Name = "getLeaderboardFolderGroup",
                IsSupported = IsCommandSupported(StatsCommand.getLeaderboardFolderGroup),
                Func = async (req, ctx) => await GetLeaderboardFolderGroupAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<LeaderboardStatsRequest, LeaderboardStatValues, EmptyMessage>()
            {
                Id = (ushort)StatsCommand.getLeaderboard,
                Name = "getLeaderboard",
                IsSupported = IsCommandSupported(StatsCommand.getLeaderboard),
                Func = async (req, ctx) => await GetLeaderboardAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<CenteredLeaderboardStatsRequest, LeaderboardStatValues, EmptyMessage>()
            {
                Id = (ushort)StatsCommand.getCenteredLeaderboard,
                Name = "getCenteredLeaderboard",
                IsSupported = IsCommandSupported(StatsCommand.getCenteredLeaderboard),
                Func = async (req, ctx) => await GetCenteredLeaderboardAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<FilteredLeaderboardStatsRequest, LeaderboardStatValues, EmptyMessage>()
            {
                Id = (ushort)StatsCommand.getFilteredLeaderboard,
                Name = "getFilteredLeaderboard",
                IsSupported = IsCommandSupported(StatsCommand.getFilteredLeaderboard),
                Func = async (req, ctx) => await GetFilteredLeaderboardAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, KeyScopes, EmptyMessage>()
            {
                Id = (ushort)StatsCommand.getKeyScopesMap,
                Name = "getKeyScopesMap",
                IsSupported = IsCommandSupported(StatsCommand.getKeyScopesMap),
                Func = async (req, ctx) => await GetKeyScopesMapAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetStatsByGroupRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)StatsCommand.getStatsByGroupAsync,
                Name = "getStatsByGroupAsync",
                IsSupported = IsCommandSupported(StatsCommand.getStatsByGroupAsync),
                Func = async (req, ctx) => await GetStatsByGroupAsyncAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetLeaderboardTreeRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)StatsCommand.getLeaderboardTreeAsync,
                Name = "getLeaderboardTreeAsync",
                IsSupported = IsCommandSupported(StatsCommand.getLeaderboardTreeAsync),
                Func = async (req, ctx) => await GetLeaderboardTreeAsyncAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<LeaderboardEntityCountRequest, EntityCount, EmptyMessage>()
            {
                Id = (ushort)StatsCommand.getLeaderboardEntityCount,
                Name = "getLeaderboardEntityCount",
                IsSupported = IsCommandSupported(StatsCommand.getLeaderboardEntityCount),
                Func = async (req, ctx) => await GetLeaderboardEntityCountAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, StatCategoryList, EmptyMessage>()
            {
                Id = (ushort)StatsCommand.getStatCategoryList,
                Name = "getStatCategoryList",
                IsSupported = IsCommandSupported(StatsCommand.getStatCategoryList),
                Func = async (req, ctx) => await GetStatCategoryListAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, PeriodIds, EmptyMessage>()
            {
                Id = (ushort)StatsCommand.getPeriodIds,
                Name = "getPeriodIds",
                IsSupported = IsCommandSupported(StatsCommand.getPeriodIds),
                Func = async (req, ctx) => await GetPeriodIdsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<LeaderboardStatsRequest, LeaderboardStatValues, EmptyMessage>()
            {
                Id = (ushort)StatsCommand.getLeaderboardRaw,
                Name = "getLeaderboardRaw",
                IsSupported = IsCommandSupported(StatsCommand.getLeaderboardRaw),
                Func = async (req, ctx) => await GetLeaderboardRawAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<CenteredLeaderboardStatsRequest, LeaderboardStatValues, EmptyMessage>()
            {
                Id = (ushort)StatsCommand.getCenteredLeaderboardRaw,
                Name = "getCenteredLeaderboardRaw",
                IsSupported = IsCommandSupported(StatsCommand.getCenteredLeaderboardRaw),
                Func = async (req, ctx) => await GetCenteredLeaderboardRawAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<FilteredLeaderboardStatsRequest, LeaderboardStatValues, EmptyMessage>()
            {
                Id = (ushort)StatsCommand.getFilteredLeaderboardRaw,
                Name = "getFilteredLeaderboardRaw",
                IsSupported = IsCommandSupported(StatsCommand.getFilteredLeaderboardRaw),
                Func = async (req, ctx) => await GetFilteredLeaderboardRawAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<KeyScopeChangeRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)StatsCommand.changeKeyscopeValue,
                Name = "changeKeyscopeValue",
                IsSupported = IsCommandSupported(StatsCommand.changeKeyscopeValue),
                Func = async (req, ctx) => await ChangeKeyscopeValueAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<FilteredLeaderboardStatsRequest, GetEntityRankResponse, EmptyMessage>()
            {
                Id = (ushort)StatsCommand.getEntityRank,
                Name = "getEntityRank",
                IsSupported = IsCommandSupported(StatsCommand.getEntityRank),
                Func = async (req, ctx) => await GetEntityRankAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)StatsCommand.initializeStatsTransaction,
                Name = "initializeStatsTransaction",
                IsSupported = IsCommandSupported(StatsCommand.initializeStatsTransaction),
                Func = async (req, ctx) => await InitializeStatsTransactionAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)StatsCommand.commitStatsTransaction,
                Name = "commitStatsTransaction",
                IsSupported = IsCommandSupported(StatsCommand.commitStatsTransaction),
                Func = async (req, ctx) => await CommitStatsTransactionAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)StatsCommand.abortStatsTransaction,
                Name = "abortStatsTransaction",
                IsSupported = IsCommandSupported(StatsCommand.abortStatsTransaction),
                Func = async (req, ctx) => await AbortStatsTransactionAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)StatsCommand.calcDerivedStats,
                Name = "calcDerivedStats",
                IsSupported = IsCommandSupported(StatsCommand.calcDerivedStats),
                Func = async (req, ctx) => await CalcDerivedStatsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)StatsCommand.retrieveValuesStats,
                Name = "retrieveValuesStats",
                IsSupported = IsCommandSupported(StatsCommand.retrieveValuesStats),
                Func = async (req, ctx) => await RetrieveValuesStatsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetStatsByGroupRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)StatsCommand.getStatsByGroupAsync2,
                Name = "getStatsByGroupAsync2",
                IsSupported = IsCommandSupported(StatsCommand.getStatsByGroupAsync2),
                Func = async (req, ctx) => await GetStatsByGroupAsync2Async(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetLeaderboardTreeRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)StatsCommand.getLeaderboardTreeAsync2,
                Name = "getLeaderboardTreeAsync2",
                IsSupported = IsCommandSupported(StatsCommand.getLeaderboardTreeAsync2),
                Func = async (req, ctx) => await GetLeaderboardTreeAsync2Async(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<LeaderboardStatsRequest, LeaderboardStatValues, EmptyMessage>()
            {
                Id = (ushort)StatsCommand.getLeaderboardRaw2,
                Name = "getLeaderboardRaw2",
                IsSupported = IsCommandSupported(StatsCommand.getLeaderboardRaw2),
                Func = async (req, ctx) => await GetLeaderboardRaw2Async(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<CenteredLeaderboardStatsRequest, LeaderboardStatValues, EmptyMessage>()
            {
                Id = (ushort)StatsCommand.getCenteredLeaderboardRaw2,
                Name = "getCenteredLeaderboardRaw2",
                IsSupported = IsCommandSupported(StatsCommand.getCenteredLeaderboardRaw2),
                Func = async (req, ctx) => await GetCenteredLeaderboardRaw2Async(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<FilteredLeaderboardStatsRequest, LeaderboardStatValues, EmptyMessage>()
            {
                Id = (ushort)StatsCommand.getFilteredLeaderboardRaw2,
                Name = "getFilteredLeaderboardRaw2",
                IsSupported = IsCommandSupported(StatsCommand.getFilteredLeaderboardRaw2),
                Func = async (req, ctx) => await GetFilteredLeaderboardRaw2Async(req, ctx).ConfigureAwait(false)
            });

            // Master commands
            RegisterCommand(new RpcCommandFunc<EmptyMessage, PeriodIds, EmptyMessage>()
            {
                Id = (ushort)StatsMasterCommand.getPeriodIdsMaster,
                Name = "getPeriodIdsMaster",
                IsSupported = true,
                Func = async (req, ctx) => await GetPeriodIdsMasterAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)StatsMasterCommand.populateLeaderboardIndex,
                Name = "populateLeaderboardIndex",
                IsSupported = true,
                Func = async (req, ctx) => await PopulateLeaderboardIndexAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)StatsMasterCommand.reportGlobalCacheInstructionExecutionResult,
                Name = "reportGlobalCacheInstructionExecutionResult",
                IsSupported = true,
                Func = async (req, ctx) => await ReportGlobalCacheInstructionExecutionResultAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)StatsMasterCommand.initializeGlobalCache,
                Name = "initializeGlobalCache",
                IsSupported = true,
                Func = async (req, ctx) => await InitializeGlobalCacheAsync(req, ctx).ConfigureAwait(false)
            });

        }

        public override string GetErrorName(ushort errorCode)
        {
            return ((Error)errorCode).ToString();
        }

        // ===== Slave command handlers =====

        /// <summary>
        /// This method is called when server receives a <b>StatsComponent::getStatDescs</b> command.<br/>
        /// Request type: <see cref="GetStatDescsRequest"/><br/>
        /// Response type: <see cref="StatDescs"/><br/>
        /// </summary>
        public virtual Task<StatDescs> GetStatDescsAsync(GetStatDescsRequest request, BlazeRpcContext context)
        {
            throw new StatsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>StatsComponent::getStats</b> command.<br/>
        /// Request type: <see cref="GetStatsRequest"/><br/>
        /// Response type: <see cref="GetStatsResponse"/><br/>
        /// </summary>
        public virtual Task<GetStatsResponse> GetStatsAsync(GetStatsRequest request, BlazeRpcContext context)
        {
            throw new StatsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>StatsComponent::getStatGroupList</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="StatGroupList"/><br/>
        /// </summary>
        public virtual Task<StatGroupList> GetStatGroupListAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new StatsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>StatsComponent::getStatGroup</b> command.<br/>
        /// Request type: <see cref="GetStatGroupRequest"/><br/>
        /// Response type: <see cref="StatGroupResponse"/><br/>
        /// </summary>
        public virtual Task<StatGroupResponse> GetStatGroupAsync(GetStatGroupRequest request, BlazeRpcContext context)
        {
            throw new StatsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>StatsComponent::getStatsByGroup</b> command.<br/>
        /// Request type: <see cref="GetStatsByGroupRequest"/><br/>
        /// Response type: <see cref="GetStatsResponse"/><br/>
        /// </summary>
        public virtual Task<GetStatsResponse> GetStatsByGroupAsync(GetStatsByGroupRequest request, BlazeRpcContext context)
        {
            throw new StatsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>StatsComponent::getDateRange</b> command.<br/>
        /// Request type: <see cref="GetDateRangeRequest"/><br/>
        /// Response type: <see cref="DateRange"/><br/>
        /// </summary>
        public virtual Task<DateRange> GetDateRangeAsync(GetDateRangeRequest request, BlazeRpcContext context)
        {
            throw new StatsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>StatsComponent::getEntityCount</b> command.<br/>
        /// Request type: <see cref="GetEntityCountRequest"/><br/>
        /// Response type: <see cref="EntityCount"/><br/>
        /// </summary>
        public virtual Task<EntityCount> GetEntityCountAsync(GetEntityCountRequest request, BlazeRpcContext context)
        {
            throw new StatsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>StatsComponent::updateStats</b> command.<br/>
        /// Request type: <see cref="UpdateStatsRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> UpdateStatsAsync(UpdateStatsRequest request, BlazeRpcContext context)
        {
            throw new StatsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>StatsComponent::wipeStats</b> command.<br/>
        /// Request type: <see cref="WipeStatsRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> WipeStatsAsync(WipeStatsRequest request, BlazeRpcContext context)
        {
            throw new StatsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>StatsComponent::getLeaderboardGroup</b> command.<br/>
        /// Request type: <see cref="LeaderboardGroupRequest"/><br/>
        /// Response type: <see cref="LeaderboardGroupResponse"/><br/>
        /// </summary>
        public virtual Task<LeaderboardGroupResponse> GetLeaderboardGroupAsync(LeaderboardGroupRequest request, BlazeRpcContext context)
        {
            throw new StatsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>StatsComponent::getLeaderboardFolderGroup</b> command.<br/>
        /// Request type: <see cref="LeaderboardFolderGroupRequest"/><br/>
        /// Response type: <see cref="LeaderboardFolderGroup"/><br/>
        /// </summary>
        public virtual Task<LeaderboardFolderGroup> GetLeaderboardFolderGroupAsync(LeaderboardFolderGroupRequest request, BlazeRpcContext context)
        {
            throw new StatsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>StatsComponent::getLeaderboard</b> command.<br/>
        /// Request type: <see cref="LeaderboardStatsRequest"/><br/>
        /// Response type: <see cref="LeaderboardStatValues"/><br/>
        /// </summary>
        public virtual Task<LeaderboardStatValues> GetLeaderboardAsync(LeaderboardStatsRequest request, BlazeRpcContext context)
        {
            throw new StatsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>StatsComponent::getCenteredLeaderboard</b> command.<br/>
        /// Request type: <see cref="CenteredLeaderboardStatsRequest"/><br/>
        /// Response type: <see cref="LeaderboardStatValues"/><br/>
        /// </summary>
        public virtual Task<LeaderboardStatValues> GetCenteredLeaderboardAsync(CenteredLeaderboardStatsRequest request, BlazeRpcContext context)
        {
            throw new StatsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>StatsComponent::getFilteredLeaderboard</b> command.<br/>
        /// Request type: <see cref="FilteredLeaderboardStatsRequest"/><br/>
        /// Response type: <see cref="LeaderboardStatValues"/><br/>
        /// </summary>
        public virtual Task<LeaderboardStatValues> GetFilteredLeaderboardAsync(FilteredLeaderboardStatsRequest request, BlazeRpcContext context)
        {
            throw new StatsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>StatsComponent::getKeyScopesMap</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="KeyScopes"/><br/>
        /// </summary>
        public virtual Task<KeyScopes> GetKeyScopesMapAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new StatsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>StatsComponent::getStatsByGroupAsync</b> command.<br/>
        /// Request type: <see cref="GetStatsByGroupRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> GetStatsByGroupAsyncAsync(GetStatsByGroupRequest request, BlazeRpcContext context)
        {
            throw new StatsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>StatsComponent::getLeaderboardTreeAsync</b> command.<br/>
        /// Request type: <see cref="GetLeaderboardTreeRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> GetLeaderboardTreeAsyncAsync(GetLeaderboardTreeRequest request, BlazeRpcContext context)
        {
            throw new StatsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>StatsComponent::getLeaderboardEntityCount</b> command.<br/>
        /// Request type: <see cref="LeaderboardEntityCountRequest"/><br/>
        /// Response type: <see cref="EntityCount"/><br/>
        /// </summary>
        public virtual Task<EntityCount> GetLeaderboardEntityCountAsync(LeaderboardEntityCountRequest request, BlazeRpcContext context)
        {
            throw new StatsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>StatsComponent::getStatCategoryList</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="StatCategoryList"/><br/>
        /// </summary>
        public virtual Task<StatCategoryList> GetStatCategoryListAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new StatsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>StatsComponent::getPeriodIds</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="PeriodIds"/><br/>
        /// </summary>
        public virtual Task<PeriodIds> GetPeriodIdsAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new StatsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>StatsComponent::getLeaderboardRaw</b> command.<br/>
        /// Request type: <see cref="LeaderboardStatsRequest"/><br/>
        /// Response type: <see cref="LeaderboardStatValues"/><br/>
        /// </summary>
        public virtual Task<LeaderboardStatValues> GetLeaderboardRawAsync(LeaderboardStatsRequest request, BlazeRpcContext context)
        {
            throw new StatsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>StatsComponent::getCenteredLeaderboardRaw</b> command.<br/>
        /// Request type: <see cref="CenteredLeaderboardStatsRequest"/><br/>
        /// Response type: <see cref="LeaderboardStatValues"/><br/>
        /// </summary>
        public virtual Task<LeaderboardStatValues> GetCenteredLeaderboardRawAsync(CenteredLeaderboardStatsRequest request, BlazeRpcContext context)
        {
            throw new StatsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>StatsComponent::getFilteredLeaderboardRaw</b> command.<br/>
        /// Request type: <see cref="FilteredLeaderboardStatsRequest"/><br/>
        /// Response type: <see cref="LeaderboardStatValues"/><br/>
        /// </summary>
        public virtual Task<LeaderboardStatValues> GetFilteredLeaderboardRawAsync(FilteredLeaderboardStatsRequest request, BlazeRpcContext context)
        {
            throw new StatsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>StatsComponent::changeKeyscopeValue</b> command.<br/>
        /// Request type: <see cref="KeyScopeChangeRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> ChangeKeyscopeValueAsync(KeyScopeChangeRequest request, BlazeRpcContext context)
        {
            throw new StatsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>StatsComponent::getEntityRank</b> command.<br/>
        /// Request type: <see cref="FilteredLeaderboardStatsRequest"/><br/>
        /// Response type: <see cref="GetEntityRankResponse"/><br/>
        /// </summary>
        public virtual Task<GetEntityRankResponse> GetEntityRankAsync(FilteredLeaderboardStatsRequest request, BlazeRpcContext context)
        {
            throw new StatsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>StatsComponent::initializeStatsTransaction</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> InitializeStatsTransactionAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new StatsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>StatsComponent::commitStatsTransaction</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> CommitStatsTransactionAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new StatsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>StatsComponent::abortStatsTransaction</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> AbortStatsTransactionAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new StatsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>StatsComponent::calcDerivedStats</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> CalcDerivedStatsAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new StatsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>StatsComponent::retrieveValuesStats</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> RetrieveValuesStatsAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new StatsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>StatsComponent::getStatsByGroupAsync2</b> command.<br/>
        /// Request type: <see cref="GetStatsByGroupRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> GetStatsByGroupAsync2Async(GetStatsByGroupRequest request, BlazeRpcContext context)
        {
            throw new StatsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>StatsComponent::getLeaderboardTreeAsync2</b> command.<br/>
        /// Request type: <see cref="GetLeaderboardTreeRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> GetLeaderboardTreeAsync2Async(GetLeaderboardTreeRequest request, BlazeRpcContext context)
        {
            throw new StatsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>StatsComponent::getLeaderboardRaw2</b> command.<br/>
        /// Request type: <see cref="LeaderboardStatsRequest"/><br/>
        /// Response type: <see cref="LeaderboardStatValues"/><br/>
        /// </summary>
        public virtual Task<LeaderboardStatValues> GetLeaderboardRaw2Async(LeaderboardStatsRequest request, BlazeRpcContext context)
        {
            throw new StatsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>StatsComponent::getCenteredLeaderboardRaw2</b> command.<br/>
        /// Request type: <see cref="CenteredLeaderboardStatsRequest"/><br/>
        /// Response type: <see cref="LeaderboardStatValues"/><br/>
        /// </summary>
        public virtual Task<LeaderboardStatValues> GetCenteredLeaderboardRaw2Async(CenteredLeaderboardStatsRequest request, BlazeRpcContext context)
        {
            throw new StatsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>StatsComponent::getFilteredLeaderboardRaw2</b> command.<br/>
        /// Request type: <see cref="FilteredLeaderboardStatsRequest"/><br/>
        /// Response type: <see cref="LeaderboardStatValues"/><br/>
        /// </summary>
        public virtual Task<LeaderboardStatValues> GetFilteredLeaderboardRaw2Async(FilteredLeaderboardStatsRequest request, BlazeRpcContext context)
        {
            throw new StatsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        // ===== Master command handlers =====

        /// <summary>
        /// This method is called when server receives a <b>StatsComponent::getPeriodIdsMaster</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="PeriodIds"/><br/>
        /// </summary>
        public virtual Task<PeriodIds> GetPeriodIdsMasterAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new StatsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>StatsComponent::populateLeaderboardIndex</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> PopulateLeaderboardIndexAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new StatsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>StatsComponent::reportGlobalCacheInstructionExecutionResult</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> ReportGlobalCacheInstructionExecutionResultAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new StatsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>StatsComponent::initializeGlobalCache</b> master command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> InitializeGlobalCacheAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new StatsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        // ===== Notification helpers =====

        /// <summary>
        /// Call this method when you want to send to client a <b>StatsComponent::UpdateCacheNotification</b> notification.<br/>
        /// Notification type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public static Task NotifyUpdateCacheNotificationAsync(BlazeRpcConnection connection, EmptyMessage notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = StatsComponentBase.Id;
                frame.Command = (ushort)StatsNotification.UpdateCacheNotification;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>StatsComponent::UpdateGlobalStatsNotification</b> notification.<br/>
        /// Notification type: <see cref="UpdateStatsRequest"/><br/>
        /// </summary>
        public static Task NotifyUpdateGlobalStatsNotificationAsync(BlazeRpcConnection connection, UpdateStatsRequest notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = StatsComponentBase.Id;
                frame.Command = (ushort)StatsNotification.UpdateGlobalStatsNotification;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>StatsComponent::GetStatsAsyncNotification</b> notification.<br/>
        /// Notification type: <see cref="KeyScopedStatValues"/><br/>
        /// </summary>
        public static Task NotifyGetStatsAsyncNotificationAsync(BlazeRpcConnection connection, KeyScopedStatValues notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = StatsComponentBase.Id;
                frame.Command = (ushort)StatsNotification.GetStatsAsyncNotification;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>StatsComponent::GetLeaderboardTreeNotification</b> notification.<br/>
        /// Notification type: <see cref="LeaderboardTreeNodes"/><br/>
        /// </summary>
        public static Task NotifyGetLeaderboardTreeNotificationAsync(BlazeRpcConnection connection, LeaderboardTreeNodes notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = StatsComponentBase.Id;
                frame.Command = (ushort)StatsNotification.GetLeaderboardTreeNotification;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        // ===== Master notification helpers =====

        /// <summary>
        /// Call this method when you want to send a <b>StatsComponent::TrimPeriodNotification</b> master notification.<br/>
        /// Notification type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public static Task NotifyTrimPeriodNotificationMasterAsync(BlazeRpcConnection connection, EmptyMessage notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = StatsComponentBase.Id;
                frame.Command = (ushort)StatsMasterNotification.TrimPeriodNotification;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send a <b>StatsComponent::SetPeriodIdsNotification</b> master notification.<br/>
        /// Notification type: <see cref="PeriodIds"/><br/>
        /// </summary>
        public static Task NotifySetPeriodIdsNotificationMasterAsync(BlazeRpcConnection connection, PeriodIds notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = StatsComponentBase.Id;
                frame.Command = (ushort)StatsMasterNotification.SetPeriodIdsNotification;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send a <b>StatsComponent::ExecuteGlobalCacheInstruction</b> master notification.<br/>
        /// Notification type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public static Task NotifyExecuteGlobalCacheInstructionMasterAsync(BlazeRpcConnection connection, EmptyMessage notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = StatsComponentBase.Id;
                frame.Command = (ushort)StatsMasterNotification.ExecuteGlobalCacheInstruction;
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
