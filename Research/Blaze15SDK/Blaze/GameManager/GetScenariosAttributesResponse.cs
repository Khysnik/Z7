using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class GetScenariosAttributesResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ScenarioAttributes", "mScenarioAttributes", 0xB73A6400, TdfType.Map, 0, true), // Tag: MSID
    ];
    private ITdfMember[] __members;

    private TdfMap<string, IDictionary<string, Blaze15SDK.Blaze.GameManager.TemplateAttributeDefinition>> _scenarioAttributes = new(__typeInfos[0]);

    public GetScenariosAttributesResponse()
    {
        __members = [ _scenarioAttributes ];
    }

    public override Tdf CreateNew() => new GetScenariosAttributesResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetScenariosAttributesResponse";
    public override string GetFullClassName() => "Blaze::GameManager::GetScenariosAttributesResponse";

    public IDictionary<string, IDictionary<string, Blaze15SDK.Blaze.GameManager.TemplateAttributeDefinition>> ScenarioAttributes
    {
        get => _scenarioAttributes.Value;
        set => _scenarioAttributes.Value = value;
    }

}
