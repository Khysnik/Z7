using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Redirector;

public class X509MatchCriteria : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("PublicKeyBits", "mPublicKeyBits", 0x8A9D3300, TdfType.Int32, 0, true), // Tag: BITS
        new TdfMemberInfo("X509EntryMap", "mX509EntryMap", 0x96ED3200, TdfType.Map, 1, true), // Tag: ENTR
        new TdfMemberInfo("Hostname", "mHostname", 0xA2FCF400, TdfType.String, 2, true), // Tag: HOST
        new TdfMemberInfo("ServiceName", "mServiceName", 0xBA1B6500, TdfType.String, 3, true), // Tag: NAME
        new TdfMemberInfo("Port", "mPort", 0xC2FCB400, TdfType.Int32, 4, true), // Tag: PORT
        new TdfMemberInfo("DirtySDKVersion", "mDirtySDKVersion", 0xD8000000, TdfType.String, 5, true), // Tag: V
        new TdfMemberInfo("DirtyCertVersion", "mDirtyCertVersion", 0xDA5CB300, TdfType.UInt32, 6, true), // Tag: VERS
    ];
    private ITdfMember[] __members;

    private TdfInt32 _publicKeyBits = new(__typeInfos[0]);
    private TdfMap<string, string> _x509EntryMap = new(__typeInfos[1]);
    private TdfString _hostname = new(__typeInfos[2]);
    private TdfString _serviceName = new(__typeInfos[3]);
    private TdfInt32 _port = new(__typeInfos[4]);
    private TdfString _dirtySDKVersion = new(__typeInfos[5]);
    private TdfUInt32 _dirtyCertVersion = new(__typeInfos[6]);

    public X509MatchCriteria()
    {
        __members = [ _publicKeyBits, _x509EntryMap, _hostname, _serviceName, _port, _dirtySDKVersion, _dirtyCertVersion ];
    }

    public override Tdf CreateNew() => new X509MatchCriteria();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "X509MatchCriteria";
    public override string GetFullClassName() => "Blaze::Redirector::X509MatchCriteria";

    public int PublicKeyBits
    {
        get => _publicKeyBits.Value;
        set => _publicKeyBits.Value = value;
    }

    public IDictionary<string, string> X509EntryMap
    {
        get => _x509EntryMap.Value;
        set => _x509EntryMap.Value = value;
    }

    public string Hostname
    {
        get => _hostname.Value;
        set => _hostname.Value = value;
    }

    public string ServiceName
    {
        get => _serviceName.Value;
        set => _serviceName.Value = value;
    }

    public int Port
    {
        get => _port.Value;
        set => _port.Value = value;
    }

    public string DirtySDKVersion
    {
        get => _dirtySDKVersion.Value;
        set => _dirtySDKVersion.Value = value;
    }

    public uint DirtyCertVersion
    {
        get => _dirtyCertVersion.Value;
        set => _dirtyCertVersion.Value = value;
    }

}
