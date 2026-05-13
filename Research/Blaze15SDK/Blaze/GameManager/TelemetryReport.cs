using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class TelemetryReport : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ConnTimeAfterDemangle", "mConnTimeAfterDemangle", 0x8F486400, TdfType.UInt32, 0, true), // Tag: CTAD
        new TdfMemberInfo("ConnTimeBeforeDemangle", "mConnTimeBeforeDemangle", 0x8F48A400, TdfType.UInt32, 1, true), // Tag: CTBD
        new TdfMemberInfo("ConnTimeDemangling", "mConnTimeDemangling", 0x8F492700, TdfType.UInt32, 2, true), // Tag: CTDG
        new TdfMemberInfo("LatencyMs", "mLatencyMs", 0xB21D2300, TdfType.UInt32, 3, true), // Tag: LATC
        new TdfMemberInfo("PacketLossFloat", "mPacketLossFloat", 0xC2BB2600, TdfType.Float, 4, true), // Tag: PKLF
        new TdfMemberInfo("PacketLossPercent", "mPacketLossPercent", 0xC2BD2C00, TdfType.UInt8, 5, true), // Tag: PKTL
        new TdfMemberInfo("RemoteConnGroupId", "mRemoteConnGroupId", 0xCA3A6400, TdfType.UInt64, 6, true), // Tag: RCID
        new TdfMemberInfo("LocalPacketsReceived", "mLocalPacketsReceived", 0xCA3DA400, TdfType.UInt32, 7, true), // Tag: RCVD
        new TdfMemberInfo("ReportStats", "mReportStats", 0xCB0A2200, TdfType.Struct, 8, true), // Tag: RPHB
        new TdfMemberInfo("RemotePacketsSent", "mRemotePacketsSent", 0xCE5BB400, TdfType.UInt32, 9, true), // Tag: SENT
    ];
    private ITdfMember[] __members;

    private TdfUInt32 _connTimeAfterDemangle = new(__typeInfos[0]);
    private TdfUInt32 _connTimeBeforeDemangle = new(__typeInfos[1]);
    private TdfUInt32 _connTimeDemangling = new(__typeInfos[2]);
    private TdfUInt32 _latencyMs = new(__typeInfos[3]);
    private TdfFloat _packetLossFloat = new(__typeInfos[4]);
    private TdfUInt8 _packetLossPercent = new(__typeInfos[5]);
    private TdfUInt64 _remoteConnGroupId = new(__typeInfos[6]);
    private TdfUInt32 _localPacketsReceived = new(__typeInfos[7]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.TelemetryReportStats?> _reportStats = new(__typeInfos[8]);
    private TdfUInt32 _remotePacketsSent = new(__typeInfos[9]);

    public TelemetryReport()
    {
        __members = [ _connTimeAfterDemangle, _connTimeBeforeDemangle, _connTimeDemangling, _latencyMs, _packetLossFloat, _packetLossPercent, _remoteConnGroupId, _localPacketsReceived, _reportStats, _remotePacketsSent ];
    }

    public override Tdf CreateNew() => new TelemetryReport();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "TelemetryReport";
    public override string GetFullClassName() => "Blaze::GameManager::TelemetryReport";

    public uint ConnTimeAfterDemangle
    {
        get => _connTimeAfterDemangle.Value;
        set => _connTimeAfterDemangle.Value = value;
    }

    public uint ConnTimeBeforeDemangle
    {
        get => _connTimeBeforeDemangle.Value;
        set => _connTimeBeforeDemangle.Value = value;
    }

    public uint ConnTimeDemangling
    {
        get => _connTimeDemangling.Value;
        set => _connTimeDemangling.Value = value;
    }

    public uint LatencyMs
    {
        get => _latencyMs.Value;
        set => _latencyMs.Value = value;
    }

    public float PacketLossFloat
    {
        get => _packetLossFloat.Value;
        set => _packetLossFloat.Value = value;
    }

    public byte PacketLossPercent
    {
        get => _packetLossPercent.Value;
        set => _packetLossPercent.Value = value;
    }

    public ulong RemoteConnGroupId
    {
        get => _remoteConnGroupId.Value;
        set => _remoteConnGroupId.Value = value;
    }

    public uint LocalPacketsReceived
    {
        get => _localPacketsReceived.Value;
        set => _localPacketsReceived.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.TelemetryReportStats? ReportStats
    {
        get => _reportStats.Value;
        set => _reportStats.Value = value;
    }

    public uint RemotePacketsSent
    {
        get => _remotePacketsSent.Value;
        set => _remotePacketsSent.Value = value;
    }

}
