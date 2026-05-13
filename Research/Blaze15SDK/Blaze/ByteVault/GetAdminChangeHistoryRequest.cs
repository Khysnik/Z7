using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.ByteVault;

public class GetAdminChangeHistoryRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("AdminEmail", "mAdminEmail", 0x864B6E00, TdfType.String, 0, true), // Tag: ADMN
        new TdfMemberInfo("AuthCredentials", "mAuthCredentials", 0x875D2800, TdfType.Struct, 1, true), // Tag: AUTH
        new TdfMemberInfo("CategoryName", "mCategoryName", 0x8E1D0000, TdfType.String, 2, true), // Tag: CAT
        new TdfMemberInfo("Context", "mContext", 0x8F4E3400, TdfType.String, 3, true), // Tag: CTXT
        new TdfMemberInfo("EndDate", "mEndDate", 0x96E90000, TdfType.Int64, 4, true), // Tag: END
        new TdfMemberInfo("MaxResultCount", "mMaxResultCount", 0xB78CA300, TdfType.UInt32, 5, true), // Tag: MXRC
        new TdfMemberInfo("Offset", "mOffset", 0xBE69B300, TdfType.UInt32, 6, true), // Tag: OFFS
        new TdfMemberInfo("RecordName", "mRecordName", 0xCA58C000, TdfType.String, 7, true), // Tag: REC
        new TdfMemberInfo("StartDate", "mStartDate", 0xCF4CB400, TdfType.Int64, 8, true), // Tag: STRT
        new TdfMemberInfo("Action", "mAction", 0xD39C2500, TdfType.Enum, 9, true), // Tag: TYPE
    ];
    private ITdfMember[] __members;

    private TdfString _adminEmail = new(__typeInfos[0]);
    private TdfStruct<Blaze15SDK.Blaze.ByteVault.AuthenticationCredentials?> _authCredentials = new(__typeInfos[1]);
    private TdfString _categoryName = new(__typeInfos[2]);
    private TdfString _context = new(__typeInfos[3]);
    private TdfInt64 _endDate = new(__typeInfos[4]);
    private TdfUInt32 _maxResultCount = new(__typeInfos[5]);
    private TdfUInt32 _offset = new(__typeInfos[6]);
    private TdfString _recordName = new(__typeInfos[7]);
    private TdfInt64 _startDate = new(__typeInfos[8]);
    private TdfEnum<ActionType> _action = new(__typeInfos[9]);

    public GetAdminChangeHistoryRequest()
    {
        __members = [ _adminEmail, _authCredentials, _categoryName, _context, _endDate, _maxResultCount, _offset, _recordName, _startDate, _action ];
    }

    public override Tdf CreateNew() => new GetAdminChangeHistoryRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetAdminChangeHistoryRequest";
    public override string GetFullClassName() => "Blaze::ByteVault::GetAdminChangeHistoryRequest";

    public string AdminEmail
    {
        get => _adminEmail.Value;
        set => _adminEmail.Value = value;
    }

    public Blaze15SDK.Blaze.ByteVault.AuthenticationCredentials? AuthCredentials
    {
        get => _authCredentials.Value;
        set => _authCredentials.Value = value;
    }

    public string CategoryName
    {
        get => _categoryName.Value;
        set => _categoryName.Value = value;
    }

    public string Context
    {
        get => _context.Value;
        set => _context.Value = value;
    }

    public long EndDate
    {
        get => _endDate.Value;
        set => _endDate.Value = value;
    }

    public uint MaxResultCount
    {
        get => _maxResultCount.Value;
        set => _maxResultCount.Value = value;
    }

    public uint Offset
    {
        get => _offset.Value;
        set => _offset.Value = value;
    }

    public string RecordName
    {
        get => _recordName.Value;
        set => _recordName.Value = value;
    }

    public long StartDate
    {
        get => _startDate.Value;
        set => _startDate.Value = value;
    }

    public ActionType Action
    {
        get => _action.Value;
        set => _action.Value = value;
    }

}
