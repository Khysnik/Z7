using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class UserSessionLoginInfo : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("IsFirstConsoleLogin", "mIsFirstConsoleLogin", 0x463BEE00, TdfType.Bool, 0, true), // Tag: 1CON
        new TdfMemberInfo("PlatformInfo", "mPlatformInfo", 0x86993300, TdfType.Struct, 1, true), // Tag: AIDS
        new TdfMemberInfo("AccountLocale", "mAccountLocale", 0x86CBE300, TdfType.UInt32, 2, true), // Tag: ALOC
        new TdfMemberInfo("BlazeId", "mBlazeId", 0x8B5A6400, TdfType.Int64, 3, true), // Tag: BUID
        new TdfMemberInfo("ConnectionGroupObjectId", "mConnectionGroupObjectId", 0x8E7A6400, TdfType.ObjectId, 4, true), // Tag: CGID
        new TdfMemberInfo("AccountCountry", "mAccountCountry", 0x8EED3900, TdfType.UInt32, 5, true), // Tag: CNTY
        new TdfMemberInfo("DisplayName", "mDisplayName", 0x933BAD00, TdfType.String, 6, true), // Tag: DSNM
        new TdfMemberInfo("IsFirstLogin", "mIsFirstLogin", 0x9B2CF400, TdfType.Bool, 7, true), // Tag: FRST
        new TdfMemberInfo("SessionKey", "mSessionKey", 0xAE5E4000, TdfType.String, 8, true), // Tag: KEY
        new TdfMemberInfo("LastAuthenticated", "mLastAuthenticated", 0xB21CF400, TdfType.UInt32, 9, true), // Tag: LAST
        new TdfMemberInfo("LastLoginDateTime", "mLastLoginDateTime", 0xB2CBE700, TdfType.Int64, 10, true), // Tag: LLOG
        new TdfMemberInfo("Email", "mEmail", 0xB61A6C00, TdfType.String, 11, true), // Tag: MAIL
        new TdfMemberInfo("PersonaNamespace", "mPersonaNamespace", 0xBA1CF000, TdfType.String, 12, true), // Tag: NASP
        new TdfMemberInfo("PersonaId", "mPersonaId", 0xC2990000, TdfType.Int64, 13, true), // Tag: PID
        new TdfMemberInfo("ClientPlatform", "mClientPlatform", 0xC2C87400, TdfType.Enum, 14, true), // Tag: PLAT
        new TdfMemberInfo("AccountId", "mAccountId", 0xD6990000, TdfType.Int64, 15, true), // Tag: UID
        new TdfMemberInfo("UserSessionType", "mUserSessionType", 0xD73D3000, TdfType.Enum, 16, true), // Tag: USTP
        new TdfMemberInfo("ExtId", "mExtId", 0xE3296600, TdfType.UInt64, 17, true), // Tag: XREF
    ];
    private ITdfMember[] __members;

    private TdfBool _isFirstConsoleLogin = new(__typeInfos[0]);
    private TdfStruct<Blaze15SDK.Blaze.PlatformInfo?> _platformInfo = new(__typeInfos[1]);
    private TdfUInt32 _accountLocale = new(__typeInfos[2]);
    private TdfInt64 _blazeId = new(__typeInfos[3]);
    private TdfObjectId _connectionGroupObjectId = new(__typeInfos[4]);
    private TdfUInt32 _accountCountry = new(__typeInfos[5]);
    private TdfString _displayName = new(__typeInfos[6]);
    private TdfBool _isFirstLogin = new(__typeInfos[7]);
    private TdfString _sessionKey = new(__typeInfos[8]);
    private TdfUInt32 _lastAuthenticated = new(__typeInfos[9]);
    private TdfInt64 _lastLoginDateTime = new(__typeInfos[10]);
    private TdfString _email = new(__typeInfos[11]);
    private TdfString _personaNamespace = new(__typeInfos[12]);
    private TdfInt64 _personaId = new(__typeInfos[13]);
    private TdfEnum<Blaze15SDK.Blaze.ClientPlatformType> _clientPlatform = new(__typeInfos[14]);
    private TdfInt64 _accountId = new(__typeInfos[15]);
    private TdfEnum<Blaze15SDK.Blaze.UserSessionType> _userSessionType = new(__typeInfos[16]);
    private TdfUInt64 _extId = new(__typeInfos[17]);

    public UserSessionLoginInfo()
    {
        __members = [
            _isFirstConsoleLogin,
            _platformInfo,
            _accountLocale,
            _blazeId,
            _connectionGroupObjectId,
            _accountCountry,
            _displayName,
            _isFirstLogin,
            _sessionKey,
            _lastAuthenticated,
            _lastLoginDateTime,
            _email,
            _personaNamespace,
            _personaId,
            _clientPlatform,
            _accountId,
            _userSessionType,
            _extId,
        ];
    }

    public override Tdf CreateNew() => new UserSessionLoginInfo();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "UserSessionLoginInfo";
    public override string GetFullClassName() => "Blaze::UserSessionLoginInfo";

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

    public uint AccountLocale
    {
        get => _accountLocale.Value;
        set => _accountLocale.Value = value;
    }

    public long BlazeId
    {
        get => _blazeId.Value;
        set => _blazeId.Value = value;
    }

    public ObjectId ConnectionGroupObjectId
    {
        get => _connectionGroupObjectId.Value;
        set => _connectionGroupObjectId.Value = value;
    }

    public uint AccountCountry
    {
        get => _accountCountry.Value;
        set => _accountCountry.Value = value;
    }

    public string DisplayName
    {
        get => _displayName.Value;
        set => _displayName.Value = value;
    }

    public bool IsFirstLogin
    {
        get => _isFirstLogin.Value;
        set => _isFirstLogin.Value = value;
    }

    public string SessionKey
    {
        get => _sessionKey.Value;
        set => _sessionKey.Value = value;
    }

    public uint LastAuthenticated
    {
        get => _lastAuthenticated.Value;
        set => _lastAuthenticated.Value = value;
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

    public string PersonaNamespace
    {
        get => _personaNamespace.Value;
        set => _personaNamespace.Value = value;
    }

    public long PersonaId
    {
        get => _personaId.Value;
        set => _personaId.Value = value;
    }

    public Blaze15SDK.Blaze.ClientPlatformType ClientPlatform
    {
        get => _clientPlatform.Value;
        set => _clientPlatform.Value = value;
    }

    public long AccountId
    {
        get => _accountId.Value;
        set => _accountId.Value = value;
    }

    public Blaze15SDK.Blaze.UserSessionType UserSessionType
    {
        get => _userSessionType.Value;
        set => _userSessionType.Value = value;
    }

    public ulong ExtId
    {
        get => _extId.Value;
        set => _extId.Value = value;
    }

}
