using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class TeamBalanceRuleStatus : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("MaxTeamSizeDifferenceAccepted", "mMaxTeamSizeDifferenceAccepted", 0xCE4A6600, TdfType.UInt16, 0, true), // Tag: SDIF
    ];
    private ITdfMember[] __members;

    private TdfUInt16 _maxTeamSizeDifferenceAccepted = new(__typeInfos[0]);

    public TeamBalanceRuleStatus()
    {
        __members = [ _maxTeamSizeDifferenceAccepted ];
    }

    public override Tdf CreateNew() => new TeamBalanceRuleStatus();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "TeamBalanceRuleStatus";
    public override string GetFullClassName() => "Blaze::GameManager::TeamBalanceRuleStatus";

    public ushort MaxTeamSizeDifferenceAccepted
    {
        get => _maxTeamSizeDifferenceAccepted.Value;
        set => _maxTeamSizeDifferenceAccepted.Value = value;
    }

}
