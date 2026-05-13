using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting.ArsonCTF_Derived;

public class PlayerReport : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("FlagsCaptured", "flagsCaptured", 0x9A387000, TdfType.UInt32, 0, true), // Tag: FCAP
        new TdfMemberInfo("AccountLocale", "accountLocale", 0xB2F8C000, TdfType.UInt32, 1, true), // Tag: LOC
        new TdfMemberInfo("LeavingReason", "leavingReason", 0xCADC8000, TdfType.UInt32, 2, true), // Tag: RMR
    ];
    private ITdfMember[] __members;

    private TdfUInt32 _flagsCaptured = new(__typeInfos[0]);
    private TdfUInt32 _accountLocale = new(__typeInfos[1]);
    private TdfUInt32 _leavingReason = new(__typeInfos[2]);

    public PlayerReport()
    {
        __members = [ _flagsCaptured, _accountLocale, _leavingReason ];
    }

    public override Tdf CreateNew() => new PlayerReport();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "PlayerReport";
    public override string GetFullClassName() => "Blaze::GameReporting::ArsonCTF_Derived::PlayerReport";

    public uint FlagsCaptured
    {
        get => _flagsCaptured.Value;
        set => _flagsCaptured.Value = value;
    }

    public uint AccountLocale
    {
        get => _accountLocale.Value;
        set => _accountLocale.Value = value;
    }

    public uint LeavingReason
    {
        get => _leavingReason.Value;
        set => _leavingReason.Value = value;
    }

}
