using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class GameBrowserScenarioData : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("GetGameListRequest", "mGetGameListRequest", 0x9ECCB100, TdfType.Struct, 0, true), // Tag: GLRQ
        new TdfMemberInfo("IsSnapshotList", "mIsSnapshotList", 0xCEE87000, TdfType.Bool, 1, true), // Tag: SNAP
    ];
    private ITdfMember[] __members;

    private TdfStruct<Blaze15SDK.Blaze.GameManager.GetGameListRequest?> _getGameListRequest = new(__typeInfos[0]);
    private TdfBool _isSnapshotList = new(__typeInfos[1]);

    public GameBrowserScenarioData()
    {
        __members = [ _getGameListRequest, _isSnapshotList ];
    }

    public override Tdf CreateNew() => new GameBrowserScenarioData();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GameBrowserScenarioData";
    public override string GetFullClassName() => "Blaze::GameManager::GameBrowserScenarioData";

    public Blaze15SDK.Blaze.GameManager.GetGameListRequest? GetGameListRequest
    {
        get => _getGameListRequest.Value;
        set => _getGameListRequest.Value = value;
    }

    public bool IsSnapshotList
    {
        get => _isSnapshotList.Value;
        set => _isSnapshotList.Value = value;
    }

}
