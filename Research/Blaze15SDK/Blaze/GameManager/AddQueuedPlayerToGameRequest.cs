using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class AddQueuedPlayerToGameRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("GameId", "mGameId", 0x9E990000, TdfType.UInt64, 0, true), // Tag: GID
        new TdfMemberInfo("OverridePlayerRole", "mOverridePlayerRole", 0xBF6CA400, TdfType.Bool, 1, true), // Tag: OVRD
        new TdfMemberInfo("PlayerId", "mPlayerId", 0xC2990000, TdfType.Int64, 2, true), // Tag: PID
        new TdfMemberInfo("PlayerRole", "mPlayerRole", 0xCAFB2500, TdfType.String, 3, true), // Tag: ROLE
        new TdfMemberInfo("PlayerTeamIndex", "mPlayerTeamIndex", 0xD2993800, TdfType.UInt16, 4, true), // Tag: TIDX
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _gameId = new(__typeInfos[0]);
    private TdfBool _overridePlayerRole = new(__typeInfos[1]);
    private TdfInt64 _playerId = new(__typeInfos[2]);
    private TdfString _playerRole = new(__typeInfos[3]);
    private TdfUInt16 _playerTeamIndex = new(__typeInfos[4]);

    public AddQueuedPlayerToGameRequest()
    {
        __members = [ _gameId, _overridePlayerRole, _playerId, _playerRole, _playerTeamIndex ];
    }

    public override Tdf CreateNew() => new AddQueuedPlayerToGameRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "AddQueuedPlayerToGameRequest";
    public override string GetFullClassName() => "Blaze::GameManager::AddQueuedPlayerToGameRequest";

    public ulong GameId
    {
        get => _gameId.Value;
        set => _gameId.Value = value;
    }

    public bool OverridePlayerRole
    {
        get => _overridePlayerRole.Value;
        set => _overridePlayerRole.Value = value;
    }

    public long PlayerId
    {
        get => _playerId.Value;
        set => _playerId.Value = value;
    }

    public string PlayerRole
    {
        get => _playerRole.Value;
        set => _playerRole.Value = value;
    }

    public ushort PlayerTeamIndex
    {
        get => _playerTeamIndex.Value;
        set => _playerTeamIndex.Value = value;
    }

}
