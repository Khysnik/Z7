using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class MeshEndpointsDisconnectedRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("PlayerNetConnectionFlags", "mPlayerNetConnectionFlags", 0x9AC9F300, TdfType.Enum, 0, true), // Tag: FLGS
        new TdfMemberInfo("GameId", "mGameId", 0x9E990000, TdfType.UInt64, 1, true), // Tag: GID
        new TdfMemberInfo("PlayerNetConnectionStatus", "mPlayerNetConnectionStatus", 0xCF487400, TdfType.Enum, 2, true), // Tag: STAT
        new TdfMemberInfo("TargetGroupId", "mTargetGroupId", 0xD239C000, TdfType.ObjectId, 3, true), // Tag: TCG
    ];
    private ITdfMember[] __members;

    private TdfEnum<Blaze15SDK.Blaze.GameManager.PlayerNetConnectionFlags> _playerNetConnectionFlags = new(__typeInfos[0]);
    private TdfUInt64 _gameId = new(__typeInfos[1]);
    private TdfEnum<Blaze15SDK.Blaze.GameManager.PlayerNetConnectionStatus> _playerNetConnectionStatus = new(__typeInfos[2]);
    private TdfObjectId _targetGroupId = new(__typeInfos[3]);

    public MeshEndpointsDisconnectedRequest()
    {
        __members = [ _playerNetConnectionFlags, _gameId, _playerNetConnectionStatus, _targetGroupId ];
    }

    public override Tdf CreateNew() => new MeshEndpointsDisconnectedRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "MeshEndpointsDisconnectedRequest";
    public override string GetFullClassName() => "Blaze::GameManager::MeshEndpointsDisconnectedRequest";

    public Blaze15SDK.Blaze.GameManager.PlayerNetConnectionFlags PlayerNetConnectionFlags
    {
        get => _playerNetConnectionFlags.Value;
        set => _playerNetConnectionFlags.Value = value;
    }

    public ulong GameId
    {
        get => _gameId.Value;
        set => _gameId.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.PlayerNetConnectionStatus PlayerNetConnectionStatus
    {
        get => _playerNetConnectionStatus.Value;
        set => _playerNetConnectionStatus.Value = value;
    }

    public ObjectId TargetGroupId
    {
        get => _targetGroupId.Value;
        set => _targetGroupId.Value = value;
    }

}
