using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting;

public class GenericEvent : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("EventMap", "eventMap", 0x97697400, TdfType.Map, 0, true), // Tag: EVET
    ];
    private ITdfMember[] __members;

    private TdfMap<string, string> _eventMap = new(__typeInfos[0]);

    public GenericEvent()
    {
        __members = [ _eventMap ];
    }

    public override Tdf CreateNew() => new GenericEvent();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GenericEvent";
    public override string GetFullClassName() => "Blaze::GameReporting::GenericEvent";

    public IDictionary<string, string> EventMap
    {
        get => _eventMap.Value;
        set => _eventMap.Value = value;
    }

}
