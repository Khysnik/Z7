using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Achievements;

public class AuxiliaryAuthentication : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ApiVersion", "mApiVersion", 0x870A4000, TdfType.Int32, 0, true), // Tag: API
        new TdfMemberInfo("AuthToken", "mAuthToken", 0x874BEB00, TdfType.String, 1, true), // Tag: ATOK
        new TdfMemberInfo("TrustedAuthToken", "mTrustedAuthToken", 0x874D2B00, TdfType.String, 2, true), // Tag: ATTK
        new TdfMemberInfo("UserPassword", "mUserPassword", 0xA348A100, TdfType.String, 3, true), // Tag: HTBA
        new TdfMemberInfo("Signature", "mSignature", 0xCE99EE00, TdfType.String, 4, true), // Tag: SIGN
        new TdfMemberInfo("SecretKey", "mSecretKey", 0xCEB97900, TdfType.String, 5, true), // Tag: SKEY
        new TdfMemberInfo("Timestamp", "mTimestamp", 0xD29B6500, TdfType.Int64, 6, true), // Tag: TIME
        new TdfMemberInfo("User", "mUser", 0xD7397200, TdfType.Struct, 7, true), // Tag: USER
    ];
    private ITdfMember[] __members;

    private TdfInt32 _apiVersion = new(__typeInfos[0]);
    private TdfString _authToken = new(__typeInfos[1]);
    private TdfString _trustedAuthToken = new(__typeInfos[2]);
    private TdfString _userPassword = new(__typeInfos[3]);
    private TdfString _signature = new(__typeInfos[4]);
    private TdfString _secretKey = new(__typeInfos[5]);
    private TdfInt64 _timestamp = new(__typeInfos[6]);
    private TdfStruct<User?> _user = new(__typeInfos[7]);

    public AuxiliaryAuthentication()
    {
        __members = [ _apiVersion, _authToken, _trustedAuthToken, _userPassword, _signature, _secretKey, _timestamp, _user ];
    }

    public override Tdf CreateNew() => new AuxiliaryAuthentication();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "AuxiliaryAuthentication";
    public override string GetFullClassName() => "Blaze::Achievements::AuxiliaryAuthentication";

    public int ApiVersion
    {
        get => _apiVersion.Value;
        set => _apiVersion.Value = value;
    }

    public string AuthToken
    {
        get => _authToken.Value;
        set => _authToken.Value = value;
    }

    public string TrustedAuthToken
    {
        get => _trustedAuthToken.Value;
        set => _trustedAuthToken.Value = value;
    }

    public string UserPassword
    {
        get => _userPassword.Value;
        set => _userPassword.Value = value;
    }

    public string Signature
    {
        get => _signature.Value;
        set => _signature.Value = value;
    }

    public string SecretKey
    {
        get => _secretKey.Value;
        set => _secretKey.Value = value;
    }

    public long Timestamp
    {
        get => _timestamp.Value;
        set => _timestamp.Value = value;
    }

    public User? User
    {
        get => _user.Value;
        set => _user.Value = value;
    }

}
