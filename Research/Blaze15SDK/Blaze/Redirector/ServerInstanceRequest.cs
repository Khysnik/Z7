using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Redirector;

public class ServerInstanceRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("BlazeSDKVersion", "mBlazeSDKVersion", 0x8B392B00, TdfType.String, 0, true), // Tag: BSDK
        new TdfMemberInfo("BlazeSDKBuildDate", "mBlazeSDKBuildDate", 0x8B4A6D00, TdfType.String, 1, true), // Tag: BTIM
        new TdfMemberInfo("ClientName", "mClientName", 0x8ECBB400, TdfType.String, 2, true), // Tag: CLNT
        new TdfMemberInfo("ClientType", "mClientType", 0x8ECD3000, TdfType.Enum, 3, true), // Tag: CLTP
        new TdfMemberInfo("ClientPlatform", "mClientPlatform", 0x8F0B3400, TdfType.Enum, 4, true), // Tag: CPLT
        new TdfMemberInfo("ClientSkuId", "mClientSkuId", 0x8F3AF500, TdfType.String, 5, true), // Tag: CSKU
        new TdfMemberInfo("ClientVersion", "mClientVersion", 0x8F697200, TdfType.String, 6, true), // Tag: CVER
        new TdfMemberInfo("DirtySDKVersion", "mDirtySDKVersion", 0x93392B00, TdfType.String, 7, true), // Tag: DSDK
        new TdfMemberInfo("Environment", "mEnvironment", 0x96ED8000, TdfType.String, 8, true), // Tag: ENV
        new TdfMemberInfo("FirstPartyId", "mFirstPartyId", 0x9B0A6400, TdfType.Struct, 9, true), // Tag: FPID
        new TdfMemberInfo("ClientLocale", "mClientLocale", 0xB2F8C000, TdfType.UInt32, 10, true), // Tag: LOC
        new TdfMemberInfo("Name", "mName", 0xBA1B6500, TdfType.String, 11, true), // Tag: NAME
        new TdfMemberInfo("Platform", "mPlatform", 0xC2C87400, TdfType.String, 12, true), // Tag: PLAT
        new TdfMemberInfo("ConnectionProfile", "mConnectionProfile", 0xC32BE600, TdfType.String, 13, true), // Tag: PROF
        new TdfMemberInfo("IsTrial", "isTrial", 0xD32A6100, TdfType.Bool, 14, true), // Tag: TRIA
        new TdfMemberInfo("TimeZoneOffset", "mTimeZoneOffset", 0xD3ABE600, TdfType.Int32, 15, true), // Tag: TZOF
    ];
    private ITdfMember[] __members;

    private TdfString _blazeSDKVersion = new(__typeInfos[0]);
    private TdfString _blazeSDKBuildDate = new(__typeInfos[1]);
    private TdfString _clientName = new(__typeInfos[2]);
    private TdfEnum<Blaze15SDK.Blaze.ClientType> _clientType = new(__typeInfos[3]);
    private TdfEnum<Blaze15SDK.Blaze.ClientPlatformType> _clientPlatform = new(__typeInfos[4]);
    private TdfString _clientSkuId = new(__typeInfos[5]);
    private TdfString _clientVersion = new(__typeInfos[6]);
    private TdfString _dirtySDKVersion = new(__typeInfos[7]);
    private TdfString _environment = new(__typeInfos[8]);
    private TdfStruct<Blaze15SDK.Blaze.Redirector.FirstPartyId?> _firstPartyId = new(__typeInfos[9]);
    private TdfUInt32 _clientLocale = new(__typeInfos[10]);
    private TdfString _name = new(__typeInfos[11]);
    private TdfString _platform = new(__typeInfos[12]);
    private TdfString _connectionProfile = new(__typeInfos[13]);
    private TdfBool _isTrial = new(__typeInfos[14]);
    private TdfInt32 _timeZoneOffset = new(__typeInfos[15]);

    public ServerInstanceRequest()
    {
        __members = [ _blazeSDKVersion, _blazeSDKBuildDate, _clientName, _clientType, _clientPlatform, _clientSkuId, _clientVersion, _dirtySDKVersion, _environment, _firstPartyId, _clientLocale, _name, _platform, _connectionProfile, _isTrial, _timeZoneOffset ];
    }

    public override Tdf CreateNew() => new ServerInstanceRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ServerInstanceRequest";
    public override string GetFullClassName() => "Blaze::Redirector::ServerInstanceRequest";

    public string BlazeSDKVersion
    {
        get => _blazeSDKVersion.Value;
        set => _blazeSDKVersion.Value = value;
    }

    public string BlazeSDKBuildDate
    {
        get => _blazeSDKBuildDate.Value;
        set => _blazeSDKBuildDate.Value = value;
    }

    public string ClientName
    {
        get => _clientName.Value;
        set => _clientName.Value = value;
    }

    public Blaze15SDK.Blaze.ClientType ClientType
    {
        get => _clientType.Value;
        set => _clientType.Value = value;
    }

    public Blaze15SDK.Blaze.ClientPlatformType ClientPlatform
    {
        get => _clientPlatform.Value;
        set => _clientPlatform.Value = value;
    }

    public string ClientSkuId
    {
        get => _clientSkuId.Value;
        set => _clientSkuId.Value = value;
    }

    public string ClientVersion
    {
        get => _clientVersion.Value;
        set => _clientVersion.Value = value;
    }

    public string DirtySDKVersion
    {
        get => _dirtySDKVersion.Value;
        set => _dirtySDKVersion.Value = value;
    }

    public string Environment
    {
        get => _environment.Value;
        set => _environment.Value = value;
    }

    public Blaze15SDK.Blaze.Redirector.FirstPartyId? FirstPartyId
    {
        get => _firstPartyId.Value;
        set => _firstPartyId.Value = value;
    }

    public uint ClientLocale
    {
        get => _clientLocale.Value;
        set => _clientLocale.Value = value;
    }

    public string Name
    {
        get => _name.Value;
        set => _name.Value = value;
    }

    public string Platform
    {
        get => _platform.Value;
        set => _platform.Value = value;
    }

    public string ConnectionProfile
    {
        get => _connectionProfile.Value;
        set => _connectionProfile.Value = value;
    }

    public bool IsTrial
    {
        get => _isTrial.Value;
        set => _isTrial.Value = value;
    }

    public int TimeZoneOffset
    {
        get => _timeZoneOffset.Value;
        set => _timeZoneOffset.Value = value;
    }

}
