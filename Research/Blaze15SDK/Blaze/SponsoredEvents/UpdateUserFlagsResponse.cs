using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.SponsoredEvents;

public class UpdateUserFlagsResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("IsUpdated", "mIsUpdated", 0xCA5CEC00, TdfType.Int8, 0, true), // Tag: RESL
    ];
    private ITdfMember[] __members;

    private TdfInt8 _isUpdated = new(__typeInfos[0]);

    public UpdateUserFlagsResponse()
    {
        __members = [_isUpdated];
    }

    public override Tdf CreateNew() => new UpdateUserFlagsResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "UpdateUserFlagsResponse";
    public override string GetFullClassName() => "Blaze::SponsoredEvents::UpdateUserFlagsResponse";

    public sbyte IsUpdated
    {
        get => _isUpdated.Value;
        set => _isUpdated.Value = value;
    }
}
