using Blaze.Core;
using Blaze15SDK.Blaze.Clubs;
using EATDF;
using EATDF.Types;

namespace Blaze15SDK.Components;

public static class ClubsComponentBase
{
    public const ushort Id = 11;
    public const string Name = "ClubsComponent";

    public enum Error : ushort {
        CLUBS_ERR_INVALID_ARGUMENT = 1002,
        CLUBS_ERR_MAX_CLUBS = 1003,
        CLUBS_ERR_CLUB_NAME_IN_USE = 1004,
        CLUBS_ERR_PROFANITY_FILTER = 1005,
        CLUBS_ERR_NO_PRIVILEGE = 1007,
        CLUBS_ERR_INVALID_USER_ID = 1008,
        CLUBS_ERR_INVALID_CLUB_ID = 1009,
        CLUBS_ERR_TOO_MANY_ITEMS_PER_FETCH_REQUESTED = 1010,
        CLUBS_ERR_INVALID_CLUBNAME_SIZE = 1011,
        CLUBS_ERR_INVALID_NON_UNIQUE_NAME_SIZE = 1012,
        CLUBS_ERR_INVALID_DOMAIN_ID = 1013,
        CLUBS_ERR_INVALID_CLUBNAME_EMPTY = 1201,
        CLUBS_ERR_INVALID_NON_UNIQUE_NAME_EMPTY = 1202,
        CLUBS_ERR_USER_NOT_MEMBER = 1301,
        CLUBS_ERR_LAST_GM_CANNOT_LEAVE = 1302,
        CLUBS_ERR_CANNOT_KICK_OWNER = 1303,
        CLUBS_ERR_ALREADY_GM = 1304,
        CLUBS_ERR_MAX_INVITES_SENT = 1305,
        CLUBS_ERR_MAX_INVITES_RECEIVED = 1306,
        CLUBS_ERR_MAX_PETITIONS_SENT = 1307,
        CLUBS_ERR_MAX_PETITIONS_RECEIVED = 1308,
        CLUBS_ERR_MAX_MESSAGES_SENT = 1309,
        CLUBS_ERR_MAX_MESSAGES_RECEIVED = 1310,
        CLUBS_ERR_CLUB_FULL = 1311,
        CLUBS_ERR_TOO_MANY_GMS = 1312,
        CLUBS_ERR_INVITATION_ALREADY_SENT = 1313,
        CLUBS_ERR_DEMOTE_MEMBER = 1350,
        CLUBS_ERR_DEMOTE_OWNER = 1351,
        CLUBS_ERR_DEMOTE_LAST_GM = 1352,
        CLUBS_ERR_TRANSFER_OWNERSHIP_TO_OWNER = 1361,
        CLUBS_ERR_ALREADY_MEMBER = 1401,
        CLUBS_ERR_PETITION_DISABLED = 1402,
        CLUBS_ERR_PETITION_ALREADY_SENT = 1403,
        CLUBS_ERR_JOIN_DISABLED = 1404,
        CLUBS_ERR_JOIN_AND_PETITION_NOT_ALLOWED = 1405,
        CLUBS_ERR_JUMP_TOO_FREQUENTLY = 1406,
        CLUBS_ERR_MISSING_NEWS_TYPE_FILTER = 1501,
        CLUBS_ERR_TOO_MANY_PARAMETERS = 1502,
        CLUBS_ERR_NEWS_TEXT_OR_STRINGID_MUST_BE_EMPTY = 1503,
        CLUBS_ERR_ASSOCIATE_CLUB_ID_MUST_BE_ZERO = 1504,
        CLUBS_ERR_NEWS_ITEM_NOT_FOUND = 1505,
        CLUBS_ERR_DUPLICATE_RIVALS = 1601,
        CLUBS_ERR_USER_BANNED = 1701,
        CLUBS_ERR_USER_CANNOT_BAN_SELF = 1702,
        CLUBS_ERR_INVALID_TAG_TEXT_EMPTY = 1801,
        CLUBS_ERR_INVALID_TAG_TEXT_SIZE = 1802,
        CLUBS_ERR_TAG_TEXT_NOT_FOUND = 1803,
        CLUBS_ERR_WRONG_PASSWORD = 1901,
        CLUBS_ERR_INVALID_PASSWORD_PROFANITY = 1902,
        CLUBS_ERR_FEATURE_NOT_ENABLED = 1903,
        CLUBS_ERR_WIPE_SET_UNKNOWN = 1904,
        CLUBS_ERR_PASSWORD_REQUIRED = 1905,
    }

    public enum ClubsCommand : ushort
    {
        createClub = 1100,
        getClubs = 1200,
        findClubs = 1300,
        findClubs2 = 1310,
        removeMember = 1400,
        sendInvitation = 1500,
        getInvitations = 1600,
        revokeInvitation = 1700,
        acceptInvitation = 1800,
        declineInvitation = 1900,
        getMembers = 2000,
        promoteToGM = 2100,
        demoteToMember = 2150,
        updateClubSettings = 2200,
        postNews = 2300,
        getNews = 2400,
        setNewsItemHidden = 2450,
        setMetadata = 2500,
        setMetadata2 = 2510,
        getClubsComponentSettings = 2600,
        transferOwnership = 2650,
        getClubMembershipForUsers = 2700,
        sendPetition = 2800,
        getPetitions = 2900,
        acceptPetition = 3000,
        declinePetition = 3100,
        revokePetition = 3200,
        joinClub = 3300,
        joinOrPetitionClub = 3310,
        getClubRecordbook = 3400,
        resetClubRecords = 3410,
        updateMemberOnlineStatus = 3500,
        getClubAwards = 3600,
        updateMemberMetadata = 3700,
        findClubsAsync = 3800,
        findClubsAsync2 = 3801,
        findClubs2Async = 3810,
        findClubs2Async2 = 3811,
        listRivals = 3900,
        getClubTickerMessages = 4000,
        setClubTickerMessagesSubscription = 4100,
        changeClubStrings = 4200,
        countMessages = 4300,
        getMembersAsync = 4400,
        getMembersAsync2 = 4401,
        getClubBans = 4500,
        getUserBans = 4600,
        banMember = 4700,
        unbanMember = 4800,
        GetClubsComponentInfo = 4900,
        disbandClub = 5000,
        getNewsForClubs = 5100,
        getPetitionsForClubs = 5200,
        getClubTickerMessagesForClubs = 5300,
        countMessagesForClubs = 5400,
        getMemberOnlineStatus = 5500,
        getMemberStatusInClub = 5600,
        logEvent = 5700,
        wipeStats = 5800,
        awardClubInternal = 5900,
        lockClubsInternal = 6000,
        reportResultsInternal = 6100,
        getClubRecordsInternal = 6200,
        updateClubRecordInternal = 6300,
        updateClubRivalInternal = 6400,
        checkMembershipInternal = 6500,
        checkClubIdInternal = 6600,
        checkUserInClubInternal = 6700,
        getClubSettingsInternal = 6800,
        listRivalsInternal = 6900,
        getMembersInternal = 7000,
        updateClubLastActiveTimeInternal = 7100,
        getClubsComponentSettingsInternal = 7200,
    }

