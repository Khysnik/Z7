using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Clubs;

public class LogEventRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ClubId", "mClubId", 0x8E2A6400, TdfType.UInt64, 0, true), // Tag: CBID
        new TdfMemberInfo("EventKeyType", "mEventKeyType", 0xB25AF900, TdfType.Enum, 1, true), // Tag: LEKY
        new TdfMemberInfo("EventParams", "mEventParams", 0xB25C3600, TdfType.Map, 2, true), // Tag: LEPV
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _clubId = new(__typeInfos[0]);
    private TdfEnum<ClubLogEventKeyType> _eventKeyType = new(__typeInfos[1]);
    private TdfMap<NewsParamType, string> _eventParams = new(__typeInfos[2]);

    public LogEventRequest()
    {
        __members = [ _clubId, _eventKeyType, _eventParams ];
    }

    public override Tdf CreateNew() => new LogEventRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "LogEventRequest";
    public override string GetFullClassName() => "Blaze::Clubs::LogEventRequest";

    public ulong ClubId
    {
        get => _clubId.Value;
        set => _clubId.Value = value;
    }

    public ClubLogEventKeyType EventKeyType
    {
        get => _eventKeyType.Value;
        set => _eventKeyType.Value = value;
    }

    public IDictionary<NewsParamType, string> EventParams
    {
        get => _eventParams.Value;
        set => _eventParams.Value = value;
    }

}
