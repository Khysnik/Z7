using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class SetGameModRegisterRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("GameId", "mGameId", 0x9EDA6400, TdfType.UInt64, 0, true), // Tag: GMID
        new TdfMemberInfo("GameModRegister", "mGameModRegister", 0x9EDCA700, TdfType.UInt32, 1, true), // Tag: GMRG
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _gameId = new(__typeInfos[0]);
    private TdfUInt32 _gameModRegister = new(__typeInfos[1]);

    public SetGameModRegisterRequest()
    {
        __members = [ _gameId, _gameModRegister ];
    }

    public override Tdf CreateNew() => new SetGameModRegisterRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "SetGameModRegisterRequest";
    public override string GetFullClassName() => "Blaze::GameManager::SetGameModRegisterRequest";

    public ulong GameId
    {
        get => _gameId.Value;
        set => _gameId.Value = value;
    }

    public uint GameModRegister
    {
        get => _gameModRegister.Value;
        set => _gameModRegister.Value = value;
    }

}
