using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class LookupUsersByPersonaNameMultiNamespaceRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("PrimaryNamespaceOnly", "mPrimaryNamespaceOnly", 0xC2FBAC00, TdfType.Bool, 0, true), // Tag: PONL
        new TdfMemberInfo("PersonaName", "mPersonaName", 0xC3296600, TdfType.String, 1, true), // Tag: PREF
    ];
    private ITdfMember[] __members;

    private TdfBool _primaryNamespaceOnly = new(__typeInfos[0]);
    private TdfString _personaName = new(__typeInfos[1]);

    public LookupUsersByPersonaNameMultiNamespaceRequest()
    {
        __members = [
            _primaryNamespaceOnly,
            _personaName,
        ];
    }

    public override Tdf CreateNew() => new LookupUsersByPersonaNameMultiNamespaceRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "LookupUsersByPersonaNameMultiNamespaceRequest";
    public override string GetFullClassName() => "Blaze::LookupUsersByPersonaNameMultiNamespaceRequest";

    public bool PrimaryNamespaceOnly
    {
        get => _primaryNamespaceOnly.Value;
        set => _primaryNamespaceOnly.Value = value;
    }

    public string PersonaName
    {
        get => _personaName.Value;
        set => _personaName.Value = value;
    }

}
