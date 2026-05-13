using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class NotifyGameRemoved : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("GameId", "mGameId", 0x9E990000, TdfType.UInt64, 0, true), // Tag: GID
        new TdfMemberInfo("DestructionReason", "mDestructionReason", 0xCA587300, TdfType.Enum, 1, true), // Tag: REAS
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _gameId = new(__typeInfos[0]);
    private TdfEnum<Blaze15SDK.Blaze.GameManager.GameDestructionReason> _destructionReason = new(__typeInfos[1]);

    public NotifyGameRemoved()
    {
        __members = [ _gameId, _destructionReason ];
    }

    public override Tdf CreateNew() => new NotifyGameRemoved();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "NotifyGameRemoved";
    public override string GetFullClassName() => "Blaze::GameManager::NotifyGameRemoved";

    public ulong GameId
    {
        get => _gameId.Value;
        set => _gameId.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.GameDestructionReason DestructionReason
    {
        get => _destructionReason.Value;
        set => _destructionReason.Value = value;
    }

}
