using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.ByteVault;

public class UpsertCategoryResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Category", "mCategory", 0x8E1D0000, TdfType.Struct, 0, true), // Tag: CAT
        new TdfMemberInfo("ResourceCreated", "mResourceCreated", 0xCA3CA500, TdfType.Bool, 1, true), // Tag: RCRE
    ];
    private ITdfMember[] __members;

    private TdfStruct<Blaze15SDK.Blaze.ByteVault.Category?> _category = new(__typeInfos[0]);
    private TdfBool _resourceCreated = new(__typeInfos[1]);

    public UpsertCategoryResponse()
    {
        __members = [ _category, _resourceCreated ];
    }

    public override Tdf CreateNew() => new UpsertCategoryResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "UpsertCategoryResponse";
    public override string GetFullClassName() => "Blaze::ByteVault::UpsertCategoryResponse";

    public Blaze15SDK.Blaze.ByteVault.Category? Category
    {
        get => _category.Value;
        set => _category.Value = value;
    }

    public bool ResourceCreated
    {
        get => _resourceCreated.Value;
        set => _resourceCreated.Value = value;
    }

}
