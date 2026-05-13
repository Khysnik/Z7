using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Logging;

public class LogContext : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("BlazeId", "mBlazeId", 0x8ACA6400, TdfType.Int64, 0, true), // Tag: BLID
        new TdfMemberInfo("ClientIp", "mClientIp", 0x8ECA7000, TdfType.Struct, 1, true), // Tag: CLIP
        new TdfMemberInfo("DeviceId", "mDeviceId", 0x936A6400, TdfType.String, 2, true), // Tag: DVID
        new TdfMemberInfo("PersonaName", "mPersonaName", 0xC2E86D00, TdfType.String, 3, true), // Tag: PNAM
        new TdfMemberInfo("SlaveSessionId", "mSlaveSessionId", 0xCF3A6400, TdfType.UInt64, 4, true), // Tag: SSID
        new TdfMemberInfo("NucleusAccountId", "mNucleusAccountId", 0xD61A6400, TdfType.Int64, 5, true), // Tag: UAID
        new TdfMemberInfo("Platform", "mPlatform", 0xD70B3400, TdfType.Enum, 6, true), // Tag: UPLT
        new TdfMemberInfo("UserSessionId", "mUserSessionId", 0xD73A6400, TdfType.UInt64, 7, true), // Tag: USID
    ];
    private ITdfMember[] __members;

    private TdfInt64 _blazeId = new(__typeInfos[0]);
    private TdfStruct<Blaze15SDK.Blaze.IpAddress?> _clientIp = new(__typeInfos[1]);
    private TdfString _deviceId = new(__typeInfos[2]);
    private TdfString _personaName = new(__typeInfos[3]);
    private TdfUInt64 _slaveSessionId = new(__typeInfos[4]);
    private TdfInt64 _nucleusAccountId = new(__typeInfos[5]);
    private TdfEnum<Blaze15SDK.Blaze.ClientPlatformType> _platform = new(__typeInfos[6]);
    private TdfUInt64 _userSessionId = new(__typeInfos[7]);

    public LogContext()
    {
        __members = [
            _blazeId,
            _clientIp,
            _deviceId,
            _personaName,
            _slaveSessionId,
            _nucleusAccountId,
            _platform,
            _userSessionId,
        ];
    }

    public override Tdf CreateNew() => new LogContext();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "LogContext";
    public override string GetFullClassName() => "Blaze::Logging::LogContext";

    public long BlazeId
    {
        get => _blazeId.Value;
        set => _blazeId.Value = value;
    }

    public Blaze15SDK.Blaze.IpAddress? ClientIp
    {
        get => _clientIp.Value;
        set => _clientIp.Value = value;
    }

    public string DeviceId
    {
        get => _deviceId.Value;
        set => _deviceId.Value = value;
    }

    public string PersonaName
    {
        get => _personaName.Value;
        set => _personaName.Value = value;
    }

    public ulong SlaveSessionId
    {
        get => _slaveSessionId.Value;
        set => _slaveSessionId.Value = value;
    }

    public long NucleusAccountId
    {
        get => _nucleusAccountId.Value;
        set => _nucleusAccountId.Value = value;
    }

    public Blaze15SDK.Blaze.ClientPlatformType Platform
    {
        get => _platform.Value;
        set => _platform.Value = value;
    }

    public ulong UserSessionId
    {
        get => _userSessionId.Value;
        set => _userSessionId.Value = value;
    }

}
