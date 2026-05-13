using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Stats;

public class StatCategorySummary : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("CategoryType", "mCategoryType", 0x8F4E7000, TdfType.Enum, 0, true), // Tag: CTYP
        new TdfMemberInfo("Desc", "mDesc", 0x925CE300, TdfType.String, 1, true), // Tag: DESC
        new TdfMemberInfo("EntityType", "mEntityType", 0x974E7000, TdfType.ObjectType, 2, true), // Tag: ETYP
        new TdfMemberInfo("KeyScopes", "mKeyScopes", 0xAE5E7300, TdfType.List, 3, true), // Tag: KEYS
        new TdfMemberInfo("Name", "mName", 0xBA1B6500, TdfType.String, 4, true), // Tag: NAME
        new TdfMemberInfo("PeriodTypes", "mPeriodTypes", 0xC34E7000, TdfType.List, 5, true), // Tag: PTYP
    ];
    private ITdfMember[] __members;

    private TdfEnum<Blaze15SDK.Blaze.Stats.CategoryType> _categoryType = new(__typeInfos[0]);
    private TdfString _desc = new(__typeInfos[1]);
    private TdfObjectType _entityType = new(__typeInfos[2]);
    private TdfList<string> _keyScopes = new(__typeInfos[3]);
    private TdfString _name = new(__typeInfos[4]);
    private TdfList<int> _periodTypes = new(__typeInfos[5]);

    public StatCategorySummary()
    {
        __members = [ _categoryType, _desc, _entityType, _keyScopes, _name, _periodTypes ];
    }

    public override Tdf CreateNew() => new StatCategorySummary();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "StatCategorySummary";
    public override string GetFullClassName() => "Blaze::Stats::StatCategorySummary";

    public Blaze15SDK.Blaze.Stats.CategoryType CategoryType
    {
        get => _categoryType.Value;
        set => _categoryType.Value = value;
    }

    public string Desc
    {
        get => _desc.Value;
        set => _desc.Value = value;
    }

    public ObjectType EntityType
    {
        get => _entityType.Value;
        set => _entityType.Value = value;
    }

    public IList<string> KeyScopes
    {
        get => _keyScopes.Value;
        set => _keyScopes.Value = value;
    }

    public string Name
    {
        get => _name.Value;
        set => _name.Value = value;
    }

    public IList<int> PeriodTypes
    {
        get => _periodTypes.Value;
        set => _periodTypes.Value = value;
    }

}
