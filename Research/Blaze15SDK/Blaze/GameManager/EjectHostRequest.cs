using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class EjectHostRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("GameId", "mGameId", 0x9E990000, TdfType.UInt64, 0, true), // Tag: GID
        new TdfMemberInfo("ReplaceHost", "mReplaceHost", 0xCA5C2800, TdfType.Bool, 1, true), // Tag: REPH
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _gameId = new(__typeInfos[0]);
    private TdfBool _replaceHost = new(__typeInfos[1]);

    public EjectHostRequest()
    {
        __members = [ _gameId, _replaceHost ];
    }

    public override Tdf CreateNew() => new EjectHostRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "EjectHostRequest";
    public override string GetFullClassName() => "Blaze::GameManager::EjectHostRequest";

    public ulong GameId
    {
        get => _gameId.Value;
        set => _gameId.Value = value;
    }

    public bool ReplaceHost
    {
        get => _replaceHost.Value;
        set => _replaceHost.Value = value;
    }

}
