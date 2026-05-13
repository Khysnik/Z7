using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Social.Friends;

public class User : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("DisplayName", "mDisplayName", 0x930BAD00, TdfType.String, 0, true), // Tag: DPNM
        new TdfMemberInfo("DateTime", "mDateTime", 0x934D2D00, TdfType.String, 1, true), // Tag: DTTM
        new TdfMemberInfo("FriendType", "mFriendType", 0x9B4E7000, TdfType.String, 2, true), // Tag: FTYP
        new TdfMemberInfo("Favorite", "mFavorite", 0x9B6CB400, TdfType.Bool, 3, true), // Tag: FVRT
        new TdfMemberInfo("PersonaId", "mPersonaId", 0xC2EA6400, TdfType.Int64, 4, true), // Tag: PNID
        new TdfMemberInfo("Source", "mSource", 0xCEFCA300, TdfType.String, 5, true), // Tag: SORC
        new TdfMemberInfo("Timestamp", "mTimestamp", 0xD2DCF400, TdfType.Int64, 6, true), // Tag: TMST
        new TdfMemberInfo("UserId", "mUserId", 0xD6EA6400, TdfType.Int64, 7, true), // Tag: UNID
    ];
    private ITdfMember[] __members;

    private TdfString _displayName = new(__typeInfos[0]);
    private TdfString _dateTime = new(__typeInfos[1]);
    private TdfString _friendType = new(__typeInfos[2]);
    private TdfBool _favorite = new(__typeInfos[3]);
    private TdfInt64 _personaId = new(__typeInfos[4]);
    private TdfString _source = new(__typeInfos[5]);
    private TdfInt64 _timestamp = new(__typeInfos[6]);
    private TdfInt64 _userId = new(__typeInfos[7]);

    public User()
    {
        __members = [ _displayName, _dateTime, _friendType, _favorite, _personaId, _source, _timestamp, _userId ];
    }

    public override Tdf CreateNew() => new User();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "User";
    public override string GetFullClassName() => "Blaze::Social::Friends::User";

    public string DisplayName
    {
        get => _displayName.Value;
        set => _displayName.Value = value;
    }

    public string DateTime
    {
        get => _dateTime.Value;
        set => _dateTime.Value = value;
    }

    public string FriendType
    {
        get => _friendType.Value;
        set => _friendType.Value = value;
    }

    public bool Favorite
    {
        get => _favorite.Value;
        set => _favorite.Value = value;
    }

    public long PersonaId
    {
        get => _personaId.Value;
        set => _personaId.Value = value;
    }

    public string Source
    {
        get => _source.Value;
        set => _source.Value = value;
    }

    public long Timestamp
    {
        get => _timestamp.Value;
        set => _timestamp.Value = value;
    }

    public long UserId
    {
        get => _userId.Value;
        set => _userId.Value = value;
    }

}
