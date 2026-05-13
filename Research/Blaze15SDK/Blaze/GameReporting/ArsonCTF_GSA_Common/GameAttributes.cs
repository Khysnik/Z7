using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting.ArsonCTF_GSA_Common;

public class GameAttributes : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("DurationSec", "durationSec", 0x935C8000, TdfType.Float, 0, true), // Tag: DUR
        new TdfMemberInfo("GameName", "gameName", 0x9EE86D00, TdfType.String, 1, true), // Tag: GNAM
        new TdfMemberInfo("ProgressPercentage", "progressPercentage", 0x9F0CB000, TdfType.Float, 2, true), // Tag: GPRP
        new TdfMemberInfo("MapName", "mapName", 0xB61C0000, TdfType.String, 3, true), // Tag: MAP
        new TdfMemberInfo("Rating", "rating", 0xCA1D2700, TdfType.Int32, 4, true), // Tag: RATG
        new TdfMemberInfo("ReplayTimes", "replayTimes", 0xD29B6500, TdfType.UInt32, 5, true), // Tag: TIME
    ];
    private ITdfMember[] __members;

    private TdfFloat _durationSec = new(__typeInfos[0]);
    private TdfString _gameName = new(__typeInfos[1]);
    private TdfFloat _progressPercentage = new(__typeInfos[2]);
    private TdfString _mapName = new(__typeInfos[3]);
    private TdfInt32 _rating = new(__typeInfos[4]);
    private TdfUInt32 _replayTimes = new(__typeInfos[5]);

    public GameAttributes()
    {
        __members = [ _durationSec, _gameName, _progressPercentage, _mapName, _rating, _replayTimes ];
    }

    public override Tdf CreateNew() => new GameAttributes();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GameAttributes";
    public override string GetFullClassName() => "Blaze::GameReporting::ArsonCTF_GSA_Common::GameAttributes";

    public float DurationSec
    {
        get => _durationSec.Value;
        set => _durationSec.Value = value;
    }

    public string GameName
    {
        get => _gameName.Value;
        set => _gameName.Value = value;
    }

    public float ProgressPercentage
    {
        get => _progressPercentage.Value;
        set => _progressPercentage.Value = value;
    }

    public string MapName
    {
        get => _mapName.Value;
        set => _mapName.Value = value;
    }

    public int Rating
    {
        get => _rating.Value;
        set => _rating.Value = value;
    }

    public uint ReplayTimes
    {
        get => _replayTimes.Value;
        set => _replayTimes.Value = value;
    }

}
