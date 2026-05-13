using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class PlatformRuleCriteria : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("CrossplayMustMatch", "mCrossplayMustMatch", 0x8F0B6D00, TdfType.Bool, 0, true), // Tag: CPMM
        new TdfMemberInfo("ClientPlatformListOverride", "mClientPlatformListOverride", 0x8F0BF600, TdfType.List, 1, true), // Tag: CPOV
    ];
    private ITdfMember[] __members;

    private TdfBool _crossplayMustMatch = new(__typeInfos[0]);
    private TdfList<Blaze15SDK.Blaze.ClientPlatformType> _clientPlatformListOverride = new(__typeInfos[1]);

    public PlatformRuleCriteria()
    {
        __members = [ _crossplayMustMatch, _clientPlatformListOverride ];
    }

    public override Tdf CreateNew() => new PlatformRuleCriteria();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "PlatformRuleCriteria";
    public override string GetFullClassName() => "Blaze::GameManager::PlatformRuleCriteria";

    public bool CrossplayMustMatch
    {
        get => _crossplayMustMatch.Value;
        set => _crossplayMustMatch.Value = value;
    }

    public IList<Blaze15SDK.Blaze.ClientPlatformType> ClientPlatformListOverride
    {
        get => _clientPlatformListOverride.Value;
        set => _clientPlatformListOverride.Value = value;
    }

}
