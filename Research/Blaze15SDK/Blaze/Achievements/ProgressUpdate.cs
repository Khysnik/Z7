using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Achievements;

public class ProgressUpdate : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Current", "mCurrent", 0x8F5C8000, TdfType.Int64, 0, true), // Tag: CUR
        new TdfMemberInfo("Points", "mPoints", 0xC34CC000, TdfType.Int64, 1, true), // Tag: PTS
    ];
    private ITdfMember[] __members;

    private TdfInt64 _current = new(__typeInfos[0]);
    private TdfInt64 _points = new(__typeInfos[1]);

    public ProgressUpdate()
    {
        __members = [ _current, _points ];
    }

    public override Tdf CreateNew() => new ProgressUpdate();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ProgressUpdate";
    public override string GetFullClassName() => "Blaze::Achievements::ProgressUpdate";

    public long Current
    {
        get => _current.Value;
        set => _current.Value = value;
    }

    public long Points
    {
        get => _points.Value;
        set => _points.Value = value;
    }

}
