using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class GetMatchmakingConfigResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("GenericRules", "mGenericRules", 0x9ECCF400, TdfType.List, 0, true), // Tag: GLST
        new TdfMemberInfo("PredefinedRules", "mPredefinedRules", 0xCACCF400, TdfType.List, 1, true), // Tag: RLST
    ];
    private ITdfMember[] __members;

    private TdfList<Blaze15SDK.Blaze.GameManager.GenericRuleConfig> _genericRules = new(__typeInfos[0]);
    private TdfList<Blaze15SDK.Blaze.GameManager.PredefinedRuleConfig> _predefinedRules = new(__typeInfos[1]);

    public GetMatchmakingConfigResponse()
    {
        __members = [ _genericRules, _predefinedRules ];
    }

    public override Tdf CreateNew() => new GetMatchmakingConfigResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetMatchmakingConfigResponse";
    public override string GetFullClassName() => "Blaze::GameManager::GetMatchmakingConfigResponse";

    public IList<Blaze15SDK.Blaze.GameManager.GenericRuleConfig> GenericRules
    {
        get => _genericRules.Value;
        set => _genericRules.Value = value;
    }

    public IList<Blaze15SDK.Blaze.GameManager.PredefinedRuleConfig> PredefinedRules
    {
        get => _predefinedRules.Value;
        set => _predefinedRules.Value = value;
    }

}
