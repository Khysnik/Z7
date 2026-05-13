using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class GetGameListScenarioRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("CommonGameData", "mCommonGameData", 0x8ED9E400, TdfType.Struct, 0, true), // Tag: CMGD
        new TdfMemberInfo("GameBrowserScenarioAttributes", "mGameBrowserScenarioAttributes", 0x9E2CE100, TdfType.Map, 1, true), // Tag: GBSA
        new TdfMemberInfo("GameBrowserScenarioName", "mGameBrowserScenarioName", 0x9EECEE00, TdfType.String, 2, true), // Tag: GNSN
        new TdfMemberInfo("PlayerJoinData", "mPlayerJoinData", 0xC2CAA400, TdfType.Struct, 3, true), // Tag: PLJD
    ];
    private ITdfMember[] __members;

    private TdfStruct<Blaze15SDK.Blaze.GameManager.CommonGameRequestData?> _commonGameData = new(__typeInfos[0]);
    private TdfMap<string, object> _gameBrowserScenarioAttributes = new(__typeInfos[1]);
    private TdfString _gameBrowserScenarioName = new(__typeInfos[2]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.PlayerJoinData?> _playerJoinData = new(__typeInfos[3]);

    public GetGameListScenarioRequest()
    {
        __members = [ _commonGameData, _gameBrowserScenarioAttributes, _gameBrowserScenarioName, _playerJoinData ];
    }

    public override Tdf CreateNew() => new GetGameListScenarioRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetGameListScenarioRequest";
    public override string GetFullClassName() => "Blaze::GameManager::GetGameListScenarioRequest";

    public Blaze15SDK.Blaze.GameManager.CommonGameRequestData? CommonGameData
    {
        get => _commonGameData.Value;
        set => _commonGameData.Value = value;
    }

    public IDictionary<string, object> GameBrowserScenarioAttributes
    {
        get => _gameBrowserScenarioAttributes.Value;
        set => _gameBrowserScenarioAttributes.Value = value;
    }

    public string GameBrowserScenarioName
    {
        get => _gameBrowserScenarioName.Value;
        set => _gameBrowserScenarioName.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.PlayerJoinData? PlayerJoinData
    {
        get => _playerJoinData.Value;
        set => _playerJoinData.Value = value;
    }

}
