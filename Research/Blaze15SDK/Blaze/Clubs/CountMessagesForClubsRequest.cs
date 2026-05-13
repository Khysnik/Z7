using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Clubs;

public class CountMessagesForClubsRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ClubIdList", "mClubIdList", 0x8E992C00, TdfType.List, 0, true), // Tag: CIDL
        new TdfMemberInfo("MessageType", "mMessageType", 0xB73D3900, TdfType.Enum, 1, true), // Tag: MSTY
    ];
    private ITdfMember[] __members;

    private TdfList<ulong> _clubIdList = new(__typeInfos[0]);
    private TdfEnum<MessageType> _messageType = new(__typeInfos[1]);

    public CountMessagesForClubsRequest()
    {
        __members = [ _clubIdList, _messageType ];
    }

    public override Tdf CreateNew() => new CountMessagesForClubsRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "CountMessagesForClubsRequest";
    public override string GetFullClassName() => "Blaze::Clubs::CountMessagesForClubsRequest";

    public IList<ulong> ClubIdList
    {
        get => _clubIdList.Value;
        set => _clubIdList.Value = value;
    }

    public MessageType MessageType
    {
        get => _messageType.Value;
        set => _messageType.Value = value;
    }

}
