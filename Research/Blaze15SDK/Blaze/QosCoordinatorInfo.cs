using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class QosCoordinatorInfo : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("DefaultPingSitesPort", "mDefaultPingSitesPort", 0x930CF000, TdfType.UInt16, 0, true), // Tag: DPSP
        new TdfMemberInfo("Address", "mAddress", 0xC6384000, TdfType.String, 1, true), // Tag: QCA
        new TdfMemberInfo("Port", "mPort", 0xC63C0000, TdfType.UInt16, 2, true), // Tag: QCP
        new TdfMemberInfo("Profile", "mProfile", 0xC70C8000, TdfType.String, 3, true), // Tag: QPR
    ];
    private ITdfMember[] __members;

    private TdfUInt16 _defaultPingSitesPort = new(__typeInfos[0]);
    private TdfString _address = new(__typeInfos[1]);
    private TdfUInt16 _port = new(__typeInfos[2]);
    private TdfString _profile = new(__typeInfos[3]);

    public QosCoordinatorInfo()
    {
        __members = [
            _defaultPingSitesPort,
            _address,
            _port,
            _profile,
        ];
    }

    public override Tdf CreateNew() => new QosCoordinatorInfo();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "QosCoordinatorInfo";
    public override string GetFullClassName() => "Blaze::QosCoordinatorInfo";

    public ushort DefaultPingSitesPort
    {
        get => _defaultPingSitesPort.Value;
        set => _defaultPingSitesPort.Value = value;
    }

    public string Address
    {
        get => _address.Value;
        set => _address.Value = value;
    }

    public ushort Port
    {
        get => _port.Value;
        set => _port.Value = value;
    }

    public string Profile
    {
        get => _profile.Value;
        set => _profile.Value = value;
    }

}
