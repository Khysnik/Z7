using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Stats;

public class LeaderboardTreeNodes : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("LeaderboardTreeNodes", "mLeaderboardTreeNodes", 0xB34BAC00, TdfType.List, 0, true), // Tag: LTNL
    ];
    private ITdfMember[] __members;

    private TdfList<Blaze15SDK.Blaze.Stats.LeaderboardTreeNode> _leaderboardTreeNodes = new(__typeInfos[0]);

    public LeaderboardTreeNodes()
    {
        __members = [ _leaderboardTreeNodes ];
    }

    public override Tdf CreateNew() => new LeaderboardTreeNodes();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "LeaderboardTreeNodes";
    public override string GetFullClassName() => "Blaze::Stats::LeaderboardTreeNodes";

    public IList<Blaze15SDK.Blaze.Stats.LeaderboardTreeNode> Nodes
    {
        get => _leaderboardTreeNodes.Value;
        set => _leaderboardTreeNodes.Value = value;
    }

}
