using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class StartMatchmakingScenarioResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ExternalSessionCorrelationId", "mExternalSessionCorrelationId", 0x8EFA6400, TdfType.String, 0, true), // Tag: COID
        new TdfMemberInfo("ExternalSessionName", "mExternalSessionName", 0x973BAD00, TdfType.String, 1, true), // Tag: ESNM
        new TdfMemberInfo("EstimatedTimeToMatch", "mEstimatedTimeToMatch", 0x974D2D00, TdfType.Int64, 2, true), // Tag: ETTM
        new TdfMemberInfo("ScenarioId", "mScenarioId", 0xB73A6400, TdfType.UInt64, 3, true), // Tag: MSID
        new TdfMemberInfo("RecoTrackingTag", "mRecoTrackingTag", 0xCB486700, TdfType.String, 4, true), // Tag: RTAG
        new TdfMemberInfo("Scid", "mScid", 0xCE3A6400, TdfType.String, 5, true), // Tag: SCID
        new TdfMemberInfo("ExternalSessionTemplateName", "mExternalSessionTemplateName", 0xCF4B6E00, TdfType.String, 6, true), // Tag: STMN
        new TdfMemberInfo("ScenarioVariant", "mScenarioVariant", 0xDA1CA900, TdfType.String, 7, true), // Tag: VARI
    ];
    private ITdfMember[] __members;

    private TdfString _externalSessionCorrelationId = new(__typeInfos[0]);
    private TdfString _externalSessionName = new(__typeInfos[1]);
    private TdfInt64 _estimatedTimeToMatch = new(__typeInfos[2]);
    private TdfUInt64 _scenarioId = new(__typeInfos[3]);
    private TdfString _recoTrackingTag = new(__typeInfos[4]);
    private TdfString _scid = new(__typeInfos[5]);
    private TdfString _externalSessionTemplateName = new(__typeInfos[6]);
    private TdfString _scenarioVariant = new(__typeInfos[7]);

    public StartMatchmakingScenarioResponse()
    {
        __members = [ _externalSessionCorrelationId, _externalSessionName, _estimatedTimeToMatch, _scenarioId, _recoTrackingTag, _scid, _externalSessionTemplateName, _scenarioVariant ];
    }

    public override Tdf CreateNew() => new StartMatchmakingScenarioResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "StartMatchmakingScenarioResponse";
    public override string GetFullClassName() => "Blaze::GameManager::StartMatchmakingScenarioResponse";

    public string ExternalSessionCorrelationId
    {
        get => _externalSessionCorrelationId.Value;
        set => _externalSessionCorrelationId.Value = value;
    }

    public string ExternalSessionName
    {
        get => _externalSessionName.Value;
        set => _externalSessionName.Value = value;
    }

    public long EstimatedTimeToMatch
    {
        get => _estimatedTimeToMatch.Value;
        set => _estimatedTimeToMatch.Value = value;
    }

    public ulong ScenarioId
    {
        get => _scenarioId.Value;
        set => _scenarioId.Value = value;
    }

    public string RecoTrackingTag
    {
        get => _recoTrackingTag.Value;
        set => _recoTrackingTag.Value = value;
    }

    public string Scid
    {
        get => _scid.Value;
        set => _scid.Value = value;
    }

    public string ExternalSessionTemplateName
    {
        get => _externalSessionTemplateName.Value;
        set => _externalSessionTemplateName.Value = value;
    }

    public string ScenarioVariant
    {
        get => _scenarioVariant.Value;
        set => _scenarioVariant.Value = value;
    }

}
