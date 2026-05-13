using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class MultipleFilterAttributeDefinition : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("AttrName", "mAttrName", 0x874D3200, TdfType.String, 0, true), // Tag: ATTR
        new TdfMemberInfo("Default", "mDefault", 0x92598000, TdfType.List, 1, true), // Tag: DEF
    ];
    private ITdfMember[] __members;

    private TdfString _attrName = new(__typeInfos[0]);
    private TdfList<string> _default = new(__typeInfos[1]);

    public MultipleFilterAttributeDefinition()
    {
        __members = [ _attrName, _default ];
    }

    public override Tdf CreateNew() => new MultipleFilterAttributeDefinition();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "MultipleFilterAttributeDefinition";
    public override string GetFullClassName() => "Blaze::GameManager::MultipleFilterAttributeDefinition";

    public string AttrName
    {
        get => _attrName.Value;
        set => _attrName.Value = value;
    }

    public IList<string> Default
    {
        get => _default.Value;
        set => _default.Value = value;
    }

}
