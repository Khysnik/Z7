using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class GetGameBrowserScenariosAttributesResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("GameBrowserScenarioAttributes", "mGameBrowserScenarioAttributes", 0x9E2CA100, TdfType.Map, 0, true), // Tag: GBRA
    ];
    private ITdfMember[] __members;

    private TdfMap<string, IDictionary<string, Blaze15SDK.Blaze.GameManager.TemplateAttributeDefinition>> _gameBrowserScenarioAttributes = new(__typeInfos[0]);

    public GetGameBrowserScenariosAttributesResponse()
    {
        __members = [ _gameBrowserScenarioAttributes ];
    }

    public override Tdf CreateNew() => new GetGameBrowserScenariosAttributesResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetGameBrowserScenariosAttributesResponse";
    public override string GetFullClassName() => "Blaze::GameManager::GetGameBrowserScenariosAttributesResponse";

    public IDictionary<string, IDictionary<string, Blaze15SDK.Blaze.GameManager.TemplateAttributeDefinition>> GameBrowserScenarioAttributes
    {
        get => _gameBrowserScenarioAttributes.Value;
        set => _gameBrowserScenarioAttributes.Value = value;
    }

}
