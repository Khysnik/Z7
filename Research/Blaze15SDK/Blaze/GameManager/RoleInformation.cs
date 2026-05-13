using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class RoleInformation : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("RoleCriteriaMap", "mRoleCriteriaMap", 0x8F2A7400, TdfType.Map, 0, true), // Tag: CRIT
        new TdfMemberInfo("MultiRoleCriteria", "mMultiRoleCriteria", 0xCA3CB400, TdfType.Map, 1, true), // Tag: RCRT
    ];
    private ITdfMember[] __members;

    private TdfMap<string, Blaze15SDK.Blaze.GameManager.RoleCriteria> _roleCriteriaMap = new(__typeInfos[0]);
    private TdfMap<string, string> _multiRoleCriteria = new(__typeInfos[1]);

    public RoleInformation()
    {
        __members = [ _roleCriteriaMap, _multiRoleCriteria ];
    }

    public override Tdf CreateNew() => new RoleInformation();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "RoleInformation";
    public override string GetFullClassName() => "Blaze::GameManager::RoleInformation";

    public IDictionary<string, Blaze15SDK.Blaze.GameManager.RoleCriteria> RoleCriteriaMap
    {
        get => _roleCriteriaMap.Value;
        set => _roleCriteriaMap.Value = value;
    }

    public IDictionary<string, string> MultiRoleCriteria
    {
        get => _multiRoleCriteria.Value;
        set => _multiRoleCriteria.Value = value;
    }

}
