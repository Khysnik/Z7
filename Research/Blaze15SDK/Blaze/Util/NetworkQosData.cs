using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Util;

public class NetworkQosData : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("BandwidthErrorCode", "mBandwidthErrorCode", 0x8B7A3200, TdfType.UInt32, 0, true), // Tag: BWHR
        new TdfMemberInfo("DownstreamBitsPerSecond", "mDownstreamBitsPerSecond", 0x922C3300, TdfType.UInt32, 1, true), // Tag: DBPS
        new TdfMemberInfo("NatErrorCode", "mNatErrorCode", 0xBA1A3200, TdfType.UInt32, 2, true), // Tag: NAHR
        new TdfMemberInfo("NatType", "mNatType", 0xBA1D3400, TdfType.Enum, 3, true), // Tag: NATT
        new TdfMemberInfo("UpstreamBitsPerSecond", "mUpstreamBitsPerSecond", 0xD62C3300, TdfType.UInt32, 4, true), // Tag: UBPS
    ];
    private ITdfMember[] __members;

    private TdfUInt32 _bandwidthErrorCode = new(__typeInfos[0]);
    private TdfUInt32 _downstreamBitsPerSecond = new(__typeInfos[1]);
    private TdfUInt32 _natErrorCode = new(__typeInfos[2]);
    private TdfEnum<Blaze15SDK.Blaze.Util.NatType> _natType = new(__typeInfos[3]);
    private TdfUInt32 _upstreamBitsPerSecond = new(__typeInfos[4]);

    public NetworkQosData()
    {
        __members = [
            _bandwidthErrorCode,
            _downstreamBitsPerSecond,
            _natErrorCode,
            _natType,
            _upstreamBitsPerSecond,
        ];
    }

    public override Tdf CreateNew() => new NetworkQosData();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "NetworkQosData";
    public override string GetFullClassName() => "Blaze::Util::NetworkQosData";

    public uint BandwidthErrorCode
    {
        get => _bandwidthErrorCode.Value;
        set => _bandwidthErrorCode.Value = value;
    }

    public uint DownstreamBitsPerSecond
    {
        get => _downstreamBitsPerSecond.Value;
        set => _downstreamBitsPerSecond.Value = value;
    }

    public uint NatErrorCode
    {
        get => _natErrorCode.Value;
        set => _natErrorCode.Value = value;
    }

    public Blaze15SDK.Blaze.Util.NatType NatType
    {
        get => _natType.Value;
        set => _natType.Value = value;
    }

    public uint UpstreamBitsPerSecond
    {
        get => _upstreamBitsPerSecond.Value;
        set => _upstreamBitsPerSecond.Value = value;
    }

}
