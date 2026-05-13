using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class NetworkAddress : Union
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("XboxClientAddress", "mXboxClientAddress", 0xDA1B3500, TdfType.Struct, 0, false), // Tag: VALU
        new TdfMemberInfo("DummyStruct", "mDummyStruct", 0xDA1B3500, TdfType.Struct, 1, false), // Tag: VALU
        new TdfMemberInfo("IpPairAddress", "mIpPairAddress", 0xDA1B3500, TdfType.Struct, 2, false), // Tag: VALU
        new TdfMemberInfo("IpAddress", "mIpAddress", 0xDA1B3500, TdfType.Struct, 3, false), // Tag: VALU
        new TdfMemberInfo("HostNameAddress", "mHostNameAddress", 0xDA1B3500, TdfType.Struct, 4, false), // Tag: VALU
    ];

    private TdfStruct<Blaze15SDK.Blaze.XboxClientAddress?> _xboxClientAddress = new(__typeInfos[0]);
    private TdfStruct<Blaze15SDK.Blaze.DummyStruct?> _dummyStruct = new(__typeInfos[1]);
    private TdfStruct<Blaze15SDK.Blaze.IpPairAddress?> _ipPairAddress = new(__typeInfos[2]);
    private TdfStruct<Blaze15SDK.Blaze.IpAddress?> _ipAddress = new(__typeInfos[3]);
    private TdfStruct<Blaze15SDK.Blaze.HostNameAddress?> _hostNameAddress = new(__typeInfos[4]);

    public override ITdfMember? GetActiveMember()
    {
        return ActiveIndex switch
        {
            0 => _xboxClientAddress,
            1 => _dummyStruct,
            2 => _ipPairAddress,
            3 => _ipAddress,
            4 => _hostNameAddress,
            _ => null
        };
    }
    public override Tdf CreateNew() => new NetworkAddress();
    public override string GetClassName() => "NetworkAddress";
    public override string GetFullClassName() => "Blaze::NetworkAddress";

    public Blaze15SDK.Blaze.XboxClientAddress? XboxClientAddress
    {
        get => _xboxClientAddress.Value;
        set
        {
            SwitchActiveIndex(0);
            _xboxClientAddress.Value = value;
        }
    }

    public Blaze15SDK.Blaze.DummyStruct? DummyStruct
    {
        get => _dummyStruct.Value;
        set
        {
            SwitchActiveIndex(1);
            _dummyStruct.Value = value;
        }
    }

    public Blaze15SDK.Blaze.IpPairAddress? IpPairAddress
    {
        get => _ipPairAddress.Value;
        set
        {
            SwitchActiveIndex(2);
            _ipPairAddress.Value = value;
        }
    }

    public Blaze15SDK.Blaze.IpAddress? IpAddress
    {
        get => _ipAddress.Value;
        set
        {
            SwitchActiveIndex(3);
            _ipAddress.Value = value;
        }
    }

    public Blaze15SDK.Blaze.HostNameAddress? HostNameAddress
    {
        get => _hostNameAddress.Value;
        set
        {
            SwitchActiveIndex(4);
            _hostNameAddress.Value = value;
        }
    }

}
