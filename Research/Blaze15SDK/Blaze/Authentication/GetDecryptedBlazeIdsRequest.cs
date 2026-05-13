using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Authentication;

public class GetDecryptedBlazeIdsRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("EncryptedBlazeIds", "mEncryptedBlazeIds", 0x96993300, TdfType.List, 0, true), // Tag: EIDS
    ];
    private ITdfMember[] __members;
    private TdfList<string> _encryptedBlazeIds = new(__typeInfos[0]);
    public GetDecryptedBlazeIdsRequest() { __members = [ _encryptedBlazeIds ]; }
    public override Tdf CreateNew() => new GetDecryptedBlazeIdsRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetDecryptedBlazeIdsRequest";
    public override string GetFullClassName() => "Blaze::Authentication::GetDecryptedBlazeIdsRequest";
    public IList<string> EncryptedBlazeIds { get => _encryptedBlazeIds.Value; set => _encryptedBlazeIds.Value = value; }
}
