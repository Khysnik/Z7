using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting.ArsonCTF_DimensionalStats;

public class PlayerReport : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Deaths", "deaths", 0x925D2800, TdfType.Int32, 0, true), // Tag: DETH
        new TdfMemberInfo("FlagsCaptured", "flagsCaptured", 0x9A387000, TdfType.UInt32, 1, true), // Tag: FCAP
        new TdfMemberInfo("Kills", "kills", 0xAE9B2C00, TdfType.Int32, 2, true), // Tag: KILL
        new TdfMemberInfo("AccountLocale", "accountLocale", 0xB2F8C000, TdfType.UInt32, 3, true), // Tag: LOC
        new TdfMemberInfo("LeavingReason", "leavingReason", 0xCADC8000, TdfType.UInt32, 4, true), // Tag: RMR
        new TdfMemberInfo("ResultMap", "resultMap", 0xCB3B7000, TdfType.Map, 5, true), // Tag: RSMP
        new TdfMemberInfo("SkippedReport", "skippedReport", 0xCEBCB000, TdfType.Variable, 6, true), // Tag: SKRP
    ];
    private ITdfMember[] __members;

    private TdfInt32 _deaths = new(__typeInfos[0]);
    private TdfUInt32 _flagsCaptured = new(__typeInfos[1]);
    private TdfInt32 _kills = new(__typeInfos[2]);
    private TdfUInt32 _accountLocale = new(__typeInfos[3]);
    private TdfUInt32 _leavingReason = new(__typeInfos[4]);
    private TdfMap<string, DimList> _resultMap = new(__typeInfos[5]);
    private TdfVariable _skippedReport = new(__typeInfos[6]);

    public PlayerReport()
    {
        __members = [ _deaths, _flagsCaptured, _kills, _accountLocale, _leavingReason, _resultMap, _skippedReport ];
    }

    public override Tdf CreateNew() => new PlayerReport();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "PlayerReport";
    public override string GetFullClassName() => "Blaze::GameReporting::ArsonCTF_DimensionalStats::PlayerReport";

    public int Deaths
    {
        get => _deaths.Value;
        set => _deaths.Value = value;
    }

    public uint FlagsCaptured
    {
        get => _flagsCaptured.Value;
        set => _flagsCaptured.Value = value;
    }

    public int Kills
    {
        get => _kills.Value;
        set => _kills.Value = value;
    }

    public uint AccountLocale
    {
        get => _accountLocale.Value;
        set => _accountLocale.Value = value;
    }

    public uint LeavingReason
    {
        get => _leavingReason.Value;
        set => _leavingReason.Value = value;
    }

    public IDictionary<string, DimList> ResultMap
    {
        get => _resultMap.Value;
        set => _resultMap.Value = value;
    }

    public object? SkippedReport
    {
        get => _skippedReport.Value;
        set => _skippedReport.Value = value;
    }

}
