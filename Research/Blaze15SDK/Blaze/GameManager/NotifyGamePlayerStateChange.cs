using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class NotifyGamePlayerStateChange : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("GameId", "mGameId", 0x9E990000, TdfType.UInt64, 0, true), // Tag: GID
        new TdfMemberInfo("PlayerId", "mPlayerId", 0xC2990000, TdfType.Int64, 1, true), // Tag: PID
        new TdfMemberInfo("PlayerState", "mPlayerState", 0xCF487400, TdfType.Enum, 2, true), // Tag: STAT
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _gameId = new(__typeInfos[0]);
    private TdfInt64 _playerId = new(__typeInfos[1]);
    private TdfEnum<Blaze15SDK.Blaze.GameManager.PlayerState> _playerState = new(__typeInfos[2]);

    public NotifyGamePlayerStateChange()
    {
        __members = [ _gameId, _playerId, _playerState ];
    }

    public override Tdf CreateNew() => new NotifyGamePlayerStateChange();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "NotifyGamePlayerStateChange";
    public override string GetFullClassName() => "Blaze::GameManager::NotifyGamePlayerStateChange";

    public ulong GameId
    {
        get => _gameId.Value;
        set => _gameId.Value = value;
    }

    public long PlayerId
    {
        get => _playerId.Value;
        set => _playerId.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.PlayerState PlayerState
    {
        get => _playerState.Value;
        set => _playerState.Value = value;
    }

}
