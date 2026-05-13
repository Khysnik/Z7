using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Clubs;

public class ClubMember : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("MembershipStatus", "mMembershipStatus", 0x8EDD3000, TdfType.Enum, 0, true), // Tag: CMTP
        new TdfMemberInfo("OnlineStatus", "mOnlineStatus", 0xB62BF300, TdfType.Enum, 1, true), // Tag: MBOS
        new TdfMemberInfo("MetaData", "mMetaData", 0xB65D2100, TdfType.Map, 2, true), // Tag: META
        new TdfMemberInfo("MembershipSinceTime", "mMembershipSinceTime", 0xB73D2D00, TdfType.UInt32, 3, true), // Tag: MSTM
        new TdfMemberInfo("User", "mUser", 0xD7397200, TdfType.Struct, 4, true), // Tag: USER
    ];
    private ITdfMember[] __members;

    private TdfEnum<MembershipStatus> _membershipStatus = new(__typeInfos[0]);
    private TdfEnum<MemberOnlineStatus> _onlineStatus = new(__typeInfos[1]);
    private TdfMap<string, string> _metaData = new(__typeInfos[2]);
    private TdfUInt32 _membershipSinceTime = new(__typeInfos[3]);
    private TdfStruct<Blaze15SDK.Blaze.CoreIdentification?> _user = new(__typeInfos[4]);

    public ClubMember()
    {
        __members = [ _membershipStatus, _onlineStatus, _metaData, _membershipSinceTime, _user ];
    }

    public override Tdf CreateNew() => new ClubMember();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ClubMember";
    public override string GetFullClassName() => "Blaze::Clubs::ClubMember";

    public MembershipStatus MembershipStatus
    {
        get => _membershipStatus.Value;
        set => _membershipStatus.Value = value;
    }

    public MemberOnlineStatus OnlineStatus
    {
        get => _onlineStatus.Value;
        set => _onlineStatus.Value = value;
    }

    public IDictionary<string, string> MetaData
    {
        get => _metaData.Value;
        set => _metaData.Value = value;
    }

    public uint MembershipSinceTime
    {
        get => _membershipSinceTime.Value;
        set => _membershipSinceTime.Value = value;
    }

    public Blaze15SDK.Blaze.CoreIdentification? User
    {
        get => _user.Value;
        set => _user.Value = value;
    }

}
