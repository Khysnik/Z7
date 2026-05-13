using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Association;

public class UpdateListMembersResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("OwnerId", "mOwnerId", 0x8A990000, TdfType.Int64, 0, true), // Tag: BID
        new TdfMemberInfo("ListMemberInfoVector", "mListMemberInfoVector", 0xB2DA6400, TdfType.List, 1, true), // Tag: LMID
        new TdfMemberInfo("RemovedListMemberIdVector", "mRemovedListMemberIdVector", 0xCA5B4000, TdfType.List, 2, true), // Tag: REM
        new TdfMemberInfo("ListType", "mListType", 0xD39C2500, TdfType.UInt16, 3, true), // Tag: TYPE
    ];
    private ITdfMember[] __members;

    private TdfInt64 _ownerId = new(__typeInfos[0]);
    private TdfList<Blaze15SDK.Blaze.Association.ListMemberInfo> _listMemberInfoVector = new(__typeInfos[1]);
    private TdfList<Blaze15SDK.Blaze.Association.ListMemberId> _removedListMemberIdVector = new(__typeInfos[2]);
    private TdfUInt16 _listType = new(__typeInfos[3]);

    public UpdateListMembersResponse()
    {
        __members = [ _ownerId, _listMemberInfoVector, _removedListMemberIdVector, _listType ];
    }

    public override Tdf CreateNew() => new UpdateListMembersResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "UpdateListMembersResponse";
    public override string GetFullClassName() => "Blaze::Association::UpdateListMembersResponse";

    public long OwnerId
    {
        get => _ownerId.Value;
        set => _ownerId.Value = value;
    }

    public IList<Blaze15SDK.Blaze.Association.ListMemberInfo> ListMemberInfoVector
    {
        get => _listMemberInfoVector.Value;
        set => _listMemberInfoVector.Value = value;
    }

    public IList<Blaze15SDK.Blaze.Association.ListMemberId> RemovedListMemberIdVector
    {
        get => _removedListMemberIdVector.Value;
        set => _removedListMemberIdVector.Value = value;
    }

    public ushort ListType
    {
        get => _listType.Value;
        set => _listType.Value = value;
    }

}
