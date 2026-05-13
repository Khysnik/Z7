using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting.ArsonMultiKeyscopes;

public class Weapon : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Hits", "hits", 0xA29D3300, TdfType.UInt16, 0, true), // Tag: HITS
        new TdfMemberInfo("ShotsFired", "shotsFired", 0xCE8BF400, TdfType.UInt16, 1, true), // Tag: SHOT
    ];
    private ITdfMember[] __members;

    private TdfUInt16 _hits = new(__typeInfos[0]);
    private TdfUInt16 _shotsFired = new(__typeInfos[1]);

    public Weapon()
    {
        __members = [ _hits, _shotsFired ];
    }

    public override Tdf CreateNew() => new Weapon();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "Weapon";
    public override string GetFullClassName() => "Blaze::GameReporting::ArsonMultiKeyscopes::Weapon";

    public ushort Hits
    {
        get => _hits.Value;
        set => _hits.Value = value;
    }

    public ushort ShotsFired
    {
        get => _shotsFired.Value;
        set => _shotsFired.Value = value;
    }

}
