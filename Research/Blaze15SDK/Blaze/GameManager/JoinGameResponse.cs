using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class JoinGameResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ExternalSessionIdentification", "mExternalSessionIdentification", 0x973A6400, TdfType.Struct, 0, true), // Tag: ESID
        new TdfMemberInfo("GameId", "mGameId", 0x9E990000, TdfType.UInt64, 1, true), // Tag: GID
        new TdfMemberInfo("JoinState", "mJoinState", 0xAA7CC000, TdfType.Enum, 2, true), // Tag: JGS
        new TdfMemberInfo("IsExternalCaller", "mIsExternalCaller", 0xBE386C00, TdfType.Bool, 3, true), // Tag: OCAL
        new TdfMemberInfo("JoinedReservedPlayerIdentifications", "mJoinedReservedPlayerIdentifications", 0xCA5A4000, TdfType.List, 4, true), // Tag: REI
    ];
    private ITdfMember[] __members;

    private TdfStruct<Blaze15SDK.Blaze.ExternalSessionIdentification?> _externalSessionIdentification = new(__typeInfos[0]);
    private TdfUInt64 _gameId = new(__typeInfos[1]);
    private TdfEnum<Blaze15SDK.Blaze.GameManager.JoinState> _joinState = new(__typeInfos[2]);
    private TdfBool _isExternalCaller = new(__typeInfos[3]);
    private TdfList<Blaze15SDK.Blaze.UserIdentification> _joinedReservedPlayerIdentifications = new(__typeInfos[4]);

    public JoinGameResponse()
    {
        __members = [ _externalSessionIdentification, _gameId, _joinState, _isExternalCaller, _joinedReservedPlayerIdentifications ];
    }

    public override Tdf CreateNew() => new JoinGameResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "JoinGameResponse";
    public override string GetFullClassName() => "Blaze::GameManager::JoinGameResponse";

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

    public bool IsExternalCaller
    {
        get => _isExternalCaller.Value;
        set => _isExternalCaller.Value = value;
    }

    public IList<Blaze15SDK.Blaze.UserIdentification> JoinedReservedPlayerIdentifications
    {
        get => _joinedReservedPlayerIdentifications.Value;
        set => _joinedReservedPlayerIdentifications.Value = value;
    }

}
