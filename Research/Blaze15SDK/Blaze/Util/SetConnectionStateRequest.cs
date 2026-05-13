using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Util;

public class SetConnectionStateRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("IsActive", "mIsActive", 0x863D3600, TdfType.Bool, 0, true), // Tag: ACTV
    ];
    private ITdfMember[] __members;

    private TdfBool _isActive = new(__typeInfos[0]);

    public SetConnectionStateRequest()
    {
        __members = [ _isActive ];
    }

    public override Tdf CreateNew() => new SetConnectionStateRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "SetConnectionStateRequest";
    public override string GetFullClassName() => "Blaze::GameManager::SetConnectionStateRequest";

    public bool IsActive
    {
        get => _isActive.Value;
        set => _isActive.Value = value;
    }

}
