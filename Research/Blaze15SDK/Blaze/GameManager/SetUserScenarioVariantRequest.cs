using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class SetUserScenarioVariantRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Persona", "mPersona", 0xC25CB300, TdfType.String, 0, true), // Tag: PERS
        new TdfMemberInfo("ScenarioName", "mScenarioName", 0xCE396E00, TdfType.String, 1, true), // Tag: SCEN
        new TdfMemberInfo("ScenarioVariant", "mScenarioVariant", 0xCE3DA100, TdfType.String, 2, true), // Tag: SCVA
    ];
    private ITdfMember[] __members;

    private TdfString _persona = new(__typeInfos[0]);
    private TdfString _scenarioName = new(__typeInfos[1]);
    private TdfString _scenarioVariant = new(__typeInfos[2]);

    public SetUserScenarioVariantRequest()
    {
        __members = [ _persona, _scenarioName, _scenarioVariant ];
    }

    public override Tdf CreateNew() => new SetUserScenarioVariantRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "SetUserScenarioVariantRequest";
    public override string GetFullClassName() => "Blaze::GameManager::SetUserScenarioVariantRequest";

    public string Persona
    {
        get => _persona.Value;
        set => _persona.Value = value;
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

}
