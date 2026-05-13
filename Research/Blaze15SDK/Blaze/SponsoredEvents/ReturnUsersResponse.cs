using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.SponsoredEvents;

public class ReturnUsersResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("NumUsers", "mNumUsers", 0xBB5B4000, TdfType.Int32, 0, true), // Tag: NUM
        new TdfMemberInfo("RegisteredUsers", "mRegisteredUsers", 0xCAFDF300, TdfType.List, 1, true), // Tag: ROWS
    ];
    private ITdfMember[] __members;

    private TdfInt32 _numUsers = new(__typeInfos[0]);
    private TdfList<SponsoredEventsUser> _registeredUsers = new(__typeInfos[1]);

    public ReturnUsersResponse()
    {
        __members = [_numUsers, _registeredUsers];
    }

    public override Tdf CreateNew() => new ReturnUsersResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ReturnUsersResponse";
    public override string GetFullClassName() => "Blaze::SponsoredEvents::ReturnUsersResponse";

    public int NumUsers
    {
        get => _numUsers.Value;
        set => _numUsers.Value = value;
    }

    public IList<SponsoredEventsUser> RegisteredUsers
    {
        get => _registeredUsers.Value;
        set => _registeredUsers.Value = value;
    }
}
