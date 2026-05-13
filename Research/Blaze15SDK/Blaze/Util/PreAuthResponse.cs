using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Util;

public class PreAuthResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("AuthenticationSource", "mAuthenticationSource", 0x873CA300, TdfType.String, 0, true), // Tag: ASRC
        new TdfMemberInfo("ComponentIds", "mComponentIds", 0x8E993300, TdfType.List, 1, true), // Tag: CIDS
        new TdfMemberInfo("ClientId", "mClientId", 0x8ECA6400, TdfType.String, 2, true), // Tag: CLID
        new TdfMemberInfo("Config", "mConfig", 0x8EFBA600, TdfType.Struct, 3, true), // Tag: CONF
        new TdfMemberInfo("EntitlementSource", "mEntitlementSource", 0x973CA300, TdfType.String, 4, true), // Tag: ESRC
        new TdfMemberInfo("ServiceName", "mServiceName", 0xA6ECF400, TdfType.String, 5, true), // Tag: INST
        new TdfMemberInfo("MachineId", "mMachineId", 0xB61A6400, TdfType.UInt32, 6, true), // Tag: MAID
        new TdfMemberInfo("UnderageSupported", "mUnderageSupported", 0xB69BB200, TdfType.Bool, 7, true), // Tag: MINR
        new TdfMemberInfo("PersonaNamespace", "mPersonaNamespace", 0xBA1CF000, TdfType.String, 8, true), // Tag: NASP
        new TdfMemberInfo("LegalDocGameIdentifier", "mLegalDocGameIdentifier", 0xC29B2400, TdfType.String, 9, true), // Tag: PILD
        new TdfMemberInfo("Platform", "mPlatform", 0xC2C87400, TdfType.String, 10, true), // Tag: PLAT
        new TdfMemberInfo("QosSettings", "mQosSettings", 0xC6FCF300, TdfType.Struct, 11, true), // Tag: QOSS
        new TdfMemberInfo("RegistrationSource", "mRegistrationSource", 0xCB3CA300, TdfType.String, 12, true), // Tag: RSRC
        new TdfMemberInfo("ServerVersion", "mServerVersion", 0xCF697200, TdfType.String, 13, true), // Tag: SVER
    ];
    private ITdfMember[] __members;

    private TdfString _authenticationSource = new(__typeInfos[0]);
    private TdfList<ushort> _componentIds = new(__typeInfos[1]);
    private TdfString _clientId = new(__typeInfos[2]);
    private TdfStruct<Blaze15SDK.Blaze.Util.FetchConfigResponse?> _config = new(__typeInfos[3]);
    private TdfString _entitlementSource = new(__typeInfos[4]);
    private TdfUInt32 _machineId = new(__typeInfos[6]);
    private TdfBool _underageSupported = new(__typeInfos[7]);
    private TdfString _personaNamespace = new(__typeInfos[8]);
    private TdfString _legalDocGameIdentifier = new(__typeInfos[9]);
    private TdfString _platform = new(__typeInfos[10]);
    private TdfStruct<Blaze15SDK.Blaze.QosConfigInfo?> _qosSettings = new(__typeInfos[11]);
    private TdfString _registrationSource = new(__typeInfos[12]);
    private TdfString _serverVersion = new(__typeInfos[13]);
    private TdfString _serviceName = new(__typeInfos[5]);

    public PreAuthResponse()
    {
        __members = [ _authenticationSource, _componentIds, _clientId, _config, _entitlementSource, _serviceName, _machineId, _underageSupported, _personaNamespace, _legalDocGameIdentifier, _platform, _qosSettings, _registrationSource, _serverVersion ];
    }

    public override Tdf CreateNew() => new PreAuthResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "PreAuthResponse";
    public override string GetFullClassName() => "Blaze::GameManager::PreAuthResponse";

    public string AuthenticationSource
    {
        get => _authenticationSource.Value;
        set => _authenticationSource.Value = value;
    }

    public IList<ushort> ComponentIds
    {
        get => _componentIds.Value;
        set => _componentIds.Value = value;
    }

    public string ClientId
    {
        get => _clientId.Value;
        set => _clientId.Value = value;
    }

    public Blaze15SDK.Blaze.Util.FetchConfigResponse? Config
    {
        get => _config.Value;
        set => _config.Value = value;
    }

    public string EntitlementSource
    {
        get => _entitlementSource.Value;
        set => _entitlementSource.Value = value;
    }

    public uint MachineId
    {
        get => _machineId.Value;
        set => _machineId.Value = value;
    }

    public bool UnderageSupported
    {
        get => _underageSupported.Value;
        set => _underageSupported.Value = value;
    }

    public string PersonaNamespace
    {
        get => _personaNamespace.Value;
        set => _personaNamespace.Value = value;
    }

    public string LegalDocGameIdentifier
    {
        get => _legalDocGameIdentifier.Value;
        set => _legalDocGameIdentifier.Value = value;
    }

    public string Platform
    {
        get => _platform.Value;
        set => _platform.Value = value;
    }

    public Blaze15SDK.Blaze.QosConfigInfo? QosSettings
    {
        get => _qosSettings.Value;
        set => _qosSettings.Value = value;
    }

    public string RegistrationSource
    {
        get => _registrationSource.Value;
        set => _registrationSource.Value = value;
    }

    public string ServerVersion
    {
        get => _serverVersion.Value;
        set => _serverVersion.Value = value;
    }

    public string ServiceName
    {
        get => _serviceName.Value;
        set => _serviceName.Value = value;
    }

}
