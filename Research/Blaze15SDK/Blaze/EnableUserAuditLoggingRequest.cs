using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class EnableUserAuditLoggingRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("BlazeIdList", "mBlazeIdList", 0xD6CCF400, TdfType.List, 0, true), // Tag: ULST
    ];
    private ITdfMember[] __members;

    private TdfList<long> _blazeIdList = new(__typeInfos[0]);

    public EnableUserAuditLoggingRequest()
    {
        __members = [
            _blazeIdList,
        ];
    }

    public override Tdf CreateNew() => new EnableUserAuditLoggingRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "EnableUserAuditLoggingRequest";
    public override string GetFullClassName() => "Blaze::EnableUserAuditLoggingRequest";

    public IList<long> BlazeIdList
    {
        get => _blazeIdList.Value;
        set => _blazeIdList.Value = value;
    }

}
