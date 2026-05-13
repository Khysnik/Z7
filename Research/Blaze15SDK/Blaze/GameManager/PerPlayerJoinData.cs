using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class PerPlayerJoinData : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("EncryptedBlazeId", "mEncryptedBlazeId", 0x96E8F200, TdfType.String, 0, true), // Tag: ENCR
        new TdfMemberInfo("IsOptionalPlayer", "mIsOptionalPlayer", 0xA7297000, TdfType.Bool, 1, true), // Tag: IREP
        new TdfMemberInfo("PlayerAttributes", "mPlayerAttributes", 0xC2CE6100, TdfType.Map, 2, true), // Tag: PLYA
        new TdfMemberInfo("Role", "mRole", 0xCACBAD00, TdfType.String, 3, true), // Tag: RLNM
        new TdfMemberInfo("Roles", "mRoles", 0xCACCF400, TdfType.List, 4, true), // Tag: RLST
        new TdfMemberInfo("SlotType", "mSlotType", 0xCECBF400, TdfType.Enum, 5, true), // Tag: SLOT
        new TdfMemberInfo("TeamId", "mTeamId", 0xD2990000, TdfType.UInt16, 6, true), // Tag: TID
        new TdfMemberInfo("TeamIndex", "mTeamIndex", 0xD2993800, TdfType.UInt16, 7, true), // Tag: TIDX
        new TdfMemberInfo("User", "mUser", 0xD73A6400, TdfType.Struct, 8, true), // Tag: USID
    ];
    private ITdfMember[] __members;

    private TdfString _encryptedBlazeId = new(__typeInfos[0]);
    private TdfBool _isOptionalPlayer = new(__typeInfos[1]);
    private TdfMap<string, string> _playerAttributes = new(__typeInfos[2]);
    private TdfString _role = new(__typeInfos[3]);
    private TdfList<string> _roles = new(__typeInfos[4]);
    private TdfEnum<Blaze15SDK.Blaze.GameManager.SlotType> _slotType = new(__typeInfos[5]);
    private TdfUInt16 _teamId = new(__typeInfos[6]);
    private TdfUInt16 _teamIndex = new(__typeInfos[7]);
    private TdfStruct<Blaze15SDK.Blaze.UserIdentification?> _user = new(__typeInfos[8]);

    public PerPlayerJoinData()
    {
        __members = [ _encryptedBlazeId, _isOptionalPlayer, _playerAttributes, _role, _roles, _slotType, _teamId, _teamIndex, _user ];
    }

    public override Tdf CreateNew() => new PerPlayerJoinData();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "PerPlayerJoinData";
    public override string GetFullClassName() => "Blaze::GameManager::PerPlayerJoinData";

    public string EncryptedBlazeId
    {
        get => _encryptedBlazeId.Value;
        set => _encryptedBlazeId.Value = value;
    }

    public bool IsOptionalPlayer
    {
        get => _isOptionalPlayer.Value;
        set => _isOptionalPlayer.Value = value;
    }

    public IDictionary<string, string> PlayerAttributes
    {
        get => _playerAttributes.Value;
        set => _playerAttributes.Value = value;
    }

    public string Role
    {
        get => _role.Value;
        set => _role.Value = value;
    }

    public IList<string> Roles
    {
        get => _roles.Value;
        set => _roles.Value = value;
    }

    public Blaze15SDK.Blaze.GameManager.SlotType SlotType
    {
        get => _slotType.Value;
        set => _slotType.Value = value;
    }

    public ushort TeamId
    {
        get => _teamId.Value;
        set => _teamId.Value = value;
    }

    public ushort TeamIndex
    {
        get => _teamIndex.Value;
        set => _teamIndex.Value = value;
    }

    public Blaze15SDK.Blaze.UserIdentification? User
    {
        get => _user.Value;
        set => _user.Value = value;
    }

}
