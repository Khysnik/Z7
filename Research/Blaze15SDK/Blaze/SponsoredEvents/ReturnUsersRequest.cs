using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.SponsoredEvents;

public class ReturnUsersRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("EventID", "mEventID", 0x976A6400, TdfType.Int32, 0, true), // Tag: EVID
        new TdfMemberInfo("RowCount", "mRowCount", 0xBB2BF700, TdfType.Int32, 1, true), // Tag: NROW
        new TdfMemberInfo("Offset", "mOffset", 0xBE69B300, TdfType.Int32, 2, true), // Tag: OFFS
        new TdfMemberInfo("GamePlatform", "mGamePlatform", 0xC26CAD00, TdfType.String, 3, true), // Tag: PFRM
        new TdfMemberInfo("GameTitle", "mGameTitle", 0xD29D2C00, TdfType.String, 4, true), // Tag: TITL
        new TdfMemberInfo("UserID", "mUserID", 0xD6990000, TdfType.Int64, 5, true), // Tag: UID
    ];
    private ITdfMember[] __members;

    private TdfInt32 _eventID = new(__typeInfos[0]);
    private TdfInt32 _rowCount = new(__typeInfos[1]);
    private TdfInt32 _offset = new(__typeInfos[2]);
    private TdfString _gamePlatform = new(__typeInfos[3]);
    private TdfString _gameTitle = new(__typeInfos[4]);
    private TdfInt64 _userID = new(__typeInfos[5]);

    public ReturnUsersRequest()
    {
        __members = [_eventID, _rowCount, _offset, _gamePlatform, _gameTitle, _userID];
    }

    public override Tdf CreateNew() => new ReturnUsersRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ReturnUsersRequest";
    public override string GetFullClassName() => "Blaze::SponsoredEvents::ReturnUsersRequest";

    public int EventID
    {
        get => _eventID.Value;
        set => _eventID.Value = value;
    }

    public int RowCount
    {
        get => _rowCount.Value;
        set => _rowCount.Value = value;
    }

    public int Offset
    {
        get => _offset.Value;
        set => _offset.Value = value;
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
