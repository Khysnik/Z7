using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class HostViabilityRuleStatus : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("MatchedHostViabilityValue", "mMatchedHostViabilityValue", 0xDB686C00, TdfType.Enum, 0, true), // Tag: VVAL
    ];
    private ITdfMember[] __members;

    private TdfEnum<Blaze15SDK.Blaze.GameManager.HostViabilityValues> _matchedHostViabilityValue = new(__typeInfos[0]);

    public HostViabilityRuleStatus()
    {
        __members = [ _matchedHostViabilityValue ];
    }

    public override Tdf CreateNew() => new HostViabilityRuleStatus();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "HostViabilityRuleStatus";
    public override string GetFullClassName() => "Blaze::GameManager::HostViabilityRuleStatus";

    public Blaze15SDK.Blaze.GameManager.HostViabilityValues MatchedHostViabilityValue
    {
        get => _matchedHostViabilityValue.Value;
        set => _matchedHostViabilityValue.Value = value;
    }

}
