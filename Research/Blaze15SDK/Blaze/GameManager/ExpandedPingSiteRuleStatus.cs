using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class ExpandedPingSiteRuleStatus : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("MaxLatency", "mMaxLatency", 0xB6C87400, TdfType.UInt32, 0, true), // Tag: MLAT
        new TdfMemberInfo("MatchedValues", "mMatchedValues", 0xDA1B3500, TdfType.List, 1, true), // Tag: VALU
    ];
    private ITdfMember[] __members;

    private TdfUInt32 _maxLatency = new(__typeInfos[0]);
    private TdfList<string> _matchedValues = new(__typeInfos[1]);

    public ExpandedPingSiteRuleStatus()
    {
        __members = [ _maxLatency, _matchedValues ];
    }

    public override Tdf CreateNew() => new ExpandedPingSiteRuleStatus();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ExpandedPingSiteRuleStatus";
    public override string GetFullClassName() => "Blaze::GameManager::ExpandedPingSiteRuleStatus";

    public uint MaxLatency
    {
        get => _maxLatency.Value;
        set => _maxLatency.Value = value;
    }

    public IList<string> MatchedValues
    {
        get => _matchedValues.Value;
        set => _matchedValues.Value = value;
    }

}
