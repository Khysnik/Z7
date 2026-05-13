using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Clubs;

public class GetInvitationsResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ClubInvList", "mClubInvList", 0x8E9CF400, TdfType.List, 0, true), // Tag: CIST
    ];
    private ITdfMember[] __members;

    private TdfList<ClubMessage> _clubInvList = new(__typeInfos[0]);

    public GetInvitationsResponse()
    {
        __members = [ _clubInvList ];
    }

    public override Tdf CreateNew() => new GetInvitationsResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetInvitationsResponse";
    public override string GetFullClassName() => "Blaze::Clubs::GetInvitationsResponse";

    public IList<ClubMessage> ClubInvList
    {
        get => _clubInvList.Value;
        set => _clubInvList.Value = value;
    }

}
