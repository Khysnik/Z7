using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Authentication;

public class LoginRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("PlatformInfo", "mPlatformInfo", 0x86993300, TdfType.Struct, 0, true), // Tag: AIDS
        new TdfMemberInfo("AuthCode", "mAuthCode", 0x875D2800, TdfType.String, 1, true), // Tag: AUTH
        new TdfMemberInfo("CrossPlatformOpt", "mCrossPlatformOpt", 0x8EFC3400, TdfType.Enum, 2, true), // Tag: COPT
        new TdfMemberInfo("ClientType", "mClientType", 0x8F4E7000, TdfType.Enum, 3, true), // Tag: CTYP
        new TdfMemberInfo("ExternalId", "mExternalId", 0x978D2900, TdfType.Int64, 4, true), // Tag: EXTI
        new TdfMemberInfo("ProductName", "mProductName", 0xC3293400, TdfType.String, 5, true), // Tag: PRDT
    ];
    private ITdfMember[] __members;

    private TdfStruct<Blaze15SDK.Blaze.PlatformInfo?> _platformInfo = new(__typeInfos[0]);
    private TdfString _authCode = new(__typeInfos[1]);
    private TdfEnum<Blaze15SDK.Blaze.Authentication.CrossPlatformOptSetting> _crossPlatformOpt = new(__typeInfos[2]);
    private TdfEnum<Blaze15SDK.Blaze.ClientType> _clientType = new(__typeInfos[3]);
    private TdfInt64 _externalId = new(__typeInfos[4]);
    private TdfString _productName = new(__typeInfos[5]);

    public LoginRequest()
    {
        __members = [
            _platformInfo,
            _authCode,
            _crossPlatformOpt,
            _clientType,
            _externalId,
            _productName,
        ];
    }

    public override Tdf CreateNew() => new LoginRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "LoginRequest";
    public override string GetFullClassName() => "Blaze::Authentication::LoginRequest";

    public Blaze15SDK.Blaze.PlatformInfo? PlatformInfo
    {
        get => _platformInfo.Value;
        set => _platformInfo.Value = value;
    }

    public string AuthCode
    {
        get => _authCode.Value;
        set => _authCode.Value = value;
    }

    public Blaze15SDK.Blaze.Authentication.CrossPlatformOptSetting CrossPlatformOpt
    {
        get => _crossPlatformOpt.Value;
        set => _crossPlatformOpt.Value = value;
    }

    public Blaze15SDK.Blaze.ClientType ClientType
    {
        get => _clientType.Value;
        set => _clientType.Value = value;
    }

    public long ExternalId
    {
        get => _externalId.Value;
        set => _externalId.Value = value;
    }

    public string ProductName
    {
        get => _productName.Value;
        set => _productName.Value = value;
    }

}
