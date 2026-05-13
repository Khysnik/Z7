using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class Ps5ExternalSessionIdentificationMatch : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("MatchId", "mMatchId", 0xB61A6400, TdfType.String, 0, true), // Tag: MAID
        new TdfMemberInfo("ActivityObjectId", "mActivityObjectId", 0xB61BE900, TdfType.String, 1, true), // Tag: MAOI
    ];
    private ITdfMember[] __members;

    private TdfString _matchId = new(__typeInfos[0]);
    private TdfString _activityObjectId = new(__typeInfos[1]);

    public Ps5ExternalSessionIdentificationMatch()
    {
        __members = [
            _matchId,
            _activityObjectId,
        ];
    }

    public override Tdf CreateNew() => new Ps5ExternalSessionIdentificationMatch();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "Ps5ExternalSessionIdentificationMatch";
    public override string GetFullClassName() => "Blaze::Ps5ExternalSessionIdentificationMatch";

    public string MatchId
    {
        get => _matchId.Value;
        set => _matchId.Value = value;
    }

    public string ActivityObjectId
    {
        get => _activityObjectId.Value;
        set => _activityObjectId.Value = value;
    }

}
