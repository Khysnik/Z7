using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class GetGameListRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("CommonGameData", "mCommonGameData", 0x8ED9E400, TdfType.Struct, 0, true), // Tag: CMGD
        new TdfMemberInfo("ListConfigName", "mListConfigName", 0x92E86D00, TdfType.String, 1, true), // Tag: DNAM
        new TdfMemberInfo("IgnoreGameEntryCriteria", "mIgnoreGameEntryCriteria", 0xA67BAF00, TdfType.Bool, 2, true), // Tag: IGNO
        new TdfMemberInfo("ListCapacity", "mListCapacity", 0xB2387000, TdfType.UInt32, 3, true), // Tag: LCAP
        new TdfMemberInfo("ListCriteria", "mListCriteria", 0xB6DB2300, TdfType.Struct, 4, true), // Tag: MMLC
        new TdfMemberInfo("PlayerJoinData", "mPlayerJoinData", 0xC2CAA400, TdfType.Struct, 5, true), // Tag: PLJD
    ];
    private ITdfMember[] __members;

    private TdfStruct<Blaze15SDK.Blaze.GameManager.CommonGameRequestData?> _commonGameData = new(__typeInfos[0]);
    private TdfString _listConfigName = new(__typeInfos[1]);
    private TdfBool _ignoreGameEntryCriteria = new(__typeInfos[2]);
    private TdfUInt32 _listCapacity = new(__typeInfos[3]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.MatchmakingCriteriaData?> _listCriteria = new(__typeInfos[4]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.PlayerJoinData?> _playerJoinData = new(__typeInfos[5]);

    public GetGameListRequest()
    {
        __members = [ _commonGameData, _listConfigName, _ignoreGameEntryCriteria, _listCapacity, _listCriteria, _playerJoinData ];
    }

    public override Tdf CreateNew() => new GetGameListRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetGameListRequest";
    public override string GetFullClassName() => "Blaze::GameManager::GetGameListRequest";

    public Blaze15SDK.Blaze.GameManager.CommonGameRequestData? CommonGameData
    {
        get => _commonGameData.Value;
        set => _commonGameData.Value = value;
    }

    public string ListConfigName
    {
        get => _listConfigName.Value;
        set => _listConfigName.Value = value;
    }

    public bool IgnoreGameEntryCriteria
    {
        get => _ignoreGameEntryCriteria.Value;
        set => _ignoreGameEntryCriteria.Value = value;
    }

    public uint ListCapacity
    {
        get => _listCapacity.Value;
        set => _listCapacity.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.MatchmakingCriteriaData? ListCriteria
    {
        get => _listCriteria.Value;
        set => _listCriteria.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.PlayerJoinData? PlayerJoinData
    {
        get => _playerJoinData.Value;
        set => _playerJoinData.Value = value;
    }

}
