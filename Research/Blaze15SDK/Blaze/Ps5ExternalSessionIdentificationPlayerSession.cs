using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class Ps5ExternalSessionIdentificationPlayerSession : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("PlayerSessionId", "mPlayerSessionId", 0xC33A6400, TdfType.String, 0, true), // Tag: PSID
    ];
    private ITdfMember[] __members;

    private TdfString _playerSessionId = new(__typeInfos[0]);

    public Ps5ExternalSessionIdentificationPlayerSession()
    {
        __members = [
            _playerSessionId,
        ];
    }

    public override Tdf CreateNew() => new Ps5ExternalSessionIdentificationPlayerSession();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "Ps5ExternalSessionIdentificationPlayerSession";
    public override string GetFullClassName() => "Blaze::Ps5ExternalSessionIdentificationPlayerSession";

    public string PlayerSessionId
    {
        get => _playerSessionId.Value;
        set => _playerSessionId.Value = value;
    }

}
