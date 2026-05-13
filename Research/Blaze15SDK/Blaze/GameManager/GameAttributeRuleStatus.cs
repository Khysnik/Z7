using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class GameAttributeRuleStatus : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("RuleName", "mRuleName", 0xBA1B6500, TdfType.String, 0, true), // Tag: NAME
        new TdfMemberInfo("MatchedValues", "mMatchedValues", 0xDA1B3500, TdfType.List, 1, true), // Tag: VALU
    ];
    private ITdfMember[] __members;

    private TdfString _ruleName = new(__typeInfos[0]);
    private TdfList<string> _matchedValues = new(__typeInfos[1]);

    public GameAttributeRuleStatus()
    {
        __members = [ _ruleName, _matchedValues ];
    }

    public override Tdf CreateNew() => new GameAttributeRuleStatus();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GameAttributeRuleStatus";
    public override string GetFullClassName() => "Blaze::GameManager::GameAttributeRuleStatus";

    public string RuleName
    {
        get => _ruleName.Value;
        set => _ruleName.Value = value;
    }

    public IList<string> MatchedValues
    {
        get => _matchedValues.Value;
        set => _matchedValues.Value = value;
    }

}
