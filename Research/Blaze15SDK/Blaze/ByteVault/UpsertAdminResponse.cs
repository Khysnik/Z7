using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.ByteVault;

public class UpsertAdminResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ResourceCreated", "mResourceCreated", 0xCA3CA500, TdfType.Bool, 0, true), // Tag: RCRE
        new TdfMemberInfo("Admin", "mAdmin", 0xD39C2500, TdfType.Struct, 1, true), // Tag: TYPE
    ];
    private ITdfMember[] __members;

    private TdfBool _resourceCreated = new(__typeInfos[0]);
    private TdfStruct<Blaze15SDK.Blaze.ByteVault.GetAdminResponse?> _admin = new(__typeInfos[1]);

    public UpsertAdminResponse()
    {
        __members = [ _resourceCreated, _admin ];
    }

    public override Tdf CreateNew() => new UpsertAdminResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "UpsertAdminResponse";
    public override string GetFullClassName() => "Blaze::ByteVault::UpsertAdminResponse";

    public bool ResourceCreated
    {
        get => _resourceCreated.Value;
        set => _resourceCreated.Value = value;
    }

    public Blaze15SDK.Blaze.ByteVault.GetAdminResponse? Admin
    {
        get => _admin.Value;
        set => _admin.Value = value;
    }

}
