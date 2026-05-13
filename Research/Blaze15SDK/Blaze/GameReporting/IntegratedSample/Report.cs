using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting.IntegratedSample;

public class Report : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("CoopResult", "coopResult", 0x8EFBF000, TdfType.UInt32, 0, true), // Tag: COOP
        new TdfMemberInfo("GameAttrs", "gameAttrs", 0x9E1B6500, TdfType.Struct, 1, true), // Tag: GAME
        new TdfMemberInfo("PlayerReports", "playerReports", 0xC2CE7200, TdfType.Map, 2, true), // Tag: PLYR
    ];
    private ITdfMember[] __members;

    private TdfUInt32 _coopResult = new(__typeInfos[0]);
    private TdfStruct<GameAttributes?> _gameAttrs = new(__typeInfos[1]);
    private TdfMap<long, PlayerReport> _playerReports = new(__typeInfos[2]);

    public Report()
    {
        __members = [ _coopResult, _gameAttrs, _playerReports ];
    }

    public override Tdf CreateNew() => new Report();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "Report";
    public override string GetFullClassName() => "Blaze::GameReporting::IntegratedSample::Report";

    public uint CoopResult
    {
        get => _coopResult.Value;
        set => _coopResult.Value = value;
    }

    public GameAttributes? GameAttrs
    {
        get => _gameAttrs.Value;
        set => _gameAttrs.Value = value;
    }

    public IDictionary<long, PlayerReport> PlayerReports
    {
        get => _playerReports.Value;
        set => _playerReports.Value = value;
    }

}
