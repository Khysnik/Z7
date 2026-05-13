using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Authentication;

public class UpdateAccessTokenResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("TokenUpdated", "mTokenUpdated", 0xD2BD7000, TdfType.Bool, 0, true), // Tag: TKUP
    ];
    private ITdfMember[] __members;
    private TdfBool _tokenUpdated = new(__typeInfos[0]);
    public UpdateAccessTokenResponse() { __members = [ _tokenUpdated ]; }
    public override Tdf CreateNew() => new UpdateAccessTokenResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "UpdateAccessTokenResponse";
    public override string GetFullClassName() => "Blaze::Authentication::UpdateAccessTokenResponse";
    public bool TokenUpdated { get => _tokenUpdated.Value; set => _tokenUpdated.Value = value; }
}
