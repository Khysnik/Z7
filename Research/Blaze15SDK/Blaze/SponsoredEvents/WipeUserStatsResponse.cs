using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.SponsoredEvents;

public class WipeUserStatsResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Success", "mSuccess", 0xCE397300, TdfType.Bool, 0, true), // Tag: SCES
    ];
    private ITdfMember[] __members;

    private TdfBool _success = new(__typeInfos[0]);

    public WipeUserStatsResponse()
    {
        __members = [_success];
    }

    public override Tdf CreateNew() => new WipeUserStatsResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "WipeUserStatsResponse";
    public override string GetFullClassName() => "Blaze::SponsoredEvents::WipeUserStatsResponse";

    public bool Success
    {
        get => _success.Value;
        set => _success.Value = value;
    }
}
