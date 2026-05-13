using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting.ArsonLeague;

public class OffensiveStats : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Games", "games", 0x9E1B6500, TdfType.UInt32, 0, true), // Tag: GAME
        new TdfMemberInfo("Hits", "hits", 0xA29D3300, TdfType.UInt32, 1, true), // Tag: HITS
        new TdfMemberInfo("Pf", "pf", 0xC2600000, TdfType.UInt32, 2, true), // Tag: PF
        new TdfMemberInfo("Serves", "serves", 0xCE5CB600, TdfType.UInt32, 3, true), // Tag: SERV
        new TdfMemberInfo("Time", "time", 0xD29B6500, TdfType.UInt32, 4, true), // Tag: TIME
    ];
    private ITdfMember[] __members;

    private TdfUInt32 _games = new(__typeInfos[0]);
    private TdfUInt32 _hits = new(__typeInfos[1]);
    private TdfUInt32 _pf = new(__typeInfos[2]);
    private TdfUInt32 _serves = new(__typeInfos[3]);
    private TdfUInt32 _time = new(__typeInfos[4]);

    public OffensiveStats()
    {
        __members = [ _games, _hits, _pf, _serves, _time ];
    }

    public override Tdf CreateNew() => new OffensiveStats();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "OffensiveStats";
    public override string GetFullClassName() => "Blaze::GameReporting::ArsonLeague::OffensiveStats";

    public uint Games
    {
        get => _games.Value;
        set => _games.Value = value;
    }

    public uint Hits
    {
        get => _hits.Value;
        set => _hits.Value = value;
    }

    public uint Pf
    {
        get => _pf.Value;
        set => _pf.Value = value;
    }

    public uint Serves
    {
        get => _serves.Value;
        set => _serves.Value = value;
    }

    public uint Time
    {
        get => _time.Value;
        set => _time.Value = value;
    }

}
