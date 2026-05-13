using EATDF;
using EATDF.Members;

namespace Blaze15SDK.Blaze.GameReporting.Fifa;

public class SoloPlayerReport : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("CommonPlayerReport", "mCommonPlayerReport", 0x8EDC3200, TdfType.Struct, 0, true), // Tag: CMPR
        new TdfMemberInfo("SoloCustomPlayerData", "mSoloCustomPlayerData", 0xCE3C3200, TdfType.Struct, 1, true), // Tag: SCPR
        new TdfMemberInfo("SeasonalPlayData", "mSeasonalPlayData", 0xCF093400, TdfType.Struct, 2, true), // Tag: SPDT
    ];
    private ITdfMember[] __members;

    private TdfStruct<CommonPlayerReport?> _commonPlayerReport = new(__typeInfos[0]);
    private TdfStruct<SoloCustomPlayerData?> _soloCustomPlayerData = new(__typeInfos[1]);
    private TdfStruct<SeasonalPlayData?> _seasonalPlayData = new(__typeInfos[2]);

    public SoloPlayerReport()
    {
        __members = [_commonPlayerReport, _soloCustomPlayerData, _seasonalPlayData];
    }

    public override Tdf CreateNew() => new SoloPlayerReport();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "SoloPlayerReport";
    public override string GetFullClassName() => "Blaze::GameReporting::Fifa::SoloPlayerReport";

    public override uint GetTdfId() => 3618316305; // 0xD7883011

    public CommonPlayerReport? CommonPlayerReport { get => _commonPlayerReport.Value; set => _commonPlayerReport.Value = value; }
    public SoloCustomPlayerData? SoloCustomPlayerData { get => _soloCustomPlayerData.Value; set => _soloCustomPlayerData.Value = value; }
    public SeasonalPlayData? SeasonalPlayData { get => _seasonalPlayData.Value; set => _seasonalPlayData.Value = value; }
}
