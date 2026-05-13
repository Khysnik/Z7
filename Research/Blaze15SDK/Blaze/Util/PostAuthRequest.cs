using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Util;

public class PostAuthRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("DirtySockUserIndex", "mDirtySockUserIndex", 0x933D6900, TdfType.Int32, 0, true), // Tag: DSUI
        new TdfMemberInfo("UseKey2", "mUseKey2", 0xAE5E5200, TdfType.Bool, 1, true), // Tag: KEY2
        new TdfMemberInfo("UniqueDeviceId", "mUniqueDeviceId", 0xD64A6400, TdfType.String, 2, true), // Tag: UDID
    ];
    private ITdfMember[] __members;

    private TdfInt32 _dirtySockUserIndex = new(__typeInfos[0]);
    private TdfBool _useKey2 = new(__typeInfos[1]);
    private TdfString _uniqueDeviceId = new(__typeInfos[2]);

    public PostAuthRequest()
    {
        __members = [ _dirtySockUserIndex, _useKey2, _uniqueDeviceId ];
    }

    public override Tdf CreateNew() => new PostAuthRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "PostAuthRequest";
    public override string GetFullClassName() => "Blaze::GameManager::PostAuthRequest";

    public int DirtySockUserIndex
    {
        get => _dirtySockUserIndex.Value;
        set => _dirtySockUserIndex.Value = value;
    }

    public bool UseKey2
    {
        get => _useKey2.Value;
        set => _useKey2.Value = value;
    }

    public string UniqueDeviceId
    {
        get => _uniqueDeviceId.Value;
        set => _uniqueDeviceId.Value = value;
    }

}
