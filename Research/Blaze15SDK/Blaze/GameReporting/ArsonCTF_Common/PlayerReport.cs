using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting.ArsonCTF_Common;

public class PlayerReport : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("CustomReport", "customReport", 0x925CB600, TdfType.Variable, 0, true), // Tag: DERV
        new TdfMemberInfo("Deaths", "deaths", 0x925D2800, TdfType.UInt16, 1, true), // Tag: DETH
        new TdfMemberInfo("Kills", "kills", 0xAE9B2C00, TdfType.UInt16, 2, true), // Tag: KILL
        new TdfMemberInfo("AccountLocale", "accountLocale", 0xB2F8C000, TdfType.UInt32, 3, true), // Tag: LOC
        new TdfMemberInfo("LeavingReason", "leavingReason", 0xCADC8000, TdfType.UInt32, 4, true), // Tag: RMR
    ];
    private ITdfMember[] __members;

    private TdfUInt16 _deaths = new(__typeInfos[1]);
    private TdfVariable _customReport = new(__typeInfos[0]);
    private TdfUInt16 _kills = new(__typeInfos[2]);
    private TdfUInt32 _accountLocale = new(__typeInfos[3]);
    private TdfUInt32 _leavingReason = new(__typeInfos[4]);

    public PlayerReport()
    {
        __members = [ _customReport, _deaths, _kills, _accountLocale, _leavingReason ];
    }

    public override Tdf CreateNew() => new PlayerReport();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "PlayerReport";
    public override string GetFullClassName() => "Blaze::GameReporting::ArsonCTF_Common::PlayerReport";

    public ushort Deaths
    {
        get => _deaths.Value;
        set => _deaths.Value = value;
    }

    public object? CustomReport
    {
        get => _customReport.Value;
        set => _customReport.Value = value;
    }

    public ushort Kills
    {
        get => _kills.Value;
        set => _kills.Value = value;
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
