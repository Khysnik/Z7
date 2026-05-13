using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting.ArsonMultiStatUpdates;

public class PlayerReport : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ClubId", "clubId", 0x8E990000, TdfType.UInt64, 0, true), // Tag: CID
        new TdfMemberInfo("Losses", "losses", 0xB2FCF300, TdfType.UInt16, 1, true), // Tag: LOSS
        new TdfMemberInfo("Points", "points", 0xC2ED3300, TdfType.UInt16, 2, true), // Tag: PNTS
        new TdfMemberInfo("Wins", "wins", 0xDE9BB300, TdfType.UInt16, 3, true), // Tag: WINS
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _clubId = new(__typeInfos[0]);
    private TdfUInt16 _losses = new(__typeInfos[1]);
    private TdfUInt16 _points = new(__typeInfos[2]);
    private TdfUInt16 _wins = new(__typeInfos[3]);

    public PlayerReport()
    {
        __members = [ _clubId, _losses, _points, _wins ];
    }

    public override Tdf CreateNew() => new PlayerReport();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "PlayerReport";
    public override string GetFullClassName() => "Blaze::GameReporting::ArsonMultiStatUpdates::PlayerReport";

    public ulong ClubId
    {
        get => _clubId.Value;
        set => _clubId.Value = value;
    }

    public ushort Losses
    {
        get => _losses.Value;
        set => _losses.Value = value;
    }

    public ushort Points
    {
        get => _points.Value;
        set => _points.Value = value;
    }

    public ushort Wins
    {
        get => _wins.Value;
        set => _wins.Value = value;
    }

}
