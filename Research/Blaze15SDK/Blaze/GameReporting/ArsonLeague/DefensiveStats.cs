using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting.ArsonLeague;

public class DefensiveStats : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Blocks", "blocks", 0x8ACBE300, TdfType.UInt32, 0, true), // Tag: BLOC
        new TdfMemberInfo("Games", "games", 0x9E1B6500, TdfType.UInt32, 1, true), // Tag: GAME
        new TdfMemberInfo("Misses", "misses", 0xB69CF300, TdfType.UInt32, 2, true), // Tag: MISS
        new TdfMemberInfo("Rebs", "rebs", 0xCA58B300, TdfType.UInt32, 3, true), // Tag: REBS
        new TdfMemberInfo("Time", "time", 0xD29B6500, TdfType.UInt32, 4, true), // Tag: TIME
    ];
    private ITdfMember[] __members;

    private TdfUInt32 _blocks = new(__typeInfos[0]);
    private TdfUInt32 _games = new(__typeInfos[1]);
    private TdfUInt32 _misses = new(__typeInfos[2]);
    private TdfUInt32 _rebs = new(__typeInfos[3]);
    private TdfUInt32 _time = new(__typeInfos[4]);

    public DefensiveStats()
    {
        __members = [ _blocks, _games, _misses, _rebs, _time ];
    }

    public override Tdf CreateNew() => new DefensiveStats();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "DefensiveStats";
    public override string GetFullClassName() => "Blaze::GameReporting::ArsonLeague::DefensiveStats";

    public uint Blocks
    {
        get => _blocks.Value;
        set => _blocks.Value = value;
    }

    public uint Games
    {
        get => _games.Value;
        set => _games.Value = value;
    }

    public uint Misses
    {
        get => _misses.Value;
        set => _misses.Value = value;
    }

    public uint Rebs
    {
        get => _rebs.Value;
        set => _rebs.Value = value;
    }

    public uint Time
    {
        get => _time.Value;
        set => _time.Value = value;
    }

}
