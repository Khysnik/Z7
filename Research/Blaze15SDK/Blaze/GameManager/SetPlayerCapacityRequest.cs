using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class SetPlayerCapacityRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("GameId", "mGameId", 0x9E990000, TdfType.UInt64, 0, true), // Tag: GID
        new TdfMemberInfo("SlotCapacities", "mSlotCapacities", 0xC2387000, TdfType.List, 1, true), // Tag: PCAP
        new TdfMemberInfo("RoleInformation", "mRoleInformation", 0xCAE9AF00, TdfType.Struct, 2, true), // Tag: RNFO
        new TdfMemberInfo("TeamDetailsList", "mTeamDetailsList", 0xD2CCF400, TdfType.List, 3, true), // Tag: TLST
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _gameId = new(__typeInfos[0]);
    private TdfList<ushort> _slotCapacities = new(__typeInfos[1]);
    private TdfStruct<Blaze15SDK.Blaze.GameManager.RoleInformation?> _roleInformation = new(__typeInfos[2]);
    private TdfList<Blaze15SDK.Blaze.GameManager.TeamDetails> _teamDetailsList = new(__typeInfos[3]);

    public SetPlayerCapacityRequest()
    {
        __members = [ _gameId, _slotCapacities, _roleInformation, _teamDetailsList ];
    }

    public override Tdf CreateNew() => new SetPlayerCapacityRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "SetPlayerCapacityRequest";
    public override string GetFullClassName() => "Blaze::GameManager::SetPlayerCapacityRequest";

    public ulong GameId
    {
        get => _gameId.Value;
        set => _gameId.Value = value;
    }

    public IList<ushort> SlotCapacities
    {
        get => _slotCapacities.Value;
        set => _slotCapacities.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.RoleInformation? RoleInformation
    {
        get => _roleInformation.Value;
        set => _roleInformation.Value = value;
    }

    public IList<Blaze15SDK.Blaze.GameManager.TeamDetails> TeamDetailsList
    {
        get => _teamDetailsList.Value;
        set => _teamDetailsList.Value = value;
    }

}
