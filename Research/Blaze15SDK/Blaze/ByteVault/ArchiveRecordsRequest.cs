using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.ByteVault;

public class ArchiveRecordsRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("AuthCredentials", "mAuthCredentials", 0x875D2800, TdfType.Struct, 0, true), // Tag: AUTH
        new TdfMemberInfo("CategoryName", "mCategoryName", 0x8E1D0000, TdfType.String, 1, true), // Tag: CAT
        new TdfMemberInfo("Context", "mContext", 0x8F4E3400, TdfType.String, 2, true), // Tag: CTXT
        new TdfMemberInfo("Record", "mRecord", 0xBA1B6500, TdfType.String, 3, true), // Tag: NAME
        new TdfMemberInfo("Owners", "mOwners", 0xBF7BB200, TdfType.List, 4, true), // Tag: OWNR
    ];
    private ITdfMember[] __members;

    private TdfStruct<Blaze15SDK.Blaze.ByteVault.AuthenticationCredentials?> _authCredentials = new(__typeInfos[0]);
    private TdfString _categoryName = new(__typeInfos[1]);
    private TdfString _context = new(__typeInfos[2]);
    private TdfString _record = new(__typeInfos[3]);
    private TdfList<Blaze15SDK.Blaze.ByteVault.User> _owners = new(__typeInfos[4]);

    public ArchiveRecordsRequest()
    {
        __members = [ _authCredentials, _categoryName, _context, _record, _owners ];
    }

    public override Tdf CreateNew() => new ArchiveRecordsRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ArchiveRecordsRequest";
    public override string GetFullClassName() => "Blaze::ByteVault::ArchiveRecordsRequest";

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

    public string Record
    {
        get => _record.Value;
        set => _record.Value = value;
    }

    public IList<Blaze15SDK.Blaze.ByteVault.User> Owners
    {
        get => _owners.Value;
        set => _owners.Value = value;
    }

}
