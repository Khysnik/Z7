using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class XboxClientAddress : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("MachineId", "mMachineId", 0xB618E900, TdfType.UInt64, 0, true), // Tag: MACI
        new TdfMemberInfo("XnAddr", "mXnAddr", 0xE2493200, TdfType.Blob, 1, true), // Tag: XDDR
        new TdfMemberInfo("Xuid", "mXuid", 0xE35A6400, TdfType.UInt64, 2, true), // Tag: XUID
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _machineId = new(__typeInfos[0]);
    private TdfBlob _xnAddr = new(__typeInfos[1]);
    private TdfUInt64 _xuid = new(__typeInfos[2]);

    public XboxClientAddress()
    {
        __members = [
            _machineId,
            _xnAddr,
            _xuid,
        ];
    }

    public override Tdf CreateNew() => new XboxClientAddress();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "XboxClientAddress";
    public override string GetFullClassName() => "Blaze::XboxClientAddress";

    public ulong MachineId
    {
        get => _machineId.Value;
        set => _machineId.Value = value;
    }

    public byte[] XnAddr
    {
        get => _xnAddr.Value;
        set => _xnAddr.Value = value;
    }

    public ulong Xuid
    {
        get => _xuid.Value;
        set => _xuid.Value = value;
    }

}
