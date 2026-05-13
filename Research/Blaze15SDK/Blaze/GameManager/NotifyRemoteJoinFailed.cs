using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class NotifyRemoteJoinFailed : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("GameId", "mGameId", 0x9E990000, TdfType.UInt64, 0, true), // Tag: GID
        new TdfMemberInfo("RemoteUserInfo", "mRemoteUserInfo", 0xA6E9AF00, TdfType.Struct, 1, true), // Tag: INFO
        new TdfMemberInfo("JoinError", "mJoinError", 0xAA5CB200, TdfType.UInt32, 2, true), // Tag: JERR
        new TdfMemberInfo("UserSessionId", "mUserSessionId", 0xD73A6400, TdfType.UInt64, 3, true), // Tag: USID
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _gameId = new(__typeInfos[0]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.RemoteInitiatorInformation?> _remoteUserInfo = new(__typeInfos[1]);
    private TdfUInt32 _joinError = new(__typeInfos[2]);
    private TdfUInt64 _userSessionId = new(__typeInfos[3]);

    public NotifyRemoteJoinFailed()
    {
        __members = [ _gameId, _remoteUserInfo, _joinError, _userSessionId ];
    }

    public override Tdf CreateNew() => new NotifyRemoteJoinFailed();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "NotifyRemoteJoinFailed";
    public override string GetFullClassName() => "Blaze::GameManager::NotifyRemoteJoinFailed";

    public ulong GameId
    {
        get => _gameId.Value;
        set => _gameId.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.RemoteInitiatorInformation? RemoteUserInfo
    {
        get => _remoteUserInfo.Value;
        set => _remoteUserInfo.Value = value;
    }

    public uint JoinError
    {
        get => _joinError.Value;
        set => _joinError.Value = value;
    }

    public ulong UserSessionId
    {
        get => _userSessionId.Value;
        set => _userSessionId.Value = value;
    }

}
