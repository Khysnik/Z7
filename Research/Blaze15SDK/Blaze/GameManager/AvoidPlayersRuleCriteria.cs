using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class AvoidPlayersRuleCriteria : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("AvoidAccountList", "mAvoidAccountList", 0x8638EC00, TdfType.List, 0, true), // Tag: ACCL
        new TdfMemberInfo("AvoidList", "mAvoidList", 0x86CCF400, TdfType.List, 1, true), // Tag: ALST
        new TdfMemberInfo("AvoidListIds", "mAvoidListIds", 0x873D3300, TdfType.List, 2, true), // Tag: ASTS
    ];
    private ITdfMember[] __members;

    private TdfList<long> _avoidAccountList = new(__typeInfos[0]);
    private TdfList<long> _avoidList = new(__typeInfos[1]);
    private TdfList<ObjectId> _avoidListIds = new(__typeInfos[2]);

    public AvoidPlayersRuleCriteria()
    {
        __members = [ _avoidAccountList, _avoidList, _avoidListIds ];
    }

    public override Tdf CreateNew() => new AvoidPlayersRuleCriteria();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "AvoidPlayersRuleCriteria";
    public override string GetFullClassName() => "Blaze::GameManager::AvoidPlayersRuleCriteria";

    public IList<long> AvoidAccountList
    {
        get => _avoidAccountList.Value;
        set => _avoidAccountList.Value = value;
    }

    public IList<long> AvoidList
    {
        get => _avoidList.Value;
        set => _avoidList.Value = value;
    }

    public IList<ObjectId> AvoidListIds
    {
        get => _avoidListIds.Value;
        set => _avoidListIds.Value = value;
    }

}
