using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class GetGameDataByUserRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ListConfigName", "mListConfigName", 0x92E86D00, TdfType.String, 0, true), // Tag: DNAM
        new TdfMemberInfo("UserSetId", "mUserSetId", 0x9F2C0000, TdfType.ObjectId, 1, true), // Tag: GRP
        new TdfMemberInfo("User", "mUser", 0xD7397200, TdfType.Struct, 2, true), // Tag: USER
    ];
    private ITdfMember[] __members;

    private TdfString _listConfigName = new(__typeInfos[0]);
    private TdfObjectId _userSetId = new(__typeInfos[1]);
    private TdfStruct<Blaze15SDK.Blaze.UserIdentification?> _user = new(__typeInfos[2]);

    public GetGameDataByUserRequest()
    {
        __members = [ _listConfigName, _userSetId, _user ];
    }

    public override Tdf CreateNew() => new GetGameDataByUserRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetGameDataByUserRequest";
    public override string GetFullClassName() => "Blaze::GameManager::GetGameDataByUserRequest";

    public string ListConfigName
    {
        get => _listConfigName.Value;
        set => _listConfigName.Value = value;
    }

    public ObjectId UserSetId
    {
        get => _userSetId.Value;
        set => _userSetId.Value = value;
    }

    public Blaze15SDK.Blaze.UserIdentification? User
    {
        get => _user.Value;
        set => _user.Value = value;
    }

}
