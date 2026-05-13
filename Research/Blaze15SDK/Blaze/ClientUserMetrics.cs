using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class ClientUserMetrics : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("SttCharCountRecv", "mSttCharCountRecv", 0xCE38EF00, TdfType.UInt32, 0, true), // Tag: SCCO
        new TdfMemberInfo("SttDurationMsSent", "mSttDurationMsSent", 0xCE4B7300, TdfType.UInt32, 1, true), // Tag: SDMS
        new TdfMemberInfo("SttEventCount", "mSttEventCount", 0xCE58EF00, TdfType.UInt32, 2, true), // Tag: SECO
        new TdfMemberInfo("SttErrorCount", "mSttErrorCount", 0xCE5CA300, TdfType.UInt32, 3, true), // Tag: SERC
        new TdfMemberInfo("SttEmptyResultCount", "mSttEmptyResultCount", 0xCE5CB500, TdfType.UInt32, 4, true), // Tag: SERU
        new TdfMemberInfo("SttDelay", "mSttDelay", 0xCF2D3400, TdfType.UInt32, 5, true), // Tag: SRTT
        new TdfMemberInfo("TtsCharCountSent", "mTtsCharCountSent", 0xD238EF00, TdfType.UInt32, 6, true), // Tag: TCCO
        new TdfMemberInfo("TtsDurationMsRecv", "mTtsDurationMsRecv", 0xD24B7300, TdfType.UInt32, 7, true), // Tag: TDMS
        new TdfMemberInfo("TtsEventCount", "mTtsEventCount", 0xD258EF00, TdfType.UInt32, 8, true), // Tag: TECO
        new TdfMemberInfo("TtsErrorCount", "mTtsErrorCount", 0xD25CA300, TdfType.UInt32, 9, true), // Tag: TERC
        new TdfMemberInfo("TtsEmptyResultCount", "mTtsEmptyResultCount", 0xD25CB500, TdfType.UInt32, 10, true), // Tag: TERU
        new TdfMemberInfo("TtsDelay", "mTtsDelay", 0xD32D3400, TdfType.UInt32, 11, true), // Tag: TRTT
    ];
    private ITdfMember[] __members;

    private TdfUInt32 _sttCharCountRecv = new(__typeInfos[0]);
    private TdfUInt32 _sttDurationMsSent = new(__typeInfos[1]);
    private TdfUInt32 _sttEventCount = new(__typeInfos[2]);
    private TdfUInt32 _sttEmptyResultCount = new(__typeInfos[4]);
    private TdfUInt32 _sttErrorCount = new(__typeInfos[3]);
    private TdfUInt32 _sttDelay = new(__typeInfos[5]);
    private TdfUInt32 _ttsCharCountSent = new(__typeInfos[6]);
    private TdfUInt32 _ttsDurationMsRecv = new(__typeInfos[7]);
    private TdfUInt32 _ttsEventCount = new(__typeInfos[8]);
    private TdfUInt32 _ttsEmptyResultCount = new(__typeInfos[10]);
    private TdfUInt32 _ttsErrorCount = new(__typeInfos[9]);
    private TdfUInt32 _ttsDelay = new(__typeInfos[11]);

    public ClientUserMetrics()
    {
        __members = [
            _sttCharCountRecv,
            _sttDurationMsSent,
            _sttEventCount,
            _sttErrorCount,
            _sttEmptyResultCount,
            _sttDelay,
            _ttsCharCountSent,
            _ttsDurationMsRecv,
            _ttsEventCount,
            _ttsErrorCount,
            _ttsEmptyResultCount,
            _ttsDelay,
        ];
    }

    public override Tdf CreateNew() => new ClientUserMetrics();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ClientUserMetrics";
    public override string GetFullClassName() => "Blaze::ClientUserMetrics";

    public uint SttCharCountRecv
    {
        get => _sttCharCountRecv.Value;
        set => _sttCharCountRecv.Value = value;
    }

    public uint SttDurationMsSent
    {
        get => _sttDurationMsSent.Value;
        set => _sttDurationMsSent.Value = value;
    }

    public uint SttEventCount
    {
        get => _sttEventCount.Value;
        set => _sttEventCount.Value = value;
    }

    public uint SttEmptyResultCount
    {
        get => _sttEmptyResultCount.Value;
        set => _sttEmptyResultCount.Value = value;
    }

    public uint SttErrorCount
    {
        get => _sttErrorCount.Value;
        set => _sttErrorCount.Value = value;
    }

    public uint SttDelay
    {
        get => _sttDelay.Value;
        set => _sttDelay.Value = value;
    }

    public uint TtsCharCountSent
    {
        get => _ttsCharCountSent.Value;
        set => _ttsCharCountSent.Value = value;
    }

    public uint TtsDurationMsRecv
    {
        get => _ttsDurationMsRecv.Value;
        set => _ttsDurationMsRecv.Value = value;
    }

    public uint TtsEventCount
    {
        get => _ttsEventCount.Value;
        set => _ttsEventCount.Value = value;
    }

    public uint TtsEmptyResultCount
    {
        get => _ttsEmptyResultCount.Value;
        set => _ttsEmptyResultCount.Value = value;
    }

    public uint TtsErrorCount
    {
        get => _ttsErrorCount.Value;
        set => _ttsErrorCount.Value = value;
    }

    public uint TtsDelay
    {
        get => _ttsDelay.Value;
        set => _ttsDelay.Value = value;
    }

}
