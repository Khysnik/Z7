using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class GetScenarioVariantsResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("UserVariants", "mUserVariants", 0xD73CB600, TdfType.Map, 0, true), // Tag: USRV
        new TdfMemberInfo("Variants", "mVariants", 0xDA1CA900, TdfType.Map, 1, true), // Tag: VARI
        new TdfMemberInfo("DefaultVariant", "mDefaultVariant", 0xDA1CB400, TdfType.String, 2, true), // Tag: VART
    ];
    private ITdfMember[] __members;

    private TdfMap<string, string> _userVariants = new(__typeInfos[0]);
    private TdfMap<string, Blaze15SDK.Blaze.GameManager.ScenarioVariantConfig> _variants = new(__typeInfos[1]);
    private TdfString _defaultVariant = new(__typeInfos[2]);

    public GetScenarioVariantsResponse()
    {
        __members = [ _userVariants, _variants, _defaultVariant ];
    }

    public override Tdf CreateNew() => new GetScenarioVariantsResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetScenarioVariantsResponse";
    public override string GetFullClassName() => "Blaze::GameManager::GetScenarioVariantsResponse";

    public IDictionary<string, string> UserVariants
    {
        get => _userVariants.Value;
        set => _userVariants.Value = value;
    }

    public IDictionary<string, Blaze15SDK.Blaze.GameManager.ScenarioVariantConfig> Variants
    {
        get => _variants.Value;
        set => _variants.Value = value;
    }

    public string DefaultVariant
    {
        get => _defaultVariant.Value;
        set => _defaultVariant.Value = value;
    }

}
