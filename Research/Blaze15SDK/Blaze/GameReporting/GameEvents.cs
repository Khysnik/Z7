using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting;

public class GameEvents : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("GameEventList", "gameEvents", 0x9ED97300, TdfType.List, 0, true), // Tag: GMES
        new TdfMemberInfo("GameEventProcessorName", "gameEventProcessorName", 0xC32BE300, TdfType.String, 1, true), // Tag: PROC
    ];
    private ITdfMember[] __members;

    private TdfList<GameEvent> _gameEventList = new(__typeInfos[0]);
    private TdfString _gameEventProcessorName = new(__typeInfos[1]);

    public GameEvents()
    {
        __members = [ _gameEventList, _gameEventProcessorName ];
    }

    public override Tdf CreateNew() => new GameEvents();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GameEvents";
    public override string GetFullClassName() => "Blaze::GameReporting::GameEvents";

    public IList<GameEvent> GameEventList
    {
        get => _gameEventList.Value;
        set => _gameEventList.Value = value;
    }

    public string GameEventProcessorName
    {
        get => _gameEventProcessorName.Value;
        set => _gameEventProcessorName.Value = value;
    }

}
