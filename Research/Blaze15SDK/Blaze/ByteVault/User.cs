using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.ByteVault;

public class User : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Id", "mId", 0xA6400000, TdfType.Int64, 0, true), // Tag: ID
        new TdfMemberInfo("Type", "mType", 0xD39C2500, TdfType.Enum, 1, true), // Tag: TYPE
    ];
    private ITdfMember[] __members;

    private TdfInt64 _id = new(__typeInfos[0]);
    private TdfEnum<UserType> _type = new(__typeInfos[1]);

    public User()
    {
        __members = [ _id, _type ];
    }

    public override Tdf CreateNew() => new User();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "User";
    public override string GetFullClassName() => "Blaze::ByteVault::User";

    public long Id
    {
        get => _id.Value;
        set => _id.Value = value;
    }

    public UserType Type
    {
        get => _type.Value;
        set => _type.Value = value;
    }

}
