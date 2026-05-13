using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.SponsoredEvents;

public class RegisterUserRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Country", "mCountry", 0x8F4CB900, TdfType.String, 0, true), // Tag: CTRY
        new TdfMemberInfo("DateOfBirth", "mDateOfBirth", 0x92F88000, TdfType.String, 1, true), // Tag: DOB
        new TdfMemberInfo("EventID", "mEventID", 0x96990000, TdfType.UInt32, 2, true), // Tag: EID
        new TdfMemberInfo("EventData", "mEventData", 0x97690000, TdfType.String, 3, true), // Tag: EVD
        new TdfMemberInfo("GamePlatform", "mGamePlatform", 0xC26CAD00, TdfType.String, 4, true), // Tag: PFRM
        new TdfMemberInfo("SendInformation", "mSendInformation", 0xCEE92900, TdfType.Int8, 5, true), // Tag: SNDI
        new TdfMemberInfo("GameTitle", "mGameTitle", 0xD29D2C00, TdfType.String, 6, true), // Tag: TITL
        new TdfMemberInfo("UserID", "mUserID", 0xD6990000, TdfType.Int64, 7, true), // Tag: UID
    ];
    private ITdfMember[] __members;

    private TdfString _country = new(__typeInfos[0]);
    private TdfString _dateOfBirth = new(__typeInfos[1]);
    private TdfUInt32 _eventID = new(__typeInfos[2]);
    private TdfString _eventData = new(__typeInfos[3]);
    private TdfString _gamePlatform = new(__typeInfos[4]);
    private TdfInt8 _sendInformation = new(__typeInfos[5]);
    private TdfString _gameTitle = new(__typeInfos[6]);
    private TdfInt64 _userID = new(__typeInfos[7]);

    public RegisterUserRequest()
    {
        __members = [_country, _dateOfBirth, _eventID, _eventData, _gamePlatform, _sendInformation, _gameTitle, _userID];
    }

    public override Tdf CreateNew() => new RegisterUserRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "RegisterUserRequest";
    public override string GetFullClassName() => "Blaze::SponsoredEvents::RegisterUserRequest";

    public string Country
    {
        get => _country.Value;
        set => _country.Value = value;
    }

    public string DateOfBirth
    {
        get => _dateOfBirth.Value;
        set => _dateOfBirth.Value = value;
    }

    public uint EventID
    {
        get => _eventID.Value;
        set => _eventID.Value = value;
    }

    public string EventData
    {
        get => _eventData.Value;
        set => _eventData.Value = value;
    }

    public string GamePlatform
    {
        get => _gamePlatform.Value;
        set => _gamePlatform.Value = value;
    }

    public sbyte SendInformation
    {
        get => _sendInformation.Value;
        set => _sendInformation.Value = value;
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
}
