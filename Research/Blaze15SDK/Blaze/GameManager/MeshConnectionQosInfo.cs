using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class MeshConnectionQosInfo : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("PacketLoss", "mPacketLoss", 0xB2FCF300, TdfType.Float, 0, true), // Tag: LOSS
        new TdfMemberInfo("LatencyMs", "mLatencyMs", 0xC29BA700, TdfType.UInt32, 1, true), // Tag: PING
    ];
    private ITdfMember[] __members;

    private TdfFloat _packetLoss = new(__typeInfos[0]);
    private TdfUInt32 _latencyMs = new(__typeInfos[1]);

    public MeshConnectionQosInfo()
    {
        __members = [ _packetLoss, _latencyMs ];
    }

    public override Tdf CreateNew() => new MeshConnectionQosInfo();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "MeshConnectionQosInfo";
    public override string GetFullClassName() => "Blaze::GameManager::MeshConnectionQosInfo";

    public float PacketLoss
    {
        get => _packetLoss.Value;
        set => _packetLoss.Value = value;
    }

    public uint LatencyMs
    {
        get => _latencyMs.Value;
        set => _latencyMs.Value = value;
    }

}
