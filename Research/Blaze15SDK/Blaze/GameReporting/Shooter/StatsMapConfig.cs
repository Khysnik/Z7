using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting.Shooter;

public class StatsMapConfig : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("StatValueScope", "mStatValueScope", 0xCF6B7000, TdfType.Map, 0, true), // Tag: SVMP
    ];
    private ITdfMember[] __members;

    private TdfMap<string, TdfMap<int, int>> _statValueScope = new(__typeInfos[0]);

    public StatsMapConfig()
    {
        __members = [ _statValueScope ];
    }

    public override Tdf CreateNew() => new StatsMapConfig();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "StatsMapConfig";
    public override string GetFullClassName() => "Blaze::GameReporting::Shooter::StatsMapConfig";

    public IDictionary<string, TdfMap<int, int>> StatValueScope
    {
        get => _statValueScope.Value;
        set => _statValueScope.Value = value;
    }

}
