using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class TemplateAttributeDefinition : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ClientDefined", "mClientDefined", 0x8E496600, TdfType.Bool, 0, true), // Tag: CDEF
        new TdfMemberInfo("Default", "mDefault", 0x92598000, TdfType.Variable, 1, true), // Tag: DEF
        new TdfMemberInfo("DebugOnly", "mDebugOnly", 0x9F38E300, TdfType.Bool, 2, true), // Tag: GSCC
        new TdfMemberInfo("IsOptional", "mIsOptional", 0xA6FC3400, TdfType.Bool, 3, true), // Tag: IOPT
        new TdfMemberInfo("AttrName", "mAttrName", 0xDA1C8000, TdfType.String, 4, true), // Tag: VAR
    ];
    private ITdfMember[] __members;

    private TdfBool _clientDefined = new(__typeInfos[0]);
    private TdfVariable _default = new(__typeInfos[1]);
    private TdfBool _debugOnly = new(__typeInfos[2]);
    private TdfBool _isOptional = new(__typeInfos[3]);
    private TdfString _attrName = new(__typeInfos[4]);

    public TemplateAttributeDefinition()
    {
        __members = [ _clientDefined, _default, _debugOnly, _isOptional, _attrName ];
    }

    public override Tdf CreateNew() => new TemplateAttributeDefinition();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "TemplateAttributeDefinition";
    public override string GetFullClassName() => "Blaze::GameManager::TemplateAttributeDefinition";

    public bool ClientDefined
    {
        get => _clientDefined.Value;
        set => _clientDefined.Value = value;
    }

    public object? Default
    {
        get => _default.Value;
        set => _default.Value = value;
    }

    public bool DebugOnly
    {
        get => _debugOnly.Value;
        set => _debugOnly.Value = value;
    }

    public bool IsOptional
    {
        get => _isOptional.Value;
        set => _isOptional.Value = value;
    }

    public string AttrName
    {
        get => _attrName.Value;
        set => _attrName.Value = value;
    }

}
