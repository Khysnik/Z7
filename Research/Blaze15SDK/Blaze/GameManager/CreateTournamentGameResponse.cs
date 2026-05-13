using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class CreateTournamentGameResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ExternalSessionIdentification", "mExternalSessionIdentification", 0x973A6400, TdfType.Struct, 0, true), // Tag: ESID
        new TdfMemberInfo("GameId", "mGameId", 0x9E990000, TdfType.UInt64, 1, true), // Tag: GID
        new TdfMemberInfo("JoinState", "mJoinState", 0xAA7CC000, TdfType.Enum, 2, true), // Tag: JGS
        new TdfMemberInfo("Scid", "mScid", 0xCE3A6400, TdfType.String, 3, true), // Tag: SCID
    ];
    private ITdfMember[] __members;

    private TdfStruct<Blaze15SDK.Blaze.ExternalSessionIdentification?> _externalSessionIdentification = new(__typeInfos[0]);
    private TdfUInt64 _gameId = new(__typeInfos[1]);
    private TdfEnum<Blaze15SDK.Blaze.GameManager.JoinState> _joinState = new(__typeInfos[2]);
    private TdfString _scid = new(__typeInfos[3]);

    public CreateTournamentGameResponse()
    {
        __members = [ _externalSessionIdentification, _gameId, _joinState, _scid ];
    }

    public override Tdf CreateNew() => new CreateTournamentGameResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "CreateTournamentGameResponse";
    public override string GetFullClassName() => "Blaze::GameManager::CreateTournamentGameResponse";

    public Blaze15SDK.Blaze.ExternalSessionIdentification? ExternalSessionIdentification
    {
        get => _externalSessionIdentification.Value;
        set => _externalSessionIdentification.Value = value;
    }

    public ulong GameId
    {
        get => _gameId.Value;
        set => _gameId.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.JoinState JoinState
    {
        get => _joinState.Value;
        set => _joinState.Value = value;
    }

    public string Scid
    {
        get => _scid.Value;
        set => _scid.Value = value;
    }

}
