using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.ByteVault;

public class Context : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("RecordCountByCategory", "mRecordCountByCategory", 0x863BB400, TdfType.Map, 0, true), // Tag: ACNT
        new TdfMemberInfo("DataAdmins", "mDataAdmins", 0x8E192D00, TdfType.List, 1, true), // Tag: CADM
        new TdfMemberInfo("CreationTime", "mCreationTime", 0x8F296100, TdfType.Int64, 2, true), // Tag: CREA
        new TdfMemberInfo("DevAdmins", "mDevAdmins", 0x92192D00, TdfType.List, 3, true), // Tag: DADM
        new TdfMemberInfo("DeletedRecordCountByCategory", "mDeletedRecordCountByCategory", 0x923BB400, TdfType.Map, 4, true), // Tag: DCNT
        new TdfMemberInfo("Deleted", "mDeleted", 0x925B0000, TdfType.Bool, 5, true), // Tag: DEL
        new TdfMemberInfo("Description", "mDescription", 0x925CE300, TdfType.String, 6, true), // Tag: DESC
        new TdfMemberInfo("NumOfDeletedRecords", "mNumOfDeletedRecords", 0x92CD3300, TdfType.Int64, 7, true), // Tag: DLTS
        new TdfMemberInfo("Label", "mLabel", 0xB218AC00, TdfType.String, 8, true), // Tag: LABL
        new TdfMemberInfo("Name", "mName", 0xBA1B6500, TdfType.String, 9, true), // Tag: NAME
        new TdfMemberInfo("RecordNamesCountByCategory", "mRecordNamesCountByCategory", 0xCA3BB400, TdfType.Map, 10, true), // Tag: RCNT
        new TdfMemberInfo("NumOfRecords", "mNumOfRecords", 0xCA58F300, TdfType.Int64, 11, true), // Tag: RECS
        new TdfMemberInfo("LastUpdateTime", "mLastUpdateTime", 0xD7093400, TdfType.Int64, 12, true), // Tag: UPDT
        new TdfMemberInfo("Version", "mVersion", 0xDA5CB300, TdfType.UInt32, 13, true), // Tag: VERS
    ];
    private ITdfMember[] __members;

    private TdfMap<string, long> _recordCountByCategory = new(__typeInfos[0]);
    private TdfList<string> _dataAdmins = new(__typeInfos[1]);
    private TdfInt64 _creationTime = new(__typeInfos[2]);
    private TdfList<string> _devAdmins = new(__typeInfos[3]);
    private TdfMap<string, long> _deletedRecordCountByCategory = new(__typeInfos[4]);
    private TdfBool _deleted = new(__typeInfos[5]);
    private TdfString _description = new(__typeInfos[6]);
    private TdfInt64 _numOfDeletedRecords = new(__typeInfos[7]);
    private TdfString _label = new(__typeInfos[8]);
    private TdfString _name = new(__typeInfos[9]);
    private TdfMap<string, long> _recordNamesCountByCategory = new(__typeInfos[10]);
    private TdfInt64 _numOfRecords = new(__typeInfos[11]);
    private TdfInt64 _lastUpdateTime = new(__typeInfos[12]);
    private TdfUInt32 _version = new(__typeInfos[13]);

    public Context()
    {
        __members = [ _recordCountByCategory, _dataAdmins, _creationTime, _devAdmins, _deletedRecordCountByCategory, _deleted, _description, _numOfDeletedRecords, _label, _name, _recordNamesCountByCategory, _numOfRecords, _lastUpdateTime, _version ];
    }

    public override Tdf CreateNew() => new Context();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "Context";
    public override string GetFullClassName() => "Blaze::ByteVault::Context";

    public IDictionary<string, long> RecordCountByCategory
    {
        get => _recordCountByCategory.Value;
        set => _recordCountByCategory.Value = value;
    }

    public IList<string> DataAdmins
    {
        get => _dataAdmins.Value;
        set => _dataAdmins.Value = value;
    }

    public long CreationTime
    {
        get => _creationTime.Value;
        set => _creationTime.Value = value;
    }

    public IList<string> DevAdmins
    {
        get => _devAdmins.Value;
        set => _devAdmins.Value = value;
    }

    public IDictionary<string, long> DeletedRecordCountByCategory
    {
        get => _deletedRecordCountByCategory.Value;
        set => _deletedRecordCountByCategory.Value = value;
    }

    public bool Deleted
    {
        get => _deleted.Value;
        set => _deleted.Value = value;
    }

    public string Description
    {
        get => _description.Value;
        set => _description.Value = value;
    }

    public long NumOfDeletedRecords
    {
        get => _numOfDeletedRecords.Value;
        set => _numOfDeletedRecords.Value = value;
    }

    public string Label
    {
        get => _label.Value;
        set => _label.Value = value;
    }

    public string Name
    {
        get => _name.Value;
        set => _name.Value = value;
    }

    public IDictionary<string, long> RecordNamesCountByCategory
    {
        get => _recordNamesCountByCategory.Value;
        set => _recordNamesCountByCategory.Value = value;
    }

    public long NumOfRecords
    {
        get => _numOfRecords.Value;
        set => _numOfRecords.Value = value;
    }

    public long LastUpdateTime
    {
        get => _lastUpdateTime.Value;
        set => _lastUpdateTime.Value = value;
    }

    public uint Version
    {
        get => _version.Value;
        set => _version.Value = value;
    }

}
