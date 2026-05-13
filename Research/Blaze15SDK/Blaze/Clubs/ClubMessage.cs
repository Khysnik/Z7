using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Clubs;

public class ClubMessage : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ClubId", "mClubId", 0x8ECA6400, TdfType.UInt64, 0, true), // Tag: CLID
        new TdfMemberInfo("ClubName", "mClubName", 0x8ECBAD00, TdfType.String, 1, true), // Tag: CLNM
        new TdfMemberInfo("MessageId", "mMessageId", 0xA6EA6400, TdfType.UInt32, 2, true), // Tag: INID
        new TdfMemberInfo("MessageType", "mMessageType", 0xA6EDB400, TdfType.Enum, 3, true), // Tag: INVT
        new TdfMemberInfo("Receiver", "mReceiver", 0xCA58F200, TdfType.Struct, 4, true), // Tag: RECR
        new TdfMemberInfo("Sender", "mSender", 0xCE593200, TdfType.Struct, 5, true), // Tag: SEDR
        new TdfMemberInfo("TimeSent", "mTimeSent", 0xD29B6500, TdfType.UInt32, 6, true), // Tag: TIME
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _clubId = new(__typeInfos[0]);
    private TdfString _clubName = new(__typeInfos[1]);
    private TdfUInt32 _messageId = new(__typeInfos[2]);
    private TdfEnum<MessageType> _messageType = new(__typeInfos[3]);
    private TdfStruct<Blaze15SDK.Blaze.CoreIdentification?> _receiver = new(__typeInfos[4]);
    private TdfStruct<Blaze15SDK.Blaze.CoreIdentification?> _sender = new(__typeInfos[5]);
    private TdfUInt32 _timeSent = new(__typeInfos[6]);

    public ClubMessage()
    {
        __members = [ _clubId, _clubName, _messageId, _messageType, _receiver, _sender, _timeSent ];
    }

    public override Tdf CreateNew() => new ClubMessage();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ClubMessage";
    public override string GetFullClassName() => "Blaze::Clubs::ClubMessage";

    public ulong ClubId
    {
        get => _clubId.Value;
        set => _clubId.Value = value;
    }

    public string ClubName
    {
        get => _clubName.Value;
        set => _clubName.Value = value;
    }

    public uint MessageId
    {
        get => _messageId.Value;
        set => _messageId.Value = value;
    }

    public MessageType MessageType
    {
        get => _messageType.Value;
        set => _messageType.Value = value;
    }

    public Blaze15SDK.Blaze.CoreIdentification? Receiver
    {
        get => _receiver.Value;
        set => _receiver.Value = value;
    }

    public Blaze15SDK.Blaze.CoreIdentification? Sender
    {
        get => _sender.Value;
        set => _sender.Value = value;
    }

    public uint TimeSent
    {
        get => _timeSent.Value;
        set => _timeSent.Value = value;
    }

}
