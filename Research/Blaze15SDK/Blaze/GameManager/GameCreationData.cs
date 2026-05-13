using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class GameCreationData : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("GameAttribs", "mGameAttribs", 0x874D3200, TdfType.Map, 0, true), // Tag: ATTR
        new TdfMemberInfo("EntryCriteriaMap", "mEntryCriteriaMap", 0x8F2A7400, TdfType.Map, 1, true), // Tag: CRIT
        new TdfMemberInfo("DisconnectReservationTimeout", "mDisconnectReservationTimeout", 0x932D2F00, TdfType.Int64, 2, true), // Tag: DRTO
        new TdfMemberInfo("DataSourceNameList", "mDataSourceNameList", 0x964CEF00, TdfType.List, 3, true), // Tag: EDSO
        new TdfMemberInfo("IsExternalOwner", "mIsExternalOwner", 0x96FDEE00, TdfType.Bool, 4, true), // Tag: EOWN
        new TdfMemberInfo("ExternalSessionRecentPlayers", "mExternalSessionRecentPlayers", 0x978CB000, TdfType.Struct, 5, true), // Tag: EXRP
        new TdfMemberInfo("ExternalSessionIdentSetup", "mExternalSessionIdentSetup", 0x978D3300, TdfType.Struct, 6, true), // Tag: EXTS
        new TdfMemberInfo("GameModRegister", "mGameModRegister", 0x9EDCA700, TdfType.UInt32, 7, true), // Tag: GMRG
        new TdfMemberInfo("GameName", "mGameName", 0x9EE86D00, TdfType.String, 8, true), // Tag: GNAM
        new TdfMemberInfo("PermissionSystemName", "mPermissionSystemName", 0x9F0CEE00, TdfType.String, 9, true), // Tag: GPSN
        new TdfMemberInfo("GameSettings", "mGameSettings", 0x9F397400, TdfType.Enum, 10, true), // Tag: GSET
        new TdfMemberInfo("GameReportName", "mGameReportName", 0x9F4E7000, TdfType.String, 11, true), // Tag: GTYP
        new TdfMemberInfo("SkipInitializing", "mSkipInitializing", 0xBA9BA900, TdfType.Bool, 12, true), // Tag: NINI
        new TdfMemberInfo("NetworkTopology", "mNetworkTopology", 0xBB4BF000, TdfType.Enum, 13, true), // Tag: NTOP
        new TdfMemberInfo("IsExternalCaller", "mIsExternalCaller", 0xBE386C00, TdfType.Bool, 14, true), // Tag: OCAL
        new TdfMemberInfo("SlotCapacitiesMap", "mSlotCapacitiesMap", 0xC23C2D00, TdfType.Map, 15, true), // Tag: PCPM
        new TdfMemberInfo("MaxPlayerCapacity", "mMaxPlayerCapacity", 0xC2D87800, TdfType.UInt16, 16, true), // Tag: PMAX
        new TdfMemberInfo("MinPlayerCapacity", "mMinPlayerCapacity", 0xC2DA6E00, TdfType.UInt16, 17, true), // Tag: PMIN
        new TdfMemberInfo("PresenceDisabledList", "mPresenceDisabledList", 0xC3292C00, TdfType.List, 18, true), // Tag: PRDL
        new TdfMemberInfo("PresenceMode", "mPresenceMode", 0xC3297300, TdfType.Enum, 19, true), // Tag: PRES
        new TdfMemberInfo("PlayerReservationTimeout", "mPlayerReservationTimeout", 0xC32D2F00, TdfType.Int64, 20, true), // Tag: PRTO
        new TdfMemberInfo("QueueCapacity", "mQueueCapacity", 0xC6387000, TdfType.UInt16, 21, true), // Tag: QCAP
        new TdfMemberInfo("RoleInformation", "mRoleInformation", 0xCAE9AF00, TdfType.Struct, 22, true), // Tag: RNFO
        new TdfMemberInfo("ExternalSessionCustomData", "mExternalSessionCustomData", 0xCE487400, TdfType.Map, 23, true), // Tag: SDAT
        new TdfMemberInfo("ExternalSessionStatus", "mExternalSessionStatus", 0xCF3D2100, TdfType.Struct, 24, true), // Tag: SSTA
        new TdfMemberInfo("ExternalSessionTemplateName", "mExternalSessionTemplateName", 0xCF4B6E00, TdfType.String, 25, true), // Tag: STMN
        new TdfMemberInfo("TeamIds", "mTeamIds", 0xD2993300, TdfType.List, 26, true), // Tag: TIDS
        new TdfMemberInfo("VoipNetwork", "mVoipNetwork", 0xDAFA7000, TdfType.Enum, 27, true), // Tag: VOIP
        new TdfMemberInfo("XblLargeSession", "mXblLargeSession", 0xE2CCA700, TdfType.Bool, 28, true), // Tag: XLRG
    ];
    private ITdfMember[] __members;

    private TdfMap<string, string> _gameAttribs = new(__typeInfos[0]);
    private TdfMap<string, string> _entryCriteriaMap = new(__typeInfos[1]);
    private TdfInt64 _disconnectReservationTimeout = new(__typeInfos[2]);
    private TdfList<string> _dataSourceNameList = new(__typeInfos[3]);
    private TdfBool _isExternalOwner = new(__typeInfos[4]);
    private TdfStruct<Blaze15SDK.Blaze.ExternalSessionRecentPlayersSettings?> _externalSessionRecentPlayers = new(__typeInfos[5]);
    private TdfStruct<Blaze15SDK.Blaze.ExternalSessionIdentification?> _externalSessionIdentSetup = new(__typeInfos[6]);
    private TdfUInt32 _gameModRegister = new(__typeInfos[7]);
    private TdfString _gameName = new(__typeInfos[8]);
    private TdfString _permissionSystemName = new(__typeInfos[9]);
    private TdfEnum<Blaze15SDK.Blaze.GameManager.GameSettings> _gameSettings = new(__typeInfos[10]);
    private TdfString _gameReportName = new(__typeInfos[11]);
    private TdfBool _skipInitializing = new(__typeInfos[12]);
    private TdfEnum<Blaze15SDK.Blaze.GameNetworkTopology> _networkTopology = new(__typeInfos[13]);
    private TdfBool _isExternalCaller = new(__typeInfos[14]);
    private TdfMap<Blaze15SDK.Blaze.GameManager.SlotType, ushort> _slotCapacitiesMap = new(__typeInfos[15]);
    private TdfUInt16 _maxPlayerCapacity = new(__typeInfos[16]);
    private TdfUInt16 _minPlayerCapacity = new(__typeInfos[17]);
    private TdfList<Blaze15SDK.Blaze.ClientPlatformType> _presenceDisabledList = new(__typeInfos[18]);
    private TdfEnum<Blaze15SDK.Blaze.PresenceMode> _presenceMode = new(__typeInfos[19]);
    private TdfInt64 _playerReservationTimeout = new(__typeInfos[20]);
    private TdfUInt16 _queueCapacity = new(__typeInfos[21]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.RoleInformation?> _roleInformation = new(__typeInfos[22]);
    private TdfMap<string, string> _externalSessionCustomData = new(__typeInfos[23]);
    private TdfStruct<Blaze15SDK.Blaze.ExternalSessionStatus?> _externalSessionStatus = new(__typeInfos[24]);
    private TdfString _externalSessionTemplateName = new(__typeInfos[25]);
    private TdfList<ushort> _teamIds = new(__typeInfos[26]);
    private TdfEnum<Blaze15SDK.Blaze.VoipTopology> _voipNetwork = new(__typeInfos[27]);
    private TdfBool _xblLargeSession = new(__typeInfos[28]);

    public GameCreationData()
    {
        __members = [ _gameAttribs, _entryCriteriaMap, _disconnectReservationTimeout, _dataSourceNameList, _isExternalOwner, _externalSessionRecentPlayers, _externalSessionIdentSetup, _gameModRegister, _gameName, _permissionSystemName, _gameSettings, _gameReportName, _skipInitializing, _networkTopology, _isExternalCaller, _slotCapacitiesMap, _maxPlayerCapacity, _minPlayerCapacity, _presenceDisabledList, _presenceMode, _playerReservationTimeout, _queueCapacity, _roleInformation, _externalSessionCustomData, _externalSessionStatus, _externalSessionTemplateName, _teamIds, _voipNetwork, _xblLargeSession ];
    }

    public override Tdf CreateNew() => new GameCreationData();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GameCreationData";
    public override string GetFullClassName() => "Blaze::GameManager::GameCreationData";

    public IDictionary<string, string> GameAttribs
    {
        get => _gameAttribs.Value;
        set => _gameAttribs.Value = value;
    }

    public IDictionary<string, string> EntryCriteriaMap
    {
        get => _entryCriteriaMap.Value;
        set => _entryCriteriaMap.Value = value;
    }

    public long DisconnectReservationTimeout
    {
        get => _disconnectReservationTimeout.Value;
        set => _disconnectReservationTimeout.Value = value;
    }

    public IList<string> DataSourceNameList
    {
        get => _dataSourceNameList.Value;
        set => _dataSourceNameList.Value = value;
    }

    public bool IsExternalOwner
    {
        get => _isExternalOwner.Value;
        set => _isExternalOwner.Value = value;
    }

    public Blaze15SDK.Blaze.ExternalSessionRecentPlayersSettings? ExternalSessionRecentPlayers
    {
        get => _externalSessionRecentPlayers.Value;
        set => _externalSessionRecentPlayers.Value = value;
    }

    public Blaze15SDK.Blaze.ExternalSessionIdentification? ExternalSessionIdentSetup
    {
        get => _externalSessionIdentSetup.Value;
        set => _externalSessionIdentSetup.Value = value;
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

    public string PermissionSystemName
    {
        get => _permissionSystemName.Value;
        set => _permissionSystemName.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.GameSettings GameSettings
    {
        get => _gameSettings.Value;
        set => _gameSettings.Value = value;
    }

    public string GameReportName
    {
        get => _gameReportName.Value;
        set => _gameReportName.Value = value;
    }

    public bool SkipInitializing
    {
        get => _skipInitializing.Value;
        set => _skipInitializing.Value = value;
    }

    public Blaze15SDK.Blaze.GameNetworkTopology NetworkTopology
    {
        get => _networkTopology.Value;
        set => _networkTopology.Value = value;
    }

    public bool IsExternalCaller
    {
        get => _isExternalCaller.Value;
        set => _isExternalCaller.Value = value;
    }

    public IDictionary<Blaze15SDK.Blaze.GameManager.SlotType, ushort> SlotCapacitiesMap
    {
        get => _slotCapacitiesMap.Value;
        set => _slotCapacitiesMap.Value = value;
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

    public IDictionary<string, string> ExternalSessionCustomData
    {
        get => _externalSessionCustomData.Value;
        set => _externalSessionCustomData.Value = value;
    }

    public Blaze15SDK.Blaze.ExternalSessionStatus? ExternalSessionStatus
    {
        get => _externalSessionStatus.Value;
        set => _externalSessionStatus.Value = value;
    }

    public string ExternalSessionTemplateName
    {
        get => _externalSessionTemplateName.Value;
        set => _externalSessionTemplateName.Value = value;
    }

    public IList<ushort> TeamIds
    {
        get => _teamIds.Value;
        set => _teamIds.Value = value;
    }

    public Blaze15SDK.Blaze.VoipTopology VoipNetwork
    {
        get => _voipNetwork.Value;
        set => _voipNetwork.Value = value;
    }

    public bool XblLargeSession
    {
        get => _xblLargeSession.Value;
        set => _xblLargeSession.Value = value;
    }

}
