using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class Ps5ExternalSessionIdentification : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Match", "mMatch", 0xB61D2300, TdfType.Struct, 0, true), // Tag: MATC
        new TdfMemberInfo("PlayerSession", "mPlayerSession", 0xC3397300, TdfType.Struct, 1, true), // Tag: PSES
    ];
    private ITdfMember[] __members;

    private TdfStruct<Blaze15SDK.Blaze.Ps5ExternalSessionIdentificationMatch?> _match = new(__typeInfos[0]);
    private TdfStruct<Blaze15SDK.Blaze.Ps5ExternalSessionIdentificationPlayerSession?> _playerSession = new(__typeInfos[1]);

    public Ps5ExternalSessionIdentification()
    {
        __members = [
            _match,
            _playerSession,
        ];
    }

    public override Tdf CreateNew() => new Ps5ExternalSessionIdentification();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "Ps5ExternalSessionIdentification";
    public override string GetFullClassName() => "Blaze::Ps5ExternalSessionIdentification";

    public Blaze15SDK.Blaze.Ps5ExternalSessionIdentificationMatch? Match
    {
        get => _match.Value;
        set => _match.Value = value;
    }

    public Blaze15SDK.Blaze.Ps5ExternalSessionIdentificationPlayerSession? PlayerSession
    {
        get => _playerSession.Value;
        set => _playerSession.Value = value;
    }

}
