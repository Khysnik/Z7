using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting.ArsonLeague;

public class AthleteReport : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("DefensivePlayerStats", "defensivePlayerStats", 0x9259B300, TdfType.Struct, 0, true), // Tag: DEFS
        new TdfMemberInfo("OffensivePlayerStats", "offensivePlayerStats", 0xBE69B300, TdfType.Struct, 1, true), // Tag: OFFS
    ];
    private ITdfMember[] __members;

    private TdfStruct<DefensiveStats?> _defensivePlayerStats = new(__typeInfos[0]);
    private TdfStruct<OffensiveStats?> _offensivePlayerStats = new(__typeInfos[1]);

    public AthleteReport()
    {
        __members = [ _defensivePlayerStats, _offensivePlayerStats ];
    }

    public override Tdf CreateNew() => new AthleteReport();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "AthleteReport";
    public override string GetFullClassName() => "Blaze::GameReporting::ArsonLeague::AthleteReport";

    public DefensiveStats? DefensivePlayerStats
    {
        get => _defensivePlayerStats.Value;
        set => _defensivePlayerStats.Value = value;
    }

    public OffensiveStats? OffensivePlayerStats
    {
        get => _offensivePlayerStats.Value;
        set => _offensivePlayerStats.Value = value;
    }

}
