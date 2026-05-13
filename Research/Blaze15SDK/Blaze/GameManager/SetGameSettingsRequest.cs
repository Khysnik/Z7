using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class SetGameSettingsRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("GameId", "mGameId", 0x9E990000, TdfType.UInt64, 0, true), // Tag: GID
        new TdfMemberInfo("GameSettings", "mGameSettings", 0x9F397400, TdfType.Enum, 1, true), // Tag: GSET
        new TdfMemberInfo("GameSettingsMask", "mGameSettingsMask", 0x9F3D2D00, TdfType.Enum, 2, true), // Tag: GSTM
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _gameId = new(__typeInfos[0]);
    private TdfEnum<Blaze15SDK.Blaze.GameManager.GameSettings> _gameSettings = new(__typeInfos[1]);
    private TdfEnum<Blaze15SDK.Blaze.GameManager.GameSettings> _gameSettingsMask = new(__typeInfos[2]);

    public SetGameSettingsRequest()
    {
        __members = [ _gameId, _gameSettings, _gameSettingsMask ];
    }

    public override Tdf CreateNew() => new SetGameSettingsRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "SetGameSettingsRequest";
    public override string GetFullClassName() => "Blaze::GameManager::SetGameSettingsRequest";

    public ulong GameId
    {
        get => _gameId.Value;
        set => _gameId.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.GameSettings GameSettings
    {
        get => _gameSettings.Value;
        set => _gameSettings.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.GameSettings GameSettingsMask
    {
        get => _gameSettingsMask.Value;
        set => _gameSettingsMask.Value = value;
    }

}
