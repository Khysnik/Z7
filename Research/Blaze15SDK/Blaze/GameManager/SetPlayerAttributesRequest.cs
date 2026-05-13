using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class SetPlayerAttributesRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("PlayerAttributes", "mPlayerAttributes", 0x874D3200, TdfType.Map, 0, true), // Tag: ATTR
        new TdfMemberInfo("GameId", "mGameId", 0x9E990000, TdfType.UInt64, 1, true), // Tag: GID
        new TdfMemberInfo("PlayerId", "mPlayerId", 0xC2990000, TdfType.Int64, 2, true), // Tag: PID
    ];
    private ITdfMember[] __members;

    private TdfMap<string, string> _playerAttributes = new(__typeInfos[0]);
    private TdfUInt64 _gameId = new(__typeInfos[1]);
    private TdfInt64 _playerId = new(__typeInfos[2]);

    public SetPlayerAttributesRequest()
    {
        __members = [ _playerAttributes, _gameId, _playerId ];
    }

    public override Tdf CreateNew() => new SetPlayerAttributesRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "SetPlayerAttributesRequest";
    public override string GetFullClassName() => "Blaze::GameManager::SetPlayerAttributesRequest";

    public IDictionary<string, string> PlayerAttributes
    {
        get => _playerAttributes.Value;
        set => _playerAttributes.Value = value;
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
