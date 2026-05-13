using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.ByteVault;

public class AdminHistory : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("AdminEmail", "mAdminEmail", 0x864B6E00, TdfType.String, 0, true), // Tag: ADMN
        new TdfMemberInfo("AdminTypeListByContextMap", "mAdminTypeListByContextMap", 0xB61C0000, TdfType.Map, 1, true), // Tag: MAP
    ];
    private ITdfMember[] __members;

    private TdfString _adminEmail = new(__typeInfos[0]);
    private TdfMap<string, IList<AdminType>> _adminTypeListByContextMap = new(__typeInfos[1]);

    public AdminHistory()
    {
        __members = [ _adminEmail, _adminTypeListByContextMap ];
    }

    public override Tdf CreateNew() => new AdminHistory();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "AdminHistory";
    public override string GetFullClassName() => "Blaze::ByteVault::AdminHistory";

    public string AdminEmail
    {
        get => _adminEmail.Value;
        set => _adminEmail.Value = value;
    }

    public IDictionary<string, IList<AdminType>> AdminTypeListByContextMap
    {
        get => _adminTypeListByContextMap.Value;
        set => _adminTypeListByContextMap.Value = value;
    }

}
