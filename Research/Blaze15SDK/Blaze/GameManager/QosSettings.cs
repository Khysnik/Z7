using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class QosSettings : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("DurationMs", "mDurationMs", 0x935CA100, TdfType.UInt32, 0, true), // Tag: DURA
        new TdfMemberInfo("IntervalMs", "mIntervalMs", 0xA6ED3600, TdfType.UInt32, 1, true), // Tag: INTV
        new TdfMemberInfo("PacketSize", "mPacketSize", 0xCE9EA500, TdfType.UInt32, 2, true), // Tag: SIZE
    ];
    private ITdfMember[] __members;

    private TdfUInt32 _durationMs = new(__typeInfos[0]);
    private TdfUInt32 _intervalMs = new(__typeInfos[1]);
    private TdfUInt32 _packetSize = new(__typeInfos[2]);

    public QosSettings()
    {
        __members = [ _durationMs, _intervalMs, _packetSize ];
    }

    public override Tdf CreateNew() => new QosSettings();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "QosSettings";
    public override string GetFullClassName() => "Blaze::GameManager::QosSettings";

    public uint DurationMs
    {
        get => _durationMs.Value;
        set => _durationMs.Value = value;
    }

    public uint IntervalMs
    {
        get => _intervalMs.Value;
        set => _intervalMs.Value = value;
    }

    public uint PacketSize
    {
        get => _packetSize.Value;
        set => _packetSize.Value = value;
    }

}
