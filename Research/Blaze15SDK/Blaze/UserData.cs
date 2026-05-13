using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class UserData : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ExtendedData", "mExtendedData", 0x96487400, TdfType.Struct, 0, true), // Tag: EDAT
        new TdfMemberInfo("StatusFlags", "mStatusFlags", 0x9AC9F300, TdfType.Enum, 1, true), // Tag: FLGS
        new TdfMemberInfo("UserInfo", "mUserInfo", 0xD7397200, TdfType.Struct, 2, true), // Tag: USER
    ];
    private ITdfMember[] __members;

    private TdfStruct<Blaze15SDK.Blaze.UserSessionExtendedData?> _extendedData = new(__typeInfos[0]);
    private TdfEnum<Blaze15SDK.Blaze.UserDataFlags> _statusFlags = new(__typeInfos[1]);
    private TdfStruct<Blaze15SDK.Blaze.UserIdentification?> _userInfo = new(__typeInfos[2]);

    public UserData()
    {
        __members = [
            _extendedData,
            _statusFlags,
            _userInfo,
        ];
    }

    public override Tdf CreateNew() => new UserData();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "UserData";
    public override string GetFullClassName() => "Blaze::UserData";

    public Blaze15SDK.Blaze.UserSessionExtendedData? ExtendedData
    {
        get => _extendedData.Value;
        set => _extendedData.Value = value;
    }

    public Blaze15SDK.Blaze.UserDataFlags StatusFlags
    {
        get => _statusFlags.Value;
        set => _statusFlags.Value = value;
    }

    public Blaze15SDK.Blaze.UserIdentification? UserInfo
    {
        get => _userInfo.Value;
        set => _userInfo.Value = value;
    }

}
