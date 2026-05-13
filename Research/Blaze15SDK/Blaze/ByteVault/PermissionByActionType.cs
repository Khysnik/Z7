using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.ByteVault;

public class PermissionByActionType : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("PermissionByActionTypeMap", "mPermissionByActionTypeMap", 0xC25CAD00, TdfType.Map, 0, true), // Tag: PERM
    ];
    private ITdfMember[] __members;

    private TdfMap<int, int> _permissionByActionTypeMap = new(__typeInfos[0]);

    public PermissionByActionType()
    {
        __members = [ _permissionByActionTypeMap ];
    }

    public override Tdf CreateNew() => new PermissionByActionType();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "PermissionByActionType";
    public override string GetFullClassName() => "Blaze::ByteVault::PermissionByActionType";

    public IDictionary<int, int> PermissionByActionTypeMap
    {
        get => _permissionByActionTypeMap.Value;
        set => _permissionByActionTypeMap.Value = value;
    }

}
