using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Clubs;

public class BanUnbanMemberRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ClubId", "mClubId", 0x8ECA6400, TdfType.UInt64, 0, true), // Tag: CLID
        new TdfMemberInfo("BlazeId", "mBlazeId", 0xD6990000, TdfType.Int64, 1, true), // Tag: UID
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _clubId = new(__typeInfos[0]);
    private TdfInt64 _blazeId = new(__typeInfos[1]);

    public BanUnbanMemberRequest()
    {
        __members = [ _clubId, _blazeId ];
    }

    public override Tdf CreateNew() => new BanUnbanMemberRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "BanUnbanMemberRequest";
    public override string GetFullClassName() => "Blaze::Clubs::BanUnbanMemberRequest";

    public ulong ClubId
    {
        get => _clubId.Value;
        set => _clubId.Value = value;
    }

    public long BlazeId
    {
        get => _blazeId.Value;
        set => _blazeId.Value = value;
    }

}
