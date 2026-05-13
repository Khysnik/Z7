using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class GameSessionUpdatedNotification : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("GameId", "mGameId", 0x9E990000, TdfType.UInt64, 0, true), // Tag: GID
        new TdfMemberInfo("OwnsFirstPartyPresence", "mOwnsFirstPartyPresence", 0xA30CB300, TdfType.Bool, 1, true), // Tag: HPRS
        new TdfMemberInfo("NpSessionId", "mNpSessionId", 0xBB0CE900, TdfType.String, 2, true), // Tag: NPSI
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _gameId = new(__typeInfos[0]);
    private TdfBool _ownsFirstPartyPresence = new(__typeInfos[1]);
    private TdfString _npSessionId = new(__typeInfos[2]);

    public GameSessionUpdatedNotification()
    {
        __members = [ _gameId, _ownsFirstPartyPresence, _npSessionId ];
    }

    public override Tdf CreateNew() => new GameSessionUpdatedNotification();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GameSessionUpdatedNotification";
    public override string GetFullClassName() => "Blaze::GameManager::GameSessionUpdatedNotification";

    public ulong GameId
    {
        get => _gameId.Value;
        set => _gameId.Value = value;
    }

    public bool OwnsFirstPartyPresence
    {
        get => _ownsFirstPartyPresence.Value;
        set => _ownsFirstPartyPresence.Value = value;
    }

    public string NpSessionId
    {
        get => _npSessionId.Value;
        set => _npSessionId.Value = value;
    }

}
