using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Messaging;

public class ServerMessage : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Flags", "mFlags", 0x9AC86700, TdfType.Enum, 0, true), // Tag: FLAG
        new TdfMemberInfo("MessageId", "mMessageId", 0xB67A6400, TdfType.UInt64, 1, true), // Tag: MGID
        new TdfMemberInfo("Payload", "mPayload", 0xC39B2400, TdfType.Struct, 2, true), // Tag: PYLD
        new TdfMemberInfo("Source", "mSource", 0xCF28E500, TdfType.ObjectId, 3, true), // Tag: SRCE
        new TdfMemberInfo("Timestamp", "mTimestamp", 0xD29B6500, TdfType.UInt32, 4, true), // Tag: TIME
        new TdfMemberInfo("SourceIdent", "mSourceIdent", 0xD7397200, TdfType.Struct, 5, true), // Tag: USER
    ];
    private ITdfMember[] __members;

    private TdfEnum<ServerFlags> _flags = new(__typeInfos[0]);
    private TdfUInt64 _messageId = new(__typeInfos[1]);
    private TdfStruct<ClientMessage?> _payload = new(__typeInfos[2]);
    private TdfObjectId _source = new(__typeInfos[3]);
    private TdfUInt32 _timestamp = new(__typeInfos[4]);
    private TdfStruct<Blaze15SDK.Blaze.CoreIdentification?> _sourceIdent = new(__typeInfos[5]);

    public ServerMessage()
    {
        __members = [ _flags, _messageId, _payload, _source, _timestamp, _sourceIdent ];
    }

    public override Tdf CreateNew() => new ServerMessage();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ServerMessage";
    public override string GetFullClassName() => "Blaze::Messaging::ServerMessage";

    public ServerFlags Flags
    {
        get => _flags.Value;
        set => _flags.Value = value;
    }

    public ulong MessageId
    {
        get => _messageId.Value;
        set => _messageId.Value = value;
    }

    public ClientMessage? Payload
    {
        get => _payload.Value;
        set => _payload.Value = value;
    }

    public ObjectId Source
    {
        get => _source.Value;
        set => _source.Value = value;
    }

    public uint Timestamp
    {
        get => _timestamp.Value;
        set => _timestamp.Value = value;
    }

    public Blaze15SDK.Blaze.CoreIdentification? SourceIdent
    {
        get => _sourceIdent.Value;
        set => _sourceIdent.Value = value;
    }

}
