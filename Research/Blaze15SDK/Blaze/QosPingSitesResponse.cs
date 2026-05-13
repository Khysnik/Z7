using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class QosPingSitesResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("PingSiteInfoByAliasMap", "mPingSiteInfoByAliasMap", 0xB34C3300, TdfType.Map, 0, true), // Tag: LTPS
        new TdfMemberInfo("Profile", "mProfile", 0xC70C8000, TdfType.String, 1, true), // Tag: QPR
    ];
    private ITdfMember[] __members;

    private TdfMap<string, Blaze15SDK.Blaze.QosPingSiteInfo> _pingSiteInfoByAliasMap = new(__typeInfos[0]);
    private TdfString _profile = new(__typeInfos[1]);

    public QosPingSitesResponse()
    {
        __members = [
            _pingSiteInfoByAliasMap,
            _profile,
        ];
    }

    public override Tdf CreateNew() => new QosPingSitesResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "QosPingSitesResponse";
    public override string GetFullClassName() => "Blaze::QosPingSitesResponse";

    public IDictionary<string, Blaze15SDK.Blaze.QosPingSiteInfo> PingSiteInfoByAliasMap
    {
        get => _pingSiteInfoByAliasMap.Value;
        set => _pingSiteInfoByAliasMap.Value = value;
    }

    public string Profile
    {
        get => _profile.Value;
        set => _profile.Value = value;
    }

}
