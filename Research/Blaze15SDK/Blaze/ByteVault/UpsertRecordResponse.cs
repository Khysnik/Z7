using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.ByteVault;

public class UpsertRecordResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Location", "mLocation", 0xB2F8C000, TdfType.String, 0, true), // Tag: LOC
        new TdfMemberInfo("Record", "mRecord", 0xCA3A6E00, TdfType.Struct, 1, true), // Tag: RCIN
        new TdfMemberInfo("ResourceCreated", "mResourceCreated", 0xCA3CA500, TdfType.Bool, 2, true), // Tag: RCRE
        new TdfMemberInfo("IsRemoteResource", "mIsRemoteResource", 0xCADBF400, TdfType.Bool, 3, true), // Tag: RMOT
    ];
    private ITdfMember[] __members;

    private TdfString _location = new(__typeInfos[0]);
    private TdfStruct<Blaze15SDK.Blaze.ByteVault.UpsertRecordInfo?> _record = new(__typeInfos[1]);
    private TdfBool _resourceCreated = new(__typeInfos[2]);
    private TdfBool _isRemoteResource = new(__typeInfos[3]);

    public UpsertRecordResponse()
    {
        __members = [ _location, _record, _resourceCreated, _isRemoteResource ];
    }

    public override Tdf CreateNew() => new UpsertRecordResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "UpsertRecordResponse";
    public override string GetFullClassName() => "Blaze::ByteVault::UpsertRecordResponse";

    public string Location
    {
        get => _location.Value;
        set => _location.Value = value;
    }

    public Blaze15SDK.Blaze.ByteVault.UpsertRecordInfo? Record
    {
        get => _record.Value;
        set => _record.Value = value;
    }

    public bool ResourceCreated
    {
        get => _resourceCreated.Value;
        set => _resourceCreated.Value = value;
    }

    public bool IsRemoteResource
    {
        get => _isRemoteResource.Value;
        set => _isRemoteResource.Value = value;
    }

}
