using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class TournamentIdentification : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("TournamentId", "mTournamentId", 0xD2990000, TdfType.String, 0, true), // Tag: TID
        new TdfMemberInfo("TournamentOrganizer", "mTournamentOrganizer", 0xD2FCA700, TdfType.String, 1, true), // Tag: TORG
    ];
    private ITdfMember[] __members;

    private TdfString _tournamentId = new(__typeInfos[0]);
    private TdfString _tournamentOrganizer = new(__typeInfos[1]);

    public TournamentIdentification()
    {
        __members = [ _tournamentId, _tournamentOrganizer ];
    }

    public override Tdf CreateNew() => new TournamentIdentification();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "TournamentIdentification";
    public override string GetFullClassName() => "Blaze::GameManager::TournamentIdentification";

    public string TournamentId
    {
        get => _tournamentId.Value;
        set => _tournamentId.Value = value;
    }

    public string TournamentOrganizer
    {
        get => _tournamentOrganizer.Value;
        set => _tournamentOrganizer.Value = value;
    }

}
