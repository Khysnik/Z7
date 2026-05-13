using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.ByteVault;

public class GetErrorRatesResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ErrorsPerCategoryPerContext", "mErrorsPerCategoryPerContext", 0xCA1D2D00, TdfType.Map, 0, true), // Tag: RATM
    ];
    private ITdfMember[] __members;

    private TdfMap<string, IDictionary<string, IDictionary<string, IDictionary<string, uint>>>> _errorsPerCategoryPerContext = new(__typeInfos[0]);

    public GetErrorRatesResponse()
    {
        __members = [ _errorsPerCategoryPerContext ];
    }

    public override Tdf CreateNew() => new GetErrorRatesResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetErrorRatesResponse";
    public override string GetFullClassName() => "Blaze::ByteVault::GetErrorRatesResponse";

    public IDictionary<string, IDictionary<string, IDictionary<string, IDictionary<string, uint>>>> ErrorsPerCategoryPerContext
    {
        get => _errorsPerCategoryPerContext.Value;
        set => _errorsPerCategoryPerContext.Value = value;
    }

}
