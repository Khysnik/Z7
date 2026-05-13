using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Authentication;

public class ListPersonaEntitlements2Request : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("EndGrantDate", "mEndGrantDate", 0x96792100, TdfType.String, 0, true), // Tag: EGDA
        new TdfMemberInfo("PageNo", "mPageNo", 0x970CEE00, TdfType.UInt16, 1, true), // Tag: EPSN
        new TdfMemberInfo("PageSize", "mPageSize", 0x970CFA00, TdfType.UInt16, 2, true), // Tag: EPSZ
        new TdfMemberInfo("EntitlementTag", "mEntitlementTag", 0x97486700, TdfType.String, 3, true), // Tag: ETAG
        new TdfMemberInfo("EndTerminationDate", "mEndTerminationDate", 0x97492100, TdfType.String, 4, true), // Tag: ETDA
        new TdfMemberInfo("GroupNameList", "mGroupNameList", 0x9EEB3300, TdfType.List, 5, true), // Tag: GNLS
        new TdfMemberInfo("PersonaId", "mPersonaId", 0xC2990000, TdfType.Int64, 6, true), // Tag: PID
        new TdfMemberInfo("ProjectId", "mProjectId", 0xC2AA6400, TdfType.String, 7, true), // Tag: PJID
        new TdfMemberInfo("ProductId", "mProductId", 0xC32A6400, TdfType.String, 8, true), // Tag: PRID
        new TdfMemberInfo("RecursiveSearch", "mRecursiveSearch", 0xCA58F500, TdfType.Bool, 9, true), // Tag: RECU
        new TdfMemberInfo("StartGrantDate", "mStartGrantDate", 0xCE792100, TdfType.String, 10, true), // Tag: SGDA
        new TdfMemberInfo("Status", "mStatus", 0xCF487400, TdfType.Enum, 11, true), // Tag: STAT
        new TdfMemberInfo("StartTerminationDate", "mStartTerminationDate", 0xCF492100, TdfType.String, 12, true), // Tag: STDA
        new TdfMemberInfo("EntitlementType", "mEntitlementType", 0xD39C2500, TdfType.Enum, 13, true), // Tag: TYPE
    ];
    private ITdfMember[] __members;

    private TdfString _endGrantDate = new(__typeInfos[0]);
    private TdfUInt16 _pageNo = new(__typeInfos[1]);
    private TdfUInt16 _pageSize = new(__typeInfos[2]);
    private TdfString _entitlementTag = new(__typeInfos[3]);
    private TdfString _endTerminationDate = new(__typeInfos[4]);
    private TdfList<string> _groupNameList = new(__typeInfos[5]);
    private TdfInt64 _personaId = new(__typeInfos[6]);
    private TdfString _projectId = new(__typeInfos[7]);
    private TdfString _productId = new(__typeInfos[8]);
    private TdfBool _recursiveSearch = new(__typeInfos[9]);
    private TdfString _startGrantDate = new(__typeInfos[10]);
    private TdfEnum<Blaze15SDK.Blaze.Authentication.EntitlementStatus> _status = new(__typeInfos[11]);
    private TdfString _startTerminationDate = new(__typeInfos[12]);
    private TdfEnum<Blaze15SDK.Blaze.Authentication.EntitlementType> _entitlementType = new(__typeInfos[13]);

    public ListPersonaEntitlements2Request()
    {
        __members = [
            _endGrantDate,
            _pageNo,
            _pageSize,
            _entitlementTag,
            _endTerminationDate,
            _groupNameList,
            _personaId,
            _projectId,
            _productId,
            _recursiveSearch,
            _startGrantDate,
            _status,
            _startTerminationDate,
            _entitlementType,
        ];
    }

    public override Tdf CreateNew() => new ListPersonaEntitlements2Request();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ListPersonaEntitlements2Request";
    public override string GetFullClassName() => "Blaze::Authentication::ListPersonaEntitlements2Request";

    public string EndGrantDate { get => _endGrantDate.Value; set => _endGrantDate.Value = value; }
    public ushort PageNo { get => _pageNo.Value; set => _pageNo.Value = value; }
    public ushort PageSize { get => _pageSize.Value; set => _pageSize.Value = value; }
    public string EntitlementTag { get => _entitlementTag.Value; set => _entitlementTag.Value = value; }
    public string EndTerminationDate { get => _endTerminationDate.Value; set => _endTerminationDate.Value = value; }
    public IList<string> GroupNameList { get => _groupNameList.Value; set => _groupNameList.Value = value; }
    public long PersonaId { get => _personaId.Value; set => _personaId.Value = value; }
    public string ProjectId { get => _projectId.Value; set => _projectId.Value = value; }
    public string ProductId { get => _productId.Value; set => _productId.Value = value; }
    public bool RecursiveSearch { get => _recursiveSearch.Value; set => _recursiveSearch.Value = value; }
    public string StartGrantDate { get => _startGrantDate.Value; set => _startGrantDate.Value = value; }
    public Blaze15SDK.Blaze.Authentication.EntitlementStatus Status { get => _status.Value; set => _status.Value = value; }
    public string StartTerminationDate { get => _startTerminationDate.Value; set => _startTerminationDate.Value = value; }
    public Blaze15SDK.Blaze.Authentication.EntitlementType EntitlementType { get => _entitlementType.Value; set => _entitlementType.Value = value; }
}
