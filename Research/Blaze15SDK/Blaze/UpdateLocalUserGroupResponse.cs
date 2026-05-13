using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class UpdateLocalUserGroupResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ConnUserIndexList", "mConnUserIndexList", 0x8F5A6C00, TdfType.List, 0, true), // Tag: CUIL
        new TdfMemberInfo("UserGroupObjectId", "mUserGroupObjectId", 0xD67A6400, TdfType.ObjectId, 1, true), // Tag: UGID
    ];
    private ITdfMember[] __members;

    private TdfList<uint> _connUserIndexList = new(__typeInfos[0]);
    private TdfObjectId _userGroupObjectId = new(__typeInfos[1]);

    public UpdateLocalUserGroupResponse()
    {
        __members = [
            _connUserIndexList,
            _userGroupObjectId,
        ];
    }

    public override Tdf CreateNew() => new UpdateLocalUserGroupResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "UpdateLocalUserGroupResponse";
    public override string GetFullClassName() => "Blaze::UpdateLocalUserGroupResponse";

    public IList<uint> ConnUserIndexList
    {
        get => _connUserIndexList.Value;
        set => _connUserIndexList.Value = value;
    }

    public EATDF.Types.ObjectId UserGroupObjectId
    {
        get => _userGroupObjectId.Value;
        set => _userGroupObjectId.Value = value;
    }

}
