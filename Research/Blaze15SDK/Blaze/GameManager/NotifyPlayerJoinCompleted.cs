using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class NotifyPlayerJoinCompleted : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("GameId", "mGameId", 0x9E990000, TdfType.UInt64, 0, true), // Tag: GID
        new TdfMemberInfo("PlayerId", "mPlayerId", 0xC2990000, TdfType.Int64, 1, true), // Tag: PID
        new TdfMemberInfo("JoinedGameTimestamp", "mJoinedGameTimestamp", 0xD29B6500, TdfType.Int64, 2, true), // Tag: TIME
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _gameId = new(__typeInfos[0]);
    private TdfInt64 _playerId = new(__typeInfos[1]);
    private TdfInt64 _joinedGameTimestamp = new(__typeInfos[2]);

    public NotifyPlayerJoinCompleted()
    {
        __members = [ _gameId, _playerId, _joinedGameTimestamp ];
    }

    public override Tdf CreateNew() => new NotifyPlayerJoinCompleted();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "NotifyPlayerJoinCompleted";
    public override string GetFullClassName() => "Blaze::GameManager::NotifyPlayerJoinCompleted";

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

    public long JoinedGameTimestamp
    {
        get => _joinedGameTimestamp.Value;
        set => _joinedGameTimestamp.Value = value;
    }

}
