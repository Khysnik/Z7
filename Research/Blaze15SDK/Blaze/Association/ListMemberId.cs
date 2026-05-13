using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Association;

public class ListMemberId : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Attributes", "mAttributes", 0x874D3200, TdfType.Enum, 0, true), // Tag: ATTR
        new TdfMemberInfo("ExternalSystemId", "mExternalSystemId", 0xC2C87400, TdfType.Enum, 1, true), // Tag: PLAT
        new TdfMemberInfo("User", "mUser", 0xD7397200, TdfType.Struct, 2, true), // Tag: USER
    ];
    private ITdfMember[] __members;

    private TdfEnum<Blaze15SDK.Blaze.Association.ListMemberAttributes> _attributes = new(__typeInfos[0]);
    private TdfEnum<Blaze15SDK.Blaze.ClientPlatformType> _externalSystemId = new(__typeInfos[1]);
    private TdfStruct<Blaze15SDK.Blaze.CoreIdentification?> _user = new(__typeInfos[2]);

    public ListMemberId()
    {
        __members = [ _attributes, _externalSystemId, _user ];
    }

    public override Tdf CreateNew() => new ListMemberId();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ListMemberId";
    public override string GetFullClassName() => "Blaze::Association::ListMemberId";

    public Blaze15SDK.Blaze.Association.ListMemberAttributes Attributes
    {
        get => _attributes.Value;
        set => _attributes.Value = value;
    }

    public Blaze15SDK.Blaze.ClientPlatformType ExternalSystemId
    {
        get => _externalSystemId.Value;
        set => _externalSystemId.Value = value;
    }

    public Blaze15SDK.Blaze.CoreIdentification? User
    {
        get => _user.Value;
        set => _user.Value = value;
    }

}
