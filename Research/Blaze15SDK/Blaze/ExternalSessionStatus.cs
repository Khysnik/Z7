using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class ExternalSessionStatus : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("LangMap", "mLangMap", 0xB2F8EC00, TdfType.Map, 0, true), // Tag: LOCL
        new TdfMemberInfo("UnlocalizedValue", "mUnlocalizedValue", 0xD6EB2F00, TdfType.String, 1, true), // Tag: UNLO
    ];
    private ITdfMember[] __members;

    private TdfString _unlocalizedValue = new(__typeInfos[1]);
    private TdfMap<string, string> _langMap = new(__typeInfos[1]);

    public ExternalSessionStatus()
    {
        __members = [
            _langMap,
            _unlocalizedValue,
        ];
    }

    public override Tdf CreateNew() => new ExternalSessionStatus();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ExternalSessionStatus";
    public override string GetFullClassName() => "Blaze::ExternalSessionStatus";

    public string UnlocalizedValue
    {
        get => _unlocalizedValue.Value;
        set => _unlocalizedValue.Value = value;
    }

    public IDictionary<string, string> LangMap
    {
        get => _langMap.Value;
        set => _langMap.Value = value;
    }

}
