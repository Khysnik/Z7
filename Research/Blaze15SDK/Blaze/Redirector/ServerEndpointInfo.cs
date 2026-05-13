using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Redirector;

public class ServerEndpointInfo : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Addresses", "mAddresses", 0x864CB300, TdfType.List, 0, true), // Tag: ADRS
        new TdfMemberInfo("BindType", "mBindType", 0x8A9BA400, TdfType.Enum, 1, true), // Tag: BIND
        new TdfMemberInfo("CurrentConnections", "mCurrentConnections", 0x8E3BEE00, TdfType.UInt32, 2, true), // Tag: CCON
        new TdfMemberInfo("Channel", "mChannel", 0x8E886E00, TdfType.String, 3, true), // Tag: CHAN
        new TdfMemberInfo("ConnectionTypeString", "mConnectionTypeString", 0x8F4E7000, TdfType.String, 4, true), // Tag: CTYP
        new TdfMemberInfo("Decoder", "mDecoder", 0x9258C000, TdfType.String, 5, true), // Tag: DEC
        new TdfMemberInfo("Encoder", "mEncoder", 0x96E8C000, TdfType.String, 6, true), // Tag: ENC
        new TdfMemberInfo("MaxConnections", "mMaxConnections", 0xB63BEE00, TdfType.UInt32, 7, true), // Tag: MCON
        new TdfMemberInfo("Protocol", "mProtocol", 0xC32BF400, TdfType.String, 8, true), // Tag: PROT
    ];
    private ITdfMember[] __members;

    private TdfList<Blaze15SDK.Blaze.Redirector.ServerAddressInfo> _addresses = new(__typeInfos[0]);
    private TdfEnum<Blaze15SDK.Blaze.BindType> _bindType = new(__typeInfos[1]);
    private TdfUInt32 _currentConnections = new(__typeInfos[2]);
    private TdfString _channel = new(__typeInfos[3]);
    private TdfString _connectionTypeString = new(__typeInfos[4]);
    private TdfString _decoder = new(__typeInfos[5]);
    private TdfString _encoder = new(__typeInfos[6]);
    private TdfUInt32 _maxConnections = new(__typeInfos[7]);
    private TdfString _protocol = new(__typeInfos[8]);

    public ServerEndpointInfo()
    {
        __members = [ _addresses, _bindType, _currentConnections, _channel, _connectionTypeString, _decoder, _encoder, _maxConnections, _protocol ];
    }

    public override Tdf CreateNew() => new ServerEndpointInfo();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ServerEndpointInfo";
    public override string GetFullClassName() => "Blaze::Redirector::ServerEndpointInfo";

    public IList<Blaze15SDK.Blaze.Redirector.ServerAddressInfo> Addresses
    {
        get => _addresses.Value;
        set => _addresses.Value = value;
    }

    public Blaze15SDK.Blaze.BindType BindType
    {
        get => _bindType.Value;
        set => _bindType.Value = value;
    }

    public uint CurrentConnections
    {
        get => _currentConnections.Value;
        set => _currentConnections.Value = value;
    }

    public string Channel
    {
        get => _channel.Value;
        set => _channel.Value = value;
    }

    public string ConnectionTypeString
    {
        get => _connectionTypeString.Value;
        set => _connectionTypeString.Value = value;
    }

    public string Decoder
    {
        get => _decoder.Value;
        set => _decoder.Value = value;
    }

    public string Encoder
    {
        get => _encoder.Value;
        set => _encoder.Value = value;
    }

    public uint MaxConnections
    {
        get => _maxConnections.Value;
        set => _maxConnections.Value = value;
    }

    public string Protocol
    {
        get => _protocol.Value;
        set => _protocol.Value = value;
    }

}
