using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class PlayerCountRuleCriteria : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("IsSingleGroupMatch", "mIsSingleGroupMatch", 0xA73CE700, TdfType.UInt8, 0, true), // Tag: ISSG
        new TdfMemberInfo("MaxPlayerCount", "mMaxPlayerCount", 0xC2387000, TdfType.UInt16, 1, true), // Tag: PCAP
        new TdfMemberInfo("DesiredPlayerCount", "mDesiredPlayerCount", 0xC23BB400, TdfType.UInt16, 2, true), // Tag: PCNT
        new TdfMemberInfo("MinPlayerCount", "mMinPlayerCount", 0xC2DA6E00, TdfType.UInt16, 3, true), // Tag: PMIN
        new TdfMemberInfo("RangeOffsetListName", "mRangeOffsetListName", 0xD28B2400, TdfType.String, 4, true), // Tag: THLD
    ];
    private ITdfMember[] __members;

    private TdfUInt8 _isSingleGroupMatch = new(__typeInfos[0]);
    private TdfUInt16 _maxPlayerCount = new(__typeInfos[1]);
    private TdfUInt16 _desiredPlayerCount = new(__typeInfos[2]);
    private TdfUInt16 _minPlayerCount = new(__typeInfos[3]);
    private TdfString _rangeOffsetListName = new(__typeInfos[4]);

    public PlayerCountRuleCriteria()
    {
        __members = [ _isSingleGroupMatch, _maxPlayerCount, _desiredPlayerCount, _minPlayerCount, _rangeOffsetListName ];
    }

    public override Tdf CreateNew() => new PlayerCountRuleCriteria();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "PlayerCountRuleCriteria";
    public override string GetFullClassName() => "Blaze::GameManager::PlayerCountRuleCriteria";

    public byte IsSingleGroupMatch
    {
        get => _isSingleGroupMatch.Value;
        set => _isSingleGroupMatch.Value = value;
    }

    public ushort MaxPlayerCount
    {
        get => _maxPlayerCount.Value;
        set => _maxPlayerCount.Value = value;
    }

    public ushort DesiredPlayerCount
    {
        get => _desiredPlayerCount.Value;
        set => _desiredPlayerCount.Value = value;
    }

    public ushort MinPlayerCount
    {
        get => _minPlayerCount.Value;
        set => _minPlayerCount.Value = value;
    }

    public string RangeOffsetListName
    {
        get => _rangeOffsetListName.Value;
        set => _rangeOffsetListName.Value = value;
    }

}
