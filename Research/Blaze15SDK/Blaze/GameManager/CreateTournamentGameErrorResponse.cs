using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class CreateTournamentGameErrorResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("FailedEncryptedBlazeIds", "mFailedEncryptedBlazeIds", 0x9A5BA300, TdfType.List, 0, true), // Tag: FENC
    ];
    private ITdfMember[] __members;

    private TdfList<string> _failedEncryptedBlazeIds = new(__typeInfos[0]);

    public CreateTournamentGameErrorResponse()
    {
        __members = [ _failedEncryptedBlazeIds ];
    }

    public override Tdf CreateNew() => new CreateTournamentGameErrorResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "CreateTournamentGameErrorResponse";
    public override string GetFullClassName() => "Blaze::GameManager::CreateTournamentGameErrorResponse";

    public IList<string> FailedEncryptedBlazeIds
    {
        get => _failedEncryptedBlazeIds.Value;
        set => _failedEncryptedBlazeIds.Value = value;
    }

}
