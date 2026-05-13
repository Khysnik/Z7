using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.OAuth;

public class GetServerAccessTokenRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("AllowedScopes", "mAllowedScopes", 0xCE3BF000, TdfType.String, 0, true), // Tag: SCOP
        new TdfMemberInfo("ServiceName", "mServiceName", 0xCF68EE00, TdfType.String, 1, true), // Tag: SVCN
    ];
    private ITdfMember[] __members;

    private TdfString _serviceName = new(__typeInfos[1]);
    private TdfString _allowedScopes = new(__typeInfos[0]);

    public GetServerAccessTokenRequest()
    {
        __members = [
            _allowedScopes,
            _serviceName,
        ];
    }

    public override Tdf CreateNew() => new GetServerAccessTokenRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetServerAccessTokenRequest";
    public override string GetFullClassName() => "Blaze::OAuth::GetServerAccessTokenRequest";

    public string ServiceName
    {
        get => _serviceName.Value;
        set => _serviceName.Value = value;
    }

    public string AllowedScopes
    {
        get => _allowedScopes.Value;
        set => _allowedScopes.Value = value;
    }

}
