using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Messaging;

public class SendGlobalMessageResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("MessageIds", "mMessageIds", 0xB6993300, TdfType.List, 0, true), // Tag: MIDS
    ];
    private ITdfMember[] __members;

    private TdfList<ulong> _messageIds = new(__typeInfos[0]);

    public SendGlobalMessageResponse()
    {
        __members = [ _messageIds ];
    }

    public override Tdf CreateNew() => new SendGlobalMessageResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "SendGlobalMessageResponse";
    public override string GetFullClassName() => "Blaze::Messaging::SendGlobalMessageResponse";

    public IList<ulong> MessageIds
    {
        get => _messageIds.Value;
        set => _messageIds.Value = value;
    }

}
