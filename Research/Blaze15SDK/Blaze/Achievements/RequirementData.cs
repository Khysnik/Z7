using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Achievements;

public class RequirementData : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Current", "mCurrent", 0x8F5C8000, TdfType.Int64, 0, true), // Tag: CUR
        new TdfMemberInfo("LastUpdated", "mLastUpdated", 0xB21CF400, TdfType.Int64, 1, true), // Tag: LAST
        new TdfMemberInfo("Name", "mName", 0xBA1B6500, TdfType.String, 2, true), // Tag: NAME
        new TdfMemberInfo("Required", "mRequired", 0xCA5C4000, TdfType.Int64, 3, true), // Tag: REQ
        new TdfMemberInfo("Stat", "mStat", 0xCF487400, TdfType.String, 4, true), // Tag: STAT
        new TdfMemberInfo("User", "mUser", 0xD7397200, TdfType.Int64, 5, true), // Tag: USER
    ];
    private ITdfMember[] __members;

    private TdfInt64 _current = new(__typeInfos[0]);
    private TdfInt64 _lastUpdated = new(__typeInfos[1]);
    private TdfString _name = new(__typeInfos[2]);
    private TdfInt64 _required = new(__typeInfos[3]);
    private TdfString _stat = new(__typeInfos[4]);
    private TdfInt64 _user = new(__typeInfos[5]);

    public RequirementData()
    {
        __members = [ _current, _lastUpdated, _name, _required, _stat, _user ];
    }

    public override Tdf CreateNew() => new RequirementData();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "RequirementData";
    public override string GetFullClassName() => "Blaze::Achievements::RequirementData";

    public long Current
    {
        get => _current.Value;
        set => _current.Value = value;
    }

    public long LastUpdated
    {
        get => _lastUpdated.Value;
        set => _lastUpdated.Value = value;
    }

    public string Name
    {
        get => _name.Value;
        set => _name.Value = value;
    }

    public long Required
    {
        get => _required.Value;
        set => _required.Value = value;
    }

    public string Stat
    {
        get => _stat.Value;
        set => _stat.Value = value;
    }

    public long User
    {
        get => _user.Value;
        set => _user.Value = value;
    }

}
