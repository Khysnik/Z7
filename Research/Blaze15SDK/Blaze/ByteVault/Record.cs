using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.ByteVault;

public class Record : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("DeletedTime", "mDeletedTime", 0x925B3400, TdfType.Int64, 0, true), // Tag: DELT
        new TdfMemberInfo("Info", "mInfo", 0xA6E9AF00, TdfType.Struct, 1, true), // Tag: INFO
        new TdfMemberInfo("Payload", "mPayload", 0xB2F86400, TdfType.Struct, 2, true), // Tag: LOAD
    ];
    private ITdfMember[] __members;

    private TdfInt64 _deletedTime = new(__typeInfos[0]);
    private TdfStruct<RecordInfo?> _info = new(__typeInfos[1]);
    private TdfStruct<RecordPayload?> _payload = new(__typeInfos[2]);

    public Record()
    {
        __members = [ _deletedTime, _info, _payload ];
    }

    public override Tdf CreateNew() => new Record();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "Record";
    public override string GetFullClassName() => "Blaze::ByteVault::Record";

    public long DeletedTime
    {
        get => _deletedTime.Value;
        set => _deletedTime.Value = value;
    }

    public RecordInfo? Info
    {
        get => _info.Value;
        set => _info.Value = value;
    }

    public RecordPayload? Payload
    {
        get => _payload.Value;
        set => _payload.Value = value;
    }

}
