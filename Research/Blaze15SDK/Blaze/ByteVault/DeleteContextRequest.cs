using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.ByteVault;

public class DeleteContextRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("AuthCredentials", "mAuthCredentials", 0x875D2800, TdfType.Struct, 0, true), // Tag: AUTH
        new TdfMemberInfo("Context", "mContext", 0x8F4E3400, TdfType.String, 1, true), // Tag: CTXT
    ];
    private ITdfMember[] __members;

    private TdfStruct<Blaze15SDK.Blaze.ByteVault.AuthenticationCredentials?> _authCredentials = new(__typeInfos[0]);
    private TdfString _context = new(__typeInfos[1]);

    public DeleteContextRequest()
    {
        __members = [ _authCredentials, _context ];
    }

    public override Tdf CreateNew() => new DeleteContextRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "DeleteContextRequest";
    public override string GetFullClassName() => "Blaze::ByteVault::DeleteContextRequest";

    public Blaze15SDK.Blaze.ByteVault.AuthenticationCredentials? AuthCredentials
    {
        get => _authCredentials.Value;
        set => _authCredentials.Value = value;
    }

    public string Context
    {
        get => _context.Value;
        set => _context.Value = value;
    }

}
