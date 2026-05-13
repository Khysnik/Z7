using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class DebugRuleResult : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Conditions", "mConditions", 0x8EFBA400, TdfType.List, 0, true), // Tag: COND
        new TdfMemberInfo("FitScore", "mFitScore", 0x9A9D0000, TdfType.UInt32, 1, true), // Tag: FIT
        new TdfMemberInfo("MaxFitScore", "mMaxFitScore", 0xB66A7400, TdfType.UInt32, 2, true), // Tag: MFIT
        new TdfMemberInfo("Result", "mResult", 0xCA5CC000, TdfType.Enum, 3, true), // Tag: RES
    ];
    private ITdfMember[] __members;

    private TdfList<string> _conditions = new(__typeInfos[0]);
    private TdfUInt32 _fitScore = new(__typeInfos[1]);
    private TdfUInt32 _maxFitScore = new(__typeInfos[2]);
    private TdfEnum<Blaze15SDK.Blaze.GameManager.DebugEvaluationResult> _result = new(__typeInfos[3]);

    public DebugRuleResult()
    {
        __members = [ _conditions, _fitScore, _maxFitScore, _result ];
    }

    public override Tdf CreateNew() => new DebugRuleResult();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "DebugRuleResult";
    public override string GetFullClassName() => "Blaze::GameManager::DebugRuleResult";

    public IList<string> Conditions
    {
        get => _conditions.Value;
        set => _conditions.Value = value;
    }

    public uint FitScore
    {
        get => _fitScore.Value;
        set => _fitScore.Value = value;
    }

    public uint MaxFitScore
    {
        get => _maxFitScore.Value;
        set => _maxFitScore.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.DebugEvaluationResult Result
    {
        get => _result.Value;
        set => _result.Value = value;
    }

}
