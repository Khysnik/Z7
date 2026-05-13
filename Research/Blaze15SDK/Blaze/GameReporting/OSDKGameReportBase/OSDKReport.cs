using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting.OSDKGameReportBase;

public class OSDKReport : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("CustomReport", "customReport", 0x8E7CB400, TdfType.Variable, 0, true), // Tag: CGRT
        new TdfMemberInfo("EnhancedReport", "enhancedReport", 0x967CB400, TdfType.Variable, 1, true), // Tag: EGRT
        new TdfMemberInfo("GameReport", "gameReport", 0x9E1B7200, TdfType.Struct, 2, true), // Tag: GAMR
        new TdfMemberInfo("FieldPlayerReports", "fieldPlayerReports", 0xA66C3200, TdfType.Variable, 3, true), // Tag: IFPR
        new TdfMemberInfo("PlayerReports", "playerReports", 0xC2CE7200, TdfType.Map, 4, true), // Tag: PLYR
        new TdfMemberInfo("TeamReports", "teamReports", 0xD21B7200, TdfType.Variable, 5, true), // Tag: TAMR
    ];
    private ITdfMember[] __members;

    private TdfVariable _customReport = new(__typeInfos[0]);
    private TdfVariable _enhancedReport = new(__typeInfos[1]);
    private TdfStruct<OSDKGameReport?> _gameReport = new(__typeInfos[2]);
    private TdfVariable _fieldPlayerReports = new(__typeInfos[3]);
    private TdfMap<long, OSDKPlayerReport> _playerReports = new(__typeInfos[4]);
    private TdfVariable _teamReports = new(__typeInfos[5]);

    public OSDKReport()
    {
        __members = [_customReport, _enhancedReport, _gameReport, _fieldPlayerReports, _playerReports, _teamReports];
    }

    public override Tdf CreateNew() => new OSDKReport();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "OSDKReport";
    public override string GetFullClassName() => "Blaze::GameReporting::OSDKGameReportBase::OSDKReport";

    public override uint GetTdfId() => 2552107044; // 0x980E4024

    public object? CustomReport
    {
        get => _customReport.Value;
        set => _customReport.Value = value;
    }

    public object? EnhancedReport
    {
        get => _enhancedReport.Value;
        set => _enhancedReport.Value = value;
    }

    public OSDKGameReport? GameReport
    {
        get => _gameReport.Value;
        set => _gameReport.Value = value;
    }

    public object? FieldPlayerReports
    {
        get => _fieldPlayerReports.Value;
        set => _fieldPlayerReports.Value = value;
    }

    public IDictionary<long, OSDKPlayerReport> PlayerReports
    {
        get => _playerReports.Value;
        set => _playerReports.Value = value;
    }

    public object? TeamReports
    {
        get => _teamReports.Value;
        set => _teamReports.Value = value;
    }
}
