using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class TelemetryReportStats : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("DistProcTime", "mDistProcTime", 0x930CB400, TdfType.Float, 0, true), // Tag: DPRT
        new TdfMemberInfo("DistWaitTime", "mDistWaitTime", 0x937D3400, TdfType.Float, 1, true), // Tag: DWTT
        new TdfMemberInfo("Idpps", "mIdpps", 0xA64C3300, TdfType.UInt32, 2, true), // Tag: IDPS
        new TdfMemberInfo("LpackLost", "mLpackLost", 0xB30AEC00, TdfType.Float, 3, true), // Tag: LPKL
        new TdfMemberInfo("Odpps", "mOdpps", 0xBE4C3300, TdfType.UInt32, 4, true), // Tag: ODPS
        new TdfMemberInfo("RpackLost", "mRpackLost", 0xCB0AEC00, TdfType.Float, 5, true), // Tag: RPKL
    ];
    private ITdfMember[] __members;

    private TdfFloat _distProcTime = new(__typeInfos[0]);
    private TdfFloat _distWaitTime = new(__typeInfos[1]);
    private TdfUInt32 _idpps = new(__typeInfos[2]);
    private TdfFloat _lpackLost = new(__typeInfos[3]);
    private TdfUInt32 _odpps = new(__typeInfos[4]);
    private TdfFloat _rpackLost = new(__typeInfos[5]);

    public TelemetryReportStats()
    {
        __members = [ _distProcTime, _distWaitTime, _idpps, _lpackLost, _odpps, _rpackLost ];
    }

    public override Tdf CreateNew() => new TelemetryReportStats();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "TelemetryReportStats";
    public override string GetFullClassName() => "Blaze::GameManager::TelemetryReportStats";

    public float DistProcTime
    {
        get => _distProcTime.Value;
        set => _distProcTime.Value = value;
    }

    public float DistWaitTime
    {
        get => _distWaitTime.Value;
        set => _distWaitTime.Value = value;
    }

    public uint Idpps
    {
        get => _idpps.Value;
        set => _idpps.Value = value;
    }

    public float LpackLost
    {
        get => _lpackLost.Value;
        set => _lpackLost.Value = value;
    }

    public uint Odpps
    {
        get => _odpps.Value;
        set => _odpps.Value = value;
    }

    public float RpackLost
    {
        get => _rpackLost.Value;
        set => _rpackLost.Value = value;
    }

}
