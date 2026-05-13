using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting.IntegratedSample;

public class PlayerReport : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Hits", "hits", 0xA29D3300, TdfType.UInt16, 0, true), // Tag: HITS
        new TdfMemberInfo("Loser", "loser", 0xB2FCE500, TdfType.UInt16, 1, true), // Tag: LOSE
        new TdfMemberInfo("Misses", "misses", 0xB69CF300, TdfType.UInt16, 2, true), // Tag: MISS
        new TdfMemberInfo("Score", "score", 0xCE3C8000, TdfType.UInt16, 3, true), // Tag: SCR
        new TdfMemberInfo("Torchings", "torchings", 0xD328E800, TdfType.UInt16, 4, true), // Tag: TRCH
        new TdfMemberInfo("Winner", "winner", 0xDE9B8000, TdfType.UInt16, 5, true), // Tag: WIN
    ];
    private ITdfMember[] __members;

    private TdfUInt16 _hits = new(__typeInfos[0]);
    private TdfUInt16 _loser = new(__typeInfos[1]);
    private TdfUInt16 _misses = new(__typeInfos[2]);
    private TdfUInt16 _score = new(__typeInfos[3]);
    private TdfUInt16 _torchings = new(__typeInfos[4]);
    private TdfUInt16 _winner = new(__typeInfos[5]);

    public PlayerReport()
    {
        __members = [ _hits, _loser, _misses, _score, _torchings, _winner ];
    }

    public override Tdf CreateNew() => new PlayerReport();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "PlayerReport";
    public override string GetFullClassName() => "Blaze::GameReporting::IntegratedSample::PlayerReport";

    public ushort Hits
    {
        get => _hits.Value;
        set => _hits.Value = value;
    }

    public ushort Loser
    {
        get => _loser.Value;
        set => _loser.Value = value;
    }

    public ushort Misses
    {
        get => _misses.Value;
        set => _misses.Value = value;
    }

    public ushort Score
    {
        get => _score.Value;
        set => _score.Value = value;
    }

    public ushort Torchings
    {
        get => _torchings.Value;
        set => _torchings.Value = value;
    }

    public ushort Winner
    {
        get => _winner.Value;
        set => _winner.Value = value;
    }

}
