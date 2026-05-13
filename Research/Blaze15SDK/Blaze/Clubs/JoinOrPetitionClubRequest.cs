using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Clubs;

public class JoinOrPetitionClubRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ClubId", "mClubId", 0x8ECA6400, TdfType.UInt64, 0, true), // Tag: CLID
        new TdfMemberInfo("PetitionIfJoinFails", "mPetitionIfJoinFails", 0xC29AA600, TdfType.Bool, 1, true), // Tag: PIJF
        new TdfMemberInfo("Password", "mPassword", 0xC33DE400, TdfType.String, 2, true), // Tag: PSWD
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _clubId = new(__typeInfos[0]);
    private TdfBool _petitionIfJoinFails = new(__typeInfos[1]);
    private TdfString _password = new(__typeInfos[2]);

    public JoinOrPetitionClubRequest()
    {
        __members = [ _clubId, _petitionIfJoinFails, _password ];
    }

    public override Tdf CreateNew() => new JoinOrPetitionClubRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "JoinOrPetitionClubRequest";
    public override string GetFullClassName() => "Blaze::Clubs::JoinOrPetitionClubRequest";

    public ulong ClubId
    {
        get => _clubId.Value;
        set => _clubId.Value = value;
    }

    public bool PetitionIfJoinFails
    {
        get => _petitionIfJoinFails.Value;
        set => _petitionIfJoinFails.Value = value;
    }

    public string Password
    {
        get => _password.Value;
        set => _password.Value = value;
    }

}
