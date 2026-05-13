using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class NotifyGameReportingIdChange : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("GameId", "gameId", 0x9E990000, TdfType.UInt64, 0, true), // Tag: GID
        new TdfMemberInfo("GameReportingId", "gameReportingId", 0x9F2A6400, TdfType.UInt64, 1, true), // Tag: GRID
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _gameId = new(__typeInfos[0]);
    private TdfUInt64 _gameReportingId = new(__typeInfos[1]);

    public NotifyGameReportingIdChange()
    {
        __members = [ _gameId, _gameReportingId ];
    }

    public override Tdf CreateNew() => new NotifyGameReportingIdChange();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "NotifyGameReportingIdChange";
    public override string GetFullClassName() => "Blaze::GameManager::NotifyGameReportingIdChange";

    public ulong GameId
    {
        get => _gameId.Value;
        set => _gameId.Value = value;
    }

    public ulong GameReportingId
    {
        get => _gameReportingId.Value;
        set => _gameReportingId.Value = value;
    }

}
