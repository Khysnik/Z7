using EATDF;
using EATDF.Members;

namespace Blaze15SDK.Blaze.GameReporting.Fifa;

public class SoloGameReport : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("OpponentTeamId", "opponentTeamId", 0x874A6400, TdfType.UInt32, 0, true), // Tag: ATID
        new TdfMemberInfo("CommonGameReport", "mCommonGameReport", 0x8ED9F200, TdfType.Struct, 1, true), // Tag: CMGR
        new TdfMemberInfo("MatchDifficulty", "matchDifficulty", 0xB64A6600, TdfType.UInt16, 2, true), // Tag: MDIF
        new TdfMemberInfo("MatchSubType", "matchSubType", 0xB73D3000, TdfType.UInt16, 3, true), // Tag: MSTP
        new TdfMemberInfo("ProfileDifficulty", "profileDifficulty", 0xC2D92600, TdfType.UInt16, 4, true), // Tag: PMDF
    ];
    private ITdfMember[] __members;

    private TdfUInt32 _opponentTeamId = new(__typeInfos[0]);
    private TdfStruct<CommonGameReport?> _commonGameReport = new(__typeInfos[1]);
    private TdfUInt16 _matchDifficulty = new(__typeInfos[2]);
    private TdfUInt16 _matchSubType = new(__typeInfos[3]);
    private TdfUInt16 _profileDifficulty = new(__typeInfos[4]);

    public SoloGameReport()
    {
        __members = [_opponentTeamId, _commonGameReport, _matchDifficulty, _matchSubType, _profileDifficulty];
    }

    public override Tdf CreateNew() => new SoloGameReport();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "SoloGameReport";
    public override string GetFullClassName() => "Blaze::GameReporting::Fifa::SoloGameReport";

    public override uint GetTdfId() => 2528847536; // 0x96B4B2B0

    public uint OpponentTeamId { get => _opponentTeamId.Value; set => _opponentTeamId.Value = value; }
    public CommonGameReport? CommonGameReport { get => _commonGameReport.Value; set => _commonGameReport.Value = value; }
    public ushort MatchDifficulty { get => _matchDifficulty.Value; set => _matchDifficulty.Value = value; }
    public ushort MatchSubType { get => _matchSubType.Value; set => _matchSubType.Value = value; }
    public ushort ProfileDifficulty { get => _profileDifficulty.Value; set => _profileDifficulty.Value = value; }
}
