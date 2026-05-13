using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.Clubs;

public class ClubRival : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("RivalClubId", "mRivalClubId", 0x8ECA6400, TdfType.UInt64, 0, true), // Tag: CLID
        new TdfMemberInfo("CustOpt1", "mCustOpt1", 0x8EFC1100, TdfType.UInt64, 1, true), // Tag: COP1
        new TdfMemberInfo("CustOpt2", "mCustOpt2", 0x8EFC1200, TdfType.UInt64, 2, true), // Tag: COP2
        new TdfMemberInfo("CustOpt3", "mCustOpt3", 0x8EFC1300, TdfType.UInt64, 3, true), // Tag: COP3
        new TdfMemberInfo("CreationTime", "mCreationTime", 0x8F2D2900, TdfType.UInt32, 4, true), // Tag: CRTI
        new TdfMemberInfo("LastUpdateTime", "mLastUpdateTime", 0xB21D2900, TdfType.UInt32, 5, true), // Tag: LATI
        new TdfMemberInfo("MetaData", "mMetaData", 0xB65D2100, TdfType.String, 6, true), // Tag: META
    ];
    private ITdfMember[] __members;

    private TdfUInt64 _rivalClubId = new(__typeInfos[0]);
    private TdfUInt64 _custOpt1 = new(__typeInfos[1]);
    private TdfUInt64 _custOpt2 = new(__typeInfos[2]);
    private TdfUInt64 _custOpt3 = new(__typeInfos[3]);
    private TdfUInt32 _creationTime = new(__typeInfos[4]);
    private TdfUInt32 _lastUpdateTime = new(__typeInfos[5]);
    private TdfString _metaData = new(__typeInfos[6]);

    public ClubRival()
    {
        __members = [ _rivalClubId, _custOpt1, _custOpt2, _custOpt3, _creationTime, _lastUpdateTime, _metaData ];
    }

    public override Tdf CreateNew() => new ClubRival();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "ClubRival";
    public override string GetFullClassName() => "Blaze::Clubs::ClubRival";

    public ulong RivalClubId
    {
        get => _rivalClubId.Value;
        set => _rivalClubId.Value = value;
    }

    public ulong CustOpt1
    {
        get => _custOpt1.Value;
        set => _custOpt1.Value = value;
    }

    public ulong CustOpt2
    {
        get => _custOpt2.Value;
        set => _custOpt2.Value = value;
    }

    public ulong CustOpt3
    {
        get => _custOpt3.Value;
        set => _custOpt3.Value = value;
    }

    public uint CreationTime
    {
        get => _creationTime.Value;
        set => _creationTime.Value = value;
    }

    public uint LastUpdateTime
    {
        get => _lastUpdateTime.Value;
        set => _lastUpdateTime.Value = value;
    }

    public string MetaData
    {
        get => _metaData.Value;
        set => _metaData.Value = value;
    }

}
