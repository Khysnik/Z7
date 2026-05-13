using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Achievements;

public class EventsPayload : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Device", "mDevice", 0x925D8000, TdfType.String, 0, true), // Tag: DEV
        new TdfMemberInfo("Events", "mEvents", 0x97696E00, TdfType.List, 1, true), // Tag: EVEN
    ];
    private ITdfMember[] __members;

    private TdfString _device = new(__typeInfos[0]);
    private TdfList<EventData> _events = new(__typeInfos[1]);

    public EventsPayload()
    {
        __members = [ _device, _events ];
    }

    public override Tdf CreateNew() => new EventsPayload();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "EventsPayload";
    public override string GetFullClassName() => "Blaze::Achievements::EventsPayload";

    public string Device
    {
        get => _device.Value;
        set => _device.Value = value;
    }

    public IList<EventData> Events
    {
        get => _events.Value;
        set => _events.Value = value;
    }

}
