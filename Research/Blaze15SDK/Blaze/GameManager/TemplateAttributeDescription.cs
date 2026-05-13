using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class TemplateAttributeDescription : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Default", "mDefault", 0x92598000, TdfType.Variable, 0, true), // Tag: DEF
        new TdfMemberInfo("IsOptional", "mIsOptional", 0xA6FC3400, TdfType.Bool, 1, true), // Tag: IOPT
        new TdfMemberInfo("AttrTdfId", "mAttrTdfId", 0xDA1C8000, TdfType.UInt32, 2, true), // Tag: VAR
    ];
    private ITdfMember[] __members;

    private TdfVariable _default = new(__typeInfos[0]);
    private TdfBool _isOptional = new(__typeInfos[1]);
    private TdfUInt32 _attrTdfId = new(__typeInfos[2]);

    public TemplateAttributeDescription()
    {
        __members = [ _default, _isOptional, _attrTdfId ];
    }

    public override Tdf CreateNew() => new TemplateAttributeDescription();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "TemplateAttributeDescription";
    public override string GetFullClassName() => "Blaze::GameManager::TemplateAttributeDescription";

    public object? Default
    {
        get => _default.Value;
        set => _default.Value = value;
    }

    public bool IsOptional
    {
        get => _isOptional.Value;
        set => _isOptional.Value = value;
    }

    public uint AttrTdfId
    {
        get => _attrTdfId.Value;
        set => _attrTdfId.Value = value;
    }

}
