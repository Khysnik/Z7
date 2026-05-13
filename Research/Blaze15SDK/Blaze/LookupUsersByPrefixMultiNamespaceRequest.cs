using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze;

public class LookupUsersByPrefixMultiNamespaceRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("MaxResultCount", "mMaxResultCount", 0x8E1C0000, TdfType.UInt32, 0, true), // Tag: CAP
        new TdfMemberInfo("PrimaryNamespaceOnly", "mPrimaryNamespaceOnly", 0xC2FBAC00, TdfType.Bool, 1, true), // Tag: PONL
        new TdfMemberInfo("PrefixName", "mPrefixName", 0xC3296600, TdfType.String, 2, true), // Tag: PREF
    ];
    private ITdfMember[] __members;

    private TdfUInt32 _maxResultCount = new(__typeInfos[0]);
    private TdfBool _primaryNamespaceOnly = new(__typeInfos[1]);
    private TdfString _prefixName = new(__typeInfos[2]);

    public LookupUsersByPrefixMultiNamespaceRequest()
    {
        __members = [
            _maxResultCount,
            _primaryNamespaceOnly,
            _prefixName,
        ];
    }

    public override Tdf CreateNew() => new LookupUsersByPrefixMultiNamespaceRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "LookupUsersByPrefixMultiNamespaceRequest";
    public override string GetFullClassName() => "Blaze::LookupUsersByPrefixMultiNamespaceRequest";

    public uint MaxResultCount
    {
        get => _maxResultCount.Value;
        set => _maxResultCount.Value = value;
    }

    public bool PrimaryNamespaceOnly
    {
        get => _primaryNamespaceOnly.Value;
        set => _primaryNamespaceOnly.Value = value;
    }

    public string PrefixName
    {
        get => _prefixName.Value;
        set => _prefixName.Value = value;
    }

}
