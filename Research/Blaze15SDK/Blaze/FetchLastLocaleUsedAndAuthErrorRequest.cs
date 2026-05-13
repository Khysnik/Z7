using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class FetchLastLocaleUsedAndAuthErrorRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("BlazeId", "mBlazeId", 0xA6400000, TdfType.Int64, 0, true), // Tag: ID
    ];
    private ITdfMember[] __members;

    private TdfInt64 _blazeId = new(__typeInfos[0]);

    public FetchLastLocaleUsedAndAuthErrorRequest()
    {
        __members = [
            _blazeId,
        ];
    }

    public override Tdf CreateNew() => new FetchLastLocaleUsedAndAuthErrorRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "FetchLastLocaleUsedAndAuthErrorRequest";
    public override string GetFullClassName() => "Blaze::FetchLastLocaleUsedAndAuthErrorRequest";

    public long BlazeId
    {
        get => _blazeId.Value;
        set => _blazeId.Value = value;
    }

}
