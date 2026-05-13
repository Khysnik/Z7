using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.ByteVault;

public class GetCategoriesRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("AuthCredentials", "mAuthCredentials", 0x875D2800, TdfType.Struct, 0, true), // Tag: AUTH
        new TdfMemberInfo("CategoryName", "mCategoryName", 0x8E1D0000, TdfType.String, 1, true), // Tag: CAT
        new TdfMemberInfo("Context", "mContext", 0x8F4E3400, TdfType.String, 2, true), // Tag: CTXT
        new TdfMemberInfo("MaxResultCount", "mMaxResultCount", 0xB78CA300, TdfType.UInt32, 3, true), // Tag: MXRC
        new TdfMemberInfo("Offset", "mOffset", 0xBE69B300, TdfType.UInt32, 4, true), // Tag: OFFS
    ];
    private ITdfMember[] __members;

    private TdfStruct<Blaze15SDK.Blaze.ByteVault.AuthenticationCredentials?> _authCredentials = new(__typeInfos[0]);
    private TdfString _categoryName = new(__typeInfos[1]);
    private TdfString _context = new(__typeInfos[2]);
    private TdfUInt32 _maxResultCount = new(__typeInfos[3]);
    private TdfUInt32 _offset = new(__typeInfos[4]);

    public GetCategoriesRequest()
    {
        __members = [ _authCredentials, _categoryName, _context, _maxResultCount, _offset ];
    }

    public override Tdf CreateNew() => new GetCategoriesRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetCategoriesRequest";
    public override string GetFullClassName() => "Blaze::ByteVault::GetCategoriesRequest";

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

}
