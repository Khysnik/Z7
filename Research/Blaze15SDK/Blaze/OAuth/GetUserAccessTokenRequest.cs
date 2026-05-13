using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.OAuth;

public class GetUserAccessTokenRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ServiceName", "mServiceName", 0xCF68EE00, TdfType.String, 0, true), // Tag: SVCN
        new TdfMemberInfo("UserSessionId", "mUserSessionId", 0xD73A6400, TdfType.UInt64, 1, true), // Tag: USID
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _userSessionId = new(__typeInfos[1]);
    private TdfString _serviceName = new(__typeInfos[0]);

    public GetUserAccessTokenRequest()
    {
        __members = [
            _serviceName,
            _userSessionId,
        ];
    }

    public override Tdf CreateNew() => new GetUserAccessTokenRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetUserAccessTokenRequest";
    public override string GetFullClassName() => "Blaze::OAuth::GetUserAccessTokenRequest";

    public ulong UserSessionId
    {
        get => _userSessionId.Value;
        set => _userSessionId.Value = value;
    }

    public string ServiceName
    {
        get => _serviceName.Value;
        set => _serviceName.Value = value;
    }

}
