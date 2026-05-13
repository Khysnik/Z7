using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class PlayerSlotUtilizationRuleCriteria : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("DesiredPercentFull", "mDesiredPercentFull", 0x925CF000, TdfType.UInt8, 0, true), // Tag: DESP
        new TdfMemberInfo("MaxPercentFull", "mMaxPercentFull", 0xB61E3000, TdfType.UInt8, 1, true), // Tag: MAXP
        new TdfMemberInfo("MinPercentFull", "mMinPercentFull", 0xB69BB000, TdfType.UInt8, 2, true), // Tag: MINP
        new TdfMemberInfo("RangeOffsetListName", "mRangeOffsetListName", 0xD28B2400, TdfType.String, 3, true), // Tag: THLD
    ];
    private ITdfMember[] __members;

    private TdfUInt8 _desiredPercentFull = new(__typeInfos[0]);
    private TdfUInt8 _maxPercentFull = new(__typeInfos[1]);
    private TdfUInt8 _minPercentFull = new(__typeInfos[2]);
    private TdfString _rangeOffsetListName = new(__typeInfos[3]);

    public PlayerSlotUtilizationRuleCriteria()
    {
        __members = [ _desiredPercentFull, _maxPercentFull, _minPercentFull, _rangeOffsetListName ];
    }

    public override Tdf CreateNew() => new PlayerSlotUtilizationRuleCriteria();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "PlayerSlotUtilizationRuleCriteria";
    public override string GetFullClassName() => "Blaze::GameManager::PlayerSlotUtilizationRuleCriteria";

    public byte DesiredPercentFull
    {
        get => _desiredPercentFull.Value;
        set => _desiredPercentFull.Value = value;
    }

    public byte MaxPercentFull
    {
        get => _maxPercentFull.Value;
        set => _maxPercentFull.Value = value;
    }

    public byte MinPercentFull
    {
        get => _minPercentFull.Value;
        set => _minPercentFull.Value = value;
    }

    public string RangeOffsetListName
    {
        get => _rangeOffsetListName.Value;
        set => _rangeOffsetListName.Value = value;
    }

}
