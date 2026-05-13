using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class FilterAttributeDefinition : Union
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("MultipleFilterAttributeDefinition", "mMultipleFilterAttributeDefinition", 0xB75B3400, TdfType.Struct, 0, true), // Tag: MULT
        new TdfMemberInfo("SingleFilterAttributeDefinition", "mSingleFilterAttributeDefinition", 0xCE9BA700, TdfType.Struct, 1, true), // Tag: SING
    ];
    private ITdfMember[] __members;

    private TdfStruct<Blaze15SDK.Blaze.GameManager.MultipleFilterAttributeDefinition?> _multipleFilterAttributeDefinition = new(__typeInfos[0]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.SingleFilterAttributeDefinition?> _singleFilterAttributeDefinition = new(__typeInfos[1]);

    public FilterAttributeDefinition()
    {
        __members = [ _multipleFilterAttributeDefinition, _singleFilterAttributeDefinition ];
    }

    public override Tdf CreateNew() => new FilterAttributeDefinition();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "FilterAttributeDefinition";
    public override string GetFullClassName() => "Blaze::GameManager::FilterAttributeDefinition";

    public override ITdfMember? GetActiveMember()
    {
        return ActiveIndex switch
        {
            0 => _multipleFilterAttributeDefinition,
            1 => _singleFilterAttributeDefinition,
            _ => null
        };
    }

    public Blaze15SDK.Blaze.GameManager.MultipleFilterAttributeDefinition? MultipleFilterAttributeDefinition
    {
        get => _multipleFilterAttributeDefinition.Value;
        set { SwitchActiveIndex(0); _multipleFilterAttributeDefinition.Value = value; }
    }

    public Blaze15SDK.Blaze.GameManager.SingleFilterAttributeDefinition? SingleFilterAttributeDefinition
    {
        get => _singleFilterAttributeDefinition.Value;
        set { SwitchActiveIndex(1); _singleFilterAttributeDefinition.Value = value; }
    }
}
