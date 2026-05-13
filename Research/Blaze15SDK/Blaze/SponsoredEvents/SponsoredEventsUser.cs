using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.SponsoredEvents;

public class SponsoredEventsUser : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("IsBanned", "mIsBanned", 0x8A1B8000, TdfType.Int8, 0, true), // Tag: BAN
        new TdfMemberInfo("CreateDate", "mCreateDate", 0x8E487400, TdfType.String, 1, true), // Tag: CDAT
        new TdfMemberInfo("Country", "mCountry", 0x8EF00000, TdfType.String, 2, true), // Tag: CO
        new TdfMemberInfo("DOB", "mDOB", 0x92F88000, TdfType.String, 3, true), // Tag: DOB
        new TdfMemberInfo("EventID", "mEventID", 0x976A6400, TdfType.Int32, 4, true), // Tag: EVID
        new TdfMemberInfo("Flags", "mFlags", 0x9AC9F300, TdfType.UInt32, 5, true), // Tag: FLGS
        new TdfMemberInfo("Note", "mNote", 0xBAFD2500, TdfType.String, 6, true), // Tag: NOTE
        new TdfMemberInfo("GamePlatform", "mGamePlatform", 0xC26CAD00, TdfType.String, 7, true), // Tag: PFRM
        new TdfMemberInfo("ShareInfo", "mShareInfo", 0xCE9BA600, TdfType.Int8, 8, true), // Tag: SINF
        new TdfMemberInfo("GameTitle", "mGameTitle", 0xD29D2C00, TdfType.String, 9, true), // Tag: TITL
        new TdfMemberInfo("UserID", "mUserID", 0xD6990000, TdfType.Int64, 10, true), // Tag: UID
        new TdfMemberInfo("WhoBan", "mWhoBan", 0xDE8BE200, TdfType.String, 11, true), // Tag: WHOB
        new TdfMemberInfo("WhyBan", "mWhyBan", 0xDE8E6200, TdfType.String, 12, true), // Tag: WHYB
    ];
    private ITdfMember[] __members;

    private TdfInt8 _isBanned = new(__typeInfos[0]);
    private TdfString _createDate = new(__typeInfos[1]);
    private TdfString _country = new(__typeInfos[2]);
    private TdfString _dob = new(__typeInfos[3]);
    private TdfInt32 _eventID = new(__typeInfos[4]);
    private TdfUInt32 _flags = new(__typeInfos[5]);
    private TdfString _note = new(__typeInfos[6]);
    private TdfString _gamePlatform = new(__typeInfos[7]);
    private TdfInt8 _shareInfo = new(__typeInfos[8]);
    private TdfString _gameTitle = new(__typeInfos[9]);
    private TdfInt64 _userID = new(__typeInfos[10]);
    private TdfString _whoBan = new(__typeInfos[11]);
    private TdfString _whyBan = new(__typeInfos[12]);

    public SponsoredEventsUser()
    {
        __members = [_isBanned, _createDate, _country, _dob, _eventID, _flags, _note, _gamePlatform, _shareInfo, _gameTitle, _userID, _whoBan, _whyBan];
    }

    public override Tdf CreateNew() => new SponsoredEventsUser();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "SponsoredEventsUser";
    public override string GetFullClassName() => "Blaze::SponsoredEvents::SponsoredEventsUser";

    public sbyte IsBanned
    {
        get => _isBanned.Value;
        set => _isBanned.Value = value;
    }

    public string CreateDate
    {
        get => _createDate.Value;
        set => _createDate.Value = value;
    }

    public string Country
    {
        get => _country.Value;
        set => _country.Value = value;
    }

    public string DOB
    {
        get => _dob.Value;
        set => _dob.Value = value;
    }

    public int EventID
    {
        get => _eventID.Value;
        set => _eventID.Value = value;
    }

    public uint Flags
    {
        get => _flags.Value;
        set => _flags.Value = value;
    }

    public string Note
    {
        get => _note.Value;
        set => _note.Value = value;
    }

    public string GamePlatform
    {
        get => _gamePlatform.Value;
        set => _gamePlatform.Value = value;
    }

    public sbyte ShareInfo
    {
        get => _shareInfo.Value;
        set => _shareInfo.Value = value;
    }

    public string GameTitle
    {
        get => _gameTitle.Value;
        set => _gameTitle.Value = value;
    }

    public long UserID
    {
        get => _userID.Value;
        set => _userID.Value = value;
    }

    public string WhoBan
    {
        get => _whoBan.Value;
        set => _whoBan.Value = value;
    }

    public string WhyBan
    {
        get => _whyBan.Value;
        set => _whyBan.Value = value;
    }
}
