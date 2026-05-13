using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.OAuth;

public class GetUserXblTokenResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("XblToken", "mXblToken", 0x934BEB00, TdfType.String, 0, true), // Tag: DTOK
        new TdfMemberInfo("ProofKey", "mProofKey", 0xC32BE600, TdfType.String, 1, true), // Tag: PROF
    ];
    private ITdfMember[] __members;

    private TdfString _xblToken = new(__typeInfos[0]);
    private TdfString _proofKey = new(__typeInfos[1]);

    public GetUserXblTokenResponse()
    {
        __members = [
            _xblToken,
            _proofKey,
        ];
    }

    public override Tdf CreateNew() => new GetUserXblTokenResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetUserXblTokenResponse";
    public override string GetFullClassName() => "Blaze::OAuth::GetUserXblTokenResponse";

    public string XblToken
    {
        get => _xblToken.Value;
        set => _xblToken.Value = value;
    }

    public string ProofKey
    {
        get => _proofKey.Value;
        set => _proofKey.Value = value;
    }

}
