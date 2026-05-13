using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class TeamCountRulePrefs : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("TeamCount", "mTeamCount", 0xD23BB400, TdfType.UInt16, 0, true), // Tag: TCNT
    ];
    private ITdfMember[] __members;

    private TdfUInt16 _teamCount = new(__typeInfos[0]);

    public TeamCountRulePrefs()
    {
        __members = [ _teamCount ];
    }

    public override Tdf CreateNew() => new TeamCountRulePrefs();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "TeamCountRulePrefs";
    public override string GetFullClassName() => "Blaze::GameManager::TeamCountRulePrefs";

    public ushort TeamCount
    {
        get => _teamCount.Value;
        set => _teamCount.Value = value;
    }

}
