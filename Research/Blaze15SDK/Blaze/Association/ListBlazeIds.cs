using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Association;

public class ListBlazeIds : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("BlazeIds", "mBlazeIds", 0x8A993300, TdfType.List, 0, true), // Tag: BIDS
    ];
    private ITdfMember[] __members;

    private TdfList<long> _blazeIds = new(__typeInfos[0]);

    public ListBlazeIds()
    {
        __members = [ _blazeIds ];
    }

    public override Tdf CreateNew() => new ListBlazeIds();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ListBlazeIds";
    public override string GetFullClassName() => "Blaze::Association::ListBlazeIds";

    public IList<long> BlazeIds
    {
        get => _blazeIds.Value;
        set => _blazeIds.Value = value;
    }

}
