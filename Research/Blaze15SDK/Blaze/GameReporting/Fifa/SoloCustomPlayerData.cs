using EATDF;
using EATDF.Members;

namespace Blaze15SDK.Blaze.GameReporting.Fifa;

public class SoloCustomPlayerData : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("GoalAgainst", "goalAgainst", 0x9EC86700, TdfType.UInt16, 0, true), // Tag: GLAG
        new TdfMemberInfo("Losses", "losses", 0xB2FCF300, TdfType.UInt16, 1, true), // Tag: LOSS
        new TdfMemberInfo("OppGoal", "oppGoal", 0xBF0C2700, TdfType.UInt16, 2, true), // Tag: OPPG
        new TdfMemberInfo("Result", "result", 0xCB3B3400, TdfType.UInt16, 3, true), // Tag: RSLT
        new TdfMemberInfo("ShotsAgainst", "shotsAgainst", 0xCE886700, TdfType.UInt16, 4, true), // Tag: SHAG
        new TdfMemberInfo("ShotsAgainstOnGoal", "shotsAgainstOnGoal", 0xCE89EC00, TdfType.UInt16, 5, true), // Tag: SHGL
        new TdfMemberInfo("Team", "team", 0xD2586D00, TdfType.UInt32, 6, true), // Tag: TEAM
        new TdfMemberInfo("Ties", "ties", 0xD2997300, TdfType.UInt16, 7, true), // Tag: TIES
        new TdfMemberInfo("Wins", "wins", 0xDE9BB300, TdfType.UInt16, 8, true), // Tag: WINS
    ];
    private ITdfMember[] __members;

    private TdfUInt16 _goalAgainst = new(__typeInfos[0]);
    private TdfUInt16 _losses = new(__typeInfos[1]);
    private TdfUInt16 _oppGoal = new(__typeInfos[2]);
    private TdfUInt16 _result = new(__typeInfos[3]);
    private TdfUInt16 _shotsAgainst = new(__typeInfos[4]);
    private TdfUInt16 _shotsAgainstOnGoal = new(__typeInfos[5]);
    private TdfUInt32 _team = new(__typeInfos[6]);
    private TdfUInt16 _ties = new(__typeInfos[7]);
    private TdfUInt16 _wins = new(__typeInfos[8]);

    public SoloCustomPlayerData()
    {
        __members = [_goalAgainst, _losses, _oppGoal, _result, _shotsAgainst, _shotsAgainstOnGoal, _team, _ties, _wins];
    }

    public override Tdf CreateNew() => new SoloCustomPlayerData();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "SoloCustomPlayerData";
    public override string GetFullClassName() => "Blaze::GameReporting::Fifa::SoloCustomPlayerData";

    public ushort GoalAgainst { get => _goalAgainst.Value; set => _goalAgainst.Value = value; }
    public ushort Losses { get => _losses.Value; set => _losses.Value = value; }
    public ushort OppGoal { get => _oppGoal.Value; set => _oppGoal.Value = value; }
    public ushort Result { get => _result.Value; set => _result.Value = value; }
    public ushort ShotsAgainst { get => _shotsAgainst.Value; set => _shotsAgainst.Value = value; }
    public ushort ShotsAgainstOnGoal { get => _shotsAgainstOnGoal.Value; set => _shotsAgainstOnGoal.Value = value; }
    public uint Team { get => _team.Value; set => _team.Value = value; }
    public ushort Ties { get => _ties.Value; set => _ties.Value = value; }
    public ushort Wins { get => _wins.Value; set => _wins.Value = value; }
}
