using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public enum ExternalSessionRecentPlayersGroupingMode : int {
    NO_GROUPING = 0x0,
    GAME_ROSTER = 0x01,
    TEAM = 0x02,
}

public class ExternalSessionRecentPlayersSettings : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Grouping", "mGrouping", 0x9F2C0000, TdfType.Enum, 0, true), // Tag: GRP
        new TdfMemberInfo("MaxPlayersPerUpdate", "mMaxPlayersPerUpdate", 0xB61E3000, TdfType.UInt64, 1, true), // Tag: MAXP
    ];
    private ITdfMember[] __members;

    private TdfEnum<ExternalSessionRecentPlayersGroupingMode> _grouping = new(__typeInfos[0]);
    private TdfUInt64 _maxPlayersPerUpdate = new(__typeInfos[1]);

    public ExternalSessionRecentPlayersSettings()
    {
        __members = [ _grouping, _maxPlayersPerUpdate ];
    }

    public override Tdf CreateNew() => new ExternalSessionRecentPlayersSettings();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ExternalSessionRecentPlayersSettings";
    public override string GetFullClassName() => "Blaze::ExternalSessionRecentPlayersSettings";

    public ExternalSessionRecentPlayersGroupingMode Grouping
    {
        get => _grouping.Value;
        set => _grouping.Value = value;
    }

    public ulong MaxPlayersPerUpdate
    {
        get => _maxPlayersPerUpdate.Value;
        set => _maxPlayersPerUpdate.Value = value;
    }

}
