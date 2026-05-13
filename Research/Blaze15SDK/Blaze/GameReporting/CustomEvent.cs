using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting;

public class CustomEvent : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("EventData", "mEventData", 0x97696400, TdfType.Variable, 0, true), // Tag: EVED
        new TdfMemberInfo("EventType", "mEventType", 0x976D2E00, TdfType.String, 1, true), // Tag: EVTN
    ];
    private ITdfMember[] __members;

    private TdfVariable _eventData = new(__typeInfos[0]);
    private TdfString _eventType = new(__typeInfos[1]);

    public CustomEvent()
    {
        __members = [ _eventData, _eventType ];
    }

    public override Tdf CreateNew() => new CustomEvent();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "CustomEvent";
    public override string GetFullClassName() => "Blaze::GameReporting::CustomEvent";

    public object? EventData
    {
        get => _eventData.Value;
        set => _eventData.Value = value;
    }

    public string EventType
    {
        get => _eventType.Value;
        set => _eventType.Value = value;
    }

}
