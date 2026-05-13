using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Clubs;

public class GetNewsForClubsRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ClubIdList", "mClubIdList", 0x8E992C00, TdfType.List, 0, true), // Tag: CIDL
        new TdfMemberInfo("MaxResultCount", "mMaxResultCount", 0xB63BB400, TdfType.UInt32, 1, true), // Tag: MCNT
        new TdfMemberInfo("SortType", "mSortType", 0xBB3BF400, TdfType.Enum, 2, true), // Tag: NSOT
        new TdfMemberInfo("OffSet", "mOffSet", 0xBE6CF400, TdfType.UInt32, 3, true), // Tag: OFST
        new TdfMemberInfo("TypeFilters", "mTypeFilters", 0xD26A6C00, TdfType.List, 4, true), // Tag: TFIL
    ];
    private ITdfMember[] __members;

    private TdfList<ulong> _clubIdList = new(__typeInfos[0]);
    private TdfUInt32 _maxResultCount = new(__typeInfos[1]);
    private TdfEnum<TimeSortType> _sortType = new(__typeInfos[2]);
    private TdfUInt32 _offSet = new(__typeInfos[3]);
    private TdfList<NewsType> _typeFilters = new(__typeInfos[4]);

    public GetNewsForClubsRequest()
    {
        __members = [ _clubIdList, _maxResultCount, _sortType, _offSet, _typeFilters ];
    }

    public override Tdf CreateNew() => new GetNewsForClubsRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetNewsForClubsRequest";
    public override string GetFullClassName() => "Blaze::Clubs::GetNewsForClubsRequest";

    public IList<ulong> ClubIdList
    {
        get => _clubIdList.Value;
        set => _clubIdList.Value = value;
    }

    public uint MaxResultCount
    {
        get => _maxResultCount.Value;
        set => _maxResultCount.Value = value;
    }

    public TimeSortType SortType
    {
        get => _sortType.Value;
        set => _sortType.Value = value;
    }

    public uint OffSet
    {
        get => _offSet.Value;
        set => _offSet.Value = value;
    }

    public IList<NewsType> TypeFilters
    {
        get => _typeFilters.Value;
        set => _typeFilters.Value = value;
    }

}
