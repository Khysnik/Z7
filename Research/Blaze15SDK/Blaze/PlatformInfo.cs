using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class PlatformInfo : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("EaIds", "mEaIds", 0x961A6400, TdfType.Struct, 0, true), // Tag: EAID
        new TdfMemberInfo("ExternalIds", "mExternalIds", 0x978A6400, TdfType.Struct, 1, true), // Tag: EXID
        new TdfMemberInfo("ClientPlatform", "mClientPlatform", 0xC2C87400, TdfType.Enum, 2, true), // Tag: PLAT
    ];
    private ITdfMember[] __members;

    private TdfStruct<Blaze15SDK.Blaze.EaIdentifiers?> _eaIds = new(__typeInfos[0]);
    private TdfStruct<Blaze15SDK.Blaze.ExternalIdentifiers?> _externalIds = new(__typeInfos[1]);
    private TdfEnum<Blaze15SDK.Blaze.ClientPlatformType> _clientPlatform = new(__typeInfos[2]);

    public PlatformInfo()
    {
        __members = [
            _eaIds,
            _externalIds,
            _clientPlatform,
        ];
    }

    public override Tdf CreateNew() => new PlatformInfo();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "PlatformInfo";
    public override string GetFullClassName() => "Blaze::PlatformInfo";

    public Blaze15SDK.Blaze.EaIdentifiers? EaIds
    {
        get => _eaIds.Value;
        set => _eaIds.Value = value;
    }

    public Blaze15SDK.Blaze.ExternalIdentifiers? ExternalIds
    {
        get => _externalIds.Value;
        set => _externalIds.Value = value;
    }

    public Blaze15SDK.Blaze.ClientPlatformType ClientPlatform
    {
        get => _clientPlatform.Value;
        set => _clientPlatform.Value = value;
    }

}
