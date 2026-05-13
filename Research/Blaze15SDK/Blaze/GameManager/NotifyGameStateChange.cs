using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class NotifyGameStateChange : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("GameId", "mGameId", 0x9E990000, TdfType.UInt64, 0, true), // Tag: GID
        new TdfMemberInfo("NewGameState", "mNewGameState", 0x9F3D2100, TdfType.Enum, 1, true), // Tag: GSTA
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _gameId = new(__typeInfos[0]);
    private TdfEnum<Blaze15SDK.Blaze.GameManager.GameState> _newGameState = new(__typeInfos[1]);

    public NotifyGameStateChange()
    {
        __members = [ _gameId, _newGameState ];
    }

    public override Tdf CreateNew() => new NotifyGameStateChange();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "NotifyGameStateChange";
    public override string GetFullClassName() => "Blaze::GameManager::NotifyGameStateChange";

    public ulong GameId
    {
        get => _gameId.Value;
        set => _gameId.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.GameState NewGameState
    {
        get => _newGameState.Value;
        set => _newGameState.Value = value;
    }

}
