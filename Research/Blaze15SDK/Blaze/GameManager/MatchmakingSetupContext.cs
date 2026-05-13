using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class MatchmakingSetupContext : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("EstimatedTimeToMatch", "mEstimatedTimeToMatch", 0x974D2D00, TdfType.Int64, 0, true), // Tag: ETTM
        new TdfMemberInfo("FitScore", "mFitScore", 0x9A9D0000, TdfType.UInt32, 1, true), // Tag: FIT
        new TdfMemberInfo("FinalizationJobId", "mFinalizationJobId", 0x9AABE200, TdfType.UInt64, 2, true), // Tag: FJOB
        new TdfMemberInfo("GameEntryType", "mGameEntryType", 0x9E5BB400, TdfType.Enum, 3, true), // Tag: GENT
        new TdfMemberInfo("InitiatorId", "mInitiatorId", 0xA6EA7400, TdfType.Int64, 4, true), // Tag: INIT
        new TdfMemberInfo("MaxPossibleFitScore", "mMaxPossibleFitScore", 0xB61E2600, TdfType.UInt32, 5, true), // Tag: MAXF
        new TdfMemberInfo("ScenarioId", "mScenarioId", 0xB738E400, TdfType.UInt64, 6, true), // Tag: MSCD
        new TdfMemberInfo("SessionId", "mSessionId", 0xB73A6400, TdfType.UInt64, 7, true), // Tag: MSID
        new TdfMemberInfo("MatchmakingResult", "mMatchmakingResult", 0xCB3B3400, TdfType.Enum, 8, true), // Tag: RSLT
        new TdfMemberInfo("TotalUsersMatched", "mTotalUsersMatched", 0xD2D87400, TdfType.UInt64, 9, true), // Tag: TMAT
        new TdfMemberInfo("MatchmakingTimeoutDuration", "mMatchmakingTimeoutDuration", 0xD2FD7400, TdfType.Int64, 10, true), // Tag: TOUT
        new TdfMemberInfo("TotalUsersPotentiallyMatched", "mTotalUsersPotentiallyMatched", 0xD30B6100, TdfType.UInt64, 11, true), // Tag: TPMA
        new TdfMemberInfo("TimeToMatch", "mTimeToMatch", 0xD34B4000, TdfType.Int64, 12, true), // Tag: TTM
        new TdfMemberInfo("TotalUsersInGame", "mTotalUsersInGame", 0xD359ED00, TdfType.UInt64, 13, true), // Tag: TUGM
        new TdfMemberInfo("TotalUsersInMatchmaking", "mTotalUsersInMatchmaking", 0xD35B6D00, TdfType.UInt64, 14, true), // Tag: TUMM
        new TdfMemberInfo("TotalUsersOnline", "mTotalUsersOnline", 0xD35CF200, TdfType.UInt64, 15, true), // Tag: TUSR
        new TdfMemberInfo("UserSessionId", "mUserSessionId", 0xD73A6400, TdfType.UInt64, 16, true), // Tag: USID
    ];
    private ITdfMember[] __members;

    private TdfInt64 _estimatedTimeToMatch = new(__typeInfos[0]);
    private TdfUInt32 _fitScore = new(__typeInfos[1]);
    private TdfUInt64 _finalizationJobId = new(__typeInfos[2]);
    private TdfEnum<Blaze15SDK.Blaze.GameManager.GameEntryType> _gameEntryType = new(__typeInfos[3]);
    private TdfInt64 _initiatorId = new(__typeInfos[4]);
    private TdfUInt32 _maxPossibleFitScore = new(__typeInfos[5]);
    private TdfUInt64 _scenarioId = new(__typeInfos[6]);
    private TdfUInt64 _sessionId = new(__typeInfos[7]);
    private TdfEnum<Blaze15SDK.Blaze.GameManager.MatchmakingResult> _matchmakingResult = new(__typeInfos[8]);
    private TdfUInt64 _totalUsersMatched = new(__typeInfos[9]);
    private TdfInt64 _matchmakingTimeoutDuration = new(__typeInfos[10]);
    private TdfUInt64 _totalUsersPotentiallyMatched = new(__typeInfos[11]);
    private TdfInt64 _timeToMatch = new(__typeInfos[12]);
    private TdfUInt64 _totalUsersInGame = new(__typeInfos[13]);
    private TdfUInt64 _totalUsersInMatchmaking = new(__typeInfos[14]);
    private TdfUInt64 _totalUsersOnline = new(__typeInfos[15]);
    private TdfUInt64 _userSessionId = new(__typeInfos[16]);

    public MatchmakingSetupContext()
    {
        __members = [ _estimatedTimeToMatch, _fitScore, _finalizationJobId, _gameEntryType, _initiatorId, _maxPossibleFitScore, _scenarioId, _sessionId, _matchmakingResult, _totalUsersMatched, _matchmakingTimeoutDuration, _totalUsersPotentiallyMatched, _timeToMatch, _totalUsersInGame, _totalUsersInMatchmaking, _totalUsersOnline, _userSessionId ];
    }

    public override Tdf CreateNew() => new MatchmakingSetupContext();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "MatchmakingSetupContext";
    public override string GetFullClassName() => "Blaze::GameManager::MatchmakingSetupContext";

    public long EstimatedTimeToMatch
    {
        get => _estimatedTimeToMatch.Value;
        set => _estimatedTimeToMatch.Value = value;
    }

    public uint FitScore
    {
        get => _fitScore.Value;
        set => _fitScore.Value = value;
    }

    public ulong FinalizationJobId
    {
        get => _finalizationJobId.Value;
        set => _finalizationJobId.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.GameEntryType GameEntryType
    {
        get => _gameEntryType.Value;
        set => _gameEntryType.Value = value;
    }

    public long InitiatorId
    {
        get => _initiatorId.Value;
        set => _initiatorId.Value = value;
    }

    public uint MaxPossibleFitScore
    {
        get => _maxPossibleFitScore.Value;
        set => _maxPossibleFitScore.Value = value;
    }

    public ulong ScenarioId
    {
        get => _scenarioId.Value;
        set => _scenarioId.Value = value;
    }

    public ulong SessionId
    {
        get => _sessionId.Value;
        set => _sessionId.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.MatchmakingResult MatchmakingResult
    {
        get => _matchmakingResult.Value;
        set => _matchmakingResult.Value = value;
    }

    public ulong TotalUsersMatched
    {
        get => _totalUsersMatched.Value;
        set => _totalUsersMatched.Value = value;
    }

    public long MatchmakingTimeoutDuration
    {
        get => _matchmakingTimeoutDuration.Value;
        set => _matchmakingTimeoutDuration.Value = value;
    }

    public ulong TotalUsersPotentiallyMatched
    {
        get => _totalUsersPotentiallyMatched.Value;
        set => _totalUsersPotentiallyMatched.Value = value;
    }

    public long TimeToMatch
    {
        get => _timeToMatch.Value;
        set => _timeToMatch.Value = value;
    }

    public ulong TotalUsersInGame
    {
        get => _totalUsersInGame.Value;
        set => _totalUsersInGame.Value = value;
    }

    public ulong TotalUsersInMatchmaking
    {
        get => _totalUsersInMatchmaking.Value;
        set => _totalUsersInMatchmaking.Value = value;
    }

    public ulong TotalUsersOnline
    {
        get => _totalUsersOnline.Value;
        set => _totalUsersOnline.Value = value;
    }

    public ulong UserSessionId
    {
        get => _userSessionId.Value;
        set => _userSessionId.Value = value;
    }

}
