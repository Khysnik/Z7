using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class RemoteInitiatorInformation : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("AssociatedUserGroupId", "mAssociatedUserGroupId", 0x9E990000, TdfType.ObjectId, 0, true), // Tag: GID
        new TdfMemberInfo("InitiatorId", "mInitiatorId", 0xA6EA6400, TdfType.Int64, 1, true), // Tag: INID
    ];
    private ITdfMember[] __members;

    private TdfObjectId _associatedUserGroupId = new(__typeInfos[0]);
    private TdfInt64 _initiatorId = new(__typeInfos[1]);

    public RemoteInitiatorInformation()
    {
        __members = [ _associatedUserGroupId, _initiatorId ];
    }

    public override Tdf CreateNew() => new RemoteInitiatorInformation();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "RemoteInitiatorInformation";
    public override string GetFullClassName() => "Blaze::GameManager::RemoteInitiatorInformation";

    public ObjectId AssociatedUserGroupId
    {
        get => _associatedUserGroupId.Value;
        set => _associatedUserGroupId.Value = value;
    }

    public long InitiatorId
    {
        get => _initiatorId.Value;
        set => _initiatorId.Value = value;
    }

}
