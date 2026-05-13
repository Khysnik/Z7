using EATDF;
using EATDF.Members;

namespace Blaze15SDK.Blaze.GameReporting.Fifa;

public class H2HNotificationPlayerCustomData : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("SkillPoints", "skillPoints", 0x9F3C3400, TdfType.UInt32, 0, true), // Tag: GSPT
    ];
    private ITdfMember[] __members;

    private TdfUInt32 _skillPoints = new(__typeInfos[0]);

    public H2HNotificationPlayerCustomData()
    {
        __members = [_skillPoints];
    }

    public override Tdf CreateNew() => new H2HNotificationPlayerCustomData();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "H2HNotificationPlayerCustomData";
    public override string GetFullClassName() => "Blaze::GameReporting::Fifa::H2HNotificationPlayerCustomData";

    public uint SkillPoints { get => _skillPoints.Value; set => _skillPoints.Value = value; }
}
