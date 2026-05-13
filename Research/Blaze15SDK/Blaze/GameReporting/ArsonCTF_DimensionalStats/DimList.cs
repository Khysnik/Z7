using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting.ArsonCTF_DimensionalStats;

public class DimList : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("DimList", "dimList", 0x933D2C00, TdfType.List, 0, true), // Tag: DSTL
    ];
    private ITdfMember[] __members;

    private TdfList<DimStat> _dimList = new(__typeInfos[0]);

    public DimList()
    {
        __members = [ _dimList ];
    }

    public override Tdf CreateNew() => new DimList();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "DimList";
    public override string GetFullClassName() => "Blaze::GameReporting::ArsonCTF_DimensionalStats::DimList";

    public IList<DimStat> DimListItems
    {
        get => _dimList.Value;
        set => _dimList.Value = value;
    }

}
