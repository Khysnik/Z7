using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class UserSessionLogoutInfo : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("BlazeId", "mBlazeId", 0x8A990000, TdfType.Int64, 0, true), // Tag: BID
        new TdfMemberInfo("ForcedLogoutReason", "mForcedLogoutReason", 0x9ACCB300, TdfType.Enum, 1, true), // Tag: FLRS
        new TdfMemberInfo("UserSessionType", "mUserSessionType", 0xD73D3000, TdfType.Enum, 2, true), // Tag: USTP
    ];
    private ITdfMember[] __members;

    private TdfInt64 _blazeId = new(__typeInfos[0]);
    private TdfEnum<Blaze15SDK.Blaze.UserSessionForcedLogoutType> _forcedLogoutReason = new(__typeInfos[1]);
    private TdfEnum<Blaze15SDK.Blaze.UserSessionType> _userSessionType = new(__typeInfos[2]);

    public UserSessionLogoutInfo()
    {
        __members = [
            _blazeId,
            _forcedLogoutReason,
            _userSessionType,
        ];
    }

    public override Tdf CreateNew() => new UserSessionLogoutInfo();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "UserSessionLogoutInfo";
    public override string GetFullClassName() => "Blaze::UserSessionLogoutInfo";

    public long BlazeId
    {
        get => _blazeId.Value;
        set => _blazeId.Value = value;
    }

    public Blaze15SDK.Blaze.UserSessionForcedLogoutType ForcedLogoutReason
    {
        get => _forcedLogoutReason.Value;
        set => _forcedLogoutReason.Value = value;
    }

    public Blaze15SDK.Blaze.UserSessionType UserSessionType
    {
        get => _userSessionType.Value;
        set => _userSessionType.Value = value;
    }

}
