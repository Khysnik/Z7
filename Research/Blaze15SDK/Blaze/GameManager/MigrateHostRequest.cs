using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class MigrateHostRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("GameId", "mGameId", 0x9E990000, TdfType.UInt64, 0, true), // Tag: GID
        new TdfMemberInfo("NewHostPlayer", "mNewHostPlayer", 0xA2FCF400, TdfType.Int64, 1, true), // Tag: HOST
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _gameId = new(__typeInfos[0]);
    private TdfInt64 _newHostPlayer = new(__typeInfos[1]);

    public MigrateHostRequest()
    {
        __members = [ _gameId, _newHostPlayer ];
    }

    public override Tdf CreateNew() => new MigrateHostRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "MigrateHostRequest";
    public override string GetFullClassName() => "Blaze::GameManager::MigrateHostRequest";

    public ulong GameId
    {
        get => _gameId.Value;
        set => _gameId.Value = value;
    }

    public long NewHostPlayer
    {
        get => _newHostPlayer.Value;
        set => _newHostPlayer.Value = value;
    }

}
