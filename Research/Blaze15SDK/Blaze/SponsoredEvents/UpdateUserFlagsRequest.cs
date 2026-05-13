using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.SponsoredEvents;

public class UpdateUserFlagsRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("EventID", "mEventID", 0x976A6400, TdfType.Int32, 0, true), // Tag: EVID
        new TdfMemberInfo("Flags", "mFlags", 0x9AC9F300, TdfType.UInt32, 1, true), // Tag: FLGS
        new TdfMemberInfo("GamePlatform", "mGamePlatform", 0xC26CAD00, TdfType.String, 2, true), // Tag: PFRM
        new TdfMemberInfo("GameTitle", "mGameTitle", 0xD29D2C00, TdfType.String, 3, true), // Tag: TITL
        new TdfMemberInfo("UserID", "mUserID", 0xD6990000, TdfType.Int64, 4, true), // Tag: UID
    ];
    private ITdfMember[] __members;

    private TdfInt32 _eventID = new(__typeInfos[0]);
    private TdfUInt32 _flags = new(__typeInfos[1]);
    private TdfString _gamePlatform = new(__typeInfos[2]);
    private TdfString _gameTitle = new(__typeInfos[3]);
    private TdfInt64 _userID = new(__typeInfos[4]);

    public UpdateUserFlagsRequest()
    {
        __members = [_eventID, _flags, _gamePlatform, _gameTitle, _userID];
    }

    public override Tdf CreateNew() => new UpdateUserFlagsRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "UpdateUserFlagsRequest";
    public override string GetFullClassName() => "Blaze::SponsoredEvents::UpdateUserFlagsRequest";

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

    public string GamePlatform
    {
        get => _gamePlatform.Value;
        set => _gamePlatform.Value = value;
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
