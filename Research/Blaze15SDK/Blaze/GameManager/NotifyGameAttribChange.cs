using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class NotifyGameAttribChange : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("GameId", "mGameId", 0x9E990000, TdfType.UInt64, 0, true), // Tag: GID
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _gameId = new(__typeInfos[0]);

    public NotifyGameAttribChange()
    {
        __members = [ _gameId ];
    }

    public override Tdf CreateNew() => new NotifyGameAttribChange();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "NotifyGameAttribChange";
    public override string GetFullClassName() => "Blaze::GameManager::NotifyGameAttribChange";

    public ulong GameId
    {
        get => _gameId.Value;
        set => _gameId.Value = value;
    }

}
