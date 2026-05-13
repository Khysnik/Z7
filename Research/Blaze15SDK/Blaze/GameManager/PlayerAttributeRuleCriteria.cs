using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class PlayerAttributeRuleCriteria : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("MinFitThresholdName", "mMinFitThresholdName", 0xD28B2400, TdfType.String, 0, true), // Tag: THLD
        new TdfMemberInfo("DesiredValues", "mDesiredValues", 0xDA1B3500, TdfType.List, 1, true), // Tag: VALU
    ];
    private ITdfMember[] __members;

    private TdfString _minFitThresholdName = new(__typeInfos[0]);
    private TdfList<string> _desiredValues = new(__typeInfos[1]);

    public PlayerAttributeRuleCriteria()
    {
        __members = [ _minFitThresholdName, _desiredValues ];
    }

    public override Tdf CreateNew() => new PlayerAttributeRuleCriteria();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "PlayerAttributeRuleCriteria";
    public override string GetFullClassName() => "Blaze::GameManager::PlayerAttributeRuleCriteria";

    public string MinFitThresholdName
    {
        get => _minFitThresholdName.Value;
        set => _minFitThresholdName.Value = value;
    }

    public IList<string> DesiredValues
    {
        get => _desiredValues.Value;
        set => _desiredValues.Value = value;
    }

}
