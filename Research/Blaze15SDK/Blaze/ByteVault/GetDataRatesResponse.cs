using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.ByteVault;

public class GetDataRatesResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("DataRates", "mDataRates", 0xCA1D2500, TdfType.Struct, 0, true), // Tag: RATE
        new TdfMemberInfo("DataRatesPerCategoryPerContext", "mDataRatesPerCategoryPerContext", 0xCA1D2D00, TdfType.Map, 1, true), // Tag: RATM
    ];
    private ITdfMember[] __members;

    private TdfStruct<Blaze15SDK.Blaze.ByteVault.DataRates?> _dataRates = new(__typeInfos[0]);
    private TdfMap<string, IDictionary<string, Blaze15SDK.Blaze.ByteVault.DataRates>> _dataRatesPerCategoryPerContext = new(__typeInfos[1]);

    public GetDataRatesResponse()
    {
        __members = [ _dataRates, _dataRatesPerCategoryPerContext ];
    }

    public override Tdf CreateNew() => new GetDataRatesResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetDataRatesResponse";
    public override string GetFullClassName() => "Blaze::ByteVault::GetDataRatesResponse";

    public Blaze15SDK.Blaze.ByteVault.DataRates? DataRates
    {
        get => _dataRates.Value;
        set => _dataRates.Value = value;
    }

    public IDictionary<string, IDictionary<string, Blaze15SDK.Blaze.ByteVault.DataRates>> DataRatesPerCategoryPerContext
    {
        get => _dataRatesPerCategoryPerContext.Value;
        set => _dataRatesPerCategoryPerContext.Value = value;
    }

}
