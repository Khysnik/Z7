using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting.ArsonClubGameKeyscopes_NonDerived;

public class ClubReport : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Losses", "losses", 0xB2FCF300, TdfType.UInt16, 0, true), // Tag: LOSS
        new TdfMemberInfo("Wins", "wins", 0xDE9BB300, TdfType.UInt16, 1, true), // Tag: WINS
    ];
    private ITdfMember[] __members;

    private TdfUInt16 _losses = new(__typeInfos[0]);
    private TdfUInt16 _wins = new(__typeInfos[1]);

    public ClubReport()
    {
        __members = [ _losses, _wins ];
    }

    public override Tdf CreateNew() => new ClubReport();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ClubReport";
    public override string GetFullClassName() => "Blaze::GameReporting::ArsonClubGameKeyscopes_NonDerived::ClubReport";

    public ushort Losses
    {
        get => _losses.Value;
        set => _losses.Value = value;
    }

    public ushort Wins
    {
        get => _wins.Value;
        set => _wins.Value = value;
    }

}
