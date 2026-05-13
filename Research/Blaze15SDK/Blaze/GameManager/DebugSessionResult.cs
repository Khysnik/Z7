using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class DebugSessionResult : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("MatchedSessionId", "mMatchedSessionId", 0xB6DCE900, TdfType.UInt64, 0, true), // Tag: MMSI
        new TdfMemberInfo("OwnerBlazeId", "mOwnerBlazeId", 0xBE2A6400, TdfType.Int64, 1, true), // Tag: OBID
        new TdfMemberInfo("OverallFitScore", "mOverallFitScore", 0xBE6A7400, TdfType.UInt32, 2, true), // Tag: OFIT
        new TdfMemberInfo("OwnerPlayerName", "mOwnerPlayerName", 0xC2CBA100, TdfType.String, 3, true), // Tag: PLNA
        new TdfMemberInfo("RuleResults", "mRuleResults", 0xCB5B3300, TdfType.Map, 4, true), // Tag: RULS
        new TdfMemberInfo("PlayerCount", "mPlayerCount", 0xCE9EA500, TdfType.UInt16, 5, true), // Tag: SIZE
        new TdfMemberInfo("TimeToMatch", "mTimeToMatch", 0xD34B4000, TdfType.Int64, 6, true), // Tag: TTM
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _matchedSessionId = new(__typeInfos[0]);
    private TdfInt64 _ownerBlazeId = new(__typeInfos[1]);
    private TdfUInt32 _overallFitScore = new(__typeInfos[2]);
    private TdfString _ownerPlayerName = new(__typeInfos[3]);
    private TdfMap<string, Blaze15SDK.Blaze.GameManager.DebugRuleResult> _ruleResults = new(__typeInfos[4]);
    private TdfUInt16 _playerCount = new(__typeInfos[5]);
    private TdfInt64 _timeToMatch = new(__typeInfos[6]);

    public DebugSessionResult()
    {
        __members = [ _matchedSessionId, _ownerBlazeId, _overallFitScore, _ownerPlayerName, _ruleResults, _playerCount, _timeToMatch ];
    }

    public override Tdf CreateNew() => new DebugSessionResult();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "DebugSessionResult";
    public override string GetFullClassName() => "Blaze::GameManager::DebugSessionResult";

    public ulong MatchedSessionId
    {
        get => _matchedSessionId.Value;
        set => _matchedSessionId.Value = value;
    }

    public long OwnerBlazeId
    {
        get => _ownerBlazeId.Value;
        set => _ownerBlazeId.Value = value;
    }

    public uint OverallFitScore
    {
        get => _overallFitScore.Value;
        set => _overallFitScore.Value = value;
    }

    public string OwnerPlayerName
    {
        get => _ownerPlayerName.Value;
        set => _ownerPlayerName.Value = value;
    }

    public IDictionary<string, Blaze15SDK.Blaze.GameManager.DebugRuleResult> RuleResults
    {
        get => _ruleResults.Value;
        set => _ruleResults.Value = value;
    }

    public ushort PlayerCount
    {
        get => _playerCount.Value;
        set => _playerCount.Value = value;
    }

    public long TimeToMatch
    {
        get => _timeToMatch.Value;
        set => _timeToMatch.Value = value;
    }

}
