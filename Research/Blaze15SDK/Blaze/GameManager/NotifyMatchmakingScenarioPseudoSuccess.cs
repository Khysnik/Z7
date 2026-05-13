using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class NotifyMatchmakingScenarioPseudoSuccess : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ScenarioId", "mScenarioId", 0xB738E900, TdfType.UInt64, 0, true), // Tag: MSCI
        new TdfMemberInfo("PseudoSuccessList", "mPseudoSuccessList", 0xC33CEC00, TdfType.List, 1, true), // Tag: PSSL
        new TdfMemberInfo("ScenarioVersion", "mScenarioVersion", 0xCE397600, TdfType.UInt32, 2, true), // Tag: SCEV
        new TdfMemberInfo("ScenarioName", "mScenarioName", 0xCE3BAD00, TdfType.String, 3, true), // Tag: SCNM
        new TdfMemberInfo("ScenarioVariant", "mScenarioVariant", 0xCE3DA100, TdfType.String, 4, true), // Tag: SCVA
        new TdfMemberInfo("ScenarioTimeoutDuration", "mScenarioTimeoutDuration", 0xD2FD7400, TdfType.Int64, 5, true), // Tag: TOUT
        new TdfMemberInfo("UserSessionId", "mUserSessionId", 0xD73A6400, TdfType.UInt64, 6, true), // Tag: USID
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _scenarioId = new(__typeInfos[0]);
    private TdfList<Blaze15SDK.Blaze.GameManager.NotifyMatchmakingPseudoSuccess> _pseudoSuccessList = new(__typeInfos[1]);
    private TdfUInt32 _scenarioVersion = new(__typeInfos[2]);
    private TdfString _scenarioName = new(__typeInfos[3]);
    private TdfString _scenarioVariant = new(__typeInfos[4]);
    private TdfInt64 _scenarioTimeoutDuration = new(__typeInfos[5]);
    private TdfUInt64 _userSessionId = new(__typeInfos[6]);

    public NotifyMatchmakingScenarioPseudoSuccess()
    {
        __members = [ _scenarioId, _pseudoSuccessList, _scenarioVersion, _scenarioName, _scenarioVariant, _scenarioTimeoutDuration, _userSessionId ];
    }

    public override Tdf CreateNew() => new NotifyMatchmakingScenarioPseudoSuccess();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "NotifyMatchmakingScenarioPseudoSuccess";
    public override string GetFullClassName() => "Blaze::GameManager::NotifyMatchmakingScenarioPseudoSuccess";

    public ulong ScenarioId
    {
        get => _scenarioId.Value;
        set => _scenarioId.Value = value;
    }

    public IList<Blaze15SDK.Blaze.GameManager.NotifyMatchmakingPseudoSuccess> PseudoSuccessList
    {
        get => _pseudoSuccessList.Value;
        set => _pseudoSuccessList.Value = value;
    }

    public uint ScenarioVersion
    {
        get => _scenarioVersion.Value;
        set => _scenarioVersion.Value = value;
    }

    public string ScenarioName
    {
        get => _scenarioName.Value;
        set => _scenarioName.Value = value;
    }

    public string ScenarioVariant
    {
        get => _scenarioVariant.Value;
        set => _scenarioVariant.Value = value;
    }

    public long ScenarioTimeoutDuration
    {
        get => _scenarioTimeoutDuration.Value;
        set => _scenarioTimeoutDuration.Value = value;
    }

    public ulong UserSessionId
    {
        get => _userSessionId.Value;
        set => _userSessionId.Value = value;
    }

}
