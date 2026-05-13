using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Authentication;

public class GetEmailOptInSettingsRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("IsoCountryCode", "mIsoCountryCode", 0x8F4CB900, TdfType.String, 0, true), // Tag: CTRY
        new TdfMemberInfo("Platform", "mPlatform", 0xC349AD00, TdfType.Enum, 1, true), // Tag: PTFM
    ];
    private ITdfMember[] __members;

    private TdfString _isoCountryCode = new(__typeInfos[0]);
    private TdfEnum<Blaze15SDK.Blaze.ClientPlatformType> _platform = new(__typeInfos[1]);

    public GetEmailOptInSettingsRequest()
    {
        __members = [ _isoCountryCode, _platform ];
    }

    public override Tdf CreateNew() => new GetEmailOptInSettingsRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetEmailOptInSettingsRequest";
    public override string GetFullClassName() => "Blaze::Authentication::GetEmailOptInSettingsRequest";

    public string IsoCountryCode { get => _isoCountryCode.Value; set => _isoCountryCode.Value = value; }
    public Blaze15SDK.Blaze.ClientPlatformType Platform { get => _platform.Value; set => _platform.Value = value; }
}
