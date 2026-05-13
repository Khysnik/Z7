using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class ClientInfo : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("BlazeSDKVersion", "mBlazeSDKVersion", 0x8B392B00, TdfType.String, 0, true), // Tag: BSDK
        new TdfMemberInfo("BlazeSDKBuildDate", "mBlazeSDKBuildDate", 0x8B4A6D00, TdfType.String, 1, true), // Tag: BTIM
        new TdfMemberInfo("ClientName", "mClientName", 0x8ECBB400, TdfType.String, 2, true), // Tag: CLNT
        new TdfMemberInfo("Platform", "mPlatform", 0x8F09B400, TdfType.Enum, 3, true), // Tag: CPFT
        new TdfMemberInfo("ClientSkuId", "mClientSkuId", 0x8F3AF500, TdfType.String, 4, true), // Tag: CSKU
        new TdfMemberInfo("ClientCountry", "mClientCountry", 0x8F4CB900, TdfType.UInt32, 5, true), // Tag: CTRY
        new TdfMemberInfo("ClientVersion", "mClientVersion", 0x8F697200, TdfType.String, 6, true), // Tag: CVER
        new TdfMemberInfo("DirtySDKVersion", "mDirtySDKVersion", 0x93392B00, TdfType.String, 7, true), // Tag: DSDK
        new TdfMemberInfo("Environment", "mEnvironment", 0x96ED8000, TdfType.String, 8, true), // Tag: ENV
        new TdfMemberInfo("ClientLocale", "mClientLocale", 0xB2F8C000, TdfType.UInt32, 9, true), // Tag: LOC
        new TdfMemberInfo("ProtoTunnelVersion", "mProtoTunnelVersion", 0xC34DB200, TdfType.String, 10, true), // Tag: PTVR
        new TdfMemberInfo("PsnPushContextId", "mPsnPushContextId", 0xC35CE800, TdfType.String, 11, true), // Tag: PUSH
        new TdfMemberInfo("TitleId", "mTitleId", 0xD34A6400, TdfType.String, 12, true), // Tag: TTID
    ];
    private ITdfMember[] __members;

    private TdfString _blazeSDKVersion = new(__typeInfos[0]);
    private TdfString _blazeSDKBuildDate = new(__typeInfos[1]);
    private TdfString _clientName = new(__typeInfos[2]);
    private TdfEnum<Blaze15SDK.Blaze.ClientPlatformType> _platform = new(__typeInfos[3]);
    private TdfString _clientSkuId = new(__typeInfos[4]);
    private TdfString _clientVersion = new(__typeInfos[6]);
    private TdfString _dirtySDKVersion = new(__typeInfos[7]);
    private TdfString _environment = new(__typeInfos[8]);
    private TdfUInt32 _clientLocale = new(__typeInfos[9]);
    private TdfUInt32 _clientCountry = new(__typeInfos[5]);
    private TdfString _protoTunnelVersion = new(__typeInfos[10]);
    private TdfString _psnPushContextId = new(__typeInfos[11]);
    private TdfString _titleId = new(__typeInfos[12]);

    public ClientInfo()
    {
        __members = [
            _blazeSDKVersion,
            _blazeSDKBuildDate,
            _clientName,
            _platform,
            _clientSkuId,
            _clientCountry,
            _clientVersion,
            _dirtySDKVersion,
            _environment,
            _clientLocale,
            _protoTunnelVersion,
            _psnPushContextId,
            _titleId,
        ];
    }

    public override Tdf CreateNew() => new ClientInfo();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ClientInfo";
    public override string GetFullClassName() => "Blaze::ClientInfo";

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

    public Blaze15SDK.Blaze.ClientPlatformType Platform
    {
        get => _platform.Value;
        set => _platform.Value = value;
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

    public uint ClientLocale
    {
        get => _clientLocale.Value;
        set => _clientLocale.Value = value;
    }

    public uint ClientCountry
    {
        get => _clientCountry.Value;
        set => _clientCountry.Value = value;
    }

    public string ProtoTunnelVersion
    {
        get => _protoTunnelVersion.Value;
        set => _protoTunnelVersion.Value = value;
    }

    public string PsnPushContextId
    {
        get => _psnPushContextId.Value;
        set => _psnPushContextId.Value = value;
    }

    public string TitleId
    {
        get => _titleId.Value;
        set => _titleId.Value = value;
    }

}
