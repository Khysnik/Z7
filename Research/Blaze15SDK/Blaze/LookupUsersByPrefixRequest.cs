using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class LookupUsersByPrefixRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("MaxResultCount", "mMaxResultCount", 0x8E1C0000, TdfType.UInt32, 0, true), // Tag: CAP
        new TdfMemberInfo("PersonaNamespace", "mPersonaNamespace", 0xBA1CF000, TdfType.String, 1, true), // Tag: NASP
        new TdfMemberInfo("PrimaryNamespaceOnly", "mPrimaryNamespaceOnly", 0xC2FBAC00, TdfType.Bool, 2, true), // Tag: PONL
        new TdfMemberInfo("PrefixName", "mPrefixName", 0xC3296600, TdfType.String, 3, true), // Tag: PREF
    ];
    private ITdfMember[] __members;

    private TdfUInt32 _maxResultCount = new(__typeInfos[0]);
    private TdfString _personaNamespace = new(__typeInfos[1]);
    private TdfBool _primaryNamespaceOnly = new(__typeInfos[2]);
    private TdfString _prefixName = new(__typeInfos[3]);

    public LookupUsersByPrefixRequest()
    {
        __members = [
            _maxResultCount,
            _personaNamespace,
            _primaryNamespaceOnly,
            _prefixName,
        ];
    }

    public override Tdf CreateNew() => new LookupUsersByPrefixRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "LookupUsersByPrefixRequest";
    public override string GetFullClassName() => "Blaze::LookupUsersByPrefixRequest";

    public uint MaxResultCount
    {
        get => _maxResultCount.Value;
        set => _maxResultCount.Value = value;
    }

    public string PersonaNamespace
    {
        get => _personaNamespace.Value;
        set => _personaNamespace.Value = value;
    }

    public bool PrimaryNamespaceOnly
    {
        get => _primaryNamespaceOnly.Value;
        set => _primaryNamespaceOnly.Value = value;
    }

    public string PrefixName
    {
        get => _prefixName.Value;
        set => _prefixName.Value = value;
    }

}
