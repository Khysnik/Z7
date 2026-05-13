using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Clubs;

public class PublishClubTickerMessageRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ClubId", "mClubId", 0x8ECA6400, TdfType.UInt64, 0, true), // Tag: CLID
        new TdfMemberInfo("Message", "mMessage", 0x8F4B7300, TdfType.Struct, 1, true), // Tag: CTMS
        new TdfMemberInfo("ExcludeBlazeId", "mExcludeBlazeId", 0x978D6900, TdfType.Int64, 2, true), // Tag: EXUI
        new TdfMemberInfo("IncludeBlazeId", "mIncludeBlazeId", 0xA6ED6900, TdfType.Int64, 3, true), // Tag: INUI
        new TdfMemberInfo("Params", "mParams", 0xC32B7300, TdfType.String, 4, true), // Tag: PRMS
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _clubId = new(__typeInfos[0]);
    private TdfStruct<ClubTickerMessage?> _message = new(__typeInfos[1]);
    private TdfInt64 _excludeBlazeId = new(__typeInfos[2]);
    private TdfInt64 _includeBlazeId = new(__typeInfos[3]);
    private TdfString _params = new(__typeInfos[4]);

    public PublishClubTickerMessageRequest()
    {
        __members = [ _clubId, _message, _excludeBlazeId, _includeBlazeId, _params ];
    }

    public override Tdf CreateNew() => new PublishClubTickerMessageRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "PublishClubTickerMessageRequest";
    public override string GetFullClassName() => "Blaze::Clubs::PublishClubTickerMessageRequest";

    public ulong ClubId
    {
        get => _clubId.Value;
        set => _clubId.Value = value;
    }

    public ClubTickerMessage? Message
    {
        get => _message.Value;
        set => _message.Value = value;
    }

    public long ExcludeBlazeId
    {
        get => _excludeBlazeId.Value;
        set => _excludeBlazeId.Value = value;
    }

    public long IncludeBlazeId
    {
        get => _includeBlazeId.Value;
        set => _includeBlazeId.Value = value;
    }

    public string Params
    {
        get => _params.Value;
        set => _params.Value = value;
    }

}
