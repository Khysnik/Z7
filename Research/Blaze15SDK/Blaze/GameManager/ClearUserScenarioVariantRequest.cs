using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class ClearUserScenarioVariantRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("BlazeId", "mBlazeId", 0x8A990000, TdfType.Int64, 0, true), // Tag: BID
        new TdfMemberInfo("ScenarioName", "mScenarioName", 0xCE396E00, TdfType.String, 1, true), // Tag: SCEN
    ];
    private ITdfMember[] __members;

    private TdfInt64 _blazeId = new(__typeInfos[0]);
    private TdfString _scenarioName = new(__typeInfos[1]);

    public ClearUserScenarioVariantRequest()
    {
        __members = [ _blazeId, _scenarioName ];
    }

    public override Tdf CreateNew() => new ClearUserScenarioVariantRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ClearUserScenarioVariantRequest";
    public override string GetFullClassName() => "Blaze::GameManager::ClearUserScenarioVariantRequest";

    public long BlazeId
    {
        get => _blazeId.Value;
        set => _blazeId.Value = value;
    }

    public string ScenarioName
    {
        get => _scenarioName.Value;
        set => _scenarioName.Value = value;
    }

}
