using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class ModRuleCriteria : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("IsEnabled", "mIsEnabled", 0xA7396E00, TdfType.Bool, 0, true), // Tag: ISEN
        new TdfMemberInfo("DesiredModRegister", "mDesiredModRegister", 0xB6F93300, TdfType.UInt32, 1, true), // Tag: MODS
    ];
    private ITdfMember[] __members;

    private TdfBool _isEnabled = new(__typeInfos[0]);
    private TdfUInt32 _desiredModRegister = new(__typeInfos[1]);

    public ModRuleCriteria()
    {
        __members = [ _isEnabled, _desiredModRegister ];
    }

    public override Tdf CreateNew() => new ModRuleCriteria();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ModRuleCriteria";
    public override string GetFullClassName() => "Blaze::GameManager::ModRuleCriteria";

    public bool IsEnabled
    {
        get => _isEnabled.Value;
        set => _isEnabled.Value = value;
    }

    public uint DesiredModRegister
    {
        get => _desiredModRegister.Value;
        set => _desiredModRegister.Value = value;
    }

}
