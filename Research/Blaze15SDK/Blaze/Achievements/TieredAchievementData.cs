using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Achievements;

public class TieredAchievementData : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Desc", "mDesc", 0x925CE300, TdfType.String, 0, true), // Tag: DESC
        new TdfMemberInfo("Howto", "mHowto", 0xA2FDF400, TdfType.String, 1, true), // Tag: HOWT
        new TdfMemberInfo("Icons", "mIcons", 0xA63BEE00, TdfType.Map, 2, true), // Tag: ICON
        new TdfMemberInfo("Img", "mImg", 0xA6D9C000, TdfType.String, 3, true), // Tag: IMG
        new TdfMemberInfo("Name", "mName", 0xBA1B6500, TdfType.String, 4, true), // Tag: NAME
        new TdfMemberInfo("Requirements", "mRequirements", 0xCA5C6C00, TdfType.List, 5, true), // Tag: REQL
        new TdfMemberInfo("Rp", "mRp", 0xCB0D3300, TdfType.Int64, 6, true), // Tag: RPTS
        new TdfMemberInfo("T", "mT", 0xD30D3300, TdfType.Int64, 7, true), // Tag: TPTS
        new TdfMemberInfo("Xp", "mXp", 0xE30D3300, TdfType.Int64, 8, true), // Tag: XPTS
    ];
    private ITdfMember[] __members;

    private TdfString _desc = new(__typeInfos[0]);
    private TdfString _howto = new(__typeInfos[1]);
    private TdfMap<uint, string> _icons = new(__typeInfos[2]);
    private TdfString _img = new(__typeInfos[3]);
    private TdfString _name = new(__typeInfos[4]);
    private TdfList<RequirementData> _requirements = new(__typeInfos[5]);
    private TdfInt64 _rp = new(__typeInfos[6]);
    private TdfInt64 _t = new(__typeInfos[7]);
    private TdfInt64 _xp = new(__typeInfos[8]);

    public TieredAchievementData()
    {
        __members = [ _desc, _howto, _icons, _img, _name, _requirements, _rp, _t, _xp ];
    }

    public override Tdf CreateNew() => new TieredAchievementData();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "TieredAchievementData";
    public override string GetFullClassName() => "Blaze::Achievements::TieredAchievementData";

    public string Desc
    {
        get => _desc.Value;
        set => _desc.Value = value;
    }

    public string Howto
    {
        get => _howto.Value;
        set => _howto.Value = value;
    }

    public IDictionary<uint, string> Icons
    {
        get => _icons.Value;
        set => _icons.Value = value;
    }

    public string Img
    {
        get => _img.Value;
        set => _img.Value = value;
    }

    public string Name
    {
        get => _name.Value;
        set => _name.Value = value;
    }

    public IList<RequirementData> Requirements
    {
        get => _requirements.Value;
        set => _requirements.Value = value;
    }

    public long Rp
    {
        get => _rp.Value;
        set => _rp.Value = value;
    }

    public long T
    {
        get => _t.Value;
        set => _t.Value = value;
    }

    public long Xp
    {
        get => _xp.Value;
        set => _xp.Value = value;
    }

}
