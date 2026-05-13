using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class GameActivity : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("SessionIdentification", "mSessionIdentification", 0x973A6400, TdfType.Struct, 0, true), // Tag: ESID
        new TdfMemberInfo("ExternalSessionName", "mExternalSessionName", 0x973BAD00, TdfType.String, 1, true), // Tag: ESNM
        new TdfMemberInfo("GameType", "mGameType", 0x9E7D3900, TdfType.Enum, 2, true), // Tag: GGTY
        new TdfMemberInfo("GameId", "mGameId", 0x9E990000, TdfType.UInt64, 3, true), // Tag: GID
        new TdfMemberInfo("Player", "mPlayer", 0xC2CE7200, TdfType.Struct, 4, true), // Tag: PLYR
        new TdfMemberInfo("PresenceMode", "mPresenceMode", 0xC3297300, TdfType.Enum, 5, true), // Tag: PRES
        new TdfMemberInfo("PlayerState", "mPlayerState", 0xC33D2100, TdfType.Enum, 6, true), // Tag: PSTA
        new TdfMemberInfo("ExternalSessionTemplateName", "mExternalSessionTemplateName", 0xCF4B6E00, TdfType.String, 7, true), // Tag: STMN
        new TdfMemberInfo("JoinedGameTimestamp", "mJoinedGameTimestamp", 0xD29B6500, TdfType.Int64, 8, true), // Tag: TIME
    ];
    private ITdfMember[] __members;

    private TdfStruct<Blaze15SDK.Blaze.ExternalSessionIdentification?> _sessionIdentification = new(__typeInfos[0]);
    private TdfString _externalSessionName = new(__typeInfos[1]);
    private TdfEnum<Blaze15SDK.Blaze.GameManager.GameType> _gameType = new(__typeInfos[2]);
    private TdfUInt64 _gameId = new(__typeInfos[3]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.PlayerPresenceData?> _player = new(__typeInfos[4]);
    private TdfEnum<Blaze15SDK.Blaze.PresenceMode> _presenceMode = new(__typeInfos[5]);
    private TdfEnum<Blaze15SDK.Blaze.GameManager.PlayerState> _playerState = new(__typeInfos[6]);
    private TdfString _externalSessionTemplateName = new(__typeInfos[7]);
    private TdfInt64 _joinedGameTimestamp = new(__typeInfos[8]);

    public GameActivity()
    {
        __members = [ _sessionIdentification, _externalSessionName, _gameType, _gameId, _player, _presenceMode, _playerState, _externalSessionTemplateName, _joinedGameTimestamp ];
    }

    public override Tdf CreateNew() => new GameActivity();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GameActivity";
    public override string GetFullClassName() => "Blaze::GameManager::GameActivity";

    public Blaze15SDK.Blaze.ExternalSessionIdentification? SessionIdentification
    {
        get => _sessionIdentification.Value;
        set => _sessionIdentification.Value = value;
    }

    public string ExternalSessionName
    {
        get => _externalSessionName.Value;
        set => _externalSessionName.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.GameType GameType
    {
        get => _gameType.Value;
        set => _gameType.Value = value;
    }

    public ulong GameId
    {
        get => _gameId.Value;
        set => _gameId.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.PlayerPresenceData? Player
    {
        get => _player.Value;
        set => _player.Value = value;
    }

    public Blaze15SDK.Blaze.PresenceMode PresenceMode
    {
        get => _presenceMode.Value;
        set => _presenceMode.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.PlayerState PlayerState
    {
        get => _playerState.Value;
        set => _playerState.Value = value;
    }

    public string ExternalSessionTemplateName
    {
        get => _externalSessionTemplateName.Value;
        set => _externalSessionTemplateName.Value = value;
    }

    public long JoinedGameTimestamp
    {
        get => _joinedGameTimestamp.Value;
        set => _joinedGameTimestamp.Value = value;
    }

}
