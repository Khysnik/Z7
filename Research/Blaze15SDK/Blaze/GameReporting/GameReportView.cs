using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting;

public class GameReportView : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("FilterList", "mFilterList", 0x9A9B3400, TdfType.List, 0, true), // Tag: FILT
        new TdfMemberInfo("ViewInfo", "mViewInfo", 0xA6E9AF00, TdfType.Struct, 1, true), // Tag: INFO
        new TdfMemberInfo("Columns", "mColumns", 0xB27CA300, TdfType.List, 2, true), // Tag: LGRC
        new TdfMemberInfo("MaxGames", "mMaxGames", 0xB61E2700, TdfType.UInt32, 3, true), // Tag: MAXG
        new TdfMemberInfo("RowTypeName", "mRowTypeName", 0xCB4E7000, TdfType.String, 4, true), // Tag: RTYP
    ];
    private ITdfMember[] __members;

    private TdfList<GameReportFilter> _filterList = new(__typeInfos[0]);
    private TdfStruct<GameReportViewInfo?> _viewInfo = new(__typeInfos[1]);
    private TdfList<GameReportColumn> _columns = new(__typeInfos[2]);
    private TdfUInt32 _maxGames = new(__typeInfos[3]);
    private TdfString _rowTypeName = new(__typeInfos[4]);

    public GameReportView()
    {
        __members = [ _filterList, _viewInfo, _columns, _maxGames, _rowTypeName ];
    }

    public override Tdf CreateNew() => new GameReportView();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GameReportView";
    public override string GetFullClassName() => "Blaze::GameReporting::GameReportView";

    public IList<GameReportFilter> FilterList
    {
        get => _filterList.Value;
        set => _filterList.Value = value;
    }

    public GameReportViewInfo? ViewInfo
    {
        get => _viewInfo.Value;
        set => _viewInfo.Value = value;
    }

    public IList<GameReportColumn> Columns
    {
        get => _columns.Value;
        set => _columns.Value = value;
    }

    public uint MaxGames
    {
        get => _maxGames.Value;
        set => _maxGames.Value = value;
    }

    public string RowTypeName
    {
        get => _rowTypeName.Value;
        set => _rowTypeName.Value = value;
    }

}
