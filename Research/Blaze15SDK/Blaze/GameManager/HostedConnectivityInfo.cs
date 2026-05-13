using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class HostedConnectivityInfo : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("HostingServerConnectionSetId", "mHostingServerConnectionSetId", 0x8F3A6400, TdfType.String, 0, true), // Tag: CSID
        new TdfMemberInfo("HostingServerNetworkAddress", "mHostingServerNetworkAddress", 0xA2E86400, TdfType.Union, 1, true), // Tag: HNAD
        new TdfMemberInfo("HostingServerConnectivityId", "mHostingServerConnectivityId", 0xA33A6400, TdfType.UInt32, 2, true), // Tag: HSID
        new TdfMemberInfo("LocalLowLevelConnectivityId", "mLocalLowLevelConnectivityId", 0xB23A6400, TdfType.UInt32, 3, true), // Tag: LCID
        new TdfMemberInfo("RemoteLowLevelConnectivityId", "mRemoteLowLevelConnectivityId", 0xCA3A6400, TdfType.UInt32, 4, true), // Tag: RCID
    ];
    private ITdfMember[] __members;

    private TdfString _hostingServerConnectionSetId = new(__typeInfos[0]);
    private TdfUnion<Blaze15SDK.Blaze.NetworkAddress> _hostingServerNetworkAddress = new(__typeInfos[1]);
    private TdfUInt32 _hostingServerConnectivityId = new(__typeInfos[2]);
    private TdfUInt32 _localLowLevelConnectivityId = new(__typeInfos[3]);
    private TdfUInt32 _remoteLowLevelConnectivityId = new(__typeInfos[4]);

    public HostedConnectivityInfo()
    {
        __members = [ _hostingServerConnectionSetId, _hostingServerNetworkAddress, _hostingServerConnectivityId, _localLowLevelConnectivityId, _remoteLowLevelConnectivityId ];
    }

    public override Tdf CreateNew() => new HostedConnectivityInfo();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "HostedConnectivityInfo";
    public override string GetFullClassName() => "Blaze::GameManager::HostedConnectivityInfo";

    public string HostingServerConnectionSetId
    {
        get => _hostingServerConnectionSetId.Value;
        set => _hostingServerConnectionSetId.Value = value;
    }

    public Blaze15SDK.Blaze.NetworkAddress HostingServerNetworkAddress
    {
        get => _hostingServerNetworkAddress.Value;
        set => _hostingServerNetworkAddress.Value = value;
    }

    public uint HostingServerConnectivityId
    {
        get => _hostingServerConnectivityId.Value;
        set => _hostingServerConnectivityId.Value = value;
    }

    public uint LocalLowLevelConnectivityId
    {
        get => _localLowLevelConnectivityId.Value;
        set => _localLowLevelConnectivityId.Value = value;
    }

    public uint RemoteLowLevelConnectivityId
    {
        get => _remoteLowLevelConnectivityId.Value;
        set => _remoteLowLevelConnectivityId.Value = value;
    }

}
