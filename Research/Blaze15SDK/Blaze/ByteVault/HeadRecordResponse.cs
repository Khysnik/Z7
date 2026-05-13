using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.ByteVault;

public class HeadRecordResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Creation", "mCreation", 0x8F296100, TdfType.Int64, 0, true), // Tag: CREA
        new TdfMemberInfo("Location", "mLocation", 0xB2F8C000, TdfType.String, 1, true), // Tag: LOC
        new TdfMemberInfo("ContentType", "mContentType", 0xB69B6500, TdfType.String, 2, true), // Tag: MIME
        new TdfMemberInfo("IsRemoteResource", "mIsRemoteResource", 0xCADBF400, TdfType.Bool, 3, true), // Tag: RMOT
        new TdfMemberInfo("PayloadSize", "mPayloadSize", 0xCE9EA500, TdfType.UInt32, 4, true), // Tag: SIZE
        new TdfMemberInfo("LastUpdate", "mLastUpdate", 0xD7093400, TdfType.Int64, 5, true), // Tag: UPDT
    ];
    private ITdfMember[] __members;

    private TdfInt64 _creation = new(__typeInfos[0]);
    private TdfString _location = new(__typeInfos[1]);
    private TdfString _contentType = new(__typeInfos[2]);
    private TdfBool _isRemoteResource = new(__typeInfos[3]);
    private TdfUInt32 _payloadSize = new(__typeInfos[4]);
    private TdfInt64 _lastUpdate = new(__typeInfos[5]);

    public HeadRecordResponse()
    {
        __members = [ _creation, _location, _contentType, _isRemoteResource, _payloadSize, _lastUpdate ];
    }

    public override Tdf CreateNew() => new HeadRecordResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "HeadRecordResponse";
    public override string GetFullClassName() => "Blaze::ByteVault::HeadRecordResponse";

    public long Creation
    {
        get => _creation.Value;
        set => _creation.Value = value;
    }

    public string Location
    {
        get => _location.Value;
        set => _location.Value = value;
    }

    public string ContentType
    {
        get => _contentType.Value;
        set => _contentType.Value = value;
    }

    public bool IsRemoteResource
    {
        get => _isRemoteResource.Value;
        set => _isRemoteResource.Value = value;
    }

    public uint PayloadSize
    {
        get => _payloadSize.Value;
        set => _payloadSize.Value = value;
    }

    public long LastUpdate
    {
        get => _lastUpdate.Value;
        set => _lastUpdate.Value = value;
    }

}
