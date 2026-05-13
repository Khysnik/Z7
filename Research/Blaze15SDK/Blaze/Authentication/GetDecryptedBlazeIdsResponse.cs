using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Authentication;

public class GetDecryptedBlazeIdsResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("DecryptedIds", "mDecryptedIds", 0x92993300, TdfType.Map, 0, true), // Tag: DIDS
    ];
    private ITdfMember[] __members;
    private TdfMap<string, long> _decryptedIds = new(__typeInfos[0]);
    public GetDecryptedBlazeIdsResponse() { __members = [ _decryptedIds ]; }
    public override Tdf CreateNew() => new GetDecryptedBlazeIdsResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetDecryptedBlazeIdsResponse";
    public override string GetFullClassName() => "Blaze::Authentication::GetDecryptedBlazeIdsResponse";
    public IDictionary<string, long> DecryptedIds { get => _decryptedIds.Value; set => _decryptedIds.Value = value; }
}
