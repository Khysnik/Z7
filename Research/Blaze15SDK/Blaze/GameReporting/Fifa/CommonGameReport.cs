using EATDF;
using EATDF.Members;

namespace Blaze15SDK.Blaze.GameReporting.Fifa;

public class CommonGameReport : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("AwayKitId", "awayKitId", 0x86BA6400, TdfType.Int32, 0, true), // Tag: AKID
        new TdfMemberInfo("AwaySubList", "awaySubList", 0x8738AC00, TdfType.List, 1, true), // Tag: ASBL
        new TdfMemberInfo("AwayStartingXI", "awayStartingXI", 0x873D3000, TdfType.List, 2, true), // Tag: ASTP
        new TdfMemberInfo("BallId", "ballId", 0x8ACA6400, TdfType.Int32, 3, true), // Tag: BLID
        new TdfMemberInfo("HomeKitId", "homeKitId", 0xA2BA6400, TdfType.Int32, 4, true), // Tag: HKID
        new TdfMemberInfo("HomeSubList", "homeSubList", 0xA338AC00, TdfType.List, 5, true), // Tag: HSBL
        new TdfMemberInfo("HomeStartingXI", "homeStartingXI", 0xA33D3000, TdfType.List, 6, true), // Tag: HSTP
        new TdfMemberInfo("IsRivalMatch", "isRivalMatch", 0xCB6B2D00, TdfType.Bool, 7, true), // Tag: RVLM
        new TdfMemberInfo("StadiumId", "stadiumId", 0xCF4A6400, TdfType.Int32, 8, true), // Tag: STID
        new TdfMemberInfo("WentToPk", "wentToPk", 0xDEEC2B00, TdfType.UInt16, 9, true), // Tag: WNPK
    ];
    private ITdfMember[] __members;

    private TdfInt32 _awayKitId = new(__typeInfos[0]);
    private TdfList<Substitution> _awaySubList = new(__typeInfos[1]);
    private TdfList<int> _awayStartingXI = new(__typeInfos[2]);
    private TdfInt32 _ballId = new(__typeInfos[3]);
    private TdfInt32 _homeKitId = new(__typeInfos[4]);
    private TdfList<Substitution> _homeSubList = new(__typeInfos[5]);
    private TdfList<int> _homeStartingXI = new(__typeInfos[6]);
    private TdfBool _isRivalMatch = new(__typeInfos[7]);
    private TdfInt32 _stadiumId = new(__typeInfos[8]);
    private TdfUInt16 _wentToPk = new(__typeInfos[9]);

    public CommonGameReport()
    {
        __members = [_awayKitId, _awaySubList, _awayStartingXI, _ballId, _homeKitId, _homeSubList, _homeStartingXI, _isRivalMatch, _stadiumId, _wentToPk];
    }

    public override Tdf CreateNew() => new CommonGameReport();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "CommonGameReport";
    public override string GetFullClassName() => "Blaze::GameReporting::Fifa::CommonGameReport";

    public int AwayKitId { get => _awayKitId.Value; set => _awayKitId.Value = value; }
    public IList<Substitution> AwaySubList { get => _awaySubList.Value; set => _awaySubList.Value = value; }
    public IList<int> AwayStartingXI { get => _awayStartingXI.Value; set => _awayStartingXI.Value = value; }
    public int BallId { get => _ballId.Value; set => _ballId.Value = value; }
    public int HomeKitId { get => _homeKitId.Value; set => _homeKitId.Value = value; }
    public IList<Substitution> HomeSubList { get => _homeSubList.Value; set => _homeSubList.Value = value; }
    public IList<int> HomeStartingXI { get => _homeStartingXI.Value; set => _homeStartingXI.Value = value; }
    public bool IsRivalMatch { get => _isRivalMatch.Value; set => _isRivalMatch.Value = value; }
    public int StadiumId { get => _stadiumId.Value; set => _stadiumId.Value = value; }
    public ushort WentToPk { get => _wentToPk.Value; set => _wentToPk.Value = value; }
}
