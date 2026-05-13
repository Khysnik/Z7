using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class ExternalSessionActivityTypeInfo : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Platform", "mPlatform", 0xC2C87400, TdfType.Enum, 0, true), // Tag: PLAT
        new TdfMemberInfo("Type", "mType", 0xD39C2500, TdfType.Enum, 1, true), // Tag: TYPE
    ];
    private ITdfMember[] __members;

    private TdfEnum<Blaze15SDK.Blaze.ClientPlatformType> _platform = new(__typeInfos[0]);
    private TdfEnum<Blaze15SDK.Blaze.ExternalSessionActivityType> _type = new(__typeInfos[1]);

    public ExternalSessionActivityTypeInfo()
    {
        __members = [
            _platform,
            _type,
        ];
    }

    public override Tdf CreateNew() => new ExternalSessionActivityTypeInfo();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ExternalSessionActivityTypeInfo";
    public override string GetFullClassName() => "Blaze::ExternalSessionActivityTypeInfo";

    public Blaze15SDK.Blaze.ClientPlatformType Platform
    {
        get => _platform.Value;
        set => _platform.Value = value;
    }

    public Blaze15SDK.Blaze.ExternalSessionActivityType Type
    {
        get => _type.Value;
        set => _type.Value = value;
    }

}
