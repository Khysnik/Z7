using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class PreferredGamesRuleCriteria : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("PreferredList", "mPreferredList", 0xC2CCF400, TdfType.List, 0, true), // Tag: PLST
        new TdfMemberInfo("RequirePreferredGame", "mRequirePreferredGame", 0xCA5C7000, TdfType.Bool, 1, true), // Tag: REQP
    ];
    private ITdfMember[] __members;

    private TdfList<ulong> _preferredList = new(__typeInfos[0]);
    private TdfBool _requirePreferredGame = new(__typeInfos[1]);

    public PreferredGamesRuleCriteria()
    {
        __members = [ _preferredList, _requirePreferredGame ];
    }

    public override Tdf CreateNew() => new PreferredGamesRuleCriteria();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "PreferredGamesRuleCriteria";
    public override string GetFullClassName() => "Blaze::GameManager::PreferredGamesRuleCriteria";

    public IList<ulong> PreferredList
    {
        get => _preferredList.Value;
        set => _preferredList.Value = value;
    }

    public bool RequirePreferredGame
    {
        get => _requirePreferredGame.Value;
        set => _requirePreferredGame.Value = value;
    }

}
