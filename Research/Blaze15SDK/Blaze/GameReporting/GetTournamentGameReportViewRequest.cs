using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting;

public class GetTournamentGameReportViewRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("GameId", "mGameId", 0x9EDA6400, TdfType.UInt64, 0, true), // Tag: GMID
        new TdfMemberInfo("Name", "mName", 0xBA1B6500, TdfType.String, 1, true), // Tag: NAME
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _gameId = new(__typeInfos[0]);
    private TdfString _name = new(__typeInfos[1]);

    public GetTournamentGameReportViewRequest()
    {
        __members = [ _gameId, _name ];
    }

    public override Tdf CreateNew() => new GetTournamentGameReportViewRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetTournamentGameReportViewRequest";
    public override string GetFullClassName() => "Blaze::GameReporting::GetTournamentGameReportViewRequest";

    public ulong GameId
    {
        get => _gameId.Value;
        set => _gameId.Value = value;
    }

    public string Name
    {
        get => _name.Value;
        set => _name.Value = value;
    }

}
