using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class DestroyGamesRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("GameIdList", "mGameIdList", 0x9E992C00, TdfType.List, 0, true), // Tag: GIDL
        new TdfMemberInfo("DestructionReason", "mDestructionReason", 0xCA587300, TdfType.Enum, 1, true), // Tag: REAS
    ];
    private ITdfMember[] __members;

    private TdfList<ulong> _gameIdList = new(__typeInfos[0]);
    private TdfEnum<Blaze15SDK.Blaze.GameManager.GameDestructionReason> _destructionReason = new(__typeInfos[1]);

    public DestroyGamesRequest()
    {
        __members = [ _gameIdList, _destructionReason ];
    }

    public override Tdf CreateNew() => new DestroyGamesRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "DestroyGamesRequest";
    public override string GetFullClassName() => "Blaze::GameManager::DestroyGamesRequest";

    public IList<ulong> GameIdList
    {
        get => _gameIdList.Value;
        set => _gameIdList.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.GameDestructionReason DestructionReason
    {
        get => _destructionReason.Value;
        set => _destructionReason.Value = value;
    }

}
