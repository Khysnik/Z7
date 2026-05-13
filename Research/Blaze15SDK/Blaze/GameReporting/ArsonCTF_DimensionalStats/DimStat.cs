using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting.ArsonCTF_DimensionalStats;

public class DimStat : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("DimMap", "dimMap", 0x92DB7000, TdfType.Map, 0, true), // Tag: DMMP
        new TdfMemberInfo("Value", "value", 0xCF686C00, TdfType.Int32, 1, true), // Tag: SVAL
    ];
    private ITdfMember[] __members;

    private TdfMap<string, string> _dimMap = new(__typeInfos[0]);
    private TdfInt32 _value = new(__typeInfos[1]);

    public DimStat()
    {
        __members = [ _dimMap, _value ];
    }

    public override Tdf CreateNew() => new DimStat();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "DimStat";
    public override string GetFullClassName() => "Blaze::GameReporting::ArsonCTF_DimensionalStats::DimStat";

    public IDictionary<string, string> DimMap
    {
        get => _dimMap.Value;
        set => _dimMap.Value = value;
    }

    public int Value
    {
        get => _value.Value;
        set => _value.Value = value;
    }

}
