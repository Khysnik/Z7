using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Messaging;

public class SendMessageResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("MessageId", "mMessageId", 0xB67A6400, TdfType.UInt64, 0, true), // Tag: MGID
        new TdfMemberInfo("MessageIds", "mMessageIds", 0xB6993300, TdfType.List, 1, true), // Tag: MIDS
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _messageId = new(__typeInfos[0]);
    private TdfList<ulong> _messageIds = new(__typeInfos[1]);

    public SendMessageResponse()
    {
        __members = [ _messageId, _messageIds ];
    }

    public override Tdf CreateNew() => new SendMessageResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "SendMessageResponse";
    public override string GetFullClassName() => "Blaze::Messaging::SendMessageResponse";

    public ulong MessageId
    {
        get => _messageId.Value;
        set => _messageId.Value = value;
    }

    public IList<ulong> MessageIds
    {
        get => _messageIds.Value;
        set => _messageIds.Value = value;
    }

}
