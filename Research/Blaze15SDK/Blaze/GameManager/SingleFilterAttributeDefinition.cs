using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class SingleFilterAttributeDefinition : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("AttrName", "mAttrName", 0x874D3200, TdfType.String, 0, true), // Tag: ATTR
        new TdfMemberInfo("Default", "mDefault", 0x92598000, TdfType.String, 1, true), // Tag: DEF
    ];
    private ITdfMember[] __members;

    private TdfString _attrName = new(__typeInfos[0]);
    private TdfString _default = new(__typeInfos[1]);

    public SingleFilterAttributeDefinition()
    {
        __members = [ _attrName, _default ];
    }

    public override Tdf CreateNew() => new SingleFilterAttributeDefinition();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "SingleFilterAttributeDefinition";
    public override string GetFullClassName() => "Blaze::GameManager::SingleFilterAttributeDefinition";

    public string AttrName
    {
        get => _attrName.Value;
        set => _attrName.Value = value;
    }

    public string Default
    {
        get => _default.Value;
        set => _default.Value = value;
    }

}
