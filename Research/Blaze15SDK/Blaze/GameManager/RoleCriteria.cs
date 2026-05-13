using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class RoleCriteria : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("RoleEntryCriteriaMap", "mRoleEntryCriteriaMap", 0x8F2A7400, TdfType.Map, 0, true), // Tag: CRIT
        new TdfMemberInfo("RoleCapacity", "mRoleCapacity", 0xCA387000, TdfType.UInt16, 1, true), // Tag: RCAP
    ];
    private ITdfMember[] __members;

    private TdfMap<string, string> _roleEntryCriteriaMap = new(__typeInfos[0]);
    private TdfUInt16 _roleCapacity = new(__typeInfos[1]);

    public RoleCriteria()
    {
        __members = [ _roleEntryCriteriaMap, _roleCapacity ];
    }

    public override Tdf CreateNew() => new RoleCriteria();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "RoleCriteria";
    public override string GetFullClassName() => "Blaze::GameManager::RoleCriteria";

    public IDictionary<string, string> RoleEntryCriteriaMap
    {
        get => _roleEntryCriteriaMap.Value;
        set => _roleEntryCriteriaMap.Value = value;
    }

    public ushort RoleCapacity
    {
        get => _roleCapacity.Value;
        set => _roleCapacity.Value = value;
    }

}
