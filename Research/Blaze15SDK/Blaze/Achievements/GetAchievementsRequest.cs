using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Achievements;

public class GetAchievementsRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("AuxAuth", "mAuxAuth", 0x875E2100, TdfType.Struct, 0, true), // Tag: AUXA
        new TdfMemberInfo("Filtered", "mFiltered", 0x9A9B3400, TdfType.Bool, 1, true), // Tag: FILT
        new TdfMemberInfo("Language", "mLanguage", 0xB21BA700, TdfType.String, 2, true), // Tag: LANG
        new TdfMemberInfo("Limit", "mLimit", 0xB29B6900, TdfType.Enum, 3, true), // Tag: LIMI
        new TdfMemberInfo("IncludeMetadata", "mIncludeMetadata", 0xB65D2100, TdfType.Bool, 4, true), // Tag: META
        new TdfMemberInfo("ProductId", "mProductId", 0xC32BE400, TdfType.String, 5, true), // Tag: PROD
        new TdfMemberInfo("User", "mUser", 0xD7397200, TdfType.Struct, 6, true), // Tag: USER
    ];
    private ITdfMember[] __members;

    private TdfStruct<AuxiliaryAuthentication?> _auxAuth = new(__typeInfos[0]);
    private TdfBool _filtered = new(__typeInfos[1]);
    private TdfString _language = new(__typeInfos[2]);
    private TdfEnum<FilterType> _limit = new(__typeInfos[3]);
    private TdfBool _includeMetadata = new(__typeInfos[4]);
    private TdfString _productId = new(__typeInfos[5]);
    private TdfStruct<User?> _user = new(__typeInfos[6]);

    public GetAchievementsRequest()
    {
        __members = [ _auxAuth, _filtered, _language, _limit, _includeMetadata, _productId, _user ];
    }

    public override Tdf CreateNew() => new GetAchievementsRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetAchievementsRequest";
    public override string GetFullClassName() => "Blaze::Achievements::GetAchievementsRequest";

    public AuxiliaryAuthentication? AuxAuth
    {
        get => _auxAuth.Value;
        set => _auxAuth.Value = value;
    }

    public bool Filtered
    {
        get => _filtered.Value;
        set => _filtered.Value = value;
    }

    public string Language
    {
        get => _language.Value;
        set => _language.Value = value;
    }

    public FilterType Limit
    {
        get => _limit.Value;
        set => _limit.Value = value;
    }

    public bool IncludeMetadata
    {
        get => _includeMetadata.Value;
        set => _includeMetadata.Value = value;
    }

    public string ProductId
    {
        get => _productId.Value;
        set => _productId.Value = value;
    }

    public User? User
    {
        get => _user.Value;
        set => _user.Value = value;
    }

}
