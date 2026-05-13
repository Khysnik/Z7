using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting.ArsonClubGameKeyscopes_NonDerived;

public class Report : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ClubReports", "clubReports", 0x8ECD6200, TdfType.Map, 0, true), // Tag: CLUB
        new TdfMemberInfo("OffensiveAthleteReports", "offensiveAthleteReports", 0xBE69B300, TdfType.Map, 1, true), // Tag: OFFS
        new TdfMemberInfo("PlayerReports", "playerReports", 0xC2CE7200, TdfType.Map, 2, true), // Tag: PLYR
    ];
    private ITdfMember[] __members;

    private TdfMap<ulong, ClubReport> _clubReports = new(__typeInfos[0]);
    private TdfMap<uint, OffensiveAthlete> _offensiveAthleteReports = new(__typeInfos[1]);
    private TdfMap<long, PlayerReport> _playerReports = new(__typeInfos[2]);

    public Report()
    {
        __members = [ _clubReports, _offensiveAthleteReports, _playerReports ];
    }

    public override Tdf CreateNew() => new Report();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "Report";
    public override string GetFullClassName() => "Blaze::GameReporting::ArsonClubGameKeyscopes_NonDerived::Report";

    public IDictionary<ulong, ClubReport> ClubReports
    {
        get => _clubReports.Value;
        set => _clubReports.Value = value;
    }

    public IDictionary<uint, OffensiveAthlete> OffensiveAthleteReports
    {
        get => _offensiveAthleteReports.Value;
        set => _offensiveAthleteReports.Value = value;
    }

    public IDictionary<long, PlayerReport> PlayerReports
    {
        get => _playerReports.Value;
        set => _playerReports.Value = value;
    }

}
