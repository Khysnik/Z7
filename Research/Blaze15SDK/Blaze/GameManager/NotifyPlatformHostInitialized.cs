using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class NotifyPlatformHostInitialized : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("GameId", "mGameId", 0x9E990000, TdfType.UInt64, 0, true), // Tag: GID
        new TdfMemberInfo("PlatformHostId", "mPlatformHostId", 0xC28A6400, TdfType.Int64, 1, true), // Tag: PHID
        new TdfMemberInfo("PlatformHostSlotId", "mPlatformHostSlotId", 0xC28CF400, TdfType.UInt8, 2, true), // Tag: PHST
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _gameId = new(__typeInfos[0]);
    private TdfInt64 _platformHostId = new(__typeInfos[1]);
    private TdfUInt8 _platformHostSlotId = new(__typeInfos[2]);

    public NotifyPlatformHostInitialized()
    {
        __members = [ _gameId, _platformHostId, _platformHostSlotId ];
    }

    public override Tdf CreateNew() => new NotifyPlatformHostInitialized();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "NotifyPlatformHostInitialized";
    public override string GetFullClassName() => "Blaze::GameManager::NotifyPlatformHostInitialized";

    public ulong GameId
    {
        get => _gameId.Value;
        set => _gameId.Value = value;
    }

    public long PlatformHostId
    {
        get => _platformHostId.Value;
        set => _platformHostId.Value = value;
    }

    public byte PlatformHostSlotId
    {
        get => _platformHostSlotId.Value;
        set => _platformHostSlotId.Value = value;
    }

}
