using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class QosPingSiteInfo : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Address", "mAddress", 0xC3384000, TdfType.String, 0, true), // Tag: PSA
        new TdfMemberInfo("Port", "mPort", 0xC33C0000, TdfType.UInt16, 1, true), // Tag: PSP
    ];
    private ITdfMember[] __members;

    private TdfString _address = new(__typeInfos[0]);
    private TdfUInt16 _port = new(__typeInfos[1]);

    public QosPingSiteInfo()
    {
        __members = [
            _address,
            _port,
        ];
    }

    public override Tdf CreateNew() => new QosPingSiteInfo();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "QosPingSiteInfo";
    public override string GetFullClassName() => "Blaze::QosPingSiteInfo";

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

}
