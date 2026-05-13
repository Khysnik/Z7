using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class ServiceNameInfo : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Platform", "platform", 0xC2C87400, TdfType.Enum, 0, true), // Tag: PLAT
        new TdfMemberInfo("ProductName", "mProductName", 0xC3293400, TdfType.String, 1, true), // Tag: PRDT
        new TdfMemberInfo("ReleaseType", "releaseType", 0xCA5B3400, TdfType.String, 2, true), // Tag: RELT
        new TdfMemberInfo("BlazeServerClientId", "mBlazeServerClientId", 0xCF2A6400, TdfType.String, 3, true), // Tag: SRID
    ];
    private ITdfMember[] __members;

    private TdfEnum<Blaze15SDK.Blaze.ClientPlatformType> _platform = new(__typeInfos[0]);
    private TdfString _productName = new(__typeInfos[1]);
    private TdfString _releaseType = new(__typeInfos[2]);
    private TdfString _blazeServerClientId = new(__typeInfos[3]);

    public ServiceNameInfo()
    {
        __members = [
            _platform,
            _productName,
            _releaseType,
            _blazeServerClientId,
        ];
    }

    public override Tdf CreateNew() => new ServiceNameInfo();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ServiceNameInfo";
    public override string GetFullClassName() => "Blaze::ServiceNameInfo";

    public Blaze15SDK.Blaze.ClientPlatformType Platform
    {
        get => _platform.Value;
        set => _platform.Value = value;
    }

    public string ProductName
    {
        get => _productName.Value;
        set => _productName.Value = value;
    }

    public string ReleaseType
    {
        get => _releaseType.Value;
        set => _releaseType.Value = value;
    }

    public string BlazeServerClientId
    {
        get => _blazeServerClientId.Value;
        set => _blazeServerClientId.Value = value;
    }

}
