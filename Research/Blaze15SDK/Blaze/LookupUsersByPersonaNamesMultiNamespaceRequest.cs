using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class LookupUsersByPersonaNamesMultiNamespaceRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("PersonaNameList", "mPersonaNameList", 0xC2CCF400, TdfType.List, 0, true), // Tag: PLST
        new TdfMemberInfo("PrimaryNamespaceOnly", "mPrimaryNamespaceOnly", 0xC2FBAC00, TdfType.Bool, 1, true), // Tag: PONL
    ];
    private ITdfMember[] __members;

    private TdfList<string> _personaNameList = new(__typeInfos[0]);
    private TdfBool _primaryNamespaceOnly = new(__typeInfos[1]);

    public LookupUsersByPersonaNamesMultiNamespaceRequest()
    {
        __members = [
            _personaNameList,
            _primaryNamespaceOnly,
        ];
    }

    public override Tdf CreateNew() => new LookupUsersByPersonaNamesMultiNamespaceRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "LookupUsersByPersonaNamesMultiNamespaceRequest";
    public override string GetFullClassName() => "Blaze::LookupUsersByPersonaNamesMultiNamespaceRequest";

    public IList<string> PersonaNameList
    {
        get => _personaNameList.Value;
        set => _personaNameList.Value = value;
    }

    public bool PrimaryNamespaceOnly
    {
        get => _primaryNamespaceOnly.Value;
        set => _primaryNamespaceOnly.Value = value;
    }

}
