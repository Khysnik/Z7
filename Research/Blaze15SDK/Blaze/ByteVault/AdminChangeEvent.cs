using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.ByteVault;

public class AdminChangeEvent : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("AdminEmail", "mAdminEmail", 0x864B6E00, TdfType.String, 0, true), // Tag: ADMN
        new TdfMemberInfo("After", "mAfter", 0x866D3200, TdfType.Variable, 1, true), // Tag: AFTR
        new TdfMemberInfo("Before", "mBefore", 0x8A59B200, TdfType.Variable, 2, true), // Tag: BEFR
        new TdfMemberInfo("CategoryName", "mCategoryName", 0x8E1D0000, TdfType.String, 3, true), // Tag: CAT
        new TdfMemberInfo("Context", "mContext", 0x8F4E3400, TdfType.String, 4, true), // Tag: CTXT
        new TdfMemberInfo("AdminIp", "mAdminIp", 0xA7000000, TdfType.String, 5, true), // Tag: IP
        new TdfMemberInfo("Timestamp", "mTimestamp", 0xD29B6500, TdfType.Int64, 6, true), // Tag: TIME
        new TdfMemberInfo("ActionType", "mActionType", 0xD39C2500, TdfType.Enum, 7, true), // Tag: TYPE
    ];
    private ITdfMember[] __members;

    private TdfString _adminEmail = new(__typeInfos[0]);
    private TdfVariable _after = new(__typeInfos[1]);
    private TdfVariable _before = new(__typeInfos[2]);
    private TdfString _categoryName = new(__typeInfos[3]);
    private TdfString _context = new(__typeInfos[4]);
    private TdfString _adminIp = new(__typeInfos[5]);
    private TdfInt64 _timestamp = new(__typeInfos[6]);
    private TdfEnum<ActionType> _actionType = new(__typeInfos[7]);

    public AdminChangeEvent()
    {
        __members = [ _adminEmail, _after, _before, _categoryName, _context, _adminIp, _timestamp, _actionType ];
    }

    public override Tdf CreateNew() => new AdminChangeEvent();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "AdminChangeEvent";
    public override string GetFullClassName() => "Blaze::ByteVault::AdminChangeEvent";

    public string AdminEmail
    {
        get => _adminEmail.Value;
        set => _adminEmail.Value = value;
    }

    public object? After
    {
        get => _after.Value;
        set => _after.Value = value;
    }

    public object? Before
    {
        get => _before.Value;
        set => _before.Value = value;
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

    public string AdminIp
    {
        get => _adminIp.Value;
        set => _adminIp.Value = value;
    }

    public long Timestamp
    {
        get => _timestamp.Value;
        set => _timestamp.Value = value;
    }

    public ActionType ActionType
    {
        get => _actionType.Value;
        set => _actionType.Value = value;
    }

}
