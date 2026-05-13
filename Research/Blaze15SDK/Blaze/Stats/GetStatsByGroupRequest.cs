using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Stats;

public class GetStatsByGroupRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("EntityIds", "mEntityIds", 0x96990000, TdfType.List, 0, true), // Tag: EID
        new TdfMemberInfo("KeyScopeNameValueMap", "mKeyScopeNameValueMap", 0xAF3D6D00, TdfType.Map, 1, true), // Tag: KSUM
        new TdfMemberInfo("GroupName", "mGroupName", 0xBA1B6500, TdfType.String, 2, true), // Tag: NAME
        new TdfMemberInfo("PeriodCtr", "mPeriodCtr", 0xC23D3200, TdfType.Int32, 3, true), // Tag: PCTR
        new TdfMemberInfo("PeriodOffset", "mPeriodOffset", 0xC2F9A600, TdfType.Int32, 4, true), // Tag: POFF
        new TdfMemberInfo("PeriodId", "mPeriodId", 0xC32A6400, TdfType.Int32, 5, true), // Tag: PRID
        new TdfMemberInfo("PeriodType", "mPeriodType", 0xC34E7000, TdfType.Int32, 6, true), // Tag: PTYP
        new TdfMemberInfo("Time", "mTime", 0xD29B6500, TdfType.Int32, 7, true), // Tag: TIME
        new TdfMemberInfo("ViewId", "mViewId", 0xDA990000, TdfType.UInt32, 8, true), // Tag: VID
    ];
    private ITdfMember[] __members;

    private TdfList<ulong> _entityIds = new(__typeInfos[0]);
    private TdfMap<string, long> _keyScopeNameValueMap = new(__typeInfos[1]);
    private TdfString _groupName = new(__typeInfos[2]);
    private TdfInt32 _periodCtr = new(__typeInfos[3]);
    private TdfInt32 _periodOffset = new(__typeInfos[4]);
    private TdfInt32 _periodId = new(__typeInfos[5]);
    private TdfInt32 _periodType = new(__typeInfos[6]);
    private TdfInt32 _time = new(__typeInfos[7]);
    private TdfUInt32 _viewId = new(__typeInfos[8]);

    public GetStatsByGroupRequest()
    {
        __members = [ _entityIds, _keyScopeNameValueMap, _groupName, _periodCtr, _periodOffset, _periodId, _periodType, _time, _viewId ];
    }

    public override Tdf CreateNew() => new GetStatsByGroupRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetStatsByGroupRequest";
    public override string GetFullClassName() => "Blaze::Stats::GetStatsByGroupRequest";

    public IList<ulong> EntityIds
    {
        get => _entityIds.Value;
        set => _entityIds.Value = value;
    }

    public IDictionary<string, long> KeyScopeNameValueMap
    {
        get => _keyScopeNameValueMap.Value;
        set => _keyScopeNameValueMap.Value = value;
    }

    public string GroupName
    {
        get => _groupName.Value;
        set => _groupName.Value = value;
    }

    public int PeriodCtr
    {
        get => _periodCtr.Value;
        set => _periodCtr.Value = value;
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

    public int PeriodType
    {
        get => _periodType.Value;
        set => _periodType.Value = value;
    }

    public int Time
    {
        get => _time.Value;
        set => _time.Value = value;
    }

    public uint ViewId
    {
        get => _viewId.Value;
        set => _viewId.Value = value;
    }

}
