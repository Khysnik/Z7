using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class OptInRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("BlazeId", "mBlazeId", 0xA6400000, TdfType.Int64, 0, true), // Tag: ID
        new TdfMemberInfo("OptIn", "mOptIn", 0xBF0D0000, TdfType.Bool, 1, true), // Tag: OPT
    ];
    private ITdfMember[] __members;

    private TdfInt64 _blazeId = new(__typeInfos[0]);
    private TdfBool _optIn = new(__typeInfos[1]);

    public OptInRequest()
    {
        __members = [
            _blazeId,
            _optIn,
        ];
    }

    public override Tdf CreateNew() => new OptInRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "OptInRequest";
    public override string GetFullClassName() => "Blaze::OptInRequest";

    public long BlazeId
    {
        get => _blazeId.Value;
        set => _blazeId.Value = value;
    }

    public bool OptIn
    {
        get => _optIn.Value;
        set => _optIn.Value = value;
    }

}
