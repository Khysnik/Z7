using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class DisableUserAuditLoggingRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("BlazeIdList", "mBlazeIdList", 0xD6CCF400, TdfType.List, 0, true), // Tag: ULST
    ];
    private ITdfMember[] __members;

    private TdfList<long> _blazeIdList = new(__typeInfos[0]);

    public DisableUserAuditLoggingRequest()
    {
        __members = [
            _blazeIdList,
        ];
    }

    public override Tdf CreateNew() => new DisableUserAuditLoggingRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "DisableUserAuditLoggingRequest";
    public override string GetFullClassName() => "Blaze::DisableUserAuditLoggingRequest";

    public IList<long> BlazeIdList
    {
        get => _blazeIdList.Value;
        set => _blazeIdList.Value = value;
    }

}
