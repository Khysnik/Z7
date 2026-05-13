using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.ByteVault;

public class UpsertCategoryRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("AuthCredentials", "mAuthCredentials", 0x875D2800, TdfType.Struct, 0, true), // Tag: AUTH
        new TdfMemberInfo("CategoryName", "mCategoryName", 0x8E1D0000, TdfType.String, 1, true), // Tag: CAT
        new TdfMemberInfo("Configuration", "mConfiguration", 0x8EFBA600, TdfType.Struct, 2, true), // Tag: CONF
        new TdfMemberInfo("Context", "mContext", 0x8F4E3400, TdfType.String, 3, true), // Tag: CTXT
        new TdfMemberInfo("Description", "mDescription", 0x925CE300, TdfType.String, 4, true), // Tag: DESC
        new TdfMemberInfo("TrustedSources", "mTrustedSources", 0xD32CF400, TdfType.List, 5, true), // Tag: TRST
    ];
    private ITdfMember[] __members;

    private TdfStruct<Blaze15SDK.Blaze.ByteVault.AuthenticationCredentials?> _authCredentials = new(__typeInfos[0]);
    private TdfString _categoryName = new(__typeInfos[1]);
    private TdfStruct<Blaze15SDK.Blaze.ByteVault.CategorySettings?> _configuration = new(__typeInfos[2]);
    private TdfString _context = new(__typeInfos[3]);
    private TdfString _description = new(__typeInfos[4]);
    private TdfList<string> _trustedSources = new(__typeInfos[5]);

    public UpsertCategoryRequest()
    {
        __members = [ _authCredentials, _categoryName, _configuration, _context, _description, _trustedSources ];
    }

    public override Tdf CreateNew() => new UpsertCategoryRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "UpsertCategoryRequest";
    public override string GetFullClassName() => "Blaze::ByteVault::UpsertCategoryRequest";

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

    public Blaze15SDK.Blaze.ByteVault.CategorySettings? Configuration
    {
        get => _configuration.Value;
        set => _configuration.Value = value;
    }

    public string Context
    {
        get => _context.Value;
        set => _context.Value = value;
    }

    public string Description
    {
        get => _description.Value;
        set => _description.Value = value;
    }

    public IList<string> TrustedSources
    {
        get => _trustedSources.Value;
        set => _trustedSources.Value = value;
    }

}
