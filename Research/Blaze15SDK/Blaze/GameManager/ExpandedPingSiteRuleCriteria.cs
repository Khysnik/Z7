using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class ExpandedPingSiteRuleCriteria : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("LatencyCalcMethod", "mLatencyCalcMethod", 0xB23B4000, TdfType.Enum, 0, true), // Tag: LCM
        new TdfMemberInfo("PingSiteSelectionMethod", "mPingSiteSelectionMethod", 0xC33CED00, TdfType.Enum, 1, true), // Tag: PSSM
        new TdfMemberInfo("PingSiteWhitelist", "mPingSiteWhitelist", 0xC33DEC00, TdfType.List, 2, true), // Tag: PSWL
        new TdfMemberInfo("SessionMatchCalcMethod", "mSessionMatchCalcMethod", 0xCED8ED00, TdfType.Enum, 3, true), // Tag: SMCM
        new TdfMemberInfo("RangeOffsetListName", "mRangeOffsetListName", 0xD28B2400, TdfType.String, 4, true), // Tag: THLD
    ];
    private ITdfMember[] __members;

    private TdfEnum<Blaze15SDK.Blaze.GameManager.LatencyCalcMethod> _latencyCalcMethod = new(__typeInfos[0]);
    private TdfEnum<Blaze15SDK.Blaze.GameManager.PingSiteSelectionMethod> _pingSiteSelectionMethod = new(__typeInfos[1]);
    private TdfList<string> _pingSiteWhitelist = new(__typeInfos[2]);
    private TdfEnum<Blaze15SDK.Blaze.GameManager.SessionMatchCalcMethod> _sessionMatchCalcMethod = new(__typeInfos[3]);
    private TdfString _rangeOffsetListName = new(__typeInfos[4]);

    public ExpandedPingSiteRuleCriteria()
    {
        __members = [ _latencyCalcMethod, _pingSiteSelectionMethod, _pingSiteWhitelist, _sessionMatchCalcMethod, _rangeOffsetListName ];
    }

    public override Tdf CreateNew() => new ExpandedPingSiteRuleCriteria();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ExpandedPingSiteRuleCriteria";
    public override string GetFullClassName() => "Blaze::GameManager::ExpandedPingSiteRuleCriteria";

    public Blaze15SDK.Blaze.GameManager.LatencyCalcMethod LatencyCalcMethod
    {
        get => _latencyCalcMethod.Value;
        set => _latencyCalcMethod.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.PingSiteSelectionMethod PingSiteSelectionMethod
    {
        get => _pingSiteSelectionMethod.Value;
        set => _pingSiteSelectionMethod.Value = value;
    }

    public IList<string> PingSiteWhitelist
    {
        get => _pingSiteWhitelist.Value;
        set => _pingSiteWhitelist.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.SessionMatchCalcMethod SessionMatchCalcMethod
    {
        get => _sessionMatchCalcMethod.Value;
        set => _sessionMatchCalcMethod.Value = value;
    }

    public string RangeOffsetListName
    {
        get => _rangeOffsetListName.Value;
        set => _rangeOffsetListName.Value = value;
    }

}
