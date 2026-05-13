using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class TournamentSessionData : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ArbitrationTimeout", "mArbitrationTimeout", 0x872D2F00, TdfType.TimeValue, 0, true), // Tag: ARTO
        new TdfMemberInfo("ForfeitTimeout", "mForfeitTimeout", 0x9B2D2F00, TdfType.TimeValue, 1, true), // Tag: FRTO
        new TdfMemberInfo("TournamentDefinition", "mTournamentDefinition", 0xD2496600, TdfType.String, 2, true), // Tag: TDEF
        new TdfMemberInfo("ScheduledStartTime", "mScheduledStartTime", 0xD33D2100, TdfType.String, 3, true), // Tag: TSTA
    ];
    private ITdfMember[] __members;

    private TdfString _scheduledStartTime = new(__typeInfos[3]);
    private TdfTimeValue _arbitrationTimeout = new(__typeInfos[0]);
    private TdfTimeValue _forfeitTimeout = new(__typeInfos[1]);
    private TdfString _tournamentDefinition = new(__typeInfos[2]);

    public TournamentSessionData()
    {
        __members = [
            _arbitrationTimeout,
            _forfeitTimeout,
            _tournamentDefinition,
            _scheduledStartTime,
        ];
    }

    public override Tdf CreateNew() => new TournamentSessionData();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "TournamentSessionData";
    public override string GetFullClassName() => "Blaze::TournamentSessionData";

    public string ScheduledStartTime
    {
        get => _scheduledStartTime.Value;
        set => _scheduledStartTime.Value = value;
    }

    public TimeValue ArbitrationTimeout
    {
        get => _arbitrationTimeout.Value;
        set => _arbitrationTimeout.Value = value;
    }

    public TimeValue ForfeitTimeout
    {
        get => _forfeitTimeout.Value;
        set => _forfeitTimeout.Value = value;
    }

    public string TournamentDefinition
    {
        get => _tournamentDefinition.Value;
        set => _tournamentDefinition.Value = value;
    }

}
