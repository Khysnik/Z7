using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.XBLSystem;

public class ValidateStringsRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("AuthToken", "mAuthToken", 0x874AEE00, TdfType.String, 0, true), // Tag: ATKN
        new TdfMemberInfo("Payload", "mPayload", 0x8AF93900, TdfType.Struct, 1, true), // Tag: BODY
    ];
    private ITdfMember[] __members;

    private TdfString _authToken = new(__typeInfos[0]);
    private TdfStruct<StringList?> _payload = new(__typeInfos[1]);

    public ValidateStringsRequest()
    {
        __members = [
            _authToken,
            _payload,
        ];
    }

    public override Tdf CreateNew() => new ValidateStringsRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ValidateStringsRequest";
    public override string GetFullClassName() => "Blaze::XBLSystem::ValidateStringsRequest";

    public string AuthToken
    {
        get => _authToken.Value;
        set => _authToken.Value = value;
    }

    public StringList? Payload
    {
        get => _payload.Value;
        set => _payload.Value = value;
    }

}
