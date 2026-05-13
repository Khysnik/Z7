using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class ExternalHttpGamePlayerEventData : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("EncryptedBlazeId", "mEncryptedBlazeId", 0x96E8F200, TdfType.String, 0, true), // Tag: ENCR
        new TdfMemberInfo("BlazeId", "mBlazeId", 0xC2990000, TdfType.Int64, 1, true), // Tag: PID
        new TdfMemberInfo("PlayerState", "mPlayerState", 0xCF487400, TdfType.Enum, 2, true), // Tag: STAT
    ];
    private ITdfMember[] __members;

    private TdfString _encryptedBlazeId = new(__typeInfos[0]);
    private TdfInt64 _blazeId = new(__typeInfos[1]);
    private TdfEnum<Blaze15SDK.Blaze.GameManager.PlayerState> _playerState = new(__typeInfos[2]);

    public ExternalHttpGamePlayerEventData()
    {
        __members = [ _encryptedBlazeId, _blazeId, _playerState ];
    }

    public override Tdf CreateNew() => new ExternalHttpGamePlayerEventData();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ExternalHttpGamePlayerEventData";
    public override string GetFullClassName() => "Blaze::GameManager::ExternalHttpGamePlayerEventData";

    public string EncryptedBlazeId
    {
        get => _encryptedBlazeId.Value;
        set => _encryptedBlazeId.Value = value;
    }

    public long BlazeId
    {
        get => _blazeId.Value;
        set => _blazeId.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.PlayerState PlayerState
    {
        get => _playerState.Value;
        set => _playerState.Value = value;
    }

}
