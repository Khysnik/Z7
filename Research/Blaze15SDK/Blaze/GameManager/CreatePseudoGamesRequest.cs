using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class CreatePseudoGamesRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("PseudoGameVariantCountMap", "mPseudoGameVariantCountMap", 0xC27DAD00, TdfType.Map, 0, true), // Tag: PGVM
    ];
    private ITdfMember[] __members;

    private TdfMap<string, uint> _pseudoGameVariantCountMap = new(__typeInfos[0]);

    public CreatePseudoGamesRequest()
    {
        __members = [ _pseudoGameVariantCountMap ];
    }

    public override Tdf CreateNew() => new CreatePseudoGamesRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "CreatePseudoGamesRequest";
    public override string GetFullClassName() => "Blaze::GameManager::CreatePseudoGamesRequest";

    public IDictionary<string, uint> PseudoGameVariantCountMap
    {
        get => _pseudoGameVariantCountMap.Value;
        set => _pseudoGameVariantCountMap.Value = value;
    }

}
