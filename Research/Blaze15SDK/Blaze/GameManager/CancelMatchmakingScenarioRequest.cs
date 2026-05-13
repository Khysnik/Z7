using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class CancelMatchmakingScenarioRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("MatchmakingScenarioId", "mMatchmakingScenarioId", 0xB73A6400, TdfType.UInt64, 0, true), // Tag: MSID
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _matchmakingScenarioId = new(__typeInfos[0]);

    public CancelMatchmakingScenarioRequest()
    {
        __members = [ _matchmakingScenarioId ];
    }

    public override Tdf CreateNew() => new CancelMatchmakingScenarioRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "CancelMatchmakingScenarioRequest";
    public override string GetFullClassName() => "Blaze::GameManager::CancelMatchmakingScenarioRequest";

    public ulong MatchmakingScenarioId
    {
        get => _matchmakingScenarioId.Value;
        set => _matchmakingScenarioId.Value = value;
    }

}
