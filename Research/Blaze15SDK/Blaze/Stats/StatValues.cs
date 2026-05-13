using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Stats;

public class StatValues : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("EntityStatsList", "mEntityStatsList", 0xCF487400, TdfType.List, 0, true), // Tag: STAT
    ];
    private ITdfMember[] __members;

    private TdfList<Blaze15SDK.Blaze.Stats.EntityStats> _entityStatsList = new(__typeInfos[0]);

    public StatValues()
    {
        __members = [ _entityStatsList ];
    }

    public override Tdf CreateNew() => new StatValues();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "StatValues";
    public override string GetFullClassName() => "Blaze::Stats::StatValues";

    public IList<Blaze15SDK.Blaze.Stats.EntityStats> EntityStatsList
    {
        get => _entityStatsList.Value;
        set => _entityStatsList.Value = value;
    }

}
