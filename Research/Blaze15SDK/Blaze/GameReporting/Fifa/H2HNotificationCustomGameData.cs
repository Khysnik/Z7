using EATDF;
using EATDF.Members;

namespace Blaze15SDK.Blaze.GameReporting.Fifa;

public class H2HNotificationCustomGameData : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("GameReportingId", "gameReportingId", 0x9F2A6400, TdfType.UInt64, 0, true), // Tag: GRID
        new TdfMemberInfo("HomeMatchHash", "homeMatchHash", 0x9E8A3300, TdfType.String, 1, true), // Tag: GHHS
        new TdfMemberInfo("AwayMatchHash", "awayMatchHash", 0x9E1A3300, TdfType.String, 2, true), // Tag: GAHS
        new TdfMemberInfo("PlayerCustomDataReports", "playerCustomDataReports", 0xC2393200, TdfType.Map, 3, true), // Tag: PCDR
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _gameReportingId = new(__typeInfos[0]);
    private TdfString _homeMatchHash = new(__typeInfos[1]);
    private TdfString _awayMatchHash = new(__typeInfos[2]);
    private TdfMap<long, H2HNotificationPlayerCustomData> _playerCustomDataReports = new(__typeInfos[3]);

    public H2HNotificationCustomGameData()
    {
        __members = [_gameReportingId, _homeMatchHash, _awayMatchHash, _playerCustomDataReports];
    }

    public override Tdf CreateNew() => new H2HNotificationCustomGameData();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "H2HNotificationCustomGameData";
    public override string GetFullClassName() => "Blaze::GameReporting::Fifa::H2HNotificationCustomGameData";
    public override uint GetTdfId() => 0x0F9BB3FB;

    public ulong GameReportingId { get => _gameReportingId.Value; set => _gameReportingId.Value = value; }
    public string HomeMatchHash { get => _homeMatchHash.Value; set => _homeMatchHash.Value = value; }
    public string AwayMatchHash { get => _awayMatchHash.Value; set => _awayMatchHash.Value = value; }
    public IDictionary<long, H2HNotificationPlayerCustomData> PlayerCustomDataReports { get => _playerCustomDataReports.Value; set => _playerCustomDataReports.Value = value; }
}
