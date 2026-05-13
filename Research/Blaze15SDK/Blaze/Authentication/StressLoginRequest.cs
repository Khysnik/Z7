using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Authentication;

public class StressLoginRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ClientType", "mClientType", 0x8F4E7000, TdfType.Enum, 0, true), // Tag: CTYP
        new TdfMemberInfo("Email", "mEmail", 0xB61A6C00, TdfType.String, 1, true), // Tag: MAIL
        new TdfMemberInfo("AccountId", "mAccountId", 0xBB5A6400, TdfType.Int64, 2, true), // Tag: NUID
        new TdfMemberInfo("PersonaName", "mPersonaName", 0xC2E86D00, TdfType.String, 3, true), // Tag: PNAM
        new TdfMemberInfo("ProductName", "mProductName", 0xC3293400, TdfType.String, 4, true), // Tag: PRDT
    ];
    private ITdfMember[] __members;

    private TdfEnum<Blaze15SDK.Blaze.ClientType> _clientType = new(__typeInfos[0]);
    private TdfString _email = new(__typeInfos[1]);
    private TdfInt64 _accountId = new(__typeInfos[2]);
    private TdfString _personaName = new(__typeInfos[3]);
    private TdfString _productName = new(__typeInfos[4]);

    public StressLoginRequest()
    {
        __members = [ _clientType, _email, _accountId, _personaName, _productName ];
    }

    public override Tdf CreateNew() => new StressLoginRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "StressLoginRequest";
    public override string GetFullClassName() => "Blaze::Authentication::StressLoginRequest";

    public Blaze15SDK.Blaze.ClientType ClientType { get => _clientType.Value; set => _clientType.Value = value; }
    public string Email { get => _email.Value; set => _email.Value = value; }
    public long AccountId { get => _accountId.Value; set => _accountId.Value = value; }
    public string PersonaName { get => _personaName.Value; set => _personaName.Value = value; }
    public string ProductName { get => _productName.Value; set => _productName.Value = value; }
}
