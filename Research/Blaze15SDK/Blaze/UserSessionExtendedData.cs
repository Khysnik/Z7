using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class UserSessionExtendedData : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Address", "mAddress", 0x86493200, TdfType.Union, 0, true), // Tag: ADDR
        new TdfMemberInfo("BestPingSiteAlias", "mBestPingSiteAlias", 0x8B0CC000, TdfType.String, 1, true), // Tag: BPS
        new TdfMemberInfo("Country", "mCountry", 0x8F4E4000, TdfType.String, 2, true), // Tag: CTY
        new TdfMemberInfo("ClientData", "mClientData", 0x8F687200, TdfType.Variable, 3, true), // Tag: CVAR
        new TdfMemberInfo("DataMap", "mDataMap", 0x92D87000, TdfType.Map, 4, true), // Tag: DMAP
        new TdfMemberInfo("HardwareFlags", "mHardwareFlags", 0xA379A700, TdfType.Enum, 5, true), // Tag: HWFG
        new TdfMemberInfo("ISP", "mISP", 0xA73C0000, TdfType.String, 6, true), // Tag: ISP
        new TdfMemberInfo("LatencyList", "mLatencyList", 0xC33B2D00, TdfType.List, 7, true), // Tag: PSLM
        new TdfMemberInfo("LatencyMap", "mLatencyMap", 0xC33B4000, TdfType.Map, 8, true), // Tag: PSM
        new TdfMemberInfo("QosData", "mQosData", 0xC6487400, TdfType.Struct, 9, true), // Tag: QDAT
        new TdfMemberInfo("TimeZone", "mTimeZone", 0xD3A00000, TdfType.String, 10, true), // Tag: TZ
        new TdfMemberInfo("UserInfoAttribute", "mUserInfoAttribute", 0xD61D3400, TdfType.UInt64, 11, true), // Tag: UATT
        new TdfMemberInfo("BlazeObjectIdList", "mBlazeObjectIdList", 0xD6CCF400, TdfType.List, 12, true), // Tag: ULST
        new TdfMemberInfo("CrossPlatformOptIn", "mCrossPlatformOptIn", 0xE30B3400, TdfType.Bool, 13, true), // Tag: XPLT
    ];
    private ITdfMember[] __members;

    private TdfUnion<Blaze15SDK.Blaze.NetworkAddress> _address = new(__typeInfos[0]);
    private TdfString _bestPingSiteAlias = new(__typeInfos[1]);
    private TdfString _country = new(__typeInfos[2]);
    private TdfVariable _clientData = new(__typeInfos[3]);
    private TdfMap<uint, long> _dataMap = new(__typeInfos[4]);
    private TdfEnum<Blaze15SDK.Blaze.HardwareFlags> _hardwareFlags = new(__typeInfos[5]);
    private TdfString _isp = new(__typeInfos[6]);
    private TdfList<int> _latencyList = new(__typeInfos[7]);
    private TdfMap<string, int> _latencyMap = new(__typeInfos[8]);
    private TdfStruct<Blaze15SDK.Blaze.Util.NetworkQosData?> _qosData = new(__typeInfos[9]);
    private TdfString _timeZone = new(__typeInfos[10]);
    private TdfUInt64 _userInfoAttribute = new(__typeInfos[11]);
    private TdfList<ObjectId> _blazeObjectIdList = new(__typeInfos[12]);
    private TdfBool _crossPlatformOptIn = new(__typeInfos[13]);

    public UserSessionExtendedData()
    {
        __members = [
            _address,
            _bestPingSiteAlias,
            _country,
            _clientData,
            _dataMap,
            _hardwareFlags,
            _isp,
            _latencyList,
            _latencyMap,
            _qosData,
            _timeZone,
            _userInfoAttribute,
            _blazeObjectIdList,
            _crossPlatformOptIn,
        ];
    }

    public override Tdf CreateNew() => new UserSessionExtendedData();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "UserSessionExtendedData";
    public override string GetFullClassName() => "Blaze::UserSessionExtendedData";

    public Blaze15SDK.Blaze.NetworkAddress Address
    {
        get => _address.Value;
        set => _address.Value = value;
    }

    public string BestPingSiteAlias
    {
        get => _bestPingSiteAlias.Value;
        set => _bestPingSiteAlias.Value = value;
    }

    public string Country
    {
        get => _country.Value;
        set => _country.Value = value;
    }

    public object? ClientData
    {
        get => _clientData.Value;
        set => _clientData.Value = value;
    }

    public IDictionary<uint, long> DataMap
    {
        get => _dataMap.Value;
        set => _dataMap.Value = value;
    }

    public Blaze15SDK.Blaze.HardwareFlags HardwareFlags
    {
        get => _hardwareFlags.Value;
        set => _hardwareFlags.Value = value;
    }

    public string ISP
    {
        get => _isp.Value;
        set => _isp.Value = value;
    }

    public IList<int> LatencyList
    {
        get => _latencyList.Value;
        set => _latencyList.Value = value;
    }

    public IDictionary<string, int> LatencyMap
    {
        get => _latencyMap.Value;
        set => _latencyMap.Value = value;
    }

    public Blaze15SDK.Blaze.Util.NetworkQosData? QosData
    {
        get => _qosData.Value;
        set => _qosData.Value = value;
    }

    public string TimeZone
    {
        get => _timeZone.Value;
        set => _timeZone.Value = value;
    }

    public ulong UserInfoAttribute
    {
        get => _userInfoAttribute.Value;
        set => _userInfoAttribute.Value = value;
    }

    public IList<ObjectId> BlazeObjectIdList
    {
        get => _blazeObjectIdList.Value;
        set => _blazeObjectIdList.Value = value;
    }

    public bool CrossPlatformOptIn
    {
        get => _crossPlatformOptIn.Value;
        set => _crossPlatformOptIn.Value = value;
    }

}
