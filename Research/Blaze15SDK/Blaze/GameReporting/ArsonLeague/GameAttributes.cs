using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting.ArsonLeague;

public class GameAttributes : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("IsSim", "isSim", 0xA73A6D00, TdfType.Int32, 0, true), // Tag: ISIM
        new TdfMemberInfo("LeagueId", "leagueId", 0xB27A6400, TdfType.UInt64, 1, true), // Tag: LGID
    ];
    private ITdfMember[] __members;

    private TdfInt32 _isSim = new(__typeInfos[0]);
    private TdfUInt64 _leagueId = new(__typeInfos[1]);

    public GameAttributes()
    {
        __members = [ _isSim, _leagueId ];
    }

    public override Tdf CreateNew() => new GameAttributes();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GameAttributes";
    public override string GetFullClassName() => "Blaze::GameReporting::ArsonLeague::GameAttributes";

    public int IsSim
    {
        get => _isSim.Value;
        set => _isSim.Value = value;
    }

    public ulong LeagueId
    {
        get => _leagueId.Value;
        set => _leagueId.Value = value;
    }

}
