using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Authentication;

public class OriginPersonaIdentifier : Union
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("BlazeId", "mBlazeId", 0x8B5A6400, TdfType.Int64, 0, true), // Tag: BUID
        new TdfMemberInfo("PersonaName", "mPersonaName", 0xC2E86D00, TdfType.String, 1, true), // Tag: PNAM
    ];
    private ITdfMember[] __members;

    private TdfInt64 _blazeId = new(__typeInfos[0]);
    private TdfString _personaName = new(__typeInfos[1]);

    public OriginPersonaIdentifier()
    {
        __members = [ _blazeId, _personaName ];
    }

    public override Tdf CreateNew() => new OriginPersonaIdentifier();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "OriginPersonaIdentifier";
    public override string GetFullClassName() => "Blaze::Authentication::OriginPersonaIdentifier";

    public override ITdfMember? GetActiveMember()
    {
        return ActiveIndex switch
        {
            0 => _blazeId,
            1 => _personaName,
            _ => null
        };
    }

    public long BlazeId
    {
        get => _blazeId.Value;
        set { SwitchActiveIndex(0); _blazeId.Value = value; }
    }

    public string PersonaName
    {
        get => _personaName.Value;
        set { SwitchActiveIndex(1); _personaName.Value = value; }
    }
}
