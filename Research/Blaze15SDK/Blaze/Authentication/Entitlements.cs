using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Authentication;

public class Entitlements : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("EntitlementsList", "mEntitlements", 0xBACCF400, TdfType.List, 0, true), // Tag: NLST
    ];
    private ITdfMember[] __members;

    private TdfList<Blaze15SDK.Blaze.Authentication.Entitlement> _entitlements = new(__typeInfos[0]);

    public Entitlements()
    {
        __members = [ _entitlements ];
    }

    public override Tdf CreateNew() => new Entitlements();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "Entitlements";
    public override string GetFullClassName() => "Blaze::Authentication::Entitlements";

    public IList<Blaze15SDK.Blaze.Authentication.Entitlement> EntitlementsList { get => _entitlements.Value; set => _entitlements.Value = value; }
}
