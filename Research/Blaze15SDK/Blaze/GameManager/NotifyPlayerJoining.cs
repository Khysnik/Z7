using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class NotifyPlayerJoining : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("GameId", "mGameId", 0x9E990000, TdfType.UInt64, 0, true), // Tag: GID
        new TdfMemberInfo("JoiningPlayer", "mJoiningPlayer", 0xC2487400, TdfType.Struct, 1, true), // Tag: PDAT
        new TdfMemberInfo("ValidateQosForJoiningPlayer", "mValidateQosForJoiningPlayer", 0xC6FCF400, TdfType.Bool, 2, true), // Tag: QOST
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _gameId = new(__typeInfos[0]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.ReplicatedGamePlayer?> _joiningPlayer = new(__typeInfos[1]);
    private TdfBool _validateQosForJoiningPlayer = new(__typeInfos[2]);

    public NotifyPlayerJoining()
    {
        __members = [ _gameId, _joiningPlayer, _validateQosForJoiningPlayer ];
    }

    public override Tdf CreateNew() => new NotifyPlayerJoining();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "NotifyPlayerJoining";
    public override string GetFullClassName() => "Blaze::GameManager::NotifyPlayerJoining";

    public ulong GameId
    {
        get => _gameId.Value;
        set => _gameId.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.ReplicatedGamePlayer? JoiningPlayer
    {
        get => _joiningPlayer.Value;
        set => _joiningPlayer.Value = value;
    }

    public bool ValidateQosForJoiningPlayer
    {
        get => _validateQosForJoiningPlayer.Value;
        set => _validateQosForJoiningPlayer.Value = value;
    }

}
