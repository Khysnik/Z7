using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.ByteVault;

public class DataRates : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("AverageGetRecordSize", "mAverageGetRecordSize", 0x9E1DA700, TdfType.UInt64, 0, true), // Tag: GAVG
        new TdfMemberInfo("MaximumGetRecordSize", "mMaximumGetRecordSize", 0x9ED87800, TdfType.UInt64, 1, true), // Tag: GMAX
        new TdfMemberInfo("MinimumGetRecordSize", "mMinimumGetRecordSize", 0x9EDA6E00, TdfType.UInt64, 2, true), // Tag: GMIN
        new TdfMemberInfo("NumOfGetRecords", "mNumOfGetRecords", 0x9EEB7200, TdfType.UInt32, 3, true), // Tag: GNMR
        new TdfMemberInfo("BytesIn", "mBytesIn", 0xA7287400, TdfType.UInt64, 4, true), // Tag: IRAT
        new TdfMemberInfo("BytesOut", "mBytesOut", 0xBF287400, TdfType.UInt64, 5, true), // Tag: ORAT
        new TdfMemberInfo("AverageUpsertRecordSize", "mAverageUpsertRecordSize", 0xD61DA700, TdfType.UInt64, 6, true), // Tag: UAVG
        new TdfMemberInfo("MaximumUpsertRecordSize", "mMaximumUpsertRecordSize", 0xD6D87800, TdfType.UInt64, 7, true), // Tag: UMAX
        new TdfMemberInfo("MinimumUpsertRecordSize", "mMinimumUpsertRecordSize", 0xD6DA6E00, TdfType.UInt64, 8, true), // Tag: UMIN
        new TdfMemberInfo("NumOfUpsertRecords", "mNumOfUpsertRecords", 0xD6EB7200, TdfType.UInt32, 9, true), // Tag: UNMR
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _averageGetRecordSize = new(__typeInfos[0]);
    private TdfUInt64 _maximumGetRecordSize = new(__typeInfos[1]);
    private TdfUInt64 _minimumGetRecordSize = new(__typeInfos[2]);
    private TdfUInt32 _numOfGetRecords = new(__typeInfos[3]);
    private TdfUInt64 _bytesIn = new(__typeInfos[4]);
    private TdfUInt64 _bytesOut = new(__typeInfos[5]);
    private TdfUInt64 _averageUpsertRecordSize = new(__typeInfos[6]);
    private TdfUInt64 _maximumUpsertRecordSize = new(__typeInfos[7]);
    private TdfUInt64 _minimumUpsertRecordSize = new(__typeInfos[8]);
    private TdfUInt32 _numOfUpsertRecords = new(__typeInfos[9]);

    public DataRates()
    {
        __members = [ _averageGetRecordSize, _maximumGetRecordSize, _minimumGetRecordSize, _numOfGetRecords, _bytesIn, _bytesOut, _averageUpsertRecordSize, _maximumUpsertRecordSize, _minimumUpsertRecordSize, _numOfUpsertRecords ];
    }

    public override Tdf CreateNew() => new DataRates();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "DataRates";
    public override string GetFullClassName() => "Blaze::ByteVault::DataRates";

    public ulong AverageGetRecordSize
    {
        get => _averageGetRecordSize.Value;
        set => _averageGetRecordSize.Value = value;
    }

    public ulong MaximumGetRecordSize
    {
        get => _maximumGetRecordSize.Value;
        set => _maximumGetRecordSize.Value = value;
    }

    public ulong MinimumGetRecordSize
    {
        get => _minimumGetRecordSize.Value;
        set => _minimumGetRecordSize.Value = value;
    }

    public uint NumOfGetRecords
    {
        get => _numOfGetRecords.Value;
        set => _numOfGetRecords.Value = value;
    }

    public ulong BytesIn
    {
        get => _bytesIn.Value;
        set => _bytesIn.Value = value;
    }

    public ulong BytesOut
    {
        get => _bytesOut.Value;
        set => _bytesOut.Value = value;
    }

    public ulong AverageUpsertRecordSize
    {
        get => _averageUpsertRecordSize.Value;
        set => _averageUpsertRecordSize.Value = value;
    }

    public ulong MaximumUpsertRecordSize
    {
        get => _maximumUpsertRecordSize.Value;
        set => _maximumUpsertRecordSize.Value = value;
    }

    public ulong MinimumUpsertRecordSize
    {
        get => _minimumUpsertRecordSize.Value;
        set => _minimumUpsertRecordSize.Value = value;
    }

    public uint NumOfUpsertRecords
    {
        get => _numOfUpsertRecords.Value;
        set => _numOfUpsertRecords.Value = value;
    }

}
