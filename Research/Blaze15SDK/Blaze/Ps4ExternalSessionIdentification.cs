using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class Ps4ExternalSessionIdentification : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("NpSessionId", "mNpSessionId", 0xBB0CE900, TdfType.String, 0, true), // Tag: NPSI
    ];
    private ITdfMember[] __members;

    private TdfString _npSessionId = new(__typeInfos[0]);

    public Ps4ExternalSessionIdentification()
    {
        __members = [
            _npSessionId,
        ];
    }

    public override Tdf CreateNew() => new Ps4ExternalSessionIdentification();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "Ps4ExternalSessionIdentification";
    public override string GetFullClassName() => "Blaze::Ps4ExternalSessionIdentification";

    public string NpSessionId
    {
        get => _npSessionId.Value;
        set => _npSessionId.Value = value;
    }

}
