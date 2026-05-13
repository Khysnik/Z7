using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class MatchmakingAsyncStatus : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("CreateGameStatus", "mCreateGameStatus", 0x8E7CC000, TdfType.Struct, 0, true), // Tag: CGS
        new TdfMemberInfo("VariableCustomAsyncStatus", "mVariableCustomAsyncStatus", 0x8F687200, TdfType.Map, 1, true), // Tag: CVAR
        new TdfMemberInfo("ExpandedPingSiteRuleStatus", "mExpandedPingSiteRuleStatus", 0x970CF200, TdfType.Struct, 2, true), // Tag: EPSR
        new TdfMemberInfo("FindGameStatus", "mFindGameStatus", 0x9A7CC000, TdfType.Struct, 3, true), // Tag: FGS
        new TdfMemberInfo("GameAttributeRuleStatusMap", "mGameAttributeRuleStatusMap", 0x9E1CED00, TdfType.Map, 4, true), // Tag: GASM
        new TdfMemberInfo("GeoLocationRuleStatus", "mGeoLocationRuleStatus", 0x9E5BF300, TdfType.Struct, 5, true), // Tag: GEOS
        new TdfMemberInfo("HostBalanceRuleStatus", "mHostBalanceRuleStatus", 0xA22CA400, TdfType.Struct, 6, true), // Tag: HBRD
        new TdfMemberInfo("HostViabilityRuleStatus", "mHostViabilityRuleStatus", 0xA36CA400, TdfType.Struct, 7, true), // Tag: HVRD
        new TdfMemberInfo("PlayerAttributeRuleStatusMap", "mPlayerAttributeRuleStatusMap", 0xC21CED00, TdfType.Map, 8, true), // Tag: PASM
        new TdfMemberInfo("PlayerCountRuleStatus", "mPlayerCountRuleStatus", 0xC2C8EE00, TdfType.Struct, 9, true), // Tag: PLCN
        new TdfMemberInfo("PlayerSlotUtilizationRuleStatus", "mPlayerSlotUtilizationRuleStatus", 0xC2CD7400, TdfType.Struct, 10, true), // Tag: PLUT
        new TdfMemberInfo("RankRuleStatus", "mRankRuleStatus", 0xCB292100, TdfType.Struct, 11, true), // Tag: RRDA
        new TdfMemberInfo("QualityFactorScores", "mQualityFactorScores", 0xCE3BF200, TdfType.List, 12, true), // Tag: SCOR
        new TdfMemberInfo("TeamBalanceRuleStatus", "mTeamBalanceRuleStatus", 0xD22CB300, TdfType.Struct, 13, true), // Tag: TBRS
        new TdfMemberInfo("TeamCompositionRuleStatus", "mTeamCompositionRuleStatus", 0xD23C3300, TdfType.Struct, 14, true), // Tag: TCPS
        new TdfMemberInfo("TeamMinSizeRuleStatus", "mTeamMinSizeRuleStatus", 0xD2DCF300, TdfType.Struct, 15, true), // Tag: TMSS
        new TdfMemberInfo("TotalPlayerSlotsRuleStatus", "mTotalPlayerSlotsRuleStatus", 0xD2FD3300, TdfType.Struct, 16, true), // Tag: TOTS
        new TdfMemberInfo("TeamUEDPositionParityRuleStatus", "mTeamUEDPositionParityRuleStatus", 0xD30C3300, TdfType.Struct, 17, true), // Tag: TPPS
        new TdfMemberInfo("TeamUEDBalanceRuleStatus", "mTeamUEDBalanceRuleStatus", 0xD358B300, TdfType.Struct, 18, true), // Tag: TUBS
        new TdfMemberInfo("UEDRuleStatusMap", "mUEDRuleStatusMap", 0xD6593300, TdfType.Map, 19, true), // Tag: UEDS
        new TdfMemberInfo("VirtualGameRuleStatus", "mVirtualGameRuleStatus", 0xDA7CB300, TdfType.Struct, 20, true), // Tag: VGRS
    ];
    private ITdfMember[] __members;

    private TdfStruct<Blaze15SDK.Blaze.GameManager.CreateGameStatus?> _createGameStatus = new(__typeInfos[0]);
    private TdfMap<string, object?> _variableCustomAsyncStatus = new(__typeInfos[1]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.ExpandedPingSiteRuleStatus?> _expandedPingSiteRuleStatus = new(__typeInfos[2]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.FindGameStatus?> _findGameStatus = new(__typeInfos[3]);
    private TdfMap<string, Blaze15SDK.Blaze.GameManager.GameAttributeRuleStatus> _gameAttributeRuleStatusMap = new(__typeInfos[4]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.GeoLocationRuleStatus?> _geoLocationRuleStatus = new(__typeInfos[5]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.HostBalanceRuleStatus?> _hostBalanceRuleStatus = new(__typeInfos[6]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.HostViabilityRuleStatus?> _hostViabilityRuleStatus = new(__typeInfos[7]);
    private TdfMap<string, Blaze15SDK.Blaze.GameManager.PlayerAttributeRuleStatus> _playerAttributeRuleStatusMap = new(__typeInfos[8]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.PlayerCountRuleStatus?> _playerCountRuleStatus = new(__typeInfos[9]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.PlayerSlotUtilizationRuleStatus?> _playerSlotUtilizationRuleStatus = new(__typeInfos[10]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.RankRuleStatus?> _rankRuleStatus = new(__typeInfos[11]);
    private TdfList<float> _qualityFactorScores = new(__typeInfos[12]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.TeamBalanceRuleStatus?> _teamBalanceRuleStatus = new(__typeInfos[13]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.TeamCompositionRuleStatus?> _teamCompositionRuleStatus = new(__typeInfos[14]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.TeamMinSizeRuleStatus?> _teamMinSizeRuleStatus = new(__typeInfos[15]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.TotalPlayerSlotsRuleStatus?> _totalPlayerSlotsRuleStatus = new(__typeInfos[16]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.TeamUEDPositionParityRuleStatus?> _teamUEDPositionParityRuleStatus = new(__typeInfos[17]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.TeamUEDBalanceRuleStatus?> _teamUEDBalanceRuleStatus = new(__typeInfos[18]);
    private TdfMap<string, Blaze15SDK.Blaze.GameManager.UEDRuleStatus> _uEDRuleStatusMap = new(__typeInfos[19]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.VirtualGameRuleStatus?> _virtualGameRuleStatus = new(__typeInfos[20]);

    public MatchmakingAsyncStatus()
    {
        __members = [ _createGameStatus, _variableCustomAsyncStatus, _expandedPingSiteRuleStatus, _findGameStatus, _gameAttributeRuleStatusMap, _geoLocationRuleStatus, _hostBalanceRuleStatus, _hostViabilityRuleStatus, _playerAttributeRuleStatusMap, _playerCountRuleStatus, _playerSlotUtilizationRuleStatus, _rankRuleStatus, _qualityFactorScores, _teamBalanceRuleStatus, _teamCompositionRuleStatus, _teamMinSizeRuleStatus, _totalPlayerSlotsRuleStatus, _teamUEDPositionParityRuleStatus, _teamUEDBalanceRuleStatus, _uEDRuleStatusMap, _virtualGameRuleStatus ];
    }

    public override Tdf CreateNew() => new MatchmakingAsyncStatus();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "MatchmakingAsyncStatus";
    public override string GetFullClassName() => "Blaze::GameManager::MatchmakingAsyncStatus";

    public Blaze15SDK.Blaze.GameManager.CreateGameStatus? CreateGameStatus
    {
        get => _createGameStatus.Value;
        set => _createGameStatus.Value = value;
    }

    public IDictionary<string, object?> VariableCustomAsyncStatus
    {
        get => _variableCustomAsyncStatus.Value;
        set => _variableCustomAsyncStatus.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.ExpandedPingSiteRuleStatus? ExpandedPingSiteRuleStatus
    {
        get => _expandedPingSiteRuleStatus.Value;
        set => _expandedPingSiteRuleStatus.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.FindGameStatus? FindGameStatus
    {
        get => _findGameStatus.Value;
        set => _findGameStatus.Value = value;
    }

    public IDictionary<string, Blaze15SDK.Blaze.GameManager.GameAttributeRuleStatus> GameAttributeRuleStatusMap
    {
        get => _gameAttributeRuleStatusMap.Value;
        set => _gameAttributeRuleStatusMap.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.GeoLocationRuleStatus? GeoLocationRuleStatus
    {
        get => _geoLocationRuleStatus.Value;
        set => _geoLocationRuleStatus.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.HostBalanceRuleStatus? HostBalanceRuleStatus
    {
        get => _hostBalanceRuleStatus.Value;
        set => _hostBalanceRuleStatus.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.HostViabilityRuleStatus? HostViabilityRuleStatus
    {
        get => _hostViabilityRuleStatus.Value;
        set => _hostViabilityRuleStatus.Value = value;
    }

    public IDictionary<string, Blaze15SDK.Blaze.GameManager.PlayerAttributeRuleStatus> PlayerAttributeRuleStatusMap
    {
        get => _playerAttributeRuleStatusMap.Value;
        set => _playerAttributeRuleStatusMap.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.PlayerCountRuleStatus? PlayerCountRuleStatus
    {
        get => _playerCountRuleStatus.Value;
        set => _playerCountRuleStatus.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.PlayerSlotUtilizationRuleStatus? PlayerSlotUtilizationRuleStatus
    {
        get => _playerSlotUtilizationRuleStatus.Value;
        set => _playerSlotUtilizationRuleStatus.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.RankRuleStatus? RankRuleStatus
    {
        get => _rankRuleStatus.Value;
        set => _rankRuleStatus.Value = value;
    }

    public IList<float> QualityFactorScores
    {
        get => _qualityFactorScores.Value;
        set => _qualityFactorScores.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.TeamBalanceRuleStatus? TeamBalanceRuleStatus
    {
        get => _teamBalanceRuleStatus.Value;
        set => _teamBalanceRuleStatus.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.TeamCompositionRuleStatus? TeamCompositionRuleStatus
    {
        get => _teamCompositionRuleStatus.Value;
        set => _teamCompositionRuleStatus.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.TeamMinSizeRuleStatus? TeamMinSizeRuleStatus
    {
        get => _teamMinSizeRuleStatus.Value;
        set => _teamMinSizeRuleStatus.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.TotalPlayerSlotsRuleStatus? TotalPlayerSlotsRuleStatus
    {
        get => _totalPlayerSlotsRuleStatus.Value;
        set => _totalPlayerSlotsRuleStatus.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.TeamUEDPositionParityRuleStatus? TeamUEDPositionParityRuleStatus
    {
        get => _teamUEDPositionParityRuleStatus.Value;
        set => _teamUEDPositionParityRuleStatus.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.TeamUEDBalanceRuleStatus? TeamUEDBalanceRuleStatus
    {
        get => _teamUEDBalanceRuleStatus.Value;
        set => _teamUEDBalanceRuleStatus.Value = value;
    }

    public IDictionary<string, Blaze15SDK.Blaze.GameManager.UEDRuleStatus> UEDRuleStatusMap
    {
        get => _uEDRuleStatusMap.Value;
        set => _uEDRuleStatusMap.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.VirtualGameRuleStatus? VirtualGameRuleStatus
    {
        get => _virtualGameRuleStatus.Value;
        set => _virtualGameRuleStatus.Value = value;
    }

}
