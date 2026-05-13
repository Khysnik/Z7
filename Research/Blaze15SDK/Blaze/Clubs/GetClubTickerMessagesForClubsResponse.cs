using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Clubs;

public class GetClubTickerMessagesForClubsResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("MsgListMap", "mMsgListMap", 0xB6FB6C00, TdfType.Struct, 0, true), // Tag: MOML
    ];
    private ITdfMember[] __members;

    private TdfMap<ulong, IList<ClubTickerMessage>> _msgListMap = new(__typeInfos[0]);

    public GetClubTickerMessagesForClubsResponse()
    {
        __members = [ _msgListMap ];
    }

    public override Tdf CreateNew() => new GetClubTickerMessagesForClubsResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetClubTickerMessagesForClubsResponse";
    public override string GetFullClassName() => "Blaze::Clubs::GetClubTickerMessagesForClubsResponse";

    public IDictionary<ulong, IList<ClubTickerMessage>> MsgListMap
    {
        get => _msgListMap.Value;
        set => _msgListMap.Value = value;
    }

}
