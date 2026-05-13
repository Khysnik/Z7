using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting.ArsonLeague;

public class PlayerReport : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Blocks", "blocks", 0x8ACAF300, TdfType.Int32, 0, true), // Tag: BLKS
        new TdfMemberInfo("Hits", "hits", 0xA29D3300, TdfType.Int32, 1, true), // Tag: HITS
        new TdfMemberInfo("Misses", "misses", 0xB69CF300, TdfType.Int32, 2, true), // Tag: MISS
        new TdfMemberInfo("Rebs", "rebs", 0xCA58B300, TdfType.Int32, 3, true), // Tag: REBS
        new TdfMemberInfo("Score", "score", 0xCE3BF200, TdfType.Int32, 4, true), // Tag: SCOR
        new TdfMemberInfo("Serves", "serves", 0xCE5CB600, TdfType.Int32, 5, true), // Tag: SERV
    ];
    private ITdfMember[] __members;

    private TdfInt32 _blocks = new(__typeInfos[0]);
    private TdfInt32 _hits = new(__typeInfos[1]);
    private TdfInt32 _misses = new(__typeInfos[2]);
    private TdfInt32 _rebs = new(__typeInfos[3]);
    private TdfInt32 _score = new(__typeInfos[4]);
    private TdfInt32 _serves = new(__typeInfos[5]);

    public PlayerReport()
    {
        __members = [ _blocks, _hits, _misses, _rebs, _score, _serves ];
    }

    public override Tdf CreateNew() => new PlayerReport();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "PlayerReport";
    public override string GetFullClassName() => "Blaze::GameReporting::ArsonLeague::PlayerReport";

    public int Blocks
    {
        get => _blocks.Value;
        set => _blocks.Value = value;
    }

    public int Hits
    {
        get => _hits.Value;
        set => _hits.Value = value;
    }

    public int Misses
    {
        get => _misses.Value;
        set => _misses.Value = value;
    }

    public int Rebs
    {
        get => _rebs.Value;
        set => _rebs.Value = value;
    }

    public int Score
    {
        get => _score.Value;
        set => _score.Value = value;
    }

    public int Serves
    {
        get => _serves.Value;
        set => _serves.Value = value;
    }

}
