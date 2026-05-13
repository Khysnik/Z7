using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting.ArsonClub;

public class PlayerReport : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ClubId", "clubId", 0x8ECA6400, TdfType.UInt64, 0, true), // Tag: CLID
        new TdfMemberInfo("Points", "points", 0xC2ED3300, TdfType.UInt16, 1, true), // Tag: PNTS
        new TdfMemberInfo("RivalPoints", "rivalPoints", 0xCB0D3300, TdfType.UInt16, 2, true), // Tag: RPTS
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _clubId = new(__typeInfos[0]);
    private TdfUInt16 _points = new(__typeInfos[1]);
    private TdfUInt16 _rivalPoints = new(__typeInfos[2]);

    public PlayerReport()
    {
        __members = [ _clubId, _points, _rivalPoints ];
    }

    public override Tdf CreateNew() => new PlayerReport();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "PlayerReport";
    public override string GetFullClassName() => "Blaze::GameReporting::ArsonClub::PlayerReport";

    public ulong ClubId
    {
        get => _clubId.Value;
        set => _clubId.Value = value;
    }

    public ushort Points
    {
        get => _points.Value;
        set => _points.Value = value;
    }

    public ushort RivalPoints
    {
        get => _rivalPoints.Value;
        set => _rivalPoints.Value = value;
    }

}
