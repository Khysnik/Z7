using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Authentication;

public class UserLoginInfo : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("IsFirstConsoleLogin", "mIsFirstConsoleLogin", 0x463BEE00, TdfType.Bool, 0, true), // Tag: 1CON
        new TdfMemberInfo("PlatformInfo", "mPlatformInfo", 0x86993300, TdfType.Struct, 1, true), // Tag: AIDS
        new TdfMemberInfo("BlazeId", "mBlazeId", 0x8B5A6400, TdfType.Int64, 2, true), // Tag: BUID
        new TdfMemberInfo("IsFirstLogin", "mIsFirstLogin", 0x9B2CF400, TdfType.Bool, 3, true), // Tag: FRST
        new TdfMemberInfo("GeoIpSucceeded", "mGeoIpSucceeded", 0x9E5BC000, TdfType.Bool, 4, true), // Tag: GEO
        new TdfMemberInfo("SessionKey", "mSessionKey", 0xAE5E4000, TdfType.String, 5, true), // Tag: KEY
        new TdfMemberInfo("LastLoginDateTime", "mLastLoginDateTime", 0xB2CBE700, TdfType.Int64, 6, true), // Tag: LLOG
        new TdfMemberInfo("Email", "mEmail", 0xB61A6C00, TdfType.String, 7, true), // Tag: MAIL
        new TdfMemberInfo("PersonaDetails", "mPersonaDetails", 0xC24D2C00, TdfType.Struct, 8, true), // Tag: PDTL
        new TdfMemberInfo("AccountId", "mAccountId", 0xD6990000, TdfType.Int64, 9, true), // Tag: UID
    ];
    private ITdfMember[] __members;

    private TdfBool _isFirstConsoleLogin = new(__typeInfos[0]);
    private TdfStruct<Blaze15SDK.Blaze.PlatformInfo?> _platformInfo = new(__typeInfos[1]);
    private TdfInt64 _blazeId = new(__typeInfos[2]);
    private TdfBool _isFirstLogin = new(__typeInfos[3]);
    private TdfBool _geoIpSucceeded = new(__typeInfos[4]);
    private TdfString _sessionKey = new(__typeInfos[5]);
    private TdfInt64 _lastLoginDateTime = new(__typeInfos[6]);
    private TdfString _email = new(__typeInfos[7]);
    private TdfStruct<Blaze15SDK.Blaze.Authentication.PersonaDetails?> _personaDetails = new(__typeInfos[8]);
    private TdfInt64 _accountId = new(__typeInfos[9]);

    public UserLoginInfo()
    {
        __members = [
            _isFirstConsoleLogin,
            _platformInfo,
            _blazeId,
            _isFirstLogin,
            _geoIpSucceeded,
            _sessionKey,
            _lastLoginDateTime,
            _email,
            _personaDetails,
            _accountId,
        ];
    }

    public override Tdf CreateNew() => new UserLoginInfo();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "UserLoginInfo";
    public override string GetFullClassName() => "Blaze::Authentication::UserLoginInfo";

    public bool IsFirstConsoleLogin
    {
        get => _isFirstConsoleLogin.Value;
        set => _isFirstConsoleLogin.Value = value;
    }

    public Blaze15SDK.Blaze.PlatformInfo? PlatformInfo
    {
        get => _platformInfo.Value;
        set => _platformInfo.Value = value;
    }

    public long BlazeId
    {
        get => _blazeId.Value;
        set => _blazeId.Value = value;
    }

    public bool IsFirstLogin
    {
        get => _isFirstLogin.Value;
        set => _isFirstLogin.Value = value;
    }

    public bool GeoIpSucceeded
    {
        get => _geoIpSucceeded.Value;
        set => _geoIpSucceeded.Value = value;
    }

    public string SessionKey
    {
        get => _sessionKey.Value;
        set => _sessionKey.Value = value;
    }

    public long LastLoginDateTime
    {
        get => _lastLoginDateTime.Value;
        set => _lastLoginDateTime.Value = value;
    }

    public string Email
    {
        get => _email.Value;
        set => _email.Value = value;
    }

    public Blaze15SDK.Blaze.Authentication.PersonaDetails? PersonaDetails
    {
        get => _personaDetails.Value;
        set => _personaDetails.Value = value;
    }

    public long AccountId
    {
        get => _accountId.Value;
        set => _accountId.Value = value;
    }

}
