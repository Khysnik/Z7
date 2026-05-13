using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Achievements;

public class EventData : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Id", "mId", 0x863A6400, TdfType.String, 0, true), // Tag: ACID
        new TdfMemberInfo("Data", "mData", 0x921D2100, TdfType.Map, 1, true), // Tag: DATA
    ];
    private ITdfMember[] __members;

    private TdfString _id = new(__typeInfos[0]);
    private TdfMap<string, string> _data = new(__typeInfos[1]);

    public EventData()
    {
        __members = [ _id, _data ];
    }

    public override Tdf CreateNew() => new EventData();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "EventData";
    public override string GetFullClassName() => "Blaze::Achievements::EventData";

    public string Id
    {
        get => _id.Value;
        set => _id.Value = value;
    }

    public IDictionary<string, string> Data
    {
        get => _data.Value;
        set => _data.Value = value;
    }

}
