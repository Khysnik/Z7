using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Clubs;

public class GetClubsRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ClubIdList", "mClubIdList", 0x8ECA6400, TdfType.List, 0, true), // Tag: CLID
        new TdfMemberInfo("IncludeClubTags", "mIncludeClubTags", 0x8ECD2900, TdfType.Bool, 1, true), // Tag: CLTI
        new TdfMemberInfo("ClubsOrder", "mClubsOrder", 0x8EF93200, TdfType.Enum, 2, true), // Tag: CODR
        new TdfMemberInfo("MaxResultCount", "mMaxResultCount", 0xB78CA300, TdfType.UInt32, 3, true), // Tag: MXRC
        new TdfMemberInfo("OrderMode", "mOrderMode", 0xBE4B6400, TdfType.Enum, 4, true), // Tag: ODMD
        new TdfMemberInfo("Offset", "mOffset", 0xBE6CA300, TdfType.UInt32, 5, true), // Tag: OFRC
        new TdfMemberInfo("SkipCalcDbRows", "mSkipCalcDbRows", 0xCEB8F400, TdfType.Bool, 6, true), // Tag: SKCT
    ];
    private ITdfMember[] __members;

    private TdfList<ulong> _clubIdList = new(__typeInfos[0]);
    private TdfBool _includeClubTags = new(__typeInfos[1]);
    private TdfEnum<ClubsOrder> _clubsOrder = new(__typeInfos[2]);
    private TdfUInt32 _maxResultCount = new(__typeInfos[3]);
    private TdfEnum<OrderMode> _orderMode = new(__typeInfos[4]);
    private TdfUInt32 _offset = new(__typeInfos[5]);
    private TdfBool _skipCalcDbRows = new(__typeInfos[6]);

    public GetClubsRequest()
    {
        __members = [ _clubIdList, _includeClubTags, _clubsOrder, _maxResultCount, _orderMode, _offset, _skipCalcDbRows ];
    }

    public override Tdf CreateNew() => new GetClubsRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetClubsRequest";
    public override string GetFullClassName() => "Blaze::Clubs::GetClubsRequest";

    public IList<ulong> ClubIdList
    {
        get => _clubIdList.Value;
        set => _clubIdList.Value = value;
    }

    public bool IncludeClubTags
    {
        get => _includeClubTags.Value;
        set => _includeClubTags.Value = value;
    }

    public ClubsOrder ClubsOrder
    {
        get => _clubsOrder.Value;
        set => _clubsOrder.Value = value;
    }

    public uint MaxResultCount
    {
        get => _maxResultCount.Value;
        set => _maxResultCount.Value = value;
    }

    public OrderMode OrderMode
    {
        get => _orderMode.Value;
        set => _orderMode.Value = value;
    }

    public uint Offset
    {
        get => _offset.Value;
        set => _offset.Value = value;
    }

    public bool SkipCalcDbRows
    {
        get => _skipCalcDbRows.Value;
        set => _skipCalcDbRows.Value = value;
    }

}
