using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class DummyStruct : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("MachineId", "mMachineId", 0xB618E900, TdfType.UInt64, 0, true), // Tag: MACI
        new TdfMemberInfo("Port", "mPort", 0xC2FCB400, TdfType.UInt16, 1, true), // Tag: PORT
        new TdfMemberInfo("SiteName", "mSiteName", 0xCE9D2500, TdfType.String, 2, true), // Tag: SITE
        new TdfMemberInfo("Sid", "mSid", 0xCF6A6400, TdfType.UInt32, 3, true), // Tag: SVID
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _machineId = new(__typeInfos[0]);
    private TdfUInt16 _port = new(__typeInfos[1]);
    private TdfString _siteName = new(__typeInfos[2]);
    private TdfUInt32 _sid = new(__typeInfos[3]);

    public DummyStruct()
    {
        __members = [
            _machineId,
            _port,
            _siteName,
            _sid,
        ];
    }

    public override Tdf CreateNew() => new DummyStruct();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "DummyStruct";
    public override string GetFullClassName() => "Blaze::DummyStruct";

    public ulong MachineId
    {
        get => _machineId.Value;
        set => _machineId.Value = value;
    }

    public ushort Port
    {
        get => _port.Value;
        set => _port.Value = value;
    }

    public string SiteName
    {
        get => _siteName.Value;
        set => _siteName.Value = value;
    }

    public uint Sid
    {
        get => _sid.Value;
        set => _sid.Value = value;
    }

}
