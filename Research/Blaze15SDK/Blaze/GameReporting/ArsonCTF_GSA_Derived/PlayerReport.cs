using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting.ArsonCTF_GSA_Derived;

public class PlayerReport : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("FlagsCaptured", "flagsCaptured", 0x9A387000, TdfType.UInt32, 0, true), // Tag: FCAP
    ];
    private ITdfMember[] __members;

    private TdfUInt32 _flagsCaptured = new(__typeInfos[0]);

    public PlayerReport()
    {
        __members = [ _flagsCaptured ];
    }

    public override Tdf CreateNew() => new PlayerReport();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "PlayerReport";
    public override string GetFullClassName() => "Blaze::GameReporting::ArsonCTF_GSA_Derived::PlayerReport";

    public uint FlagsCaptured
    {
        get => _flagsCaptured.Value;
        set => _flagsCaptured.Value = value;
    }

}