    public enum ClubsNotification : ushort
    {
        FindClubsAsyncNotification = 0,
        NewClubTickerMessageNotification = 1,
        GetMembersAsyncNotification = 2,
        InvalidateCacheNotification = 3,
        UpdateCacheNotification = 4,
        PublishClubTickerMessageNotification = 5,
        UpdateClubsComponentInfo = 6,
        UpdateOnlineStatusNotification = 7,
    }

    public enum ClubsEvent : ushort
    {
        PlayerPromotedToGMEvent = 1000,
        GMDemotedtoMemberEvent = 1001,
        PetitionSentEvent = 1002,
        UserJoinedClubEvent = 1003,
        MemberRemovedFromClubEvent = 1004,
        ClubOwnershipTransferredEvent = 1005,
        NewClubCreatedEvent = 1006,
        ClubSettingsUpdatedEvent = 1007,
        ClubInvitationSentEvent = 1008,
    }

    public class Server : BlazeComponent
    {
        public override ushort Id => ClubsComponentBase.Id;
        public override string Name => ClubsComponentBase.Name;

        public virtual bool IsCommandSupported(ClubsCommand command) => false;

        public class ClubsException : BlazeRpcException
        {
            public ClubsException(Error error) : base((ushort)error, null) { }
            public ClubsException(ServerError error) : base(error.WithErrorPrefix(), null) { }
            public ClubsException(Error error, Tdf? errorResponse) : base((ushort)error, errorResponse) { }
            public ClubsException(ServerError error, Tdf? errorResponse) : base(error.WithErrorPrefix(), errorResponse) { }
            public ClubsException(Error error, Tdf? errorResponse, string? message) : base((ushort)error, errorResponse, message) { }
            public ClubsException(ServerError error, Tdf? errorResponse, string? message) : base(error.WithErrorPrefix(), errorResponse, message) { }
            public ClubsException(Error error, Tdf? errorResponse, string? message, Exception? innerException) : base((ushort)error, errorResponse, message, innerException) { }
            public ClubsException(ServerError error, Tdf? errorResponse, string? message, Exception? innerException) : base(error.WithErrorPrefix(), errorResponse, message, innerException) { }
        }

