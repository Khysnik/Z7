using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting.ArsonMultiStatUpdates;

public class GameAttributes : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("LeagueId", "leagueId", 0xB2586700, TdfType.UInt64, 0, true), // Tag: LEAG
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _leagueId = new(__typeInfos[0]);

    public GameAttributes()
    {
        __members = [ _leagueId ];
    }

    public override Tdf CreateNew() => new GameAttributes();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GameAttributes";
    public override string GetFullClassName() => "Blaze::GameReporting::ArsonMultiStatUpdates::GameAttributes";

    public ulong LeagueId
    {
        get => _leagueId.Value;
        set => _leagueId.Value = value;
    }

}
