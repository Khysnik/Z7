using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.ByteVault;

public class HistoryRecordInfo : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Archived", "mArchived", 0x8728E800, TdfType.Bool, 0, true), // Tag: ARCH
        new TdfMemberInfo("CreationTime", "mCreationTime", 0x8F296100, TdfType.Int64, 1, true), // Tag: CREA
        new TdfMemberInfo("Owner", "mOwner", 0xBF7BB200, TdfType.Struct, 2, true), // Tag: OWNR
        new TdfMemberInfo("RecordName", "mRecordName", 0xCA58C000, TdfType.String, 3, true), // Tag: REC
        new TdfMemberInfo("LastUpdateTime", "mLastUpdateTime", 0xD7093400, TdfType.Int64, 4, true), // Tag: UPDT
    ];
    private ITdfMember[] __members;

    private TdfBool _archived = new(__typeInfos[0]);
    private TdfInt64 _creationTime = new(__typeInfos[1]);
    private TdfStruct<User?> _owner = new(__typeInfos[2]);
    private TdfString _recordName = new(__typeInfos[3]);
    private TdfInt64 _lastUpdateTime = new(__typeInfos[4]);

    public HistoryRecordInfo()
    {
        __members = [ _archived, _creationTime, _owner, _recordName, _lastUpdateTime ];
    }

    public override Tdf CreateNew() => new HistoryRecordInfo();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "HistoryRecordInfo";
    public override string GetFullClassName() => "Blaze::ByteVault::HistoryRecordInfo";

    public bool Archived
    {
        get => _archived.Value;
        set => _archived.Value = value;
    }

    public long CreationTime
    {
        get => _creationTime.Value;
        set => _creationTime.Value = value;
    }

    public User? Owner
    {
        get => _owner.Value;
        set => _owner.Value = value;
    }

    public string RecordName
    {
        get => _recordName.Value;
        set => _recordName.Value = value;
    }

    public long LastUpdateTime
    {
        get => _lastUpdateTime.Value;
        set => _lastUpdateTime.Value = value;
    }

}
