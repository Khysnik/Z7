using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class LookupUsersByPersonaNamesRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("PersonaNamespace", "mPersonaNamespace", 0xBA1CF000, TdfType.String, 0, true), // Tag: NASP
        new TdfMemberInfo("PersonaNameList", "mPersonaNameList", 0xC2CCF400, TdfType.List, 1, true), // Tag: PLST
    ];
    private ITdfMember[] __members;

    private TdfString _personaNamespace = new(__typeInfos[0]);
    private TdfList<string> _personaNameList = new(__typeInfos[1]);

    public LookupUsersByPersonaNamesRequest()
    {
        __members = [
            _personaNamespace,
            _personaNameList,
        ];
    }

    public override Tdf CreateNew() => new LookupUsersByPersonaNamesRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "LookupUsersByPersonaNamesRequest";
    public override string GetFullClassName() => "Blaze::LookupUsersByPersonaNamesRequest";

    public string PersonaNamespace
    {
        get => _personaNamespace.Value;
        set => _personaNamespace.Value = value;
    }

    public IList<string> PersonaNameList
    {
        get => _personaNameList.Value;
        set => _personaNameList.Value = value;
    }

}
