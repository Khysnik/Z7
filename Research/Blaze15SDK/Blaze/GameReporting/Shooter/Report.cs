using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting.Shooter;

public class Report : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("GroupReports", "groupReports", 0x8EC8B300, TdfType.Map, 0, true), // Tag: CLBS
        new TdfMemberInfo("GameAttributes", "gameAttributes", 0x9E1B6500, TdfType.Map, 1, true), // Tag: GAME
        new TdfMemberInfo("NationReports", "nationReports", 0xBA1D2E00, TdfType.Map, 2, true), // Tag: NATN
        new TdfMemberInfo("PlayerReports", "playerReports", 0xC2CE7200, TdfType.Map, 3, true), // Tag: PLYR
        new TdfMemberInfo("ServerReports", "serverReports", 0xCF2DB200, TdfType.Map, 4, true), // Tag: SRVR
    ];
    private ITdfMember[] __members;

    private TdfMap<ulong, GroupReport> _groupReports = new(__typeInfos[0]);
    private TdfMap<string, string> _gameAttributes = new(__typeInfos[1]);
    private TdfMap<int, GroupReport> _nationReports = new(__typeInfos[2]);
    private TdfMap<long, EntityReport> _playerReports = new(__typeInfos[3]);
    private TdfMap<int, GroupReport> _serverReports = new(__typeInfos[4]);

    public Report()
    {
        __members = [ _groupReports, _gameAttributes, _nationReports, _playerReports, _serverReports ];
    }

    public override Tdf CreateNew() => new Report();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "Report";
    public override string GetFullClassName() => "Blaze::GameReporting::Shooter::Report";

    public IDictionary<ulong, GroupReport> GroupReports
    {
        get => _groupReports.Value;
        set => _groupReports.Value = value;
    }

    public IDictionary<string, string> GameAttributes
    {
        get => _gameAttributes.Value;
        set => _gameAttributes.Value = value;
    }

    public IDictionary<int, GroupReport> NationReports
    {
        get => _nationReports.Value;
        set => _nationReports.Value = value;
    }

    public IDictionary<long, EntityReport> PlayerReports
    {
        get => _playerReports.Value;
        set => _playerReports.Value = value;
    }

    public IDictionary<int, GroupReport> ServerReports
    {
        get => _serverReports.Value;
        set => _serverReports.Value = value;
    }

}
