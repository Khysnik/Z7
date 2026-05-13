using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Stats;

public class WipeStatsRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("CategoryName", "mCategoryName", 0x8E1D0000, TdfType.String, 0, true), // Tag: CAT
        new TdfMemberInfo("EntityId", "mEntityId", 0x96990000, TdfType.UInt64, 1, true), // Tag: EID
        new TdfMemberInfo("EntityObjectId", "mEntityObjectId", 0x974E7000, TdfType.ObjectId, 2, true), // Tag: ETYP
        new TdfMemberInfo("KeyScopeNameValueMap", "mKeyScopeNameValueMap", 0xAF3D6D00, TdfType.Map, 3, true), // Tag: KSUM
        new TdfMemberInfo("Operation", "mOperation", 0xBF097200, TdfType.Enum, 4, true), // Tag: OPER
    ];
    private ITdfMember[] __members;

    private TdfString _categoryName = new(__typeInfos[0]);
    private TdfUInt64 _entityId = new(__typeInfos[1]);
    private TdfObjectId _entityObjectId = new(__typeInfos[2]);
    private TdfMap<string, long> _keyScopeNameValueMap = new(__typeInfos[3]);
    private TdfEnum<Blaze15SDK.Blaze.Stats.WipeStatsOperation> _operation = new(__typeInfos[4]);

    public WipeStatsRequest()
    {
        __members = [ _categoryName, _entityId, _entityObjectId, _keyScopeNameValueMap, _operation ];
    }

    public override Tdf CreateNew() => new WipeStatsRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "WipeStatsRequest";
    public override string GetFullClassName() => "Blaze::Stats::WipeStatsRequest";

    public string CategoryName
    {
        get => _categoryName.Value;
        set => _categoryName.Value = value;
    }

    public ulong EntityId
    {
        get => _entityId.Value;
        set => _entityId.Value = value;
    }

    public ObjectId EntityObjectId
    {
        get => _entityObjectId.Value;
        set => _entityObjectId.Value = value;
    }

    public IDictionary<string, long> KeyScopeNameValueMap
    {
        get => _keyScopeNameValueMap.Value;
        set => _keyScopeNameValueMap.Value = value;
    }

    public Blaze15SDK.Blaze.Stats.WipeStatsOperation Operation
    {
        get => _operation.Value;
        set => _operation.Value = value;
    }

}
