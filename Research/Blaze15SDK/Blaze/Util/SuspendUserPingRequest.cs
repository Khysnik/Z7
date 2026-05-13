using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Util;

public class SuspendUserPingRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("SuspendTime", "suspendTime", 0xD3686C00, TdfType.Int64, 0, true), // Tag: TVAL
    ];
    private ITdfMember[] __members;

    private TdfInt64 _suspendTime = new(__typeInfos[0]);

    public SuspendUserPingRequest()
    {
        __members = [ _suspendTime ];
    }

    public override Tdf CreateNew() => new SuspendUserPingRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "SuspendUserPingRequest";
    public override string GetFullClassName() => "Blaze::GameManager::SuspendUserPingRequest";

    public long SuspendTime
    {
        get => _suspendTime.Value;
        set => _suspendTime.Value = value;
    }

}
