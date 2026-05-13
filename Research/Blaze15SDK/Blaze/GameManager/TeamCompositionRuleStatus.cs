using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class TeamCompositionRuleStatus : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("AcceptableCompositionsForMyTeam", "mAcceptableCompositionsForMyTeam", 0xB79D2D00, TdfType.List, 0, true), // Tag: MYTM
        new TdfMemberInfo("RuleName", "mRuleName", 0xBA1B6500, TdfType.String, 1, true), // Tag: NAME
        new TdfMemberInfo("AcceptableCompositionsForOtherTeams", "mAcceptableCompositionsForOtherTeams", 0xBF4D2D00, TdfType.List, 2, true), // Tag: OTTM
    ];
    private ITdfMember[] __members;

    private TdfList<IList<ushort>> _acceptableCompositionsForMyTeam = new(__typeInfos[0]);
    private TdfString _ruleName = new(__typeInfos[1]);
    private TdfList<IList<ushort>> _acceptableCompositionsForOtherTeams = new(__typeInfos[2]);

    public TeamCompositionRuleStatus()
    {
        __members = [ _acceptableCompositionsForMyTeam, _ruleName, _acceptableCompositionsForOtherTeams ];
    }

    public override Tdf CreateNew() => new TeamCompositionRuleStatus();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "TeamCompositionRuleStatus";
    public override string GetFullClassName() => "Blaze::GameManager::TeamCompositionRuleStatus";

    public IList<IList<ushort>> AcceptableCompositionsForMyTeam
    {
        get => _acceptableCompositionsForMyTeam.Value;
        set => _acceptableCompositionsForMyTeam.Value = value;
    }

    public string RuleName
    {
        get => _ruleName.Value;
        set => _ruleName.Value = value;
    }

    public IList<IList<ushort>> AcceptableCompositionsForOtherTeams
    {
        get => _acceptableCompositionsForOtherTeams.Value;
        set => _acceptableCompositionsForOtherTeams.Value = value;
    }

}
