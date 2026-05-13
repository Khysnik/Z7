using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class NotifyPlayerAttribChange : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("PlayerAttribs", "mPlayerAttribs", 0x874D3200, TdfType.Map, 0, true), // Tag: ATTR
        new TdfMemberInfo("GameId", "mGameId", 0x9E990000, TdfType.UInt64, 1, true), // Tag: GID
        new TdfMemberInfo("PlayerId", "mPlayerId", 0xC2990000, TdfType.Int64, 2, true), // Tag: PID
    ];
    private ITdfMember[] __members;

    private TdfMap<string, string> _playerAttribs = new(__typeInfos[0]);
    private TdfUInt64 _gameId = new(__typeInfos[1]);
    private TdfInt64 _playerId = new(__typeInfos[2]);

    public NotifyPlayerAttribChange()
    {
        __members = [ _playerAttribs, _gameId, _playerId ];
    }

    public override Tdf CreateNew() => new NotifyPlayerAttribChange();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "NotifyPlayerAttribChange";
    public override string GetFullClassName() => "Blaze::GameManager::NotifyPlayerAttribChange";

    public IDictionary<string, string> PlayerAttribs
    {
        get => _playerAttribs.Value;
        set => _playerAttribs.Value = value;
    }

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

}
