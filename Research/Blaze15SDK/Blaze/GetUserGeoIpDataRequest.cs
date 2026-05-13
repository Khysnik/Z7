using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class GetUserGeoIpDataRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("BlazeId", "mBlazeId", 0x8A990000, TdfType.Int64, 0, true), // Tag: BID
        new TdfMemberInfo("UserSessionId", "mUserSessionId", 0xD6990000, TdfType.UInt64, 1, true), // Tag: UID
    ];
    private ITdfMember[] __members;

    private TdfInt64 _blazeId = new(__typeInfos[0]);
    private TdfUInt64 _userSessionId = new(__typeInfos[1]);

    public GetUserGeoIpDataRequest()
    {
        __members = [
            _blazeId,
            _userSessionId,
        ];
    }

    public override Tdf CreateNew() => new GetUserGeoIpDataRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetUserGeoIpDataRequest";
    public override string GetFullClassName() => "Blaze::GetUserGeoIpDataRequest";

    public long BlazeId
    {
        get => _blazeId.Value;
        set => _blazeId.Value = value;
    }

    public ulong UserSessionId
    {
        get => _userSessionId.Value;
        set => _userSessionId.Value = value;
    }

}
