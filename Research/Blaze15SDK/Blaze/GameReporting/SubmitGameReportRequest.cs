using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting;

public class SubmitGameReportRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("FinishedStatus", "finishedStatus", 0x9AECE800, TdfType.Enum, 0, true), // Tag: FNSH
        new TdfMemberInfo("PrivateReport", "privateReport", 0xC32DB400, TdfType.Variable, 1, true), // Tag: PRVT
        new TdfMemberInfo("GameReport", "gameReport", 0xCB0CB400, TdfType.Struct, 2, true), // Tag: RPRT
    ];
    private ITdfMember[] __members;

    private TdfEnum<GameReportPlayerFinishedStatus> _finishedStatus = new(__typeInfos[0]);
    private TdfVariable _privateReport = new(__typeInfos[1]);
    private TdfStruct<GameReport?> _gameReport = new(__typeInfos[2]);

    public SubmitGameReportRequest()
    {
        __members = [ _finishedStatus, _privateReport, _gameReport ];
    }

    public override Tdf CreateNew() => new SubmitGameReportRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "SubmitGameReportRequest";
    public override string GetFullClassName() => "Blaze::GameReporting::SubmitGameReportRequest";

    public GameReportPlayerFinishedStatus FinishedStatus
    {
        get => _finishedStatus.Value;
        set => _finishedStatus.Value = value;
    }

    public object? PrivateReport
    {
        get => _privateReport.Value;
        set => _privateReport.Value = value;
    }

    public GameReport? GameReport
    {
        get => _gameReport.Value;
        set => _gameReport.Value = value;
    }

}
