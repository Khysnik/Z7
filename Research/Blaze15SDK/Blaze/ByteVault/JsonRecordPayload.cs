using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.ByteVault;

public class JsonRecordPayload : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ContentType", "mContentType", 0xB69B6500, TdfType.String, 0, true), // Tag: MIME
        new TdfMemberInfo("Blob", "mBlob", 0xCE487400, TdfType.String, 1, true), // Tag: SDAT
    ];
    private ITdfMember[] __members;

    private TdfString _contentType = new(__typeInfos[0]);
    private TdfString _blob = new(__typeInfos[1]);

    public JsonRecordPayload()
    {
        __members = [ _contentType, _blob ];
    }

    public override Tdf CreateNew() => new JsonRecordPayload();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "JsonRecordPayload";
    public override string GetFullClassName() => "Blaze::ByteVault::JsonRecordPayload";

    public string ContentType
    {
        get => _contentType.Value;
        set => _contentType.Value = value;
    }

    public string Blob
    {
        get => _blob.Value;
        set => _blob.Value = value;
    }

}
