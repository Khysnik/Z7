using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class EaIdentifiers : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("OriginPersonaName", "mOriginPersonaName", 0xBA1B6500, TdfType.String, 0, true), // Tag: NAME
        new TdfMemberInfo("NucleusAccountId", "mNucleusAccountId", 0xBA990000, TdfType.Int64, 1, true), // Tag: NID
        new TdfMemberInfo("OriginPersonaId", "mOriginPersonaId", 0xC23A6400, TdfType.UInt64, 2, true), // Tag: PCID
    ];
    private ITdfMember[] __members;

    private TdfString _originPersonaName = new(__typeInfos[0]);
    private TdfInt64 _nucleusAccountId = new(__typeInfos[1]);
    private TdfUInt64 _originPersonaId = new(__typeInfos[2]);

    public EaIdentifiers()
    {
        __members = [
            _originPersonaName,
            _nucleusAccountId,
            _originPersonaId,
        ];
    }

    public override Tdf CreateNew() => new EaIdentifiers();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "EaIdentifiers";
    public override string GetFullClassName() => "Blaze::EaIdentifiers";

    public string OriginPersonaName
    {
        get => _originPersonaName.Value;
        set => _originPersonaName.Value = value;
    }

    public long NucleusAccountId
    {
        get => _nucleusAccountId.Value;
        set => _nucleusAccountId.Value = value;
    }

    public ulong OriginPersonaId
    {
        get => _originPersonaId.Value;
        set => _originPersonaId.Value = value;
    }

}
