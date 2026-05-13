using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class GetTournamentGameStatusResult : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("GameId", "mGameId", 0x9E990000, TdfType.UInt64, 0, true), // Tag: GID
        new TdfMemberInfo("GameName", "mGameName", 0x9EE86D00, TdfType.String, 1, true), // Tag: GNAM
        new TdfMemberInfo("GameState", "mGameState", 0x9F3D2100, TdfType.Enum, 2, true), // Tag: GSTA
        new TdfMemberInfo("GameRoster", "mGameRoster", 0xCAFCF400, TdfType.List, 3, true), // Tag: ROST
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _gameId = new(__typeInfos[0]);
    private TdfString _gameName = new(__typeInfos[1]);
    private TdfEnum<Blaze15SDK.Blaze.GameManager.GameState> _gameState = new(__typeInfos[2]);
    private TdfList<Blaze15SDK.Blaze.GameManager.GameBrowserPlayerData> _gameRoster = new(__typeInfos[3]);

    public GetTournamentGameStatusResult()
    {
        __members = [ _gameId, _gameName, _gameState, _gameRoster ];
    }

    public override Tdf CreateNew() => new GetTournamentGameStatusResult();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetTournamentGameStatusResult";
    public override string GetFullClassName() => "Blaze::GameManager::GetTournamentGameStatusResult";

    public ulong GameId
    {
        get => _gameId.Value;
        set => _gameId.Value = value;
    }

    public string GameName
    {
        get => _gameName.Value;
        set => _gameName.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.GameState GameState
    {
        get => _gameState.Value;
        set => _gameState.Value = value;
    }

    public IList<Blaze15SDK.Blaze.GameManager.GameBrowserPlayerData> GameRoster
    {
        get => _gameRoster.Value;
        set => _gameRoster.Value = value;
    }

}
