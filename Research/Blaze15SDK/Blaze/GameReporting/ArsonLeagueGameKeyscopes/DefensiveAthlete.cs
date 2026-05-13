using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting.ArsonLeagueGameKeyscopes;

public class DefensiveAthlete : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Blocks", "blocks", 0x8ACAF300, TdfType.UInt16, 0, true), // Tag: BLKS
    ];
    private ITdfMember[] __members;

    private TdfUInt16 _blocks = new(__typeInfos[0]);

    public DefensiveAthlete()
    {
        __members = [ _blocks ];
    }

    public override Tdf CreateNew() => new DefensiveAthlete();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "DefensiveAthlete";
    public override string GetFullClassName() => "Blaze::GameReporting::ArsonLeagueGameKeyscopes::DefensiveAthlete";

    public ushort Blocks
    {
        get => _blocks.Value;
        set => _blocks.Value = value;
    }

}
