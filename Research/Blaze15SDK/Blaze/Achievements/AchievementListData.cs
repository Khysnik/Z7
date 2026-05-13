using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Achievements;

public class AchievementListData : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("AchievementList", "mAchievementList", 0x863B2900, TdfType.Map, 0, true), // Tag: ACLI
    ];
    private ITdfMember[] __members;

    private TdfMap<string, AchievementData> _achievementList = new(__typeInfos[0]);

    public AchievementListData()
    {
        __members = [ _achievementList ];
    }

    public override Tdf CreateNew() => new AchievementListData();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "AchievementListData";
    public override string GetFullClassName() => "Blaze::Achievements::AchievementListData";

    public IDictionary<string, AchievementData> AchievementList
    {
        get => _achievementList.Value;
        set => _achievementList.Value = value;
    }

}
