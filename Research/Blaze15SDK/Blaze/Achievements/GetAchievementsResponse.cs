using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Achievements;

public class GetAchievementsResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Achievements", "mAchievements", 0x863A3300, TdfType.Map, 0, true), // Tag: ACHS
    ];
    private ITdfMember[] __members;

    private TdfMap<string, AchievementData> _achievements = new(__typeInfos[0]);

    public GetAchievementsResponse()
    {
        __members = [ _achievements ];
    }

    public override Tdf CreateNew() => new GetAchievementsResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetAchievementsResponse";
    public override string GetFullClassName() => "Blaze::Achievements::GetAchievementsResponse";

    public IDictionary<string, AchievementData> Achievements
    {
        get => _achievements.Value;
        set => _achievements.Value = value;
    }

}
