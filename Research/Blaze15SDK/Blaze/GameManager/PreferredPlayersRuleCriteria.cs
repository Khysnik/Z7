using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class PreferredPlayersRuleCriteria : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("PreferredAccountList", "mPreferredAccountList", 0x86CCF400, TdfType.List, 0, true), // Tag: ALST
        new TdfMemberInfo("PreferredList", "mPreferredList", 0xC2CCF400, TdfType.List, 1, true), // Tag: PLST
        new TdfMemberInfo("PreferredListId", "mPreferredListId", 0xC3397400, TdfType.ObjectId, 2, true), // Tag: PSET
        new TdfMemberInfo("RequirePreferredPlayer", "mRequirePreferredPlayer", 0xCA5C7000, TdfType.Bool, 3, true), // Tag: REQP
    ];
    private ITdfMember[] __members;

    private TdfList<long> _preferredAccountList = new(__typeInfos[0]);
    private TdfList<long> _preferredList = new(__typeInfos[1]);
    private TdfObjectId _preferredListId = new(__typeInfos[2]);
    private TdfBool _requirePreferredPlayer = new(__typeInfos[3]);

    public PreferredPlayersRuleCriteria()
    {
        __members = [ _preferredAccountList, _preferredList, _preferredListId, _requirePreferredPlayer ];
    }

    public override Tdf CreateNew() => new PreferredPlayersRuleCriteria();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "PreferredPlayersRuleCriteria";
    public override string GetFullClassName() => "Blaze::GameManager::PreferredPlayersRuleCriteria";

    public IList<long> PreferredAccountList
    {
        get => _preferredAccountList.Value;
        set => _preferredAccountList.Value = value;
    }

    public IList<long> PreferredList
    {
        get => _preferredList.Value;
        set => _preferredList.Value = value;
    }

    public ObjectId PreferredListId
    {
        get => _preferredListId.Value;
        set => _preferredListId.Value = value;
    }

    public bool RequirePreferredPlayer
    {
        get => _requirePreferredPlayer.Value;
        set => _requirePreferredPlayer.Value = value;
    }

}
