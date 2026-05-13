using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting.ArsonCTF_GSA_NonDerived;

public class PlayerReport : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Accuracy", "accuracy", 0x8638F200, TdfType.Float, 0, true), // Tag: ACCR
        new TdfMemberInfo("Deaths", "deaths", 0x925D2800, TdfType.UInt16, 1, true), // Tag: DETH
        new TdfMemberInfo("FlagsCaptured", "flagsCaptured", 0x9A387000, TdfType.UInt32, 2, true), // Tag: FCAP
        new TdfMemberInfo("Kills", "kills", 0xAE9B2C00, TdfType.UInt16, 3, true), // Tag: KILL
        new TdfMemberInfo("Score", "score", 0xCE3BF200, TdfType.UInt32, 4, true), // Tag: SCOR
    ];
    private ITdfMember[] __members;

    private TdfFloat _accuracy = new(__typeInfos[0]);
    private TdfUInt16 _deaths = new(__typeInfos[1]);
    private TdfUInt32 _flagsCaptured = new(__typeInfos[2]);
    private TdfUInt16 _kills = new(__typeInfos[3]);
    private TdfUInt32 _score = new(__typeInfos[4]);

    public PlayerReport()
    {
        __members = [ _accuracy, _deaths, _flagsCaptured, _kills, _score ];
    }

    public override Tdf CreateNew() => new PlayerReport();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "PlayerReport";
    public override string GetFullClassName() => "Blaze::GameReporting::ArsonCTF_GSA_NonDerived::PlayerReport";

    public float Accuracy
    {
        get => _accuracy.Value;
        set => _accuracy.Value = value;
    }

    public ushort Deaths
    {
        get => _deaths.Value;
        set => _deaths.Value = value;
    }

    public uint FlagsCaptured
    {
        get => _flagsCaptured.Value;
        set => _flagsCaptured.Value = value;
    }

    public ushort Kills
    {
        get => _kills.Value;
        set => _kills.Value = value;
    }

    public uint Score
    {
        get => _score.Value;
        set => _score.Value = value;
    }

}
