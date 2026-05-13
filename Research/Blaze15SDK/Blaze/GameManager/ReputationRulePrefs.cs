using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class ReputationRulePrefs : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ReputationRequirement", "mReputationRequirement", 0xCA5C3200, TdfType.Enum, 0, true), // Tag: REPR
    ];
    private ITdfMember[] __members;

    private TdfEnum<Blaze15SDK.Blaze.GameManager.ReputationRequirement> _reputationRequirement = new(__typeInfos[0]);

    public ReputationRulePrefs()
    {
        __members = [ _reputationRequirement ];
    }

    public override Tdf CreateNew() => new ReputationRulePrefs();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ReputationRulePrefs";
    public override string GetFullClassName() => "Blaze::GameManager::ReputationRulePrefs";

    public Blaze15SDK.Blaze.GameManager.ReputationRequirement ReputationRequirement
    {
        get => _reputationRequirement.Value;
        set => _reputationRequirement.Value = value;
    }

}
