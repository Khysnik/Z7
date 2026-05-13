using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.ByteVault;

public class AccessPermission : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Others", "mOthers", 0xBF4A3200, TdfType.Enum, 0, true), // Tag: OTHR
        new TdfMemberInfo("Owner", "mOwner", 0xBF7BB200, TdfType.Enum, 1, true), // Tag: OWNR
        new TdfMemberInfo("Trusted", "mTrusted", 0xD32CF400, TdfType.Enum, 2, true), // Tag: TRST
    ];
    private ITdfMember[] __members;

    private TdfEnum<Permission> _others = new(__typeInfos[0]);
    private TdfEnum<Permission> _owner = new(__typeInfos[1]);
    private TdfEnum<Permission> _trusted = new(__typeInfos[2]);

    public AccessPermission()
    {
        __members = [ _others, _owner, _trusted ];
    }

    public override Tdf CreateNew() => new AccessPermission();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "AccessPermission";
    public override string GetFullClassName() => "Blaze::ByteVault::AccessPermission";

    public Permission Others
    {
        get => _others.Value;
        set => _others.Value = value;
    }

    public Permission Owner
    {
        get => _owner.Value;
        set => _owner.Value = value;
    }

    public Permission Trusted
    {
        get => _trusted.Value;
        set => _trusted.Value = value;
    }

}
