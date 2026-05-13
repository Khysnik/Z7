using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Util;

public class DeleteUserSettingsRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("Key", "mKey", 0xAE5E4000, TdfType.String, 0, true), // Tag: KEY
        new TdfMemberInfo("BlazeId", "mBlazeId", 0xD6990000, TdfType.Int64, 1, true), // Tag: UID
    ];
    private ITdfMember[] __members;

    private TdfString _key = new(__typeInfos[0]);
    private TdfInt64 _blazeId = new(__typeInfos[1]);

    public DeleteUserSettingsRequest()
    {
        __members = [ _key, _blazeId ];
    }

    public override Tdf CreateNew() => new DeleteUserSettingsRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "DeleteUserSettingsRequest";
    public override string GetFullClassName() => "Blaze::GameManager::DeleteUserSettingsRequest";

    public string Key
    {
        get => _key.Value;
        set => _key.Value = value;
    }

    public long BlazeId
    {
        get => _blazeId.Value;
        set => _blazeId.Value = value;
    }

}
