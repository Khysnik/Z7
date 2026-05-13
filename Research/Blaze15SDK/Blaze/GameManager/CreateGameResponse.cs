using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class CreateGameResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ExternalSessionIdentification", "mExternalSessionIdentification", 0x973A6400, TdfType.Struct, 0, true), // Tag: ESID
        new TdfMemberInfo("GameId", "mGameId", 0x9E990000, TdfType.UInt64, 1, true), // Tag: GID
        new TdfMemberInfo("JoinedReservedPlayerIdentifications", "mJoinedReservedPlayerIdentifications", 0xCA5A4000, TdfType.List, 2, true), // Tag: REI
    ];
    private ITdfMember[] __members;

    private TdfStruct<Blaze15SDK.Blaze.ExternalSessionIdentification?> _externalSessionIdentification = new(__typeInfos[0]);
    private TdfUInt64 _gameId = new(__typeInfos[1]);
    private TdfList<Blaze15SDK.Blaze.UserIdentification> _joinedReservedPlayerIdentifications = new(__typeInfos[2]);

    public CreateGameResponse()
    {
        __members = [ _externalSessionIdentification, _gameId, _joinedReservedPlayerIdentifications ];
    }

    public override Tdf CreateNew() => new CreateGameResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "CreateGameResponse";
    public override string GetFullClassName() => "Blaze::GameManager::CreateGameResponse";

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

    public IList<Blaze15SDK.Blaze.UserIdentification> JoinedReservedPlayerIdentifications
    {
        get => _joinedReservedPlayerIdentifications.Value;
        set => _joinedReservedPlayerIdentifications.Value = value;
    }

}
