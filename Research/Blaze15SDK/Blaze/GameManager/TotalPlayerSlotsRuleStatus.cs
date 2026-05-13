using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class TotalPlayerSlotsRuleStatus : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("MaxTotalPlayerSlotsAccepted", "mMaxTotalPlayerSlotsAccepted", 0xC2D87800, TdfType.UInt16, 0, true), // Tag: PMAX
        new TdfMemberInfo("MinTotalPlayerSlotsAccepted", "mMinTotalPlayerSlotsAccepted", 0xC2DA6E00, TdfType.UInt16, 1, true), // Tag: PMIN
    ];
    private ITdfMember[] __members;

    private TdfUInt16 _maxTotalPlayerSlotsAccepted = new(__typeInfos[0]);
    private TdfUInt16 _minTotalPlayerSlotsAccepted = new(__typeInfos[1]);

    public TotalPlayerSlotsRuleStatus()
    {
        __members = [ _maxTotalPlayerSlotsAccepted, _minTotalPlayerSlotsAccepted ];
    }

    public override Tdf CreateNew() => new TotalPlayerSlotsRuleStatus();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "TotalPlayerSlotsRuleStatus";
    public override string GetFullClassName() => "Blaze::GameManager::TotalPlayerSlotsRuleStatus";

    public ushort MaxTotalPlayerSlotsAccepted
    {
        get => _maxTotalPlayerSlotsAccepted.Value;
        set => _maxTotalPlayerSlotsAccepted.Value = value;
    }

    public ushort MinTotalPlayerSlotsAccepted
    {
        get => _minTotalPlayerSlotsAccepted.Value;
        set => _minTotalPlayerSlotsAccepted.Value = value;
    }

}
