using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.ByteVault;

public class AuthenticationCredentials : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Token", "mToken", 0xD2FAEE00, TdfType.String, 0, true), // Tag: TOKN
        new TdfMemberInfo("TokenType", "mTokenType", 0xD34E7000, TdfType.Enum, 1, true), // Tag: TTYP
        new TdfMemberInfo("User", "mUser", 0xD7397200, TdfType.Struct, 2, true), // Tag: USER
    ];
    private ITdfMember[] __members;

    private TdfString _token = new(__typeInfos[0]);
    private TdfEnum<TokenType> _tokenType = new(__typeInfos[1]);
    private TdfStruct<User?> _user = new(__typeInfos[2]);

    public AuthenticationCredentials()
    {
        __members = [ _token, _tokenType, _user ];
    }

    public override Tdf CreateNew() => new AuthenticationCredentials();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "AuthenticationCredentials";
    public override string GetFullClassName() => "Blaze::ByteVault::AuthenticationCredentials";

    public string Token
    {
        get => _token.Value;
        set => _token.Value = value;
    }

    public TokenType TokenType
    {
        get => _tokenType.Value;
        set => _tokenType.Value = value;
    }

    public User? User
    {
        get => _user.Value;
        set => _user.Value = value;
    }

}
