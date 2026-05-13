using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Stats;

public class EntityStats : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("EntityId", "mEntityId", 0x96990000, TdfType.UInt64, 0, true), // Tag: EID
        new TdfMemberInfo("EntityType", "mEntityType", 0x974E7000, TdfType.ObjectType, 1, true), // Tag: ETYP
        new TdfMemberInfo("PeriodOffset", "mPeriodOffset", 0xC2F9A600, TdfType.Int32, 2, true), // Tag: POFF
        new TdfMemberInfo("StatValues", "mStatValues", 0xCF487400, TdfType.List, 3, true), // Tag: STAT
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _entityId = new(__typeInfos[0]);
    private TdfObjectType _entityType = new(__typeInfos[1]);
    private TdfInt32 _periodOffset = new(__typeInfos[2]);
    private TdfList<string> _statValues = new(__typeInfos[3]);

    public EntityStats()
    {
        __members = [ _entityId, _entityType, _periodOffset, _statValues ];
    }

    public override Tdf CreateNew() => new EntityStats();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "EntityStats";
    public override string GetFullClassName() => "Blaze::Stats::EntityStats";

    public ulong EntityId
    {
        get => _entityId.Value;
        set => _entityId.Value = value;
    }

    public ObjectType EntityType
    {
        get => _entityType.Value;
        set => _entityType.Value = value;
    }

    public int PeriodOffset
    {
        get => _periodOffset.Value;
        set => _periodOffset.Value = value;
    }

    public IList<string> StatValues
    {
        get => _statValues.Value;
        set => _statValues.Value = value;
    }

}
