using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class NotifyGamePlayerTeamRoleSlotChange : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("GameId", "mGameId", 0x9E990000, TdfType.UInt64, 0, true), // Tag: GID
        new TdfMemberInfo("PlayerId", "mPlayerId", 0xC2990000, TdfType.Int64, 1, true), // Tag: PID
        new TdfMemberInfo("PlayerRole", "mPlayerRole", 0xCAFB2500, TdfType.String, 2, true), // Tag: ROLE
        new TdfMemberInfo("SlotType", "mSlotType", 0xCECBF400, TdfType.Enum, 3, true), // Tag: SLOT
        new TdfMemberInfo("TeamIndex", "mTeamIndex", 0xD2993800, TdfType.UInt16, 4, true), // Tag: TIDX
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _gameId = new(__typeInfos[0]);
    private TdfInt64 _playerId = new(__typeInfos[1]);
    private TdfString _playerRole = new(__typeInfos[2]);
    private TdfEnum<Blaze15SDK.Blaze.GameManager.SlotType> _slotType = new(__typeInfos[3]);
    private TdfUInt16 _teamIndex = new(__typeInfos[4]);

    public NotifyGamePlayerTeamRoleSlotChange()
    {
        __members = [ _gameId, _playerId, _playerRole, _slotType, _teamIndex ];
    }

    public override Tdf CreateNew() => new NotifyGamePlayerTeamRoleSlotChange();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "NotifyGamePlayerTeamRoleSlotChange";
    public override string GetFullClassName() => "Blaze::GameManager::NotifyGamePlayerTeamRoleSlotChange";

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

    public string PlayerRole
    {
        get => _playerRole.Value;
        set => _playerRole.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.SlotType SlotType
    {
        get => _slotType.Value;
        set => _slotType.Value = value;
    }

    public ushort TeamIndex
    {
        get => _teamIndex.Value;
        set => _teamIndex.Value = value;
    }

}
