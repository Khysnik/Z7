using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GpsContentController;

public class FilePetitionRequest : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("AttributeMap", "attributeMap", 0x86EDB000, TdfType.Map, 0, true), // Tag: ANVP
        new TdfMemberInfo("ContentId", "mContentId", 0x8EFA6400, TdfType.ObjectId, 1, true), // Tag: COID
        new TdfMemberInfo("ComplaintType", "mComplaintType", 0x8EFD3900, TdfType.String, 2, true), // Tag: COTY
        new TdfMemberInfo("ContentType", "mContentType", 0xC2E8F400, TdfType.Enum, 3, true), // Tag: PNCT
        new TdfMemberInfo("PetitionDetail", "mPetitionDetail", 0xC3492500, TdfType.String, 4, true), // Tag: PTDE
        new TdfMemberInfo("Subject", "mSubject", 0xCF58AA00, TdfType.String, 5, true), // Tag: SUBJ
        new TdfMemberInfo("TimeZone", "mTimeZone", 0xD2DEAF00, TdfType.String, 6, true), // Tag: TMZO
        new TdfMemberInfo("TargetUsers", "mTargetUsers", 0xD329F400, TdfType.List, 7, true), // Tag: TRGT
    ];
    private ITdfMember[] __members;

    private TdfMap<string, string> _attributeMap = new(__typeInfos[0]);
    private TdfObjectId _contentId = new(__typeInfos[1]);
    private TdfString _complaintType = new(__typeInfos[2]);
    private TdfEnum<PetitionContentType> _contentType = new(__typeInfos[3]);
    private TdfString _petitionDetail = new(__typeInfos[4]);
    private TdfString _subject = new(__typeInfos[5]);
    private TdfString _timeZone = new(__typeInfos[6]);
    private TdfList<long> _targetUsers = new(__typeInfos[7]);

    public FilePetitionRequest()
    {
        __members = [
            _attributeMap,
            _contentId,
            _complaintType,
            _contentType,
            _petitionDetail,
            _subject,
            _timeZone,
            _targetUsers,
        ];
    }

    public override Tdf CreateNew() => new FilePetitionRequest();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "FilePetitionRequest";
    public override string GetFullClassName() => "Blaze::GpsContentController::FilePetitionRequest";

    public IDictionary<string, string> AttributeMap
    {
        get => _attributeMap.Value;
        set => _attributeMap.Value = value;
    }

    public EATDF.Types.ObjectId ContentId
    {
        get => _contentId.Value;
        set => _contentId.Value = value;
    }

    public string ComplaintType
    {
        get => _complaintType.Value;
        set => _complaintType.Value = value;
    }

    public PetitionContentType ContentType
    {
        get => _contentType.Value;
        set => _contentType.Value = value;
    }

    public string PetitionDetail
    {
        get => _petitionDetail.Value;
        set => _petitionDetail.Value = value;
    }

    public string Subject
    {
        get => _subject.Value;
        set => _subject.Value = value;
    }

    public string TimeZone
    {
        get => _timeZone.Value;
        set => _timeZone.Value = value;
    }

    public IList<long> TargetUsers
    {
        get => _targetUsers.Value;
        set => _targetUsers.Value = value;
    }

}
