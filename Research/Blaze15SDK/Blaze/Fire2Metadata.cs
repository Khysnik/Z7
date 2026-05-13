using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class Fire2Metadata : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Context", "context", 0x8EED3800, TdfType.UInt64, 0, true), // Tag: CNTX
        new TdfMemberInfo("ErrorCode", "errorCode", 0x972CA300, TdfType.Int32, 1, true), // Tag: ERRC
        new TdfMemberInfo("Flags", "mFlags", 0x9AC86700, TdfType.Enum, 2, true), // Tag: FLAG
        new TdfMemberInfo("LogContext", "mLogContext", 0xB23D3800, TdfType.Struct, 3, true), // Tag: LCTX
        new TdfMemberInfo("MovedToAddr", "movedToAddr", 0xB6193200, TdfType.Struct, 4, true), // Tag: MADR
        new TdfMemberInfo("MovedTo", "movedTo", 0xB69A6400, TdfType.UInt16, 5, true), // Tag: MIID
        new TdfMemberInfo("MovedToHostName", "movedToHostName", 0xBA1B6500, TdfType.String, 6, true), // Tag: NAME
        new TdfMemberInfo("SliverIdentity", "sliverIdentity", 0xCE992E00, TdfType.UInt32, 7, true), // Tag: SIDN
        new TdfMemberInfo("SessionKey", "sessionKey", 0xCEB97900, TdfType.String, 8, true), // Tag: SKEY
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _context = new(__typeInfos[0]);
    private TdfInt32 _errorCode = new(__typeInfos[1]);
    private TdfEnum<Blaze15SDK.Blaze.Fire2MetadataFlags> _flags = new(__typeInfos[2]);
    private TdfStruct<Blaze15SDK.Blaze.Logging.LogContext?> _logContext = new(__typeInfos[3]);
    private TdfStruct<Blaze15SDK.Blaze.IpAddress?> _movedToAddr = new(__typeInfos[4]);
    private TdfUInt16 _movedTo = new(__typeInfos[5]);
    private TdfString _movedToHostName = new(__typeInfos[6]);
    private TdfUInt32 _sliverIdentity = new(__typeInfos[7]);
    private TdfString _sessionKey = new(__typeInfos[8]);

    public Fire2Metadata()
    {
        __members = [
            _context,
            _errorCode,
            _flags,
            _logContext,
            _movedToAddr,
            _movedTo,
            _movedToHostName,
            _sliverIdentity,
            _sessionKey,
        ];
    }

    public override Tdf CreateNew() => new Fire2Metadata();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "Fire2Metadata";
    public override string GetFullClassName() => "Blaze::Fire2Metadata";

    public ulong Context
    {
        get => _context.Value;
        set => _context.Value = value;
    }

    public int ErrorCode
    {
        get => _errorCode.Value;
        set => _errorCode.Value = value;
    }

    public Blaze15SDK.Blaze.Fire2MetadataFlags Flags
    {
        get => _flags.Value;
        set => _flags.Value = value;
    }

    public Blaze15SDK.Blaze.Logging.LogContext? LogContext
    {
        get => _logContext.Value;
        set => _logContext.Value = value;
    }

    public Blaze15SDK.Blaze.IpAddress? MovedToAddr
    {
        get => _movedToAddr.Value;
        set => _movedToAddr.Value = value;
    }

    public ushort MovedTo
    {
        get => _movedTo.Value;
        set => _movedTo.Value = value;
    }

    public string MovedToHostName
    {
        get => _movedToHostName.Value;
        set => _movedToHostName.Value = value;
    }

    public uint SliverIdentity
    {
        get => _sliverIdentity.Value;
        set => _sliverIdentity.Value = value;
    }

    public string SessionKey
    {
        get => _sessionKey.Value;
        set => _sessionKey.Value = value;
    }

}
