using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class CreateGameRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("AdminPlayerList", "mAdminPlayerList", 0x864B6E00, TdfType.List, 0, true), // Tag: ADMN
        new TdfMemberInfo("CommonGameData", "mCommonGameData", 0x8ED9E400, TdfType.Struct, 1, true), // Tag: CMGD
        new TdfMemberInfo("ClientPlatformListOverride", "mClientPlatformListOverride", 0x8F0BF600, TdfType.List, 2, true), // Tag: CPOV
        new TdfMemberInfo("DedicatedServerAttribs", "mDedicatedServerAttribs", 0x933D3200, TdfType.Map, 3, true), // Tag: DSTR
        new TdfMemberInfo("TournamentSessionData", "mTournamentSessionData", 0x974CAE00, TdfType.Struct, 4, true), // Tag: ETRN
        new TdfMemberInfo("GamePingSiteAlias", "mGamePingSiteAlias", 0x9E3D3200, TdfType.String, 5, true), // Tag: GCTR
        new TdfMemberInfo("GameEventAddress", "mGameEventAddress", 0x9E586400, TdfType.String, 6, true), // Tag: GEAD
        new TdfMemberInfo("GameEndEventUri", "mGameEndEventUri", 0x9E597500, TdfType.String, 7, true), // Tag: GEEU
        new TdfMemberInfo("GameCreationData", "mGameCreationData", 0x9ED8E400, TdfType.Struct, 8, true), // Tag: GMCD
        new TdfMemberInfo("GameEventContentType", "mGameEventContentType", 0x9F396E00, TdfType.String, 9, true), // Tag: GSEN
        new TdfMemberInfo("GameStartEventUri", "mGameStartEventUri", 0x9F397500, TdfType.String, 10, true), // Tag: GSEU
        new TdfMemberInfo("GameReportName", "mGameReportName", 0x9F4E7000, TdfType.String, 11, true), // Tag: GTYP
        new TdfMemberInfo("GameStatusUrl", "mGameStatusUrl", 0x9F5CAC00, TdfType.String, 12, true), // Tag: GURL
        new TdfMemberInfo("MeshAttribs", "mMeshAttribs", 0xB61D3200, TdfType.Map, 13, true), // Tag: MATR
        new TdfMemberInfo("ServerNotResetable", "mServerNotResetable", 0xBB297300, TdfType.Bool, 14, true), // Tag: NRES
        new TdfMemberInfo("SlotCapacities", "mSlotCapacities", 0xC2387000, TdfType.List, 15, true), // Tag: PCAP
        new TdfMemberInfo("SlotCapacitiesMap", "mSlotCapacitiesMap", 0xC23C2D00, TdfType.Map, 16, true), // Tag: PCPM
        new TdfMemberInfo("PersistedGameId", "mPersistedGameId", 0xC27A6400, TdfType.String, 17, true), // Tag: PGID
        new TdfMemberInfo("PersistedGameIdSecret", "mPersistedGameIdSecret", 0xC27CE300, TdfType.Blob, 18, true), // Tag: PGSC
        new TdfMemberInfo("PlayerJoinData", "mPlayerJoinData", 0xC2CAA400, TdfType.Struct, 19, true), // Tag: PLJD
        new TdfMemberInfo("TournamentIdentification", "mTournamentIdentification", 0xD2990000, TdfType.Struct, 20, true), // Tag: TID
        new TdfMemberInfo("TeamIds", "mTeamIds", 0xD2993300, TdfType.List, 21, true), // Tag: TIDS
    ];
    private ITdfMember[] __members;

    private TdfList<long> _adminPlayerList = new(__typeInfos[0]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.CommonGameRequestData?> _commonGameData = new(__typeInfos[1]);
    private TdfList<Blaze15SDK.Blaze.ClientPlatformType> _clientPlatformListOverride = new(__typeInfos[2]);
    private TdfMap<string, string> _dedicatedServerAttribs = new(__typeInfos[3]);
    private TdfStruct<Blaze15SDK.Blaze.TournamentSessionData?> _tournamentSessionData = new(__typeInfos[4]);
    private TdfString _gamePingSiteAlias = new(__typeInfos[5]);
    private TdfString _gameEventAddress = new(__typeInfos[6]);
    private TdfString _gameEndEventUri = new(__typeInfos[7]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.GameCreationData?> _gameCreationData = new(__typeInfos[8]);
    private TdfString _gameEventContentType = new(__typeInfos[9]);
    private TdfString _gameStartEventUri = new(__typeInfos[10]);
    private TdfString _gameReportName = new(__typeInfos[11]);
    private TdfString _gameStatusUrl = new(__typeInfos[12]);
    private TdfMap<string, string> _meshAttribs = new(__typeInfos[13]);
    private TdfBool _serverNotResetable = new(__typeInfos[14]);
    private TdfList<ushort> _slotCapacities = new(__typeInfos[15]);
    private TdfMap<Blaze15SDK.Blaze.GameManager.SlotType, ushort> _slotCapacitiesMap = new(__typeInfos[16]);
    private TdfString _persistedGameId = new(__typeInfos[17]);
    private TdfBlob _persistedGameIdSecret = new(__typeInfos[18]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.PlayerJoinData?> _playerJoinData = new(__typeInfos[19]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.TournamentIdentification?> _tournamentIdentification = new(__typeInfos[20]);
    private TdfList<ushort> _teamIds = new(__typeInfos[21]);

    public CreateGameRequest()
    {
        __members = [ _adminPlayerList, _commonGameData, _clientPlatformListOverride, _dedicatedServerAttribs, _tournamentSessionData, _gamePingSiteAlias, _gameEventAddress, _gameEndEventUri, _gameCreationData, _gameEventContentType, _gameStartEventUri, _gameReportName, _gameStatusUrl, _meshAttribs, _serverNotResetable, _slotCapacities, _slotCapacitiesMap, _persistedGameId, _persistedGameIdSecret, _playerJoinData, _tournamentIdentification, _teamIds ];
    }

    public override Tdf CreateNew() => new CreateGameRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "CreateGameRequest";
    public override string GetFullClassName() => "Blaze::GameManager::CreateGameRequest";

    public IList<long> AdminPlayerList
    {
        get => _adminPlayerList.Value;
        set => _adminPlayerList.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.CommonGameRequestData? CommonGameData
    {
        get => _commonGameData.Value;
        set => _commonGameData.Value = value;
    }

    public IList<Blaze15SDK.Blaze.ClientPlatformType> ClientPlatformListOverride
    {
        get => _clientPlatformListOverride.Value;
        set => _clientPlatformListOverride.Value = value;
    }

    public IDictionary<string, string> DedicatedServerAttribs
    {
        get => _dedicatedServerAttribs.Value;
        set => _dedicatedServerAttribs.Value = value;
    }

    public Blaze15SDK.Blaze.TournamentSessionData? TournamentSessionData
    {
        get => _tournamentSessionData.Value;
        set => _tournamentSessionData.Value = value;
    }

    public string GamePingSiteAlias
    {
        get => _gamePingSiteAlias.Value;
        set => _gamePingSiteAlias.Value = value;
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

    public Blaze15SDK.Blaze.GameManager.GameCreationData? GameCreationData
    {
        get => _gameCreationData.Value;
        set => _gameCreationData.Value = value;
    }

    public string GameEventContentType
    {
        get => _gameEventContentType.Value;
        set => _gameEventContentType.Value = value;
    }

    public string GameStartEventUri
    {
        get => _gameStartEventUri.Value;
        set => _gameStartEventUri.Value = value;
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

    public IDictionary<string, string> MeshAttribs
    {
        get => _meshAttribs.Value;
        set => _meshAttribs.Value = value;
    }

    public bool ServerNotResetable
    {
        get => _serverNotResetable.Value;
        set => _serverNotResetable.Value = value;
    }

    public IList<ushort> SlotCapacities
    {
        get => _slotCapacities.Value;
        set => _slotCapacities.Value = value;
    }

    public IDictionary<Blaze15SDK.Blaze.GameManager.SlotType, ushort> SlotCapacitiesMap
    {
        get => _slotCapacitiesMap.Value;
        set => _slotCapacitiesMap.Value = value;
    }

    public string PersistedGameId
    {
        get => _persistedGameId.Value;
        set => _persistedGameId.Value = value;
    }

    public byte[] PersistedGameIdSecret
    {
        get => _persistedGameIdSecret.Value;
        set => _persistedGameIdSecret.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.PlayerJoinData? PlayerJoinData
    {
        get => _playerJoinData.Value;
        set => _playerJoinData.Value = value;
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

}
