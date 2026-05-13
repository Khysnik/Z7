using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Clubs;

public class ClubInfo : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("AwardCount", "mAwardCount", 0x8778EE00, TdfType.UInt32, 0, true), // Tag: AWCN
        new TdfMemberInfo("MemberCount", "mMemberCount", 0x8E9B6300, TdfType.UInt32, 1, true), // Tag: CIMC
        new TdfMemberInfo("CreationTime", "mCreationTime", 0x8F2D2900, TdfType.UInt32, 2, true), // Tag: CRTI
        new TdfMemberInfo("GmCount", "mGmCount", 0x9ED8EE00, TdfType.UInt32, 3, true), // Tag: GMCN
        new TdfMemberInfo("LastActiveTime", "mLastActiveTime", 0xB21D2900, TdfType.UInt32, 4, true), // Tag: LATI
        new TdfMemberInfo("LastGameTime", "mLastGameTime", 0xB27D2D00, TdfType.UInt32, 5, true), // Tag: LGTM
        new TdfMemberInfo("LastGameResult", "mLastGameResult", 0xB339F200, TdfType.String, 6, true), // Tag: LSGR
        new TdfMemberInfo("LastOppo", "mLastOppo", 0xB33BF000, TdfType.UInt64, 7, true), // Tag: LSOP
        new TdfMemberInfo("MemberOnlineStatusCounts", "mMemberOnlineStatusCounts", 0xB738EF00, TdfType.Map, 8, true), // Tag: MSCO
        new TdfMemberInfo("LastOppoName", "mLastOppoName", 0xBF0BAD00, TdfType.String, 9, true), // Tag: OPNM
        new TdfMemberInfo("RivalCount", "mRivalCount", 0xCB68EE00, TdfType.UInt32, 10, true), // Tag: RVCN
    ];
    private ITdfMember[] __members;

    private TdfUInt32 _awardCount = new(__typeInfos[0]);
    private TdfUInt32 _memberCount = new(__typeInfos[1]);
    private TdfUInt32 _creationTime = new(__typeInfos[2]);
    private TdfUInt32 _gmCount = new(__typeInfos[3]);
    private TdfUInt32 _lastActiveTime = new(__typeInfos[4]);
    private TdfUInt32 _lastGameTime = new(__typeInfos[5]);
    private TdfString _lastGameResult = new(__typeInfos[6]);
    private TdfUInt64 _lastOppo = new(__typeInfos[7]);
    private TdfMap<MemberOnlineStatus, ushort> _memberOnlineStatusCounts = new(__typeInfos[8]);
    private TdfString _lastOppoName = new(__typeInfos[9]);
    private TdfUInt32 _rivalCount = new(__typeInfos[10]);

    public ClubInfo()
    {
        __members = [ _awardCount, _memberCount, _creationTime, _gmCount, _lastActiveTime, _lastGameTime, _lastGameResult, _lastOppo, _memberOnlineStatusCounts, _lastOppoName, _rivalCount ];
    }

    public override Tdf CreateNew() => new ClubInfo();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ClubInfo";
    public override string GetFullClassName() => "Blaze::Clubs::ClubInfo";

    public uint AwardCount
    {
        get => _awardCount.Value;
        set => _awardCount.Value = value;
    }

    public uint MemberCount
    {
        get => _memberCount.Value;
        set => _memberCount.Value = value;
    }

    public uint CreationTime
    {
        get => _creationTime.Value;
        set => _creationTime.Value = value;
    }

    public uint GmCount
    {
        get => _gmCount.Value;
        set => _gmCount.Value = value;
    }

    public uint LastActiveTime
    {
        get => _lastActiveTime.Value;
        set => _lastActiveTime.Value = value;
    }

    public uint LastGameTime
    {
        get => _lastGameTime.Value;
        set => _lastGameTime.Value = value;
    }

    public string LastGameResult
    {
        get => _lastGameResult.Value;
        set => _lastGameResult.Value = value;
    }

    public ulong LastOppo
    {
        get => _lastOppo.Value;
        set => _lastOppo.Value = value;
    }

    public IDictionary<MemberOnlineStatus, ushort> MemberOnlineStatusCounts
    {
        get => _memberOnlineStatusCounts.Value;
        set => _memberOnlineStatusCounts.Value = value;
    }

    public string LastOppoName
    {
        get => _lastOppoName.Value;
        set => _lastOppoName.Value = value;
    }

    public uint RivalCount
    {
        get => _rivalCount.Value;
        set => _rivalCount.Value = value;
    }

}
