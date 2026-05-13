using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.ByteVault;

public class GetContextsResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Contexts", "mContexts", 0xB29CF400, TdfType.List, 0, true), // Tag: LIST
    ];
    private ITdfMember[] __members;

    private TdfList<Blaze15SDK.Blaze.ByteVault.Context> _contexts = new(__typeInfos[0]);

    public GetContextsResponse()
    {
        __members = [ _contexts ];
    }

    public override Tdf CreateNew() => new GetContextsResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetContextsResponse";
    public override string GetFullClassName() => "Blaze::ByteVault::GetContextsResponse";

    public IList<Blaze15SDK.Blaze.ByteVault.Context> Contexts
    {
        get => _contexts.Value;
        set => _contexts.Value = value;
    }

}
