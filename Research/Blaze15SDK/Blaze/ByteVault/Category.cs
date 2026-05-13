using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.ByteVault;

public class Category : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("CategoryName", "mCategoryName", 0x8E1D0000, TdfType.String, 0, true), // Tag: CAT
        new TdfMemberInfo("Configuration", "mConfiguration", 0x8EFBA600, TdfType.Struct, 1, true), // Tag: CONF
        new TdfMemberInfo("CreationTime", "mCreationTime", 0x8F296100, TdfType.Int64, 2, true), // Tag: CREA
        new TdfMemberInfo("DeletedRecordCountByName", "mDeletedRecordCountByName", 0x923BB400, TdfType.Map, 3, true), // Tag: DCNT
        new TdfMemberInfo("Deleted", "mDeleted", 0x925B0000, TdfType.Bool, 4, true), // Tag: DEL
        new TdfMemberInfo("Description", "mDescription", 0x925CE300, TdfType.String, 5, true), // Tag: DESC
        new TdfMemberInfo("NumOfDeletedRecords", "mNumOfDeletedRecords", 0x92CD3300, TdfType.Int64, 6, true), // Tag: DLTS
        new TdfMemberInfo("RecordCountByName", "mRecordCountByName", 0xCA3BB400, TdfType.Map, 7, true), // Tag: RCNT
        new TdfMemberInfo("NumOfRecords", "mNumOfRecords", 0xCA58F300, TdfType.Int64, 8, true), // Tag: RECS
        new TdfMemberInfo("TrustedSources", "mTrustedSources", 0xD32CF400, TdfType.List, 9, true), // Tag: TRST
        new TdfMemberInfo("LastUpdateTime", "mLastUpdateTime", 0xD7093400, TdfType.Int64, 10, true), // Tag: UPDT
    ];
    private ITdfMember[] __members;

    private TdfString _categoryName = new(__typeInfos[0]);
    private TdfStruct<CategorySettings?> _configuration = new(__typeInfos[1]);
    private TdfInt64 _creationTime = new(__typeInfos[2]);
    private TdfMap<string, long> _deletedRecordCountByName = new(__typeInfos[3]);
    private TdfBool _deleted = new(__typeInfos[4]);
    private TdfString _description = new(__typeInfos[5]);
    private TdfInt64 _numOfDeletedRecords = new(__typeInfos[6]);
    private TdfMap<string, long> _recordCountByName = new(__typeInfos[7]);
    private TdfInt64 _numOfRecords = new(__typeInfos[8]);
    private TdfList<string> _trustedSources = new(__typeInfos[9]);
    private TdfInt64 _lastUpdateTime = new(__typeInfos[10]);

    public Category()
    {
        __members = [ _categoryName, _configuration, _creationTime, _deletedRecordCountByName, _deleted, _description, _numOfDeletedRecords, _recordCountByName, _numOfRecords, _trustedSources, _lastUpdateTime ];
    }

    public override Tdf CreateNew() => new Category();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "Category";
    public override string GetFullClassName() => "Blaze::ByteVault::Category";

    public string CategoryName
    {
        get => _categoryName.Value;
        set => _categoryName.Value = value;
    }

    public CategorySettings? Configuration
    {
        get => _configuration.Value;
        set => _configuration.Value = value;
    }

    public long CreationTime
    {
        get => _creationTime.Value;
        set => _creationTime.Value = value;
    }

    public IDictionary<string, long> DeletedRecordCountByName
    {
        get => _deletedRecordCountByName.Value;
        set => _deletedRecordCountByName.Value = value;
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

    public IDictionary<string, long> RecordCountByName
    {
        get => _recordCountByName.Value;
        set => _recordCountByName.Value = value;
    }

    public long NumOfRecords
    {
        get => _numOfRecords.Value;
        set => _numOfRecords.Value = value;
    }

    public IList<string> TrustedSources
    {
        get => _trustedSources.Value;
        set => _trustedSources.Value = value;
    }

    public long LastUpdateTime
    {
        get => _lastUpdateTime.Value;
        set => _lastUpdateTime.Value = value;
    }

}
