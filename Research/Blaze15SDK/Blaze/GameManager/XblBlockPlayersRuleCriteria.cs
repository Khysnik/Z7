using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class XblBlockPlayersRuleCriteria : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("DisableRule", "mDisableRule", 0x933CAC00, TdfType.Bool, 0, true), // Tag: DSRL
    ];
    private ITdfMember[] __members;

    private TdfBool _disableRule = new(__typeInfos[0]);

    public XblBlockPlayersRuleCriteria()
    {
        __members = [ _disableRule ];
    }

    public override Tdf CreateNew() => new XblBlockPlayersRuleCriteria();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "XblBlockPlayersRuleCriteria";
    public override string GetFullClassName() => "Blaze::GameManager::XblBlockPlayersRuleCriteria";

    public bool DisableRule
    {
        get => _disableRule.Value;
        set => _disableRule.Value = value;
    }

}
