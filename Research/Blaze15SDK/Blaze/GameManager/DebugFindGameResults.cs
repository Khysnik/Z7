using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class DebugFindGameResults : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("TopResultList", "mTopResultList", 0x934C8000, TdfType.List, 0, true), // Tag: DTR
        new TdfMemberInfo("FoundGameFitScore", "mFoundGameFitScore", 0x9A6A7400, TdfType.UInt32, 1, true), // Tag: FFIT
        new TdfMemberInfo("FoundGame", "mFoundGame", 0x9A786D00, TdfType.Struct, 2, true), // Tag: FGAM
        new TdfMemberInfo("FoundGameTimeToMatch", "mFoundGameTimeToMatch", 0x9B4D2D00, TdfType.Int64, 3, true), // Tag: FTTM
        new TdfMemberInfo("MaxFitScore", "mMaxFitScore", 0xB66A7400, TdfType.UInt32, 4, true), // Tag: MFIT
        new TdfMemberInfo("JoinedPlayerCount", "mJoinedPlayerCount", 0xCE9EA500, TdfType.UInt16, 5, true), // Tag: SIZE
    ];
    private ITdfMember[] __members;

    private TdfList<Blaze15SDK.Blaze.GameManager.DebugTopResult> _topResultList = new(__typeInfos[0]);
    private TdfUInt32 _foundGameFitScore = new(__typeInfos[1]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.ListGameData?> _foundGame = new(__typeInfos[2]);
    private TdfInt64 _foundGameTimeToMatch = new(__typeInfos[3]);
    private TdfUInt32 _maxFitScore = new(__typeInfos[4]);
    private TdfUInt16 _joinedPlayerCount = new(__typeInfos[5]);

    public DebugFindGameResults()
    {
        __members = [ _topResultList, _foundGameFitScore, _foundGame, _foundGameTimeToMatch, _maxFitScore, _joinedPlayerCount ];
    }

    public override Tdf CreateNew() => new DebugFindGameResults();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "DebugFindGameResults";
    public override string GetFullClassName() => "Blaze::GameManager::DebugFindGameResults";

    public IList<Blaze15SDK.Blaze.GameManager.DebugTopResult> TopResultList
    {
        get => _topResultList.Value;
        set => _topResultList.Value = value;
    }

    public uint FoundGameFitScore
    {
        get => _foundGameFitScore.Value;
        set => _foundGameFitScore.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.ListGameData? FoundGame
    {
        get => _foundGame.Value;
        set => _foundGame.Value = value;
    }

    public long FoundGameTimeToMatch
    {
        get => _foundGameTimeToMatch.Value;
        set => _foundGameTimeToMatch.Value = value;
    }

    public uint MaxFitScore
    {
        get => _maxFitScore.Value;
        set => _maxFitScore.Value = value;
    }

    public ushort JoinedPlayerCount
    {
        get => _joinedPlayerCount.Value;
        set => _joinedPlayerCount.Value = value;
    }

}
