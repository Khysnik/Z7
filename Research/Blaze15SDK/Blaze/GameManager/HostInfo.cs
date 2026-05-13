using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class HostInfo : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ConnectionGroupId", "mConnectionGroupId", 0x8EFBA700, TdfType.UInt64, 0, true), // Tag: CONG
        new TdfMemberInfo("ConnectionSlotId", "mConnectionSlotId", 0x8F3A6400, TdfType.UInt8, 1, true), // Tag: CSID
        new TdfMemberInfo("PlayerId", "mPlayerId", 0xA30A6400, TdfType.Int64, 2, true), // Tag: HPID
        new TdfMemberInfo("UserSessionId", "mUserSessionId", 0xA3397300, TdfType.UInt64, 3, true), // Tag: HSES
        new TdfMemberInfo("SlotId", "mSlotId", 0xA33B3400, TdfType.UInt8, 4, true), // Tag: HSLT
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _connectionGroupId = new(__typeInfos[0]);
    private TdfUInt8 _connectionSlotId = new(__typeInfos[1]);
    private TdfInt64 _playerId = new(__typeInfos[2]);
    private TdfUInt64 _userSessionId = new(__typeInfos[3]);
    private TdfUInt8 _slotId = new(__typeInfos[4]);

    public HostInfo()
    {
        __members = [ _connectionGroupId, _connectionSlotId, _playerId, _userSessionId, _slotId ];
    }

    public override Tdf CreateNew() => new HostInfo();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "HostInfo";
    public override string GetFullClassName() => "Blaze::GameManager::HostInfo";

    public ulong ConnectionGroupId
    {
        get => _connectionGroupId.Value;
        set => _connectionGroupId.Value = value;
    }

    public byte ConnectionSlotId
    {
        get => _connectionSlotId.Value;
        set => _connectionSlotId.Value = value;
    }

    public long PlayerId
    {
        get => _playerId.Value;
        set => _playerId.Value = value;
    }

    public ulong UserSessionId
    {
        get => _userSessionId.Value;
        set => _userSessionId.Value = value;
    }

    public byte SlotId
    {
        get => _slotId.Value;
        set => _slotId.Value = value;
    }

}
