using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting.Shooter;

public class OfflineReport : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("NationReports", "nationReports", 0xBA1D2E00, TdfType.Map, 0, true), // Tag: NATN
        new TdfMemberInfo("PlayerReports", "playerReports", 0xC2CE7200, TdfType.Map, 1, true), // Tag: PLYR
    ];
    private ITdfMember[] __members;

    private TdfMap<int, GroupReport> _nationReports = new(__typeInfos[0]);
    private TdfMap<long, EntityReport> _playerReports = new(__typeInfos[1]);

    public OfflineReport()
    {
        __members = [ _nationReports, _playerReports ];
    }

    public override Tdf CreateNew() => new OfflineReport();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "OfflineReport";
    public override string GetFullClassName() => "Blaze::GameReporting::Shooter::OfflineReport";

    public IDictionary<int, GroupReport> NationReports
    {
        get => _nationReports.Value;
        set => _nationReports.Value = value;
    }

    public IDictionary<long, EntityReport> PlayerReports
    {
        get => _playerReports.Value;
        set => _playerReports.Value = value;
    }

}
