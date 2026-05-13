using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class UpdateLocalUserGroupRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ConnUserIndexList", "mConnUserIndexList", 0x8F5A6C00, TdfType.List, 0, true), // Tag: CUIL
    ];
    private ITdfMember[] __members;

    private TdfList<uint> _connUserIndexList = new(__typeInfos[0]);

    public UpdateLocalUserGroupRequest()
    {
        __members = [
            _connUserIndexList,
        ];
    }

    public override Tdf CreateNew() => new UpdateLocalUserGroupRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "UpdateLocalUserGroupRequest";
    public override string GetFullClassName() => "Blaze::UpdateLocalUserGroupRequest";

    public IList<uint> ConnUserIndexList
    {
        get => _connUserIndexList.Value;
        set => _connUserIndexList.Value = value;
    }

}
