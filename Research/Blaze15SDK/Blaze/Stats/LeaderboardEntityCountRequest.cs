using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Stats;

public class LeaderboardEntityCountRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("KeyScopeNameValueMap", "mKeyScopeNameValueMap", 0xAF3D6D00, TdfType.Map, 0, true), // Tag: KSUM
        new TdfMemberInfo("BoardId", "mBoardId", 0xB22A6400, TdfType.Int32, 1, true), // Tag: LBID
        new TdfMemberInfo("BoardName", "mBoardName", 0xBA1B6500, TdfType.String, 2, true), // Tag: NAME
        new TdfMemberInfo("PeriodOffset", "mPeriodOffset", 0xC2F9A600, TdfType.Int32, 3, true), // Tag: POFF
        new TdfMemberInfo("PeriodId", "mPeriodId", 0xC32A6400, TdfType.Int32, 4, true), // Tag: PRID
    ];
    private ITdfMember[] __members;

    private TdfMap<string, long> _keyScopeNameValueMap = new(__typeInfos[0]);
    private TdfInt32 _boardId = new(__typeInfos[1]);
    private TdfString _boardName = new(__typeInfos[2]);
    private TdfInt32 _periodOffset = new(__typeInfos[3]);
    private TdfInt32 _periodId = new(__typeInfos[4]);

    public LeaderboardEntityCountRequest()
    {
        __members = [ _keyScopeNameValueMap, _boardId, _boardName, _periodOffset, _periodId ];
    }

    public override Tdf CreateNew() => new LeaderboardEntityCountRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "LeaderboardEntityCountRequest";
    public override string GetFullClassName() => "Blaze::Stats::LeaderboardEntityCountRequest";

    public IDictionary<string, long> KeyScopeNameValueMap
    {
        get => _keyScopeNameValueMap.Value;
        set => _keyScopeNameValueMap.Value = value;
    }

    public int BoardId
    {
        get => _boardId.Value;
        set => _boardId.Value = value;
    }

    public string BoardName
    {
        get => _boardName.Value;
        set => _boardName.Value = value;
    }

    public int PeriodOffset
    {
        get => _periodOffset.Value;
        set => _periodOffset.Value = value;
    }

    public int PeriodId
    {
        get => _periodId.Value;
        set => _periodId.Value = value;
    }

}
