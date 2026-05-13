using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Util;

public class GetTelemetryServerResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Address", "mAddress", 0x864CB300, TdfType.String, 0, true), // Tag: ADRS
        new TdfMemberInfo("IsAnonymous", "mIsAnonymous", 0x86EBEE00, TdfType.Bool, 1, true), // Tag: ANON
        new TdfMemberInfo("Key2", "mKey2", 0x8AB97900, TdfType.Blob, 2, true), // Tag: BKEY
        new TdfMemberInfo("Country", "mCountry", 0x8F4CB900, TdfType.UInt32, 3, true), // Tag: CTRY
        new TdfMemberInfo("Disable", "mDisable", 0x929CE100, TdfType.String, 4, true), // Tag: DISA
        new TdfMemberInfo("EnableDisconnectTelemetry", "mEnableDisconnectTelemetry", 0x9648F400, TdfType.Bool, 5, true), // Tag: EDCT
        new TdfMemberInfo("Filter", "mFilter", 0x9A9B3400, TdfType.String, 6, true), // Tag: FILT
        new TdfMemberInfo("Locale", "mLocale", 0xB2F8C000, TdfType.UInt32, 7, true), // Tag: LOC
        new TdfMemberInfo("Underage", "mUnderage", 0xB69BB200, TdfType.Bool, 8, true), // Tag: MINR
        new TdfMemberInfo("NoToggleOk", "mNoToggleOk", 0xBAFBEB00, TdfType.String, 9, true), // Tag: NOOK
        new TdfMemberInfo("PinEnvironment", "mPinEnvironment", 0xC25BB600, TdfType.String, 10, true), // Tag: PENV
        new TdfMemberInfo("Port", "mPort", 0xC2FCB400, TdfType.UInt32, 11, true), // Tag: PORT
        new TdfMemberInfo("PinUrl", "mPinUrl", 0xC35CAC00, TdfType.String, 12, true), // Tag: PURL
        new TdfMemberInfo("SendDelay", "mSendDelay", 0xCE4B3900, TdfType.UInt32, 13, true), // Tag: SDLY
        new TdfMemberInfo("SessionID", "mSessionID", 0xCE5CF300, TdfType.String, 14, true), // Tag: SESS
        new TdfMemberInfo("Key", "mKey", 0xCEB97900, TdfType.String, 15, true), // Tag: SKEY
        new TdfMemberInfo("SendPercentage", "mSendPercentage", 0xCF08F400, TdfType.UInt32, 16, true), // Tag: SPCT
        new TdfMemberInfo("UseServerTime", "mUseServerTime", 0xCF4A6D00, TdfType.String, 17, true), // Tag: STIM
        new TdfMemberInfo("TelemetryServiceName", "mTelemetryServiceName", 0xCF6BAD00, TdfType.String, 18, true), // Tag: SVNM
    ];
    private ITdfMember[] __members;

    private TdfString _address = new(__typeInfos[0]);
    private TdfBool _isAnonymous = new(__typeInfos[1]);
    private TdfBlob _key2 = new(__typeInfos[2]);
    private TdfUInt32 _country = new(__typeInfos[3]);
    private TdfString _disable = new(__typeInfos[4]);
    private TdfBool _enableDisconnectTelemetry = new(__typeInfos[5]);
    private TdfString _filter = new(__typeInfos[6]);
    private TdfUInt32 _locale = new(__typeInfos[7]);
    private TdfBool _underage = new(__typeInfos[8]);
    private TdfString _noToggleOk = new(__typeInfos[9]);
    private TdfString _pinEnvironment = new(__typeInfos[10]);
    private TdfUInt32 _port = new(__typeInfos[11]);
    private TdfString _pinUrl = new(__typeInfos[12]);
    private TdfUInt32 _sendDelay = new(__typeInfos[13]);
    private TdfString _sessionID = new(__typeInfos[14]);
    private TdfString _key = new(__typeInfos[15]);
    private TdfUInt32 _sendPercentage = new(__typeInfos[16]);
    private TdfString _useServerTime = new(__typeInfos[17]);
    private TdfString _telemetryServiceName = new(__typeInfos[18]);

    public GetTelemetryServerResponse()
    {
        __members = [ _address, _isAnonymous, _key2, _country, _disable, _enableDisconnectTelemetry, _filter, _locale, _underage, _noToggleOk, _pinEnvironment, _port, _pinUrl, _sendDelay, _sessionID, _key, _sendPercentage, _useServerTime, _telemetryServiceName ];
    }

    public override Tdf CreateNew() => new GetTelemetryServerResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetTelemetryServerResponse";
    public override string GetFullClassName() => "Blaze::GameManager::GetTelemetryServerResponse";

    public string Address
    {
        get => _address.Value;
        set => _address.Value = value;
    }

    public bool IsAnonymous
    {
        get => _isAnonymous.Value;
        set => _isAnonymous.Value = value;
    }

    public byte[] Key2
    {
        get => _key2.Value;
        set => _key2.Value = value;
    }

    public uint Country
    {
        get => _country.Value;
        set => _country.Value = value;
    }

    public string Disable
    {
        get => _disable.Value;
        set => _disable.Value = value;
    }

    public bool EnableDisconnectTelemetry
    {
        get => _enableDisconnectTelemetry.Value;
        set => _enableDisconnectTelemetry.Value = value;
    }

    public string Filter
    {
        get => _filter.Value;
        set => _filter.Value = value;
    }

    public uint Locale
    {
        get => _locale.Value;
        set => _locale.Value = value;
    }

    public bool Underage
    {
        get => _underage.Value;
        set => _underage.Value = value;
    }

    public string NoToggleOk
    {
        get => _noToggleOk.Value;
        set => _noToggleOk.Value = value;
    }

    public string PinEnvironment
    {
        get => _pinEnvironment.Value;
        set => _pinEnvironment.Value = value;
    }

    public uint Port
    {
        get => _port.Value;
        set => _port.Value = value;
    }

    public string PinUrl
    {
        get => _pinUrl.Value;
        set => _pinUrl.Value = value;
    }

    public uint SendDelay
    {
        get => _sendDelay.Value;
        set => _sendDelay.Value = value;
    }

    public string SessionID
    {
        get => _sessionID.Value;
        set => _sessionID.Value = value;
    }

    public string Key
    {
        get => _key.Value;
        set => _key.Value = value;
    }

    public uint SendPercentage
    {
        get => _sendPercentage.Value;
        set => _sendPercentage.Value = value;
    }

    public string UseServerTime
    {
        get => _useServerTime.Value;
        set => _useServerTime.Value = value;
    }

    public string TelemetryServiceName
    {
        get => _telemetryServiceName.Value;
        set => _telemetryServiceName.Value = value;
    }

}
