using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Authentication;

public class GetDecryptedBlazeIdsResponseError : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("FailedEncryptedBlazeIds", "mFailedEncryptedBlazeIds", 0x9A5BA300, TdfType.List, 0, true), // Tag: FENC
    ];
    private ITdfMember[] __members;
    private TdfList<string> _failedEncryptedBlazeIds = new(__typeInfos[0]);
    public GetDecryptedBlazeIdsResponseError() { __members = [ _failedEncryptedBlazeIds ]; }
    public override Tdf CreateNew() => new GetDecryptedBlazeIdsResponseError();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetDecryptedBlazeIdsResponseError";
    public override string GetFullClassName() => "Blaze::Authentication::GetDecryptedBlazeIdsResponseError";
    public IList<string> FailedEncryptedBlazeIds { get => _failedEncryptedBlazeIds.Value; set => _failedEncryptedBlazeIds.Value = value; }
}
