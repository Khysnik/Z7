using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class NotifyRemoteMatchmakingStarted : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("RemoteUserInfo", "mRemoteUserInfo", 0xA6E9AF00, TdfType.Struct, 0, true), // Tag: INFO
        new TdfMemberInfo("MMScenarioId", "mMMScenarioId", 0xB738E400, TdfType.UInt64, 1, true), // Tag: MSCD
        new TdfMemberInfo("MMSessionId", "mMMSessionId", 0xB73A6400, TdfType.UInt64, 2, true), // Tag: MSID
        new TdfMemberInfo("UserSessionId", "mUserSessionId", 0xD73A6400, TdfType.UInt64, 3, true), // Tag: USID
    ];
    private ITdfMember[] __members;

    private TdfStruct<Blaze15SDK.Blaze.GameManager.RemoteInitiatorInformation?> _remoteUserInfo = new(__typeInfos[0]);
    private TdfUInt64 _mMScenarioId = new(__typeInfos[1]);
    private TdfUInt64 _mMSessionId = new(__typeInfos[2]);
    private TdfUInt64 _userSessionId = new(__typeInfos[3]);

    public NotifyRemoteMatchmakingStarted()
    {
        __members = [ _remoteUserInfo, _mMScenarioId, _mMSessionId, _userSessionId ];
    }

    public override Tdf CreateNew() => new NotifyRemoteMatchmakingStarted();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "NotifyRemoteMatchmakingStarted";
    public override string GetFullClassName() => "Blaze::GameManager::NotifyRemoteMatchmakingStarted";

    public Blaze15SDK.Blaze.GameManager.RemoteInitiatorInformation? RemoteUserInfo
    {
        get => _remoteUserInfo.Value;
        set => _remoteUserInfo.Value = value;
    }

    public ulong MMScenarioId
    {
        get => _mMScenarioId.Value;
        set => _mMScenarioId.Value = value;
    }

    public ulong MMSessionId
    {
        get => _mMSessionId.Value;
        set => _mMSessionId.Value = value;
    }

    public ulong UserSessionId
    {
        get => _userSessionId.Value;
        set => _userSessionId.Value = value;
    }

}
