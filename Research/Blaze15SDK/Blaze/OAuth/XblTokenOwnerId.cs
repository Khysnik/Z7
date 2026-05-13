using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.OAuth;

public class XblTokenOwnerId : Union
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("PersonaId", "mPersonaId", 0xDA1B3500, TdfType.Int64, 0, false), // Tag: VALU
        new TdfMemberInfo("ExternalId", "mExternalId", 0xDA1B3500, TdfType.UInt64, 1, false), // Tag: VALU
    ];

    private TdfInt64 _personaId = new(__typeInfos[0]);
    private TdfUInt64 _externalId = new(__typeInfos[1]);

    public override ITdfMember? GetActiveMember()
    {
        return ActiveIndex switch
        {
            0 => _personaId,
            1 => _externalId,
            _ => null
        };
    }
    public override Tdf CreateNew() => new XblTokenOwnerId();
    public override string GetClassName() => "XblTokenOwnerId";
    public override string GetFullClassName() => "Blaze::OAuth::XblTokenOwnerId";

    public long PersonaId
    {
        get => _personaId.Value;
        set
        {
            SwitchActiveIndex(0);
            _personaId.Value = value;
        }
    }

    public ulong ExternalId
    {
        get => _externalId.Value;
        set
        {
            SwitchActiveIndex(1);
            _externalId.Value = value;
        }
    }

}
