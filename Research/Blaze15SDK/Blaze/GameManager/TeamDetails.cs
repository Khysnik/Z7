using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class TeamDetails : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("TeamRoster", "mTeamRoster", 0xCB3D3200, TdfType.List, 0, true), // Tag: RSTR
        new TdfMemberInfo("TeamId", "mTeamId", 0xD2990000, TdfType.UInt16, 1, true), // Tag: TID
    ];
    private ITdfMember[] __members;

    private TdfList<Blaze15SDK.Blaze.GameManager.TeamMemberInfo> _teamRoster = new(__typeInfos[0]);
    private TdfUInt16 _teamId = new(__typeInfos[1]);

    public TeamDetails()
    {
        __members = [ _teamRoster, _teamId ];
    }

    public override Tdf CreateNew() => new TeamDetails();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "TeamDetails";
    public override string GetFullClassName() => "Blaze::GameManager::TeamDetails";

    public IList<Blaze15SDK.Blaze.GameManager.TeamMemberInfo> TeamRoster
    {
        get => _teamRoster.Value;
        set => _teamRoster.Value = value;
    }

    public ushort TeamId
    {
        get => _teamId.Value;
        set => _teamId.Value = value;
    }

}
