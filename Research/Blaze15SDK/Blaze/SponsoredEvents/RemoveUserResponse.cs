using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.SponsoredEvents;

public class RemoveUserResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("IsDeleted", "mIsDeleted", 0x925AC000, TdfType.Int8, 0, true), // Tag: DEK
    ];
    private ITdfMember[] __members;

    private TdfInt8 _isDeleted = new(__typeInfos[0]);

    public RemoveUserResponse()
    {
        __members = [_isDeleted];
    }

    public override Tdf CreateNew() => new RemoveUserResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "RemoveUserResponse";
    public override string GetFullClassName() => "Blaze::SponsoredEvents::RemoveUserResponse";

    public sbyte IsDeleted
    {
        get => _isDeleted.Value;
        set => _isDeleted.Value = value;
    }
}
