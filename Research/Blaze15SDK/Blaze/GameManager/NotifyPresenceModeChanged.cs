using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class NotifyPresenceModeChanged : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("GameId", "mGameId", 0x9E990000, TdfType.UInt64, 0, true), // Tag: GID
        new TdfMemberInfo("NewPresenceMode", "mNewPresenceMode", 0xC3297300, TdfType.Enum, 1, true), // Tag: PRES
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _gameId = new(__typeInfos[0]);
    private TdfEnum<Blaze15SDK.Blaze.PresenceMode> _newPresenceMode = new(__typeInfos[1]);

    public NotifyPresenceModeChanged()
    {
        __members = [ _gameId, _newPresenceMode ];
    }

    public override Tdf CreateNew() => new NotifyPresenceModeChanged();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "NotifyPresenceModeChanged";
    public override string GetFullClassName() => "Blaze::GameManager::NotifyPresenceModeChanged";

    public ulong GameId
    {
        get => _gameId.Value;
        set => _gameId.Value = value;
    }

    public Blaze15SDK.Blaze.PresenceMode NewPresenceMode
    {
        get => _newPresenceMode.Value;
        set => _newPresenceMode.Value = value;
    }

}
