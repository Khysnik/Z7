using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Stats;

public class FilteredLeaderboardStatsRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("EnforceCutoffStatValue", "mEnforceCutoffStatValue", 0x8EF9B600, TdfType.Bool, 0, true), // Tag: COFV
        new TdfMemberInfo("IncludeStatlessEntities", "mIncludeStatlessEntities", 0x9A9B3400, TdfType.Bool, 1, true), // Tag: FILT
        new TdfMemberInfo("ListOfIds", "mListOfIds", 0xA64B3300, TdfType.List, 2, true), // Tag: IDLS
        new TdfMemberInfo("KeyScopeNameValueMap", "mKeyScopeNameValueMap", 0xAF3D6D00, TdfType.Map, 3, true), // Tag: KSUM
        new TdfMemberInfo("BoardId", "mBoardId", 0xB22A6400, TdfType.Int32, 4, true), // Tag: LBID
        new TdfMemberInfo("Limit", "mLimit", 0xB29B7400, TdfType.UInt32, 5, true), // Tag: LIMT
        new TdfMemberInfo("BoardName", "mBoardName", 0xBA1B6500, TdfType.String, 6, true), // Tag: NAME
        new TdfMemberInfo("PeriodOffset", "mPeriodOffset", 0xC2F9A600, TdfType.Int32, 7, true), // Tag: POFF
        new TdfMemberInfo("PeriodId", "mPeriodId", 0xC32A6400, TdfType.Int32, 8, true), // Tag: PRID
        new TdfMemberInfo("Time", "mTime", 0xD29B6500, TdfType.Int32, 9, true), // Tag: TIME
        new TdfMemberInfo("UserSetId", "mUserSetId", 0xD7397400, TdfType.ObjectId, 10, true), // Tag: USET
    ];
    private ITdfMember[] __members;

    private TdfBool _enforceCutoffStatValue = new(__typeInfos[0]);
    private TdfBool _includeStatlessEntities = new(__typeInfos[1]);
    private TdfList<ulong> _listOfIds = new(__typeInfos[2]);
    private TdfMap<string, long> _keyScopeNameValueMap = new(__typeInfos[3]);
    private TdfInt32 _boardId = new(__typeInfos[4]);
    private TdfUInt32 _limit = new(__typeInfos[5]);
    private TdfString _boardName = new(__typeInfos[6]);
    private TdfInt32 _periodOffset = new(__typeInfos[7]);
    private TdfInt32 _periodId = new(__typeInfos[8]);
    private TdfInt32 _time = new(__typeInfos[9]);
    private TdfObjectId _userSetId = new(__typeInfos[10]);

    public FilteredLeaderboardStatsRequest()
    {
        __members = [ _enforceCutoffStatValue, _includeStatlessEntities, _listOfIds, _keyScopeNameValueMap, _boardId, _limit, _boardName, _periodOffset, _periodId, _time, _userSetId ];
    }

    public override Tdf CreateNew() => new FilteredLeaderboardStatsRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "FilteredLeaderboardStatsRequest";
    public override string GetFullClassName() => "Blaze::Stats::FilteredLeaderboardStatsRequest";

    public bool EnforceCutoffStatValue
    {
        get => _enforceCutoffStatValue.Value;
        set => _enforceCutoffStatValue.Value = value;
    }

    public bool IncludeStatlessEntities
    {
        get => _includeStatlessEntities.Value;
        set => _includeStatlessEntities.Value = value;
    }

    public IList<ulong> ListOfIds
    {
        get => _listOfIds.Value;
        set => _listOfIds.Value = value;
    }

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

    public uint Limit
    {
        get => _limit.Value;
        set => _limit.Value = value;
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

    public int Time
    {
        get => _time.Value;
        set => _time.Value = value;
    }

    public ObjectId UserSetId
    {
        get => _userSetId.Value;
        set => _userSetId.Value = value;
    }

}
