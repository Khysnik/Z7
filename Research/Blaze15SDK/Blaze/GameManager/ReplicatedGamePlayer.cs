using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class ReplicatedGamePlayer : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("PlatformInfo", "mPlatformInfo", 0x86993300, TdfType.Struct, 0, true), // Tag: AIDS
        new TdfMemberInfo("CustomData", "mCustomData", 0x8ACBE200, TdfType.Blob, 1, true), // Tag: BLOB
        new TdfMemberInfo("AccountCountry", "mAccountCountry", 0x8EED3900, TdfType.UInt32, 2, true), // Tag: CNTY
        new TdfMemberInfo("ConnectionGroupId", "mConnectionGroupId", 0x8EFBA700, TdfType.UInt64, 3, true), // Tag: CONG
        new TdfMemberInfo("ConnectionSlotId", "mConnectionSlotId", 0x8F3A6400, TdfType.UInt8, 4, true), // Tag: CSID
        new TdfMemberInfo("DirtySockUserIndex", "mDirtySockUserIndex", 0x933D6900, TdfType.Int32, 5, true), // Tag: DSUI
        new TdfMemberInfo("EncryptedBlazeId", "mEncryptedBlazeId", 0x96E8F200, TdfType.String, 6, true), // Tag: ENCR
        new TdfMemberInfo("ExternalId", "mExternalId", 0x978A6400, TdfType.UInt64, 7, true), // Tag: EXID
        new TdfMemberInfo("GameId", "mGameId", 0x9E990000, TdfType.UInt64, 8, true), // Tag: GID
        new TdfMemberInfo("HasJoinFirstPartyGameSessionPermission", "mHasJoinFirstPartyGameSessionPermission", 0xAA6C3300, TdfType.Bool, 9, true), // Tag: JFPS
        new TdfMemberInfo("JoinedViaMatchmaking", "mJoinedViaMatchmaking", 0xAB6B6D00, TdfType.Bool, 10, true), // Tag: JVMM
        new TdfMemberInfo("AccountLocale", "mAccountLocale", 0xB2F8C000, TdfType.UInt32, 11, true), // Tag: LOC
        new TdfMemberInfo("PlayerName", "mPlayerName", 0xBA1B6500, TdfType.String, 12, true), // Tag: NAME
        new TdfMemberInfo("PersonaNamespace", "mPersonaNamespace", 0xBA1CF000, TdfType.String, 13, true), // Tag: NASP
        new TdfMemberInfo("PlayerAttribs", "mPlayerAttribs", 0xC21D3400, TdfType.Map, 14, true), // Tag: PATT
        new TdfMemberInfo("PlayerId", "mPlayerId", 0xC2990000, TdfType.Int64, 15, true), // Tag: PID
        new TdfMemberInfo("NetworkAddress", "mNetworkAddress", 0xC2E97400, TdfType.Union, 16, true), // Tag: PNET
        new TdfMemberInfo("PlayerSettings", "mPlayerSettings", 0xC3397400, TdfType.Enum, 17, true), // Tag: PSET
        new TdfMemberInfo("ReservationCreationTimestamp", "mReservationCreationTimestamp", 0xCA3CA500, TdfType.Int64, 18, true), // Tag: RCRE
        new TdfMemberInfo("RoleName", "mRoleName", 0xCAFB2500, TdfType.String, 19, true), // Tag: ROLE
        new TdfMemberInfo("ScenarioName", "mScenarioName", 0xCE396E00, TdfType.String, 20, true), // Tag: SCEN
        new TdfMemberInfo("SlotId", "mSlotId", 0xCE990000, TdfType.UInt8, 21, true), // Tag: SID
        new TdfMemberInfo("SlotType", "mSlotType", 0xCECBF400, TdfType.Enum, 22, true), // Tag: SLOT
        new TdfMemberInfo("PlayerState", "mPlayerState", 0xCF487400, TdfType.Enum, 23, true), // Tag: STAT
        new TdfMemberInfo("TeamIndex", "mTeamIndex", 0xD2993800, TdfType.UInt16, 24, true), // Tag: TIDX
        new TdfMemberInfo("JoinedGameTimestamp", "mJoinedGameTimestamp", 0xD29B6500, TdfType.Int64, 25, true), // Tag: TIME
        new TdfMemberInfo("UserGroupId", "mUserGroupId", 0xD67A6400, TdfType.ObjectId, 26, true), // Tag: UGID
        new TdfMemberInfo("PlayerSessionId", "mPlayerSessionId", 0xD6990000, TdfType.UInt64, 27, true), // Tag: UID
        new TdfMemberInfo("UUID", "mUUID", 0xD75A6400, TdfType.String, 28, true), // Tag: UUID
    ];
    private ITdfMember[] __members;

    private TdfStruct<Blaze15SDK.Blaze.PlatformInfo?> _platformInfo = new(__typeInfos[0]);
    private TdfBlob _customData = new(__typeInfos[1]);
    private TdfUInt32 _accountCountry = new(__typeInfos[2]);
    private TdfUInt64 _connectionGroupId = new(__typeInfos[3]);
    private TdfUInt8 _connectionSlotId = new(__typeInfos[4]);
    private TdfInt32 _dirtySockUserIndex = new(__typeInfos[5]);
    private TdfString _encryptedBlazeId = new(__typeInfos[6]);
    private TdfUInt64 _externalId = new(__typeInfos[7]);
    private TdfUInt64 _gameId = new(__typeInfos[8]);
    private TdfBool _hasJoinFirstPartyGameSessionPermission = new(__typeInfos[9]);
    private TdfBool _joinedViaMatchmaking = new(__typeInfos[10]);
    private TdfUInt32 _accountLocale = new(__typeInfos[11]);
    private TdfString _playerName = new(__typeInfos[12]);
    private TdfString _personaNamespace = new(__typeInfos[13]);
    private TdfMap<string, string> _playerAttribs = new(__typeInfos[14]);
    private TdfInt64 _playerId = new(__typeInfos[15]);
    private TdfUnion<Blaze15SDK.Blaze.NetworkAddress> _networkAddress = new(__typeInfos[16]);
    private TdfEnum<Blaze15SDK.Blaze.GameManager.PlayerSettings> _playerSettings = new(__typeInfos[17]);
    private TdfInt64 _reservationCreationTimestamp = new(__typeInfos[18]);
    private TdfString _roleName = new(__typeInfos[19]);
    private TdfString _scenarioName = new(__typeInfos[20]);
    private TdfUInt8 _slotId = new(__typeInfos[21]);
    private TdfEnum<Blaze15SDK.Blaze.GameManager.SlotType> _slotType = new(__typeInfos[22]);
    private TdfEnum<Blaze15SDK.Blaze.GameManager.PlayerState> _playerState = new(__typeInfos[23]);
    private TdfUInt16 _teamIndex = new(__typeInfos[24]);
    private TdfInt64 _joinedGameTimestamp = new(__typeInfos[25]);
    private TdfObjectId _userGroupId = new(__typeInfos[26]);
    private TdfUInt64 _playerSessionId = new(__typeInfos[27]);
    private TdfString _uUID = new(__typeInfos[28]);

    public ReplicatedGamePlayer()
    {
        __members = [ _platformInfo, _customData, _accountCountry, _connectionGroupId, _connectionSlotId, _dirtySockUserIndex, _encryptedBlazeId, _externalId, _gameId, _hasJoinFirstPartyGameSessionPermission, _joinedViaMatchmaking, _accountLocale, _playerName, _personaNamespace, _playerAttribs, _playerId, _networkAddress, _playerSettings, _reservationCreationTimestamp, _roleName, _scenarioName, _slotId, _slotType, _playerState, _teamIndex, _joinedGameTimestamp, _userGroupId, _playerSessionId, _uUID ];
    }

    public override Tdf CreateNew() => new ReplicatedGamePlayer();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ReplicatedGamePlayer";
    public override string GetFullClassName() => "Blaze::GameManager::ReplicatedGamePlayer";

    public Blaze15SDK.Blaze.PlatformInfo? PlatformInfo
    {
        get => _platformInfo.Value;
        set => _platformInfo.Value = value;
    }

    public byte[] CustomData
    {
        get => _customData.Value;
        set => _customData.Value = value;
    }

    public uint AccountCountry
    {
        get => _accountCountry.Value;
        set => _accountCountry.Value = value;
    }

    public ulong ConnectionGroupId
    {
        get => _connectionGroupId.Value;
        set => _connectionGroupId.Value = value;
    }

    public byte ConnectionSlotId
    {
        get => _connectionSlotId.Value;
        set => _connectionSlotId.Value = value;
    }

    public int DirtySockUserIndex
    {
        get => _dirtySockUserIndex.Value;
        set => _dirtySockUserIndex.Value = value;
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

    public ulong GameId
    {
        get => _gameId.Value;
        set => _gameId.Value = value;
    }

    public bool HasJoinFirstPartyGameSessionPermission
    {
        get => _hasJoinFirstPartyGameSessionPermission.Value;
        set => _hasJoinFirstPartyGameSessionPermission.Value = value;
    }

    public bool JoinedViaMatchmaking
    {
        get => _joinedViaMatchmaking.Value;
        set => _joinedViaMatchmaking.Value = value;
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

    public string PersonaNamespace
    {
        get => _personaNamespace.Value;
        set => _personaNamespace.Value = value;
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

    public Blaze15SDK.Blaze.NetworkAddress NetworkAddress
    {
        get => _networkAddress.Value;
        set => _networkAddress.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.PlayerSettings PlayerSettings
    {
        get => _playerSettings.Value;
        set => _playerSettings.Value = value;
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

    public string ScenarioName
    {
        get => _scenarioName.Value;
        set => _scenarioName.Value = value;
    }

    public byte SlotId
    {
        get => _slotId.Value;
        set => _slotId.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.SlotType SlotType
    {
        get => _slotType.Value;
        set => _slotType.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.PlayerState PlayerState
    {
        get => _playerState.Value;
        set => _playerState.Value = value;
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

    public ObjectId UserGroupId
    {
        get => _userGroupId.Value;
        set => _userGroupId.Value = value;
    }

    public ulong PlayerSessionId
    {
        get => _playerSessionId.Value;
        set => _playerSessionId.Value = value;
    }

    public string UUID
    {
        get => _uUID.Value;
        set => _uUID.Value = value;
    }

}
