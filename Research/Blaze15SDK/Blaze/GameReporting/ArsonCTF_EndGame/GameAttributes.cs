using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting.ArsonCTF_EndGame;

public class GameAttributes : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("DurationSec", "durationSec", 0x935C8000, TdfType.Float, 0, true), // Tag: DUR
        new TdfMemberInfo("ReplayTimes", "replayTimes", 0xD29B6500, TdfType.UInt32, 1, true), // Tag: TIME
    ];
    private ITdfMember[] __members;

    private TdfFloat _durationSec = new(__typeInfos[0]);
    private TdfUInt32 _replayTimes = new(__typeInfos[1]);

    public GameAttributes()
    {
        __members = [ _durationSec, _replayTimes ];
    }

    public override Tdf CreateNew() => new GameAttributes();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GameAttributes";
    public override string GetFullClassName() => "Blaze::GameReporting::ArsonCTF_EndGame::GameAttributes";

    public float DurationSec
    {
        get => _durationSec.Value;
        set => _durationSec.Value = value;
    }

    public uint ReplayTimes
    {
        get => _replayTimes.Value;
        set => _replayTimes.Value = value;
    }

}
