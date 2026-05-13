using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class IpPairAddress : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ExternalAddress", "mExternalAddress", 0x978A7000, TdfType.Struct, 0, true), // Tag: EXIP
        new TdfMemberInfo("InternalAddress", "mInternalAddress", 0xA6EA7000, TdfType.Struct, 1, true), // Tag: INIP
        new TdfMemberInfo("MachineId", "mMachineId", 0xB618E900, TdfType.UInt64, 2, true), // Tag: MACI
    ];
    private ITdfMember[] __members;

    private TdfStruct<Blaze15SDK.Blaze.IpAddress?> _externalAddress = new(__typeInfos[0]);
    private TdfStruct<Blaze15SDK.Blaze.IpAddress?> _internalAddress = new(__typeInfos[1]);
    private TdfUInt64 _machineId = new(__typeInfos[2]);

    public IpPairAddress()
    {
        __members = [
            _externalAddress,
            _internalAddress,
            _machineId,
        ];
    }

    public override Tdf CreateNew() => new IpPairAddress();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "IpPairAddress";
    public override string GetFullClassName() => "Blaze::IpPairAddress";

    public Blaze15SDK.Blaze.IpAddress? ExternalAddress
    {
        get => _externalAddress.Value;
        set => _externalAddress.Value = value;
    }

    public Blaze15SDK.Blaze.IpAddress? InternalAddress
    {
        get => _internalAddress.Value;
        set => _internalAddress.Value = value;
    }

    public ulong MachineId
    {
        get => _machineId.Value;
        set => _machineId.Value = value;
    }

}
