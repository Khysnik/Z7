using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting.GameHistoryClubs_NonDerived;

public class ClubReport : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Losses", "losses", 0xB2FCF300, TdfType.UInt16, 0, true), // Tag: LOSS
        new TdfMemberInfo("Points", "points", 0xC2FA6E00, TdfType.UInt16, 1, true), // Tag: POIN
        new TdfMemberInfo("Ties", "ties", 0xD2997300, TdfType.UInt16, 2, true), // Tag: TIES
        new TdfMemberInfo("Wins", "wins", 0xDE9BB300, TdfType.UInt16, 3, true), // Tag: WINS
    ];
    private ITdfMember[] __members;

    private TdfUInt16 _losses = new(__typeInfos[0]);
    private TdfUInt16 _points = new(__typeInfos[1]);
    private TdfUInt16 _ties = new(__typeInfos[2]);
    private TdfUInt16 _wins = new(__typeInfos[3]);

    public ClubReport()
    {
        __members = [ _losses, _points, _ties, _wins ];
    }

    public override Tdf CreateNew() => new ClubReport();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ClubReport";
    public override string GetFullClassName() => "Blaze::GameReporting::GameHistoryClubs_NonDerived::ClubReport";

    public ushort Losses
    {
        get => _losses.Value;
        set => _losses.Value = value;
    }

    public ushort Points
    {
        get => _points.Value;
        set => _points.Value = value;
    }

    public ushort Ties
    {
        get => _ties.Value;
        set => _ties.Value = value;
    }

    public ushort Wins
    {
        get => _wins.Value;
        set => _wins.Value = value;
    }

}
