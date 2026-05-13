using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class IsGameCaptureDoneResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("IsRedisDone", "mIsRedisDone", 0xA7290000, TdfType.Bool, 0, true), // Tag: IRD
    ];
    private ITdfMember[] __members;

    private TdfBool _isRedisDone = new(__typeInfos[0]);

    public IsGameCaptureDoneResponse()
    {
        __members = [ _isRedisDone ];
    }

    public override Tdf CreateNew() => new IsGameCaptureDoneResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "IsGameCaptureDoneResponse";
    public override string GetFullClassName() => "Blaze::GameManager::IsGameCaptureDoneResponse";

    public bool IsRedisDone
    {
        get => _isRedisDone.Value;
        set => _isRedisDone.Value = value;
    }

}
