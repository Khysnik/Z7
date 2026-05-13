using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Association;

public class CheckListMembershipRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("MemberBlazeId", "mMemberBlazeId", 0x8A990000, TdfType.Int64, 0, true), // Tag: BID
        new TdfMemberInfo("OwnersBlazeIds", "mOwnersBlazeIds", 0xB22A6400, TdfType.Struct, 1, true), // Tag: LBID
        new TdfMemberInfo("ListIdentification", "mListIdentification", 0xB2990000, TdfType.Struct, 2, true), // Tag: LID
    ];
    private ITdfMember[] __members;

    private TdfInt64 _memberBlazeId = new(__typeInfos[0]);
    private TdfStruct<Blaze15SDK.Blaze.Association.ListBlazeIds?> _ownersBlazeIds = new(__typeInfos[1]);
    private TdfStruct<Blaze15SDK.Blaze.Association.ListIdentification?> _listIdentification = new(__typeInfos[2]);

    public CheckListMembershipRequest()
    {
        __members = [ _memberBlazeId, _ownersBlazeIds, _listIdentification ];
    }

    public override Tdf CreateNew() => new CheckListMembershipRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "CheckListMembershipRequest";
    public override string GetFullClassName() => "Blaze::Association::CheckListMembershipRequest";

    public long MemberBlazeId
    {
        get => _memberBlazeId.Value;
        set => _memberBlazeId.Value = value;
    }

    public Blaze15SDK.Blaze.Association.ListBlazeIds? OwnersBlazeIds
    {
        get => _ownersBlazeIds.Value;
        set => _ownersBlazeIds.Value = value;
    }

    public Blaze15SDK.Blaze.Association.ListIdentification? ListIdentification
    {
        get => _listIdentification.Value;
        set => _listIdentification.Value = value;
    }

}
