using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class GetTemplatesAttributesResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("TemplateAttributes", "mTemplateAttributes", 0xB73A6400, TdfType.Map, 0, true), // Tag: MSID
    ];
    private ITdfMember[] __members;

    private TdfMap<string, IDictionary<string, Blaze15SDK.Blaze.GameManager.TemplateAttributeDefinition>> _templateAttributes = new(__typeInfos[0]);

    public GetTemplatesAttributesResponse()
    {
        __members = [ _templateAttributes ];
    }

    public override Tdf CreateNew() => new GetTemplatesAttributesResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetTemplatesAttributesResponse";
    public override string GetFullClassName() => "Blaze::GameManager::GetTemplatesAttributesResponse";

    public IDictionary<string, IDictionary<string, Blaze15SDK.Blaze.GameManager.TemplateAttributeDefinition>> TemplateAttributes
    {
        get => _templateAttributes.Value;
        set => _templateAttributes.Value = value;
    }

}
