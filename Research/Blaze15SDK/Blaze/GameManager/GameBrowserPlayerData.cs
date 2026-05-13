using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class GameBrowserPlayerData : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("PlatformInfo", "mPlatformInfo", 0x86993300, TdfType.Struct, 0, true), // Tag: AIDS
        new TdfMemberInfo("AccountCountry", "mAccountCountry", 0x8EED3900, TdfType.UInt32, 1, true), // Tag: CNTY
        new TdfMemberInfo("EncryptedBlazeId", "mEncryptedBlazeId", 0x96EA6400, TdfType.String, 2, true), // Tag: ENID
        new TdfMemberInfo("ExternalId", "mExternalId", 0x978A6400, TdfType.UInt64, 3, true), // Tag: EXID
        new TdfMemberInfo("JoinedGameTimestamp", "mJoinedGameTimestamp", 0xAA7D3300, TdfType.Int64, 4, true), // Tag: JGTS
        new TdfMemberInfo("AccountLocale", "mAccountLocale", 0xB2F8C000, TdfType.UInt32, 5, true), // Tag: LOC
        new TdfMemberInfo("PlayerName", "mPlayerName", 0xBA1B6500, TdfType.String, 6, true), // Tag: NAME
        new TdfMemberInfo("PlayerNamespace", "mPlayerNamespace", 0xBA1CF000, TdfType.String, 7, true), // Tag: NASP
        new TdfMemberInfo("PlayerAttribs", "mPlayerAttribs", 0xC21D3400, TdfType.Map, 8, true), // Tag: PATT
        new TdfMemberInfo("PlayerId", "mPlayerId", 0xC2990000, TdfType.Int64, 9, true), // Tag: PID
        new TdfMemberInfo("ReservationCreationTimestamp", "mReservationCreationTimestamp", 0xCA3D3300, TdfType.Int64, 10, true), // Tag: RCTS
        new TdfMemberInfo("RoleName", "mRoleName", 0xCAFB2500, TdfType.String, 11, true), // Tag: ROLE
        new TdfMemberInfo("PlayerState", "mPlayerState", 0xCF487400, TdfType.Enum, 12, true), // Tag: STAT
        new TdfMemberInfo("SlotType", "mSlotType", 0xCF4E7000, TdfType.Enum, 13, true), // Tag: STYP
        new TdfMemberInfo("TeamIndex", "mTeamIndex", 0xD2993800, TdfType.UInt16, 14, true), // Tag: TIDX
    ];
    private ITdfMember[] __members;

    private TdfStruct<Blaze15SDK.Blaze.PlatformInfo?> _platformInfo = new(__typeInfos[0]);
    private TdfUInt32 _accountCountry = new(__typeInfos[1]);
    private TdfString _encryptedBlazeId = new(__typeInfos[2]);
    private TdfUInt64 _externalId = new(__typeInfos[3]);
    private TdfInt64 _joinedGameTimestamp = new(__typeInfos[4]);
    private TdfUInt32 _accountLocale = new(__typeInfos[5]);
    private TdfString _playerName = new(__typeInfos[6]);
    private TdfString _playerNamespace = new(__typeInfos[7]);
    private TdfMap<string, string> _playerAttribs = new(__typeInfos[8]);
    private TdfInt64 _playerId = new(__typeInfos[9]);
    private TdfInt64 _reservationCreationTimestamp = new(__typeInfos[10]);
    private TdfString _roleName = new(__typeInfos[11]);
    private TdfEnum<Blaze15SDK.Blaze.GameManager.PlayerState> _playerState = new(__typeInfos[12]);
    private TdfEnum<Blaze15SDK.Blaze.GameManager.SlotType> _slotType = new(__typeInfos[13]);
    private TdfUInt16 _teamIndex = new(__typeInfos[14]);

    public GameBrowserPlayerData()
    {
        __members = [ _platformInfo, _accountCountry, _encryptedBlazeId, _externalId, _joinedGameTimestamp, _accountLocale, _playerName, _playerNamespace, _playerAttribs, _playerId, _reservationCreationTimestamp, _roleName, _playerState, _slotType, _teamIndex ];
    }

    public override Tdf CreateNew() => new GameBrowserPlayerData();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GameBrowserPlayerData";
    public override string GetFullClassName() => "Blaze::GameManager::GameBrowserPlayerData";

    public Blaze15SDK.Blaze.PlatformInfo? PlatformInfo
    {
        get => _platformInfo.Value;
        set => _platformInfo.Value = value;
    }

    public uint AccountCountry
    {
        get => _accountCountry.Value;
        set => _accountCountry.Value = value;
    }

    public string EncryptedBlazeId
    {
        get => _encryptedBlazeId.Value;
        set => _encryptedBlazeId.Value = value;
    }

    public ulong ExternalId
    {
        get => _externalId.Value;
        set => _externalId.Value = value;
    }

    public long JoinedGameTimestamp
    {
        get => _joinedGameTimestamp.Value;
        set => _joinedGameTimestamp.Value = value;
    }

    public uint AccountLocale
    {
        get => _accountLocale.Value;
        set => _accountLocale.Value = value;
    }

    public string PlayerName
    {
        get => _playerName.Value;
        set => _playerName.Value = value;
    }

    public string PlayerNamespace
    {
        get => _playerNamespace.Value;
        set => _playerNamespace.Value = value;
    }

    public IDictionary<string, string> PlayerAttribs
    {
        get => _playerAttribs.Value;
        set => _playerAttribs.Value = value;
    }

    public long PlayerId
    {
        get => _playerId.Value;
        set => _playerId.Value = value;
    }

    public long ReservationCreationTimestamp
    {
        get => _reservationCreationTimestamp.Value;
        set => _reservationCreationTimestamp.Value = value;
    }

    public string RoleName
    {
        get => _roleName.Value;
        set => _roleName.Value = value;
    }

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

}
