using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class QosConfigInfo : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ClientQosFailureRefreshRate", "mClientQosFailureRefreshRate", 0x8F19B200, TdfType.TimeValue, 0, true), // Tag: CQFR
        new TdfMemberInfo("ClientQosRefreshRate", "mClientQosRefreshRate", 0x8F1CB200, TdfType.TimeValue, 1, true), // Tag: CQRR
        new TdfMemberInfo("PingSiteInfoByAliasMap", "mPingSiteInfoByAliasMap", 0xB34C3300, TdfType.Map, 2, true), // Tag: LTPS
        new TdfMemberInfo("QosCoordinatorInfo", "mQosCoordinatorInfo", 0xC63BA600, TdfType.Struct, 3, true), // Tag: QCNF
        new TdfMemberInfo("ServerQosRefreshRate", "mServerQosRefreshRate", 0xCF1CB200, TdfType.TimeValue, 4, true), // Tag: SQRR
    ];
    private ITdfMember[] __members;

    private TdfTimeValue _clientQosFailureRefreshRate = new(__typeInfos[0]);
    private TdfTimeValue _clientQosRefreshRate = new(__typeInfos[1]);
    private TdfMap<string, Blaze15SDK.Blaze.QosPingSiteInfo> _pingSiteInfoByAliasMap = new(__typeInfos[2]);
    private TdfStruct<Blaze15SDK.Blaze.QosCoordinatorInfo?> _qosCoordinatorInfo = new(__typeInfos[3]);
    private TdfTimeValue _serverQosRefreshRate = new(__typeInfos[4]);

    public QosConfigInfo()
    {
        __members = [
            _clientQosFailureRefreshRate,
            _clientQosRefreshRate,
            _pingSiteInfoByAliasMap,
            _qosCoordinatorInfo,
            _serverQosRefreshRate,
        ];
    }

    public override Tdf CreateNew() => new QosConfigInfo();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "QosConfigInfo";
    public override string GetFullClassName() => "Blaze::QosConfigInfo";

    public TimeValue ClientQosFailureRefreshRate
    {
        get => _clientQosFailureRefreshRate.Value;
        set => _clientQosFailureRefreshRate.Value = value;
    }

    public TimeValue ClientQosRefreshRate
    {
        get => _clientQosRefreshRate.Value;
        set => _clientQosRefreshRate.Value = value;
    }

    public IDictionary<string, Blaze15SDK.Blaze.QosPingSiteInfo> PingSiteInfoByAliasMap
    {
        get => _pingSiteInfoByAliasMap.Value;
        set => _pingSiteInfoByAliasMap.Value = value;
    }

    public Blaze15SDK.Blaze.QosCoordinatorInfo? QosCoordinatorInfo
    {
        get => _qosCoordinatorInfo.Value;
        set => _qosCoordinatorInfo.Value = value;
    }

    public TimeValue ServerQosRefreshRate
    {
        get => _serverQosRefreshRate.Value;
        set => _serverQosRefreshRate.Value = value;
    }

}
