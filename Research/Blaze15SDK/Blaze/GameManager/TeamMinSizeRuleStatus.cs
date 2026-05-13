using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class TeamMinSizeRuleStatus : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("TeamMinSizeAccepted", "mTeamMinSizeAccepted", 0xC23BB400, TdfType.UInt16, 0, true), // Tag: PCNT
    ];
    private ITdfMember[] __members;

    private TdfUInt16 _teamMinSizeAccepted = new(__typeInfos[0]);

    public TeamMinSizeRuleStatus()
    {
        __members = [ _teamMinSizeAccepted ];
    }

    public override Tdf CreateNew() => new TeamMinSizeRuleStatus();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "TeamMinSizeRuleStatus";
    public override string GetFullClassName() => "Blaze::GameManager::TeamMinSizeRuleStatus";

    public ushort TeamMinSizeAccepted
    {
        get => _teamMinSizeAccepted.Value;
        set => _teamMinSizeAccepted.Value = value;
    }

}
