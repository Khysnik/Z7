using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Authentication;

public class GetLegalDocContentRequest : Tdf
{
    public enum ContentType : int
    {
        PLAIN = 0,
        HTML = 1,
    }

    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Platform", "mPlatform", 0x8F09B400, TdfType.Enum, 0, true), // Tag: CPFT
        new TdfMemberInfo("IsoCountryCode", "mIsoCountryCode", 0x8F4CB900, TdfType.String, 1, true), // Tag: CTRY
        new TdfMemberInfo("FetchContent", "mFetchContent", 0x9B48E800, TdfType.Bool, 2, true), // Tag: FTCH
        new TdfMemberInfo("IsoLanguage", "mIsoLanguage", 0xB21BA700, TdfType.String, 3, true), // Tag: LANG
        new TdfMemberInfo("ContentType", "mContentType", 0xD25E3400, TdfType.Enum, 4, true), // Tag: TEXT
    ];
    private ITdfMember[] __members;

    private TdfEnum<Blaze15SDK.Blaze.ClientPlatformType> _platform = new(__typeInfos[0]);
    private TdfString _isoCountryCode = new(__typeInfos[1]);
    private TdfBool _fetchContent = new(__typeInfos[2]);
    private TdfString _isoLanguage = new(__typeInfos[3]);
    private TdfEnum<Blaze15SDK.Blaze.Authentication.GetLegalDocContentRequest.ContentType> _contentType = new(__typeInfos[4]);

    public GetLegalDocContentRequest()
    {
        __members = [ _platform, _isoCountryCode, _fetchContent, _isoLanguage, _contentType ];
    }

    public override Tdf CreateNew() => new GetLegalDocContentRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetLegalDocContentRequest";
    public override string GetFullClassName() => "Blaze::Authentication::GetLegalDocContentRequest";

    public Blaze15SDK.Blaze.ClientPlatformType Platform { get => _platform.Value; set => _platform.Value = value; }
    public string IsoCountryCode { get => _isoCountryCode.Value; set => _isoCountryCode.Value = value; }
    public bool FetchContent { get => _fetchContent.Value; set => _fetchContent.Value = value; }
    public string IsoLanguage { get => _isoLanguage.Value; set => _isoLanguage.Value = value; }
    public Blaze15SDK.Blaze.Authentication.GetLegalDocContentRequest.ContentType ContentTypeValue { get => _contentType.Value; set => _contentType.Value = value; }
}
