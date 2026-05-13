using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class LookupUsersRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("LookupType", "mLookupType", 0xB34E7000, TdfType.Enum, 0, true), // Tag: LTYP
        new TdfMemberInfo("UserIdentificationList", "mUserIdentificationList", 0xD6CCF400, TdfType.List, 1, true), // Tag: ULST
    ];
    private ITdfMember[] __members;

    private TdfEnum<Blaze15SDK.Blaze.LookupType> _lookupType = new(__typeInfos[0]);
    private TdfList<Blaze15SDK.Blaze.UserIdentification> _userIdentificationList = new(__typeInfos[1]);

    public LookupUsersRequest()
    {
        __members = [
            _lookupType,
            _userIdentificationList,
        ];
    }

    public override Tdf CreateNew() => new LookupUsersRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "LookupUsersRequest";
    public override string GetFullClassName() => "Blaze::LookupUsersRequest";

    public Blaze15SDK.Blaze.LookupType mLookupType
    {
        get => _lookupType.Value;
        set => _lookupType.Value = value;
    }

    public IList<Blaze15SDK.Blaze.UserIdentification> UserIdentificationList
    {
        get => _userIdentificationList.Value;
        set => _userIdentificationList.Value = value;
    }

}
