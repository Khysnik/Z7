using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Achievements;

public class GrantAchievementRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("AchieveId", "mAchieveId", 0x868A6400, TdfType.String, 0, true), // Tag: AHID
        new TdfMemberInfo("AuxAuth", "mAuxAuth", 0x875E2100, TdfType.Struct, 1, true), // Tag: AUXA
        new TdfMemberInfo("Language", "mLanguage", 0xB21BA700, TdfType.String, 2, true), // Tag: LANG
        new TdfMemberInfo("IncludeMetadata", "mIncludeMetadata", 0xB65D2100, TdfType.Bool, 3, true), // Tag: META
        new TdfMemberInfo("ProductId", "mProductId", 0xC32BE400, TdfType.String, 4, true), // Tag: PROD
        new TdfMemberInfo("Progress", "mProgress", 0xC32BE700, TdfType.Struct, 5, true), // Tag: PROG
        new TdfMemberInfo("User", "mUser", 0xD7397200, TdfType.Struct, 6, true), // Tag: USER
    ];
    private ITdfMember[] __members;

    private TdfString _achieveId = new(__typeInfos[0]);
    private TdfStruct<AuxiliaryAuthentication?> _auxAuth = new(__typeInfos[1]);
    private TdfString _language = new(__typeInfos[2]);
    private TdfBool _includeMetadata = new(__typeInfos[3]);
    private TdfString _productId = new(__typeInfos[4]);
    private TdfStruct<ProgressUpdate?> _progress = new(__typeInfos[5]);
    private TdfStruct<User?> _user = new(__typeInfos[6]);

    public GrantAchievementRequest()
    {
        __members = [ _achieveId, _auxAuth, _language, _includeMetadata, _productId, _progress, _user ];
    }

    public override Tdf CreateNew() => new GrantAchievementRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GrantAchievementRequest";
    public override string GetFullClassName() => "Blaze::Achievements::GrantAchievementRequest";

    public string AchieveId
    {
        get => _achieveId.Value;
        set => _achieveId.Value = value;
    }

    public AuxiliaryAuthentication? AuxAuth
    {
        get => _auxAuth.Value;
        set => _auxAuth.Value = value;
    }

    public string Language
    {
        get => _language.Value;
        set => _language.Value = value;
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

    public ProgressUpdate? Progress
    {
        get => _progress.Value;
        set => _progress.Value = value;
    }

    public User? User
    {
        get => _user.Value;
        set => _user.Value = value;
    }

}
