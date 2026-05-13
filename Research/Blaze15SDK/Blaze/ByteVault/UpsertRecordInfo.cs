using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.ByteVault;

public class UpsertRecordInfo : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Owner", "mOwner", 0xBF7BB200, TdfType.Struct, 0, true), // Tag: OWNR
        new TdfMemberInfo("RecordName", "mRecordName", 0xCA58C000, TdfType.String, 1, true), // Tag: REC
        new TdfMemberInfo("LastUpdateTime", "mLastUpdateTime", 0xD7093400, TdfType.Int64, 2, true), // Tag: UPDT
    ];
    private ITdfMember[] __members;

    private TdfStruct<User?> _owner = new(__typeInfos[0]);
    private TdfString _recordName = new(__typeInfos[1]);
    private TdfInt64 _lastUpdateTime = new(__typeInfos[2]);

    public UpsertRecordInfo()
    {
        __members = [ _owner, _recordName, _lastUpdateTime ];
    }

    public override Tdf CreateNew() => new UpsertRecordInfo();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "UpsertRecordInfo";
    public override string GetFullClassName() => "Blaze::ByteVault::UpsertRecordInfo";

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
