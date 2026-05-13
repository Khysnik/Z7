using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class ConnectionValidationResults : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("AvoidPlayerIdList", "mAvoidPlayerIdList", 0x86CCF400, TdfType.List, 0, true), // Tag: ALST
        new TdfMemberInfo("FailCount", "mFailCount", 0x9A3BB400, TdfType.UInt16, 1, true), // Tag: FCNT
        new TdfMemberInfo("AvoidGameIdList", "mAvoidGameIdList", 0x9E992C00, TdfType.List, 2, true), // Tag: GIDL
        new TdfMemberInfo("NetworkTopology", "mNetworkTopology", 0xBB4BF000, TdfType.Enum, 3, true), // Tag: NTOP
        new TdfMemberInfo("Tier", "mTier", 0xD2997200, TdfType.UInt16, 4, true), // Tag: TIER
    ];
    private ITdfMember[] __members;

    private TdfList<long> _avoidPlayerIdList = new(__typeInfos[0]);
    private TdfUInt16 _failCount = new(__typeInfos[1]);
    private TdfList<ulong> _avoidGameIdList = new(__typeInfos[2]);
    private TdfEnum<Blaze15SDK.Blaze.GameNetworkTopology> _networkTopology = new(__typeInfos[3]);
    private TdfUInt16 _tier = new(__typeInfos[4]);

    public ConnectionValidationResults()
    {
        __members = [ _avoidPlayerIdList, _failCount, _avoidGameIdList, _networkTopology, _tier ];
    }

    public override Tdf CreateNew() => new ConnectionValidationResults();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ConnectionValidationResults";
    public override string GetFullClassName() => "Blaze::GameManager::ConnectionValidationResults";

    public IList<long> AvoidPlayerIdList
    {
        get => _avoidPlayerIdList.Value;
        set => _avoidPlayerIdList.Value = value;
    }

    public ushort FailCount
    {
        get => _failCount.Value;
        set => _failCount.Value = value;
    }

    public IList<ulong> AvoidGameIdList
    {
        get => _avoidGameIdList.Value;
        set => _avoidGameIdList.Value = value;
    }

    public Blaze15SDK.Blaze.GameNetworkTopology NetworkTopology
    {
        get => _networkTopology.Value;
        set => _networkTopology.Value = value;
    }

    public ushort Tier
    {
        get => _tier.Value;
        set => _tier.Value = value;
    }

}
