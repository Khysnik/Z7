using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Clubs;

public class RemoveMemberRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("BlazeId", "mBlazeId", 0x8ACA6400, TdfType.Int64, 0, true), // Tag: BLID
        new TdfMemberInfo("ClubId", "mClubId", 0x8ECA6400, TdfType.UInt64, 1, true), // Tag: CLID
    ];
    private ITdfMember[] __members;

    private TdfInt64 _blazeId = new(__typeInfos[0]);
    private TdfUInt64 _clubId = new(__typeInfos[1]);

    public RemoveMemberRequest()
    {
        __members = [ _blazeId, _clubId ];
    }

    public override Tdf CreateNew() => new RemoveMemberRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "RemoveMemberRequest";
    public override string GetFullClassName() => "Blaze::Clubs::RemoveMemberRequest";

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

}
