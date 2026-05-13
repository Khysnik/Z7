using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Clubs;

public class ClubsComponentSettings : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("AwardSettings", "mAwardSettings", 0x877CF400, TdfType.List, 0, true), // Tag: AWST
        new TdfMemberInfo("ClubDivisionSize", "mClubDivisionSize", 0x8EC93300, TdfType.UInt16, 1, true), // Tag: CLDS
        new TdfMemberInfo("DomainList", "mDomainList", 0x92DBB300, TdfType.List, 2, true), // Tag: DMNS
        new TdfMemberInfo("MaxEvents", "mMaxEvents", 0xB7897600, TdfType.UInt16, 3, true), // Tag: MXEV
        new TdfMemberInfo("MaxRivalsPerClub", "mMaxRivalsPerClub", 0xB78CB600, TdfType.UInt16, 4, true), // Tag: MXRV
        new TdfMemberInfo("PurgeHour", "mPurgeHour", 0xC35A3200, TdfType.UInt16, 5, true), // Tag: PUHR
        new TdfMemberInfo("RecordSettings", "mRecordSettings", 0xCA5CF400, TdfType.List, 6, true), // Tag: REST
        new TdfMemberInfo("SeasonRolloverTime", "mSeasonRolloverTime", 0xCEFDB200, TdfType.Int32, 7, true), // Tag: SOVR
        new TdfMemberInfo("SeasonStartTime", "mSeasonStartTime", 0xCF4CB400, TdfType.Int32, 8, true), // Tag: STRT
    ];
    private ITdfMember[] __members;

    private TdfList<AwardSettings> _awardSettings = new(__typeInfos[0]);
    private TdfUInt16 _clubDivisionSize = new(__typeInfos[1]);
    private TdfList<ClubDomain> _domainList = new(__typeInfos[2]);
    private TdfUInt16 _maxEvents = new(__typeInfos[3]);
    private TdfUInt16 _maxRivalsPerClub = new(__typeInfos[4]);
    private TdfUInt16 _purgeHour = new(__typeInfos[5]);
    private TdfList<RecordSettings> _recordSettings = new(__typeInfos[6]);
    private TdfInt32 _seasonRolloverTime = new(__typeInfos[7]);
    private TdfInt32 _seasonStartTime = new(__typeInfos[8]);

    public ClubsComponentSettings()
    {
        __members = [ _awardSettings, _clubDivisionSize, _domainList, _maxEvents, _maxRivalsPerClub, _purgeHour, _recordSettings, _seasonRolloverTime, _seasonStartTime ];
    }

    public override Tdf CreateNew() => new ClubsComponentSettings();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ClubsComponentSettings";
    public override string GetFullClassName() => "Blaze::Clubs::ClubsComponentSettings";

    public IList<AwardSettings> AwardSettings
    {
        get => _awardSettings.Value;
        set => _awardSettings.Value = value;
    }

    public ushort ClubDivisionSize
    {
        get => _clubDivisionSize.Value;
        set => _clubDivisionSize.Value = value;
    }

    public IList<ClubDomain> DomainList
    {
        get => _domainList.Value;
        set => _domainList.Value = value;
    }

    public ushort MaxEvents
    {
        get => _maxEvents.Value;
        set => _maxEvents.Value = value;
    }

    public ushort MaxRivalsPerClub
    {
        get => _maxRivalsPerClub.Value;
        set => _maxRivalsPerClub.Value = value;
    }

    public ushort PurgeHour
    {
        get => _purgeHour.Value;
        set => _purgeHour.Value = value;
    }

    public IList<RecordSettings> RecordSettings
    {
        get => _recordSettings.Value;
        set => _recordSettings.Value = value;
    }

    public int SeasonRolloverTime
    {
        get => _seasonRolloverTime.Value;
        set => _seasonRolloverTime.Value = value;
    }

    public int SeasonStartTime
    {
        get => _seasonStartTime.Value;
        set => _seasonStartTime.Value = value;
    }

}
