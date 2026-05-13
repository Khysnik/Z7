using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class GetTemplatesAndAttributesResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("TemplateAttributeDescriptions", "mTemplateAttributeDescriptions", 0xB73A6400, TdfType.Map, 0, true), // Tag: MSID
    ];
    private ITdfMember[] __members;

    private TdfMap<string, IDictionary<string, Blaze15SDK.Blaze.GameManager.TemplateAttributeDescription>> _templateAttributeDescriptions = new(__typeInfos[0]);

    public GetTemplatesAndAttributesResponse()
    {
        __members = [ _templateAttributeDescriptions ];
    }

    public override Tdf CreateNew() => new GetTemplatesAndAttributesResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetTemplatesAndAttributesResponse";
    public override string GetFullClassName() => "Blaze::GameManager::GetTemplatesAndAttributesResponse";

    public IDictionary<string, IDictionary<string, Blaze15SDK.Blaze.GameManager.TemplateAttributeDescription>> TemplateAttributeDescriptions
    {
        get => _templateAttributeDescriptions.Value;
        set => _templateAttributeDescriptions.Value = value;
    }

}
