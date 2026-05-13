using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Authentication;

public class ExpressLoginRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("CrossPlatformOpt", "mCrossPlatformOpt", 0x8EFC3400, TdfType.Enum, 0, true), // Tag: COPT
        new TdfMemberInfo("ClientType", "mClientType", 0x8F4E7000, TdfType.Enum, 1, true), // Tag: CTYP
        new TdfMemberInfo("Email", "mEmail", 0xB61A6C00, TdfType.String, 2, true), // Tag: MAIL
        new TdfMemberInfo("Password", "mPassword", 0xC21CF300, TdfType.String, 3, true), // Tag: PASS
        new TdfMemberInfo("PersonaName", "mPersonaName", 0xC2E86D00, TdfType.String, 4, true), // Tag: PNAM
        new TdfMemberInfo("ProductName", "mProductName", 0xC3293400, TdfType.String, 5, true), // Tag: PRDT
    ];
    private ITdfMember[] __members;

    private TdfEnum<Blaze15SDK.Blaze.Authentication.CrossPlatformOptSetting> _crossPlatformOpt = new(__typeInfos[0]);
    private TdfEnum<Blaze15SDK.Blaze.ClientType> _clientType = new(__typeInfos[1]);
    private TdfString _email = new(__typeInfos[2]);
    private TdfString _password = new(__typeInfos[3]);
    private TdfString _personaName = new(__typeInfos[4]);
    private TdfString _productName = new(__typeInfos[5]);

    public ExpressLoginRequest()
    {
        __members = [ _crossPlatformOpt, _clientType, _email, _password, _personaName, _productName ];
    }

    public override Tdf CreateNew() => new ExpressLoginRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ExpressLoginRequest";
    public override string GetFullClassName() => "Blaze::Authentication::ExpressLoginRequest";

    public Blaze15SDK.Blaze.Authentication.CrossPlatformOptSetting CrossPlatformOpt { get => _crossPlatformOpt.Value; set => _crossPlatformOpt.Value = value; }
    public Blaze15SDK.Blaze.ClientType ClientType { get => _clientType.Value; set => _clientType.Value = value; }
    public string Email { get => _email.Value; set => _email.Value = value; }
    public string Password { get => _password.Value; set => _password.Value = value; }
    public string PersonaName { get => _personaName.Value; set => _personaName.Value = value; }
    public string ProductName { get => _productName.Value; set => _productName.Value = value; }
}
