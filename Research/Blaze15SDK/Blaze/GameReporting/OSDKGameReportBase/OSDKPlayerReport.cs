using EATDF;
using EATDF.Members;

namespace Blaze15SDK.Blaze.GameReporting.OSDKGameReportBase;

public class OSDKPlayerReport : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("CustomDnf", "customDnf", 0x8E4BA600, TdfType.UInt32, 0, true), // Tag: CDNF
        new TdfMemberInfo("CustomPlayerReport", "customPlayerReport", 0x8F0CB400, TdfType.Variable, 1, true), // Tag: CPRT
        new TdfMemberInfo("ClientScore", "clientScore", 0x8F38EF00, TdfType.UInt32, 2, true), // Tag: CSCO
        new TdfMemberInfo("AccountCountry", "accountCountry", 0x8F4CB900, TdfType.UInt16, 3, true), // Tag: CTRY
        new TdfMemberInfo("FinishReason", "finishReason", 0x9A8CAE00, TdfType.UInt32, 4, true), // Tag: FHRN
        new TdfMemberInfo("GameResult", "gameResult", 0x9F2B3400, TdfType.UInt32, 5, true), // Tag: GRLT
        new TdfMemberInfo("Home", "home", 0xA2FB6500, TdfType.Bool, 6, true), // Tag: HOME
        new TdfMemberInfo("Losses", "losses", 0xB2FCF300, TdfType.UInt32, 7, true), // Tag: LOSS
        new TdfMemberInfo("Name", "name", 0xBA1B6500, TdfType.String, 8, true), // Tag: NAME
        new TdfMemberInfo("OpponentCount", "opponentCount", 0xBF08F400, TdfType.UInt32, 9, true), // Tag: OPCT
        new TdfMemberInfo("PrivatePlayerReport", "privatePlayerReport", 0xBF0C3200, TdfType.Struct, 10, true), // Tag: OPPR
        new TdfMemberInfo("ExternalId", "externalId", 0xC25A6400, TdfType.UInt64, 11, true), // Tag: PEID
        new TdfMemberInfo("NucleusId", "nucleusId", 0xC2EA6400, TdfType.UInt64, 12, true), // Tag: PNID
        new TdfMemberInfo("Persona", "persona", 0xC30BA100, TdfType.String, 13, true), // Tag: PPNA
        new TdfMemberInfo("PointsAgainst", "pointsAgainst", 0xC3486700, TdfType.UInt32, 14, true), // Tag: PTAG
        new TdfMemberInfo("UserResult", "userResult", 0xCA5B3400, TdfType.UInt32, 15, true), // Tag: RELT
        new TdfMemberInfo("Score", "score", 0xCE3BF200, TdfType.UInt32, 16, true), // Tag: SCOR
        new TdfMemberInfo("Skill", "skill", 0xCEBA6C00, TdfType.UInt32, 17, true), // Tag: SKIL
        new TdfMemberInfo("SkillPoints", "skillPoints", 0xCEBC3400, TdfType.UInt32, 18, true), // Tag: SKPT
        new TdfMemberInfo("Team", "team", 0xD2586D00, TdfType.UInt32, 19, true), // Tag: TEAM
        new TdfMemberInfo("Ties", "ties", 0xD2997300, TdfType.UInt32, 20, true), // Tag: TIES
        new TdfMemberInfo("WinnerByDnf", "winnerByDnf", 0xDE4BA600, TdfType.UInt32, 21, true), // Tag: WDNF
        new TdfMemberInfo("Wins", "wins", 0xDE9BB300, TdfType.UInt32, 22, true), // Tag: WINS
    ];
    private ITdfMember[] __members;

    private TdfUInt32 _customDnf = new(__typeInfos[0]);
    private TdfVariable _customPlayerReport = new(__typeInfos[1]);
    private TdfUInt32 _clientScore = new(__typeInfos[2]);
    private TdfUInt16 _accountCountry = new(__typeInfos[3]);
    private TdfUInt32 _finishReason = new(__typeInfos[4]);
    private TdfUInt32 _gameResult = new(__typeInfos[5]);
    private TdfBool _home = new(__typeInfos[6]);
    private TdfUInt32 _losses = new(__typeInfos[7]);
    private TdfString _name = new(__typeInfos[8]);
    private TdfUInt32 _opponentCount = new(__typeInfos[9]);
    private TdfStruct<OSDKPrivatePlayerReport?> _privatePlayerReport = new(__typeInfos[10]);
    private TdfUInt64 _externalId = new(__typeInfos[11]);
    private TdfUInt64 _nucleusId = new(__typeInfos[12]);
    private TdfString _persona = new(__typeInfos[13]);
    private TdfUInt32 _pointsAgainst = new(__typeInfos[14]);
    private TdfUInt32 _userResult = new(__typeInfos[15]);
    private TdfUInt32 _score = new(__typeInfos[16]);
    private TdfUInt32 _skill = new(__typeInfos[17]);
    private TdfUInt32 _skillPoints = new(__typeInfos[18]);
    private TdfUInt32 _team = new(__typeInfos[19]);
    private TdfUInt32 _ties = new(__typeInfos[20]);
    private TdfUInt32 _winnerByDnf = new(__typeInfos[21]);
    private TdfUInt32 _wins = new(__typeInfos[22]);

    public OSDKPlayerReport()
    {
        __members = [_customDnf, _customPlayerReport, _clientScore, _accountCountry, _finishReason, _gameResult, _home, _losses, _name, _opponentCount, _privatePlayerReport, _externalId, _nucleusId, _persona, _pointsAgainst, _userResult, _score, _skill, _skillPoints, _team, _ties, _winnerByDnf, _wins];
    }

    public override Tdf CreateNew() => new OSDKPlayerReport();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "OSDKPlayerReport";
    public override string GetFullClassName() => "Blaze::GameReporting::OSDKGameReportBase::OSDKPlayerReport";

    public uint CustomDnf { get => _customDnf.Value; set => _customDnf.Value = value; }
    public object? CustomPlayerReport { get => _customPlayerReport.Value; set => _customPlayerReport.Value = value; }
    public uint ClientScore { get => _clientScore.Value; set => _clientScore.Value = value; }
    public ushort AccountCountry { get => _accountCountry.Value; set => _accountCountry.Value = value; }
    public uint FinishReason { get => _finishReason.Value; set => _finishReason.Value = value; }
    public uint GameResult { get => _gameResult.Value; set => _gameResult.Value = value; }
    public bool Home { get => _home.Value; set => _home.Value = value; }
    public uint Losses { get => _losses.Value; set => _losses.Value = value; }
    public string Name { get => _name.Value; set => _name.Value = value; }
    public uint OpponentCount { get => _opponentCount.Value; set => _opponentCount.Value = value; }
    public OSDKPrivatePlayerReport? PrivatePlayerReport { get => _privatePlayerReport.Value; set => _privatePlayerReport.Value = value; }
    public ulong ExternalId { get => _externalId.Value; set => _externalId.Value = value; }
    public ulong NucleusId { get => _nucleusId.Value; set => _nucleusId.Value = value; }
    public string Persona { get => _persona.Value; set => _persona.Value = value; }
    public uint PointsAgainst { get => _pointsAgainst.Value; set => _pointsAgainst.Value = value; }
    public uint UserResult { get => _userResult.Value; set => _userResult.Value = value; }
    public uint Score { get => _score.Value; set => _score.Value = value; }
    public uint Skill { get => _skill.Value; set => _skill.Value = value; }
    public uint SkillPoints { get => _skillPoints.Value; set => _skillPoints.Value = value; }
    public uint Team { get => _team.Value; set => _team.Value = value; }
    public uint Ties { get => _ties.Value; set => _ties.Value = value; }
    public uint WinnerByDnf { get => _winnerByDnf.Value; set => _winnerByDnf.Value = value; }
    public uint Wins { get => _wins.Value; set => _wins.Value = value; }
}
