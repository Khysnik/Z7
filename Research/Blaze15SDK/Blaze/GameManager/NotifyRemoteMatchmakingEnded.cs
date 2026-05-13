using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class NotifyRemoteMatchmakingEnded : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("GameId", "mGameId", 0x9E990000, TdfType.UInt64, 0, true), // Tag: GID
        new TdfMemberInfo("RemoteUserInfo", "mRemoteUserInfo", 0xA6E9AF00, TdfType.Struct, 1, true), // Tag: INFO
        new TdfMemberInfo("MMScenarioId", "mMMScenarioId", 0xB738E400, TdfType.UInt64, 2, true), // Tag: MSCD
        new TdfMemberInfo("MMSessionId", "mMMSessionId", 0xB73A6400, TdfType.UInt64, 3, true), // Tag: MSID
        new TdfMemberInfo("MatchmakingResult", "mMatchmakingResult", 0xCB3B3400, TdfType.Enum, 4, true), // Tag: RSLT
        new TdfMemberInfo("UserSessionId", "mUserSessionId", 0xD73A6400, TdfType.UInt64, 5, true), // Tag: USID
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _gameId = new(__typeInfos[0]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.RemoteInitiatorInformation?> _remoteUserInfo = new(__typeInfos[1]);
    private TdfUInt64 _mMScenarioId = new(__typeInfos[2]);
    private TdfUInt64 _mMSessionId = new(__typeInfos[3]);
    private TdfEnum<Blaze15SDK.Blaze.GameManager.MatchmakingResult> _matchmakingResult = new(__typeInfos[4]);
    private TdfUInt64 _userSessionId = new(__typeInfos[5]);

    public NotifyRemoteMatchmakingEnded()
    {
        __members = [ _gameId, _remoteUserInfo, _mMScenarioId, _mMSessionId, _matchmakingResult, _userSessionId ];
    }

    public override Tdf CreateNew() => new NotifyRemoteMatchmakingEnded();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "NotifyRemoteMatchmakingEnded";
    public override string GetFullClassName() => "Blaze::GameManager::NotifyRemoteMatchmakingEnded";

    public ulong GameId
    {
        get => _gameId.Value;
        set => _gameId.Value = value;
    }

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

    public Blaze15SDK.Blaze.GameManager.MatchmakingResult MatchmakingResult
    {
        get => _matchmakingResult.Value;
        set => _matchmakingResult.Value = value;
    }

    public ulong UserSessionId
    {
        get => _userSessionId.Value;
        set => _userSessionId.Value = value;
    }

}
