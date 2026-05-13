using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Clubs;

public class GetMemberOnlineStatusResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Status", "mStatus", 0xB6FCED00, TdfType.Map, 0, true), // Tag: MOSM
    ];
    private ITdfMember[] __members;

    private TdfMap<long, uint> _status = new(__typeInfos[0]);

    public GetMemberOnlineStatusResponse()
    {
        __members = [ _status ];
    }

    public override Tdf CreateNew() => new GetMemberOnlineStatusResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetMemberOnlineStatusResponse";
    public override string GetFullClassName() => "Blaze::Clubs::GetMemberOnlineStatusResponse";

    public IDictionary<long, uint> Status
    {
        get => _status.Value;
        set => _status.Value = value;
    }

}
