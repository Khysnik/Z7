using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class UpdateGameSessionRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("GameId", "mGameId", 0x9E990000, TdfType.UInt64, 0, true), // Tag: GID
        new TdfMemberInfo("NpSessionId", "mNpSessionId", 0xBB0CE900, TdfType.String, 1, true), // Tag: NPSI
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _gameId = new(__typeInfos[0]);
    private TdfString _npSessionId = new(__typeInfos[1]);

    public UpdateGameSessionRequest()
    {
        __members = [ _gameId, _npSessionId ];
    }

    public override Tdf CreateNew() => new UpdateGameSessionRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "UpdateGameSessionRequest";
    public override string GetFullClassName() => "Blaze::GameManager::UpdateGameSessionRequest";

    public ulong GameId
    {
        get => _gameId.Value;
        set => _gameId.Value = value;
    }

    public string NpSessionId
    {
        get => _npSessionId.Value;
        set => _npSessionId.Value = value;
    }

}
