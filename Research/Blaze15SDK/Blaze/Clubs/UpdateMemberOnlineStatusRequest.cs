using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Clubs;

public class UpdateMemberOnlineStatusRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ClubId", "mClubId", 0x8ECA6400, TdfType.UInt64, 0, true), // Tag: CLID
        new TdfMemberInfo("NewStatus", "mNewStatus", 0xCF487400, TdfType.Enum, 1, true), // Tag: STAT
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _clubId = new(__typeInfos[0]);
    private TdfEnum<MemberOnlineStatus> _newStatus = new(__typeInfos[1]);

    public UpdateMemberOnlineStatusRequest()
    {
        __members = [ _clubId, _newStatus ];
    }

    public override Tdf CreateNew() => new UpdateMemberOnlineStatusRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "UpdateMemberOnlineStatusRequest";
    public override string GetFullClassName() => "Blaze::Clubs::UpdateMemberOnlineStatusRequest";

    public ulong ClubId
    {
        get => _clubId.Value;
        set => _clubId.Value = value;
    }

    public MemberOnlineStatus NewStatus
    {
        get => _newStatus.Value;
        set => _newStatus.Value = value;
    }

}
