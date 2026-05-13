using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Util;

public class UserSettingsLoadMultipleResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("DataMap", "mDataMap", 0xCED87000, TdfType.Map, 0, true), // Tag: SMAP
    ];
    private ITdfMember[] __members;

    private TdfMap<string, string> _dataMap = new(__typeInfos[0]);

    public UserSettingsLoadMultipleResponse()
    {
        __members = [ _dataMap ];
    }

    public override Tdf CreateNew() => new UserSettingsLoadMultipleResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "UserSettingsLoadMultipleResponse";
    public override string GetFullClassName() => "Blaze::GameManager::UserSettingsLoadMultipleResponse";

    public IDictionary<string, string> DataMap
    {
        get => _dataMap.Value;
        set => _dataMap.Value = value;
    }

}
