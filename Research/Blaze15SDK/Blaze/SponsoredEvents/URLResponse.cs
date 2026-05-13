using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.SponsoredEvents;

public class URLResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("URL", "mURL", 0xD72B0000, TdfType.String, 0, true), // Tag: URL
    ];
    private ITdfMember[] __members;

    private TdfString _url = new(__typeInfos[0]);

    public URLResponse()
    {
        __members = [_url];
    }

    public override Tdf CreateNew() => new URLResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "URLResponse";
    public override string GetFullClassName() => "Blaze::SponsoredEvents::URLResponse";

    public string URL
    {
        get => _url.Value;
        set => _url.Value = value;
    }
}
