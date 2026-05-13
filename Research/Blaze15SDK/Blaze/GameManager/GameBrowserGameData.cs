using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class GameBrowserGameData : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("AdminPlayerList", "mAdminPlayerList", 0x864B6E00, TdfType.List, 0, true), // Tag: ADMN
        new TdfMemberInfo("GameAttribs", "mGameAttribs", 0x874D3200, TdfType.Map, 1, true), // Tag: ATTR
        new TdfMemberInfo("SlotCapacities", "mSlotCapacities", 0x8E1C0000, TdfType.List, 2, true), // Tag: CAP
        new TdfMemberInfo("ClientPlatformList", "mClientPlatformList", 0x8F0BF600, TdfType.List, 3, true), // Tag: CPOV
        new TdfMemberInfo("EntryCriteriaMap", "mEntryCriteriaMap", 0x8F2A7400, TdfType.Map, 4, true), // Tag: CRIT
        new TdfMemberInfo("DedicatedServerAttribs", "mDedicatedServerAttribs", 0x933D3200, TdfType.Map, 5, true), // Tag: DSTR
        new TdfMemberInfo("GameId", "mGameId", 0x9E990000, TdfType.UInt64, 6, true), // Tag: GID
        new TdfMemberInfo("GameModRegister", "mGameModRegister", 0x9EDCA700, TdfType.UInt32, 7, true), // Tag: GMRG
        new TdfMemberInfo("GameName", "mGameName", 0x9EE86D00, TdfType.String, 8, true), // Tag: GNAM
        new TdfMemberInfo("GameSettings", "mGameSettings", 0x9F397400, TdfType.Enum, 9, true), // Tag: GSET
        new TdfMemberInfo("GameState", "mGameState", 0x9F3D2100, TdfType.Enum, 10, true), // Tag: GSTA
        new TdfMemberInfo("GameType", "mGameType", 0x9F4E7000, TdfType.Enum, 11, true), // Tag: GTYP
        new TdfMemberInfo("GameStatusUrl", "mGameStatusUrl", 0x9F5CAC00, TdfType.String, 12, true), // Tag: GURL
        new TdfMemberInfo("HostNetworkAddressList", "mHostNetworkAddressList", 0xA2E97400, TdfType.List, 13, true), // Tag: HNET
        new TdfMemberInfo("HostId", "mHostId", 0xA2FCF400, TdfType.Int64, 14, true), // Tag: HOST
        new TdfMemberInfo("TopologyHostSessionId", "mTopologyHostSessionId", 0xA3397300, TdfType.UInt64, 15, true), // Tag: HSES
        new TdfMemberInfo("GameMode", "mGameMode", 0xB6F92500, TdfType.String, 16, true), // Tag: MODE
        new TdfMemberInfo("NetworkTopology", "mNetworkTopology", 0xBB4BF000, TdfType.Enum, 17, true), // Tag: NTOP
        new TdfMemberInfo("PlayerCounts", "mPlayerCounts", 0xC23BB400, TdfType.List, 18, true), // Tag: PCNT
        new TdfMemberInfo("PresenceDisabledList", "mPresenceDisabledList", 0xC3292C00, TdfType.List, 19, true), // Tag: PRDL
        new TdfMemberInfo("PresenceMode", "mPresenceMode", 0xC3297300, TdfType.Enum, 20, true), // Tag: PRES
        new TdfMemberInfo("PingSiteAlias", "mPingSiteAlias", 0xC3387300, TdfType.String, 21, true), // Tag: PSAS
        new TdfMemberInfo("PersistedGameId", "mPersistedGameId", 0xC33A6400, TdfType.String, 22, true), // Tag: PSID
        new TdfMemberInfo("QueueCapacity", "mQueueCapacity", 0xC6387000, TdfType.UInt16, 23, true), // Tag: QCAP
        new TdfMemberInfo("QueueCount", "mQueueCount", 0xC63BB400, TdfType.UInt16, 24, true), // Tag: QCNT
        new TdfMemberInfo("RoleInformation", "mRoleInformation", 0xCAE9AF00, TdfType.Struct, 25, true), // Tag: RNFO
        new TdfMemberInfo("GameRoster", "mGameRoster", 0xCAFCF400, TdfType.List, 26, true), // Tag: ROST
        new TdfMemberInfo("ExternalSessionId", "mExternalSessionId", 0xCE990000, TdfType.UInt64, 27, true), // Tag: SID
        new TdfMemberInfo("TeamCapacity", "mTeamCapacity", 0xD2387000, TdfType.UInt16, 28, true), // Tag: TCAP
        new TdfMemberInfo("GameBrowserTeamInfoVector", "mGameBrowserTeamInfoVector", 0xD29BA600, TdfType.List, 29, true), // Tag: TINF
        new TdfMemberInfo("VoipTopology", "mVoipTopology", 0xDAFA7000, TdfType.Enum, 30, true), // Tag: VOIP
        new TdfMemberInfo("GameProtocolVersionString", "mGameProtocolVersionString", 0xDB3D3200, TdfType.String, 31, true), // Tag: VSTR
    ];
    private ITdfMember[] __members;

    private TdfList<long> _adminPlayerList = new(__typeInfos[0]);
    private TdfMap<string, string> _gameAttribs = new(__typeInfos[1]);
    private TdfList<ushort> _slotCapacities = new(__typeInfos[2]);
    private TdfList<Blaze15SDK.Blaze.ClientPlatformType> _clientPlatformList = new(__typeInfos[3]);
    private TdfMap<string, string> _entryCriteriaMap = new(__typeInfos[4]);
    private TdfMap<string, string> _dedicatedServerAttribs = new(__typeInfos[5]);
    private TdfUInt64 _gameId = new(__typeInfos[6]);
    private TdfUInt32 _gameModRegister = new(__typeInfos[7]);
    private TdfString _gameName = new(__typeInfos[8]);
    private TdfEnum<Blaze15SDK.Blaze.GameManager.GameSettings> _gameSettings = new(__typeInfos[9]);
    private TdfEnum<Blaze15SDK.Blaze.GameManager.GameState> _gameState = new(__typeInfos[10]);
    private TdfEnum<Blaze15SDK.Blaze.GameManager.GameType> _gameType = new(__typeInfos[11]);
    private TdfString _gameStatusUrl = new(__typeInfos[12]);
    private TdfList<Blaze15SDK.Blaze.NetworkAddress> _hostNetworkAddressList = new(__typeInfos[13]);
    private TdfInt64 _hostId = new(__typeInfos[14]);
    private TdfUInt64 _topologyHostSessionId = new(__typeInfos[15]);
    private TdfString _gameMode = new(__typeInfos[16]);
    private TdfEnum<Blaze15SDK.Blaze.GameNetworkTopology> _networkTopology = new(__typeInfos[17]);
    private TdfList<ushort> _playerCounts = new(__typeInfos[18]);
    private TdfList<Blaze15SDK.Blaze.ClientPlatformType> _presenceDisabledList = new(__typeInfos[19]);
    private TdfEnum<Blaze15SDK.Blaze.PresenceMode> _presenceMode = new(__typeInfos[20]);
    private TdfString _pingSiteAlias = new(__typeInfos[21]);
    private TdfString _persistedGameId = new(__typeInfos[22]);
    private TdfUInt16 _queueCapacity = new(__typeInfos[23]);
    private TdfUInt16 _queueCount = new(__typeInfos[24]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.RoleInformation?> _roleInformation = new(__typeInfos[25]);
    private TdfList<Blaze15SDK.Blaze.GameManager.GameBrowserPlayerData> _gameRoster = new(__typeInfos[26]);
    private TdfUInt64 _externalSessionId = new(__typeInfos[27]);
    private TdfUInt16 _teamCapacity = new(__typeInfos[28]);
    private TdfList<Blaze15SDK.Blaze.GameManager.GameBrowserTeamInfo> _gameBrowserTeamInfoVector = new(__typeInfos[29]);
    private TdfEnum<Blaze15SDK.Blaze.VoipTopology> _voipTopology = new(__typeInfos[30]);
    private TdfString _gameProtocolVersionString = new(__typeInfos[31]);

    public GameBrowserGameData()
    {
        __members = [ _adminPlayerList, _gameAttribs, _slotCapacities, _clientPlatformList, _entryCriteriaMap, _dedicatedServerAttribs, _gameId, _gameModRegister, _gameName, _gameSettings, _gameState, _gameType, _gameStatusUrl, _hostNetworkAddressList, _hostId, _topologyHostSessionId, _gameMode, _networkTopology, _playerCounts, _presenceDisabledList, _presenceMode, _pingSiteAlias, _persistedGameId, _queueCapacity, _queueCount, _roleInformation, _gameRoster, _externalSessionId, _teamCapacity, _gameBrowserTeamInfoVector, _voipTopology, _gameProtocolVersionString ];
    }

    public override Tdf CreateNew() => new GameBrowserGameData();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GameBrowserGameData";
    public override string GetFullClassName() => "Blaze::GameManager::GameBrowserGameData";

    public IList<long> AdminPlayerList
    {
        get => _adminPlayerList.Value;
        set => _adminPlayerList.Value = value;
    }

    public IDictionary<string, string> GameAttribs
    {
        get => _gameAttribs.Value;
        set => _gameAttribs.Value = value;
    }

    public IList<ushort> SlotCapacities
    {
        get => _slotCapacities.Value;
        set => _slotCapacities.Value = value;
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

    public IDictionary<string, string> DedicatedServerAttribs
    {
        get => _dedicatedServerAttribs.Value;
        set => _dedicatedServerAttribs.Value = value;
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

    public Blaze15SDK.Blaze.GameManager.GameSettings GameSettings
    {
        get => _gameSettings.Value;
        set => _gameSettings.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.GameState GameState
    {
        get => _gameState.Value;
        set => _gameState.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.GameType GameType
    {
        get => _gameType.Value;
        set => _gameType.Value = value;
    }

    public string GameStatusUrl
    {
        get => _gameStatusUrl.Value;
        set => _gameStatusUrl.Value = value;
    }

    public IList<Blaze15SDK.Blaze.NetworkAddress> HostNetworkAddressList
    {
        get => _hostNetworkAddressList.Value;
        set => _hostNetworkAddressList.Value = value;
    }

    public long HostId
    {
        get => _hostId.Value;
        set => _hostId.Value = value;
    }

    public ulong TopologyHostSessionId
    {
        get => _topologyHostSessionId.Value;
        set => _topologyHostSessionId.Value = value;
    }

    public string GameMode
    {
        get => _gameMode.Value;
        set => _gameMode.Value = value;
    }

    public Blaze15SDK.Blaze.GameNetworkTopology NetworkTopology
    {
        get => _networkTopology.Value;
        set => _networkTopology.Value = value;
    }

    public IList<ushort> PlayerCounts
    {
        get => _playerCounts.Value;
        set => _playerCounts.Value = value;
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

    public string PingSiteAlias
    {
        get => _pingSiteAlias.Value;
        set => _pingSiteAlias.Value = value;
    }

    public string PersistedGameId
    {
        get => _persistedGameId.Value;
        set => _persistedGameId.Value = value;
    }

    public ushort QueueCapacity
    {
        get => _queueCapacity.Value;
        set => _queueCapacity.Value = value;
    }

    public ushort QueueCount
    {
        get => _queueCount.Value;
        set => _queueCount.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.RoleInformation? RoleInformation
    {
        get => _roleInformation.Value;
        set => _roleInformation.Value = value;
    }

    public IList<Blaze15SDK.Blaze.GameManager.GameBrowserPlayerData> GameRoster
    {
        get => _gameRoster.Value;
        set => _gameRoster.Value = value;
    }

    public ulong ExternalSessionId
    {
        get => _externalSessionId.Value;
        set => _externalSessionId.Value = value;
    }

    public ushort TeamCapacity
    {
        get => _teamCapacity.Value;
        set => _teamCapacity.Value = value;
    }

    public IList<Blaze15SDK.Blaze.GameManager.GameBrowserTeamInfo> GameBrowserTeamInfoVector
    {
        get => _gameBrowserTeamInfoVector.Value;
        set => _gameBrowserTeamInfoVector.Value = value;
    }

    public Blaze15SDK.Blaze.VoipTopology VoipTopology
    {
        get => _voipTopology.Value;
        set => _voipTopology.Value = value;
    }

    public string GameProtocolVersionString
    {
        get => _gameProtocolVersionString.Value;
        set => _gameProtocolVersionString.Value = value;
    }

}
