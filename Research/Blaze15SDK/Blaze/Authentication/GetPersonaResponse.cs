using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Authentication;

public class GetPersonaResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("PersonaInfo", "mPersonaInfo", 0xC29BA600, TdfType.Struct, 0, true), // Tag: PINF
        new TdfMemberInfo("AccountId", "mAccountId", 0xD6990000, TdfType.Int64, 1, true), // Tag: UID
    ];
    private ITdfMember[] __members;

    private TdfStruct<Blaze15SDK.Blaze.Authentication.PersonaInfo?> _personaInfo = new(__typeInfos[0]);
    private TdfInt64 _accountId = new(__typeInfos[1]);

    public GetPersonaResponse()
    {
        __members = [ _personaInfo, _accountId ];
    }

    public override Tdf CreateNew() => new GetPersonaResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetPersonaResponse";
    public override string GetFullClassName() => "Blaze::Authentication::GetPersonaResponse";

    public Blaze15SDK.Blaze.Authentication.PersonaInfo? PersonaInfo { get => _personaInfo.Value; set => _personaInfo.Value = value; }
    public long AccountId { get => _accountId.Value; set => _accountId.Value = value; }
}
