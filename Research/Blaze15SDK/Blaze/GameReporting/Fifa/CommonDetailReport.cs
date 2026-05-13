using EATDF;
using EATDF.Members;

namespace Blaze15SDK.Blaze.GameReporting.Fifa;

public class CommonDetailReport : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("AveragePossessionLength", "averagePossessionLength", 0x867C2C00, TdfType.UInt16, 0, true), // Tag: AGPL
        new TdfMemberInfo("AverageFatigueAfterNinety", "averageFatigueAfterNinety", 0x9B4BB400, TdfType.UInt16, 1, true), // Tag: FTNT
        new TdfMemberInfo("InjuriesRed", "injuriesRed", 0xA6ACA400, TdfType.UInt16, 2, true), // Tag: IJRD
        new TdfMemberInfo("PassesIntercepted", "passesIntercepted", 0xC33A7400, TdfType.UInt16, 3, true), // Tag: PSIT
        new TdfMemberInfo("PenaltiesAwarded", "penaltiesAwarded", 0xC3487700, TdfType.UInt16, 4, true), // Tag: PTAW
        new TdfMemberInfo("PenaltiesMissed", "penaltiesMissed", 0xC34B7300, TdfType.UInt16, 5, true), // Tag: PTMS
        new TdfMemberInfo("PenaltiesScored", "penaltiesScored", 0xC34CE300, TdfType.UInt16, 6, true), // Tag: PTSC
        new TdfMemberInfo("PenaltiesSaved", "penaltiesSaved", 0xC34CF600, TdfType.UInt16, 7, true), // Tag: PTSV
    ];
    private ITdfMember[] __members;

    private TdfUInt16 _averagePossessionLength = new(__typeInfos[0]);
    private TdfUInt16 _averageFatigueAfterNinety = new(__typeInfos[1]);
    private TdfUInt16 _injuriesRed = new(__typeInfos[2]);
    private TdfUInt16 _passesIntercepted = new(__typeInfos[3]);
    private TdfUInt16 _penaltiesAwarded = new(__typeInfos[4]);
    private TdfUInt16 _penaltiesMissed = new(__typeInfos[5]);
    private TdfUInt16 _penaltiesScored = new(__typeInfos[6]);
    private TdfUInt16 _penaltiesSaved = new(__typeInfos[7]);

    public CommonDetailReport()
    {
        __members = [_averagePossessionLength, _averageFatigueAfterNinety, _injuriesRed, _passesIntercepted, _penaltiesAwarded, _penaltiesMissed, _penaltiesScored, _penaltiesSaved];
    }

    public override Tdf CreateNew() => new CommonDetailReport();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "CommonDetailReport";
    public override string GetFullClassName() => "Blaze::GameReporting::Fifa::CommonDetailReport";

    public ushort AveragePossessionLength { get => _averagePossessionLength.Value; set => _averagePossessionLength.Value = value; }
    public ushort AverageFatigueAfterNinety { get => _averageFatigueAfterNinety.Value; set => _averageFatigueAfterNinety.Value = value; }
    public ushort InjuriesRed { get => _injuriesRed.Value; set => _injuriesRed.Value = value; }
    public ushort PassesIntercepted { get => _passesIntercepted.Value; set => _passesIntercepted.Value = value; }
    public ushort PenaltiesAwarded { get => _penaltiesAwarded.Value; set => _penaltiesAwarded.Value = value; }
    public ushort PenaltiesMissed { get => _penaltiesMissed.Value; set => _penaltiesMissed.Value = value; }
    public ushort PenaltiesScored { get => _penaltiesScored.Value; set => _penaltiesScored.Value = value; }
    public ushort PenaltiesSaved { get => _penaltiesSaved.Value; set => _penaltiesSaved.Value = value; }
}
