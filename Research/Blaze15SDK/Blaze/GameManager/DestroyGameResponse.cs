using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class DestroyGameResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("GameId", "mGameId", 0x9E990000, TdfType.UInt64, 0, true), // Tag: GID
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _gameId = new(__typeInfos[0]);

    public DestroyGameResponse()
    {
        __members = [ _gameId ];
    }

    public override Tdf CreateNew() => new DestroyGameResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "DestroyGameResponse";
    public override string GetFullClassName() => "Blaze::GameManager::DestroyGameResponse";

    public ulong GameId
    {
        get => _gameId.Value;
        set => _gameId.Value = value;
    }

}
