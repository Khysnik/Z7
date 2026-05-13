using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class DebugTopResult : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("GameId", "mGameId", 0x9E990000, TdfType.UInt64, 0, true), // Tag: GID
        new TdfMemberInfo("GameName", "mGameName", 0x9EE86D00, TdfType.String, 1, true), // Tag: GNAM
        new TdfMemberInfo("MaxFitScore", "mMaxFitScore", 0xB66A7400, TdfType.UInt32, 2, true), // Tag: MFIT
        new TdfMemberInfo("OverallFitScore", "mOverallFitScore", 0xBE6A7400, TdfType.UInt32, 3, true), // Tag: OFIT
        new TdfMemberInfo("OverallResult", "mOverallResult", 0xBF297300, TdfType.Enum, 4, true), // Tag: ORES
        new TdfMemberInfo("RuleResults", "mRuleResults", 0xCB5B3300, TdfType.Map, 5, true), // Tag: RULS
        new TdfMemberInfo("TimeToMatch", "mTimeToMatch", 0xD34B4000, TdfType.Int64, 6, true), // Tag: TTM
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _gameId = new(__typeInfos[0]);
    private TdfString _gameName = new(__typeInfos[1]);
    private TdfUInt32 _maxFitScore = new(__typeInfos[2]);
    private TdfUInt32 _overallFitScore = new(__typeInfos[3]);
    private TdfEnum<Blaze15SDK.Blaze.GameManager.DebugEvaluationResult> _overallResult = new(__typeInfos[4]);
    private TdfMap<string, Blaze15SDK.Blaze.GameManager.DebugRuleResult> _ruleResults = new(__typeInfos[5]);
    private TdfInt64 _timeToMatch = new(__typeInfos[6]);

    public DebugTopResult()
    {
        __members = [ _gameId, _gameName, _maxFitScore, _overallFitScore, _overallResult, _ruleResults, _timeToMatch ];
    }

    public override Tdf CreateNew() => new DebugTopResult();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "DebugTopResult";
    public override string GetFullClassName() => "Blaze::GameManager::DebugTopResult";

    public ulong GameId
    {
        get => _gameId.Value;
        set => _gameId.Value = value;
    }

    public string GameName
    {
        get => _gameName.Value;
        set => _gameName.Value = value;
    }

    public uint MaxFitScore
    {
        get => _maxFitScore.Value;
        set => _maxFitScore.Value = value;
    }

    public uint OverallFitScore
    {
        get => _overallFitScore.Value;
        set => _overallFitScore.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.DebugEvaluationResult OverallResult
    {
        get => _overallResult.Value;
        set => _overallResult.Value = value;
    }

    public IDictionary<string, Blaze15SDK.Blaze.GameManager.DebugRuleResult> RuleResults
    {
        get => _ruleResults.Value;
        set => _ruleResults.Value = value;
    }

    public long TimeToMatch
    {
        get => _timeToMatch.Value;
        set => _timeToMatch.Value = value;
    }

}
