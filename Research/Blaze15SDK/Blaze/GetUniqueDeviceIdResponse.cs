using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class GetUniqueDeviceIdResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("UniqueDeviceId", "mUniqueDeviceId", 0xD64A6400, TdfType.String, 0, true), // Tag: UDID
    ];
    private ITdfMember[] __members;

    private TdfString _uniqueDeviceId = new(__typeInfos[0]);

    public GetUniqueDeviceIdResponse()
    {
        __members = [
            _uniqueDeviceId,
        ];
    }

    public override Tdf CreateNew() => new GetUniqueDeviceIdResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetUniqueDeviceIdResponse";
    public override string GetFullClassName() => "Blaze::GetUniqueDeviceIdResponse";

    public string UniqueDeviceId
    {
        get => _uniqueDeviceId.Value;
        set => _uniqueDeviceId.Value = value;
    }

}
