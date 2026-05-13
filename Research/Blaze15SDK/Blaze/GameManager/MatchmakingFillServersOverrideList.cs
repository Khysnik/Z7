using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class MatchmakingFillServersOverrideList : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("GameIdList", "mGameIdList", 0x9E990000, TdfType.List, 0, true), // Tag: GID
    ];
    private ITdfMember[] __members;

    private TdfList<ulong> _gameIdList = new(__typeInfos[0]);

    public MatchmakingFillServersOverrideList()
    {
        __members = [ _gameIdList ];
    }

    public override Tdf CreateNew() => new MatchmakingFillServersOverrideList();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "MatchmakingFillServersOverrideList";
    public override string GetFullClassName() => "Blaze::GameManager::MatchmakingFillServersOverrideList";

    public IList<ulong> GameIdList
    {
        get => _gameIdList.Value;
        set => _gameIdList.Value = value;
    }

}
