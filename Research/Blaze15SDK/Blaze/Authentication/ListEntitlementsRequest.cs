using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Authentication;

public class ListEntitlementsRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("BlazeId", "mBlazeId", 0x8B5A6400, TdfType.Int64, 0, true), // Tag: BUID
        new TdfMemberInfo("PageNo", "mPageNo", 0x970CEE00, TdfType.UInt16, 1, true), // Tag: EPSN
        new TdfMemberInfo("PageSize", "mPageSize", 0x970CFA00, TdfType.UInt16, 2, true), // Tag: EPSZ
        new TdfMemberInfo("EntitlementSearchFlag", "mEntitlementSearchFlag", 0x9AC86700, TdfType.Int32, 3, true), // Tag: FLAG
        new TdfMemberInfo("GroupNameList", "mGroupNameList", 0x9EEB3300, TdfType.List, 4, true), // Tag: GNLS
    ];
    private ITdfMember[] __members;

    private TdfInt64 _blazeId = new(__typeInfos[0]);
    private TdfUInt16 _pageSize = new(__typeInfos[2]);
    private TdfUInt16 _pageNo = new(__typeInfos[1]);
    private TdfInt32 _entitlementSearchFlag = new(__typeInfos[3]);
    private TdfList<string> _groupNameList = new(__typeInfos[4]);

    public ListEntitlementsRequest()
    {
        __members = [ _blazeId, _pageNo, _pageSize, _entitlementSearchFlag, _groupNameList ];
    }

    public override Tdf CreateNew() => new ListEntitlementsRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ListEntitlementsRequest";
    public override string GetFullClassName() => "Blaze::Authentication::ListEntitlementsRequest";

    public long BlazeId { get => _blazeId.Value; set => _blazeId.Value = value; }
    public ushort PageSize { get => _pageSize.Value; set => _pageSize.Value = value; }
    public ushort PageNo { get => _pageNo.Value; set => _pageNo.Value = value; }
    public int EntitlementSearchFlag { get => _entitlementSearchFlag.Value; set => _entitlementSearchFlag.Value = value; }
    public IList<string> GroupNameList { get => _groupNameList.Value; set => _groupNameList.Value = value; }
}
