using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.ByteVault;

public class GetContextsRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("AuthCredentials", "mAuthCredentials", 0x875D2800, TdfType.Struct, 0, true), // Tag: AUTH
        new TdfMemberInfo("Context", "mContext", 0x8F4E3400, TdfType.String, 1, true), // Tag: CTXT
        new TdfMemberInfo("MaxResultCount", "mMaxResultCount", 0xB78CA300, TdfType.UInt32, 2, true), // Tag: MXRC
        new TdfMemberInfo("Offset", "mOffset", 0xBE69B300, TdfType.UInt32, 3, true), // Tag: OFFS
    ];
    private ITdfMember[] __members;

    private TdfStruct<Blaze15SDK.Blaze.ByteVault.AuthenticationCredentials?> _authCredentials = new(__typeInfos[0]);
    private TdfString _context = new(__typeInfos[1]);
    private TdfUInt32 _maxResultCount = new(__typeInfos[2]);
    private TdfUInt32 _offset = new(__typeInfos[3]);

    public GetContextsRequest()
    {
        __members = [ _authCredentials, _context, _maxResultCount, _offset ];
    }

    public override Tdf CreateNew() => new GetContextsRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetContextsRequest";
    public override string GetFullClassName() => "Blaze::ByteVault::GetContextsRequest";

    public Blaze15SDK.Blaze.ByteVault.AuthenticationCredentials? AuthCredentials
    {
        get => _authCredentials.Value;
        set => _authCredentials.Value = value;
    }

    public string Context
    {
        get => _context.Value;
        set => _context.Value = value;
    }

    public uint MaxResultCount
    {
        get => _maxResultCount.Value;
        set => _maxResultCount.Value = value;
    }

    public uint Offset
    {
        get => _offset.Value;
        set => _offset.Value = value;
    }

}
