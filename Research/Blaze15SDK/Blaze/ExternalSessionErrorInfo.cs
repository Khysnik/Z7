using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class ExternalSessionErrorInfo : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Code", "mCode", 0x8EF92500, TdfType.Int32, 0, true), // Tag: CODE
        new TdfMemberInfo("Message", "mMessage", 0xB65CE700, TdfType.String, 1, true), // Tag: MESG
        new TdfMemberInfo("RateLimitInfo", "mRateLimitInfo", 0xCA1D2500, TdfType.Struct, 2, true), // Tag: RATE
    ];
    private ITdfMember[] __members;

    private TdfStruct<Blaze15SDK.Blaze.RateLimitInfo?> _rateLimitInfo = new(__typeInfos[2]);
    private TdfInt32 _code = new(__typeInfos[0]);
    private TdfString _message = new(__typeInfos[1]);

    public ExternalSessionErrorInfo()
    {
        __members = [
            _code,
            _message,
            _rateLimitInfo,
        ];
    }

    public override Tdf CreateNew() => new ExternalSessionErrorInfo();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ExternalSessionErrorInfo";
    public override string GetFullClassName() => "Blaze::ExternalSessionErrorInfo";

    public Blaze15SDK.Blaze.RateLimitInfo? RateLimitInfo
    {
        get => _rateLimitInfo.Value;
        set => _rateLimitInfo.Value = value;
    }

    public int Code
    {
        get => _code.Value;
        set => _code.Value = value;
    }

    public string Message
    {
        get => _message.Value;
        set => _message.Value = value;
    }

}
