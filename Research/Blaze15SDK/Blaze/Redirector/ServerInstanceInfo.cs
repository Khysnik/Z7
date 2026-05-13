using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Redirector;

public class ServerInstanceInfo : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Address", "mAddress", 0x86493200, TdfType.Union, 0, true), // Tag: ADDR
        new TdfMemberInfo("AddressRemaps", "mAddressRemaps", 0x86D87000, TdfType.List, 1, true), // Tag: AMAP
        new TdfMemberInfo("CertificateList", "mCertificateList", 0x8E5CB400, TdfType.List, 2, true), // Tag: CERT
        new TdfMemberInfo("Messages", "mMessages", 0xB739F300, TdfType.List, 3, true), // Tag: MSGS
        new TdfMemberInfo("NameRemaps", "mNameRemaps", 0xBAD87000, TdfType.List, 4, true), // Tag: NMAP
        new TdfMemberInfo("Secure", "mSecure", 0xCE58F500, TdfType.Bool, 5, true), // Tag: SECU
        new TdfMemberInfo("TrialServiceName", "mTrialServiceName", 0xD33DAE00, TdfType.String, 6, true), // Tag: TSVN
        new TdfMemberInfo("DefaultDnsAddress", "mDefaultDnsAddress", 0xE24BB300, TdfType.UInt32, 7, true), // Tag: XDNS
    ];
    private ITdfMember[] __members;

    private TdfUnion<Blaze15SDK.Blaze.Redirector.ServerAddress> _address = new(__typeInfos[0]);
    private TdfList<Blaze15SDK.Blaze.Redirector.AddressRemapEntry> _addressRemaps = new(__typeInfos[1]);
    private TdfList<byte[]> _certificateList = new(__typeInfos[2]);
    private TdfList<string> _messages = new(__typeInfos[3]);
    private TdfList<Blaze15SDK.Blaze.Redirector.NameRemapEntry> _nameRemaps = new(__typeInfos[4]);
    private TdfBool _secure = new(__typeInfos[5]);
    private TdfString _trialServiceName = new(__typeInfos[6]);
    private TdfUInt32 _defaultDnsAddress = new(__typeInfos[7]);

    public ServerInstanceInfo()
    {
        __members = [ _address, _addressRemaps, _certificateList, _messages, _nameRemaps, _secure, _trialServiceName, _defaultDnsAddress ];
    }

    public override Tdf CreateNew() => new ServerInstanceInfo();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ServerInstanceInfo";
    public override string GetFullClassName() => "Blaze::Redirector::ServerInstanceInfo";

    public Blaze15SDK.Blaze.Redirector.ServerAddress Address
    {
        get => _address.Value;
        set => _address.Value = value;
    }

    public IList<Blaze15SDK.Blaze.Redirector.AddressRemapEntry> AddressRemaps
    {
        get => _addressRemaps.Value;
        set => _addressRemaps.Value = value;
    }

    public IList<byte[]> CertificateList
    {
        get => _certificateList.Value;
        set => _certificateList.Value = value;
    }

    public IList<string> Messages
    {
        get => _messages.Value;
        set => _messages.Value = value;
    }

    public IList<Blaze15SDK.Blaze.Redirector.NameRemapEntry> NameRemaps
    {
        get => _nameRemaps.Value;
        set => _nameRemaps.Value = value;
    }

    public bool Secure
    {
        get => _secure.Value;
        set => _secure.Value = value;
    }

    public string TrialServiceName
    {
        get => _trialServiceName.Value;
        set => _trialServiceName.Value = value;
    }

    public uint DefaultDnsAddress
    {
        get => _defaultDnsAddress.Value;
        set => _defaultDnsAddress.Value = value;
    }

}
