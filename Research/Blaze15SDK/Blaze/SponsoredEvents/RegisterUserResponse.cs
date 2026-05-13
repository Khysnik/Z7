using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.SponsoredEvents;

public class RegisterUserResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("EventID", "mEventID", 0x976A6400, TdfType.Int32, 0, true), // Tag: EVID
    ];
    private ITdfMember[] __members;

    private TdfInt32 _eventID = new(__typeInfos[0]);

    public RegisterUserResponse()
    {
        __members = [_eventID];
    }

    public override Tdf CreateNew() => new RegisterUserResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "RegisterUserResponse";
    public override string GetFullClassName() => "Blaze::SponsoredEvents::RegisterUserResponse";

    public int EventID
    {
        get => _eventID.Value;
        set => _eventID.Value = value;
    }
}
