using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Clubs;

public class JoinOrPetitionClubResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ClubJoinOrPetitionStatus", "mClubJoinOrPetitionStatus", 0x8EAC3300, TdfType.Enum, 0, true), // Tag: CJPS
    ];
    private ITdfMember[] __members;

    private TdfEnum<ClubJoinOrPetitionStatus> _clubJoinOrPetitionStatus = new(__typeInfos[0]);

    public JoinOrPetitionClubResponse()
    {
        __members = [ _clubJoinOrPetitionStatus ];
    }

    public override Tdf CreateNew() => new JoinOrPetitionClubResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "JoinOrPetitionClubResponse";
    public override string GetFullClassName() => "Blaze::Clubs::JoinOrPetitionClubResponse";

    public ClubJoinOrPetitionStatus ClubJoinOrPetitionStatus
    {
        get => _clubJoinOrPetitionStatus.Value;
        set => _clubJoinOrPetitionStatus.Value = value;
    }

}
