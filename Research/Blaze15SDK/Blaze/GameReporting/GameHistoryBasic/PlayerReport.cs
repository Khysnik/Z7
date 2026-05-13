using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting.GameHistoryBasic;

public class PlayerReport : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Deaths", "deaths", 0x925D2800, TdfType.UInt16, 0, true), // Tag: DETH
        new TdfMemberInfo("GlickoRd", "glickoRd", 0x9ECCA400, TdfType.Int32, 1, true), // Tag: GLRD
        new TdfMemberInfo("GlickoSkillPoints", "glickoSkillPoints", 0x9F3C3400, TdfType.Int32, 2, true), // Tag: GSPT
        new TdfMemberInfo("Kills", "kills", 0xAE9B2C00, TdfType.UInt16, 3, true), // Tag: KILL
        new TdfMemberInfo("Loser", "loser", 0xB2FCF200, TdfType.Bool, 4, true), // Tag: LOSR
        new TdfMemberInfo("LobbySkillPoints", "lobbySkillPoints", 0xB33C3400, TdfType.Int32, 5, true), // Tag: LSPT
        new TdfMemberInfo("Money", "money", 0xB6FBB900, TdfType.UInt32, 6, true), // Tag: MONY
        new TdfMemberInfo("PlayerClass", "playerClass", 0xC23B3300, TdfType.Int32, 7, true), // Tag: PCLS
        new TdfMemberInfo("Rank", "rank", 0xCA1BAB00, TdfType.Int32, 8, true), // Tag: RANK
        new TdfMemberInfo("LongestTimeAlive", "longestTimeAlive", 0xD21B3600, TdfType.UInt32, 9, true), // Tag: TALV
        new TdfMemberInfo("WinnerByDNF", "winnerByDNF", 0xDE4BA600, TdfType.Bool, 10, true), // Tag: WDNF
        new TdfMemberInfo("Winner", "winner", 0xDE9BB200, TdfType.Bool, 11, true), // Tag: WINR
    ];
    private ITdfMember[] __members;

    private TdfUInt16 _deaths = new(__typeInfos[0]);
    private TdfInt32 _glickoRd = new(__typeInfos[1]);
    private TdfInt32 _glickoSkillPoints = new(__typeInfos[2]);
    private TdfUInt16 _kills = new(__typeInfos[3]);
    private TdfBool _loser = new(__typeInfos[4]);
    private TdfInt32 _lobbySkillPoints = new(__typeInfos[5]);
    private TdfUInt32 _money = new(__typeInfos[6]);
    private TdfInt32 _playerClass = new(__typeInfos[7]);
    private TdfInt32 _rank = new(__typeInfos[8]);
    private TdfUInt32 _longestTimeAlive = new(__typeInfos[9]);
    private TdfBool _winnerByDNF = new(__typeInfos[10]);
    private TdfBool _winner = new(__typeInfos[11]);

    public PlayerReport()
    {
        __members = [ _deaths, _glickoRd, _glickoSkillPoints, _kills, _loser, _lobbySkillPoints, _money, _playerClass, _rank, _longestTimeAlive, _winnerByDNF, _winner ];
    }

    public override Tdf CreateNew() => new PlayerReport();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "PlayerReport";
    public override string GetFullClassName() => "Blaze::GameReporting::GameHistoryBasic::PlayerReport";

    public ushort Deaths
    {
        get => _deaths.Value;
        set => _deaths.Value = value;
    }

    public int GlickoRd
    {
        get => _glickoRd.Value;
        set => _glickoRd.Value = value;
    }

    public int GlickoSkillPoints
    {
        get => _glickoSkillPoints.Value;
        set => _glickoSkillPoints.Value = value;
    }

    public ushort Kills
    {
        get => _kills.Value;
        set => _kills.Value = value;
    }

    public bool Loser
    {
        get => _loser.Value;
        set => _loser.Value = value;
    }

    public int LobbySkillPoints
    {
        get => _lobbySkillPoints.Value;
        set => _lobbySkillPoints.Value = value;
    }

    public uint Money
    {
        get => _money.Value;
        set => _money.Value = value;
    }

    public int PlayerClass
    {
        get => _playerClass.Value;
        set => _playerClass.Value = value;
    }

    public int Rank
    {
        get => _rank.Value;
        set => _rank.Value = value;
    }

    public uint LongestTimeAlive
    {
        get => _longestTimeAlive.Value;
        set => _longestTimeAlive.Value = value;
    }

    public bool WinnerByDNF
    {
        get => _winnerByDNF.Value;
        set => _winnerByDNF.Value = value;
    }

    public bool Winner
    {
        get => _winner.Value;
        set => _winner.Value = value;
    }

}
