using EATDF;
using EATDF.Members;

namespace Blaze15SDK.Blaze.GameReporting.Fifa;

public class CommonPlayerReport : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Assists", "assists", 0x8E1CF300, TdfType.UInt16, 0, true), // Tag: CASS
        new TdfMemberInfo("GoalsConceded", "goalsConceded", 0x8E7B2300, TdfType.UInt16, 1, true), // Tag: CGLC
        new TdfMemberInfo("HasCleanSheets", "hasCleanSheets", 0x8ECCE800, TdfType.UInt8, 2, true), // Tag: CLSH
        new TdfMemberInfo("Corners", "corners", 0x8EFCAE00, TdfType.UInt16, 3, true), // Tag: CORN
        new TdfMemberInfo("CommonDetailReport", "commondetailreport", 0x8F093200, TdfType.Struct, 4, true), // Tag: CPDR
        new TdfMemberInfo("PassAttempts", "passAttempts", 0x8F0CE100, TdfType.UInt16, 5, true), // Tag: CPSA
        new TdfMemberInfo("PassesMade", "passesMade", 0x8F0CED00, TdfType.UInt16, 6, true), // Tag: CPSM
        new TdfMemberInfo("Rating", "rating", 0x8F287400, TdfType.Float, 7, true), // Tag: CRAT
        new TdfMemberInfo("Saves", "saves", 0x8F387600, TdfType.UInt16, 8, true), // Tag: CSAV
        new TdfMemberInfo("Shots", "shots", 0x8F3A2F00, TdfType.UInt16, 9, true), // Tag: CSHO
        new TdfMemberInfo("TackleAttempts", "tackleAttempts", 0x8F4AE100, TdfType.UInt16, 10, true), // Tag: CTKA
        new TdfMemberInfo("TacklesMade", "tacklesMade", 0x8F4AED00, TdfType.UInt16, 11, true), // Tag: CTKM
        new TdfMemberInfo("Fouls", "fouls", 0x9AFD6C00, TdfType.UInt16, 12, true), // Tag: FOUL
        new TdfMemberInfo("Goals", "goals", 0x9EF86C00, TdfType.UInt16, 13, true), // Tag: GOAL
        new TdfMemberInfo("Interceptions", "interceptions", 0xA6ED2300, TdfType.UInt16, 14, true), // Tag: INTC
        new TdfMemberInfo("HasMOTM", "hasMOTM", 0xB6FD2D00, TdfType.UInt8, 15, true), // Tag: MOTM
        new TdfMemberInfo("Offsides", "offsides", 0xBE69B300, TdfType.UInt16, 16, true), // Tag: OFFS
        new TdfMemberInfo("OwnGoals", "ownGoals", 0xBF79EC00, TdfType.UInt16, 17, true), // Tag: OWGL
        new TdfMemberInfo("PkGoals", "pkGoals", 0xC2B9EC00, TdfType.UInt16, 18, true), // Tag: PKGL
        new TdfMemberInfo("Possession", "possession", 0xC338F400, TdfType.UInt16, 19, true), // Tag: PSCT
        new TdfMemberInfo("RedCard", "redCard", 0xCA48E400, TdfType.UInt16, 20, true), // Tag: RDCD
        new TdfMemberInfo("ShotsOnGoal", "shotsOnGoal", 0xCE89EC00, TdfType.UInt16, 21, true), // Tag: SHGL
        new TdfMemberInfo("UnadjustedScore", "unadjustedScore", 0xD6ECE300, TdfType.UInt16, 22, true), // Tag: UNSC
        new TdfMemberInfo("YellowCard", "yellowCard", 0xE778E400, TdfType.UInt16, 23, true), // Tag: YWCD
    ];
    private ITdfMember[] __members;

    private TdfUInt16 _assists = new(__typeInfos[0]);
    private TdfUInt16 _goalsConceded = new(__typeInfos[1]);
    private TdfUInt8 _hasCleanSheets = new(__typeInfos[2]);
    private TdfUInt16 _corners = new(__typeInfos[3]);
    private TdfStruct<CommonDetailReport?> _commonDetailReport = new(__typeInfos[4]);
    private TdfUInt16 _passAttempts = new(__typeInfos[5]);
    private TdfUInt16 _passesMade = new(__typeInfos[6]);
    private TdfFloat _rating = new(__typeInfos[7]);
    private TdfUInt16 _saves = new(__typeInfos[8]);
    private TdfUInt16 _shots = new(__typeInfos[9]);
    private TdfUInt16 _tackleAttempts = new(__typeInfos[10]);
    private TdfUInt16 _tacklesMade = new(__typeInfos[11]);
    private TdfUInt16 _fouls = new(__typeInfos[12]);
    private TdfUInt16 _goals = new(__typeInfos[13]);
    private TdfUInt16 _interceptions = new(__typeInfos[14]);
    private TdfUInt8 _hasMOTM = new(__typeInfos[15]);
    private TdfUInt16 _offsides = new(__typeInfos[16]);
    private TdfUInt16 _ownGoals = new(__typeInfos[17]);
    private TdfUInt16 _pkGoals = new(__typeInfos[18]);
    private TdfUInt16 _possession = new(__typeInfos[19]);
    private TdfUInt16 _redCard = new(__typeInfos[20]);
    private TdfUInt16 _shotsOnGoal = new(__typeInfos[21]);
    private TdfUInt16 _unadjustedScore = new(__typeInfos[22]);
    private TdfUInt16 _yellowCard = new(__typeInfos[23]);

    public CommonPlayerReport()
    {
        __members = [_assists, _goalsConceded, _hasCleanSheets, _corners, _commonDetailReport, _passAttempts, _passesMade, _rating, _saves, _shots, _tackleAttempts, _tacklesMade, _fouls, _goals, _interceptions, _hasMOTM, _offsides, _ownGoals, _pkGoals, _possession, _redCard, _shotsOnGoal, _unadjustedScore, _yellowCard];
    }

    public override Tdf CreateNew() => new CommonPlayerReport();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "CommonPlayerReport";
    public override string GetFullClassName() => "Blaze::GameReporting::Fifa::CommonPlayerReport";

    public ushort Assists { get => _assists.Value; set => _assists.Value = value; }
    public ushort GoalsConceded { get => _goalsConceded.Value; set => _goalsConceded.Value = value; }
    public byte HasCleanSheets { get => _hasCleanSheets.Value; set => _hasCleanSheets.Value = value; }
    public ushort Corners { get => _corners.Value; set => _corners.Value = value; }
    public CommonDetailReport? CommonDetailReport { get => _commonDetailReport.Value; set => _commonDetailReport.Value = value; }
    public ushort PassAttempts { get => _passAttempts.Value; set => _passAttempts.Value = value; }
    public ushort PassesMade { get => _passesMade.Value; set => _passesMade.Value = value; }
    public float Rating { get => _rating.Value; set => _rating.Value = value; }
    public ushort Saves { get => _saves.Value; set => _saves.Value = value; }
    public ushort Shots { get => _shots.Value; set => _shots.Value = value; }
    public ushort TackleAttempts { get => _tackleAttempts.Value; set => _tackleAttempts.Value = value; }
    public ushort TacklesMade { get => _tacklesMade.Value; set => _tacklesMade.Value = value; }
    public ushort Fouls { get => _fouls.Value; set => _fouls.Value = value; }
    public ushort Goals { get => _goals.Value; set => _goals.Value = value; }
    public ushort Interceptions { get => _interceptions.Value; set => _interceptions.Value = value; }
    public byte HasMOTM { get => _hasMOTM.Value; set => _hasMOTM.Value = value; }
    public ushort Offsides { get => _offsides.Value; set => _offsides.Value = value; }
    public ushort OwnGoals { get => _ownGoals.Value; set => _ownGoals.Value = value; }
    public ushort PkGoals { get => _pkGoals.Value; set => _pkGoals.Value = value; }
    public ushort Possession { get => _possession.Value; set => _possession.Value = value; }
    public ushort RedCard { get => _redCard.Value; set => _redCard.Value = value; }
    public ushort ShotsOnGoal { get => _shotsOnGoal.Value; set => _shotsOnGoal.Value = value; }
    public ushort UnadjustedScore { get => _unadjustedScore.Value; set => _unadjustedScore.Value = value; }
    public ushort YellowCard { get => _yellowCard.Value; set => _yellowCard.Value = value; }
}
