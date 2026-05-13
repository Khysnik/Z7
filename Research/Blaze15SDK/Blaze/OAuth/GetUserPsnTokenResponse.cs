using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.OAuth;

public class GetUserPsnTokenResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("PsnToken", "mPsnToken", 0x934BEB00, TdfType.String, 0, true), // Tag: DTOK
        new TdfMemberInfo("ExpiresAt", "mExpiresAt", 0x978C2900, TdfType.TimeValue, 1, true), // Tag: EXPI
    ];
    private ITdfMember[] __members;

    private TdfString _psnToken = new(__typeInfos[0]);
    private TdfTimeValue _expiresAt = new(__typeInfos[1]);

    public GetUserPsnTokenResponse()
    {
        __members = [
            _psnToken,
            _expiresAt,
        ];
    }

    public override Tdf CreateNew() => new GetUserPsnTokenResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetUserPsnTokenResponse";
    public override string GetFullClassName() => "Blaze::OAuth::GetUserPsnTokenResponse";

    public string PsnToken
    {
        get => _psnToken.Value;
        set => _psnToken.Value = value;
    }

    public TimeValue ExpiresAt
    {
        get => _expiresAt.Value;
        set => _expiresAt.Value = value;
    }

}
