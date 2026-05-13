using EATDF;
using EATDF.Members;

namespace Blaze15SDK.Blaze.GameReporting.OSDKGameReportBase;

public class OSDKNotifyReport : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("CustomDataReport", "customDataReport", 0x8E4CAE00, TdfType.Variable, 0, true), // Tag: CDRN
    ];
    private ITdfMember[] __members;

    private TdfVariable _customDataReport = new(__typeInfos[0]);

    public OSDKNotifyReport()
    {
        __members = [_customDataReport];
    }

    public override Tdf CreateNew() => new OSDKNotifyReport();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "OSDKNotifyReport";
    public override string GetFullClassName() => "Blaze::GameReporting::OSDKGameReportBase::OSDKNotifyReport";
    public override uint GetTdfId() => 0x1391AD77;

    public object? CustomDataReport
    {
        get => _customDataReport.Value;
        set => _customDataReport.Value = value;
    }
}
