using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.ByteVault;

public class UpsertContextResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Context", "mContext", 0x8F4E3400, TdfType.Struct, 0, true), // Tag: CTXT
        new TdfMemberInfo("ResourceCreated", "mResourceCreated", 0xCA3CA500, TdfType.Bool, 1, true), // Tag: RCRE
    ];
    private ITdfMember[] __members;

    private TdfStruct<Blaze15SDK.Blaze.ByteVault.Context?> _context = new(__typeInfos[0]);
    private TdfBool _resourceCreated = new(__typeInfos[1]);

    public UpsertContextResponse()
    {
        __members = [ _context, _resourceCreated ];
    }

    public override Tdf CreateNew() => new UpsertContextResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "UpsertContextResponse";
    public override string GetFullClassName() => "Blaze::ByteVault::UpsertContextResponse";

    public Blaze15SDK.Blaze.ByteVault.Context? Context
    {
        get => _context.Value;
        set => _context.Value = value;
    }

    public bool ResourceCreated
    {
        get => _resourceCreated.Value;
        set => _resourceCreated.Value = value;
    }

}
