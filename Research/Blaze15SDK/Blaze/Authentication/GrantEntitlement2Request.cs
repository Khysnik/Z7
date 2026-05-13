using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Authentication;

public class GrantEntitlement2Request : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("BlazeId", "mBlazeId", 0x8B5A6400, TdfType.Int64, 0, true), // Tag: BUID
        new TdfMemberInfo("UseCount", "mUseCount", 0x8EFD6E00, TdfType.String, 1, true), // Tag: COUN
        new TdfMemberInfo("DeviceId", "mDeviceId", 0x925A6400, TdfType.String, 2, true), // Tag: DEID
        new TdfMemberInfo("Termination", "mTermination", 0x978C2900, TdfType.String, 3, true), // Tag: EXPI
        new TdfMemberInfo("GrantDate", "mGrantDate", 0x9E487900, TdfType.String, 4, true), // Tag: GDAY
        new TdfMemberInfo("GroupName", "mGroupName", 0x9EE86D00, TdfType.String, 5, true), // Tag: GNAM
        new TdfMemberInfo("IsSearch", "mIsSearch", 0xA73CE500, TdfType.Bool, 6, true), // Tag: ISSE
        new TdfMemberInfo("ManagedLifecycle", "mManagedLifecycle", 0xB61B2900, TdfType.Bool, 7, true), // Tag: MALI
        new TdfMemberInfo("WithPersona", "mWithPersona", 0xC25CB300, TdfType.Bool, 8, true), // Tag: PERS
        new TdfMemberInfo("ProjectId", "mProjectId", 0xC2AA6400, TdfType.String, 9, true), // Tag: PJID
        new TdfMemberInfo("ProductCatalog", "mProductCatalog", 0xC328E100, TdfType.Enum, 10, true), // Tag: PRCA
        new TdfMemberInfo("ProductId", "mProductId", 0xC32A6400, TdfType.String, 11, true), // Tag: PRID
        new TdfMemberInfo("Status", "mStatus", 0xCF487400, TdfType.Enum, 12, true), // Tag: STAT
        new TdfMemberInfo("StatusReasonCode", "mStatusReasonCode", 0xCF4CA300, TdfType.Enum, 13, true), // Tag: STRC
        new TdfMemberInfo("EntitlementTag", "mEntitlementTag", 0xD219C000, TdfType.String, 14, true), // Tag: TAG
        new TdfMemberInfo("EntitlementType", "mEntitlementType", 0xD39C2500, TdfType.Enum, 15, true), // Tag: TYPE
    ];
    private ITdfMember[] __members;

    private TdfInt64 _blazeId = new(__typeInfos[0]);
    private TdfString _useCount = new(__typeInfos[1]);
    private TdfString _deviceId = new(__typeInfos[2]);
    private TdfString _termination = new(__typeInfos[3]);
    private TdfString _grantDate = new(__typeInfos[4]);
    private TdfString _groupName = new(__typeInfos[5]);
    private TdfBool _isSearch = new(__typeInfos[6]);
    private TdfBool _managedLifecycle = new(__typeInfos[7]);
    private TdfBool _withPersona = new(__typeInfos[8]);
    private TdfString _projectId = new(__typeInfos[9]);
    private TdfEnum<Blaze15SDK.Blaze.Authentication.ProductCatalog> _productCatalog = new(__typeInfos[10]);
    private TdfString _productId = new(__typeInfos[11]);
    private TdfEnum<Blaze15SDK.Blaze.Authentication.EntitlementStatus> _status = new(__typeInfos[12]);
    private TdfEnum<Blaze15SDK.Blaze.Authentication.StatusReason> _statusReasonCode = new(__typeInfos[13]);
    private TdfString _entitlementTag = new(__typeInfos[14]);
    private TdfEnum<Blaze15SDK.Blaze.Authentication.EntitlementType> _entitlementType = new(__typeInfos[15]);

    public GrantEntitlement2Request()
    {
        __members = [
            _blazeId,
            _useCount,
            _deviceId,
            _termination,
            _grantDate,
            _groupName,
            _isSearch,
            _managedLifecycle,
            _withPersona,
            _projectId,
            _productCatalog,
            _productId,
            _status,
            _statusReasonCode,
            _entitlementTag,
            _entitlementType,
        ];
    }

    public override Tdf CreateNew() => new GrantEntitlement2Request();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GrantEntitlement2Request";
    public override string GetFullClassName() => "Blaze::Authentication::GrantEntitlement2Request";

    public long BlazeId { get => _blazeId.Value; set => _blazeId.Value = value; }
    public string UseCount { get => _useCount.Value; set => _useCount.Value = value; }
    public string DeviceId { get => _deviceId.Value; set => _deviceId.Value = value; }
    public string Termination { get => _termination.Value; set => _termination.Value = value; }
    public string GrantDate { get => _grantDate.Value; set => _grantDate.Value = value; }
    public string GroupName { get => _groupName.Value; set => _groupName.Value = value; }
    public bool IsSearch { get => _isSearch.Value; set => _isSearch.Value = value; }
    public bool ManagedLifecycle { get => _managedLifecycle.Value; set => _managedLifecycle.Value = value; }
    public bool WithPersona { get => _withPersona.Value; set => _withPersona.Value = value; }
    public string ProjectId { get => _projectId.Value; set => _projectId.Value = value; }
    public Blaze15SDK.Blaze.Authentication.ProductCatalog ProductCatalog { get => _productCatalog.Value; set => _productCatalog.Value = value; }
    public string ProductId { get => _productId.Value; set => _productId.Value = value; }
    public Blaze15SDK.Blaze.Authentication.EntitlementStatus Status { get => _status.Value; set => _status.Value = value; }
    public Blaze15SDK.Blaze.Authentication.StatusReason StatusReasonCode { get => _statusReasonCode.Value; set => _statusReasonCode.Value = value; }
    public string EntitlementTag { get => _entitlementTag.Value; set => _entitlementTag.Value = value; }
    public Blaze15SDK.Blaze.Authentication.EntitlementType EntitlementType { get => _entitlementType.Value; set => _entitlementType.Value = value; }
}
