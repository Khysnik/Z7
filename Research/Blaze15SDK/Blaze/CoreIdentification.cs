using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class CoreIdentification : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("PlatformInfo", "mPlatformInfo", 0x86993300, TdfType.Struct, 0, true), // Tag: AIDS
        new TdfMemberInfo("ExternalId", "mExternalId", 0x978A6400, TdfType.UInt64, 1, true), // Tag: EXID
        new TdfMemberInfo("BlazeId", "mBlazeId", 0xA6400000, TdfType.Int64, 2, true), // Tag: ID
        new TdfMemberInfo("Name", "mName", 0xBA1B6500, TdfType.String, 3, true), // Tag: NAME
        new TdfMemberInfo("PersonaNamespace", "mPersonaNamespace", 0xBA1CF000, TdfType.String, 4, true), // Tag: NASP
    ];
    private ITdfMember[] __members;

    private TdfStruct<Blaze15SDK.Blaze.PlatformInfo?> _platformInfo = new(__typeInfos[0]);
    private TdfUInt64 _externalId = new(__typeInfos[1]);
    private TdfInt64 _blazeId = new(__typeInfos[2]);
    private TdfString _name = new(__typeInfos[3]);
    private TdfString _personaNamespace = new(__typeInfos[4]);

    public CoreIdentification()
    {
        __members = [
            _platformInfo,
            _externalId,
            _blazeId,
            _name,
            _personaNamespace,
        ];
    }

    public override Tdf CreateNew() => new CoreIdentification();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "CoreIdentification";
    public override string GetFullClassName() => "Blaze::CoreIdentification";

    public Blaze15SDK.Blaze.PlatformInfo? PlatformInfo
    {
        get => _platformInfo.Value;
        set => _platformInfo.Value = value;
    }

    public ulong ExternalId
    {
        get => _externalId.Value;
        set => _externalId.Value = value;
    }

    public long BlazeId
    {
        get => _blazeId.Value;
        set => _blazeId.Value = value;
    }

    public string Name
    {
        get => _name.Value;
        set => _name.Value = value;
    }

    public string PersonaNamespace
    {
        get => _personaNamespace.Value;
        set => _personaNamespace.Value = value;
    }

}
