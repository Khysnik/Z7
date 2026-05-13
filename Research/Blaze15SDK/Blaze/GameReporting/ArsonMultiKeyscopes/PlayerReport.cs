using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting.ArsonMultiKeyscopes;

public class PlayerReport : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Weapons", "weapons", 0xDE587000, TdfType.Map, 0, true), // Tag: WEAP
    ];
    private ITdfMember[] __members;

    private TdfMap<uint, Weapon> _weapons = new(__typeInfos[0]);

    public PlayerReport()
    {
        __members = [ _weapons ];
    }

    public override Tdf CreateNew() => new PlayerReport();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "PlayerReport";
    public override string GetFullClassName() => "Blaze::GameReporting::ArsonMultiKeyscopes::PlayerReport";

    public IDictionary<uint, Weapon> Weapons
    {
        get => _weapons.Value;
        set => _weapons.Value = value;
    }

}
