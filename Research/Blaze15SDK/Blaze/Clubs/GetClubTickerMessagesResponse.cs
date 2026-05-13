using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Clubs;

public class GetClubTickerMessagesResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("MsgList", "mMsgList", 0xB73B2900, TdfType.List, 0, true), // Tag: MSLI
    ];
    private ITdfMember[] __members;

    private TdfList<ClubTickerMessage> _msgList = new(__typeInfos[0]);

    public GetClubTickerMessagesResponse()
    {
        __members = [ _msgList ];
    }

    public override Tdf CreateNew() => new GetClubTickerMessagesResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetClubTickerMessagesResponse";
    public override string GetFullClassName() => "Blaze::Clubs::GetClubTickerMessagesResponse";

    public IList<ClubTickerMessage> MsgList
    {
        get => _msgList.Value;
        set => _msgList.Value = value;
    }

}
