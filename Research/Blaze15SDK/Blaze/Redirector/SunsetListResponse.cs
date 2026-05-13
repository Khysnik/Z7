using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Redirector;

public class SunsetListResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Titles", "mTitles", 0xCF4A6C00, TdfType.Map, 0, true), // Tag: STIL
    ];
    private ITdfMember[] __members;

    private TdfMap<string, Blaze15SDK.Blaze.Redirector.SunsetData> _titles = new(__typeInfos[0]);

    public SunsetListResponse()
    {
        __members = [ _titles ];
    }

    public override Tdf CreateNew() => new SunsetListResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "SunsetListResponse";
    public override string GetFullClassName() => "Blaze::Redirector::SunsetListResponse";

    public IDictionary<string, Blaze15SDK.Blaze.Redirector.SunsetData> Titles
    {
        get => _titles.Value;
        set => _titles.Value = value;
    }

}
