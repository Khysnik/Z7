using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class PlayerCountRuleStatus : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("MaxPlayerCountAccepted", "mMaxPlayerCountAccepted", 0xC2D87800, TdfType.UInt16, 0, true), // Tag: PMAX
        new TdfMemberInfo("MinPlayerCountAccepted", "mMinPlayerCountAccepted", 0xC2DA6E00, TdfType.UInt16, 1, true), // Tag: PMIN
    ];
    private ITdfMember[] __members;

    private TdfUInt16 _maxPlayerCountAccepted = new(__typeInfos[0]);
    private TdfUInt16 _minPlayerCountAccepted = new(__typeInfos[1]);

    public PlayerCountRuleStatus()
    {
        __members = [ _maxPlayerCountAccepted, _minPlayerCountAccepted ];
    }

    public override Tdf CreateNew() => new PlayerCountRuleStatus();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "PlayerCountRuleStatus";
    public override string GetFullClassName() => "Blaze::GameManager::PlayerCountRuleStatus";

    public ushort MaxPlayerCountAccepted
    {
        get => _maxPlayerCountAccepted.Value;
        set => _maxPlayerCountAccepted.Value = value;
    }

    public ushort MinPlayerCountAccepted
    {
        get => _minPlayerCountAccepted.Value;
        set => _minPlayerCountAccepted.Value = value;
    }

}
