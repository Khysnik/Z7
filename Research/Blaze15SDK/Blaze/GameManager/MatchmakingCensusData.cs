using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class MatchmakingCensusData : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("PlayerMatchmakingRatePerPingsite", "mPlayerMatchmakingRatePerPingsite", 0xC2DC8000, TdfType.Map, 0, true), // Tag: PMR
        new TdfMemberInfo("PlayerMatchmakingRatePerPingsiteGroup", "mPlayerMatchmakingRatePerPingsiteGroup", 0xC2DCA700, TdfType.Map, 1, true), // Tag: PMRG
        new TdfMemberInfo("EstimatedTimeToMatchPerPingsiteGroup", "mEstimatedTimeToMatchPerPingsiteGroup", 0xC34D2700, TdfType.Map, 2, true), // Tag: PTTG
        new TdfMemberInfo("EstimatedTimeToMatchPerPingsite", "mEstimatedTimeToMatchPerPingsite", 0xC34D2D00, TdfType.Map, 3, true), // Tag: PTTM
        new TdfMemberInfo("ScenarioMatchmakingData", "mScenarioMatchmakingData", 0xCE4D2100, TdfType.Map, 4, true), // Tag: SDTA
    ];
    private ITdfMember[] __members;

    private TdfMap<string, float> _playerMatchmakingRatePerPingsite = new(__typeInfos[0]);
    private TdfMap<string, IDictionary<string, float>> _playerMatchmakingRatePerPingsiteGroup = new(__typeInfos[1]);
    private TdfMap<string, IDictionary<string, long>> _estimatedTimeToMatchPerPingsiteGroup = new(__typeInfos[2]);
    private TdfMap<string, long> _estimatedTimeToMatchPerPingsite = new(__typeInfos[3]);
    private TdfMap<string, Blaze15SDK.Blaze.GameManager.ScenarioMatchmakingCensusData> _scenarioMatchmakingData = new(__typeInfos[4]);

    public MatchmakingCensusData()
    {
        __members = [ _playerMatchmakingRatePerPingsite, _playerMatchmakingRatePerPingsiteGroup, _estimatedTimeToMatchPerPingsiteGroup, _estimatedTimeToMatchPerPingsite, _scenarioMatchmakingData ];
    }

    public override Tdf CreateNew() => new MatchmakingCensusData();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "MatchmakingCensusData";
    public override string GetFullClassName() => "Blaze::GameManager::MatchmakingCensusData";

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

    public IDictionary<string, Blaze15SDK.Blaze.GameManager.ScenarioMatchmakingCensusData> ScenarioMatchmakingData
    {
        get => _scenarioMatchmakingData.Value;
        set => _scenarioMatchmakingData.Value = value;
    }

}
