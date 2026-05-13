using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class HostBalanceRuleStatus : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("MatchedHostBalanceValue", "mMatchedHostBalanceValue", 0x8B686C00, TdfType.Enum, 0, true), // Tag: BVAL
    ];
    private ITdfMember[] __members;

    private TdfEnum<Blaze15SDK.Blaze.GameManager.HostBalanceValues> _matchedHostBalanceValue = new(__typeInfos[0]);

    public HostBalanceRuleStatus()
    {
        __members = [ _matchedHostBalanceValue ];
    }

    public override Tdf CreateNew() => new HostBalanceRuleStatus();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "HostBalanceRuleStatus";
    public override string GetFullClassName() => "Blaze::GameManager::HostBalanceRuleStatus";

    public Blaze15SDK.Blaze.GameManager.HostBalanceValues MatchedHostBalanceValue
    {
        get => _matchedHostBalanceValue.Value;
        set => _matchedHostBalanceValue.Value = value;
    }

}
