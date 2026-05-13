using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting.GameHistoryBasic;

public class GameAttributes : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("MapId", "mapId", 0xB61C0000, TdfType.Int32, 0, true), // Tag: MAP
        new TdfMemberInfo("Mode", "mode", 0xB6F92500, TdfType.String, 1, true), // Tag: MODE
    ];
    private ITdfMember[] __members;

    private TdfInt32 _mapId = new(__typeInfos[0]);
    private TdfString _mode = new(__typeInfos[1]);

    public GameAttributes()
    {
        __members = [ _mapId, _mode ];
    }

    public override Tdf CreateNew() => new GameAttributes();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GameAttributes";
    public override string GetFullClassName() => "Blaze::GameReporting::GameHistoryBasic::GameAttributes";

    public int MapId
    {
        get => _mapId.Value;
        set => _mapId.Value = value;
    }

    public string Mode
    {
        get => _mode.Value;
        set => _mode.Value = value;
    }

}
