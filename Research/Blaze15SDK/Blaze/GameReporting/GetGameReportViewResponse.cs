using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting;

public class GetGameReportViewResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("EntityIds", "mEntityIds", 0x96EA6400, TdfType.List, 0, true), // Tag: ENID
        new TdfMemberInfo("Columns", "mColumns", 0xB27CA300, TdfType.List, 1, true), // Tag: LGRC
    ];
    private ITdfMember[] __members;

    private TdfList<ulong> _entityIds = new(__typeInfos[0]);
    private TdfList<GameReportColumn> _columns = new(__typeInfos[1]);

    public GetGameReportViewResponse()
    {
        __members = [ _entityIds, _columns ];
    }

    public override Tdf CreateNew() => new GetGameReportViewResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetGameReportViewResponse";
    public override string GetFullClassName() => "Blaze::GameReporting::GetGameReportViewResponse";

    public IList<ulong> EntityIds
    {
        get => _entityIds.Value;
        set => _entityIds.Value = value;
    }

    public IList<GameReportColumn> Columns
    {
        get => _columns.Value;
        set => _columns.Value = value;
    }

}
