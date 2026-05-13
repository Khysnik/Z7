using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Achievements;

public class PostEventsRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("AuxAuth", "mAuxAuth", 0x875E2100, TdfType.Struct, 0, true), // Tag: AUXA
        new TdfMemberInfo("Payload", "mPayload", 0x97696E00, TdfType.Struct, 1, true), // Tag: EVEN
        new TdfMemberInfo("ProductId", "mProductId", 0xC32BE400, TdfType.String, 2, true), // Tag: PROD
        new TdfMemberInfo("User", "mUser", 0xD7397200, TdfType.Struct, 3, true), // Tag: USER
    ];
    private ITdfMember[] __members;

    private TdfStruct<AuxiliaryAuthentication?> _auxAuth = new(__typeInfos[0]);
    private TdfStruct<EventsPayload?> _payload = new(__typeInfos[1]);
    private TdfString _productId = new(__typeInfos[2]);
    private TdfStruct<User?> _user = new(__typeInfos[3]);

    public PostEventsRequest()
    {
        __members = [ _auxAuth, _payload, _productId, _user ];
    }

    public override Tdf CreateNew() => new PostEventsRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "PostEventsRequest";
    public override string GetFullClassName() => "Blaze::Achievements::PostEventsRequest";

    public AuxiliaryAuthentication? AuxAuth
    {
        get => _auxAuth.Value;
        set => _auxAuth.Value = value;
    }

    public EventsPayload? Payload
    {
        get => _payload.Value;
        set => _payload.Value = value;
    }

    public string ProductId
    {
        get => _productId.Value;
        set => _productId.Value = value;
    }

    public User? User
    {
        get => _user.Value;
        set => _user.Value = value;
    }

}
