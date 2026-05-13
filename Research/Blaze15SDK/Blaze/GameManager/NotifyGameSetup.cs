using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class NotifyGameSetup : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("GameData", "mGameData", 0x9E1B6500, TdfType.Struct, 0, true), // Tag: GAME
        new TdfMemberInfo("IsLockableForPreferredJoins", "mIsLockableForPreferredJoins", 0xB26C2A00, TdfType.Bool, 1, true), // Tag: LFPJ
        new TdfMemberInfo("GameModeAttributeName", "mGameModeAttributeName", 0xB6E86D00, TdfType.String, 2, true), // Tag: MNAM
        new TdfMemberInfo("GameRoster", "mGameRoster", 0xC32BF300, TdfType.List, 3, true), // Tag: PROS
        new TdfMemberInfo("QosSettings", "mQosSettings", 0xC6FCF300, TdfType.Struct, 4, true), // Tag: QOSS
        new TdfMemberInfo("PerformQosValidation", "mPerformQosValidation", 0xC6FCF600, TdfType.Bool, 5, true), // Tag: QOSV
        new TdfMemberInfo("GameQueue", "mGameQueue", 0xC7597500, TdfType.List, 6, true), // Tag: QUEU
        new TdfMemberInfo("GameSetupReason", "gameSetupReason", 0xCA587300, TdfType.Union, 7, true), // Tag: REAS
        new TdfMemberInfo("QosTelemetryInterval", "mQosTelemetryInterval", 0xD25B2D00, TdfType.Int64, 8, true), // Tag: TELM
    ];
    private ITdfMember[] __members;

    private TdfStruct<Blaze15SDK.Blaze.GameManager.ReplicatedGameData?> _gameData = new(__typeInfos[0]);
    private TdfBool _isLockableForPreferredJoins = new(__typeInfos[1]);
    private TdfString _gameModeAttributeName = new(__typeInfos[2]);
    private TdfList<Blaze15SDK.Blaze.GameManager.ReplicatedGamePlayer> _gameRoster = new(__typeInfos[3]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.QosSettings?> _qosSettings = new(__typeInfos[4]);
    private TdfBool _performQosValidation = new(__typeInfos[5]);
    private TdfList<Blaze15SDK.Blaze.GameManager.ReplicatedGamePlayer> _gameQueue = new(__typeInfos[6]);
    private TdfUnion<Blaze15SDK.Blaze.GameManager.GameSetupReason> _gameSetupReason = new(__typeInfos[7]);
    private TdfInt64 _qosTelemetryInterval = new(__typeInfos[8]);

    public NotifyGameSetup()
    {
        __members = [ _gameData, _isLockableForPreferredJoins, _gameModeAttributeName, _gameRoster, _qosSettings, _performQosValidation, _gameQueue, _gameSetupReason, _qosTelemetryInterval ];
    }

    public override Tdf CreateNew() => new NotifyGameSetup();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "NotifyGameSetup";
    public override string GetFullClassName() => "Blaze::GameManager::NotifyGameSetup";

    public Blaze15SDK.Blaze.GameManager.ReplicatedGameData? GameData
    {
        get => _gameData.Value;
        set => _gameData.Value = value;
    }

    public bool IsLockableForPreferredJoins
    {
        get => _isLockableForPreferredJoins.Value;
        set => _isLockableForPreferredJoins.Value = value;
    }

    public string GameModeAttributeName
    {
        get => _gameModeAttributeName.Value;
        set => _gameModeAttributeName.Value = value;
    }

    public IList<Blaze15SDK.Blaze.GameManager.ReplicatedGamePlayer> GameRoster
    {
        get => _gameRoster.Value;
        set => _gameRoster.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.QosSettings? QosSettings
    {
        get => _qosSettings.Value;
        set => _qosSettings.Value = value;
    }

    public bool PerformQosValidation
    {
        get => _performQosValidation.Value;
        set => _performQosValidation.Value = value;
    }

    public IList<Blaze15SDK.Blaze.GameManager.ReplicatedGamePlayer> GameQueue
    {
        get => _gameQueue.Value;
        set => _gameQueue.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.GameSetupReason GameSetupReason
    {
        get => _gameSetupReason.Value;
        set => _gameSetupReason.Value = value;
    }

    public long QosTelemetryInterval
    {
        get => _qosTelemetryInterval.Value;
        set => _qosTelemetryInterval.Value = value;
    }

}
