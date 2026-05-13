using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class GameCaptureResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("LastSaveTime", "mLastSaveTime", 0xB33D0000, TdfType.UInt64, 0, true), // Tag: LST
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _lastSaveTime = new(__typeInfos[0]);

    public GameCaptureResponse()
    {
        __members = [ _lastSaveTime ];
    }

    public override Tdf CreateNew() => new GameCaptureResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GameCaptureResponse";
    public override string GetFullClassName() => "Blaze::GameManager::GameCaptureResponse";

    public ulong LastSaveTime
    {
        get => _lastSaveTime.Value;
        set => _lastSaveTime.Value = value;
    }

}
