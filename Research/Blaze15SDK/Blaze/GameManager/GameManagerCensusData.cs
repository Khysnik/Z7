using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class GameManagerCensusData : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("NumOfActiveGame", "mNumOfActiveGame", 0x867B8000, TdfType.UInt32, 0, true), // Tag: AGN
        new TdfMemberInfo("NumOfActiveGamePerPingSite", "mNumOfActiveGamePerPingSite", 0x867C3300, TdfType.Map, 1, true), // Tag: AGPS
        new TdfMemberInfo("GameAttributesData", "mGameAttributesData", 0x9E18E400, TdfType.List, 2, true), // Tag: GACD
        new TdfMemberInfo("NumOfGameGroup", "mNumOfGameGroup", 0x9E7B8000, TdfType.UInt32, 3, true), // Tag: GGN
        new TdfMemberInfo("NumOfJoinedPlayer", "mNumOfJoinedPlayer", 0xAB0B8000, TdfType.UInt32, 4, true), // Tag: JPN
        new TdfMemberInfo("NumOfJoinedPlayerPerPingSite", "mNumOfJoinedPlayerPerPingSite", 0xAB0C3300, TdfType.Map, 5, true), // Tag: JPPS
        new TdfMemberInfo("NumOfLoggedSession", "mNumOfLoggedSession", 0xB33B8000, TdfType.UInt32, 6, true), // Tag: LSN
        new TdfMemberInfo("MatchmakingCensusData", "mMatchmakingCensusData", 0xB6D8E400, TdfType.Struct, 7, true), // Tag: MMCD
        new TdfMemberInfo("NumOfMatchmakingSession", "mNumOfMatchmakingSession", 0xB6DCEE00, TdfType.UInt32, 8, true), // Tag: MMSN
        new TdfMemberInfo("NumOfMatchmakingUsers", "mNumOfMatchmakingUsers", 0xBB5B6D00, TdfType.UInt32, 9, true), // Tag: NUMM
        new TdfMemberInfo("NumOfPlayersInGameGroup", "mNumOfPlayersInGameGroup", 0xC299E700, TdfType.UInt32, 10, true), // Tag: PIGG
    ];
    private ITdfMember[] __members;

    private TdfUInt32 _numOfActiveGame = new(__typeInfos[0]);
    private TdfMap<string, uint> _numOfActiveGamePerPingSite = new(__typeInfos[1]);
    private TdfList<Blaze15SDK.Blaze.GameManager.GameAttributeCensusData> _gameAttributesData = new(__typeInfos[2]);
    private TdfUInt32 _numOfGameGroup = new(__typeInfos[3]);
    private TdfUInt32 _numOfJoinedPlayer = new(__typeInfos[4]);
    private TdfMap<string, uint> _numOfJoinedPlayerPerPingSite = new(__typeInfos[5]);
    private TdfUInt32 _numOfLoggedSession = new(__typeInfos[6]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.MatchmakingCensusData?> _matchmakingCensusData = new(__typeInfos[7]);
    private TdfUInt32 _numOfMatchmakingSession = new(__typeInfos[8]);
    private TdfUInt32 _numOfMatchmakingUsers = new(__typeInfos[9]);
    private TdfUInt32 _numOfPlayersInGameGroup = new(__typeInfos[10]);

    public GameManagerCensusData()
    {
        __members = [ _numOfActiveGame, _numOfActiveGamePerPingSite, _gameAttributesData, _numOfGameGroup, _numOfJoinedPlayer, _numOfJoinedPlayerPerPingSite, _numOfLoggedSession, _matchmakingCensusData, _numOfMatchmakingSession, _numOfMatchmakingUsers, _numOfPlayersInGameGroup ];
    }

    public override Tdf CreateNew() => new GameManagerCensusData();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GameManagerCensusData";
    public override string GetFullClassName() => "Blaze::GameManager::GameManagerCensusData";

    public uint NumOfActiveGame
    {
        get => _numOfActiveGame.Value;
        set => _numOfActiveGame.Value = value;
    }

    public IDictionary<string, uint> NumOfActiveGamePerPingSite
    {
        get => _numOfActiveGamePerPingSite.Value;
        set => _numOfActiveGamePerPingSite.Value = value;
    }

    public IList<Blaze15SDK.Blaze.GameManager.GameAttributeCensusData> GameAttributesData
    {
        get => _gameAttributesData.Value;
        set => _gameAttributesData.Value = value;
    }

    public uint NumOfGameGroup
    {
        get => _numOfGameGroup.Value;
        set => _numOfGameGroup.Value = value;
    }

    public uint NumOfJoinedPlayer
    {
        get => _numOfJoinedPlayer.Value;
        set => _numOfJoinedPlayer.Value = value;
    }

    public IDictionary<string, uint> NumOfJoinedPlayerPerPingSite
    {
        get => _numOfJoinedPlayerPerPingSite.Value;
        set => _numOfJoinedPlayerPerPingSite.Value = value;
    }

    public uint NumOfLoggedSession
    {
        get => _numOfLoggedSession.Value;
        set => _numOfLoggedSession.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.MatchmakingCensusData? MatchmakingCensusData
    {
        get => _matchmakingCensusData.Value;
        set => _matchmakingCensusData.Value = value;
    }

    public uint NumOfMatchmakingSession
    {
        get => _numOfMatchmakingSession.Value;
        set => _numOfMatchmakingSession.Value = value;
    }

    public uint NumOfMatchmakingUsers
    {
        get => _numOfMatchmakingUsers.Value;
        set => _numOfMatchmakingUsers.Value = value;
    }

    public uint NumOfPlayersInGameGroup
    {
        get => _numOfPlayersInGameGroup.Value;
        set => _numOfPlayersInGameGroup.Value = value;
    }

}
