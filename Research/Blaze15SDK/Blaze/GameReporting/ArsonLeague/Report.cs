using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting.ArsonLeague;

public class Report : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("GameAttrs", "gameAttrs", 0x9E1B6500, TdfType.Struct, 0, true), // Tag: GAME
        new TdfMemberInfo("AthleteReports", "athleteReports", 0xBE69B300, TdfType.Map, 1, true), // Tag: OFFS
        new TdfMemberInfo("PlayerReports", "playerReports", 0xC2CE7200, TdfType.Map, 2, true), // Tag: PLYR
    ];
    private ITdfMember[] __members;

    private TdfStruct<GameAttributes?> _gameAttrs = new(__typeInfos[0]);
    private TdfMap<ulong, AthleteReport> _athleteReports = new(__typeInfos[1]);
    private TdfMap<long, PlayerReport> _playerReports = new(__typeInfos[2]);

    public Report()
    {
        __members = [ _gameAttrs, _athleteReports, _playerReports ];
    }

    public override Tdf CreateNew() => new Report();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "Report";
    public override string GetFullClassName() => "Blaze::GameReporting::ArsonLeague::Report";

    public GameAttributes? GameAttrs
    {
        get => _gameAttrs.Value;
        set => _gameAttrs.Value = value;
    }

    public IDictionary<ulong, AthleteReport> AthleteReports
    {
        get => _athleteReports.Value;
        set => _athleteReports.Value = value;
    }

    public IDictionary<long, PlayerReport> PlayerReports
    {
        get => _playerReports.Value;
        set => _playerReports.Value = value;
    }

}
