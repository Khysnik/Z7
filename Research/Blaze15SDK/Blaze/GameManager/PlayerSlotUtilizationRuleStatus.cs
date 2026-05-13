using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class PlayerSlotUtilizationRuleStatus : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("MaxPercentFullAccepted", "mMaxPercentFullAccepted", 0xC2D87800, TdfType.UInt8, 0, true), // Tag: PMAX
        new TdfMemberInfo("MinPercentFullAccepted", "mMinPercentFullAccepted", 0xC2DA6E00, TdfType.UInt8, 1, true), // Tag: PMIN
    ];
    private ITdfMember[] __members;

    private TdfUInt8 _maxPercentFullAccepted = new(__typeInfos[0]);
    private TdfUInt8 _minPercentFullAccepted = new(__typeInfos[1]);

    public PlayerSlotUtilizationRuleStatus()
    {
        __members = [ _maxPercentFullAccepted, _minPercentFullAccepted ];
    }

    public override Tdf CreateNew() => new PlayerSlotUtilizationRuleStatus();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "PlayerSlotUtilizationRuleStatus";
    public override string GetFullClassName() => "Blaze::GameManager::PlayerSlotUtilizationRuleStatus";

    public byte MaxPercentFullAccepted
    {
        get => _maxPercentFullAccepted.Value;
        set => _maxPercentFullAccepted.Value = value;
    }

    public byte MinPercentFullAccepted
    {
        get => _minPercentFullAccepted.Value;
        set => _minPercentFullAccepted.Value = value;
    }

}
