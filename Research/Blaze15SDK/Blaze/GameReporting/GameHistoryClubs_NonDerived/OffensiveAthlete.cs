using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting.GameHistoryClubs_NonDerived;

public class OffensiveAthlete : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Hits", "hits", 0xA29D3300, TdfType.UInt16, 0, true), // Tag: HITS
        new TdfMemberInfo("Serves", "serves", 0xCE5CB600, TdfType.UInt16, 1, true), // Tag: SERV
    ];
    private ITdfMember[] __members;

    private TdfUInt16 _hits = new(__typeInfos[0]);
    private TdfUInt16 _serves = new(__typeInfos[1]);

    public OffensiveAthlete()
    {
        __members = [ _hits, _serves ];
    }

    public override Tdf CreateNew() => new OffensiveAthlete();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "OffensiveAthlete";
    public override string GetFullClassName() => "Blaze::GameReporting::GameHistoryClubs_NonDerived::OffensiveAthlete";

    public ushort Hits
    {
        get => _hits.Value;
        set => _hits.Value = value;
    }

    public ushort Serves
    {
        get => _serves.Value;
        set => _serves.Value = value;
    }

}
