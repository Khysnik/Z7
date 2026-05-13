using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Messaging;

public class GetMessagesResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Messages", "mMessages", 0xB73B3400, TdfType.List, 0, true), // Tag: MSLT
    ];
    private ITdfMember[] __members;

    private TdfList<ServerMessage> _messages = new(__typeInfos[0]);

    public GetMessagesResponse()
    {
        __members = [ _messages ];
    }

    public override Tdf CreateNew() => new GetMessagesResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetMessagesResponse";
    public override string GetFullClassName() => "Blaze::Messaging::GetMessagesResponse";

    public IList<ServerMessage> Messages
    {
        get => _messages.Value;
        set => _messages.Value = value;
    }

}
