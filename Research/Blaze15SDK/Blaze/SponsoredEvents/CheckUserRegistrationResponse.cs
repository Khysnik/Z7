using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.SponsoredEvents;

public class CheckUserRegistrationResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("IsRegistered", "mIsRegistered", 0x8B29C000, TdfType.Int8, 0, true), // Tag: BRG
        new TdfMemberInfo("EventData", "mEventData", 0x97690000, TdfType.String, 1, true), // Tag: EVD
        new TdfMemberInfo("EventID", "mEventID", 0x976A6400, TdfType.Int32, 2, true), // Tag: EVID
    ];
    private ITdfMember[] __members;

    private TdfInt8 _isRegistered = new(__typeInfos[0]);
    private TdfString _eventData = new(__typeInfos[1]);
    private TdfInt32 _eventID = new(__typeInfos[2]);

    public CheckUserRegistrationResponse()
    {
        __members = [_isRegistered, _eventData, _eventID];
    }

    public override Tdf CreateNew() => new CheckUserRegistrationResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "CheckUserRegistrationResponse";
    public override string GetFullClassName() => "Blaze::SponsoredEvents::CheckUserRegistrationResponse";

    public sbyte IsRegistered
    {
        get => _isRegistered.Value;
        set => _isRegistered.Value = value;
    }

    public string EventData
    {
        get => _eventData.Value;
        set => _eventData.Value = value;
    }

    public int EventID
    {
        get => _eventID.Value;
        set => _eventID.Value = value;
    }
}
