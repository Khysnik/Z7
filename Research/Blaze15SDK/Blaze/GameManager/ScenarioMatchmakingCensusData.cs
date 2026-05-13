using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class ScenarioMatchmakingCensusData : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("GlobalPlayerMatchmakingRate", "mGlobalPlayerMatchmakingRate", 0x9F0B7200, TdfType.Float, 0, true), // Tag: GPMR
        new TdfMemberInfo("GlobalEstimatedTimeToMatch", "mGlobalEstimatedTimeToMatch", 0x9F4D2D00, TdfType.Int64, 1, true), // Tag: GTTM
        new TdfMemberInfo("MatchmakingSessionPerPingSite", "mMatchmakingSessionPerPingSite", 0xB73C3000, TdfType.Map, 2, true), // Tag: MSPP
        new TdfMemberInfo("NumOfMatchmakingSession", "mNumOfMatchmakingSession", 0xBB5B7300, TdfType.UInt32, 3, true), // Tag: NUMS
        new TdfMemberInfo("PlayerMatchmakingRatePerPingsite", "mPlayerMatchmakingRatePerPingsite", 0xC2DC8000, TdfType.Map, 4, true), // Tag: PMR
        new TdfMemberInfo("PlayerMatchmakingRatePerPingsiteGroup", "mPlayerMatchmakingRatePerPingsiteGroup", 0xC2DCA700, TdfType.Map, 5, true), // Tag: PMRG
        new TdfMemberInfo("EstimatedTimeToMatchPerPingsiteGroup", "mEstimatedTimeToMatchPerPingsiteGroup", 0xC34D2700, TdfType.Map, 6, true), // Tag: PTTG
        new TdfMemberInfo("EstimatedTimeToMatchPerPingsite", "mEstimatedTimeToMatchPerPingsite", 0xC34D2D00, TdfType.Map, 7, true), // Tag: PTTM
        new TdfMemberInfo("ScenarioName", "mScenarioName", 0xCEE86D00, TdfType.String, 8, true), // Tag: SNAM
    ];
    private ITdfMember[] __members;

    private TdfFloat _globalPlayerMatchmakingRate = new(__typeInfos[0]);
    private TdfInt64 _globalEstimatedTimeToMatch = new(__typeInfos[1]);
    private TdfMap<string, uint> _matchmakingSessionPerPingSite = new(__typeInfos[2]);
    private TdfUInt32 _numOfMatchmakingSession = new(__typeInfos[3]);
    private TdfMap<string, float> _playerMatchmakingRatePerPingsite = new(__typeInfos[4]);
    private TdfMap<string, IDictionary<string, float>> _playerMatchmakingRatePerPingsiteGroup = new(__typeInfos[5]);
    private TdfMap<string, IDictionary<string, long>> _estimatedTimeToMatchPerPingsiteGroup = new(__typeInfos[6]);
    private TdfMap<string, long> _estimatedTimeToMatchPerPingsite = new(__typeInfos[7]);
    private TdfString _scenarioName = new(__typeInfos[8]);

    public ScenarioMatchmakingCensusData()
    {
        __members = [ _globalPlayerMatchmakingRate, _globalEstimatedTimeToMatch, _matchmakingSessionPerPingSite, _numOfMatchmakingSession, _playerMatchmakingRatePerPingsite, _playerMatchmakingRatePerPingsiteGroup, _estimatedTimeToMatchPerPingsiteGroup, _estimatedTimeToMatchPerPingsite, _scenarioName ];
    }

    public override Tdf CreateNew() => new ScenarioMatchmakingCensusData();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ScenarioMatchmakingCensusData";
    public override string GetFullClassName() => "Blaze::GameManager::ScenarioMatchmakingCensusData";

    public float GlobalPlayerMatchmakingRate
    {
        get => _globalPlayerMatchmakingRate.Value;
        set => _globalPlayerMatchmakingRate.Value = value;
    }

    public long GlobalEstimatedTimeToMatch
    {
        get => _globalEstimatedTimeToMatch.Value;
        set => _globalEstimatedTimeToMatch.Value = value;
    }

    public IDictionary<string, uint> MatchmakingSessionPerPingSite
    {
        get => _matchmakingSessionPerPingSite.Value;
        set => _matchmakingSessionPerPingSite.Value = value;
    }

    public uint NumOfMatchmakingSession
    {
        get => _numOfMatchmakingSession.Value;
        set => _numOfMatchmakingSession.Value = value;
    }

    public IDictionary<string, float> PlayerMatchmakingRatePerPingsite
    {
        get => _playerMatchmakingRatePerPingsite.Value;
        set => _playerMatchmakingRatePerPingsite.Value = value;
    }

    public IDictionary<string, IDictionary<string, float>> PlayerMatchmakingRatePerPingsiteGroup
    {
        get => _playerMatchmakingRatePerPingsiteGroup.Value;
        set => _playerMatchmakingRatePerPingsiteGroup.Value = value;
    }

    public IDictionary<string, IDictionary<string, long>> EstimatedTimeToMatchPerPingsiteGroup
    {
        get => _estimatedTimeToMatchPerPingsiteGroup.Value;
        set => _estimatedTimeToMatchPerPingsiteGroup.Value = value;
    }

    public IDictionary<string, long> EstimatedTimeToMatchPerPingsite
    {
        get => _estimatedTimeToMatchPerPingsite.Value;
        set => _estimatedTimeToMatchPerPingsite.Value = value;
    }

    public string ScenarioName
    {
        get => _scenarioName.Value;
        set => _scenarioName.Value = value;
    }

}
