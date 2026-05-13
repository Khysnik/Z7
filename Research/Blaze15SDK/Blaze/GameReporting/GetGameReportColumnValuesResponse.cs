using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting;

public class GetGameReportColumnValuesResponse : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("EntityIds", "mEntityIds", 0x96EA6400, TdfType.List, 0, true), // Tag: ENID
        new TdfMemberInfo("ColumnValues", "mColumnValues", 0xB27CA300, TdfType.List, 1, true), // Tag: LGRC
    ];
    private ITdfMember[] __members;

    private TdfList<ulong> _entityIds = new(__typeInfos[0]);
    private TdfList<GameReportColumnValues> _columnValues = new(__typeInfos[1]);

    public GetGameReportColumnValuesResponse()
    {
        __members = [ _entityIds, _columnValues ];
    }

    public override Tdf CreateNew() => new GetGameReportColumnValuesResponse();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GetGameReportColumnValuesResponse";
    public override string GetFullClassName() => "Blaze::GameReporting::GetGameReportColumnValuesResponse";

    public IList<ulong> EntityIds
    {
        get => _entityIds.Value;
        set => _entityIds.Value = value;
    }

    public IList<GameReportColumnValues> ColumnValues
    {
        get => _columnValues.Value;
        set => _columnValues.Value = value;
    }

}
