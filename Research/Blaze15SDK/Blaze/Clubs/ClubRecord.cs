using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Clubs;

public class ClubRecord : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("LastUpdateTime", "mLastUpdateTime", 0xB3593400, TdfType.UInt32, 0, true), // Tag: LUDT
        new TdfMemberInfo("RecordDescription", "mRecordDescription", 0xCA392300, TdfType.String, 1, true), // Tag: RCDC
        new TdfMemberInfo("RecordId", "mRecordId", 0xCA3A6400, TdfType.UInt32, 2, true), // Tag: RCID
        new TdfMemberInfo("RecordName", "mRecordName", 0xCA3BAD00, TdfType.String, 3, true), // Tag: RCNM
        new TdfMemberInfo("RecordStat", "mRecordStat", 0xCF487400, TdfType.String, 4, true), // Tag: STAT
        new TdfMemberInfo("RecordStatType", "mRecordStatType", 0xCF4E7000, TdfType.Enum, 5, true), // Tag: STYP
        new TdfMemberInfo("User", "mUser", 0xD7397200, TdfType.Struct, 6, true), // Tag: USER
    ];
    private ITdfMember[] __members;

    private TdfUInt32 _lastUpdateTime = new(__typeInfos[0]);
    private TdfString _recordDescription = new(__typeInfos[1]);
    private TdfUInt32 _recordId = new(__typeInfos[2]);
    private TdfString _recordName = new(__typeInfos[3]);
    private TdfString _recordStat = new(__typeInfos[4]);
    private TdfEnum<RecordStatType> _recordStatType = new(__typeInfos[5]);
    private TdfStruct<Blaze15SDK.Blaze.CoreIdentification?> _user = new(__typeInfos[6]);

    public ClubRecord()
    {
        __members = [ _lastUpdateTime, _recordDescription, _recordId, _recordName, _recordStat, _recordStatType, _user ];
    }

    public override Tdf CreateNew() => new ClubRecord();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ClubRecord";
    public override string GetFullClassName() => "Blaze::Clubs::ClubRecord";

    public uint LastUpdateTime
    {
        get => _lastUpdateTime.Value;
        set => _lastUpdateTime.Value = value;
    }

    public string RecordDescription
    {
        get => _recordDescription.Value;
        set => _recordDescription.Value = value;
    }

    public uint RecordId
    {
        get => _recordId.Value;
        set => _recordId.Value = value;
    }

    public string RecordName
    {
        get => _recordName.Value;
        set => _recordName.Value = value;
    }

    public string RecordStat
    {
        get => _recordStat.Value;
        set => _recordStat.Value = value;
    }

    public RecordStatType RecordStatType
    {
        get => _recordStatType.Value;
        set => _recordStatType.Value = value;
    }

    public Blaze15SDK.Blaze.CoreIdentification? User
    {
        get => _user.Value;
        set => _user.Value = value;
    }

}
