using EATDF;
using EATDF.Members;

namespace Blaze15SDK.Blaze.GameReporting.Fifa;

public class SeasonalPlayData : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("CurrentDivision", "currentDivision", 0x8E4A7600, TdfType.UInt16, 0, true), // Tag: CDIV
        new TdfMemberInfo("CupId", "cup_id", 0x8F5A6400, TdfType.UInt16, 1, true), // Tag: CUID
        new TdfMemberInfo("UpdateDivision", "updateDivision", 0x935C2400, TdfType.UInt16, 2, true), // Tag: DUPD
        new TdfMemberInfo("GameNumber", "gameNumber", 0x9EDBB200, TdfType.UInt16, 3, true), // Tag: GMNR
        new TdfMemberInfo("SeasonId", "seasonId", 0xCE5A6400, TdfType.UInt16, 4, true), // Tag: SEID
        new TdfMemberInfo("WonCompetition", "wonCompetition", 0xDEE8F400, TdfType.UInt16, 5, true), // Tag: WNCT
        new TdfMemberInfo("WonLeagueTitle", "wonLeagueTitle", 0xDEEB3400, TdfType.Bool, 6, true), // Tag: WNLT
    ];
    private ITdfMember[] __members;

    private TdfUInt16 _currentDivision = new(__typeInfos[0]);
    private TdfUInt16 _cupId = new(__typeInfos[1]);
    private TdfUInt16 _updateDivision = new(__typeInfos[2]);
    private TdfUInt16 _gameNumber = new(__typeInfos[3]);
    private TdfUInt16 _seasonId = new(__typeInfos[4]);
    private TdfUInt16 _wonCompetition = new(__typeInfos[5]);
    private TdfBool _wonLeagueTitle = new(__typeInfos[6]);

    public SeasonalPlayData()
    {
        __members = [_currentDivision, _cupId, _updateDivision, _gameNumber, _seasonId, _wonCompetition, _wonLeagueTitle];
    }

    public override Tdf CreateNew() => new SeasonalPlayData();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "SeasonalPlayData";
    public override string GetFullClassName() => "Blaze::GameReporting::Fifa::SeasonalPlayData";

    public ushort CurrentDivision { get => _currentDivision.Value; set => _currentDivision.Value = value; }
    public ushort CupId { get => _cupId.Value; set => _cupId.Value = value; }
    public ushort UpdateDivision { get => _updateDivision.Value; set => _updateDivision.Value = value; }
    public ushort GameNumber { get => _gameNumber.Value; set => _gameNumber.Value = value; }
    public ushort SeasonId { get => _seasonId.Value; set => _seasonId.Value = value; }
    public ushort WonCompetition { get => _wonCompetition.Value; set => _wonCompetition.Value = value; }
    public bool WonLeagueTitle { get => _wonLeagueTitle.Value; set => _wonLeagueTitle.Value = value; }
}
