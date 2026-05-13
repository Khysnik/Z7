using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameReporting;

public class GameHistoryReport : Tdf
{
    public class TableRow : Tdf
    {
        static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("AttributeMap", "mAttributeMap", 0xD32BF700, TdfType.Map, 0, true), // Tag: TROW
        ];
        private ITdfMember[] __members;

        private TdfMap<string, string> _attributeMap = new(__typeInfos[0]);

        public TableRow()
        {
            __members = [ _attributeMap ];
        }

        public override Tdf CreateNew() => new TableRow();
        public override ITdfMember[] GetMembers() => __members;
        public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
        public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
        public override string GetClassName() => "TableRow";
        public override string GetFullClassName() => "Blaze::GameReporting::GameHistoryReport::TableRow";

        public IDictionary<string, string> AttributeMap
        {
            get => _attributeMap.Value;
            set => _attributeMap.Value = value;
        }

    }

    public class TableRows : Tdf
    {
        static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("TableRowList", "mTableRowList", 0xCACA7300, TdfType.List, 0, true), // Tag: RLIS
        ];
        private ITdfMember[] __members;

        private TdfList<GameHistoryReport.TableRow> _tableRowList = new(__typeInfos[0]);

        public TableRows()
        {
            __members = [ _tableRowList ];
        }

        public override Tdf CreateNew() => new TableRows();
        public override ITdfMember[] GetMembers() => __members;
        public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
        public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
        public override string GetClassName() => "TableRows";
        public override string GetFullClassName() => "Blaze::GameReporting::GameHistoryReport::TableRows";

        public IList<GameHistoryReport.TableRow> TableRowList
        {
            get => _tableRowList.Value;
            set => _tableRowList.Value = value;
        }

    }

    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("FlagReason", "mFlagReason", 0x9A7CA500, TdfType.String, 0, true), // Tag: FGRE
        new TdfMemberInfo("Flags", "mFlags", 0x9AC86700, TdfType.UInt64, 1, true), // Tag: FLAG
        new TdfMemberInfo("GameHistoryId", "mGameHistoryId", 0x9E8A6400, TdfType.UInt64, 2, true), // Tag: GHID
        new TdfMemberInfo("GameReportingId", "mGameReportingId", 0x9F2A6400, TdfType.UInt64, 3, true), // Tag: GRID
        new TdfMemberInfo("GameReportName", "mGameReportName", 0x9F4E7000, TdfType.String, 4, true), // Tag: GTYP
        new TdfMemberInfo("Online", "mOnline", 0xBEEB2900, TdfType.Bool, 5, true), // Tag: ONLI
        new TdfMemberInfo("Timestamp", "mTimestamp", 0xD29B6500, TdfType.Int64, 6, true), // Tag: TIME
        new TdfMemberInfo("TableRowMap", "mTableRowMap", 0xD32B4000, TdfType.Map, 7, true), // Tag: TRM
    ];
    private ITdfMember[] __members;

    private TdfString _flagReason = new(__typeInfos[0]);
    private TdfUInt64 _flags = new(__typeInfos[1]);
    private TdfUInt64 _gameHistoryId = new(__typeInfos[2]);
    private TdfUInt64 _gameReportingId = new(__typeInfos[3]);
    private TdfString _gameReportName = new(__typeInfos[4]);
    private TdfBool _online = new(__typeInfos[5]);
    private TdfInt64 _timestamp = new(__typeInfos[6]);
    private TdfMap<string, GameHistoryReport.TableRows> _tableRowMap = new(__typeInfos[7]);

    public GameHistoryReport()
    {
        __members = [ _flagReason, _flags, _gameHistoryId, _gameReportingId, _gameReportName, _online, _timestamp, _tableRowMap ];
    }

    public override Tdf CreateNew() => new GameHistoryReport();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "GameHistoryReport";
    public override string GetFullClassName() => "Blaze::GameReporting::GameHistoryReport";

    public string FlagReason
    {
        get => _flagReason.Value;
        set => _flagReason.Value = value;
    }

    public ulong Flags
    {
        get => _flags.Value;
        set => _flags.Value = value;
    }

    public ulong GameHistoryId
    {
        get => _gameHistoryId.Value;
        set => _gameHistoryId.Value = value;
    }

    public ulong GameReportingId
    {
        get => _gameReportingId.Value;
        set => _gameReportingId.Value = value;
    }

    public string GameReportName
    {
        get => _gameReportName.Value;
        set => _gameReportName.Value = value;
    }

    public bool Online
    {
        get => _online.Value;
        set => _online.Value = value;
    }

    public long Timestamp
    {
        get => _timestamp.Value;
        set => _timestamp.Value = value;
    }

    public IDictionary<string, GameHistoryReport.TableRows> TableRowMap
    {
        get => _tableRowMap.Value;
        set => _tableRowMap.Value = value;
    }

}
