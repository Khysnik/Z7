using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.ByteVault;

public class UpsertAdminRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("AdminEmail", "mAdminEmail", 0x864B6E00, TdfType.String, 0, true), // Tag: ADMN
        new TdfMemberInfo("AuthCredentials", "mAuthCredentials", 0x875D2800, TdfType.Struct, 1, true), // Tag: AUTH
        new TdfMemberInfo("AdminTypeListByContextMap", "mAdminTypeListByContextMap", 0xB61C0000, TdfType.Map, 2, true), // Tag: MAP
    ];
    private ITdfMember[] __members;

    private TdfString _adminEmail = new(__typeInfos[0]);
    private TdfStruct<Blaze15SDK.Blaze.ByteVault.AuthenticationCredentials?> _authCredentials = new(__typeInfos[1]);
    private TdfMap<string, IList<int>> _adminTypeListByContextMap = new(__typeInfos[2]);

    public UpsertAdminRequest()
    {
        __members = [ _adminEmail, _authCredentials, _adminTypeListByContextMap ];
    }

    public override Tdf CreateNew() => new UpsertAdminRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "UpsertAdminRequest";
    public override string GetFullClassName() => "Blaze::ByteVault::UpsertAdminRequest";

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

    public IDictionary<string, IList<int>> AdminTypeListByContextMap
    {
        get => _adminTypeListByContextMap.Value;
        set => _adminTypeListByContextMap.Value = value;
    }

}
