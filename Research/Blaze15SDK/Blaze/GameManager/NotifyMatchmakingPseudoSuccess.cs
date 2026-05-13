using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class NotifyMatchmakingPseudoSuccess : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("CreateGameResults", "mCreateGameResults", 0x9239F200, TdfType.Struct, 0, true), // Tag: DCGR
        new TdfMemberInfo("FindGameResults", "mFindGameResults", 0x9269F200, TdfType.Struct, 1, true), // Tag: DFGR
        new TdfMemberInfo("MatchmakingResult", "mMatchmakingResult", 0xB6DC8000, TdfType.Enum, 2, true), // Tag: MMR
        new TdfMemberInfo("SessionId", "mSessionId", 0xB6DCE900, TdfType.UInt64, 3, true), // Tag: MMSI
        new TdfMemberInfo("ScenarioId", "mScenarioId", 0xB738E900, TdfType.UInt64, 4, true), // Tag: MSCI
        new TdfMemberInfo("SubSessionName", "mSubSessionName", 0xCF58AE00, TdfType.String, 5, true), // Tag: SUBN
        new TdfMemberInfo("UserSessionId", "mUserSessionId", 0xD73A6400, TdfType.UInt64, 6, true), // Tag: USID
    ];
    private ITdfMember[] __members;

    private TdfStruct<Blaze15SDK.Blaze.GameManager.DebugCreateGameResults?> _createGameResults = new(__typeInfos[0]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.DebugFindGameResults?> _findGameResults = new(__typeInfos[1]);
    private TdfEnum<Blaze15SDK.Blaze.GameManager.MatchmakingResult> _matchmakingResult = new(__typeInfos[2]);
    private TdfUInt64 _sessionId = new(__typeInfos[3]);
    private TdfUInt64 _scenarioId = new(__typeInfos[4]);
    private TdfString _subSessionName = new(__typeInfos[5]);
    private TdfUInt64 _userSessionId = new(__typeInfos[6]);

    public NotifyMatchmakingPseudoSuccess()
    {
        __members = [ _createGameResults, _findGameResults, _matchmakingResult, _sessionId, _scenarioId, _subSessionName, _userSessionId ];
    }

    public override Tdf CreateNew() => new NotifyMatchmakingPseudoSuccess();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "NotifyMatchmakingPseudoSuccess";
    public override string GetFullClassName() => "Blaze::GameManager::NotifyMatchmakingPseudoSuccess";

    public Blaze15SDK.Blaze.GameManager.DebugCreateGameResults? CreateGameResults
    {
        get => _createGameResults.Value;
        set => _createGameResults.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.DebugFindGameResults? FindGameResults
    {
        get => _findGameResults.Value;
        set => _findGameResults.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.MatchmakingResult MatchmakingResult
    {
        get => _matchmakingResult.Value;
        set => _matchmakingResult.Value = value;
    }

    public ulong SessionId
    {
        get => _sessionId.Value;
        set => _sessionId.Value = value;
    }

    public ulong ScenarioId
    {
        get => _scenarioId.Value;
        set => _scenarioId.Value = value;
    }

    public string SubSessionName
    {
        get => _subSessionName.Value;
        set => _subSessionName.Value = value;
    }

    public ulong UserSessionId
    {
        get => _userSessionId.Value;
        set => _userSessionId.Value = value;
    }

}
