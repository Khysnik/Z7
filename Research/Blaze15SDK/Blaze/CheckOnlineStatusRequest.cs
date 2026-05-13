using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class CheckOnlineStatusRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("BlazeId", "mBlazeId", 0x8B5A6400, TdfType.Int64, 0, true), // Tag: BUID
    ];
    private ITdfMember[] __members;

    private TdfInt64 _blazeId = new(__typeInfos[0]);

    public CheckOnlineStatusRequest()
    {
        __members = [
            _blazeId,
        ];
    }

    public override Tdf CreateNew() => new CheckOnlineStatusRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "CheckOnlineStatusRequest";
    public override string GetFullClassName() => "Blaze::CheckOnlineStatusRequest";

    public long BlazeId
    {
        get => _blazeId.Value;
        set => _blazeId.Value = value;
    }

}
