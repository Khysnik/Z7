using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Clubs;

public class GetMembersRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ClubId", "mClubId", 0x8ECA6400, TdfType.UInt64, 0, true), // Tag: CLID
        new TdfMemberInfo("MemberType", "mMemberType", 0x9A9B2D00, TdfType.Enum, 1, true), // Tag: FILM
        new TdfMemberInfo("MaxResultCount", "mMaxResultCount", 0xB78CA300, TdfType.UInt32, 2, true), // Tag: MXRC
        new TdfMemberInfo("Offset", "mOffset", 0xBE6CA300, TdfType.UInt32, 3, true), // Tag: OFRC
        new TdfMemberInfo("OrderType", "mOrderType", 0xBF293400, TdfType.Enum, 4, true), // Tag: ORDT
        new TdfMemberInfo("PersonaNamePattern", "mPersonaNamePattern", 0xC25CB000, TdfType.String, 5, true), // Tag: PERP
        new TdfMemberInfo("SkipCalcDbRows", "mSkipCalcDbRows", 0xCEB8F400, TdfType.Bool, 6, true), // Tag: SKCT
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _clubId = new(__typeInfos[0]);
    private TdfEnum<MemberTypeFilter> _memberType = new(__typeInfos[1]);
    private TdfUInt32 _maxResultCount = new(__typeInfos[2]);
    private TdfUInt32 _offset = new(__typeInfos[3]);
    private TdfEnum<MemberOrder> _orderType = new(__typeInfos[4]);
    private TdfString _personaNamePattern = new(__typeInfos[5]);
    private TdfBool _skipCalcDbRows = new(__typeInfos[6]);

    public GetMembersRequest()
    {
        __members = [ _clubId, _memberType, _maxResultCount, _offset, _orderType, _personaNamePattern, _skipCalcDbRows ];
    }

    public override Tdf CreateNew() => new GetMembersRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetMembersRequest";
    public override string GetFullClassName() => "Blaze::Clubs::GetMembersRequest";

    public ulong ClubId
    {
        get => _clubId.Value;
        set => _clubId.Value = value;
    }

    public MemberTypeFilter MemberType
    {
        get => _memberType.Value;
        set => _memberType.Value = value;
    }

    public uint MaxResultCount
    {
        get => _maxResultCount.Value;
        set => _maxResultCount.Value = value;
    }

    public uint Offset
    {
        get => _offset.Value;
        set => _offset.Value = value;
    }

    public MemberOrder OrderType
    {
        get => _orderType.Value;
        set => _orderType.Value = value;
    }

    public string PersonaNamePattern
    {
        get => _personaNamePattern.Value;
        set => _personaNamePattern.Value = value;
    }

    public bool SkipCalcDbRows
    {
        get => _skipCalcDbRows.Value;
        set => _skipCalcDbRows.Value = value;
    }

}
