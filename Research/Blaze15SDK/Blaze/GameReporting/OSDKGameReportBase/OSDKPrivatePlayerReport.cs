using EATDF;
using EATDF.Members;

namespace Blaze15SDK.Blaze.GameReporting.OSDKGameReportBase;

public class OSDKPrivatePlayerReport : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("PrivateIntAttributeMap", "privateIntAttributeMap", 0xC2986D00, TdfType.Map, 0, true), // Tag: PIAM
        new TdfMemberInfo("PrivateAttributeMap", "privateAttributeMap", 0xC3086D00, TdfType.Map, 1, true), // Tag: PPAM
    ];
    private ITdfMember[] __members;

    private TdfMap<string, long> _privateIntAttributeMap = new(__typeInfos[0]);
    private TdfMap<string, string> _privateAttributeMap = new(__typeInfos[1]);

    public OSDKPrivatePlayerReport()
    {
        __members = [_privateIntAttributeMap, _privateAttributeMap];
    }

    public override Tdf CreateNew() => new OSDKPrivatePlayerReport();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "OSDKPrivatePlayerReport";
    public override string GetFullClassName() => "Blaze::GameReporting::OSDKGameReportBase::OSDKPrivatePlayerReport";

    public IDictionary<string, long> PrivateIntAttributeMap
    {
        get => _privateIntAttributeMap.Value;
        set => _privateIntAttributeMap.Value = value;
    }

    public IDictionary<string, string> PrivateAttributeMap
    {
        get => _privateAttributeMap.Value;
        set => _privateAttributeMap.Value = value;
    }
}
