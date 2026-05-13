using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class GetScenarioVariantsRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ScenarioName", "mScenarioName", 0xCE396E00, TdfType.String, 0, true), // Tag: SCEN
    ];
    private ITdfMember[] __members;

    private TdfString _scenarioName = new(__typeInfos[0]);

    public GetScenarioVariantsRequest()
    {
        __members = [ _scenarioName ];
    }

    public override Tdf CreateNew() => new GetScenarioVariantsRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetScenarioVariantsRequest";
    public override string GetFullClassName() => "Blaze::GameManager::GetScenarioVariantsRequest";

    public string ScenarioName
    {
        get => _scenarioName.Value;
        set => _scenarioName.Value = value;
    }

}
