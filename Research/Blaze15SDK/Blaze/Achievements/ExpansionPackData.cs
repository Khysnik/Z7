using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Achievements;

public class ExpansionPackData : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Id", "mId", 0xA6400000, TdfType.String, 0, true), // Tag: ID
        new TdfMemberInfo("Name", "mName", 0xBA1B6500, TdfType.String, 1, true), // Tag: NAME
    ];
    private ITdfMember[] __members;

    private TdfString _id = new(__typeInfos[0]);
    private TdfString _name = new(__typeInfos[1]);

    public ExpansionPackData()
    {
        __members = [ _id, _name ];
    }

    public override Tdf CreateNew() => new ExpansionPackData();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ExpansionPackData";
    public override string GetFullClassName() => "Blaze::Achievements::ExpansionPackData";

    public string Id
    {
        get => _id.Value;
        set => _id.Value = value;
    }

    public string Name
    {
        get => _name.Value;
        set => _name.Value = value;
    }

}
