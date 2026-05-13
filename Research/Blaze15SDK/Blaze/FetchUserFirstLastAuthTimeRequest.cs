using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class FetchUserFirstLastAuthTimeRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("BlazeId", "mBlazeId", 0xD6990000, TdfType.Int64, 0, true), // Tag: UID
    ];
    private ITdfMember[] __members;

    private TdfInt64 _blazeId = new(__typeInfos[0]);

    public FetchUserFirstLastAuthTimeRequest()
    {
        __members = [
            _blazeId,
        ];
    }

    public override Tdf CreateNew() => new FetchUserFirstLastAuthTimeRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "FetchUserFirstLastAuthTimeRequest";
    public override string GetFullClassName() => "Blaze::FetchUserFirstLastAuthTimeRequest";

    public long BlazeId
    {
        get => _blazeId.Value;
        set => _blazeId.Value = value;
    }

}
