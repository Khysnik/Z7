using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Authentication;

public class ModifyEntitlement2Request : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("BlazeId", "mBlazeId", 0x8B5A6400, TdfType.Int64, 0, true), // Tag: BUID
        new TdfMemberInfo("UseCount", "mUseCount", 0x8EFD6E00, TdfType.String, 1, true), // Tag: COUN
        new TdfMemberInfo("EntitlementId", "mEntitlementId", 0x96990000, TdfType.Int64, 2, true), // Tag: EID
        new TdfMemberInfo("Termination", "mTermination", 0x978C2900, TdfType.String, 3, true), // Tag: EXPI
        new TdfMemberInfo("Status", "mStatus", 0xCF487400, TdfType.Enum, 4, true), // Tag: STAT
        new TdfMemberInfo("StatusReasonCode", "mStatusReasonCode", 0xCF4CA300, TdfType.Enum, 5, true), // Tag: STRC
        new TdfMemberInfo("Version", "mVersion", 0xDA5C8000, TdfType.UInt32, 6, true), // Tag: VER
    ];
    private ITdfMember[] __members;

    private TdfInt64 _blazeId = new(__typeInfos[0]);
    private TdfString _useCount = new(__typeInfos[1]);
    private TdfInt64 _entitlementId = new(__typeInfos[2]);
    private TdfString _termination = new(__typeInfos[3]);
    private TdfEnum<Blaze15SDK.Blaze.Authentication.EntitlementStatus> _status = new(__typeInfos[4]);
    private TdfEnum<Blaze15SDK.Blaze.Authentication.StatusReason> _statusReasonCode = new(__typeInfos[5]);
    private TdfUInt32 _version = new(__typeInfos[6]);

    public ModifyEntitlement2Request()
    {
        __members = [ _blazeId, _useCount, _entitlementId, _termination, _status, _statusReasonCode, _version ];
    }

    public override Tdf CreateNew() => new ModifyEntitlement2Request();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ModifyEntitlement2Request";
    public override string GetFullClassName() => "Blaze::Authentication::ModifyEntitlement2Request";

    public long BlazeId { get => _blazeId.Value; set => _blazeId.Value = value; }
    public string UseCount { get => _useCount.Value; set => _useCount.Value = value; }
    public long EntitlementId { get => _entitlementId.Value; set => _entitlementId.Value = value; }
    public string Termination { get => _termination.Value; set => _termination.Value = value; }
    public Blaze15SDK.Blaze.Authentication.EntitlementStatus Status { get => _status.Value; set => _status.Value = value; }
    public Blaze15SDK.Blaze.Authentication.StatusReason StatusReasonCode { get => _statusReasonCode.Value; set => _statusReasonCode.Value = value; }
    public uint Version { get => _version.Value; set => _version.Value = value; }
}
