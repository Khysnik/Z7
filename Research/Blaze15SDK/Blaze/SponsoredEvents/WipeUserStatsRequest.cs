using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.SponsoredEvents;

public class WipeUserStatsRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("EventID", "mEventID", 0x976A6400, TdfType.Int32, 0, true), // Tag: EVID
        new TdfMemberInfo("UserID", "mUserID", 0xD73A6400, TdfType.Int64, 1, true), // Tag: USID
    ];
    private ITdfMember[] __members;

    private TdfInt32 _eventID = new(__typeInfos[0]);
    private TdfInt64 _userID = new(__typeInfos[1]);

    public WipeUserStatsRequest()
    {
        __members = [_eventID, _userID];
    }

    public override Tdf CreateNew() => new WipeUserStatsRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "WipeUserStatsRequest";
    public override string GetFullClassName() => "Blaze::SponsoredEvents::WipeUserStatsRequest";

    public int EventID
    {
        get => _eventID.Value;
        set => _eventID.Value = value;
    }

    public long UserID
    {
        get => _userID.Value;
        set => _userID.Value = value;
    }
}
