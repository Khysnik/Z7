using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Stats;

public class GetEntityRankResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("EntityRankMap", "mEntityRankMap", 0x972AED00, TdfType.Map, 0, true), // Tag: ERKM
    ];
    private ITdfMember[] __members;

    private TdfMap<ulong, int> _entityRankMap = new(__typeInfos[0]);

    public GetEntityRankResponse()
    {
        __members = [ _entityRankMap ];
    }

    public override Tdf CreateNew() => new GetEntityRankResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetEntityRankResponse";
    public override string GetFullClassName() => "Blaze::Stats::GetEntityRankResponse";

    public IDictionary<ulong, int> EntityRankMap
    {
        get => _entityRankMap.Value;
        set => _entityRankMap.Value = value;
    }

}
