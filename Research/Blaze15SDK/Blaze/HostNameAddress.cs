using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class HostNameAddress : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("MachineId", "mMachineId", 0xB618E900, TdfType.UInt64, 0, true), // Tag: MACI
        new TdfMemberInfo("HostName", "mHostName", 0xBA1B6500, TdfType.String, 1, true), // Tag: NAME
        new TdfMemberInfo("Port", "mPort", 0xC2FCB400, TdfType.UInt16, 2, true), // Tag: PORT
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _machineId = new(__typeInfos[0]);
    private TdfString _hostName = new(__typeInfos[1]);
    private TdfUInt16 _port = new(__typeInfos[2]);

    public HostNameAddress()
    {
        __members = [
            _machineId,
            _hostName,
            _port,
        ];
    }

    public override Tdf CreateNew() => new HostNameAddress();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "HostNameAddress";
    public override string GetFullClassName() => "Blaze::HostNameAddress";

    public ulong MachineId
    {
        get => _machineId.Value;
        set => _machineId.Value = value;
    }

    public string HostName
    {
        get => _hostName.Value;
        set => _hostName.Value = value;
    }

    public ushort Port
    {
        get => _port.Value;
        set => _port.Value = value;
    }

}
