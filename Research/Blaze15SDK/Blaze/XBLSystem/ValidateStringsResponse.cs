using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.XBLSystem;

public class ValidateStringsResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("VerifyStringResult", "mVerifyStringResult", 0xCF4CAD00, TdfType.List, 0, true), // Tag: STRM
    ];
    private ITdfMember[] __members;

    private TdfList<Result> _verifyStringResult = new(__typeInfos[0]);

    public ValidateStringsResponse()
    {
        __members = [
            _verifyStringResult,
        ];
    }

    public override Tdf CreateNew() => new ValidateStringsResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ValidateStringsResponse";
    public override string GetFullClassName() => "Blaze::XBLSystem::ValidateStringsResponse";

    public IList<Result> VerifyStringResult
    {
        get => _verifyStringResult.Value;
        set => _verifyStringResult.Value = value;
    }

}
