using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Achievements;

public class AchievementData : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Cnt", "mCnt", 0x8EED0000, TdfType.UInt32, 0, true), // Tag: CNT
        new TdfMemberInfo("Tc", "mTc", 0x8F5CB400, TdfType.Int8, 1, true), // Tag: CURT
        new TdfMemberInfo("Desc", "mDesc", 0x925CE300, TdfType.String, 2, true), // Tag: DESC
        new TdfMemberInfo("D", "mD", 0x925DA300, TdfType.String, 3, true), // Tag: DEVC
        new TdfMemberInfo("E", "mE", 0x978C3900, TdfType.Int64, 4, true), // Tag: EXPY
        new TdfMemberInfo("Howto", "mHowto", 0xA2FDF400, TdfType.String, 5, true), // Tag: HOWT
        new TdfMemberInfo("Icons", "mIcons", 0xA63BEE00, TdfType.Map, 6, true), // Tag: ICON
        new TdfMemberInfo("Img", "mImg", 0xA6D9C000, TdfType.String, 7, true), // Tag: IMG
        new TdfMemberInfo("Name", "mName", 0xBA1B6500, TdfType.String, 8, true), // Tag: NAME
        new TdfMemberInfo("Tn", "mTn", 0xBA5E3400, TdfType.List, 9, true), // Tag: NEXT
        new TdfMemberInfo("P", "mP", 0xC34CC000, TdfType.Int64, 10, true), // Tag: PTS
        new TdfMemberInfo("Requirements", "mRequirements", 0xCA5C6C00, TdfType.List, 11, true), // Tag: REQL
        new TdfMemberInfo("Rp", "mRp", 0xCB0D3300, TdfType.Int64, 12, true), // Tag: RPTS
        new TdfMemberInfo("S", "mS", 0xCF4CB400, TdfType.Int64, 13, true), // Tag: STRT
        new TdfMemberInfo("U", "mU", 0xD29B6500, TdfType.Int64, 14, true), // Tag: TIME
        new TdfMemberInfo("Tt", "mTt", 0xD2FD3400, TdfType.Int8, 15, true), // Tag: TOTT
        new TdfMemberInfo("T", "mT", 0xD30D3300, TdfType.Int64, 16, true), // Tag: TPTS
        new TdfMemberInfo("UserProgressPoints", "mUserProgressPoints", 0xD70D3300, TdfType.Int64, 17, true), // Tag: UPTS
        new TdfMemberInfo("Xpack", "mXpack", 0xE3086300, TdfType.Struct, 18, true), // Tag: XPAC
        new TdfMemberInfo("Xp", "mXp", 0xE30D3300, TdfType.Int64, 19, true), // Tag: XPTS
    ];
    private ITdfMember[] __members;

    private TdfUInt32 _cnt = new(__typeInfos[0]);
    private TdfInt8 _tc = new(__typeInfos[1]);
    private TdfString _desc = new(__typeInfos[2]);
    private TdfString _d = new(__typeInfos[3]);
    private TdfInt64 _e = new(__typeInfos[4]);
    private TdfString _howto = new(__typeInfos[5]);
    private TdfMap<uint, string> _icons = new(__typeInfos[6]);
    private TdfString _img = new(__typeInfos[7]);
    private TdfString _name = new(__typeInfos[8]);
    private TdfList<TieredAchievementData> _tn = new(__typeInfos[9]);
    private TdfInt64 _p = new(__typeInfos[10]);
    private TdfList<RequirementData> _requirements = new(__typeInfos[11]);
    private TdfInt64 _rp = new(__typeInfos[12]);
    private TdfInt64 _s = new(__typeInfos[13]);
    private TdfInt64 _u = new(__typeInfos[14]);
    private TdfInt8 _tt = new(__typeInfos[15]);
    private TdfInt64 _t = new(__typeInfos[16]);
    private TdfInt64 _userProgressPoints = new(__typeInfos[17]);
    private TdfStruct<ExpansionPackData?> _xpack = new(__typeInfos[18]);
    private TdfInt64 _xp = new(__typeInfos[19]);

    public AchievementData()
    {
        __members = [ _cnt, _tc, _desc, _d, _e, _howto, _icons, _img, _name, _tn, _p, _requirements, _rp, _s, _u, _tt, _t, _userProgressPoints, _xpack, _xp ];
    }

    public override Tdf CreateNew() => new AchievementData();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "AchievementData";
    public override string GetFullClassName() => "Blaze::Achievements::AchievementData";

    public uint Cnt
    {
        get => _cnt.Value;
        set => _cnt.Value = value;
    }

    public sbyte Tc
    {
        get => _tc.Value;
        set => _tc.Value = value;
    }

    public string Desc
    {
        get => _desc.Value;
        set => _desc.Value = value;
    }

    public string D
    {
        get => _d.Value;
        set => _d.Value = value;
    }

    public long E
    {
        get => _e.Value;
        set => _e.Value = value;
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

    public IList<TieredAchievementData> Tn
    {
        get => _tn.Value;
        set => _tn.Value = value;
    }

    public long P
    {
        get => _p.Value;
        set => _p.Value = value;
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

    public long S
    {
        get => _s.Value;
        set => _s.Value = value;
    }

    public long U
    {
        get => _u.Value;
        set => _u.Value = value;
    }

    public sbyte Tt
    {
        get => _tt.Value;
        set => _tt.Value = value;
    }

    public long T
    {
        get => _t.Value;
        set => _t.Value = value;
    }

    public long UserProgressPoints
    {
        get => _userProgressPoints.Value;
        set => _userProgressPoints.Value = value;
    }

    public ExpansionPackData? Xpack
    {
        get => _xpack.Value;
        set => _xpack.Value = value;
    }

    public long Xp
    {
        get => _xp.Value;
        set => _xp.Value = value;
    }

}
