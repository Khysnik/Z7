using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class GetGameListResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ListId", "mListId", 0x9ECA6400, TdfType.UInt64, 0, true), // Tag: GLID
        new TdfMemberInfo("IsSnapshotList", "mIsSnapshotList", 0x9EDB3400, TdfType.Bool, 1, true), // Tag: GMLT
        new TdfMemberInfo("ListCapacity", "mListCapacity", 0xB2387000, TdfType.UInt32, 2, true), // Tag: LCAP
        new TdfMemberInfo("MaxPossibleFitScore", "mMaxPossibleFitScore", 0xB61E2600, TdfType.UInt32, 3, true), // Tag: MAXF
        new TdfMemberInfo("NumberOfGamesToBeDownloaded", "mNumberOfGamesToBeDownloaded", 0xBA790000, TdfType.UInt32, 4, true), // Tag: NGD
        new TdfMemberInfo("MaxUpdateInterval", "mMaxUpdateInterval", 0xD70A6E00, TdfType.Int64, 5, true), // Tag: UPIN
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _listId = new(__typeInfos[0]);
    private TdfBool _isSnapshotList = new(__typeInfos[1]);
    private TdfUInt32 _listCapacity = new(__typeInfos[2]);
    private TdfUInt32 _maxPossibleFitScore = new(__typeInfos[3]);
    private TdfUInt32 _numberOfGamesToBeDownloaded = new(__typeInfos[4]);
    private TdfInt64 _maxUpdateInterval = new(__typeInfos[5]);

    public GetGameListResponse()
    {
        __members = [ _listId, _isSnapshotList, _listCapacity, _maxPossibleFitScore, _numberOfGamesToBeDownloaded, _maxUpdateInterval ];
    }

    public override Tdf CreateNew() => new GetGameListResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetGameListResponse";
    public override string GetFullClassName() => "Blaze::GameManager::GetGameListResponse";

    public ulong ListId
    {
        get => _listId.Value;
        set => _listId.Value = value;
    }

    public bool IsSnapshotList
    {
        get => _isSnapshotList.Value;
        set => _isSnapshotList.Value = value;
    }

    public uint ListCapacity
    {
        get => _listCapacity.Value;
        set => _listCapacity.Value = value;
    }

    public uint MaxPossibleFitScore
    {
        get => _maxPossibleFitScore.Value;
        set => _maxPossibleFitScore.Value = value;
    }

    public uint NumberOfGamesToBeDownloaded
    {
        get => _numberOfGamesToBeDownloaded.Value;
        set => _numberOfGamesToBeDownloaded.Value = value;
    }

    public long MaxUpdateInterval
    {
        get => _maxUpdateInterval.Value;
        set => _maxUpdateInterval.Value = value;
    }

}
