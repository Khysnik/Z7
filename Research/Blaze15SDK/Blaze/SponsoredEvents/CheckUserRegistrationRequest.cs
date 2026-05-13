using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.SponsoredEvents;

public class CheckUserRegistrationRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("EventID", "mEventID", 0x96990000, TdfType.UInt32, 0, true), // Tag: EID
        new TdfMemberInfo("UserID", "mUserID", 0xD6990000, TdfType.Int64, 1, true), // Tag: UID
    ];
    private ITdfMember[] __members;

    private TdfUInt32 _eventID = new(__typeInfos[0]);
    private TdfInt64 _userID = new(__typeInfos[1]);

    public CheckUserRegistrationRequest()
    {
        __members = [_eventID, _userID];
    }

    public override Tdf CreateNew() => new CheckUserRegistrationRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "CheckUserRegistrationRequest";
    public override string GetFullClassName() => "Blaze::SponsoredEvents::CheckUserRegistrationRequest";

    public uint EventID
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
