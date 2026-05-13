using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class TotalPlayerSlotsRuleCriteria : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("DesiredTotalPlayerSlots", "mDesiredTotalPlayerSlots", 0x925CF300, TdfType.UInt16, 0, true), // Tag: DESS
        new TdfMemberInfo("MaxTotalPlayerSlots", "mMaxTotalPlayerSlots", 0xB61E3300, TdfType.UInt16, 1, true), // Tag: MAXS
        new TdfMemberInfo("MinTotalPlayerSlots", "mMinTotalPlayerSlots", 0xB69BB300, TdfType.UInt16, 2, true), // Tag: MINS
        new TdfMemberInfo("RangeOffsetListName", "mRangeOffsetListName", 0xD28B2400, TdfType.String, 3, true), // Tag: THLD
    ];
    private ITdfMember[] __members;

    private TdfUInt16 _desiredTotalPlayerSlots = new(__typeInfos[0]);
    private TdfUInt16 _maxTotalPlayerSlots = new(__typeInfos[1]);
    private TdfUInt16 _minTotalPlayerSlots = new(__typeInfos[2]);
    private TdfString _rangeOffsetListName = new(__typeInfos[3]);

    public TotalPlayerSlotsRuleCriteria()
    {
        __members = [ _desiredTotalPlayerSlots, _maxTotalPlayerSlots, _minTotalPlayerSlots, _rangeOffsetListName ];
    }

    public override Tdf CreateNew() => new TotalPlayerSlotsRuleCriteria();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "TotalPlayerSlotsRuleCriteria";
    public override string GetFullClassName() => "Blaze::GameManager::TotalPlayerSlotsRuleCriteria";

    public ushort DesiredTotalPlayerSlots
    {
        get => _desiredTotalPlayerSlots.Value;
        set => _desiredTotalPlayerSlots.Value = value;
    }

    public ushort MaxTotalPlayerSlots
    {
        get => _maxTotalPlayerSlots.Value;
        set => _maxTotalPlayerSlots.Value = value;
    }

    public ushort MinTotalPlayerSlots
    {
        get => _minTotalPlayerSlots.Value;
        set => _minTotalPlayerSlots.Value = value;
    }

    public string RangeOffsetListName
    {
        get => _rangeOffsetListName.Value;
        set => _rangeOffsetListName.Value = value;
    }

}
