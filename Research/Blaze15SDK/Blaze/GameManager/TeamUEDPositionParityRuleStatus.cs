using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class TeamUEDPositionParityRuleStatus : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("MaxUEDDifferenceAcceptedBottomPlayers", "mMaxUEDDifferenceAcceptedBottomPlayers", 0x8A4A6600, TdfType.UInt64, 0, true), // Tag: BDIF
        new TdfMemberInfo("BottomPlayersCounted", "mBottomPlayersCounted", 0x8AFD2E00, TdfType.UInt16, 1, true), // Tag: BOTN
        new TdfMemberInfo("RuleName", "mRuleName", 0xBA1B6500, TdfType.String, 2, true), // Tag: NAME
        new TdfMemberInfo("MaxUEDDifferenceAcceptedTopPlayers", "mMaxUEDDifferenceAcceptedTopPlayers", 0xD24A6600, TdfType.UInt64, 3, true), // Tag: TDIF
        new TdfMemberInfo("TopPlayersCounted", "mTopPlayersCounted", 0xD2FC2E00, TdfType.UInt16, 4, true), // Tag: TOPN
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _maxUEDDifferenceAcceptedBottomPlayers = new(__typeInfos[0]);
    private TdfUInt16 _bottomPlayersCounted = new(__typeInfos[1]);
    private TdfString _ruleName = new(__typeInfos[2]);
    private TdfUInt64 _maxUEDDifferenceAcceptedTopPlayers = new(__typeInfos[3]);
    private TdfUInt16 _topPlayersCounted = new(__typeInfos[4]);

    public TeamUEDPositionParityRuleStatus()
    {
        __members = [ _maxUEDDifferenceAcceptedBottomPlayers, _bottomPlayersCounted, _ruleName, _maxUEDDifferenceAcceptedTopPlayers, _topPlayersCounted ];
    }

    public override Tdf CreateNew() => new TeamUEDPositionParityRuleStatus();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "TeamUEDPositionParityRuleStatus";
    public override string GetFullClassName() => "Blaze::GameManager::TeamUEDPositionParityRuleStatus";

    public ulong MaxUEDDifferenceAcceptedBottomPlayers
    {
        get => _maxUEDDifferenceAcceptedBottomPlayers.Value;
        set => _maxUEDDifferenceAcceptedBottomPlayers.Value = value;
    }

    public ushort BottomPlayersCounted
    {
        get => _bottomPlayersCounted.Value;
        set => _bottomPlayersCounted.Value = value;
    }

    public string RuleName
    {
        get => _ruleName.Value;
        set => _ruleName.Value = value;
    }

    public ulong MaxUEDDifferenceAcceptedTopPlayers
    {
        get => _maxUEDDifferenceAcceptedTopPlayers.Value;
        set => _maxUEDDifferenceAcceptedTopPlayers.Value = value;
    }

    public ushort TopPlayersCounted
    {
        get => _topPlayersCounted.Value;
        set => _topPlayersCounted.Value = value;
    }

}
