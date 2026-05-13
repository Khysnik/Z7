using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Social.Friends;

public class AuthenticationCredentials : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ApiVersion", "mApiVersion", 0x870A7600, TdfType.String, 0, true), // Tag: APIV
        new TdfMemberInfo("ApplicationKey", "mApplicationKey", 0x870C2B00, TdfType.String, 1, true), // Tag: APPK
        new TdfMemberInfo("Authorization", "mAuthorization", 0x8A18F400, TdfType.String, 2, true), // Tag: BACT
    ];
    private ITdfMember[] __members;

    private TdfString _apiVersion = new(__typeInfos[0]);
    private TdfString _applicationKey = new(__typeInfos[1]);
    private TdfString _authorization = new(__typeInfos[2]);

    public AuthenticationCredentials()
    {
        __members = [ _apiVersion, _applicationKey, _authorization ];
    }

    public override Tdf CreateNew() => new AuthenticationCredentials();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "AuthenticationCredentials";
    public override string GetFullClassName() => "Blaze::Social::Friends::AuthenticationCredentials";

    public string ApiVersion
    {
        get => _apiVersion.Value;
        set => _apiVersion.Value = value;
    }

    public string ApplicationKey
    {
        get => _applicationKey.Value;
        set => _applicationKey.Value = value;
    }

    public string Authorization
    {
        get => _authorization.Value;
        set => _authorization.Value = value;
    }

}
