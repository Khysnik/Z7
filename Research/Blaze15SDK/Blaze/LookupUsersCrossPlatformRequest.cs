using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class LookupUsersCrossPlatformRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("LookupType", "mLookupType", 0xB34E7000, TdfType.Enum, 0, true), // Tag: LTYP
        new TdfMemberInfo("LookupOpts", "mLookupOpts", 0xBF0D3300, TdfType.Enum, 1, true), // Tag: OPTS
        new TdfMemberInfo("PlatformInfoList", "mPlatformInfoList", 0xC29B0000, TdfType.List, 2, true), // Tag: PIL
    ];
    private ITdfMember[] __members;

    private TdfEnum<Blaze15SDK.Blaze.CrossPlatformLookupType> _lookupType = new(__typeInfos[0]);
    private TdfEnum<Blaze15SDK.Blaze.LookupOpts> _lookupOpts = new(__typeInfos[1]);
    private TdfList<Blaze15SDK.Blaze.PlatformInfo> _platformInfoList = new(__typeInfos[2]);

    public LookupUsersCrossPlatformRequest()
    {
        __members = [
            _lookupType,
            _lookupOpts,
            _platformInfoList,
        ];
    }

    public override Tdf CreateNew() => new LookupUsersCrossPlatformRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "LookupUsersCrossPlatformRequest";
    public override string GetFullClassName() => "Blaze::LookupUsersCrossPlatformRequest";

    public Blaze15SDK.Blaze.CrossPlatformLookupType LookupType
    {
        get => _lookupType.Value;
        set => _lookupType.Value = value;
    }

    public Blaze15SDK.Blaze.LookupOpts LookupOpts
    {
        get => _lookupOpts.Value;
        set => _lookupOpts.Value = value;
    }

    public IList<Blaze15SDK.Blaze.PlatformInfo> PlatformInfoList
    {
        get => _platformInfoList.Value;
        set => _platformInfoList.Value = value;
    }

}
