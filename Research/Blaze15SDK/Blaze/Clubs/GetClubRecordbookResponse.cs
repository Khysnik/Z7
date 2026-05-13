using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Clubs;

public class GetClubRecordbookResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("ClubRecordList", "mClubRecordList", 0x8ECCAC00, TdfType.List, 0, true), // Tag: CLRL
    ];
    private ITdfMember[] __members;

    private TdfList<ClubRecord> _clubRecordList = new(__typeInfos[0]);

    public GetClubRecordbookResponse()
    {
        __members = [ _clubRecordList ];
    }

    public override Tdf CreateNew() => new GetClubRecordbookResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetClubRecordbookResponse";
    public override string GetFullClassName() => "Blaze::Clubs::GetClubRecordbookResponse";

    public IList<ClubRecord> ClubRecordList
    {
        get => _clubRecordList.Value;
        set => _clubRecordList.Value = value;
    }

}
