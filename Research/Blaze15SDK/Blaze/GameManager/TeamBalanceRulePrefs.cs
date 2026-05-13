using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class TeamBalanceRulePrefs : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("MaxTeamSizeDifferenceAllowed", "mMaxTeamSizeDifferenceAllowed", 0xCE4A6600, TdfType.UInt16, 0, true), // Tag: SDIF
        new TdfMemberInfo("RangeOffsetListName", "mRangeOffsetListName", 0xD28B2400, TdfType.String, 1, true), // Tag: THLD
    ];
    private ITdfMember[] __members;

    private TdfUInt16 _maxTeamSizeDifferenceAllowed = new(__typeInfos[0]);
    private TdfString _rangeOffsetListName = new(__typeInfos[1]);

    public TeamBalanceRulePrefs()
    {
        __members = [ _maxTeamSizeDifferenceAllowed, _rangeOffsetListName ];
    }

    public override Tdf CreateNew() => new TeamBalanceRulePrefs();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "TeamBalanceRulePrefs";
    public override string GetFullClassName() => "Blaze::GameManager::TeamBalanceRulePrefs";

    public ushort MaxTeamSizeDifferenceAllowed
    {
        get => _maxTeamSizeDifferenceAllowed.Value;
        set => _maxTeamSizeDifferenceAllowed.Value = value;
    }

    public string RangeOffsetListName
    {
        get => _rangeOffsetListName.Value;
        set => _rangeOffsetListName.Value = value;
    }

}
