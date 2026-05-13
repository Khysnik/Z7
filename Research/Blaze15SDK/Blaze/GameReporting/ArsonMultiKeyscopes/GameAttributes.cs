using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting.ArsonMultiKeyscopes;

public class GameAttributes : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Level", "level", 0xB36B0000, TdfType.UInt16, 0, true), // Tag: LVL
    ];
    private ITdfMember[] __members;

    private TdfUInt16 _level = new(__typeInfos[0]);

    public GameAttributes()
    {
        __members = [ _level ];
    }

    public override Tdf CreateNew() => new GameAttributes();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GameAttributes";
    public override string GetFullClassName() => "Blaze::GameReporting::ArsonMultiKeyscopes::GameAttributes";

    public ushort Level
    {
        get => _level.Value;
        set => _level.Value = value;
    }

}
