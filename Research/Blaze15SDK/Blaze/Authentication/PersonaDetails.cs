using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Authentication;

public class PersonaDetails : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("DisplayName", "mDisplayName", 0x933BAD00, TdfType.String, 0, true), // Tag: DSNM
        new TdfMemberInfo("LastAuthenticated", "mLastAuthenticated", 0xB21CF400, TdfType.UInt32, 1, true), // Tag: LAST
        new TdfMemberInfo("PersonaId", "mPersonaId", 0xC2990000, TdfType.Int64, 2, true), // Tag: PID
        new TdfMemberInfo("Status", "mStatus", 0xCF487300, TdfType.Enum, 3, true), // Tag: STAS
        new TdfMemberInfo("ExtId", "mExtId", 0xE3296600, TdfType.Int64, 4, true), // Tag: XREF
    ];
    private ITdfMember[] __members;

    private TdfString _displayName = new(__typeInfos[0]);
    private TdfUInt32 _lastAuthenticated = new(__typeInfos[1]);
    private TdfInt64 _personaId = new(__typeInfos[2]);
    private TdfEnum<Blaze15SDK.Blaze.Authentication.PersonaStatus> _status = new(__typeInfos[3]);
    private TdfInt64 _extId = new(__typeInfos[4]);

    public PersonaDetails()
    {
        __members = [
            _displayName,
            _lastAuthenticated,
            _personaId,
            _status,
            _extId,
        ];
    }

    public override Tdf CreateNew() => new PersonaDetails();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "PersonaDetails";
    public override string GetFullClassName() => "Blaze::Authentication::PersonaDetails";

    public string DisplayName
    {
        get => _displayName.Value;
        set => _displayName.Value = value;
    }

    public uint LastAuthenticated
    {
        get => _lastAuthenticated.Value;
        set => _lastAuthenticated.Value = value;
    }

    public long PersonaId
    {
        get => _personaId.Value;
        set => _personaId.Value = value;
    }

    public Blaze15SDK.Blaze.Authentication.PersonaStatus Status
    {
        get => _status.Value;
        set => _status.Value = value;
    }

    public long ExtId
    {
        get => _extId.Value;
        set => _extId.Value = value;
    }

}
