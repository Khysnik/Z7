using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class GetScenariosAndAttributesResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ScenarioAttributeDescriptions", "mScenarioAttributeDescriptions", 0xB73A6400, TdfType.Map, 0, true), // Tag: MSID
    ];
    private ITdfMember[] __members;

    private TdfMap<string, IDictionary<string, Blaze15SDK.Blaze.GameManager.TemplateAttributeDescription>> _scenarioAttributeDescriptions = new(__typeInfos[0]);

    public GetScenariosAndAttributesResponse()
    {
        __members = [ _scenarioAttributeDescriptions ];
    }

    public override Tdf CreateNew() => new GetScenariosAndAttributesResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetScenariosAndAttributesResponse";
    public override string GetFullClassName() => "Blaze::GameManager::GetScenariosAndAttributesResponse";

    public IDictionary<string, IDictionary<string, Blaze15SDK.Blaze.GameManager.TemplateAttributeDescription>> ScenarioAttributeDescriptions
    {
        get => _scenarioAttributeDescriptions.Value;
        set => _scenarioAttributeDescriptions.Value = value;
    }

}
