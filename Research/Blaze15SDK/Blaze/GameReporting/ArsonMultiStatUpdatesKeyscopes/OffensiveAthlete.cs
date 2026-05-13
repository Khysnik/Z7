using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting.ArsonMultiStatUpdatesKeyscopes;

public class OffensiveAthlete : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Hits", "hits", 0xA29D3300, TdfType.UInt16, 0, true), // Tag: HITS
        new TdfMemberInfo("PlayerId", "playerId", 0xC2990000, TdfType.Int64, 1, true), // Tag: PID
        new TdfMemberInfo("Serves", "serves", 0xCE5CB600, TdfType.UInt16, 2, true), // Tag: SERV
    ];
    private ITdfMember[] __members;

    private TdfUInt16 _hits = new(__typeInfos[0]);
    private TdfInt64 _playerId = new(__typeInfos[1]);
    private TdfUInt16 _serves = new(__typeInfos[2]);

    public OffensiveAthlete()
    {
        __members = [ _hits, _playerId, _serves ];
    }

    public override Tdf CreateNew() => new OffensiveAthlete();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "OffensiveAthlete";
    public override string GetFullClassName() => "Blaze::GameReporting::ArsonMultiStatUpdatesKeyscopes::OffensiveAthlete";

    public ushort Hits
    {
        get => _hits.Value;
        set => _hits.Value = value;
    }

    public long PlayerId
    {
        get => _playerId.Value;
        set => _playerId.Value = value;
    }

    public ushort Serves
    {
        get => _serves.Value;
        set => _serves.Value = value;
    }

}
