using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.ByteVault;

public class GetAdminChangeHistoryResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("AdminChangeEventList", "mAdminChangeEventList", 0xB29CF400, TdfType.List, 0, true), // Tag: LIST
    ];
    private ITdfMember[] __members;

    private TdfList<Blaze15SDK.Blaze.ByteVault.AdminChangeEvent> _adminChangeEventList = new(__typeInfos[0]);

    public GetAdminChangeHistoryResponse()
    {
        __members = [ _adminChangeEventList ];
    }

    public override Tdf CreateNew() => new GetAdminChangeHistoryResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetAdminChangeHistoryResponse";
    public override string GetFullClassName() => "Blaze::ByteVault::GetAdminChangeHistoryResponse";

    public IList<Blaze15SDK.Blaze.ByteVault.AdminChangeEvent> AdminChangeEventList
    {
        get => _adminChangeEventList.Value;
        set => _adminChangeEventList.Value = value;
    }

}
