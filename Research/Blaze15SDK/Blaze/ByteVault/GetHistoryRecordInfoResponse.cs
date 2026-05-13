using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.ByteVault;

public class GetHistoryRecordInfoResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Records", "mRecords", 0xB29CF400, TdfType.List, 0, true), // Tag: LIST
        new TdfMemberInfo("TotalCount", "mTotalCount", 0xD2FD2C00, TdfType.UInt32, 1, true), // Tag: TOTL
    ];
    private ITdfMember[] __members;

    private TdfList<Blaze15SDK.Blaze.ByteVault.HistoryRecordInfo> _records = new(__typeInfos[0]);
    private TdfUInt32 _totalCount = new(__typeInfos[1]);

    public GetHistoryRecordInfoResponse()
    {
        __members = [ _records, _totalCount ];
    }

    public override Tdf CreateNew() => new GetHistoryRecordInfoResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetHistoryRecordInfoResponse";
    public override string GetFullClassName() => "Blaze::ByteVault::GetHistoryRecordInfoResponse";

    public IList<Blaze15SDK.Blaze.ByteVault.HistoryRecordInfo> Records
    {
        get => _records.Value;
        set => _records.Value = value;
    }

    public uint TotalCount
    {
        get => _totalCount.Value;
        set => _totalCount.Value = value;
    }

}
