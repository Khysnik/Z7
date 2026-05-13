using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class BanPlayerRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("PlayerRemovedTitleContext", "mPlayerRemovedTitleContext", 0x8EED3800, TdfType.UInt16, 0, true), // Tag: CNTX
        new TdfMemberInfo("GameId", "mGameId", 0x9E990000, TdfType.UInt64, 1, true), // Tag: GID
        new TdfMemberInfo("PlayerIds", "mPlayerIds", 0xC2CCF400, TdfType.List, 2, true), // Tag: PLST
    ];
    private ITdfMember[] __members;

    private TdfUInt16 _playerRemovedTitleContext = new(__typeInfos[0]);
    private TdfUInt64 _gameId = new(__typeInfos[1]);
    private TdfList<long> _playerIds = new(__typeInfos[2]);

    public BanPlayerRequest()
    {
        __members = [ _playerRemovedTitleContext, _gameId, _playerIds ];
    }

    public override Tdf CreateNew() => new BanPlayerRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "BanPlayerRequest";
    public override string GetFullClassName() => "Blaze::GameManager::BanPlayerRequest";

    public ushort PlayerRemovedTitleContext
    {
        get => _playerRemovedTitleContext.Value;
        set => _playerRemovedTitleContext.Value = value;
    }

    public ulong GameId
    {
        get => _gameId.Value;
        set => _gameId.Value = value;
    }

    public IList<long> PlayerIds
    {
        get => _playerIds.Value;
        set => _playerIds.Value = value;
    }

}
