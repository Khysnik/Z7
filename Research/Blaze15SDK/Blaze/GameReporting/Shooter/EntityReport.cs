using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting.Shooter;

public class EntityReport : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Stats", "stats", 0xCF487400, TdfType.Map, 0, true), // Tag: STAT
    ];
    private ITdfMember[] __members;

    private TdfMap<string, float> _stats = new(__typeInfos[0]);

    public EntityReport()
    {
        __members = [ _stats ];
    }

    public override Tdf CreateNew() => new EntityReport();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "EntityReport";
    public override string GetFullClassName() => "Blaze::GameReporting::Shooter::EntityReport";

    public IDictionary<string, float> Stats
    {
        get => _stats.Value;
        set => _stats.Value = value;
    }

}
