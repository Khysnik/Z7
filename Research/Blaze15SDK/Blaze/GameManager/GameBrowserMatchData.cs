using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class GameBrowserMatchData : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("FitScore", "mFitScore", 0x9A9D0000, TdfType.UInt32, 0, true), // Tag: FIT
        new TdfMemberInfo("GameData", "mGameData", 0x9E1B4000, TdfType.Struct, 1, true), // Tag: GAM
    ];
    private ITdfMember[] __members;

    private TdfUInt32 _fitScore = new(__typeInfos[0]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.GameBrowserGameData?> _gameData = new(__typeInfos[1]);

    public GameBrowserMatchData()
    {
        __members = [ _fitScore, _gameData ];
    }

    public override Tdf CreateNew() => new GameBrowserMatchData();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GameBrowserMatchData";
    public override string GetFullClassName() => "Blaze::GameManager::GameBrowserMatchData";

    public uint FitScore
    {
        get => _fitScore.Value;
        set => _fitScore.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.GameBrowserGameData? GameData
    {
        get => _gameData.Value;
        set => _gameData.Value = value;
    }

}
