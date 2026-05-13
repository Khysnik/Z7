using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class GetGameListSyncResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("MaxPossibleFitScore", "mMaxPossibleFitScore", 0xB61E2600, TdfType.UInt32, 0, true), // Tag: MAXF
        new TdfMemberInfo("NumberOfGamesToBeDownloaded", "mNumberOfGamesToBeDownloaded", 0xBA790000, TdfType.UInt32, 1, true), // Tag: NGD
        new TdfMemberInfo("GameList", "mGameList", 0xD7093400, TdfType.List, 2, true), // Tag: UPDT
    ];
    private ITdfMember[] __members;

    private TdfUInt32 _maxPossibleFitScore = new(__typeInfos[0]);
    private TdfUInt32 _numberOfGamesToBeDownloaded = new(__typeInfos[1]);
    private TdfList<Blaze15SDK.Blaze.GameManager.GameBrowserMatchData> _gameList = new(__typeInfos[2]);

    public GetGameListSyncResponse()
    {
        __members = [ _maxPossibleFitScore, _numberOfGamesToBeDownloaded, _gameList ];
    }

    public override Tdf CreateNew() => new GetGameListSyncResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetGameListSyncResponse";
    public override string GetFullClassName() => "Blaze::GameManager::GetGameListSyncResponse";

    public uint MaxPossibleFitScore
    {
        get => _maxPossibleFitScore.Value;
        set => _maxPossibleFitScore.Value = value;
    }

    public uint NumberOfGamesToBeDownloaded
    {
        get => _numberOfGamesToBeDownloaded.Value;
        set => _numberOfGamesToBeDownloaded.Value = value;
    }

    public IList<Blaze15SDK.Blaze.GameManager.GameBrowserMatchData> GameList
    {
        get => _gameList.Value;
        set => _gameList.Value = value;
    }

}
