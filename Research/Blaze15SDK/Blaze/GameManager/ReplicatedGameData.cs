using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class ReplicatedGameData : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("AdminPlayerList", "mAdminPlayerList", 0x864B6E00, TdfType.List, 0, true), // Tag: ADMN
        new TdfMemberInfo("OwnsFirstPartyPresence", "mOwnsFirstPartyPresence", 0x870CB300, TdfType.Bool, 1, true), // Tag: APRS
        new TdfMemberInfo("GameAttribs", "mGameAttribs", 0x874D3200, TdfType.Map, 2, true), // Tag: ATTR
        new TdfMemberInfo("BaseGameClientPlatformList", "mBaseGameClientPlatformList", 0x8A3C2C00, TdfType.List, 3, true), // Tag: BCPL
        new TdfMemberInfo("SlotCapacities", "mSlotCapacities", 0x8E1C0000, TdfType.List, 4, true), // Tag: CAP
        new TdfMemberInfo("CCSMode", "mCCSMode", 0x8E3B6400, TdfType.Enum, 5, true), // Tag: CCMD
        new TdfMemberInfo("ExternalSessionCorrelationId", "mExternalSessionCorrelationId", 0x8EFA6400, TdfType.String, 6, true), // Tag: COID
        new TdfMemberInfo("IsCrossplayEnabled", "mIsCrossplayEnabled", 0x8F0B3900, TdfType.Bool, 7, true), // Tag: CPLY
        new TdfMemberInfo("ClientPlatformList", "mClientPlatformList", 0x8F0BF600, TdfType.List, 8, true), // Tag: CPOV
        new TdfMemberInfo("EntryCriteriaMap", "mEntryCriteriaMap", 0x8F2A7400, TdfType.Map, 9, true), // Tag: CRIT
        new TdfMemberInfo("ConnectionSetId", "mConnectionSetId", 0x8F3A6400, TdfType.String, 10, true), // Tag: CSID
        new TdfMemberInfo("CreateTime", "mCreateTime", 0x8F4A6D00, TdfType.Int64, 11, true), // Tag: CTIM
        new TdfMemberInfo("DedicatedServerHostInfo", "mDedicatedServerHostInfo", 0x928CF400, TdfType.Struct, 12, true), // Tag: DHST
        new TdfMemberInfo("DedicatedServerHostNetworkAddressList", "mDedicatedServerHostNetworkAddressList", 0x92E97400, TdfType.List, 13, true), // Tag: DNET
        new TdfMemberInfo("DisconnectReservationTimeout", "mDisconnectReservationTimeout", 0x932D2F00, TdfType.Int64, 14, true), // Tag: DRTO
        new TdfMemberInfo("DedicatedServerAttribs", "mDedicatedServerAttribs", 0x933D3200, TdfType.Map, 15, true), // Tag: DSTR
        new TdfMemberInfo("ExternalOwnerInfo", "mExternalOwnerInfo", 0x96FDEE00, TdfType.Struct, 16, true), // Tag: EOWN
        new TdfMemberInfo("ExternalSessionIdentification", "mExternalSessionIdentification", 0x973A6400, TdfType.Struct, 17, true), // Tag: ESID
        new TdfMemberInfo("ExternalSessionName", "mExternalSessionName", 0x973BAD00, TdfType.String, 18, true), // Tag: ESNM
        new TdfMemberInfo("GameEventAddress", "mGameEventAddress", 0x9E586400, TdfType.String, 19, true), // Tag: GEAD
        new TdfMemberInfo("GameEndEventUri", "mGameEndEventUri", 0x9E597500, TdfType.String, 20, true), // Tag: GEEU
        new TdfMemberInfo("GameType", "mGameType", 0x9E7D3900, TdfType.Enum, 21, true), // Tag: GGTY
        new TdfMemberInfo("GameId", "mGameId", 0x9E990000, TdfType.UInt64, 22, true), // Tag: GID
        new TdfMemberInfo("GameModRegister", "mGameModRegister", 0x9EDCA700, TdfType.UInt32, 23, true), // Tag: GMRG
        new TdfMemberInfo("GameName", "mGameName", 0x9EE86D00, TdfType.String, 24, true), // Tag: GNAM
        new TdfMemberInfo("GameProtocolVersionHash", "mGameProtocolVersionHash", 0x9F0DA800, TdfType.UInt64, 25, true), // Tag: GPVH
        new TdfMemberInfo("GameSettings", "mGameSettings", 0x9F397400, TdfType.Enum, 26, true), // Tag: GSET
        new TdfMemberInfo("GameStartEventUri", "mGameStartEventUri", 0x9F397500, TdfType.String, 27, true), // Tag: GSEU
        new TdfMemberInfo("GameReportingId", "mGameReportingId", 0x9F3A6400, TdfType.UInt64, 28, true), // Tag: GSID
        new TdfMemberInfo("GameState", "mGameState", 0x9F3D2100, TdfType.Enum, 29, true), // Tag: GSTA
        new TdfMemberInfo("GameReportName", "mGameReportName", 0x9F4E7000, TdfType.String, 30, true), // Tag: GTYP
        new TdfMemberInfo("GameStatusUrl", "mGameStatusUrl", 0x9F5CAC00, TdfType.String, 31, true), // Tag: GURL
        new TdfMemberInfo("TopologyHostNetworkAddressList", "mTopologyHostNetworkAddressList", 0xA2E97400, TdfType.List, 32, true), // Tag: HNET
        new TdfMemberInfo("PINIsCrossplayGame", "mPINIsCrossplayGame", 0xA63C2700, TdfType.Bool, 33, true), // Tag: ICPG
        new TdfMemberInfo("MeshAttribs", "mMeshAttribs", 0xB61D3200, TdfType.Map, 34, true), // Tag: MATR
        new TdfMemberInfo("MaxPlayerCapacity", "mMaxPlayerCapacity", 0xB6387000, TdfType.UInt16, 35, true), // Tag: MCAP
        new TdfMemberInfo("MinPlayerCapacity", "mMinPlayerCapacity", 0xB6E8F000, TdfType.UInt16, 36, true), // Tag: MNCP
        new TdfMemberInfo("NpSessionId", "mNpSessionId", 0xBB0CE900, TdfType.String, 37, true), // Tag: NPSI
        new TdfMemberInfo("NetworkQosData", "mNetworkQosData", 0xBB1BF300, TdfType.Struct, 38, true), // Tag: NQOS
        new TdfMemberInfo("ServerNotResetable", "mServerNotResetable", 0xBB297300, TdfType.Bool, 39, true), // Tag: NRES
        new TdfMemberInfo("NetworkTopology", "mNetworkTopology", 0xBB4BF000, TdfType.Enum, 40, true), // Tag: NTOP
        new TdfMemberInfo("OriginalGameClientPlatformList", "mOriginalGameClientPlatformList", 0xBE3C2C00, TdfType.List, 41, true), // Tag: OCPL
        new TdfMemberInfo("PersistedGameId", "mPersistedGameId", 0xC27A6400, TdfType.String, 42, true), // Tag: PGID
        new TdfMemberInfo("PersistedGameIdSecret", "mPersistedGameIdSecret", 0xC27CF200, TdfType.String, 43, true), // Tag: PGSR
        new TdfMemberInfo("PlatformHostInfo", "mPlatformHostInfo", 0xC28CF400, TdfType.Struct, 44, true), // Tag: PHST
        new TdfMemberInfo("CCSPool", "mCCSPool", 0xC2FBEC00, TdfType.String, 45, true), // Tag: POOL
        new TdfMemberInfo("PresenceDisabledList", "mPresenceDisabledList", 0xC3292C00, TdfType.List, 46, true), // Tag: PRDL
        new TdfMemberInfo("PresenceMode", "mPresenceMode", 0xC3297300, TdfType.Enum, 47, true), // Tag: PRES
        new TdfMemberInfo("PlayerReservationTimeout", "mPlayerReservationTimeout", 0xC32D2F00, TdfType.Int64, 48, true), // Tag: PRTO
        new TdfMemberInfo("PingSiteAlias", "mPingSiteAlias", 0xC3387300, TdfType.String, 49, true), // Tag: PSAS
        new TdfMemberInfo("IsPseudoGame", "mIsPseudoGame", 0xC3397500, TdfType.Bool, 50, true), // Tag: PSEU
        new TdfMemberInfo("PermissionSystemName", "mPermissionSystemName", 0xC33BAD00, TdfType.String, 51, true), // Tag: PSNM
        new TdfMemberInfo("QueueCapacity", "mQueueCapacity", 0xC6387000, TdfType.UInt16, 52, true), // Tag: QCAP
        new TdfMemberInfo("RoleInformation", "mRoleInformation", 0xCAE9AF00, TdfType.Struct, 53, true), // Tag: RNFO
        new TdfMemberInfo("Scid", "mScid", 0xCE3A6400, TdfType.String, 54, true), // Tag: SCID
        new TdfMemberInfo("SharedSeed", "mSharedSeed", 0xCE596400, TdfType.UInt32, 55, true), // Tag: SEED
        new TdfMemberInfo("ExternalSessionTemplateName", "mExternalSessionTemplateName", 0xCF4B6E00, TdfType.String, 56, true), // Tag: STMN
        new TdfMemberInfo("CreateGameTemplateName", "mCreateGameTemplateName", 0xD21B6500, TdfType.String, 57, true), // Tag: TAME
        new TdfMemberInfo("TopologyHostInfo", "mTopologyHostInfo", 0xD28CF400, TdfType.Struct, 58, true), // Tag: THST
        new TdfMemberInfo("TournamentIdentification", "mTournamentIdentification", 0xD2990000, TdfType.Struct, 59, true), // Tag: TID
        new TdfMemberInfo("TeamIds", "mTeamIds", 0xD2993300, TdfType.List, 60, true), // Tag: TIDS
        new TdfMemberInfo("UUID", "mUUID", 0xD75A6400, TdfType.String, 61, true), // Tag: UUID
        new TdfMemberInfo("VoipNetwork", "mVoipNetwork", 0xDAFA7000, TdfType.Enum, 62, true), // Tag: VOIP
        new TdfMemberInfo("GameProtocolVersionString", "mGameProtocolVersionString", 0xDB3D3200, TdfType.String, 63, true), // Tag: VSTR
    ];
    private ITdfMember[] __members;

    private TdfList<long> _adminPlayerList = new(__typeInfos[0]);
    private TdfBool _ownsFirstPartyPresence = new(__typeInfos[1]);
    private TdfMap<string, string> _gameAttribs = new(__typeInfos[2]);
    private TdfList<Blaze15SDK.Blaze.ClientPlatformType> _baseGameClientPlatformList = new(__typeInfos[3]);
    private TdfList<ushort> _slotCapacities = new(__typeInfos[4]);
    private TdfEnum<Blaze15SDK.Blaze.GameManager.CCSMode> _cCSMode = new(__typeInfos[5]);
    private TdfString _externalSessionCorrelationId = new(__typeInfos[6]);
    private TdfBool _isCrossplayEnabled = new(__typeInfos[7]);
    private TdfList<Blaze15SDK.Blaze.ClientPlatformType> _clientPlatformList = new(__typeInfos[8]);
    private TdfMap<string, string> _entryCriteriaMap = new(__typeInfos[9]);
    private TdfString _connectionSetId = new(__typeInfos[10]);
    private TdfInt64 _createTime = new(__typeInfos[11]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.HostInfo?> _dedicatedServerHostInfo = new(__typeInfos[12]);
    private TdfList<Blaze15SDK.Blaze.NetworkAddress> _dedicatedServerHostNetworkAddressList = new(__typeInfos[13]);
    private TdfInt64 _disconnectReservationTimeout = new(__typeInfos[14]);
    private TdfMap<string, string> _dedicatedServerAttribs = new(__typeInfos[15]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.HostInfo?> _externalOwnerInfo = new(__typeInfos[16]);
    private TdfStruct<Blaze15SDK.Blaze.ExternalSessionIdentification?> _externalSessionIdentification = new(__typeInfos[17]);
    private TdfString _externalSessionName = new(__typeInfos[18]);
    private TdfString _gameEventAddress = new(__typeInfos[19]);
    private TdfString _gameEndEventUri = new(__typeInfos[20]);
    private TdfEnum<Blaze15SDK.Blaze.GameManager.GameType> _gameType = new(__typeInfos[21]);
    private TdfUInt64 _gameId = new(__typeInfos[22]);
    private TdfUInt32 _gameModRegister = new(__typeInfos[23]);
    private TdfString _gameName = new(__typeInfos[24]);
    private TdfUInt64 _gameProtocolVersionHash = new(__typeInfos[25]);
    private TdfEnum<Blaze15SDK.Blaze.GameManager.GameSettings> _gameSettings = new(__typeInfos[26]);
    private TdfString _gameStartEventUri = new(__typeInfos[27]);
    private TdfUInt64 _gameReportingId = new(__typeInfos[28]);
    private TdfEnum<Blaze15SDK.Blaze.GameManager.GameState> _gameState = new(__typeInfos[29]);
    private TdfString _gameReportName = new(__typeInfos[30]);
    private TdfString _gameStatusUrl = new(__typeInfos[31]);
    private TdfList<Blaze15SDK.Blaze.NetworkAddress> _topologyHostNetworkAddressList = new(__typeInfos[32]);
    private TdfBool _pINIsCrossplayGame = new(__typeInfos[33]);
    private TdfMap<string, string> _meshAttribs = new(__typeInfos[34]);
    private TdfUInt16 _maxPlayerCapacity = new(__typeInfos[35]);
    private TdfUInt16 _minPlayerCapacity = new(__typeInfos[36]);
    private TdfString _npSessionId = new(__typeInfos[37]);
    private TdfStruct<Blaze15SDK.Blaze.Util.NetworkQosData?> _networkQosData = new(__typeInfos[38]);
    private TdfBool _serverNotResetable = new(__typeInfos[39]);
    private TdfEnum<Blaze15SDK.Blaze.GameNetworkTopology> _networkTopology = new(__typeInfos[40]);
    private TdfList<Blaze15SDK.Blaze.ClientPlatformType> _originalGameClientPlatformList = new(__typeInfos[41]);
    private TdfString _persistedGameId = new(__typeInfos[42]);
    private TdfString _persistedGameIdSecret = new(__typeInfos[43]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.HostInfo?> _platformHostInfo = new(__typeInfos[44]);
    private TdfString _cCSPool = new(__typeInfos[45]);
    private TdfList<Blaze15SDK.Blaze.ClientPlatformType> _presenceDisabledList = new(__typeInfos[46]);
    private TdfEnum<Blaze15SDK.Blaze.PresenceMode> _presenceMode = new(__typeInfos[47]);
    private TdfInt64 _playerReservationTimeout = new(__typeInfos[48]);
    private TdfString _pingSiteAlias = new(__typeInfos[49]);
    private TdfBool _isPseudoGame = new(__typeInfos[50]);
    private TdfString _permissionSystemName = new(__typeInfos[51]);
    private TdfUInt16 _queueCapacity = new(__typeInfos[52]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.RoleInformation?> _roleInformation = new(__typeInfos[53]);
    private TdfString _scid = new(__typeInfos[54]);
    private TdfUInt32 _sharedSeed = new(__typeInfos[55]);
    private TdfString _externalSessionTemplateName = new(__typeInfos[56]);
    private TdfString _createGameTemplateName = new(__typeInfos[57]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.HostInfo?> _topologyHostInfo = new(__typeInfos[58]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.TournamentIdentification?> _tournamentIdentification = new(__typeInfos[59]);
    private TdfList<ushort> _teamIds = new(__typeInfos[60]);
    private TdfString _uUID = new(__typeInfos[61]);
    private TdfEnum<Blaze15SDK.Blaze.VoipTopology> _voipNetwork = new(__typeInfos[62]);
    private TdfString _gameProtocolVersionString = new(__typeInfos[63]);

    public ReplicatedGameData()
    {
        __members = [ _adminPlayerList, _ownsFirstPartyPresence, _gameAttribs, _baseGameClientPlatformList, _slotCapacities, _cCSMode, _externalSessionCorrelationId, _isCrossplayEnabled, _clientPlatformList, _entryCriteriaMap, _connectionSetId, _createTime, _dedicatedServerHostInfo, _dedicatedServerHostNetworkAddressList, _disconnectReservationTimeout, _dedicatedServerAttribs, _externalOwnerInfo, _externalSessionIdentification, _externalSessionName, _gameEventAddress, _gameEndEventUri, _gameType, _gameId, _gameModRegister, _gameName, _gameProtocolVersionHash, _gameSettings, _gameStartEventUri, _gameReportingId, _gameState, _gameReportName, _gameStatusUrl, _topologyHostNetworkAddressList, _pINIsCrossplayGame, _meshAttribs, _maxPlayerCapacity, _minPlayerCapacity, _npSessionId, _networkQosData, _serverNotResetable, _networkTopology, _originalGameClientPlatformList, _persistedGameId, _persistedGameIdSecret, _platformHostInfo, _cCSPool, _presenceDisabledList, _presenceMode, _playerReservationTimeout, _pingSiteAlias, _isPseudoGame, _permissionSystemName, _queueCapacity, _roleInformation, _scid, _sharedSeed, _externalSessionTemplateName, _createGameTemplateName, _topologyHostInfo, _tournamentIdentification, _teamIds, _uUID, _voipNetwork, _gameProtocolVersionString ];
    }

    public override Tdf CreateNew() => new ReplicatedGameData();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ReplicatedGameData";
    public override string GetFullClassName() => "Blaze::GameManager::ReplicatedGameData";

    public IList<long> AdminPlayerList
    {
        get => _adminPlayerList.Value;
        set => _adminPlayerList.Value = value;
    }

    public bool OwnsFirstPartyPresence
    {
        get => _ownsFirstPartyPresence.Value;
        set => _ownsFirstPartyPresence.Value = value;
    }

    public IDictionary<string, string> GameAttribs
    {
        get => _gameAttribs.Value;
        set => _gameAttribs.Value = value;
    }

    public IList<Blaze15SDK.Blaze.ClientPlatformType> BaseGameClientPlatformList
    {
        get => _baseGameClientPlatformList.Value;
        set => _baseGameClientPlatformList.Value = value;
    }

    public IList<ushort> SlotCapacities
    {
        get => _slotCapacities.Value;
        set => _slotCapacities.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.CCSMode CCSMode
    {
        get => _cCSMode.Value;
        set => _cCSMode.Value = value;
    }

    public string ExternalSessionCorrelationId
    {
        get => _externalSessionCorrelationId.Value;
        set => _externalSessionCorrelationId.Value = value;
    }

    public bool IsCrossplayEnabled
    {
        get => _isCrossplayEnabled.Value;
        set => _isCrossplayEnabled.Value = value;
    }

    public IList<Blaze15SDK.Blaze.ClientPlatformType> ClientPlatformList
    {
        get => _clientPlatformList.Value;
        set => _clientPlatformList.Value = value;
    }

    public IDictionary<string, string> EntryCriteriaMap
    {
        get => _entryCriteriaMap.Value;
        set => _entryCriteriaMap.Value = value;
    }

    public string ConnectionSetId
    {
        get => _connectionSetId.Value;
        set => _connectionSetId.Value = value;
    }

    public long CreateTime
    {
        get => _createTime.Value;
        set => _createTime.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.HostInfo? DedicatedServerHostInfo
    {
        get => _dedicatedServerHostInfo.Value;
        set => _dedicatedServerHostInfo.Value = value;
    }

    public IList<Blaze15SDK.Blaze.NetworkAddress> DedicatedServerHostNetworkAddressList
    {
        get => _dedicatedServerHostNetworkAddressList.Value;
        set => _dedicatedServerHostNetworkAddressList.Value = value;
    }

    public long DisconnectReservationTimeout
    {
        get => _disconnectReservationTimeout.Value;
        set => _disconnectReservationTimeout.Value = value;
    }

    public IDictionary<string, string> DedicatedServerAttribs
    {
        get => _dedicatedServerAttribs.Value;
        set => _dedicatedServerAttribs.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.HostInfo? ExternalOwnerInfo
    {
        get => _externalOwnerInfo.Value;
        set => _externalOwnerInfo.Value = value;
    }

    public Blaze15SDK.Blaze.ExternalSessionIdentification? ExternalSessionIdentification
    {
        get => _externalSessionIdentification.Value;
        set => _externalSessionIdentification.Value = value;
    }

    public string ExternalSessionName
    {
        get => _externalSessionName.Value;
        set => _externalSessionName.Value = value;
    }

    public string GameEventAddress
    {
        get => _gameEventAddress.Value;
        set => _gameEventAddress.Value = value;
    }

    public string GameEndEventUri
    {
        get => _gameEndEventUri.Value;
        set => _gameEndEventUri.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.GameType GameType
    {
        get => _gameType.Value;
        set => _gameType.Value = value;
    }

    public ulong GameId
    {
        get => _gameId.Value;
        set => _gameId.Value = value;
    }

    public uint GameModRegister
    {
        get => _gameModRegister.Value;
        set => _gameModRegister.Value = value;
    }

    public string GameName
    {
        get => _gameName.Value;
        set => _gameName.Value = value;
    }

    public ulong GameProtocolVersionHash
    {
        get => _gameProtocolVersionHash.Value;
        set => _gameProtocolVersionHash.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.GameSettings GameSettings
    {
        get => _gameSettings.Value;
        set => _gameSettings.Value = value;
    }

    public string GameStartEventUri
    {
        get => _gameStartEventUri.Value;
        set => _gameStartEventUri.Value = value;
    }

    public ulong GameReportingId
    {
        get => _gameReportingId.Value;
        set => _gameReportingId.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.GameState GameState
    {
        get => _gameState.Value;
        set => _gameState.Value = value;
    }

    public string GameReportName
    {
        get => _gameReportName.Value;
        set => _gameReportName.Value = value;
    }

    public string GameStatusUrl
    {
        get => _gameStatusUrl.Value;
        set => _gameStatusUrl.Value = value;
    }

    public IList<Blaze15SDK.Blaze.NetworkAddress> TopologyHostNetworkAddressList
    {
        get => _topologyHostNetworkAddressList.Value;
        set => _topologyHostNetworkAddressList.Value = value;
    }

    public bool PINIsCrossplayGame
    {
        get => _pINIsCrossplayGame.Value;
        set => _pINIsCrossplayGame.Value = value;
    }

    public IDictionary<string, string> MeshAttribs
    {
        get => _meshAttribs.Value;
        set => _meshAttribs.Value = value;
    }

    public ushort MaxPlayerCapacity
    {
        get => _maxPlayerCapacity.Value;
        set => _maxPlayerCapacity.Value = value;
    }

    public ushort MinPlayerCapacity
    {
        get => _minPlayerCapacity.Value;
        set => _minPlayerCapacity.Value = value;
    }

    public string NpSessionId
    {
        get => _npSessionId.Value;
        set => _npSessionId.Value = value;
    }

    public Blaze15SDK.Blaze.Util.NetworkQosData? NetworkQosData
    {
        get => _networkQosData.Value;
        set => _networkQosData.Value = value;
    }

    public bool ServerNotResetable
    {
        get => _serverNotResetable.Value;
        set => _serverNotResetable.Value = value;
    }

    public Blaze15SDK.Blaze.GameNetworkTopology NetworkTopology
    {
        get => _networkTopology.Value;
        set => _networkTopology.Value = value;
    }

    public IList<Blaze15SDK.Blaze.ClientPlatformType> OriginalGameClientPlatformList
    {
        get => _originalGameClientPlatformList.Value;
        set => _originalGameClientPlatformList.Value = value;
    }

    public string PersistedGameId
    {
        get => _persistedGameId.Value;
        set => _persistedGameId.Value = value;
    }

    public string PersistedGameIdSecret
    {
        get => _persistedGameIdSecret.Value;
        set => _persistedGameIdSecret.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.HostInfo? PlatformHostInfo
    {
        get => _platformHostInfo.Value;
        set => _platformHostInfo.Value = value;
    }

    public string CCSPool
    {
        get => _cCSPool.Value;
        set => _cCSPool.Value = value;
    }

    public IList<Blaze15SDK.Blaze.ClientPlatformType> PresenceDisabledList
    {
        get => _presenceDisabledList.Value;
        set => _presenceDisabledList.Value = value;
    }

    public Blaze15SDK.Blaze.PresenceMode PresenceMode
    {
        get => _presenceMode.Value;
        set => _presenceMode.Value = value;
    }

    public long PlayerReservationTimeout
    {
        get => _playerReservationTimeout.Value;
        set => _playerReservationTimeout.Value = value;
    }

    public string PingSiteAlias
    {
        get => _pingSiteAlias.Value;
        set => _pingSiteAlias.Value = value;
    }

    public bool IsPseudoGame
    {
        get => _isPseudoGame.Value;
        set => _isPseudoGame.Value = value;
    }

    public string PermissionSystemName
    {
        get => _permissionSystemName.Value;
        set => _permissionSystemName.Value = value;
    }

    public ushort QueueCapacity
    {
        get => _queueCapacity.Value;
        set => _queueCapacity.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.RoleInformation? RoleInformation
    {
        get => _roleInformation.Value;
        set => _roleInformation.Value = value;
    }

    public string Scid
    {
        get => _scid.Value;
        set => _scid.Value = value;
    }

    public uint SharedSeed
    {
        get => _sharedSeed.Value;
        set => _sharedSeed.Value = value;
    }

    public string ExternalSessionTemplateName
    {
        get => _externalSessionTemplateName.Value;
        set => _externalSessionTemplateName.Value = value;
    }

    public string CreateGameTemplateName
    {
        get => _createGameTemplateName.Value;
        set => _createGameTemplateName.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.HostInfo? TopologyHostInfo
    {
        get => _topologyHostInfo.Value;
        set => _topologyHostInfo.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.TournamentIdentification? TournamentIdentification
    {
        get => _tournamentIdentification.Value;
        set => _tournamentIdentification.Value = value;
    }

    public IList<ushort> TeamIds
    {
        get => _teamIds.Value;
        set => _teamIds.Value = value;
    }

    public string UUID
    {
        get => _uUID.Value;
        set => _uUID.Value = value;
    }

    public Blaze15SDK.Blaze.VoipTopology VoipNetwork
    {
        get => _voipNetwork.Value;
        set => _voipNetwork.Value = value;
    }

    public string GameProtocolVersionString
    {
        get => _gameProtocolVersionString.Value;
        set => _gameProtocolVersionString.Value = value;
    }

}
