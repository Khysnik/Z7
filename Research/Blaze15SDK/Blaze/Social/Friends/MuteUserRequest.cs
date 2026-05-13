using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Social.Friends;

public class MuteUserRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("AuthCredentials", "mAuthCredentials", 0x875D2800, TdfType.Struct, 0, true), // Tag: AUTH
        new TdfMemberInfo("FriendId", "mFriendId", 0x9AEA6400, TdfType.Int64, 1, true), // Tag: FNID
        new TdfMemberInfo("NucleusId", "mNucleusId", 0xD6EA6400, TdfType.Int64, 2, true), // Tag: UNID
    ];
    private ITdfMember[] __members;

    private TdfStruct<AuthenticationCredentials?> _authCredentials = new(__typeInfos[0]);
    private TdfInt64 _friendId = new(__typeInfos[1]);
    private TdfInt64 _nucleusId = new(__typeInfos[2]);

    public MuteUserRequest()
    {
        __members = [ _authCredentials, _friendId, _nucleusId ];
    }

    public override Tdf CreateNew() => new MuteUserRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "MuteUserRequest";
    public override string GetFullClassName() => "Blaze::Social::Friends::MuteUserRequest";

    public AuthenticationCredentials? AuthCredentials
    {
        get => _authCredentials.Value;
        set => _authCredentials.Value = value;
    }

    public long FriendId
    {
        get => _friendId.Value;
        set => _friendId.Value = value;
    }

    public long NucleusId
    {
        get => _nucleusId.Value;
        set => _nucleusId.Value = value;
    }

}
