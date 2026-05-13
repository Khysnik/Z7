using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class RemovePlayerRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("GroupId", "mGroupId", 0x8B4C2C00, TdfType.ObjectId, 0, true), // Tag: BTPL
        new TdfMemberInfo("PlayerRemovedTitleContext", "mPlayerRemovedTitleContext", 0x8EED3800, TdfType.UInt16, 1, true), // Tag: CNTX
        new TdfMemberInfo("GameId", "mGameId", 0x9E990000, TdfType.UInt64, 2, true), // Tag: GID
        new TdfMemberInfo("PlayerId", "mPlayerId", 0xC2990000, TdfType.Int64, 3, true), // Tag: PID
        new TdfMemberInfo("PlayerRemovedReason", "mPlayerRemovedReason", 0xCA587300, TdfType.Enum, 4, true), // Tag: REAS
        new TdfMemberInfo("TitleContextString", "mTitleContextString", 0xCE3D3800, TdfType.String, 5, true), // Tag: SCTX
    ];
    private ITdfMember[] __members;

    private TdfObjectId _groupId = new(__typeInfos[0]);
    private TdfUInt16 _playerRemovedTitleContext = new(__typeInfos[1]);
    private TdfUInt64 _gameId = new(__typeInfos[2]);
    private TdfInt64 _playerId = new(__typeInfos[3]);
    private TdfEnum<Blaze15SDK.Blaze.GameManager.PlayerRemovedReason> _playerRemovedReason = new(__typeInfos[4]);
    private TdfString _titleContextString = new(__typeInfos[5]);

    public RemovePlayerRequest()
    {
        __members = [ _groupId, _playerRemovedTitleContext, _gameId, _playerId, _playerRemovedReason, _titleContextString ];
    }

    public override Tdf CreateNew() => new RemovePlayerRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "RemovePlayerRequest";
    public override string GetFullClassName() => "Blaze::GameManager::RemovePlayerRequest";

    public ObjectId GroupId
    {
        get => _groupId.Value;
        set => _groupId.Value = value;
    }

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

    public long PlayerId
    {
        get => _playerId.Value;
        set => _playerId.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.PlayerRemovedReason PlayerRemovedReason
    {
        get => _playerRemovedReason.Value;
        set => _playerRemovedReason.Value = value;
    }

    public string TitleContextString
    {
        get => _titleContextString.Value;
        set => _titleContextString.Value = value;
    }

}
