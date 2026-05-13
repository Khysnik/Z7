using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Clubs;

public class UpdateOnlineStatusRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("BlazeId", "mBlazeId", 0x8ACA6400, TdfType.Int64, 0, true), // Tag: BLID
        new TdfMemberInfo("ClubId", "mClubId", 0x8ECA6400, TdfType.UInt64, 1, true), // Tag: CLID
        new TdfMemberInfo("Reason", "mReason", 0xCA587300, TdfType.Enum, 2, true), // Tag: REAS
        new TdfMemberInfo("OnlineStatus", "mOnlineStatus", 0xCF487400, TdfType.Enum, 3, true), // Tag: STAT
    ];
    private ITdfMember[] __members;

    private TdfInt64 _blazeId = new(__typeInfos[0]);
    private TdfUInt64 _clubId = new(__typeInfos[1]);
    private TdfEnum<UpdateReason> _reason = new(__typeInfos[2]);
    private TdfEnum<MemberOnlineStatus> _onlineStatus = new(__typeInfos[3]);

    public UpdateOnlineStatusRequest()
    {
        __members = [ _blazeId, _clubId, _reason, _onlineStatus ];
    }

    public override Tdf CreateNew() => new UpdateOnlineStatusRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "UpdateOnlineStatusRequest";
    public override string GetFullClassName() => "Blaze::Clubs::UpdateOnlineStatusRequest";

    public long BlazeId
    {
        get => _blazeId.Value;
        set => _blazeId.Value = value;
    }

    public ulong ClubId
    {
        get => _clubId.Value;
        set => _clubId.Value = value;
    }

    public UpdateReason Reason
    {
        get => _reason.Value;
        set => _reason.Value = value;
    }

    public MemberOnlineStatus OnlineStatus
    {
        get => _onlineStatus.Value;
        set => _onlineStatus.Value = value;
    }

}
