using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Clubs;

public class GetClubBansResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("BlazeIdToBanStatusMap", "mBlazeIdToBanStatusMap", 0x8A1BB300, TdfType.Map, 0, true), // Tag: BANS
    ];
    private ITdfMember[] __members;

    private TdfMap<long, uint> _blazeIdToBanStatusMap = new(__typeInfos[0]);

    public GetClubBansResponse()
    {
        __members = [ _blazeIdToBanStatusMap ];
    }

    public override Tdf CreateNew() => new GetClubBansResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetClubBansResponse";
    public override string GetFullClassName() => "Blaze::Clubs::GetClubBansResponse";

    public IDictionary<long, uint> BlazeIdToBanStatusMap
    {
        get => _blazeIdToBanStatusMap.Value;
        set => _blazeIdToBanStatusMap.Value = value;
    }

}
