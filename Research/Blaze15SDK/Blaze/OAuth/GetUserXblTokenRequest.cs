using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.OAuth;

public class GetUserXblTokenRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("RetrieveUsing", "mRetrieveUsing", 0xBF7BB200, TdfType.Union, 0, true), // Tag: OWNR
        new TdfMemberInfo("ForceRefresh", "mForceRefresh", 0xCA598000, TdfType.Bool, 1, true), // Tag: REF
        new TdfMemberInfo("RelyingParty", "mRelyingParty", 0xCACE7000, TdfType.String, 2, true), // Tag: RLYP
        new TdfMemberInfo("ServiceName", "mServiceName", 0xCF68EE00, TdfType.String, 3, true), // Tag: SVCN
    ];
    private ITdfMember[] __members;

    private TdfUnion<Blaze15SDK.Blaze.OAuth.XblTokenOwnerId> _retrieveUsing = new(__typeInfos[0]);
    private TdfBool _forceRefresh = new(__typeInfos[1]);
    private TdfString _serviceName = new(__typeInfos[3]);
    private TdfString _relyingParty = new(__typeInfos[2]);

    public GetUserXblTokenRequest()
    {
        __members = [
            _retrieveUsing,
            _forceRefresh,
            _relyingParty,
            _serviceName,
        ];
    }

    public override Tdf CreateNew() => new GetUserXblTokenRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetUserXblTokenRequest";
    public override string GetFullClassName() => "Blaze::OAuth::GetUserXblTokenRequest";

    public Blaze15SDK.Blaze.OAuth.XblTokenOwnerId RetrieveUsing
    {
        get => _retrieveUsing.Value;
        set => _retrieveUsing.Value = value;
    }

    public bool ForceRefresh
    {
        get => _forceRefresh.Value;
        set => _forceRefresh.Value = value;
    }

    public string ServiceName
    {
        get => _serviceName.Value;
        set => _serviceName.Value = value;
    }

    public string RelyingParty
    {
        get => _relyingParty.Value;
        set => _relyingParty.Value = value;
    }

}
