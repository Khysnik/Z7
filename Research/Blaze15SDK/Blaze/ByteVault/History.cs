using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.ByteVault;

public class History : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("MaxHistoryRecords", "mMaxHistoryRecords", 0xA23BB400, TdfType.UInt64, 0, true), // Tag: HCNT
        new TdfMemberInfo("MinHistoryGap", "mMinHistoryGap", 0xA2787000, TdfType.UInt64, 1, true), // Tag: HGAP
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _maxHistoryRecords = new(__typeInfos[0]);
    private TdfUInt64 _minHistoryGap = new(__typeInfos[1]);

    public History()
    {
        __members = [ _maxHistoryRecords, _minHistoryGap ];
    }

    public override Tdf CreateNew() => new History();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "History";
    public override string GetFullClassName() => "Blaze::ByteVault::History";

    public ulong MaxHistoryRecords
    {
        get => _maxHistoryRecords.Value;
        set => _maxHistoryRecords.Value = value;
    }

    public ulong MinHistoryGap
    {
        get => _minHistoryGap.Value;
        set => _minHistoryGap.Value = value;
    }

}
