using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class GetMatchmakingDedicatedServerOverrideResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("PlayerToGameMap", "mPlayerToGameMap", 0xC349ED00, TdfType.Map, 0, true), // Tag: PTGM
    ];
    private ITdfMember[] __members;

    private TdfMap<long, ulong> _playerToGameMap = new(__typeInfos[0]);

    public GetMatchmakingDedicatedServerOverrideResponse()
    {
        __members = [ _playerToGameMap ];
    }

    public override Tdf CreateNew() => new GetMatchmakingDedicatedServerOverrideResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetMatchmakingDedicatedServerOverrideResponse";
    public override string GetFullClassName() => "Blaze::GameManager::GetMatchmakingDedicatedServerOverrideResponse";

    public IDictionary<long, ulong> PlayerToGameMap
    {
        get => _playerToGameMap.Value;
        set => _playerToGameMap.Value = value;
    }

}
