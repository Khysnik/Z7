using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Authentication;

public class LoginResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("IsAnonymous", "mIsAnonymous", 0x86EBEE00, TdfType.Bool, 0, true), // Tag: ANON
        new TdfMemberInfo("UserLoginInfo", "mUserLoginInfo", 0xCE5CF300, TdfType.Struct, 1, true), // Tag: SESS
        new TdfMemberInfo("IsOfLegalContactAge", "mIsOfLegalContactAge", 0xCF086D00, TdfType.Bool, 2, true), // Tag: SPAM
        new TdfMemberInfo("IsUnderage", "mIsUnderage", 0xD6E93200, TdfType.Bool, 3, true), // Tag: UNDR
    ];
    private ITdfMember[] __members;

    private TdfBool _isAnonymous = new(__typeInfos[0]);
    private TdfStruct<Blaze15SDK.Blaze.Authentication.UserLoginInfo?> _userLoginInfo = new(__typeInfos[1]);
    private TdfBool _isOfLegalContactAge = new(__typeInfos[2]);
    private TdfBool _isUnderage = new(__typeInfos[3]);

    public LoginResponse()
    {
        __members = [ _isAnonymous, _userLoginInfo, _isOfLegalContactAge, _isUnderage ];
    }

    public override Tdf CreateNew() => new LoginResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "LoginResponse";
    public override string GetFullClassName() => "Blaze::Authentication::LoginResponse";

    public bool IsAnonymous { get => _isAnonymous.Value; set => _isAnonymous.Value = value; }
    public Blaze15SDK.Blaze.Authentication.UserLoginInfo? UserLoginInfo { get => _userLoginInfo.Value; set => _userLoginInfo.Value = value; }
    public bool IsOfLegalContactAge { get => _isOfLegalContactAge.Value; set => _isOfLegalContactAge.Value = value; }
    public bool IsUnderage { get => _isUnderage.Value; set => _isUnderage.Value = value; }
}
