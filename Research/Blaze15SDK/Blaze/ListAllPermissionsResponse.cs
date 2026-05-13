using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class ListAllPermissionsResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("PermissionsByComponent", "mPermissionsByComponent", 0xC25B2D00, TdfType.Map, 0, true), // Tag: PELM
    ];
    private ITdfMember[] __members;

    private TdfMap<string, IList<string>> _permissionsByComponent = new(__typeInfos[0]);

    public ListAllPermissionsResponse()
    {
        __members = [
            _permissionsByComponent,
        ];
    }

    public override Tdf CreateNew() => new ListAllPermissionsResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ListAllPermissionsResponse";
    public override string GetFullClassName() => "Blaze::ListAllPermissionsResponse";

    public IDictionary<string, IList<string>> PermissionsByComponent
    {
        get => _permissionsByComponent.Value;
        set => _permissionsByComponent.Value = value;
    }

}
