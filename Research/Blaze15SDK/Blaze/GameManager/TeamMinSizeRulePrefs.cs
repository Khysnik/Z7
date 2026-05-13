using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class TeamMinSizeRulePrefs : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("TeamMinSize", "mTeamMinSize", 0xC23BB400, TdfType.UInt16, 0, true), // Tag: PCNT
        new TdfMemberInfo("RangeOffsetListName", "mRangeOffsetListName", 0xD28B2400, TdfType.String, 1, true), // Tag: THLD
    ];
    private ITdfMember[] __members;

    private TdfUInt16 _teamMinSize = new(__typeInfos[0]);
    private TdfString _rangeOffsetListName = new(__typeInfos[1]);

    public TeamMinSizeRulePrefs()
    {
        __members = [ _teamMinSize, _rangeOffsetListName ];
    }

    public override Tdf CreateNew() => new TeamMinSizeRulePrefs();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "TeamMinSizeRulePrefs";
    public override string GetFullClassName() => "Blaze::GameManager::TeamMinSizeRulePrefs";

    public ushort TeamMinSize
    {
        get => _teamMinSize.Value;
        set => _teamMinSize.Value = value;
    }

    public string RangeOffsetListName
    {
        get => _rangeOffsetListName.Value;
        set => _rangeOffsetListName.Value = value;
    }

}
