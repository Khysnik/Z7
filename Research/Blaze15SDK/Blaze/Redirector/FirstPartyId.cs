using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Redirector;

public class FirstPartyId : Union
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("PS3Ticket", "mPS3Ticket", 0xDA1B3500, TdfType.Blob, 0, true), // Tag: VALU
        new TdfMemberInfo("XboxId", "mXboxId", 0xDA1B3500, TdfType.Struct, 1, true), // Tag: VALU
        new TdfMemberInfo("Persona", "mPersona", 0xDA1B3500, TdfType.String, 2, true), // Tag: VALU
    ];
    private ITdfMember[] __members;

    private TdfBlob _pS3Ticket = new(__typeInfos[0]);
    private TdfStruct<Blaze15SDK.Blaze.Redirector.XboxId?> _xboxId = new(__typeInfos[1]);
    private TdfString _persona = new(__typeInfos[2]);

    public FirstPartyId()
    {
        __members = [ _pS3Ticket, _xboxId, _persona ];
    }

    public override Tdf CreateNew() => new FirstPartyId();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "FirstPartyId";
    public override string GetFullClassName() => "Blaze::Redirector::FirstPartyId";

    public override ITdfMember? GetActiveMember()
    {
        return ActiveIndex switch
        {
            0 => _pS3Ticket,
            1 => _xboxId,
            2 => _persona,
            _ => null
        };
    }

    public byte[] PS3Ticket
    {
        get => _pS3Ticket.Value;
        set { SwitchActiveIndex(0); _pS3Ticket.Value = value; }
    }

    public Blaze15SDK.Blaze.Redirector.XboxId? XboxId
    {
        get => _xboxId.Value;
        set { SwitchActiveIndex(1); _xboxId.Value = value; }
    }

    public string Persona
    {
        get => _persona.Value;
        set { SwitchActiveIndex(2); _persona.Value = value; }
    }

}
