using EATDF;
using EATDF.Members;

namespace Blaze15SDK.Blaze.GameReporting.OSDKGameReportBase;

public class OSDKGameReport : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ArenaChallengeId", "arenaChallengeId", 0x872A6400, TdfType.UInt64, 0, true), // Tag: ARID
        new TdfMemberInfo("CustomGameReport", "customGameReport", 0x8E7CB400, TdfType.Variable, 1, true), // Tag: CGRT
        new TdfMemberInfo("CategoryId", "categoryId", 0x8F4A6400, TdfType.UInt32, 2, true), // Tag: CTID
        new TdfMemberInfo("GameReportId", "gameReportId", 0x9F2A6400, TdfType.UInt64, 3, true), // Tag: GRID
        new TdfMemberInfo("GameTime", "gameTime", 0x9F4A6D00, TdfType.UInt32, 4, true), // Tag: GTIM
        new TdfMemberInfo("Simulate", "simulate", 0xA73A6D00, TdfType.Bool, 5, true), // Tag: ISIM
        new TdfMemberInfo("LeagueId", "leagueId", 0xB27A6400, TdfType.UInt32, 6, true), // Tag: LGID
        new TdfMemberInfo("Rank", "rank", 0xCA1BAB00, TdfType.Bool, 7, true), // Tag: RANK
        new TdfMemberInfo("RoomId", "roomId", 0xCAFA6400, TdfType.UInt32, 8, true), // Tag: ROID
        new TdfMemberInfo("FinishedStatus", "finishedStatus", 0xCF4D7300, TdfType.UInt32, 9, true), // Tag: STUS
        new TdfMemberInfo("GameType", "gameType", 0xD39C2500, TdfType.String, 10, true), // Tag: TYPE
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _arenaChallengeId = new(__typeInfos[0]);
    private TdfVariable _customGameReport = new(__typeInfos[1]);
    private TdfUInt32 _categoryId = new(__typeInfos[2]);
    private TdfUInt64 _gameReportId = new(__typeInfos[3]);
    private TdfUInt32 _gameTime = new(__typeInfos[4]);
    private TdfBool _simulate = new(__typeInfos[5]);
    private TdfUInt32 _leagueId = new(__typeInfos[6]);
    private TdfBool _rank = new(__typeInfos[7]);
    private TdfUInt32 _roomId = new(__typeInfos[8]);
    private TdfUInt32 _finishedStatus = new(__typeInfos[9]);
    private TdfString _gameType = new(__typeInfos[10]);

    public OSDKGameReport()
    {
        __members = [_arenaChallengeId, _customGameReport, _categoryId, _gameReportId, _gameTime, _simulate, _leagueId, _rank, _roomId, _finishedStatus, _gameType];
    }

    public override Tdf CreateNew() => new OSDKGameReport();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "OSDKGameReport";
    public override string GetFullClassName() => "Blaze::GameReporting::OSDKGameReportBase::OSDKGameReport";

    public ulong ArenaChallengeId
    {
        get => _arenaChallengeId.Value;
        set => _arenaChallengeId.Value = value;
    }

    public object? CustomGameReport
    {
        get => _customGameReport.Value;
        set => _customGameReport.Value = value;
    }

    public uint CategoryId
    {
        get => _categoryId.Value;
        set => _categoryId.Value = value;
    }

    public ulong GameReportId
    {
        get => _gameReportId.Value;
        set => _gameReportId.Value = value;
    }

    public uint GameTime
    {
        get => _gameTime.Value;
        set => _gameTime.Value = value;
    }

    public bool Simulate
    {
        get => _simulate.Value;
        set => _simulate.Value = value;
    }

    public uint LeagueId
    {
        get => _leagueId.Value;
        set => _leagueId.Value = value;
    }

    public bool Rank
    {
        get => _rank.Value;
        set => _rank.Value = value;
    }

    public uint RoomId
    {
        get => _roomId.Value;
        set => _roomId.Value = value;
    }

    public uint FinishedStatus
    {
        get => _finishedStatus.Value;
        set => _finishedStatus.Value = value;
    }

    public string GameType
    {
        get => _gameType.Value;
        set => _gameType.Value = value;
    }
}
