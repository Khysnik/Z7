using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class RateLimitInfo : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Context", "mContext", 0x8EED3800, TdfType.String, 0, true), // Tag: CNTX
        new TdfMemberInfo("RateLimitLimit", "mRateLimitLimit", 0xCB4B2900, TdfType.UInt16, 1, true), // Tag: RTLI
        new TdfMemberInfo("RateLimitNextAvailable", "mRateLimitNextAvailable", 0xCB4BA100, TdfType.Float, 2, true), // Tag: RTNA
        new TdfMemberInfo("RateLimitRemaining", "mRateLimitRemaining", 0xCB4CA500, TdfType.UInt16, 3, true), // Tag: RTRE
        new TdfMemberInfo("RateLimitTimePeriod", "mRateLimitTimePeriod", 0xCB4D3000, TdfType.String, 4, true), // Tag: RTTP
    ];
    private ITdfMember[] __members;

    private TdfFloat _rateLimitNextAvailable = new(__typeInfos[2]);
    private TdfString _rateLimitTimePeriod = new(__typeInfos[4]);
    private TdfUInt16 _rateLimitLimit = new(__typeInfos[1]);
    private TdfUInt16 _rateLimitRemaining = new(__typeInfos[3]);
    private TdfString _context = new(__typeInfos[0]);

    public RateLimitInfo()
    {
        __members = [
            _context,
            _rateLimitLimit,
            _rateLimitNextAvailable,
            _rateLimitRemaining,
            _rateLimitTimePeriod,
        ];
    }

    public override Tdf CreateNew() => new RateLimitInfo();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "RateLimitInfo";
    public override string GetFullClassName() => "Blaze::RateLimitInfo";

    public float RateLimitNextAvailable
    {
        get => _rateLimitNextAvailable.Value;
        set => _rateLimitNextAvailable.Value = value;
    }

    public string RateLimitTimePeriod
    {
        get => _rateLimitTimePeriod.Value;
        set => _rateLimitTimePeriod.Value = value;
    }

    public ushort RateLimitLimit
    {
        get => _rateLimitLimit.Value;
        set => _rateLimitLimit.Value = value;
    }

    public ushort RateLimitRemaining
    {
        get => _rateLimitRemaining.Value;
        set => _rateLimitRemaining.Value = value;
    }

    public string Context
    {
        get => _context.Value;
        set => _context.Value = value;
    }

}
