using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Clubs;

public class GetClubTickerMessagesForClubsRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ClubIdList", "mClubIdList", 0x8E992C00, TdfType.List, 0, true), // Tag: CIDL
        new TdfMemberInfo("OldestTimestamp", "mOldestTimestamp", 0xD33D2D00, TdfType.UInt32, 1, true), // Tag: TSTM
    ];
    private ITdfMember[] __members;

    private TdfList<ulong> _clubIdList = new(__typeInfos[0]);
    private TdfUInt32 _oldestTimestamp = new(__typeInfos[1]);

    public GetClubTickerMessagesForClubsRequest()
    {
        __members = [ _clubIdList, _oldestTimestamp ];
    }

    public override Tdf CreateNew() => new GetClubTickerMessagesForClubsRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetClubTickerMessagesForClubsRequest";
    public override string GetFullClassName() => "Blaze::Clubs::GetClubTickerMessagesForClubsRequest";

    public IList<ulong> ClubIdList
    {
        get => _clubIdList.Value;
        set => _clubIdList.Value = value;
    }

    public uint OldestTimestamp
    {
        get => _oldestTimestamp.Value;
        set => _oldestTimestamp.Value = value;
    }

}
