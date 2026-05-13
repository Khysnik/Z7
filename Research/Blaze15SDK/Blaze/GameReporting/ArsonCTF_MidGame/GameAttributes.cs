using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting.ArsonCTF_MidGame;

public class GameAttributes : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("GameName", "gameName", 0x9EE86D00, TdfType.String, 0, true), // Tag: GNAM
        new TdfMemberInfo("MapName", "mapName", 0xB61C0000, TdfType.String, 1, true), // Tag: MAP
    ];
    private ITdfMember[] __members;

    private TdfString _gameName = new(__typeInfos[0]);
    private TdfString _mapName = new(__typeInfos[1]);

    public GameAttributes()
    {
        __members = [ _gameName, _mapName ];
    }

    public override Tdf CreateNew() => new GameAttributes();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GameAttributes";
    public override string GetFullClassName() => "Blaze::GameReporting::ArsonCTF_MidGame::GameAttributes";

    public string GameName
    {
        get => _gameName.Value;
        set => _gameName.Value = value;
    }

    public string MapName
    {
        get => _mapName.Value;
        set => _mapName.Value = value;
    }

}
