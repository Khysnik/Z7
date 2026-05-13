using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Clubs;

public class GetMemberStatusInClubRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ClubId", "mClubId", 0x8E2A6400, TdfType.UInt64, 0, true), // Tag: CBID
        new TdfMemberInfo("BlazeIds", "mBlazeIds", 0xD72A6400, TdfType.List, 1, true), // Tag: URID
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _clubId = new(__typeInfos[0]);
    private TdfList<long> _blazeIds = new(__typeInfos[1]);

    public GetMemberStatusInClubRequest()
    {
        __members = [ _clubId, _blazeIds ];
    }

    public override Tdf CreateNew() => new GetMemberStatusInClubRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetMemberStatusInClubRequest";
    public override string GetFullClassName() => "Blaze::Clubs::GetMemberStatusInClubRequest";

    public ulong ClubId
    {
        get => _clubId.Value;
        set => _clubId.Value = value;
    }

    public IList<long> BlazeIds
    {
        get => _blazeIds.Value;
        set => _blazeIds.Value = value;
    }

}
