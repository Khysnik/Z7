using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class ValidateSessionKeyRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("SessionKey", "mSessionKey", 0xCEB97900, TdfType.String, 0, true), // Tag: SKEY
    ];
    private ITdfMember[] __members;

    private TdfString _sessionKey = new(__typeInfos[0]);

    public ValidateSessionKeyRequest()
    {
        __members = [
            _sessionKey,
        ];
    }

    public override Tdf CreateNew() => new ValidateSessionKeyRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ValidateSessionKeyRequest";
    public override string GetFullClassName() => "Blaze::ValidateSessionKeyRequest";

    public string SessionKey
    {
        get => _sessionKey.Value;
        set => _sessionKey.Value = value;
    }

}
