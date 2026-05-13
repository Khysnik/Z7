using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.SponsoredEvents;

public class NumUsersResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("NumUsers", "mNumUsers", 0xBB5B4000, TdfType.Int32, 0, true), // Tag: NUM
    ];
    private ITdfMember[] __members;

    private TdfInt32 _numUsers = new(__typeInfos[0]);

    public NumUsersResponse()
    {
        __members = [_numUsers];
    }

    public override Tdf CreateNew() => new NumUsersResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "NumUsersResponse";
    public override string GetFullClassName() => "Blaze::SponsoredEvents::NumUsersResponse";

    public int NumUsers
    {
        get => _numUsers.Value;
        set => _numUsers.Value = value;
    }
}
