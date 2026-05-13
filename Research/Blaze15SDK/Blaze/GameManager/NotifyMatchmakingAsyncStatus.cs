using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class NotifyMatchmakingAsyncStatus : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("MatchmakingAsyncStatusList", "mMatchmakingAsyncStatusList", 0x873A6C00, TdfType.List, 0, true), // Tag: ASIL
        new TdfMemberInfo("MatchmakingScenarioId", "mMatchmakingScenarioId", 0xB738E400, TdfType.UInt64, 1, true), // Tag: MSCD
        new TdfMemberInfo("MatchmakingSessionId", "mMatchmakingSessionId", 0xB73A6400, TdfType.UInt64, 2, true), // Tag: MSID
        new TdfMemberInfo("MatchmakingSessionAge", "mMatchmakingSessionAge", 0xCE19E500, TdfType.UInt32, 3, true), // Tag: SAGE
        new TdfMemberInfo("UserSessionId", "mUserSessionId", 0xD73A6400, TdfType.UInt64, 4, true), // Tag: USID
    ];
    private ITdfMember[] __members;

    private TdfList<Blaze15SDK.Blaze.GameManager.MatchmakingAsyncStatus> _matchmakingAsyncStatusList = new(__typeInfos[0]);
    private TdfUInt64 _matchmakingScenarioId = new(__typeInfos[1]);
    private TdfUInt64 _matchmakingSessionId = new(__typeInfos[2]);
    private TdfUInt32 _matchmakingSessionAge = new(__typeInfos[3]);
    private TdfUInt64 _userSessionId = new(__typeInfos[4]);

    public NotifyMatchmakingAsyncStatus()
    {
        __members = [ _matchmakingAsyncStatusList, _matchmakingScenarioId, _matchmakingSessionId, _matchmakingSessionAge, _userSessionId ];
    }

    public override Tdf CreateNew() => new NotifyMatchmakingAsyncStatus();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "NotifyMatchmakingAsyncStatus";
    public override string GetFullClassName() => "Blaze::GameManager::NotifyMatchmakingAsyncStatus";

    public IList<Blaze15SDK.Blaze.GameManager.MatchmakingAsyncStatus> MatchmakingAsyncStatusList
    {
        get => _matchmakingAsyncStatusList.Value;
        set => _matchmakingAsyncStatusList.Value = value;
    }

    public ulong MatchmakingScenarioId
    {
        get => _matchmakingScenarioId.Value;
        set => _matchmakingScenarioId.Value = value;
    }

    public ulong MatchmakingSessionId
    {
        get => _matchmakingSessionId.Value;
        set => _matchmakingSessionId.Value = value;
    }

    public uint MatchmakingSessionAge
    {
        get => _matchmakingSessionAge.Value;
        set => _matchmakingSessionAge.Value = value;
    }

    public ulong UserSessionId
    {
        get => _userSessionId.Value;
        set => _userSessionId.Value = value;
    }

}
