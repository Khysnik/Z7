using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Authentication;

public class TrustedLoginRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ClientInfo", "mClientInfo", 0x8EE9AF00, TdfType.Struct, 0, true), // Tag: CNFO
        new TdfMemberInfo("ClientType", "mClientType", 0x8F4E7000, TdfType.Enum, 1, true), // Tag: CTYP
        new TdfMemberInfo("Id", "mId", 0xA6400000, TdfType.String, 2, true), // Tag: ID
        new TdfMemberInfo("IdType", "mIdType", 0xA74E7000, TdfType.String, 3, true), // Tag: ITYP
        new TdfMemberInfo("SetClientInfo", "mSetClientInfo", 0xCE3B2E00, TdfType.Bool, 4, true), // Tag: SCLN
        new TdfMemberInfo("AccessToken", "mAccessToken", 0xD2FAEE00, TdfType.String, 5, true), // Tag: TOKN
    ];
    private ITdfMember[] __members;

    private TdfStruct<Blaze15SDK.Blaze.ClientInfo?> _clientInfo = new(__typeInfos[0]);
    private TdfEnum<Blaze15SDK.Blaze.ClientType> _clientType = new(__typeInfos[1]);
    private TdfString _id = new(__typeInfos[2]);
    private TdfString _idType = new(__typeInfos[3]);
    private TdfBool _setClientInfo = new(__typeInfos[4]);
    private TdfString _accessToken = new(__typeInfos[5]);

    public TrustedLoginRequest()
    {
        __members = [
            _clientInfo,
            _clientType,
            _id,
            _idType,
            _setClientInfo,
            _accessToken,
        ];
    }

    public override Tdf CreateNew() => new TrustedLoginRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "TrustedLoginRequest";
    public override string GetFullClassName() => "Blaze::Authentication::TrustedLoginRequest";

    public Blaze15SDK.Blaze.ClientInfo? ClientInfo
    {
        get => _clientInfo.Value;
        set => _clientInfo.Value = value;
    }

    public Blaze15SDK.Blaze.ClientType ClientType
    {
        get => _clientType.Value;
        set => _clientType.Value = value;
    }

    public string Id
    {
        get => _id.Value;
        set => _id.Value = value;
    }

    public string IdType
    {
        get => _idType.Value;
        set => _idType.Value = value;
    }

    public bool SetClientInfo
    {
        get => _setClientInfo.Value;
        set => _setClientInfo.Value = value;
    }

    public string AccessToken
    {
        get => _accessToken.Value;
        set => _accessToken.Value = value;
    }

}
