using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class ScenarioInfo : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ScenarioAttributes", "mScenarioAttributes", 0x874D3200, TdfType.Map, 0, true), // Tag: ATTR
        new TdfMemberInfo("OriginatingScenarioId", "mOriginatingScenarioId", 0xBF3A6400, TdfType.UInt64, 1, true), // Tag: OSID
        new TdfMemberInfo("ScenarioName", "mScenarioName", 0xCE396E00, TdfType.String, 2, true), // Tag: SCEN
        new TdfMemberInfo("ScenarioVersion", "mScenarioVersion", 0xCE397600, TdfType.UInt32, 3, true), // Tag: SCEV
        new TdfMemberInfo("ScenarioVariant", "mScenarioVariant", 0xCE3DA100, TdfType.String, 4, true), // Tag: SCVA
        new TdfMemberInfo("SubSessionName", "mSubSessionName", 0xCF58AE00, TdfType.String, 5, true), // Tag: SUBN
    ];
    private ITdfMember[] __members;

    private TdfMap<string, object> _scenarioAttributes = new(__typeInfos[0]);
    private TdfUInt64 _originatingScenarioId = new(__typeInfos[1]);
    private TdfString _scenarioName = new(__typeInfos[2]);
    private TdfUInt32 _scenarioVersion = new(__typeInfos[3]);
    private TdfString _scenarioVariant = new(__typeInfos[4]);
    private TdfString _subSessionName = new(__typeInfos[5]);

    public ScenarioInfo()
    {
        __members = [ _scenarioAttributes, _originatingScenarioId, _scenarioName, _scenarioVersion, _scenarioVariant, _subSessionName ];
    }

    public override Tdf CreateNew() => new ScenarioInfo();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ScenarioInfo";
    public override string GetFullClassName() => "Blaze::GameManager::ScenarioInfo";

    public IDictionary<string, object> ScenarioAttributes
    {
        get => _scenarioAttributes.Value;
        set => _scenarioAttributes.Value = value;
    }

    public ulong OriginatingScenarioId
    {
        get => _originatingScenarioId.Value;
        set => _originatingScenarioId.Value = value;
    }

    public string ScenarioName
    {
        get => _scenarioName.Value;
        set => _scenarioName.Value = value;
    }

    public uint ScenarioVersion
    {
        get => _scenarioVersion.Value;
        set => _scenarioVersion.Value = value;
    }

    public string ScenarioVariant
    {
        get => _scenarioVariant.Value;
        set => _scenarioVariant.Value = value;
    }

    public string SubSessionName
    {
        get => _subSessionName.Value;
        set => _subSessionName.Value = value;
    }

}
