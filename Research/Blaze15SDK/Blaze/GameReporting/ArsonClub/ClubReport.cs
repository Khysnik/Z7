using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting.ArsonClub;

public class ClubReport : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ClubRegion", "clubRegion", 0x8F296700, TdfType.UInt32, 0, true), // Tag: CREG
        new TdfMemberInfo("Losses", "losses", 0xB2FCF300, TdfType.UInt16, 1, true), // Tag: LOSS
        new TdfMemberInfo("Points", "points", 0xC2ED3300, TdfType.UInt16, 2, true), // Tag: PNTS
        new TdfMemberInfo("RivalLosses", "rivalLosses", 0xCACBF300, TdfType.UInt16, 3, true), // Tag: RLOS
        new TdfMemberInfo("RivalPoints", "rivalPoints", 0xCB0D3300, TdfType.UInt16, 4, true), // Tag: RPTS
        new TdfMemberInfo("RivalTies", "rivalTies", 0xCB4A6500, TdfType.UInt16, 5, true), // Tag: RTIE
        new TdfMemberInfo("RivalWins", "rivalWins", 0xCB7A6E00, TdfType.UInt16, 6, true), // Tag: RWIN
        new TdfMemberInfo("SeasonLevel", "seasonLevel", 0xCECDAC00, TdfType.UInt32, 7, true), // Tag: SLVL
        new TdfMemberInfo("Ties", "ties", 0xD2997300, TdfType.UInt16, 8, true), // Tag: TIES
        new TdfMemberInfo("Wins", "wins", 0xDE9BB300, TdfType.UInt16, 9, true), // Tag: WINS
    ];
    private ITdfMember[] __members;

    private TdfUInt32 _clubRegion = new(__typeInfos[0]);
    private TdfUInt16 _losses = new(__typeInfos[1]);
    private TdfUInt16 _points = new(__typeInfos[2]);
    private TdfUInt16 _rivalLosses = new(__typeInfos[3]);
    private TdfUInt16 _rivalPoints = new(__typeInfos[4]);
    private TdfUInt16 _rivalTies = new(__typeInfos[5]);
    private TdfUInt16 _rivalWins = new(__typeInfos[6]);
    private TdfUInt32 _seasonLevel = new(__typeInfos[7]);
    private TdfUInt16 _ties = new(__typeInfos[8]);
    private TdfUInt16 _wins = new(__typeInfos[9]);

    public ClubReport()
    {
        __members = [ _clubRegion, _losses, _points, _rivalLosses, _rivalPoints, _rivalTies, _rivalWins, _seasonLevel, _ties, _wins ];
    }

    public override Tdf CreateNew() => new ClubReport();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ClubReport";
    public override string GetFullClassName() => "Blaze::GameReporting::ArsonClub::ClubReport";

    public uint ClubRegion
    {
        get => _clubRegion.Value;
        set => _clubRegion.Value = value;
    }

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

    public ushort RivalLosses
    {
        get => _rivalLosses.Value;
        set => _rivalLosses.Value = value;
    }

    public ushort RivalPoints
    {
        get => _rivalPoints.Value;
        set => _rivalPoints.Value = value;
    }

    public ushort RivalTies
    {
        get => _rivalTies.Value;
        set => _rivalTies.Value = value;
    }

    public ushort RivalWins
    {
        get => _rivalWins.Value;
        set => _rivalWins.Value = value;
    }

    public uint SeasonLevel
    {
        get => _seasonLevel.Value;
        set => _seasonLevel.Value = value;
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
