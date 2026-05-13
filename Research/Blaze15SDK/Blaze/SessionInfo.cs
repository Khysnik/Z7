using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class SessionInfo : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("BlazeId", "mBlazeId", 0x8B5A6400, TdfType.Int64, 0, true), // Tag: BUID
        new TdfMemberInfo("DisplayName", "mDisplayName", 0x933BAD00, TdfType.String, 1, true), // Tag: DSNM
        new TdfMemberInfo("SessionKey", "mSessionKey", 0xAE5E4000, TdfType.String, 2, true), // Tag: KEY
        new TdfMemberInfo("Email", "mEmail", 0xB61A6C00, TdfType.String, 3, true), // Tag: MAIL
        new TdfMemberInfo("AccountId", "mAccountId", 0xD6990000, TdfType.Int64, 4, true), // Tag: UID
    ];
    private ITdfMember[] __members;

    private TdfInt64 _blazeId = new(__typeInfos[0]);
    private TdfString _displayName = new(__typeInfos[1]);
    private TdfString _sessionKey = new(__typeInfos[2]);
    private TdfString _email = new(__typeInfos[3]);
    private TdfInt64 _accountId = new(__typeInfos[4]);

    public SessionInfo()
    {
        __members = [
            _blazeId,
            _displayName,
            _sessionKey,
            _email,
            _accountId,
        ];
    }

    public override Tdf CreateNew() => new SessionInfo();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "SessionInfo";
    public override string GetFullClassName() => "Blaze::SessionInfo";

    public long BlazeId
    {
        get => _blazeId.Value;
        set => _blazeId.Value = value;
    }

    public string DisplayName
    {
        get => _displayName.Value;
        set => _displayName.Value = value;
    }

    public string SessionKey
    {
        get => _sessionKey.Value;
        set => _sessionKey.Value = value;
    }

    public string Email
    {
        get => _email.Value;
        set => _email.Value = value;
    }

    public long AccountId
    {
        get => _accountId.Value;
        set => _accountId.Value = value;
    }

}
