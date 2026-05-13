using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Util;

public class UserSettingsLoadMultipleRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Keys", "mKeys", 0xAE5E7300, TdfType.List, 0, true), // Tag: KEYS
        new TdfMemberInfo("BlazeId", "mBlazeId", 0xD6990000, TdfType.Int64, 1, true), // Tag: UID
    ];
    private ITdfMember[] __members;

    private TdfList<string> _keys = new(__typeInfos[0]);
    private TdfInt64 _blazeId = new(__typeInfos[1]);

    public UserSettingsLoadMultipleRequest()
    {
        __members = [ _keys, _blazeId ];
    }

    public override Tdf CreateNew() => new UserSettingsLoadMultipleRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "UserSettingsLoadMultipleRequest";
    public override string GetFullClassName() => "Blaze::GameManager::UserSettingsLoadMultipleRequest";

    public IList<string> Keys
    {
        get => _keys.Value;
        set => _keys.Value = value;
    }

    public long BlazeId
    {
        get => _blazeId.Value;
        set => _blazeId.Value = value;
    }

}
