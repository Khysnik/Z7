using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting.ArsonCTF_DimensionalStats;

public class SkippedPlayerReport : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("LongestTimeAlive", "longestTimeAlive", 0xB3486C00, TdfType.UInt16, 0, true), // Tag: LTAL
    ];
    private ITdfMember[] __members;

    private TdfUInt16 _longestTimeAlive = new(__typeInfos[0]);

    public SkippedPlayerReport()
    {
        __members = [ _longestTimeAlive ];
    }

    public override Tdf CreateNew() => new SkippedPlayerReport();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "SkippedPlayerReport";
    public override string GetFullClassName() => "Blaze::GameReporting::ArsonCTF_DimensionalStats::SkippedPlayerReport";

    public ushort LongestTimeAlive
    {
        get => _longestTimeAlive.Value;
        set => _longestTimeAlive.Value = value;
    }

}
