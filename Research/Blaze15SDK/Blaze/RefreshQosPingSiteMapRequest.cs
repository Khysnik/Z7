using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class RefreshQosPingSiteMapRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("PingSiteAliasMap", "mPingSiteAliasMap", 0xC3386D00, TdfType.Map, 0, true), // Tag: PSAM
    ];
    private ITdfMember[] __members;

    private TdfMap<string, Blaze15SDK.Blaze.QosPingSiteInfo> _pingSiteAliasMap = new(__typeInfos[0]);

    public RefreshQosPingSiteMapRequest()
    {
        __members = [
            _pingSiteAliasMap,
        ];
    }

    public override Tdf CreateNew() => new RefreshQosPingSiteMapRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "RefreshQosPingSiteMapRequest";
    public override string GetFullClassName() => "Blaze::RefreshQosPingSiteMapRequest";

    public IDictionary<string, Blaze15SDK.Blaze.QosPingSiteInfo> PingSiteAliasMap
    {
        get => _pingSiteAliasMap.Value;
        set => _pingSiteAliasMap.Value = value;
    }

}
