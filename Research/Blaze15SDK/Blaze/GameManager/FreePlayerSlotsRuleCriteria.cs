using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class FreePlayerSlotsRuleCriteria : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("MaxFreePlayerSlots", "mMaxFreePlayerSlots", 0xB61E3300, TdfType.UInt16, 0, true), // Tag: MAXS
        new TdfMemberInfo("MinFreePlayerSlots", "mMinFreePlayerSlots", 0xB69BB300, TdfType.UInt16, 1, true), // Tag: MINS
    ];
    private ITdfMember[] __members;

    private TdfUInt16 _maxFreePlayerSlots = new(__typeInfos[0]);
    private TdfUInt16 _minFreePlayerSlots = new(__typeInfos[1]);

    public FreePlayerSlotsRuleCriteria()
    {
        __members = [ _maxFreePlayerSlots, _minFreePlayerSlots ];
    }

    public override Tdf CreateNew() => new FreePlayerSlotsRuleCriteria();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "FreePlayerSlotsRuleCriteria";
    public override string GetFullClassName() => "Blaze::GameManager::FreePlayerSlotsRuleCriteria";

    public ushort MaxFreePlayerSlots
    {
        get => _maxFreePlayerSlots.Value;
        set => _maxFreePlayerSlots.Value = value;
    }

    public ushort MinFreePlayerSlots
    {
        get => _minFreePlayerSlots.Value;
        set => _minFreePlayerSlots.Value = value;
    }

}
