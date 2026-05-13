using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Association;

public class UpdateListsRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("BlazeId", "mBlazeId", 0x8A990000, TdfType.Int64, 0, true), // Tag: BID
        new TdfMemberInfo("ListIdentificationVector", "mListIdentificationVector", 0xB2993300, TdfType.List, 1, true), // Tag: LIDS
    ];
    private ITdfMember[] __members;

    private TdfInt64 _blazeId = new(__typeInfos[0]);
    private TdfList<Blaze15SDK.Blaze.Association.ListIdentification> _listIdentificationVector = new(__typeInfos[1]);

    public UpdateListsRequest()
    {
        __members = [ _blazeId, _listIdentificationVector ];
    }

    public override Tdf CreateNew() => new UpdateListsRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "UpdateListsRequest";
    public override string GetFullClassName() => "Blaze::Association::UpdateListsRequest";

    public long BlazeId
    {
        get => _blazeId.Value;
        set => _blazeId.Value = value;
    }

    public IList<Blaze15SDK.Blaze.Association.ListIdentification> ListIdentificationVector
    {
        get => _listIdentificationVector.Value;
        set => _listIdentificationVector.Value = value;
    }

}
