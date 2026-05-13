using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Association;

public class GetMemberHashRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("BlazeId", "mBlazeId", 0x8A990000, TdfType.Int64, 0, true), // Tag: BID
        new TdfMemberInfo("ListIdentification", "mListIdentification", 0xB2990000, TdfType.Struct, 1, true), // Tag: LID
    ];
    private ITdfMember[] __members;

    private TdfInt64 _blazeId = new(__typeInfos[0]);
    private TdfStruct<Blaze15SDK.Blaze.Association.ListIdentification?> _listIdentification = new(__typeInfos[1]);

    public GetMemberHashRequest()
    {
        __members = [ _blazeId, _listIdentification ];
    }

    public override Tdf CreateNew() => new GetMemberHashRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetMemberHashRequest";
    public override string GetFullClassName() => "Blaze::Association::GetMemberHashRequest";

    public long BlazeId
    {
        get => _blazeId.Value;
        set => _blazeId.Value = value;
    }

    public Blaze15SDK.Blaze.Association.ListIdentification? ListIdentification
    {
        get => _listIdentification.Value;
        set => _listIdentification.Value = value;
    }

}
