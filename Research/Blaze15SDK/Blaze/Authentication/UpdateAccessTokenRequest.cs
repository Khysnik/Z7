using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Authentication;

public class UpdateAccessTokenRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("AccessToken", "mAccessToken", 0xD2FAEE00, TdfType.String, 0, true), // Tag: TOKN
    ];
    private ITdfMember[] __members;
    private TdfString _accessToken = new(__typeInfos[0]);
    public UpdateAccessTokenRequest() { __members = [ _accessToken ]; }
    public override Tdf CreateNew() => new UpdateAccessTokenRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "UpdateAccessTokenRequest";
    public override string GetFullClassName() => "Blaze::Authentication::UpdateAccessTokenRequest";
    public string AccessToken { get => _accessToken.Value; set => _accessToken.Value = value; }
}
