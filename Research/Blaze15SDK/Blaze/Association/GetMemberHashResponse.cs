using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Association;

public class GetMemberHashResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("BlazeId", "mBlazeId", 0x8A990000, TdfType.Int64, 0, true), // Tag: BID
        new TdfMemberInfo("MemberHash", "mMemberHash", 0xA21CE800, TdfType.UInt32, 1, true), // Tag: HASH
        new TdfMemberInfo("ListType", "mListType", 0xD39C2500, TdfType.UInt16, 2, true), // Tag: TYPE
    ];
    private ITdfMember[] __members;

    private TdfInt64 _blazeId = new(__typeInfos[0]);
    private TdfUInt32 _memberHash = new(__typeInfos[1]);
    private TdfUInt16 _listType = new(__typeInfos[2]);

    public GetMemberHashResponse()
    {
        __members = [ _blazeId, _memberHash, _listType ];
    }

    public override Tdf CreateNew() => new GetMemberHashResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetMemberHashResponse";
    public override string GetFullClassName() => "Blaze::Association::GetMemberHashResponse";

    public long BlazeId
    {
        get => _blazeId.Value;
        set => _blazeId.Value = value;
    }

    public uint MemberHash
    {
        get => _memberHash.Value;
        set => _memberHash.Value = value;
    }

    public ushort ListType
    {
        get => _listType.Value;
        set => _listType.Value = value;
    }

}
