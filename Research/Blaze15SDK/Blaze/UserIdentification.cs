using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class UserIdentification : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("AccountId", "mAccountId", 0x86990000, TdfType.Int64, 0, true), // Tag: AID
        new TdfMemberInfo("PlatformInfo", "mPlatformInfo", 0x86993300, TdfType.Struct, 1, true), // Tag: AIDS
        new TdfMemberInfo("AccountLocale", "mAccountLocale", 0x86CBE300, TdfType.UInt32, 2, true), // Tag: ALOC
        new TdfMemberInfo("AccountCountry", "mAccountCountry", 0x8EED3900, TdfType.UInt32, 3, true), // Tag: CNTY
        new TdfMemberInfo("ExternalId", "mExternalId", 0x978A6400, TdfType.UInt64, 4, true), // Tag: EXID
        new TdfMemberInfo("BlazeId", "mBlazeId", 0xA6400000, TdfType.Int64, 5, true), // Tag: ID
        new TdfMemberInfo("IsPrimaryPersona", "mIsPrimaryPersona", 0xA73C3000, TdfType.Bool, 6, true), // Tag: ISPP
        new TdfMemberInfo("Name", "mName", 0xBA1B6500, TdfType.String, 7, true), // Tag: NAME
        new TdfMemberInfo("PersonaNamespace", "mPersonaNamespace", 0xBA1CF000, TdfType.String, 8, true), // Tag: NASP
        new TdfMemberInfo("OriginPersonaId", "mOriginPersonaId", 0xBF2A6700, TdfType.UInt64, 9, true), // Tag: ORIG
        new TdfMemberInfo("PidId", "mPidId", 0xC2992900, TdfType.Int64, 10, true), // Tag: PIDI
    ];
    private ITdfMember[] __members;

    private TdfInt64 _accountId = new(__typeInfos[0]);
    private TdfStruct<Blaze15SDK.Blaze.PlatformInfo?> _platformInfo = new(__typeInfos[1]);
    private TdfUInt32 _accountLocale = new(__typeInfos[2]);
    private TdfUInt32 _accountCountry = new(__typeInfos[3]);
    private TdfUInt64 _externalId = new(__typeInfos[4]);
    private TdfInt64 _blazeId = new(__typeInfos[5]);
    private TdfBool _isPrimaryPersona = new(__typeInfos[6]);
    private TdfString _name = new(__typeInfos[7]);
    private TdfString _personaNamespace = new(__typeInfos[8]);
    private TdfUInt64 _originPersonaId = new(__typeInfos[9]);
    private TdfInt64 _pidId = new(__typeInfos[10]);

    public UserIdentification()
    {
        __members = [
            _accountId,
            _platformInfo,
            _accountLocale,
            _accountCountry,
            _externalId,
            _blazeId,
            _isPrimaryPersona,
            _name,
            _personaNamespace,
            _originPersonaId,
            _pidId,
        ];
    }

    public override Tdf CreateNew() => new UserIdentification();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "UserIdentification";
    public override string GetFullClassName() => "Blaze::UserIdentification";

    public long AccountId
    {
        get => _accountId.Value;
        set => _accountId.Value = value;
    }

    public Blaze15SDK.Blaze.PlatformInfo? PlatformInfo
    {
        get => _platformInfo.Value;
        set => _platformInfo.Value = value;
    }

    public uint AccountLocale
    {
        get => _accountLocale.Value;
        set => _accountLocale.Value = value;
    }

    public uint AccountCountry
    {
        get => _accountCountry.Value;
        set => _accountCountry.Value = value;
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

    public bool IsPrimaryPersona
    {
        get => _isPrimaryPersona.Value;
        set => _isPrimaryPersona.Value = value;
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

    public ulong OriginPersonaId
    {
        get => _originPersonaId.Value;
        set => _originPersonaId.Value = value;
    }

    public long PidId
    {
        get => _pidId.Value;
        set => _pidId.Value = value;
    }

}
