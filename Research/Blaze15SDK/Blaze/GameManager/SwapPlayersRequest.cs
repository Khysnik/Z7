using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class SwapPlayersRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("GameId", "mGameId", 0x9E990000, TdfType.UInt64, 0, true), // Tag: GID
        new TdfMemberInfo("SwapPlayers", "mSwapPlayers", 0xB2786D00, TdfType.List, 1, true), // Tag: LGAM
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _gameId = new(__typeInfos[0]);
    private TdfList<Blaze15SDK.Blaze.GameManager.SwapPlayerData> _swapPlayers = new(__typeInfos[1]);

    public SwapPlayersRequest()
    {
        __members = [ _gameId, _swapPlayers ];
    }

    public override Tdf CreateNew() => new SwapPlayersRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "SwapPlayersRequest";
    public override string GetFullClassName() => "Blaze::GameManager::SwapPlayersRequest";

    public ulong GameId
    {
        get => _gameId.Value;
        set => _gameId.Value = value;
    }

    public IList<Blaze15SDK.Blaze.GameManager.SwapPlayerData> SwapPlayers
    {
        get => _swapPlayers.Value;
        set => _swapPlayers.Value = value;
    }

}
