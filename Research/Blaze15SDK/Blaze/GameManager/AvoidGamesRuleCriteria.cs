using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class AvoidGamesRuleCriteria : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("GameIdList", "mGameIdList", 0x9E992C00, TdfType.List, 0, true), // Tag: GIDL
    ];
    private ITdfMember[] __members;

    private TdfList<ulong> _gameIdList = new(__typeInfos[0]);

    public AvoidGamesRuleCriteria()
    {
        __members = [ _gameIdList ];
    }

    public override Tdf CreateNew() => new AvoidGamesRuleCriteria();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "AvoidGamesRuleCriteria";
    public override string GetFullClassName() => "Blaze::GameManager::AvoidGamesRuleCriteria";

    public IList<ulong> GameIdList
    {
        get => _gameIdList.Value;
        set => _gameIdList.Value = value;
    }

}
