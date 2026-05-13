using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class StartMatchmakingRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("CommonGameData", "mCommonGameData", 0x8ED9E400, TdfType.Struct, 0, true), // Tag: CMGD
        new TdfMemberInfo("CriteriaData", "mCriteriaData", 0x8F290000, TdfType.Struct, 1, true), // Tag: CRD
        new TdfMemberInfo("GameCreationData", "mGameCreationData", 0x9ED8E400, TdfType.Struct, 2, true), // Tag: GMCD
        new TdfMemberInfo("PlayerJoinData", "mPlayerJoinData", 0xC2CAA400, TdfType.Struct, 3, true), // Tag: PLJD
        new TdfMemberInfo("SessionData", "mSessionData", 0xCF393400, TdfType.Struct, 4, true), // Tag: SSDT
    ];
    private ITdfMember[] __members;

    private TdfStruct<Blaze15SDK.Blaze.GameManager.CommonGameRequestData?> _commonGameData = new(__typeInfos[0]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.MatchmakingCriteriaData?> _criteriaData = new(__typeInfos[1]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.GameCreationData?> _gameCreationData = new(__typeInfos[2]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.PlayerJoinData?> _playerJoinData = new(__typeInfos[3]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.MatchmakingSessionData?> _sessionData = new(__typeInfos[4]);

    public StartMatchmakingRequest()
    {
        __members = [ _commonGameData, _criteriaData, _gameCreationData, _playerJoinData, _sessionData ];
    }

    public override Tdf CreateNew() => new StartMatchmakingRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "StartMatchmakingRequest";
    public override string GetFullClassName() => "Blaze::GameManager::StartMatchmakingRequest";

    public Blaze15SDK.Blaze.GameManager.CommonGameRequestData? CommonGameData
    {
        get => _commonGameData.Value;
        set => _commonGameData.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.MatchmakingCriteriaData? CriteriaData
    {
        get => _criteriaData.Value;
        set => _criteriaData.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.GameCreationData? GameCreationData
    {
        get => _gameCreationData.Value;
        set => _gameCreationData.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.PlayerJoinData? PlayerJoinData
    {
        get => _playerJoinData.Value;
        set => _playerJoinData.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.MatchmakingSessionData? SessionData
    {
        get => _sessionData.Value;
        set => _sessionData.Value = value;
    }

}