        public Server()
        {
            // Slave commands
            RegisterCommand(new RpcCommandFunc<CreateClubRequest, CreateClubResponse, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.createClub,
                Name = "createClub",
                IsSupported = IsCommandSupported(ClubsCommand.createClub),
                Func = async (req, ctx) => await CreateClubAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetClubsRequest, GetClubsResponse, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.getClubs,
                Name = "getClubs",
                IsSupported = IsCommandSupported(ClubsCommand.getClubs),
                Func = async (req, ctx) => await GetClubsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<FindClubsRequest, FindClubsResponse, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.findClubs,
                Name = "findClubs",
                IsSupported = IsCommandSupported(ClubsCommand.findClubs),
                Func = async (req, ctx) => await FindClubsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<FindClubs2Request, FindClubsResponse, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.findClubs2,
                Name = "findClubs2",
                IsSupported = IsCommandSupported(ClubsCommand.findClubs2),
                Func = async (req, ctx) => await FindClubs2Async(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<RemoveMemberRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.removeMember,
                Name = "removeMember",
                IsSupported = IsCommandSupported(ClubsCommand.removeMember),
                Func = async (req, ctx) => await RemoveMemberAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<SendInvitationRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.sendInvitation,
                Name = "sendInvitation",
                IsSupported = IsCommandSupported(ClubsCommand.sendInvitation),
                Func = async (req, ctx) => await SendInvitationAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetInvitationsRequest, GetInvitationsResponse, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.getInvitations,
                Name = "getInvitations",
                IsSupported = IsCommandSupported(ClubsCommand.getInvitations),
                Func = async (req, ctx) => await GetInvitationsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<ProcessInvitationRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.revokeInvitation,
                Name = "revokeInvitation",
                IsSupported = IsCommandSupported(ClubsCommand.revokeInvitation),
                Func = async (req, ctx) => await RevokeInvitationAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<ProcessInvitationRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.acceptInvitation,
                Name = "acceptInvitation",
                IsSupported = IsCommandSupported(ClubsCommand.acceptInvitation),
                Func = async (req, ctx) => await AcceptInvitationAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<ProcessInvitationRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.declineInvitation,
                Name = "declineInvitation",
                IsSupported = IsCommandSupported(ClubsCommand.declineInvitation),
                Func = async (req, ctx) => await DeclineInvitationAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetMembersRequest, GetMembersResponse, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.getMembers,
                Name = "getMembers",
                IsSupported = IsCommandSupported(ClubsCommand.getMembers),
                Func = async (req, ctx) => await GetMembersAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<PromoteToGMRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.promoteToGM,
                Name = "promoteToGM",
                IsSupported = IsCommandSupported(ClubsCommand.promoteToGM),
                Func = async (req, ctx) => await PromoteToGMAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<DemoteToMemberRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.demoteToMember,
                Name = "demoteToMember",
                IsSupported = IsCommandSupported(ClubsCommand.demoteToMember),
                Func = async (req, ctx) => await DemoteToMemberAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UpdateClubSettingsRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.updateClubSettings,
                Name = "updateClubSettings",
                IsSupported = IsCommandSupported(ClubsCommand.updateClubSettings),
                Func = async (req, ctx) => await UpdateClubSettingsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<PostNewsRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.postNews,
                Name = "postNews",
                IsSupported = IsCommandSupported(ClubsCommand.postNews),
                Func = async (req, ctx) => await PostNewsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetNewsRequest, GetNewsResponse, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.getNews,
                Name = "getNews",
                IsSupported = IsCommandSupported(ClubsCommand.getNews),
                Func = async (req, ctx) => await GetNewsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<SetNewsItemHiddenRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.setNewsItemHidden,
                Name = "setNewsItemHidden",
                IsSupported = IsCommandSupported(ClubsCommand.setNewsItemHidden),
                Func = async (req, ctx) => await SetNewsItemHiddenAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<SetMetadataRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.setMetadata,
                Name = "setMetadata",
                IsSupported = IsCommandSupported(ClubsCommand.setMetadata),
                Func = async (req, ctx) => await SetMetadataAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<SetMetadataRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.setMetadata2,
                Name = "setMetadata2",
                IsSupported = IsCommandSupported(ClubsCommand.setMetadata2),
                Func = async (req, ctx) => await SetMetadata2Async(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, ClubsComponentSettings, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.getClubsComponentSettings,
                Name = "getClubsComponentSettings",
                IsSupported = IsCommandSupported(ClubsCommand.getClubsComponentSettings),
                Func = async (req, ctx) => await GetClubsComponentSettingsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<TransferOwnershipRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.transferOwnership,
                Name = "transferOwnership",
                IsSupported = IsCommandSupported(ClubsCommand.transferOwnership),
                Func = async (req, ctx) => await TransferOwnershipAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetClubMembershipForUsersRequest, GetClubMembershipForUsersResponse, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.getClubMembershipForUsers,
                Name = "getClubMembershipForUsers",
                IsSupported = IsCommandSupported(ClubsCommand.getClubMembershipForUsers),
                Func = async (req, ctx) => await GetClubMembershipForUsersAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<SendPetitionRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.sendPetition,
                Name = "sendPetition",
                IsSupported = IsCommandSupported(ClubsCommand.sendPetition),
                Func = async (req, ctx) => await SendPetitionAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetPetitionsRequest, GetPetitionsResponse, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.getPetitions,
                Name = "getPetitions",
                IsSupported = IsCommandSupported(ClubsCommand.getPetitions),
                Func = async (req, ctx) => await GetPetitionsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<ProcessPetitionRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.acceptPetition,
                Name = "acceptPetition",
                IsSupported = IsCommandSupported(ClubsCommand.acceptPetition),
                Func = async (req, ctx) => await AcceptPetitionAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<ProcessPetitionRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.declinePetition,
                Name = "declinePetition",
                IsSupported = IsCommandSupported(ClubsCommand.declinePetition),
                Func = async (req, ctx) => await DeclinePetitionAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<ProcessPetitionRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.revokePetition,
                Name = "revokePetition",
                IsSupported = IsCommandSupported(ClubsCommand.revokePetition),
                Func = async (req, ctx) => await RevokePetitionAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<JoinClubRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.joinClub,
                Name = "joinClub",
                IsSupported = IsCommandSupported(ClubsCommand.joinClub),
                Func = async (req, ctx) => await JoinClubAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<JoinOrPetitionClubRequest, JoinOrPetitionClubResponse, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.joinOrPetitionClub,
                Name = "joinOrPetitionClub",
                IsSupported = IsCommandSupported(ClubsCommand.joinOrPetitionClub),
                Func = async (req, ctx) => await JoinOrPetitionClubAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetClubRecordbookRequest, GetClubRecordbookResponse, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.getClubRecordbook,
                Name = "getClubRecordbook",
                IsSupported = IsCommandSupported(ClubsCommand.getClubRecordbook),
                Func = async (req, ctx) => await GetClubRecordbookAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<ResetClubRecordsRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.resetClubRecords,
                Name = "resetClubRecords",
                IsSupported = IsCommandSupported(ClubsCommand.resetClubRecords),
                Func = async (req, ctx) => await ResetClubRecordsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UpdateMemberOnlineStatusRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.updateMemberOnlineStatus,
                Name = "updateMemberOnlineStatus",
                IsSupported = IsCommandSupported(ClubsCommand.updateMemberOnlineStatus),
                Func = async (req, ctx) => await UpdateMemberOnlineStatusAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetClubAwardsRequest, GetClubAwardsResponse, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.getClubAwards,
                Name = "getClubAwards",
                IsSupported = IsCommandSupported(ClubsCommand.getClubAwards),
                Func = async (req, ctx) => await GetClubAwardsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UpdateMemberMetadataRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.updateMemberMetadata,
                Name = "updateMemberMetadata",
                IsSupported = IsCommandSupported(ClubsCommand.updateMemberMetadata),
                Func = async (req, ctx) => await UpdateMemberMetadataAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<FindClubsRequest, FindClubsAsyncResponse, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.findClubsAsync,
                Name = "findClubsAsync",
                IsSupported = IsCommandSupported(ClubsCommand.findClubsAsync),
                Func = async (req, ctx) => await FindClubsAsyncAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<FindClubsRequest, FindClubsAsyncResponse, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.findClubsAsync2,
                Name = "findClubsAsync2",
                IsSupported = IsCommandSupported(ClubsCommand.findClubsAsync2),
                Func = async (req, ctx) => await FindClubsAsync2Async(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<FindClubs2Request, FindClubsAsyncResponse, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.findClubs2Async,
                Name = "findClubs2Async",
                IsSupported = IsCommandSupported(ClubsCommand.findClubs2Async),
                Func = async (req, ctx) => await FindClubs2AsyncAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<FindClubs2Request, FindClubsAsyncResponse, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.findClubs2Async2,
                Name = "findClubs2Async2",
                IsSupported = IsCommandSupported(ClubsCommand.findClubs2Async2),
                Func = async (req, ctx) => await FindClubs2Async2Async(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<ListRivalsRequest, ListRivalsResponse, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.listRivals,
                Name = "listRivals",
                IsSupported = IsCommandSupported(ClubsCommand.listRivals),
                Func = async (req, ctx) => await ListRivalsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetClubTickerMessagesRequest, GetClubTickerMessagesResponse, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.getClubTickerMessages,
                Name = "getClubTickerMessages",
                IsSupported = IsCommandSupported(ClubsCommand.getClubTickerMessages),
                Func = async (req, ctx) => await GetClubTickerMessagesAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<SetClubTickerMessagesSubscriptionRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.setClubTickerMessagesSubscription,
                Name = "setClubTickerMessagesSubscription",
                IsSupported = IsCommandSupported(ClubsCommand.setClubTickerMessagesSubscription),
                Func = async (req, ctx) => await SetClubTickerMessagesSubscriptionAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<ChangeClubStringsRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.changeClubStrings,
                Name = "changeClubStrings",
                IsSupported = IsCommandSupported(ClubsCommand.changeClubStrings),
                Func = async (req, ctx) => await ChangeClubStringsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<CountMessagesRequest, CountMessagesResponse, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.countMessages,
                Name = "countMessages",
                IsSupported = IsCommandSupported(ClubsCommand.countMessages),
                Func = async (req, ctx) => await CountMessagesAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetMembersRequest, GetMembersAsyncResponse, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.getMembersAsync,
                Name = "getMembersAsync",
                IsSupported = IsCommandSupported(ClubsCommand.getMembersAsync),
                Func = async (req, ctx) => await GetMembersAsyncAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetMembersRequest, GetMembersAsyncResponse, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.getMembersAsync2,
                Name = "getMembersAsync2",
                IsSupported = IsCommandSupported(ClubsCommand.getMembersAsync2),
                Func = async (req, ctx) => await GetMembersAsync2Async(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetClubBansRequest, GetClubBansResponse, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.getClubBans,
                Name = "getClubBans",
                IsSupported = IsCommandSupported(ClubsCommand.getClubBans),
                Func = async (req, ctx) => await GetClubBansAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetUserBansRequest, GetUserBansResponse, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.getUserBans,
                Name = "getUserBans",
                IsSupported = IsCommandSupported(ClubsCommand.getUserBans),
                Func = async (req, ctx) => await GetUserBansAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<BanUnbanMemberRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.banMember,
                Name = "banMember",
                IsSupported = IsCommandSupported(ClubsCommand.banMember),
                Func = async (req, ctx) => await BanMemberAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<BanUnbanMemberRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.unbanMember,
                Name = "unbanMember",
                IsSupported = IsCommandSupported(ClubsCommand.unbanMember),
                Func = async (req, ctx) => await UnbanMemberAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, GetClubsComponentInfoResponse, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.GetClubsComponentInfo,
                Name = "GetClubsComponentInfo",
                IsSupported = IsCommandSupported(ClubsCommand.GetClubsComponentInfo),
                Func = async (req, ctx) => await GetClubsComponentInfoAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<DisbandClubRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.disbandClub,
                Name = "disbandClub",
                IsSupported = IsCommandSupported(ClubsCommand.disbandClub),
                Func = async (req, ctx) => await DisbandClubAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetNewsForClubsRequest, GetNewsForClubsResponse, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.getNewsForClubs,
                Name = "getNewsForClubs",
                IsSupported = IsCommandSupported(ClubsCommand.getNewsForClubs),
                Func = async (req, ctx) => await GetNewsForClubsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetPetitionsForClubsRequest, GetPetitionsForClubsResponse, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.getPetitionsForClubs,
                Name = "getPetitionsForClubs",
                IsSupported = IsCommandSupported(ClubsCommand.getPetitionsForClubs),
                Func = async (req, ctx) => await GetPetitionsForClubsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetClubTickerMessagesForClubsRequest, GetClubTickerMessagesForClubsResponse, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.getClubTickerMessagesForClubs,
                Name = "getClubTickerMessagesForClubs",
                IsSupported = IsCommandSupported(ClubsCommand.getClubTickerMessagesForClubs),
                Func = async (req, ctx) => await GetClubTickerMessagesForClubsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<CountMessagesForClubsRequest, CountMessagesForClubsResponse, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.countMessagesForClubs,
                Name = "countMessagesForClubs",
                IsSupported = IsCommandSupported(ClubsCommand.countMessagesForClubs),
                Func = async (req, ctx) => await CountMessagesForClubsAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetMemberOnlineStatusRequest, GetMemberOnlineStatusResponse, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.getMemberOnlineStatus,
                Name = "getMemberOnlineStatus",
                IsSupported = IsCommandSupported(ClubsCommand.getMemberOnlineStatus),
                Func = async (req, ctx) => await GetMemberOnlineStatusAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<GetMemberStatusInClubRequest, GetMemberStatusInClubResponse, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.getMemberStatusInClub,
                Name = "getMemberStatusInClub",
                IsSupported = IsCommandSupported(ClubsCommand.getMemberStatusInClub),
                Func = async (req, ctx) => await GetMemberStatusInClubAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<LogEventRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.logEvent,
                Name = "logEvent",
                IsSupported = IsCommandSupported(ClubsCommand.logEvent),
                Func = async (req, ctx) => await LogEventAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<WipeStatsRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.wipeStats,
                Name = "wipeStats",
                IsSupported = IsCommandSupported(ClubsCommand.wipeStats),
                Func = async (req, ctx) => await WipeStatsAsync(req, ctx).ConfigureAwait(false)
            });

            // Internal/server-only commands
            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.awardClubInternal,
                Name = "awardClubInternal",
                IsSupported = IsCommandSupported(ClubsCommand.awardClubInternal),
                Func = async (req, ctx) => await AwardClubInternalAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.lockClubsInternal,
                Name = "lockClubsInternal",
                IsSupported = IsCommandSupported(ClubsCommand.lockClubsInternal),
                Func = async (req, ctx) => await LockClubsInternalAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.reportResultsInternal,
                Name = "reportResultsInternal",
                IsSupported = IsCommandSupported(ClubsCommand.reportResultsInternal),
                Func = async (req, ctx) => await ReportResultsInternalAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.getClubRecordsInternal,
                Name = "getClubRecordsInternal",
                IsSupported = IsCommandSupported(ClubsCommand.getClubRecordsInternal),
                Func = async (req, ctx) => await GetClubRecordsInternalAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.updateClubRecordInternal,
                Name = "updateClubRecordInternal",
                IsSupported = IsCommandSupported(ClubsCommand.updateClubRecordInternal),
                Func = async (req, ctx) => await UpdateClubRecordInternalAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.updateClubRivalInternal,
                Name = "updateClubRivalInternal",
                IsSupported = IsCommandSupported(ClubsCommand.updateClubRivalInternal),
                Func = async (req, ctx) => await UpdateClubRivalInternalAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.checkMembershipInternal,
                Name = "checkMembershipInternal",
                IsSupported = IsCommandSupported(ClubsCommand.checkMembershipInternal),
                Func = async (req, ctx) => await CheckMembershipInternalAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.checkClubIdInternal,
                Name = "checkClubIdInternal",
                IsSupported = IsCommandSupported(ClubsCommand.checkClubIdInternal),
                Func = async (req, ctx) => await CheckClubIdInternalAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.checkUserInClubInternal,
                Name = "checkUserInClubInternal",
                IsSupported = IsCommandSupported(ClubsCommand.checkUserInClubInternal),
                Func = async (req, ctx) => await CheckUserInClubInternalAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.getClubSettingsInternal,
                Name = "getClubSettingsInternal",
                IsSupported = IsCommandSupported(ClubsCommand.getClubSettingsInternal),
                Func = async (req, ctx) => await GetClubSettingsInternalAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, ListRivalsResponse, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.listRivalsInternal,
                Name = "listRivalsInternal",
                IsSupported = IsCommandSupported(ClubsCommand.listRivalsInternal),
                Func = async (req, ctx) => await ListRivalsInternalAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, GetMembersResponse, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.getMembersInternal,
                Name = "getMembersInternal",
                IsSupported = IsCommandSupported(ClubsCommand.getMembersInternal),
                Func = async (req, ctx) => await GetMembersInternalAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<UpdateClubLastActiveTimeRequest, EmptyMessage, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.updateClubLastActiveTimeInternal,
                Name = "updateClubLastActiveTimeInternal",
                IsSupported = IsCommandSupported(ClubsCommand.updateClubLastActiveTimeInternal),
                Func = async (req, ctx) => await UpdateClubLastActiveTimeInternalAsync(req, ctx).ConfigureAwait(false)
            });

            RegisterCommand(new RpcCommandFunc<EmptyMessage, ClubsComponentSettings, EmptyMessage>()
            {
                Id = (ushort)ClubsCommand.getClubsComponentSettingsInternal,
                Name = "getClubsComponentSettingsInternal",
                IsSupported = IsCommandSupported(ClubsCommand.getClubsComponentSettingsInternal),
                Func = async (req, ctx) => await GetClubsComponentSettingsInternalAsync(req, ctx).ConfigureAwait(false)
            });

        }

        public override string GetErrorName(ushort errorCode)
        {
            return ((Error)errorCode).ToString();
        }

        // ===== Slave command handlers =====

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::createClub</b> command.<br/>
        /// Request type: <see cref="CreateClubRequest"/><br/>
        /// Response type: <see cref="CreateClubResponse"/><br/>
        /// </summary>
        public virtual Task<CreateClubResponse> CreateClubAsync(CreateClubRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::getClubs</b> command.<br/>
        /// Request type: <see cref="GetClubsRequest"/><br/>
        /// Response type: <see cref="GetClubsResponse"/><br/>
        /// </summary>
        public virtual Task<GetClubsResponse> GetClubsAsync(GetClubsRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::findClubs</b> command.<br/>
        /// Request type: <see cref="FindClubsRequest"/><br/>
        /// Response type: <see cref="FindClubsResponse"/><br/>
        /// </summary>
        public virtual Task<FindClubsResponse> FindClubsAsync(FindClubsRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::findClubs2</b> command.<br/>
        /// Request type: <see cref="FindClubs2Request"/><br/>
        /// Response type: <see cref="FindClubsResponse"/><br/>
        /// </summary>
        public virtual Task<FindClubsResponse> FindClubs2Async(FindClubs2Request request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::removeMember</b> command.<br/>
        /// Request type: <see cref="RemoveMemberRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> RemoveMemberAsync(RemoveMemberRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::sendInvitation</b> command.<br/>
        /// Request type: <see cref="SendInvitationRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> SendInvitationAsync(SendInvitationRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::getInvitations</b> command.<br/>
        /// Request type: <see cref="GetInvitationsRequest"/><br/>
        /// Response type: <see cref="GetInvitationsResponse"/><br/>
        /// </summary>
        public virtual Task<GetInvitationsResponse> GetInvitationsAsync(GetInvitationsRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::revokeInvitation</b> command.<br/>
        /// Request type: <see cref="ProcessInvitationRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> RevokeInvitationAsync(ProcessInvitationRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::acceptInvitation</b> command.<br/>
        /// Request type: <see cref="ProcessInvitationRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> AcceptInvitationAsync(ProcessInvitationRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::declineInvitation</b> command.<br/>
        /// Request type: <see cref="ProcessInvitationRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> DeclineInvitationAsync(ProcessInvitationRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::getMembers</b> command.<br/>
        /// Request type: <see cref="GetMembersRequest"/><br/>
        /// Response type: <see cref="GetMembersResponse"/><br/>
        /// </summary>
        public virtual Task<GetMembersResponse> GetMembersAsync(GetMembersRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::promoteToGM</b> command.<br/>
        /// Request type: <see cref="PromoteToGMRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> PromoteToGMAsync(PromoteToGMRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::demoteToMember</b> command.<br/>
        /// Request type: <see cref="DemoteToMemberRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> DemoteToMemberAsync(DemoteToMemberRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::updateClubSettings</b> command.<br/>
        /// Request type: <see cref="UpdateClubSettingsRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> UpdateClubSettingsAsync(UpdateClubSettingsRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::postNews</b> command.<br/>
        /// Request type: <see cref="PostNewsRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> PostNewsAsync(PostNewsRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::getNews</b> command.<br/>
        /// Request type: <see cref="GetNewsRequest"/><br/>
        /// Response type: <see cref="GetNewsResponse"/><br/>
        /// </summary>
        public virtual Task<GetNewsResponse> GetNewsAsync(GetNewsRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::setNewsItemHidden</b> command.<br/>
        /// Request type: <see cref="SetNewsItemHiddenRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> SetNewsItemHiddenAsync(SetNewsItemHiddenRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::setMetadata</b> command.<br/>
        /// Request type: <see cref="SetMetadataRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> SetMetadataAsync(SetMetadataRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::setMetadata2</b> command.<br/>
        /// Request type: <see cref="SetMetadataRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> SetMetadata2Async(SetMetadataRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::getClubsComponentSettings</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="ClubsComponentSettings"/><br/>
        /// </summary>
        public virtual Task<ClubsComponentSettings> GetClubsComponentSettingsAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::transferOwnership</b> command.<br/>
        /// Request type: <see cref="TransferOwnershipRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> TransferOwnershipAsync(TransferOwnershipRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::getClubMembershipForUsers</b> command.<br/>
        /// Request type: <see cref="GetClubMembershipForUsersRequest"/><br/>
        /// Response type: <see cref="GetClubMembershipForUsersResponse"/><br/>
        /// </summary>
        public virtual Task<GetClubMembershipForUsersResponse> GetClubMembershipForUsersAsync(GetClubMembershipForUsersRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::sendPetition</b> command.<br/>
        /// Request type: <see cref="SendPetitionRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> SendPetitionAsync(SendPetitionRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::getPetitions</b> command.<br/>
        /// Request type: <see cref="GetPetitionsRequest"/><br/>
        /// Response type: <see cref="GetPetitionsResponse"/><br/>
        /// </summary>
        public virtual Task<GetPetitionsResponse> GetPetitionsAsync(GetPetitionsRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::acceptPetition</b> command.<br/>
        /// Request type: <see cref="ProcessPetitionRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> AcceptPetitionAsync(ProcessPetitionRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::declinePetition</b> command.<br/>
        /// Request type: <see cref="ProcessPetitionRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> DeclinePetitionAsync(ProcessPetitionRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::revokePetition</b> command.<br/>
        /// Request type: <see cref="ProcessPetitionRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> RevokePetitionAsync(ProcessPetitionRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::joinClub</b> command.<br/>
        /// Request type: <see cref="JoinClubRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> JoinClubAsync(JoinClubRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::joinOrPetitionClub</b> command.<br/>
        /// Request type: <see cref="JoinOrPetitionClubRequest"/><br/>
        /// Response type: <see cref="JoinOrPetitionClubResponse"/><br/>
        /// </summary>
        public virtual Task<JoinOrPetitionClubResponse> JoinOrPetitionClubAsync(JoinOrPetitionClubRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::getClubRecordbook</b> command.<br/>
        /// Request type: <see cref="GetClubRecordbookRequest"/><br/>
        /// Response type: <see cref="GetClubRecordbookResponse"/><br/>
        /// </summary>
        public virtual Task<GetClubRecordbookResponse> GetClubRecordbookAsync(GetClubRecordbookRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::resetClubRecords</b> command.<br/>
        /// Request type: <see cref="ResetClubRecordsRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> ResetClubRecordsAsync(ResetClubRecordsRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::updateMemberOnlineStatus</b> command.<br/>
        /// Request type: <see cref="UpdateMemberOnlineStatusRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> UpdateMemberOnlineStatusAsync(UpdateMemberOnlineStatusRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::getClubAwards</b> command.<br/>
        /// Request type: <see cref="GetClubAwardsRequest"/><br/>
        /// Response type: <see cref="GetClubAwardsResponse"/><br/>
        /// </summary>
        public virtual Task<GetClubAwardsResponse> GetClubAwardsAsync(GetClubAwardsRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::updateMemberMetadata</b> command.<br/>
        /// Request type: <see cref="UpdateMemberMetadataRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> UpdateMemberMetadataAsync(UpdateMemberMetadataRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::findClubsAsync</b> command.<br/>
        /// Request type: <see cref="FindClubsRequest"/><br/>
        /// Response type: <see cref="FindClubsAsyncResponse"/><br/>
        /// </summary>
        public virtual Task<FindClubsAsyncResponse> FindClubsAsyncAsync(FindClubsRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::findClubsAsync2</b> command.<br/>
        /// Request type: <see cref="FindClubsRequest"/><br/>
        /// Response type: <see cref="FindClubsAsyncResponse"/><br/>
        /// </summary>
        public virtual Task<FindClubsAsyncResponse> FindClubsAsync2Async(FindClubsRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::findClubs2Async</b> command.<br/>
        /// Request type: <see cref="FindClubs2Request"/><br/>
        /// Response type: <see cref="FindClubsAsyncResponse"/><br/>
        /// </summary>
        public virtual Task<FindClubsAsyncResponse> FindClubs2AsyncAsync(FindClubs2Request request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::findClubs2Async2</b> command.<br/>
        /// Request type: <see cref="FindClubs2Request"/><br/>
        /// Response type: <see cref="FindClubsAsyncResponse"/><br/>
        /// </summary>
        public virtual Task<FindClubsAsyncResponse> FindClubs2Async2Async(FindClubs2Request request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::listRivals</b> command.<br/>
        /// Request type: <see cref="ListRivalsRequest"/><br/>
        /// Response type: <see cref="ListRivalsResponse"/><br/>
        /// </summary>
        public virtual Task<ListRivalsResponse> ListRivalsAsync(ListRivalsRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::getClubTickerMessages</b> command.<br/>
        /// Request type: <see cref="GetClubTickerMessagesRequest"/><br/>
        /// Response type: <see cref="GetClubTickerMessagesResponse"/><br/>
        /// </summary>
        public virtual Task<GetClubTickerMessagesResponse> GetClubTickerMessagesAsync(GetClubTickerMessagesRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::setClubTickerMessagesSubscription</b> command.<br/>
        /// Request type: <see cref="SetClubTickerMessagesSubscriptionRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> SetClubTickerMessagesSubscriptionAsync(SetClubTickerMessagesSubscriptionRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::changeClubStrings</b> command.<br/>
        /// Request type: <see cref="ChangeClubStringsRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> ChangeClubStringsAsync(ChangeClubStringsRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::countMessages</b> command.<br/>
        /// Request type: <see cref="CountMessagesRequest"/><br/>
        /// Response type: <see cref="CountMessagesResponse"/><br/>
        /// </summary>
        public virtual Task<CountMessagesResponse> CountMessagesAsync(CountMessagesRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::getMembersAsync</b> command.<br/>
        /// Request type: <see cref="GetMembersRequest"/><br/>
        /// Response type: <see cref="GetMembersAsyncResponse"/><br/>
        /// </summary>
        public virtual Task<GetMembersAsyncResponse> GetMembersAsyncAsync(GetMembersRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::getMembersAsync2</b> command.<br/>
        /// Request type: <see cref="GetMembersRequest"/><br/>
        /// Response type: <see cref="GetMembersAsyncResponse"/><br/>
        /// </summary>
        public virtual Task<GetMembersAsyncResponse> GetMembersAsync2Async(GetMembersRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::getClubBans</b> command.<br/>
        /// Request type: <see cref="GetClubBansRequest"/><br/>
        /// Response type: <see cref="GetClubBansResponse"/><br/>
        /// </summary>
        public virtual Task<GetClubBansResponse> GetClubBansAsync(GetClubBansRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::getUserBans</b> command.<br/>
        /// Request type: <see cref="GetUserBansRequest"/><br/>
        /// Response type: <see cref="GetUserBansResponse"/><br/>
        /// </summary>
        public virtual Task<GetUserBansResponse> GetUserBansAsync(GetUserBansRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::banMember</b> command.<br/>
        /// Request type: <see cref="BanUnbanMemberRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> BanMemberAsync(BanUnbanMemberRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::unbanMember</b> command.<br/>
        /// Request type: <see cref="BanUnbanMemberRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> UnbanMemberAsync(BanUnbanMemberRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::GetClubsComponentInfo</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="GetClubsComponentInfoResponse"/><br/>
        /// </summary>
        public virtual Task<GetClubsComponentInfoResponse> GetClubsComponentInfoAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::disbandClub</b> command.<br/>
        /// Request type: <see cref="DisbandClubRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> DisbandClubAsync(DisbandClubRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::getNewsForClubs</b> command.<br/>
        /// Request type: <see cref="GetNewsForClubsRequest"/><br/>
        /// Response type: <see cref="GetNewsForClubsResponse"/><br/>
        /// </summary>
        public virtual Task<GetNewsForClubsResponse> GetNewsForClubsAsync(GetNewsForClubsRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::getPetitionsForClubs</b> command.<br/>
        /// Request type: <see cref="GetPetitionsForClubsRequest"/><br/>
        /// Response type: <see cref="GetPetitionsForClubsResponse"/><br/>
        /// </summary>
        public virtual Task<GetPetitionsForClubsResponse> GetPetitionsForClubsAsync(GetPetitionsForClubsRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::getClubTickerMessagesForClubs</b> command.<br/>
        /// Request type: <see cref="GetClubTickerMessagesForClubsRequest"/><br/>
        /// Response type: <see cref="GetClubTickerMessagesForClubsResponse"/><br/>
        /// </summary>
        public virtual Task<GetClubTickerMessagesForClubsResponse> GetClubTickerMessagesForClubsAsync(GetClubTickerMessagesForClubsRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::countMessagesForClubs</b> command.<br/>
        /// Request type: <see cref="CountMessagesForClubsRequest"/><br/>
        /// Response type: <see cref="CountMessagesForClubsResponse"/><br/>
        /// </summary>
        public virtual Task<CountMessagesForClubsResponse> CountMessagesForClubsAsync(CountMessagesForClubsRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::getMemberOnlineStatus</b> command.<br/>
        /// Request type: <see cref="GetMemberOnlineStatusRequest"/><br/>
        /// Response type: <see cref="GetMemberOnlineStatusResponse"/><br/>
        /// </summary>
        public virtual Task<GetMemberOnlineStatusResponse> GetMemberOnlineStatusAsync(GetMemberOnlineStatusRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::getMemberStatusInClub</b> command.<br/>
        /// Request type: <see cref="GetMemberStatusInClubRequest"/><br/>
        /// Response type: <see cref="GetMemberStatusInClubResponse"/><br/>
        /// </summary>
        public virtual Task<GetMemberStatusInClubResponse> GetMemberStatusInClubAsync(GetMemberStatusInClubRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::logEvent</b> command.<br/>
        /// Request type: <see cref="LogEventRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> LogEventAsync(LogEventRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::wipeStats</b> command.<br/>
        /// Request type: <see cref="WipeStatsRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> WipeStatsAsync(WipeStatsRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        // ===== Internal/server-only command handlers =====

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::awardClubInternal</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/> (server-only type)<br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> AwardClubInternalAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::lockClubsInternal</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/> (server-only type)<br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> LockClubsInternalAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::reportResultsInternal</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/> (server-only type)<br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> ReportResultsInternalAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::getClubRecordsInternal</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/> (server-only type)<br/>
        /// Response type: <see cref="EmptyMessage"/> (server-only type)<br/>
        /// </summary>
        public virtual Task<EmptyMessage> GetClubRecordsInternalAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::updateClubRecordInternal</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/> (server-only type)<br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> UpdateClubRecordInternalAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::updateClubRivalInternal</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/> (server-only type)<br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> UpdateClubRivalInternalAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::checkMembershipInternal</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/> (server-only type)<br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> CheckMembershipInternalAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::checkClubIdInternal</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/> (server-only type)<br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> CheckClubIdInternalAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::checkUserInClubInternal</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/> (server-only type)<br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> CheckUserInClubInternalAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::getClubSettingsInternal</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/> (server-only type)<br/>
        /// Response type: <see cref="EmptyMessage"/> (server-only type)<br/>
        /// </summary>
        public virtual Task<EmptyMessage> GetClubSettingsInternalAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::listRivalsInternal</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/> (server-only type)<br/>
        /// Response type: <see cref="ListRivalsResponse"/><br/>
        /// </summary>
        public virtual Task<ListRivalsResponse> ListRivalsInternalAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::getMembersInternal</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/> (server-only type)<br/>
        /// Response type: <see cref="GetMembersResponse"/><br/>
        /// </summary>
        public virtual Task<GetMembersResponse> GetMembersInternalAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::updateClubLastActiveTimeInternal</b> command.<br/>
        /// Request type: <see cref="UpdateClubLastActiveTimeRequest"/><br/>
        /// Response type: <see cref="EmptyMessage"/><br/>
        /// </summary>
        public virtual Task<EmptyMessage> UpdateClubLastActiveTimeInternalAsync(UpdateClubLastActiveTimeRequest request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        /// <summary>
        /// This method is called when server receives a <b>ClubsComponent::getClubsComponentSettingsInternal</b> command.<br/>
        /// Request type: <see cref="EmptyMessage"/><br/>
        /// Response type: <see cref="ClubsComponentSettings"/><br/>
        /// </summary>
        public virtual Task<ClubsComponentSettings> GetClubsComponentSettingsInternalAsync(EmptyMessage request, BlazeRpcContext context)
        {
            throw new ClubsException(ServerError.ERR_COMMAND_NOT_FOUND);
        }

        // ===== Notification helpers =====

        /// <summary>
        /// Call this method when you want to send to client a <b>ClubsComponent::FindClubsAsyncNotification</b> notification.<br/>
        /// Notification type: <see cref="FindClubsAsyncResult"/><br/>
        /// </summary>
        public static Task NotifyFindClubsAsyncAsync(BlazeRpcConnection connection, FindClubsAsyncResult notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = ClubsComponentBase.Id;
                frame.Command = (ushort)ClubsNotification.FindClubsAsyncNotification;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>ClubsComponent::NewClubTickerMessageNotification</b> notification.<br/>
        /// Notification type: <see cref="ClubTickerMessage"/><br/>
        /// </summary>
        public static Task NotifyNewClubTickerMessageAsync(BlazeRpcConnection connection, ClubTickerMessage notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = ClubsComponentBase.Id;
                frame.Command = (ushort)ClubsNotification.NewClubTickerMessageNotification;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send to client a <b>ClubsComponent::GetMembersAsyncNotification</b> notification.<br/>
        /// Notification type: <see cref="GetMembersAsyncResult"/><br/>
        /// </summary>
        public static Task NotifyGetMembersAsyncAsync(BlazeRpcConnection connection, GetMembersAsyncResult notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = ClubsComponentBase.Id;
                frame.Command = (ushort)ClubsNotification.GetMembersAsyncNotification;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send a <b>ClubsComponent::InvalidateCacheNotification</b> notification.<br/>
        /// Notification type: <see cref="EmptyMessage"/> (server-only type)<br/>
        /// </summary>
        public static Task NotifyInvalidateCacheAsync(BlazeRpcConnection connection, EmptyMessage notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = ClubsComponentBase.Id;
                frame.Command = (ushort)ClubsNotification.InvalidateCacheNotification;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send a <b>ClubsComponent::UpdateCacheNotification</b> notification.<br/>
        /// Notification type: <see cref="EmptyMessage"/> (server-only type)<br/>
        /// </summary>
        public static Task NotifyUpdateCacheAsync(BlazeRpcConnection connection, EmptyMessage notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = ClubsComponentBase.Id;
                frame.Command = (ushort)ClubsNotification.UpdateCacheNotification;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send a <b>ClubsComponent::PublishClubTickerMessageNotification</b> notification.<br/>
        /// Notification type: <see cref="PublishClubTickerMessageRequest"/><br/>
        /// </summary>
        public static Task NotifyPublishClubTickerMessageAsync(BlazeRpcConnection connection, PublishClubTickerMessageRequest notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = ClubsComponentBase.Id;
                frame.Command = (ushort)ClubsNotification.PublishClubTickerMessageNotification;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send a <b>ClubsComponent::UpdateClubsComponentInfo</b> notification.<br/>
        /// Notification type: <see cref="ClubsComponentInfo"/><br/>
        /// </summary>
        public static Task NotifyUpdateClubsComponentInfoAsync(BlazeRpcConnection connection, ClubsComponentInfo notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = ClubsComponentBase.Id;
                frame.Command = (ushort)ClubsNotification.UpdateClubsComponentInfo;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send a <b>ClubsComponent::UpdateOnlineStatusNotification</b> notification.<br/>
        /// Notification type: <see cref="EmptyMessage"/> (server-only type)<br/>
        /// </summary>
        public static Task NotifyUpdateOnlineStatusAsync(BlazeRpcConnection connection, EmptyMessage notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = ClubsComponentBase.Id;
                frame.Command = (ushort)ClubsNotification.UpdateOnlineStatusNotification;
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
        /// Call this method when you want to send a <b>ClubsComponent::PlayerPromotedToGMEvent</b> event.<br/>
        /// Event type: <see cref="EmptyMessage"/> (server-only type)<br/>
        /// </summary>
        public static Task NotifyPlayerPromotedToGMEventAsync(BlazeRpcConnection connection, EmptyMessage notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = ClubsComponentBase.Id;
                frame.Command = (ushort)ClubsEvent.PlayerPromotedToGMEvent;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send a <b>ClubsComponent::GMDemotedtoMemberEvent</b> event.<br/>
        /// Event type: <see cref="EmptyMessage"/> (server-only type)<br/>
        /// </summary>
        public static Task NotifyGMDemotedtoMemberEventAsync(BlazeRpcConnection connection, EmptyMessage notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = ClubsComponentBase.Id;
                frame.Command = (ushort)ClubsEvent.GMDemotedtoMemberEvent;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send a <b>ClubsComponent::PetitionSentEvent</b> event.<br/>
        /// Event type: <see cref="EmptyMessage"/> (server-only type)<br/>
        /// </summary>
        public static Task NotifyPetitionSentEventAsync(BlazeRpcConnection connection, EmptyMessage notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = ClubsComponentBase.Id;
                frame.Command = (ushort)ClubsEvent.PetitionSentEvent;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send a <b>ClubsComponent::UserJoinedClubEvent</b> event.<br/>
        /// Event type: <see cref="EmptyMessage"/> (server-only type)<br/>
        /// </summary>
        public static Task NotifyUserJoinedClubEventAsync(BlazeRpcConnection connection, EmptyMessage notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = ClubsComponentBase.Id;
                frame.Command = (ushort)ClubsEvent.UserJoinedClubEvent;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send a <b>ClubsComponent::MemberRemovedFromClubEvent</b> event.<br/>
        /// Event type: <see cref="EmptyMessage"/> (server-only type)<br/>
        /// </summary>
        public static Task NotifyMemberRemovedFromClubEventAsync(BlazeRpcConnection connection, EmptyMessage notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = ClubsComponentBase.Id;
                frame.Command = (ushort)ClubsEvent.MemberRemovedFromClubEvent;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send a <b>ClubsComponent::ClubOwnershipTransferredEvent</b> event.<br/>
        /// Event type: <see cref="EmptyMessage"/> (server-only type)<br/>
        /// </summary>
        public static Task NotifyClubOwnershipTransferredEventAsync(BlazeRpcConnection connection, EmptyMessage notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = ClubsComponentBase.Id;
                frame.Command = (ushort)ClubsEvent.ClubOwnershipTransferredEvent;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send a <b>ClubsComponent::NewClubCreatedEvent</b> event.<br/>
        /// Event type: <see cref="EmptyMessage"/> (server-only type)<br/>
        /// </summary>
        public static Task NotifyNewClubCreatedEventAsync(BlazeRpcConnection connection, EmptyMessage notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = ClubsComponentBase.Id;
                frame.Command = (ushort)ClubsEvent.NewClubCreatedEvent;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send a <b>ClubsComponent::ClubSettingsUpdatedEvent</b> event.<br/>
        /// Event type: <see cref="EmptyMessage"/> (server-only type)<br/>
        /// </summary>
        public static Task NotifyClubSettingsUpdatedEventAsync(BlazeRpcConnection connection, EmptyMessage notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = ClubsComponentBase.Id;
                frame.Command = (ushort)ClubsEvent.ClubSettingsUpdatedEvent;
                frame.MessageType = ProtoFire.Frames.MessageType.Notification;
                packet.Data = notification;
            };

            if (sendNow)
                return connection.SendAsync(configurer);

            connection.EnqequeSend(configurer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Call this method when you want to send a <b>ClubsComponent::ClubInvitationSentEvent</b> event.<br/>
        /// Event type: <see cref="EmptyMessage"/> (server-only type)<br/>
        /// </summary>
        public static Task NotifyClubInvitationSentEventAsync(BlazeRpcConnection connection, EmptyMessage notification, bool sendNow = false)
        {
            Action<BlazePacket> configurer = (packet) =>
            {
                ProtoFire.Frames.IFireFrame frame = packet.Frame;
                frame.Component = ClubsComponentBase.Id;
                frame.Command = (ushort)ClubsEvent.ClubInvitationSentEvent;
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
