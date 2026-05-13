using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class TeamCompositionRulePrefs : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("RuleName", "mRuleName", 0xBA1B6500, TdfType.String, 0, true), // Tag: NAME
        new TdfMemberInfo("MinFitThresholdName", "mMinFitThresholdName", 0xD28B2400, TdfType.String, 1, true), // Tag: THLD
    ];
    private ITdfMember[] __members;

    private TdfString _ruleName = new(__typeInfos[0]);
    private TdfString _minFitThresholdName = new(__typeInfos[1]);

    public TeamCompositionRulePrefs()
    {
        __members = [ _ruleName, _minFitThresholdName ];
    }

    public override Tdf CreateNew() => new TeamCompositionRulePrefs();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "TeamCompositionRulePrefs";
    public override string GetFullClassName() => "Blaze::GameManager::TeamCompositionRulePrefs";

    public string RuleName
    {
        get => _ruleName.Value;
        set => _ruleName.Value = value;
    }

    public string MinFitThresholdName
    {
        get => _minFitThresholdName.Value;
        set => _minFitThresholdName.Value = value;
    }

}
