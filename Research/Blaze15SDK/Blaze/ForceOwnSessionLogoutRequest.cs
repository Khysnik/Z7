using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class ForceOwnSessionLogoutRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("BlazeId", "mBlazeId", 0x8A990000, TdfType.Int64, 0, true), // Tag: BID
        new TdfMemberInfo("SessionKey", "mSessionKey", 0xAE5E4000, TdfType.String, 1, true), // Tag: KEY
    ];
    private ITdfMember[] __members;

    private TdfInt64 _blazeId = new(__typeInfos[0]);
    private TdfString _sessionKey = new(__typeInfos[1]);

    public ForceOwnSessionLogoutRequest()
    {
        __members = [
            _blazeId,
            _sessionKey,
        ];
    }

    public override Tdf CreateNew() => new ForceOwnSessionLogoutRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ForceOwnSessionLogoutRequest";
    public override string GetFullClassName() => "Blaze::ForceOwnSessionLogoutRequest";

    public long BlazeId
    {
        get => _blazeId.Value;
        set => _blazeId.Value = value;
    }

    public string SessionKey
    {
        get => _sessionKey.Value;
        set => _sessionKey.Value = value;
    }

}
