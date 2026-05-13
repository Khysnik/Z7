using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class PlayerPresenceData : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("PlayerState", "mPlayerState", 0xC33D2100, TdfType.Enum, 0, true), // Tag: PSTA
        new TdfMemberInfo("SlotType", "mSlotType", 0xCECBF400, TdfType.Enum, 1, true), // Tag: SLOT
        new TdfMemberInfo("TeamIndex", "mTeamIndex", 0xD2586D00, TdfType.UInt16, 2, true), // Tag: TEAM
        new TdfMemberInfo("JoinedGameTimestamp", "mJoinedGameTimestamp", 0xD29B6500, TdfType.Int64, 3, true), // Tag: TIME
    ];
    private ITdfMember[] __members;

    private TdfEnum<Blaze15SDK.Blaze.GameManager.PlayerState> _playerState = new(__typeInfos[0]);
    private TdfEnum<Blaze15SDK.Blaze.GameManager.SlotType> _slotType = new(__typeInfos[1]);
    private TdfUInt16 _teamIndex = new(__typeInfos[2]);
    private TdfInt64 _joinedGameTimestamp = new(__typeInfos[3]);

    public PlayerPresenceData()
    {
        __members = [ _playerState, _slotType, _teamIndex, _joinedGameTimestamp ];
    }

    public override Tdf CreateNew() => new PlayerPresenceData();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "PlayerPresenceData";
    public override string GetFullClassName() => "Blaze::GameManager::PlayerPresenceData";

    public Blaze15SDK.Blaze.GameManager.PlayerState PlayerState
    {
        get => _playerState.Value;
        set => _playerState.Value = value;
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

    public long JoinedGameTimestamp
    {
        get => _joinedGameTimestamp.Value;
        set => _joinedGameTimestamp.Value = value;
    }

}
