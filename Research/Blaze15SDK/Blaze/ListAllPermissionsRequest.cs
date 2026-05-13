using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class ListAllPermissionsRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ComponentId", "mComponentId", 0x8F4A6400, TdfType.UInt16, 0, true), // Tag: CTID
    ];
    private ITdfMember[] __members;

    private TdfUInt16 _componentId = new(__typeInfos[0]);

    public ListAllPermissionsRequest()
    {
        __members = [
            _componentId,
        ];
    }

    public override Tdf CreateNew() => new ListAllPermissionsRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ListAllPermissionsRequest";
    public override string GetFullClassName() => "Blaze::ListAllPermissionsRequest";

    public ushort ComponentId
    {
        get => _componentId.Value;
        set => _componentId.Value = value;
    }

}
