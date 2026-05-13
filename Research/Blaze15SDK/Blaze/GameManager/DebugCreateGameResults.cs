using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class DebugCreateGameResults : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("CreatorId", "mCreatorId", 0x8E2A6400, TdfType.Int64, 0, true), // Tag: CBID
        new TdfMemberInfo("CreateGameRequest", "mCreateGameRequest", 0x8E7C8000, TdfType.Struct, 1, true), // Tag: CGR
        new TdfMemberInfo("SessionResultList", "mSessionResultList", 0x933CAC00, TdfType.List, 2, true), // Tag: DSRL
        new TdfMemberInfo("FitScore", "mFitScore", 0x9A9D3300, TdfType.UInt32, 3, true), // Tag: FITS
        new TdfMemberInfo("FinalizationRuleResults", "mFinalizationRuleResults", 0x9B2CEC00, TdfType.Map, 4, true), // Tag: FRSL
        new TdfMemberInfo("MaxFitScore", "mMaxFitScore", 0xB66A7400, TdfType.UInt32, 5, true), // Tag: MFIT
        new TdfMemberInfo("TimeToMatch", "mTimeToMatch", 0xD34B4000, TdfType.Int64, 6, true), // Tag: TTM
    ];
    private ITdfMember[] __members;

    private TdfInt64 _creatorId = new(__typeInfos[0]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.CreateGameRequest?> _createGameRequest = new(__typeInfos[1]);
    private TdfList<Blaze15SDK.Blaze.GameManager.DebugSessionResult> _sessionResultList = new(__typeInfos[2]);
    private TdfUInt32 _fitScore = new(__typeInfos[3]);
    private TdfMap<string, Blaze15SDK.Blaze.GameManager.DebugRuleResult> _finalizationRuleResults = new(__typeInfos[4]);
    private TdfUInt32 _maxFitScore = new(__typeInfos[5]);
    private TdfInt64 _timeToMatch = new(__typeInfos[6]);

    public DebugCreateGameResults()
    {
        __members = [ _creatorId, _createGameRequest, _sessionResultList, _fitScore, _finalizationRuleResults, _maxFitScore, _timeToMatch ];
    }

    public override Tdf CreateNew() => new DebugCreateGameResults();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "DebugCreateGameResults";
    public override string GetFullClassName() => "Blaze::GameManager::DebugCreateGameResults";

    public long CreatorId
    {
        get => _creatorId.Value;
        set => _creatorId.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.CreateGameRequest? CreateGameRequest
    {
        get => _createGameRequest.Value;
        set => _createGameRequest.Value = value;
    }

    public IList<Blaze15SDK.Blaze.GameManager.DebugSessionResult> SessionResultList
    {
        get => _sessionResultList.Value;
        set => _sessionResultList.Value = value;
    }

    public uint FitScore
    {
        get => _fitScore.Value;
        set => _fitScore.Value = value;
    }

    public IDictionary<string, Blaze15SDK.Blaze.GameManager.DebugRuleResult> FinalizationRuleResults
    {
        get => _finalizationRuleResults.Value;
        set => _finalizationRuleResults.Value = value;
    }

    public uint MaxFitScore
    {
        get => _maxFitScore.Value;
        set => _maxFitScore.Value = value;
    }

    public long TimeToMatch
    {
        get => _timeToMatch.Value;
        set => _timeToMatch.Value = value;
    }

}
