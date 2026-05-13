using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class TeamUEDBalanceRuleStatus : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("MyUEDValue", "mMyUEDValue", 0xB7596400, TdfType.Int64, 0, true), // Tag: MUED
        new TdfMemberInfo("RuleName", "mRuleName", 0xBA1B6500, TdfType.String, 1, true), // Tag: NAME
        new TdfMemberInfo("MaxTeamUEDDifferenceAccepted", "mMaxTeamUEDDifferenceAccepted", 0xCE4A6600, TdfType.UInt64, 2, true), // Tag: SDIF
    ];
    private ITdfMember[] __members;

    private TdfInt64 _myUEDValue = new(__typeInfos[0]);
    private TdfString _ruleName = new(__typeInfos[1]);
    private TdfUInt64 _maxTeamUEDDifferenceAccepted = new(__typeInfos[2]);

    public TeamUEDBalanceRuleStatus()
    {
        __members = [ _myUEDValue, _ruleName, _maxTeamUEDDifferenceAccepted ];
    }

    public override Tdf CreateNew() => new TeamUEDBalanceRuleStatus();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "TeamUEDBalanceRuleStatus";
    public override string GetFullClassName() => "Blaze::GameManager::TeamUEDBalanceRuleStatus";

    public long MyUEDValue
    {
        get => _myUEDValue.Value;
        set => _myUEDValue.Value = value;
    }

    public string RuleName
    {
        get => _ruleName.Value;
        set => _ruleName.Value = value;
    }

    public ulong MaxTeamUEDDifferenceAccepted
    {
        get => _maxTeamUEDDifferenceAccepted.Value;
        set => _maxTeamUEDDifferenceAccepted.Value = value;
    }

}
