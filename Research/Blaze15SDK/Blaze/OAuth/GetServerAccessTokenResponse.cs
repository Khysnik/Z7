using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.OAuth;

public class GetServerAccessTokenResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("AccessToken", "mAccessToken", 0x874BEB00, TdfType.String, 0, true), // Tag: ATOK
        new TdfMemberInfo("ClientId", "mClientId", 0x8ECA6400, TdfType.String, 1, true), // Tag: CLID
        new TdfMemberInfo("ValidUntil", "mValidUntil", 0xDA1B2900, TdfType.TimeValue, 2, true), // Tag: VALI
    ];
    private ITdfMember[] __members;

    private TdfString _accessToken = new(__typeInfos[0]);
    private TdfString _clientId = new(__typeInfos[1]);
    private TdfTimeValue _validUntil = new(__typeInfos[2]);

    public GetServerAccessTokenResponse()
    {
        __members = [
            _accessToken,
            _clientId,
            _validUntil,
        ];
    }

    public override Tdf CreateNew() => new GetServerAccessTokenResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetServerAccessTokenResponse";
    public override string GetFullClassName() => "Blaze::OAuth::GetServerAccessTokenResponse";

    public string AccessToken
    {
        get => _accessToken.Value;
        set => _accessToken.Value = value;
    }

    public string ClientId
    {
        get => _clientId.Value;
        set => _clientId.Value = value;
    }

    public TimeValue ValidUntil
    {
        get => _validUntil.Value;
        set => _validUntil.Value = value;
    }

}
