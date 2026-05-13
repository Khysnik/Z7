using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Clubs;

public class GetMemberStatusInClubResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Status", "mStatus", 0xB73CF300, TdfType.Map, 0, true), // Tag: MSSS
    ];
    private ITdfMember[] __members;

    private TdfMap<long, uint> _status = new(__typeInfos[0]);

    public GetMemberStatusInClubResponse()
    {
        __members = [ _status ];
    }

    public override Tdf CreateNew() => new GetMemberStatusInClubResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetMemberStatusInClubResponse";
    public override string GetFullClassName() => "Blaze::Clubs::GetMemberStatusInClubResponse";

    public IDictionary<long, uint> Status
    {
        get => _status.Value;
        set => _status.Value = value;
    }

}
